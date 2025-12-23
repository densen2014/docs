using MiniExcelLibs;
using SignInMauiApp.Models;
using System.Text;
using IContainer = QuestPDF.Infrastructure.IContainer;
using Colors = QuestPDF.Helpers.Colors;


#if ANDROID || WINDOWS || MACCATALYST
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
#endif

namespace SignInMauiApp;

public partial class SignInReportPage : ContentPage
{
    private readonly IFreeSql? _fsql;
    private List<SignInReportItem> _report = new();

    public SignInReportPage()
    {
        InitializeComponent();
        _fsql = IPlatformApplication.Current?.Services.GetService<IFreeSql>();
        StartDatePicker.Date = DateTime.Today;
        EndDatePicker.Date = DateTime.Today;
        LoadReport();
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
    }

    private void LoadReport()
    {
        var start = StartDatePicker.Date;
        var end = EndDatePicker.Date!.Value.AddDays(1);
        _report = _fsql!.Select<SignInRecord, User, Tenant>()
            .LeftJoin((r, u, t) => r.UserId == u.Id)
            .LeftJoin((r, u, t) => r.TenantId == t.Id)
            .Where((r, u, t) => r.SignInTime >= start && r.SignInTime <= end)
            .ToList((r, u, t) => new SignInReportItem()
            {
                Username = u.Username,
                TenantName = t.Name,
                SignInTime = r.SignInTime,
                SignType = r.SignType,
            });
        ReportCollectionView.ItemsSource = _report;
    }

    private void OnQueryClicked(object sender, EventArgs e)
    {
        LoadReport();
    }

    private async void OnExportClicked(object sender, EventArgs e)
    {
        if (_report.Count == 0)
        {
            await DisplayAlertAsync("提示", "无数据可导出", "确定");
            return;
        }
        var fileName = $"签到报表_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
        await MiniExcel.SaveAsAsync(filePath, _report);
        if (!File.Exists(filePath))
        {
            await DisplayAlertAsync("错误", "文件未生成，无法分享", "确定");
            return;
        }
        try
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "导出签到报表",
                File = new ShareFile(filePath),
            });
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("错误", ex.Message, "确定");
        }
    }

    private async void OnExportPdfClicked(object sender, EventArgs e)
    {
#if ANDROID || WINDOWS || MACCATALYST
        if (_report.Count == 0)
        {
            await DisplayAlertAsync("提示", "无数据可导出", "确定");
            return;
        }
        var fileName = $"签到报表_{DateTime.Now:yyyyMMddHHmmss}.pdf";
        var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
        var report = _report;
        // 生成PDF
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Header().Text("签到历史报表").FontSize(20).Bold().AlignCenter();
                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(80); // 用户名
                        columns.ConstantColumn(80); // 公司
                        columns.ConstantColumn(80); // 类型
                        columns.RelativeColumn();   // 时间
                    });
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("用户名");
                        header.Cell().Element(CellStyle).Text("公司");
                        header.Cell().Element(CellStyle).Text("类型");
                        header.Cell().Element(CellStyle).Text("时间");
                    });
                    foreach (var item in report)
                    {
                        table.Cell().Element(CellStyle).Text(item.Username);
                        table.Cell().Element(CellStyle).Text(item.TenantName);
                        table.Cell().Element(CellStyle).Text(item.SignType.ToString());
                        table.Cell().Element(CellStyle).Text(item.SignInTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                });
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("导出时间: ");
                    x.Span(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }); 
            });
        });
        document.GeneratePdf(filePath);
        if (!File.Exists(filePath))
        {
            await DisplayAlertAsync("错误", "PDF未生成，无法分享", "确定");
            return;
        }
        try
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "导出签到报表PDF",
                File = new ShareFile(filePath),
            });
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("错误", ex.Message, "确定");
        }
        static IContainer CellStyle(IContainer container) => container.PaddingVertical(2).PaddingHorizontal(4).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
#else
        await DisplayAlertAsync("提示", "当前平台暂不支持PDF导出", "确定");
#endif

    }

    public async Task ShareText(string text)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = text,
            Title = "Share Text"
        });
    }

    public async Task ShareUri(string uri, IShare share)
    {
        await share.RequestAsync(new ShareTextRequest
        {
            Uri = uri,
            Title = "Share Web Link"
        });
    }
    public async Task ShareFile(string filename)
    {
        string fn = "Attachment.txt";
        string file = Path.Combine(FileSystem.CacheDirectory, filename);

        File.WriteAllText(file, "Hello World");

        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = "Share text file",
            File = new ShareFile(file)
        });
    }
    public async Task ShareMultipleFiles()
    {
        string file1 = Path.Combine(FileSystem.CacheDirectory, "Attachment1.txt");
        string file2 = Path.Combine(FileSystem.CacheDirectory, "Attachment2.txt");

        File.WriteAllText(file1, "Content 1");
        File.WriteAllText(file2, "Content 2");

        await Share.Default.RequestAsync(new ShareMultipleFilesRequest
        {
            Title = "Share multiple files",
            Files = new List<ShareFile> { new ShareFile(file1), new ShareFile(file2) }
        });
    }
}

