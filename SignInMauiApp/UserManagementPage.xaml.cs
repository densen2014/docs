using SignInMauiApp.Models;

namespace SignInMauiApp;

public partial class UserManagementPage : ContentPage
{
    private readonly IFreeSql? _fsql;
    private List<User> _Users = new();
    private readonly Tenant _tenant;

    public UserManagementPage(Tenant tenant)
    {
        InitializeComponent();
        _fsql = IPlatformApplication.Current?.Services.GetService<IFreeSql>();
        _tenant = tenant;
    }

    private void LoadUsers()
    {
        _Users = _fsql!.Select<User>().Where(a => a.TenantId == _tenant.Id).ToList();
        UserCollectionView.ItemsSource = _Users;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadUsers();
        if (ToolbarItems.All(t => t.Text != "Nuevo usuario"))
        {
            ToolbarItems.Add(new ToolbarItem("Nuevo usuario", null, async () =>
            {
                await Navigation.PushAsync(new RegisterPage(false));
            }));
        }
    }

    private async void OnEditUserClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is User user)
        {
            string result = await DisplayPromptAsync("Editar nombre de usuario", "Por favor ingresa un nuevo nombre", initialValue: user.Username);
            if (!string.IsNullOrEmpty(result))
            {
                user.Username = result;
                result = await DisplayPromptAsync("Editar contraseña", "Por favor ingrese nuevo contraseña");
                if (!string.IsNullOrEmpty(result) && result != user.Password)
                {
                    user.Password = result;
                }
                result = await DisplayPromptAsync("Editar nombre y apellido", "Por favor ingrese nombre y apellido", initialValue: user.Name);
                if (!string.IsNullOrEmpty(result) && result != user.Name)
                {
                    user.Name = result;
                }
                result = await DisplayPromptAsync("Editar número de identificación fiscal", "Por favor ingrese el nuevo número de identificación fiscal", initialValue: user.TaxNumber);
                if (!string.IsNullOrEmpty(result) && result != user.TaxNumber)
                {
                    user.TaxNumber = result;
                }
                result = await DisplayPromptAsync("Editar horas de trabajo", "Por favor introduce un nuevo horario laboral", initialValue: user.WorkDuration.ToString());
                if (!string.IsNullOrEmpty(result) && result != user.WorkDuration.ToString() && int.TryParse(result, out var workDuration))
                {
                    user.WorkDuration = workDuration;
                }
                await _fsql!.Update<User>().SetSource(user).ExecuteAffrowsAsync();
                LoadUsers();
            }
        }
    }

    private async void OnDeleteUserClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is User user)
        {
            if (await DisplayAlertAsync("Confirmar", $"Confirmar para eliminar empresa：{user.Name}？", "Borrar", "Cancelar"))
            {
                await _fsql!.Delete<User>().Where(t => t.Id == user.Id).ExecuteAffrowsAsync();
                LoadUsers();
            }
        }
    }
}
