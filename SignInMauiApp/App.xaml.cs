// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using KestrelWebHost;
using MauiWebApi;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Net; 

namespace SignInMauiApp;

public partial class App : Application
{
    [NotNull]
    public static IWebHost? Host { get; set; }
    public static WebHostParameters WebHostParameters { get; set; } = new WebHostParameters();

    public App()
    {
        InitializeComponent();
        InitializeData();
        Task.Run(InitializeWebHostAsync);
    }

    private void InitializeData()
    {
        var fsql = MauiProgram.CreateMauiApp().Services.GetService<IFreeSql>();
        if (fsql == null)
        {
            return;
        }
        // 初始化租户
        if (!fsql.Select<Models.Tenant>().Any())
        {
            Preferences.Set("OnboardingDone", false);
            //var tenant = new Models.Tenant { Name = "我的公司" };
            //fsql.Insert(tenant).ExecuteAffrows();
            // 初始化用户
            //fsql.Insert(new Models.User { Username = "admin", Password = "123456", TenantId = tenant.Id }).ExecuteAffrows();
        }
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());
        window.Title = "Fichaje";
        return window;
    }

    private async Task InitializeWebHostAsync()
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

    private void Log(string message)
    {
        System.Diagnostics.Debug.WriteLine($"[App] {message}");
    }
}
