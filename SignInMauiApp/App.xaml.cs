// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using KestrelWebHost;
using MauiWebApi;
using Microsoft.AspNetCore.Hosting;
using SignInMauiApp.Models;
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
        // 初始化数据库
        var tables = fsql.DbFirst.GetTablesByDatabase();
        var tableNames = tables.Select(t => t.Name).ToList();
        var missingTables = new List<string> { nameof(Tenant), nameof(User), nameof(SignInRecord) }
            .Where(t => !tableNames.Contains(t)).ToList();  
        if (missingTables.Count > 0)
        {
            fsql.CodeFirst.SyncStructure(typeof(Tenant), typeof(User), typeof(SignInRecord));
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
