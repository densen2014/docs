using AntdUI;
using MiniExcelLibs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using SignInMauiApp.Models;
using QuestPDF.Infrastructure;
using IContainer = QuestPDF.Infrastructure.IContainer;
using Colors = QuestPDF.Helpers.Colors;

namespace SignInWinApp;

public partial class SignInReportPage : AntdUI.Window
{
    private readonly IFreeSql? _fsql;
    private List<SignInReportItem> _report = new();
    int year => int.Parse(YearPicker.Text ?? DateTime.Now.Year.ToString());
    int month => int.Parse(MonthPicker.Text ?? DateTime.Now.Month.ToString());
    DateTime startDate => new DateTime(year, month, 1);
    DateTime endDate => startDate.AddMonths(1);
    private readonly User _user; 
    private AntList<SignInReportItem> antList = new AntList<SignInReportItem>();

    public SignInReportPage(User user)
    {
        InitializeComponent();
        Icon = Program.GetAppIcon();
        btnQuery.Click += (s, e) => LoadReport();
        btnToday.Click += (s, e) => LoadReport(today:true);
        //隐藏Excel导出按钮,简化界面
        btnExportExcel.Click += OnExportClicked;
        btnExportExcel.Visible = false;
        btnExportPDF.Click += OnExportPdfClicked;
        Header.BackClick += (s, e) => Close();
        ReportCollectionView.EmptyText = "No hay datos de informe disponibles";

        _fsql = Program.Fsql;
        _user = user;

        LoadUsernames();
        InitTableColumns();

        // 初始化年份和月份
        var years = Enumerable.Range(DateTime.Today.Year - 5, 11).Select(a => new SelectItem(a.ToString(), a)).ToArray();
        YearPicker.Items.AddRange(years);
        YearPicker.SelectedValue = DateTime.Today.Year;

        var months = Enumerable.Range(1, 12).Select(m => new SelectItem(m.ToString("D2"), m)).ToArray();
        MonthPicker.Items.AddRange(months);
        MonthPicker.SelectedValue = DateTime.Today.Month.ToString("D2");

        // 监听选择变化
        UsernamePicker.SelectedIndexChanged += (s, e) => LoadReport();
        YearPicker.SelectedIndexChanged += (s, e) => LoadReport();
        MonthPicker.SelectedIndexChanged += (s, e) => LoadReport();

        LoadReport();
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

    }
    private void InitTableColumns()
    {
        ReportCollectionView.Columns =
        [
            new Column("Username", "Trabajador",ColumnAlign.Center) { Width = "80", ColBreak=true },
            new Column("Day", "Día del mes",ColumnAlign.Center) { Width = "60" , ColBreak=true},
            new Column("MorningSignInTime", "Entrada de mañana",ColumnAlign.Center) { Width = "90", ColBreak=true, DisplayFormat="HH:mm"},
            new Column("MorningSignOutTime", "Salida de mañana",ColumnAlign.Center) { Width = "90", ColBreak=true, DisplayFormat="HH:mm" },
            new Column("AfternoonSignInTime", "Entrada de tarde",ColumnAlign.Center) { Width = "90", ColBreak=true, DisplayFormat="HH:mm" },
            new Column("AfternoonSignOutTime", "Salida de tarde",ColumnAlign.Center) { Width = "90", ColBreak=true, DisplayFormat="HH:mm" },
            new Column("TotalWorkHoursDisplay", "Total Horas Jornada",ColumnAlign.Center) { Width = "90", ColBreak=true },
            new Column("NormalWorkHoursDisplay", "Horas Ordinarias",ColumnAlign.Center) { Width = "90", ColBreak=true },
            new Column("ExtraWorkHoursDisplay", "Horas Complementarias",ColumnAlign.Center) { Width = "130", ColBreak=true },
            new Column("TenantName", "Empresa",ColumnAlign.Center) { Width = "120" },
        ];
        ////初始化表格列头
        //ReportCollectionView.StackedHeaderRows =
        //[
        //    new StackedHeaderRow(
        //            new StackedColumn("MorningSignInTime,MorningSignOutTime","Mañana").SetForeColor(System.Drawing.Color.Green),
        //            new StackedColumn("AfternoonSignInTime,AfternoonSignOutTime","Tarde").SetForeColor(System.Drawing.Color.Blue))
        //];
    }

    private void LoadUsernames()
    {
        UsernamePicker.Visible = _user.IsAdmin;
        UsernameLabel.Visible = _user.IsAdmin;

        if (_user.IsAdmin)
        {
            var users = _fsql!.Select<User>().Distinct().ToList(a => new SelectItem(a.Username.ToString(), a)).ToArray();
            UsernamePicker.Items.AddRange(users);
            UsernamePicker.SelectedIndex = 0;
        }
    }

    private void LoadReport(bool today=false)
    {

        var selectedUsername = _user.IsAdmin ? UsernamePicker.Text : _user.Username;
        if (_user.IsAdmin && today)
        {
            selectedUsername = null;
        }
        var records = _fsql!.Select<SignInRecord, User, Tenant>()
            .LeftJoin((r, u, t) => r.UserId == u.Id)
            .LeftJoin((r, u, t) => r.TenantId == t.Id)
            .WhereIf(!string.IsNullOrEmpty(selectedUsername), (r, u, t) => u.Username == selectedUsername)
            .WhereIf(!today ,(r, u, t) => r.SignInTime >= startDate && r.SignInTime < endDate)
            .WhereIf(today ,(r, u, t) => r.SignInTime >= DateTime.Today  && r.SignInTime < DateTime.Today.AddDays(1))
            .ToList((r, u, t) => new
            {
                r.UserId,
                Username = string.IsNullOrEmpty(u.Name) ? u.Username : u.Name,
                u.TaxNumber,
                u.WorkDuration,
                TenantName = t.Name,
                TenantTaxNumber = t.TaxNumber,
                r.SignInTime,
                r.SignType
            });
        _report = records
            .GroupBy(x => new { x.UserId, Date = x.SignInTime?.Date })
            .Select(g =>
            {
                var items = g.OrderBy(x => x.SignInTime).ToList();
                var morningSignIn = items.FirstOrDefault(x => x.SignInTime?.Hour < 16)?.SignInTime;
                var morningSignOut = items.LastOrDefault(x => x.SignInTime?.Hour < 16)?.SignInTime;
                var afternoonSignIn = items.FirstOrDefault(x => x.SignInTime?.Hour >= 16)?.SignInTime;
                var afternoonSignOut = items.LastOrDefault(x => x.SignInTime?.Hour >= 16)?.SignInTime;
                if (morningSignIn == morningSignOut)
                {
                    morningSignOut = null;
                }
                if (afternoonSignIn == afternoonSignOut)
                {
                    afternoonSignOut = null;
                }
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
                TimeSpan normal = TimeSpan.FromHours(items.First().WorkDuration);
                TimeSpan? extra = null;
                if (total.HasValue)
                {
                    extra = total > normal ? total - normal : TimeSpan.Zero;
                }

                return new SignInReportItem
                {
                    Username = items.First().Username,
                    TaxNumber = items.First().TaxNumber,
                    TenantName = items.First().TenantName,
                    TenantTaxNumber = items.First().TenantTaxNumber,
                    MorningSignInTime = morningSignIn,
                    MorningSignOutTime = morningSignOut,
                    AfternoonSignInTime = afternoonSignIn,
                    AfternoonSignOutTime = afternoonSignOut,
                    TotalWorkDuration = total,
                    NormalWorkDuration = normal,
                    ExtraWorkDuration = extra,
                    SignInTime = items.FirstOrDefault()?.SignInTime,
                    SignOutTime = items.LastOrDefault()?.SignInTime
                };
            })
            .OrderBy(x => x.MorningSignInTime ?? x.AfternoonSignInTime)
            .ToList();
        antList.Clear();
        antList.AddRange(_report);
        ReportCollectionView.Binding(antList);
    } 

    private async void OnExportClicked(object? sender, EventArgs e)
    {
        if (_report.Count == 0)
        {
            Program.DisplayAlert("Aviso", "No hay datos para exportar", "Aceptar");
            return;
        }
        var fileName = $"Informe_de_registro_de_jornada_laboral_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
        await MiniExcel.SaveAsAsync(filePath, _report);
        if (!File.Exists(filePath))
        {
            Program.DisplayAlert("Error", "El archivo no se generó y no se puede compartir.", "Aceptar");
            return;
        }
        await OpenFileAndFolder(filePath);
        return;
    }

    private async void OnExportPdfClicked(object? sender, EventArgs e)
    {
        if (_report.Count == 0)
        {
            Program.DisplayAlert("Aviso", "No hay datos para exportar", "OK");
            return;
        }
        var fileName = $"Informe_de_registro_de_jornada_laboral_{DateTime.Now:yyyyMMddHHmmss}.pdf";
        var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
        var report = _report;
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
#if NET48
                page.DefaultTextStyle(x => x 
                    .FontSize(11f)
                );
#endif
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
                        var first = report.FirstOrDefault();
                        row.RelativeItem().Element(CellStyleZero).Column(c =>
                        {
                            c.Item().Element(CellStyle).Text($"Nombre o Razón Social: {first?.TenantName ?? ""}");
                            c.Item().Element(CellStyle).Text($"CIF: {first?.TenantTaxNumber ?? ""}");
                            c.Item().Element(CellStyle).Text("C.C.C.:");
                        });
                        row.RelativeItem().Element(CellStyleZero).Column(c =>
                        {
                            c.Item().Element(CellStyle).Text($"Nombre: {first?.Username ?? ""}");
                            c.Item().Element(CellStyle).Text($"NIF: {first?.TaxNumber ?? ""}");
                            c.Item().Element(CellStyle).Text("NAF:");
                        });
                    });

                    // 结算周期行
                    var _endDate = endDate.AddMinutes(-1);
                    col.Item().Row(row =>
                    {
                        row.RelativeItem(1.5F).Element(CellStyle).Text("Período de liquidación:").Bold();
                        row.RelativeItem(3).Element(CellStyle).Text($"{startDate:dd} al {_endDate:dd} de {_endDate:MM} de {_endDate:yyyy}").AlignCenter();
                        row.RelativeItem(1.5F).Element(CellStyle).Text("Fecha:").Bold();
                        row.RelativeItem(3).Element(CellStyle).Text($"{DateTime.Now:dd} de {DateTime.Now:MM} de {DateTime.Now:yyyy}").AlignCenter();
                    });

                    // 表格
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(42); // Día del mes
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
                            header.Cell().RowSpan(2).Element(CellStyle).Text("Día del mes").FontSize(11).AlignCenter().Bold();
                            header.Cell().ColumnSpan(2).Element(CellStyle).Text("Hora de Entrada").AlignCenter().Bold();
                            header.Cell().ColumnSpan(2).Element(CellStyle).Text("Hora de Salida").AlignCenter().Bold();
                            header.Cell().RowSpan(2).Element(CellStyle).Text("Total Horas Jornada").AlignCenter().Bold();
                            header.Cell().RowSpan(2).Element(CellStyle).Text("Horas Ordinarias").AlignCenter().Bold();
                            header.Cell().RowSpan(2).Element(CellStyle).Text("Horas Complementarias").AlignCenter().Bold();

                            // 第二行
                            header.Cell().Element(CellStyle).Text("Mañana").AlignCenter();
                            header.Cell().Element(CellStyle).Text("Tarde").AlignCenter();
                            header.Cell().Element(CellStyle).Text("Mañana").AlignCenter();
                            header.Cell().Element(CellStyle).Text("Tarde").AlignCenter();
                        });
                        // 内容行
                        for (int i = 1; i <= DateTime.DaysInMonth(startDate.Year, startDate.Month); i++)
                        {
                            // 查找当天的数据
                            var item = report.FirstOrDefault(x =>
                                (x.MorningSignInTime?.Day == i) ||
                                (x.AfternoonSignInTime?.Day == i)
                            );

                            table.Cell().Element(CellStyle).Text(i.ToString()).AlignCenter();
                            table.Cell().Element(CellStyle).Text(item?.MorningSignInTime?.ToString("HH:mm") ?? "").AlignCenter();
                            table.Cell().Element(CellStyle).Text(item?.AfternoonSignInTime?.ToString("HH:mm") ?? "").AlignCenter();
                            table.Cell().Element(CellStyle).Text(item?.MorningSignOutTime?.ToString("HH:mm") ?? "").AlignCenter();
                            table.Cell().Element(CellStyle).Text(item?.AfternoonSignOutTime?.ToString("HH:mm") ?? "").AlignCenter();
                            table.Cell().Element(CellStyle).Text(item?.TotalWorkHoursDisplay).AlignCenter();
                            table.Cell().Element(CellStyle).Text(item?.NormalWorkHoursDisplay).AlignCenter();
                            table.Cell().Element(CellStyle).Text(item?.ExtraWorkHoursDisplay).AlignCenter();
                        }
                    });
                    col.Item().Row(row =>
                    {
                        row.RelativeItem(3).Element(CellStyle).Text("Total Horas Jornada:").Bold();
                        row.RelativeItem(3).Element(CellStyle).Text(report.Sum(a => (a.TotalWorkDuration ?? TimeSpan.Zero).TotalHours).ToString("0.00")).AlignCenter();
                    });
                });
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Fecha de exportación: ");
                    x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                });
            });
        });
        document.GeneratePdfAndShow();
        //document.GeneratePdf(filePath);
        //if (!File.Exists(filePath))
        //{
        //    Program.DisplayAlert("Error", "El PDF no se generó, no se puede compartir", "OK");
        //    return;
        //}
        //await OpenFileAndFolder(filePath);

        static IContainer CellStyle(IContainer container) =>
            container
                .Border(1)
                .BorderColor(Colors.Black)
                .Padding(2);
        static IContainer CellStyleZero(IContainer container) =>
            container
                .Border(1)
                .BorderColor(Colors.Black);
    }

    private async Task OpenFileAndFolder(string filePath)
    {
        var folder = Path.GetDirectoryName(filePath);
        var file = Path.GetFileName(filePath);
        if (folder != null)
        {
            // 打开文件夹并选中文件
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"/select,\"{filePath}\"",
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }
        // 打开文件
        var psi2 = new System.Diagnostics.ProcessStartInfo
        {
            FileName = filePath,
            UseShellExecute = true
        };
        System.Diagnostics.Process.Start(psi2);
    }

}
