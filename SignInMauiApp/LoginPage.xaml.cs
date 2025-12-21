using Microsoft.Maui.Controls;
using SignInMauiApp.Models;
using FreeSql;

namespace SignInMauiApp;

public partial class LoginPage : ContentPage
{
    private readonly IFreeSql? _fsql;
    private List<Tenant> _tenants = new();

    public LoginPage()
    {
        InitializeComponent();
        _fsql = IPlatformApplication.Current?.Services.GetService<IFreeSql>();
        LoadTenants();
    }

    private void LoadTenants()
    {
        _tenants = _fsql!.Select<Tenant>().ToList();
        TenantPicker.ItemsSource = _tenants.Select(t => t.Name).ToList();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text;
        var tenantIdx = TenantPicker.SelectedIndex;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || tenantIdx < 0)
        {
            ErrorLabel.Text = "请填写完整信息";
            ErrorLabel.IsVisible = true;
            return;
        }
        var tenantId = _tenants[tenantIdx].Id;
        var user = _fsql!.Select<User>().Where(u => u.Username == username && u.Password == password && u.TenantId == tenantId).First();
        if (user == null)
        {
            ErrorLabel.Text = "用户名或密码错误";
            ErrorLabel.IsVisible = true;
            return;
        }
        // 登录成功，跳转到签到页面
        await Navigation.PushAsync(new SignInPage(user, _tenants[tenantIdx]));
    }
}
