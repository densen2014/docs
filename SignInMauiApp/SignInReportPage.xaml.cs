using MiniExcelLibs;
using SignInMauiApp.Models;
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
        StartDatePicker.Date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        EndDatePicker.Date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
        LoadUsernames();
        LoadReport();
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
    }

    private void LoadUsernames()
    {
        var users = _fsql!.Select<User>().ToList();
        UsernamePicker.ItemsSource = users.Select(u => u.Username).Distinct().ToList();
        UsernamePicker.SelectedIndex = 0;
    }

    private void LoadReport()
    {
        var start = StartDatePicker.Date;
        var end = EndDatePicker.Date!.Value.AddDays(1);
        var selectedUsername = UsernamePicker.SelectedItem as string;
        var records = _fsql!.Select<SignInRecord, User, Tenant>()
            .LeftJoin((r, u, t) => r.UserId == u.Id)
            .LeftJoin((r, u, t) => r.TenantId == t.Id)
            .Where((r, u, t) => r.SignInTime >= start && r.SignInTime < end)
            .ToList((r, u, t) => new {
                r.UserId,
                Username = string.IsNullOrEmpty(u.Name) ? u.Username : u.Name,
                u.TaxNumber,        
                TenantName = t.Name,
                r.SignInTime,
                r.SignType
            });
        if (!string.IsNullOrEmpty(selectedUsername))
        {
            records = records.Where(x => x.Username == selectedUsername).ToList();
        }
        _report = records
            .GroupBy(x => new { x.UserId, Date = x.SignInTime?.Date })
            .Select(g =>
            {
                var items = g.OrderBy(x => x.SignInTime).ToList();
                var morningSignIn = items.FirstOrDefault(x => x.SignInTime?.Hour < 12)?.SignInTime;
                var morningSignOut = items.LastOrDefault(x => x.SignInTime?.Hour < 12)?.SignInTime;
                var afternoonSignIn = items.FirstOrDefault(x => x.SignInTime?.Hour >= 12)?.SignInTime;
                var afternoonSignOut = items.LastOrDefault(x => x.SignInTime?.Hour >= 12)?.SignInTime;
                // 计算工时
                TimeSpan? total = null;
                if (morningSignIn.HasValue && morningSignOut.HasValue && morningSignOut > morningSignIn)
                {
                    total = (morningSignOut - morningSignIn);
                }

                if (afternoonSignIn.HasValue && afternoonSignOut.HasValue && afternoonSignOut > afternoonSignIn)
                {
                    total = (total ?? TimeSpan.Zero) + (afternoonSignOut - afternoonSignIn);
                }
                // 正常工时8小时，补充工时为超出部分
                TimeSpan normal = TimeSpan.FromHours(8);
                TimeSpan? extra = null;
                if (total.HasValue)
                {
                    extra = total > normal ? total - normal : TimeSpan.Zero;
                }

                return new SignInReportItem
                {
                    Username = items.First().Username,
                    TenantName = items.First().TenantName,
                    MorningSignInTime = morningSignIn,
                    MorningSignOutTime = morningSignOut,
                    AfternoonSignInTime = afternoonSignIn,
                    AfternoonSignOutTime = afternoonSignOut,
                    TotalWorkDuration = total,
                    NormalWorkDuration = total.HasValue ? (total > normal ? normal : total) : null,
                    ExtraWorkDuration = extra,
                    SignInTime = items.FirstOrDefault()?.SignInTime,
                    SignOutTime = items.LastOrDefault()?.SignInTime
                };
            })
            .OrderByDescending(x => x.MorningSignInTime ?? x.AfternoonSignInTime)
            .ToList();
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
        var fileName = $"Informe_de_registro_de_jornada_laboral_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
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
                Title = "Exportar informe",
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
            await DisplayAlertAsync("Aviso", "No hay datos para exportar", "OK");
            return;
        }
        var fileName = $"Informe_de_registro_de_jornada_laboral_{DateTime.Now:yyyyMMddHHmmss}.pdf";
        var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
        var report = _report;
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.Content().Column(col =>
                {
                    // 顶部大标题
                    col.Item().Element(CellStyle).Text("REGISTRO DIARIO DE JORNADA EN TRABAJADORES A TIEMPO COMPLETO")
                        .FontSize(12).Bold().AlignCenter();

                    // EMPRESA / TRABAJADOR 行
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Element(CellStyle).Text("EMPRESA").Bold().AlignCenter();
                        row.RelativeItem().Element(CellStyle).Text("TRABAJADOR").Bold().AlignCenter();
                    });

                    // 公司/员工信息行
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Element(CellStyleZero).Column(c =>
                        {
                            c.Item().Element(CellStyle).Text("Nombre o Razón Social:");
                            c.Item().Element(CellStyle).Text("CIF:");
                            c.Item().Element(CellStyle).Text("C.C.C.:");
                        });
                        row.RelativeItem().Element(CellStyleZero).Column(c =>
                        {
                            c.Item().Element(CellStyle).Text("Nombre:");
                            c.Item().Element(CellStyle).Text("NIF:");
                            c.Item().Element(CellStyle).Text("NAF:");
                        });
                    });

                    // 结算周期行
                    col.Item().Row(row =>
                    {
                        row.RelativeItem(1.5F).Element(CellStyle).Text("Período de liquidación:").Bold();
                        row.RelativeItem(3).Element(CellStyle).Text("1 al --- de ------- de 20--");
                        row.RelativeItem(1.5F).Element(CellStyle).Text("Fecha:").Bold();
                        row.RelativeItem(3).Element(CellStyle).Text("--- de ---------- de 20--");
                    });

                    // 表格
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40); // Día del mes
                            columns.RelativeColumn(1);  // Mañana Entrada
                            columns.RelativeColumn(1);  // Tarde Entrada
                            columns.RelativeColumn(1);  // Mañana Salida
                            columns.RelativeColumn(1);  // Tarde Salida
                            columns.RelativeColumn(1.2f); // Total Horas Jornada
                            columns.RelativeColumn(1.2f); // Horas Ordinarias
                            columns.RelativeColumn(1.2f); // Horas Complementarias
                        });
                        // 多级表头
                        table.Header(header =>
                        {
                            header.Cell().RowSpan(2).Element(CellStyle).Text("Día\ndel\nmes").AlignCenter().Bold();
                            header.Cell().ColumnSpan(2).Element(CellStyle).Text("Hora de Entrada").AlignCenter().Bold();
                            header.Cell().ColumnSpan(2).Element(CellStyle).Text("Hora de Salida").AlignCenter().Bold();
                            header.Cell().RowSpan(2).Element(CellStyle).Text("Total\nHoras\nJornada").AlignCenter().Bold();
                            header.Cell().RowSpan(2).Element(CellStyle).Text("Horas\nOrdinarias").AlignCenter().Bold();
                            header.Cell().RowSpan(2).Element(CellStyle).Text("Horas\nComplementarias").AlignCenter().Bold();

                            // 第二行
                            header.Cell().Element(CellStyle).Text("Mañana").AlignCenter();
                            header.Cell().Element(CellStyle).Text("Tarde").AlignCenter();
                            header.Cell().Element(CellStyle).Text("Mañana").AlignCenter();
                            header.Cell().Element(CellStyle).Text("Tarde").AlignCenter();
                        });
                        // 内容行
                        for (int i = 1; i <= 31; i++)
                        {
                            table.Cell().Element(CellStyle).Text(i.ToString()).AlignCenter();
                            for (int j = 0; j < 7; j++)
                            {
                                table.Cell().Element(CellStyle).Text(""); // 其余单元格留空或填数据
                            }
                        }
                    });
                });
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Fecha de exportación: ");
                    x.Span(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                });
            });
        });
        document.GeneratePdf(filePath);
        if (!File.Exists(filePath))
        {
            await DisplayAlertAsync("Error", "El PDF no se generó, no se puede compartir", "OK");
            return;
        }
        try
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Exportar informe en PDF",
                File = new ShareFile(filePath),
            });
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", ex.Message, "OK");
        }
        static IContainer CellStyle(IContainer container) =>
            container 
                .Border(1)
                .BorderColor(Colors.Black)
                .Padding(2);
        static IContainer CellStyleZero(IContainer container) =>
            container 
                .Border(1)
                .BorderColor(Colors.Black);
#else
    await DisplayAlertAsync("Aviso", "La exportación a PDF no es compatible en esta plataforma", "OK");
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

