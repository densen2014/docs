using AntdUI;
using SignInMauiApp.Models;

namespace SignInWinApp;

public partial class LoginPage: AntdUI.Window
{
    private readonly IFreeSql? _fsql;
    private List<Tenant> _tenants = new();

    public LoginPage()
    {
        InitializeComponent();
        ErrorLabel.Text = "";
        Logo.Image = Program.GetLogoImage();
        btnLogin.Click += OnLoginClicked;
        btnRegister.Click += OnRegisterClicked;

        _fsql = Program.Fsql;
        // 新增：检查是否需要显示引导页
        CheckAndShowOnboardingAsync();
        LoadTenants();

        OnAppearing();

    }


    protected void OnAppearing()
    {
        LoadTenants(); // 注册后刷新租户列表
        CheckAndCreateDesktopShortcutAsync();
    }

    private async void CheckAndShowOnboardingAsync()
    {
        // 检查本地存储是否已完成引导
        var onboardingDone = Preferences.Get("OnboardingDone", false);
        var tenantsCount = _fsql!.Select<Tenant>().Count();
        if (tenantsCount == 0 || !onboardingDone)
        {
            Hide();
            var onboardingPage = new OnboardingPage(_fsql);
            onboardingPage.ShowDialog();
            onboardingPage.OnboardingCompleted += (s, e) =>
            {
                Show();
            }; 
        }
    }

    private void LoadTenants()
    {
        _tenants = _fsql!.Select<Tenant>().ToList();
        var items = _tenants.Select(a => new SelectItem(a.Name,a.Id)).ToArray();
        TenantPicker.Items.AddRange(items);


        int lastTenantId = Preferences.Get("LastTenantId", -1);
        int idx = 0;
        if (_tenants.Count > 0)
        {
            if (lastTenantId > 0)
            {
                idx = _tenants.FindIndex(t => t.Id == lastTenantId);
                if (idx < 0)
                {
                    idx = 0;
                }
            }
            TenantPicker.SelectedIndex = idx;
            //TenantPicker.IsEnabled = false;
        }
        UsernameEntry.Text = Preferences.Get("LastUsername", "");
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
    {
        ErrorLabel.Visible = false;
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text;
        var tenantIdx = TenantPicker.SelectedIndex;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || tenantIdx < 0)
        {
            ErrorLabel.Text = "请填写完整信息";
            ErrorLabel.Visible = true;
            return;
        }
        var tenantId = _tenants[tenantIdx].Id;
        var user = _fsql!.Select<User>().Where(u => u.Username == username && u.Password == password && u.TenantId == tenantId).First();
        if (user == null)
        {
            ErrorLabel.Text = "用户名或密码错误";
            ErrorLabel.Visible = true;
            return;
        }
        // 记住租户和用户名
        Preferences.Set("LastTenantId", tenantId);
        Preferences.Set("LastUsername", username);
        // 登录成功，跳转到签到页面
        Hide();
        var SignIn = new SignInPage(user, _tenants[tenantIdx]);
        SignIn.ShowDialog();
        Show();
    }

    private async void OnRegisterClicked(object? sender, EventArgs e)
    {
        var registerPage = new RegisterPage();
        Hide();
        registerPage.ShowDialog();
        Show();
    }

    private async void CheckAndCreateDesktopShortcutAsync()
    {
        string shortcutName = "SignIn.lnk";
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string shortcutPath = Path.Combine(desktopPath, shortcutName);
        if (!File.Exists(shortcutPath))
        {
            bool create = Program.DisplayAlert("创建桌面快捷方式", "未检测到桌面快捷方式，是否创建？", "是", "否");
            if (create)
            {
                try
                {
                    CreateShortcut(shortcutPath);
                }
                catch (Exception ex)
                {
                    Program.DisplayAlert("错误", $"创建快捷方式失败: {ex.Message}", "确定");
                }
            }
        }
    }


    private void CreateShortcut(string shortcutPath)
    {
        // 需要引用 Windows Script Host Object Model (wshom.ocx)
        // 但.NET MAUI桌面项目可用COM
        var shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell")!);
        var shortcut = shell?.GetType().InvokeMember("CreateShortcut", System.Reflection.BindingFlags.InvokeMethod, null, shell, new object[] { shortcutPath });
        // 获取当前可执行文件路径
        string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule!.FileName;
        shortcut!.GetType().InvokeMember("TargetPath", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object[] { exePath });
        shortcut.GetType().InvokeMember("WorkingDirectory", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object?[] { Path.GetDirectoryName(exePath) });
        shortcut.GetType().InvokeMember("WindowStyle", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object[] { 1 });
        shortcut.GetType().InvokeMember("Description", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object[] { "SignInMauiApp" });
        shortcut.GetType().InvokeMember("Save", System.Reflection.BindingFlags.InvokeMethod, null, shortcut, null);
    }
}

