using SignInMauiApp.Models;
using MenuItem = AntdUI.MenuItem;

namespace SignInWinApp;

public partial class SignInPage: AntdUI.Window
{
    private readonly IFreeSql? _fsql;
    private readonly User _user;
    private readonly Tenant _tenant;

    public SignInPage(User user, Tenant tenant)
    {
        InitializeComponent();
        Icon = Program.GetAppIcon();
        SignInResultLabel.Text = "";
        Logo.Image = Program.GetLogoImage();
        btnSignIn.Click += OnSignInClicked; 
        btnSignOut.Click += OnSignOutClicked;
        Header.BackClick += (s, e) => Close();
        _fsql = Program.Fsql;
        _user = user;
        _tenant = tenant;
        WelcomeLabel.Text = $"Bienvenido {_user.Username}，empresa：{_tenant.Name}";
        OnAppearing();
        var lastSignIn = _fsql!.Select<SignInRecord>()
            .Where(r => r.UserId == _user.Id && r.TenantId == _tenant.Id)
            .OrderByDescending(r => r.SignInTime)
            .First();
        if (lastSignIn?.SignInTime != null)
        {
            SignInResultLabel.Text = $"Hora del último marcar la {(lastSignIn.SignType == SignTypeEnum.SignInWork ? "entrada" : "salida")}：{lastSignIn.SignInTime:dd/MM/yyyy HH:mm}";
            SignInResultLabel.Visible = true;
        }
    }

    private async void OnSignInClicked(object? sender, EventArgs e)
    {
        var record = new SignInRecord
        {
            UserId = _user.Id,
            TenantId = _tenant.Id,
            SignInTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0)
        };
        await _fsql!.Insert(record).ExecuteAffrowsAsync();
        SignInResultLabel.Text = $"Hora de entrada：{record.SignInTime:dd/MM/yyyy HH:mm}";
        SignInResultLabel.Visible = true;
        Hide();
        // 跳转到签到历史页面
        var signInHistoryPage=new SignInReportPage(_user);
        signInHistoryPage.ShowDialog();
        Show();
    }


    private async void OnSignOutClicked(object? sender, EventArgs e)
    {
        var record = new SignInRecord
        {
            UserId = _user.Id,
            TenantId = _tenant.Id,
            SignInTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0),
            SignType = SignTypeEnum.SignOutWork
        };
        await _fsql!.Insert(record).ExecuteAffrowsAsync();
        SignInResultLabel.Text = $"Hora de salida：{record.SignInTime:dd/MM/yyyy HH:mm}";
        SignInResultLabel.Visible = true;
        Hide();
        // 跳转到签到历史页面
        var signInHistoryPage = new SignInReportPage(_user);
        signInHistoryPage.ShowDialog();
        Show();
    }
    protected void OnAppearing()
    {
        // 仅管理员可见
        if (_user.IsAdmin)
        {
            //添加签到报表按钮
            var tenantMenu = new MenuItem("Informe");
            MenuTop.Items.Add(tenantMenu);
            // 添加租户管理按钮
            var reportMenu = new MenuItem("Gestión de la empresa");
            MenuTop.Items.Add(reportMenu);
            // 添加用户管理按钮
            var userMenu = new MenuItem("Gestión de usuarios");
            MenuTop.Items.Add(userMenu);
        }
        // 添加签到历史
        if (!_user.IsAdmin)
        {
            var historyMenu = new MenuItem("Historial");
            MenuTop.Items.Add(historyMenu);
        }
        // 添加退出登录按钮
        var exitMenuItem = new MenuItem("Salir");
        MenuTop.Items.Add(exitMenuItem);

        MenuTop.SelectChanged += (s, e) =>
        {
            var selectedItem = MenuTop.SelectItem;
            if (selectedItem != null)
            {
                if (selectedItem.Text == "Informe")
                {
                    Hide();
                    //打开签到报表页面
                    var reportPage = new SignInReportPage(_user);
                    reportPage.ShowDialog();
                    Show();
                }
                else if (selectedItem.Text == "Gestión de la empresa")
                {
                    Hide();
                    //打开租户管理页面
                    var tenantPage = new TenantManagementPage();
                    tenantPage.ShowDialog();
                    Show();
                }
                else if (selectedItem.Text == "Gestión de usuarios")
                {
                    Hide();
                    //打开租户管理页面
                    var tenantPage = new UserManagementPage(_tenant);
                    tenantPage.ShowDialog();
                    Show();
                }
                else if (selectedItem.Text == "Historial")
                {
                    Hide();
                    //打开租户管理页面
                    var tenantPage = new SignInReportPage(_user);
                    tenantPage.ShowDialog();
                    Show();
               }
                else if(selectedItem.Text == "Salir")
                {
                    //关闭当前窗口，返回登录页面
                    this.Close();
                }                
            }
        };
    }
     
}
