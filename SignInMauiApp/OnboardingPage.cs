using SignInMauiApp.Models;

namespace SignInMauiApp;

public partial class OnboardingPage : ContentPage
{
    private readonly IFreeSql? _fsql;
    private Entry _tenantEntry;
    private Entry _adminUserEntry;
    private Entry _adminPassEntry;
    private Entry _firstUserEntry;
    private Entry _firstUserPassEntry;
    private Button _submitButton;

    public OnboardingPage(IFreeSql? fsql)
    {
        _fsql = fsql;
        Title = "Configuración por primera vez";
        _tenantEntry = new Entry { Placeholder = "Nombre de la empresa, la predeterminada es [Mi empresa]", MaximumWidthRequest = 600, Margin = 5 };
        _adminUserEntry = new Entry { Placeholder = "Cuenta de administrador, la predeterminada es [admin]", MaximumWidthRequest = 600, Margin = 5 };
        _adminPassEntry = new Entry { Placeholder = "Contraseña de administrador, la predeterminada es [123456]", IsPassword = true, MaximumWidthRequest = 600, Margin = 5 };
        _firstUserEntry = new Entry { Placeholder = "El primer nombre de usuario, el predeterminado es [demo]", MaximumWidthRequest = 600, Margin = 5 };
        _firstUserPassEntry = new Entry { Placeholder = "La contraseña del primer usuario, la predeterminada es [0]", IsPassword = true, MaximumWidthRequest = 600, Margin = 5 };
        _submitButton = new Button { Text = "Configuración completa", MaximumWidthRequest = 600, Margin = new Thickness(0, 50) };
        _submitButton.Clicked += OnSubmitClicked;
        Content = new VerticalStackLayout
        {
            Padding = 30,
            Children =
            {
                new Label { Text = "Bienvenido, complete la configuración inicial：", FontSize = 20, HorizontalOptions = LayoutOptions.Center , Margin = new Thickness(0,50) },
                _tenantEntry,
                _adminUserEntry,
                _adminPassEntry,
                _firstUserEntry,
                _firstUserPassEntry,
                _submitButton
            }
        };
    }

    private async void OnSubmitClicked(object? sender, EventArgs e)
    {
        var tenantName = _tenantEntry.Text?.Trim() ?? "Mi empresa";
        var adminUser = _adminUserEntry.Text?.Trim() ?? "admin";
        var adminPass = _adminPassEntry.Text?.Trim() ?? "123456";
        var firstUser = _firstUserEntry.Text?.Trim() ?? "demo";
        var firstUserPass = _firstUserPassEntry.Text?.Trim() ?? "0";
        if (string.IsNullOrEmpty(tenantName) || string.IsNullOrEmpty(adminUser) || string.IsNullOrEmpty(adminPass) || string.IsNullOrEmpty(firstUser))
        {
            await DisplayAlertAsync("Aviso", "Por favor complete toda la información", "Aceptar");
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
        await DisplayAlertAsync("Finalizar", "Inicialización completada！", "Ingrese al sistema");
        await Navigation.PopModalAsync();
    }
}
