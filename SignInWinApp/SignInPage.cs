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
        SignInResultLabel.Text = "";
        Logo.Image = Program.GetLogoImage();

        _fsql = Program.Fsql;
        _user = user;
        _tenant = tenant;
        WelcomeLabel.Text = $"欢迎 {_user.Username}，公司：{_tenant.Name}";

        OnAppearing();
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
        SignInResultLabel.Visible = true;
        Hide();
        // 跳转到签到历史页面
        var signInHistoryPage=new SignInHistoryPage(_user, _tenant);
        signInHistoryPage.ShowDialog();
        Show();
    }

    protected void OnAppearing()
    {
        //添加退出登录按钮
        var exitMenu = new MenuItem("退出登录");
        MenuTop.Items.Add(exitMenu); 
        // 添加签到报表按钮（仅管理员可见，这里假设用户名为admin为管理员）
        if (_user.Username == "admin")
        {
            //添加租户管理按钮
            var tenantMenu = new MenuItem("公司管理");
            MenuTop.Items.Add(tenantMenu); 
            var reportMenu = new MenuItem("签到报表");
            MenuTop.Items.Add(reportMenu); 
        }
        MenuTop.SelectChanged += (s, e) =>
        {
            var selectedItem = MenuTop.SelectItem;
            if (selectedItem != null)
            {
                if (selectedItem.Text == "退出登录")
                {
                    //关闭当前窗口，返回登录页面
                    this.Close();
                }
                else if (selectedItem.Text == "签到报表")
                {
                    //打开签到报表页面
                    var reportPage = new SignInReportPage();
                    reportPage.Show();
                }
                else if (selectedItem.Text == "公司管理")
                {
                    //打开租户管理页面
                    var tenantPage = new TenantManagementPage();
                    tenantPage.Show();
                }
            }
        };
    }
     
}
