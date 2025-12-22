using AntdUI;
using SignInMauiApp.Models;

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
        // 跳转到签到历史页面
        //await Navigation.PushAsync(new SignInHistoryPage(_user, _tenant));
    }

    protected void OnAppearing()
    {
        // 添加退出登录按钮
        //if (ToolbarItems.Items.All(t => t.Text != "退出登录"))
        //{
        //    ToolbarItems.Items.Add(new TagTabCollection(new TabHeader(){Text ="退出登录", onclu async () =>
        //    {
        //        await Navigation.PopToRootAsync();
        //    }));
        //}
        // 添加租户管理按钮
        //if (ToolbarItems.Items.All(t => t.Text != "公司管理"))
        //{
        //    ToolbarItems.Add(new ToolbarItem("公司管理", null, async () =>
        //    {
        //        await Navigation.PushAsync(new TenantManagementPage());
        //    }));
        //}
        //// 添加签到报表按钮（仅管理员可见，这里假设用户名为admin为管理员）
        //if (_user.Username == "admin" && ToolbarItems.Items.All(t => t.Text != "签到报表"))
        //{
        //    ToolbarItems.Add(new ToolbarItem("签到报表", null, async () =>
        //    {
        //        await Navigation.PushAsync(new SignInReportPage());
        //    }));
        //}
    }

    private void ToolbarItems_TabChanged(object sender, TabChangedEventArgs e)
    {
        //if (e.Index == 0)
        //{
        //    new TenantManagementPage();
        //}else if (e.Index == 1)
        //{
        //    new SignInReportPage();
        //}
    }
}
