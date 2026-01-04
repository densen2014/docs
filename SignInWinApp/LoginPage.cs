using AntdUI;
using KestrelWebHost;
using QRCoder;
using SignInMauiApp.Models;

namespace SignInWinApp;

public partial class LoginPage : AntdUI.Window
{
    private readonly IFreeSql? _fsql;
    private List<Tenant> _tenants = new();
    private string qrLink = "";

    public LoginPage()
    {
        InitializeComponent();
        Logo.Image = Program.GetLogoImage();
        Icon = Program.GetAppIcon();
        ErrorLabel.Visible = false;
        SignInResultLabel.Visible = false;
        ErrorLabel.Text = "";
        SignInResultLabel.Text = "";
        btnLogin.Click += OnLoginClicked;
        btnRegister.Click += OnRegisterClicked;
        ImageQR.MouseClick += OnImageQRTapped;

        _fsql = Program.Fsql;
        // 检查是否需要显示引导页
        GenerateAndShowQRCode();
        CheckAndShowOnboardingAsync();
        OnAppearing();
        // 订阅 OnPlayControl 回调
        WebApp.OnControl = async signInWeb =>
        {
            var res = await OnsigninWeb(signInWeb);
            return res;
        };
        CopyRightLabel.Click += (s, e) =>
        {
            var url = CopyRightLabel.Tag as string;
            if (!string.IsNullOrEmpty(url))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
        };
    }

    private void GenerateAndShowQRCode()
    {
        qrLink = $"http://{Program.WebHostParameters.ServerIpEndpoint?.Address}:{Program.WebHostParameters.ServerIpEndpoint?.Port}"; // 可自定义内容
        using (var qrGenerator = new QRCodeGenerator())
        using (var qrCodeData = qrGenerator.CreateQrCode(qrLink, QRCodeGenerator.ECCLevel.Q))
        using (var qrCode = new PngByteQRCode(qrCodeData))
        {
            var qrBytes = qrCode.GetGraphic(20);
            ImageQR.Image = [new ImagePreviewItem() { ID = "0", Img = Image.FromStream(new MemoryStream(qrBytes)) }];
        }
    }
    protected void OnAppearing()
    {
        LoadTenants(); // 注册后刷新租户列表
        PasswordEntry.Text = "";
        // 延时3秒后异步执行
        Task.Run(async () =>
        {
            await Task.Delay(3000);
            CheckAndCreateDesktopShortcutAsync();
        });
    }

    private async void CheckAndShowOnboardingAsync()
    {
        var tenantsCount = _fsql!.Select<Tenant>().Count();
        if (tenantsCount == 0) 
        {
            Hide();
            var onboardingPage = new OnboardingPage(_fsql);
            onboardingPage.ShowDialog();
            OnAppearing();
            Show();
        }
    }

    private void LoadTenants()
    {
        _tenants = _fsql!.Select<Tenant>().ToList();
        var items = _tenants.Select(a => new SelectItem(a.Name, a.Id)).ToArray();
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
        SignInResultLabel.Visible = false;
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text;
        var tenantIdx = TenantPicker.SelectedIndex;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || tenantIdx < 0)
        {
            ErrorLabel.Text = "Por favor complete la información completa";
            SignInResultLabel.Text = "";
            ErrorLabel.Visible = true;
            return;
        }
        var tenantId = _tenants[tenantIdx].Id;
        var user = _fsql!.Select<User>().Where(u => u.Username == username && u.Password == password && u.TenantId == tenantId).First();
        if (user == null)
        {
            ErrorLabel.Text = "Nombre de usuario o contraseña incorrectos";
            SignInResultLabel.Text = "";
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
        OnAppearing();
        Show();
    }

    private async Task<SignInResponse> OnsigninWeb(SignInWeb signInWeb)
    {
        Invoke(() =>
        {
            ErrorLabel.Visible = false;
            SignInResultLabel.Visible = false;
        });
        var username = signInWeb.Username.Trim();
        var password = signInWeb.Password;
        var tenantIdx = TenantPicker.SelectedIndex;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || tenantIdx < 0)
        {
            Invoke(() =>
            {
                ErrorLabel.Text = "Por favor complete la información completa";
                SignInResultLabel.Text = "";
                ErrorLabel.Visible = true;
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
            Invoke(() =>
            {
                ErrorLabel.Text = "Nombre de usuario o contraseña incorrectos";
                SignInResultLabel.Text = "";
                ErrorLabel.Visible = true;
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
                Invoke(() =>
                {
                    SignInResultLabel.Text = $"{user.Username} iniciar sesión exitosamente, Último marcar de {(lastSignIn.SignType == SignTypeEnum.SignInWork ? "entrada" : "salida")}：{lastSignIn.SignInTime:dd/MM/yyyy HH:mm:ss}";
                    SignInResultLabel.Visible = true;
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
            Invoke(() =>
            {
                SignInResultLabel.Text = message;
                SignInResultLabel.Visible = true;
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
            Invoke(() =>
            {
                SignInResultLabel.Text = message;
                SignInResultLabel.Visible = true;
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
        Invoke(() =>
        {
            SignInResultLabel.Text = message;
            ErrorLabel.Text = "";
            SignInResultLabel.Visible = true;
        });
        return new SignInResponse()
        {
            Success = true,
            Message = message,
            LastSignIn = DateTime.Now
        };
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
        string shortcutName = "FichajeLite.lnk";
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string shortcutPath = Path.Combine(desktopPath, shortcutName);
        if (!File.Exists(shortcutPath))
        {
            Invoke(() =>
            {
                bool create = Program.DisplayAlert("Crear acceso directo en el escritorio ", "Acceso directo en el escritorio no detectado, ¿crearlo?", "Sí", "No");
                if (create)
                {
                    try
                    {
                        CreateShortcut(shortcutPath);
                    }
                    catch (Exception ex)
                    {
                        Program.DisplayAlert("error", $"No se pudo crear el acceso directo: {ex.Message}", "Aceptar");
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

    private void OnImageQRTapped(object? sender, MouseEventArgs e)
    {
        if (e.Button == System.Windows.Forms.MouseButtons.Left)
        {

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = qrLink,
                UseShellExecute = true
            });
        }
        else
        {
            if (ImageQR.Image is ImagePreviewItemCollection images && images.Count > 0 && images[0].Img != null)
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "qr_temp.png");
                images[0].Img!.Save(tempPath, System.Drawing.Imaging.ImageFormat.Png);
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = tempPath,
                    UseShellExecute = true
                });
            }
        }
    }
}

