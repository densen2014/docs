using FreeSql;
using KestrelWebHost;
using QRCoder;
using SignInMauiApp.Models;

namespace SignInMauiApp;

public partial class LoginPage : ContentPage
{
    private readonly IFreeSql? _fsql;
    private List<Tenant> _tenants = new();
    private string qrLink = "";

    public LoginPage()
    {
        InitializeComponent();
        _fsql = IPlatformApplication.Current?.Services.GetService<IFreeSql>();
        // 检查是否需要显示引导页
        CheckAndShowOnboardingAsync();
        LoadTenants();
        GenerateAndShowQRCode();
        // 订阅 OnPlayControl 回调
        WebApp.OnControl = async signInWeb =>
        {
            var res = await OnsigninWeb(signInWeb);
            return res;
        };

    }

    private void GenerateAndShowQRCode()
    {
        qrLink = $"http://{App.WebHostParameters.ServerIpEndpoint?.Address}:{App.WebHostParameters.ServerIpEndpoint?.Port}"; // 可自定义内容
        using (var qrGenerator = new QRCodeGenerator())
        using (var qrCodeData = qrGenerator.CreateQrCode(qrLink, QRCodeGenerator.ECCLevel.Q))
        using (var qrCode = new PngByteQRCode(qrCodeData))
        {
            var qrBytes = qrCode.GetGraphic(20);
            ImageQR.Source = ImageSource.FromStream(() => new MemoryStream(qrBytes));
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        PasswordEntry.Text = "";
        LoadTenants(); // 注册后刷新租户列表
#if WINDOWS
        CheckAndCreateDesktopShortcutAsync();
#endif
    }

    private async void CheckAndShowOnboardingAsync()
    {
        var tenantsCount = _fsql!.Select<Tenant>().Count();
        if (tenantsCount == 0)
        {
            await Navigation.PushModalAsync(new OnboardingPage(_fsql));
        }
    }

    private void LoadTenants()
    {
        _tenants = _fsql!.Select<Tenant>().ToList();
        TenantPicker.ItemsSource = _tenants.Select(t => t.Name).ToList();
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

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;
        SignInResultLabel.IsVisible = false;
        ErrorLabel.Text = "";
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text;
        var tenantIdx = TenantPicker.SelectedIndex;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || tenantIdx < 0)
        {
            ErrorLabel.Text = "Por favor complete la información completa";
            SignInResultLabel.Text = "";
            ErrorLabel.IsVisible = true;
            return;
        }
        var tenantId = _tenants[tenantIdx].Id;
        var user = _fsql!.Select<User>().Where(u => u.Username == username && u.Password == password && u.TenantId == tenantId).First();
        if (user == null)
        {
            ErrorLabel.Text = "Nombre de usuario o contraseña incorrectos";
            SignInResultLabel.Text = "";
            ErrorLabel.IsVisible = true;
            return;
        }
        // 记住租户和用户名
        Preferences.Set("LastTenantId", tenantId);
        Preferences.Set("LastUsername", username);
        // 登录成功，跳转到签到页面
        await Navigation.PushAsync(new SignInPage(user, _tenants[tenantIdx]));
    }

    private async Task<SignInResponse> OnsigninWeb(SignInWeb signInWeb)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ErrorLabel.IsVisible = false;
            SignInResultLabel.IsVisible = false;
        });
        var username = signInWeb.Username.Trim();
        var password = signInWeb.Password;
        var tenantIdx = TenantPicker.SelectedIndex;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || tenantIdx < 0)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ErrorLabel.Text = "Por favor complete la información completa";
                SignInResultLabel.Text = "";
                ErrorLabel.IsVisible = true;
            });
            return new SignInResponse()
            {
                Success = false,
                Message = "Por favor complete la información completa"
            };
        }
        var tenantId = _tenants[tenantIdx].Id;
        var user = _fsql!.Select<User>().Where(u => u.Username == username && u.Password == password && u.TenantId == tenantId).First();
        if (user == null)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ErrorLabel.Text = "Nombre de usuario o contraseña incorrectos";
                SignInResultLabel.Text = "";
                ErrorLabel.IsVisible = true;
            });
            return new SignInResponse()
            {
                Success = false,
                Message = "Nombre de usuario o contraseña incorrectos"
            };
        }
        var lastSignIn = _fsql!.Select<SignInRecord>()
                   .Where(r => r.UserId == user.Id && r.TenantId == tenantId)
                   .OrderByDescending(r => r.SignInTime)
                   .First();
        if (signInWeb.Action == "login")
        {
            if (lastSignIn?.SignInTime != null)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    SignInResultLabel.Text = $"{user.Username} iniciar sesión exitosamente, Último marcar de {(lastSignIn.SignType == SignTypeEnum.SignInWork ? "entrada" : "salida")}：{lastSignIn.SignInTime:dd/MM/yyyy HH:mm:ss}";
                    SignInResultLabel.IsVisible = true;
                });
            }
            return new SignInResponse()
            {
                Success = true,
                Message = "Iniciar sesión exitosamente",
                LastSignIn = lastSignIn?.SignInTime
            };
        }
        var record = new SignInRecord
        {
            UserId = user.Id,
            TenantId = tenantId,
        };

        var message = string.Empty;
        if (signInWeb.Action == "signin" && lastSignIn?.SignInTime != null && lastSignIn.SignType == SignTypeEnum.SignInWork && lastSignIn.SignInTime.Value.Date == DateTime.Today)
        {
            message = $"{user.Username},la operación no se puede repetir. Último marcar de {(lastSignIn.SignType == SignTypeEnum.SignInWork ? "entrada" : "salida")}：{lastSignIn.SignInTime:dd/MM/yyyy HH:mm:ss}";
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SignInResultLabel.Text = message;
                SignInResultLabel.IsVisible = true;
            });
            return new SignInResponse()
            {
                Success = false,
                Message = message,
                LastSignIn = lastSignIn?.SignInTime
            };
        }
        else if (signInWeb.Action == "signout" && lastSignIn?.SignInTime != null && lastSignIn.SignType == SignTypeEnum.SignOutWork && lastSignIn.SignInTime.Value.Date == DateTime.Today)
        {
            message = $"{user.Username},la operación no se puede repetir. Último marcar de {(lastSignIn.SignType == SignTypeEnum.SignInWork ? "entrada" : "salida")}：{lastSignIn.SignInTime:dd/MM/yyyy HH:mm:ss}";
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SignInResultLabel.Text = message;
                SignInResultLabel.IsVisible = true;
            });
            return new SignInResponse()
            {
                Success = false,
                Message = message,
                LastSignIn = lastSignIn?.SignInTime
            };
        }
        else if (signInWeb.Action == "signin")
        {
            {
                record.SignInTime = DateTime.Now;
                record.SignType = SignTypeEnum.SignInWork;
            }
        }
        else
        {
            record.SignInTime = DateTime.Now;
            record.SignType = SignTypeEnum.SignOutWork;
        }
        await _fsql!.Insert(record).ExecuteAffrowsAsync();
        message = $"{user.Username}, {(signInWeb.Action == "signin" ? "Hora de entrada" : "Hora de salida")}：{record.SignInTime:dd/MM/yyyy HH:mm:ss}"; 
        MainThread.BeginInvokeOnMainThread(() =>
        {
            SignInResultLabel.Text = message;
            ErrorLabel.Text = "";
            SignInResultLabel.IsVisible = true;
        });
        return new SignInResponse()
        {
            Success = true,
            Message = message,
            LastSignIn = DateTime.Now 
        };
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

#if WINDOWS
    private async void CheckAndCreateDesktopShortcutAsync()
    {
        string shortcutName = "Fichaje.lnk";
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string shortcutPath = Path.Combine(desktopPath, shortcutName);
        if (!File.Exists(shortcutPath))
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                bool create = await DisplayAlertAsync("Crear acceso directo en el escritorio ", "Acceso directo en el escritorio no detectado, ¿crearlo?", "Sí", "No");
                if (create)
                {
                    try
                    {
                        CreateShortcut(shortcutPath);
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlertAsync("error", $"No se pudo crear el acceso directo: {ex.Message}", "Aceptar");
                    }
                }
            });
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
        shortcut.GetType().InvokeMember("Description", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object[] { "Fichaje - Registro de Jornada Laboral" });
        shortcut.GetType().InvokeMember("Save", System.Reflection.BindingFlags.InvokeMethod, null, shortcut, null);
    }
#endif

    private async void OnImageQRTapped(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(qrLink);
    }
}
