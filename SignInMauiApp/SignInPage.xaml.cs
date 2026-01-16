using SignInMauiApp.Models;

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
        WelcomeLabel.Text = $"Bienvenido {_user.Username}，empresa：{_tenant.Name}";
        var lastSignIn = _fsql!.Select<SignInRecord>()
            .Where(r => r.UserId == _user.Id && r.TenantId == _tenant.Id)
            .OrderByDescending(r => r.SignInTime)
            .First();
        if (lastSignIn?.SignInTime != null)
        {
            SignInResultLabel.Text = $"Hora del último marcar la {(lastSignIn.SignType == SignTypeEnum.SignInWork ? "entrada" : "salida")}：{lastSignIn.SignInTime:dd/MM/yyyy HH:mm}";
            SignInResultLabel.IsVisible = true;
        }
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        var now = DateTime.Now;
        var record = new SignInRecord
        {
            UserId = _user.Id,
            TenantId = _tenant.Id,
            SignInTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0),
            SignType = SignTypeEnum.SignInWork
        };
        await _fsql!.Insert(record).ExecuteAffrowsAsync();
        SignInResultLabel.Text = $"Hora de entrada：{record.SignInTime:dd/MM/yyyy HH:mm}";
        SignInResultLabel.IsVisible = true;
        // 跳转到签到历史页面
        await Navigation.PushAsync(new SignInReportPage(_user));
    }


    private async void OnSignOutClicked(object sender, EventArgs e)
    {
        var now = DateTime.Now;
        var record = new SignInRecord
        {
            UserId = _user.Id,
            TenantId = _tenant.Id,
            SignInTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0),
            SignType = SignTypeEnum.SignOutWork
        };
        await _fsql!.Insert(record).ExecuteAffrowsAsync();
        SignInResultLabel.Text = $"Hora de salida：{record.SignInTime:dd/MM/yyyy HH:mm}";
        SignInResultLabel.IsVisible = true;
        // 跳转到签到历史页面
        await Navigation.PushAsync(new SignInReportPage(_user));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // 仅管理员可见
        if (_user.IsAdmin)
        {
            // 添加签到报表按钮
            if (_user.IsAdmin && ToolbarItems.All(t => t.Text != "Informe"))
            {
                ToolbarItems.Add(new ToolbarItem("Informe", null, async () =>
                {
                    await Navigation.PushAsync(new SignInReportPage(_user));
                }));
            }
            // 添加租户管理按钮
            if (_user.IsAdmin && ToolbarItems.All(t => t.Text != "Gestión de la empresa"))
            {
                ToolbarItems.Add(new ToolbarItem("Gestión de la empresa", null, async () =>
                {
                    await Navigation.PushAsync(new TenantManagementPage());
                }));
            }
            // 添加用户管理按钮
            if (_user.IsAdmin && ToolbarItems.All(t => t.Text != "Gestión de usuarios"))
            {
                ToolbarItems.Add(new ToolbarItem("Gestión de usuarios", null, async () =>
                {
                    await Navigation.PushAsync(new UserManagementPage(_tenant));
                }));
            }
        }
        // 添加签到历史
        if (!_user.IsAdmin && ToolbarItems.All(t => t.Text != "Historial"))
        {
            ToolbarItems.Add(new ToolbarItem("Historial", null, async () =>
            {
                await Navigation.PushAsync(new SignInReportPage(_user)); 
            }));
        }
        // 添加退出登录按钮
        if (ToolbarItems.All(t => t.Text != "Salir"))
        {
            ToolbarItems.Add(new ToolbarItem("Salir", null, async () =>
            {
                await Navigation.PopToRootAsync();
            }));
        }
    }
}
