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
#if WINDOWS
using System.Security.AccessControl;
using System.Security.Principal;
using System.Xml.Linq;
#endif 

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
        // 解决权限问题
#if WINDOWS
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
#endif 
        var tableNames = tables.Select(t => t.Name).ToList();
        var missingTables = new List<string> { nameof(Tenant), nameof(User), nameof(SignInRecord), nameof(DbVersion) }
            .Where(t => !tableNames.Contains(t)).ToList();
        if (missingTables.Count > 0)
        {
            fsql.CodeFirst.SyncStructure(typeof(Tenant), typeof(User), typeof(SignInRecord), typeof(DbVersion));
            if (fsql.Select<DbVersion>().Count() == 0)
            {
                fsql.Insert<DbVersion>().AppendData(new DbVersion() { Version = 0 }).ExecuteAffrows();
            }
        }
        CheckAndUpgradeDatabase(fsql);
    }

    private static void CheckAndUpgradeDatabase(IFreeSql fsql)
    {
        // 检查并执行数据库升级
        DbVersion vers = new();
        int verFinal = 1;
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
