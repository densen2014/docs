using AntdUI;
using SignInMauiApp.Models;
using System.ComponentModel;
using Button = AntdUI.Button;
using Label = AntdUI.Label;

namespace SignInWinApp;

public partial class OnboardingPage : AntdUI.Window
{
    private readonly IFreeSql? _fsql;
    private Label _labelHeader;
    private Input _tenantEntry;
    private Input _adminUserEntry;
    private Input _adminPassEntry;
    private Input _firstUserEntry;
    private Input _firstUserPassEntry;
    private Button _submitButton;
    private StackPanel Content;
    private PageHeader? Header;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Action<object, object>? OnboardingCompleted { get; internal set; }

    private IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    public OnboardingPage(IFreeSql? fsql)
    {

        Header = new PageHeader();

        SuspendLayout();
        // 
        // Header
        // 
        Header.DividerShow = true;
        Header.Dock = DockStyle.Top;
        Header.Font = new Font("Microsoft YaHei UI", 11F);
        Header.Location = new Point(0, 0);
        Header.Margin = new Padding(4);
        Header.Name = "Header";
        Header.ShowButton = true;
        Header.Size = new Size(655, 35);
        Header.TabIndex = 16;
        Header.Text = "APP";
        Header.UseForeColorDrawIcons = true;
        Header.UseLeftMargin = false;
        // 
        // OnboardingPage
        //
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(655, 600);
        Controls.Add(Header);
        Font = new Font("Microsoft YaHei UI", 11F);
        FormBorderStyle = FormBorderStyle.Sizable;
        Margin = new Padding(3, 4, 3, 4);
        Mode = TAMode.Light;
        Name = "OnboardingPage";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "初次设置";

        var padding = new Padding(3);
        var size = new Size(500, 39);
        _tenantEntry = new Input
        {
            PlaceholderText = "请输入公司名称, 默认为[我的公司]",
            Width = 600,
            Margin = padding,
            Size = size
        };
        _adminUserEntry = new Input
        {
            PlaceholderText = "管理员账号, 默认为[admin]",
            Width = 600,
            Margin = padding,
            Size = size
        };
        _adminPassEntry = new Input
        {
            PlaceholderText = "管理员密码, 默认为[123456]",
            PasswordChar = '*',
            Width = 600,
            Margin = padding,
            Size = size
        };
        _firstUserEntry = new Input
        {
            PlaceholderText = "第一个用户名称, 默认为[demo]",
            Width = 600,
            Margin = padding,
            Size = size
        };
        _firstUserPassEntry = new Input
        {
            PlaceholderText = "第一个用户密码, 默认为[0]",
            PasswordChar = '*',
            Width = 600,
            Margin = padding,
            Size = size
        };
        _submitButton = new Button
        {
            Text = "完成设置",
            Width = 600,
            Margin = new Padding(0, 50, 0, 50),
            Size = size
        };
        _submitButton.Click += OnSubmitClicked;

        _labelHeader = new Label
        {
            Text = "欢迎使用，请完成初始设置：",
            Font = new Font("Microsoft YaHei UI", 20F),
            Margin = new Padding(0, 50, 0, 50),
            TextAlign = ContentAlignment.MiddleCenter,
            Size = size
        };
        Content = new StackPanel
        {
            Vertical = true,
            Padding = new Padding(30),
            Dock = DockStyle.Fill,
        };

        Control[] controls = [
                _submitButton,
                _firstUserPassEntry,
                _firstUserEntry,
                _adminPassEntry,
                _adminUserEntry,
                _tenantEntry,
                _labelHeader,
                ];
        Content.Controls.AddRange(controls);


        Controls.Add(Content);
        ResumeLayout(false);

        _fsql = fsql;
        Text = "初次设置";

    }

    private async void OnSubmitClicked(object? sender, EventArgs e)
    {
        var tenantName = _tenantEntry.Text.IsNull("我的公司");
        var adminUser = _adminUserEntry.Text.IsNull("admin");
        var adminPass = _adminPassEntry.Text.IsNull("123456");
        var firstUser = _firstUserEntry.Text.IsNull("demo");
        var firstUserPass = _firstUserPassEntry.Text.IsNull("0");
        if (string.IsNullOrEmpty(tenantName) || string.IsNullOrEmpty(adminUser) || string.IsNullOrEmpty(adminPass) || string.IsNullOrEmpty(firstUser))
        {
            Program.DisplayAlert("提示", "请填写所有信息", "确定");
            return;
        }
        // 创建租户
        var tenant = new Tenant { Name = tenantName };
        tenant.Id = (int)_fsql!.Insert(tenant).ExecuteIdentity();
        // 创建管理员
        var admin = new User { Username = adminUser, Password = adminPass, IsAdmin = true, TenantId = tenant.Id };
        _fsql?.Insert(admin).ExecuteAffrows();
        // 创建第一个普通用户
        var user = new User { Username = firstUser, Password = firstUserPass, IsAdmin = false, TenantId = tenant.Id };
        _fsql?.Insert(user).ExecuteAffrows();
        Preferences.Set("OnboardingDone", true);
        Program.DisplayAlert("完成", "初始化完成！", "进入系统");
        Close();
        OnboardingCompleted?.Invoke(this, EventArgs.Empty);
    }


}
