using AntdUI;
using SignInMauiApp.Models;

namespace SignInWinApp;

public partial class RegisterPage : AntdUI.Window
{
    private readonly IFreeSql? _fsql;
    private List<Tenant> _tenants = new();

    public RegisterPage()
    {
        InitializeComponent();
        ErrorLabel.Text = "";
        btnRegister.Click += OnRegisterClicked;

        _fsql = Program.Fsql;
        LoadTenants();
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
    }

    private async void OnRegisterClicked(object? sender, EventArgs e)
    {
        ErrorLabel.Visible = false;
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text;
        var newTenant = NewTenantEntry.Text?.Trim();
        int tenantId = -1;
        if (!string.IsNullOrEmpty(newTenant))
        {
            var tenant = new Tenant { Name = newTenant! };
            tenantId = (int)await _fsql!.Insert(tenant).ExecuteIdentityAsync();
        }
        else if (TenantPicker.SelectedIndex >= 0)
        {
            tenantId = _tenants[TenantPicker.SelectedIndex].Id;
        }
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || tenantId < 0)
        {
            ErrorLabel.Text = "Por favor complete la información completa";
            ErrorLabel.Visible = true;
            return;
        }
        if (_fsql!.Select<User>().Any(u => u.Username == username && u.TenantId == tenantId))
        {
            ErrorLabel.Text = "Este usuario ya existe";
            ErrorLabel.Visible = true;
            return;
        }
        var user = new User { Username = username!, Password = password, TenantId = tenantId };
        await _fsql!.Insert(user).ExecuteAffrowsAsync();
        Program.DisplayAlert("Registro exitoso", "Por favor regrese para iniciar sesión", "Aceptar");
        Close();
    }

}

