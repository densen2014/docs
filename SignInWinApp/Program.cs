using FreeSql;
using Microsoft.Extensions.Logging;
using SignInMauiApp.Models;
using System.Diagnostics.CodeAnalysis;

namespace SignInWinApp;

internal static class Program
{
    public static IFreeSql? Fsql;
    public static ILoggerFactory? LoggerFactory;
    public static ILogger? Logger;
    private static LoginPage? loginPage;

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

        // 初始化 LoggerFactory，使用 Debug 输出
        LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.AddDebug();
        });
        Logger = LoggerFactory.CreateLogger("Startup");
        Logger.LogInformation("程序启动，初始化 FreeSql...");

        // 初始化 FreeSql ORM，使用 SQLite 数据库
        Fsql = new FreeSqlBuilder()
            .UseAutoSyncStructure(true)
            .UseConnectionString(DataType.Sqlite, "Data Source=signindb.db")
            .Build();
        Logger.LogInformation("FreeSql 初始化完成。");

        InitializeData();

        AntdUI.Localization.DefaultLanguage = "zh-CN";
        //若文字不清晰，切换其他渲染方式
        AntdUI.Config.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        AntdUI.Config.TextRenderingHighQuality = true;

        //ApplicationConfiguration.Initialize();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        loginPage = new LoginPage();
        Application.Run(loginPage);
    }

    static void InitializeData()
    {
        if (Fsql == null) return;
        // 检查是否有租户
        if (!Fsql.Select<SignInMauiApp.Models.Tenant>().Any())
        {
            Preferences.Set("OnboardingDone", false);
            //var tenant = new SignInMauiApp.Models.Tenant { Name = "我的公司" };
            //tenant.Id = (int)Fsql.Insert(tenant).ExecuteIdentity();
            //// 初始化用户
            //var user = new SignInMauiApp.Models.User { Username = "admin", Password = "123456", TenantId = tenant.Id };
            //Fsql.Insert(user).ExecuteAffrows();
            //Logger?.LogInformation("已初始化默认租户和管理员用户。");
        }
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
}

public static class Ext
{

    public static string? IsNull(this string str, string defaultValue)
    {
        return string.IsNullOrEmpty(str) ? defaultValue : str?.Trim();
    }
}
