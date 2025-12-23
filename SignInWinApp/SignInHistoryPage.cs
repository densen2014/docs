using AntdUI;
using SignInMauiApp.Models;

namespace SignInWinApp;

public partial class SignInHistoryPage: AntdUI.Window
{
    private readonly IFreeSql? _fsql;
    private readonly User _user;
    private readonly Tenant _tenant;
    private AntList<SignInRecord>? antList;

    public SignInHistoryPage(User user, Tenant tenant)
    {
        InitializeComponent();
        TitleLabel.Text = ""; 

        _fsql = Program.Fsql;
        _user = user;
        _tenant = tenant;
        TitleLabel.Text = $"{_user.Username} 的签到历史（租户：{_tenant.Name}）";
        LoadHistory();
        OnAppearing();
    }

     

    private void LoadHistory()
    {
        var records = _fsql!.Select<SignInRecord>()
            .Where(r => r.UserId == _user.Id && r.TenantId == _tenant.Id)
            .OrderByDescending(r => r.SignInTime)
            .ToList();
        antList = new AntList<SignInRecord>();
        records.ForEach(a=>antList.Add(a));
        HistoryCollectionView.Binding(antList);
        //         <Label Text="{Binding SignInTime, StringFormat='签到时间：{0:yyyy-MM-dd HH:mm:ss}'}" />

    }

    protected void OnAppearing()
    {
        //// 添加退出登录按钮
        //if (ToolbarItems.All(t => t.Text != "退出登录"))
        //{
        //    ToolbarItems.Add(new ToolbarItem("退出登录", null, async () => {
        //        await Navigation.PopToRootAsync();
        //    }));
        //}
    }
}

