using FreeSql;
using KestrelWebHost;
using MauiWebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using SignInMauiApp.Models;
using System.Globalization;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;

namespace SignInWinApp;

internal static class Program
{
    public static IFreeSql? Fsql;
    public static ILoggerFactory? LoggerFactory;
    public static ILogger? Logger;
    private static LoginPage? loginPage;
    public static IWebHost? Host { get; set; }
    public static WebHostParameters WebHostParameters { get; set; } = new WebHostParameters();

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

        var cultureInfo = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        // 初始化 LoggerFactory，使用 Debug 输出
        LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.AddDebug();
        });
        Logger = LoggerFactory.CreateLogger("Startup");
        Logger.LogInformation("程序启动，初始化 FreeSql...");

        // 初始化 FreeSql ORM，使用 SQLite 数据库
        Fsql = new FreeSqlBuilder()
#if DEBUG
            //.UseAutoSyncStructure(true)
#endif
            .UseConnectionString(DataType.Sqlite, "Data Source=signindb.db")
            .Build();
        Logger.LogInformation("FreeSql 初始化完成。");

        InitializeData();
        Task.Run(InitializeWebHostAsync);

        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        AntdUI.Config.Theme()
            .Light("#fff", "#000")  
            .Dark("#000", "#fff")  
            .Header("#f3f3f3", "#111111"); 

        AntdUI.Config.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        AntdUI.Config.TextRenderingHighQuality = true;
        AntdUI.Config.ShowInWindow = true;
#if NET48
        float dpi = AntdUI.Config.Dpi;
        AntdUI.Config.SetDpi(dpi);
#endif 
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        loginPage = new LoginPage();
        Application.Run(loginPage);
    }

    static void InitializeData()
    {
        if (Fsql == null)
        {
            return;
        }
        // 初始化数据库
        var tables = Fsql.DbFirst.GetTablesByDatabase();
        // 解决权限问题
        var dbPath = Path.Combine(AppContext.BaseDirectory, "signindb.db");

        if (File.Exists(dbPath))
        {
            var fileInfo = new FileInfo(dbPath);
            var security = fileInfo.GetAccessControl();
            security.AddAccessRule(new FileSystemAccessRule(
                WindowsIdentity.GetCurrent().User!,
                FileSystemRights.FullControl,
                AccessControlType.Allow));
            fileInfo.SetAccessControl(security);
        }
        var tableNames = tables.Select(t => t.Name).ToList();
        var missingTables = new List<string> { nameof(Tenant), nameof(User), nameof(SignInRecord), nameof(DbVersion) }
            .Where(t => !tableNames.Contains(t)).ToList();
        if (missingTables.Count > 0)
        {
            Fsql.CodeFirst.SyncStructure(typeof(Tenant), typeof(User), typeof(SignInRecord), typeof(DbVersion));
            if (Fsql.Select<DbVersion>().Count() == 0)
            {
                Fsql.Insert<DbVersion>().AppendData(new DbVersion() { Version = 0 }).ExecuteAffrows();
            }
        }
        CheckAndUpgradeDatabase(Fsql);
    }

    private static void CheckAndUpgradeDatabase(IFreeSql fsql)
    {
        // 检查并执行数据库升级
        DbVersion vers = new();
        int verFinal = 2;
        try
        {
            vers = fsql.Select<DbVersion>().OrderBy(a => a.Id).First();
        }
        catch (Exception)
        {
            fsql.CodeFirst.SyncStructure<DbVersion>();
            if (fsql.Select<DbVersion>().Count() == 0)
            {
                fsql.Insert<DbVersion>().AppendData(new DbVersion() { Version = 0 }).ExecuteAffrows();
            }
            vers = fsql.Select<DbVersion>().OrderBy(a => a.Id).First();
        }
        // 数据库升级, 比对当前数据库版本号
        while (vers.Version < verFinal)
        {
            try
            {
                switch (vers.Version)
                {
                    case 0:
                        fsql.CodeFirst.SyncStructure(typeof(User));
                        fsql.Update<User>().Set(a => a.WorkDuration, 7.5f).Where(a => a.WorkDuration == 8f).ExecuteAffrows();
                        vers.Version = 1;
                        break;
                    case 1:
                        var list = fsql.Select<SignInRecord>().ToList();
                        list.ForEach(a =>
                        {
                            if (a.SignInTime != null)
                            {
                                a.SignInTime = new DateTime(a.SignInTime.Value.Year, a.SignInTime.Value.Month, a.SignInTime.Value.Day, a.SignInTime.Value.Hour, a.SignInTime.Value.Minute, 0);
                            }
                        });
                        fsql.Update<SignInRecord>().SetSource(list).ExecuteAffrows();
                        vers.Version = 2;
                        break;
                }
            }
            catch (Exception)
            {
                vers.Version += 1;
            }
            fsql.Update<DbVersion>().SetSource(vers).UpdateColumns(a => a.Version).ExecuteAffrows();
            vers = fsql.Select<DbVersion>().OrderBy(a => a.Id).First();
        }
    }

    private static async Task InitializeWebHostAsync()
    {
        try
        {
            var ip = NetworkHelper.GetIpAddress() ?? IPAddress.Loopback;
            WebHostParameters.ServerIpEndpoint = new IPEndPoint(ip, 5001);

            Log($"监听地址: {WebHostParameters.ServerIpEndpoint}");
            await KestrelWebHost.WebHostProgram.WebHostMain(WebHostParameters);
        }
        catch (Exception ex)
        {
            Log($"######## EXCEPTION: {ex.Message}");
        }
    }
    private static void Log(string message)
    {
        System.Diagnostics.Debug.WriteLine($"[App] {message}");
    }

    internal static bool DisplayAlert(string v1, string v2, string v3, string? v4 = null)
    {
        return MessageBox.Show(v2, v1, MessageBoxButtons.YesNo) == DialogResult.Yes;
    }

    internal static Image? GetLogoImage()
    {
        var logoPath = Path.Combine(AppContext.BaseDirectory, "logo.jpg");
        if (File.Exists(logoPath))
        {
            return Image.FromFile(logoPath);
        }
        else
        {
            return null;
        }
    }

    // 捕获UI线程中的未处理异常
    static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
        AntdUI.Notification.error(loginPage!, "未处理的UI线程异常", e.Exception.Message, autoClose: 3, align: AntdUI.TAlignFrom.TR);
    }

    // 捕获非UI线程中的未处理异常
    static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        AntdUI.Notification.error(loginPage!, "未处理的非UI线程异常", e.ToString()!, autoClose: 3, align: AntdUI.TAlignFrom.TR);
    }

    internal static Icon GetAppIcon()
    {
        var iconPath = Path.Combine(AppContext.BaseDirectory, "Registro_de_Jornada.ico");
        if (File.Exists(iconPath))
        {
            return new Icon(iconPath);
        }
        else
        {
            // Optionally, return a default icon or null
            return SystemIcons.Application;
        }
    }
}

public static class Ext
{

    public static string? IsNull(this string str, string defaultValue)
    {
        return string.IsNullOrEmpty(str) ? defaultValue : str?.Trim();
    }
}
