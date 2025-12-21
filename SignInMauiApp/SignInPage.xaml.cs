using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using SignInMauiApp.Models;
using FreeSql;
using System.Diagnostics.CodeAnalysis;

namespace SignInMauiApp;

public partial class SignInPage : ContentPage
{
    private readonly IFreeSql? _fsql;
    private readonly User _user;
    private readonly Tenant _tenant;

    public SignInPage(User user, Tenant tenant)
    {
        InitializeComponent();
        _fsql = IPlatformApplication.Current?.Services.GetService<IFreeSql>();
        _user = user;
        _tenant = tenant;
        WelcomeLabel.Text = $"欢迎 {_user.Username}，租户：{_tenant.Name}";
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        var record = new SignInRecord
        {
            UserId = _user.Id,
            TenantId = _tenant.Id,
            SignInTime = DateTime.Now
        };
        await _fsql!.Insert(record).ExecuteAffrowsAsync();
        SignInResultLabel.Text = $"签到成功：{record.SignInTime:yyyy-MM-dd HH:mm:ss}";
        SignInResultLabel.IsVisible = true;
    }
}
