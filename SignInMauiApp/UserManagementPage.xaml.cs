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
        LoadUsers();
    }

    private void LoadUsers()
    {
        _Users = _fsql!.Select<User>().Where(a=>a.TenantId== _tenant.Id).ToList();
        UserCollectionView.ItemsSource = _Users;
    }  

    private async void OnEditUserClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is User User)
        {
            string result = await DisplayPromptAsync("Editar nombre", "Por favor ingresa un nuevo nombre", initialValue: User.Name);
            if (!string.IsNullOrEmpty(result))
            {
                User.Name = result;
                result = await DisplayPromptAsync("Editar número de identificación fiscal", "Por favor ingrese el nuevo número de identificación fiscal", initialValue: User.TaxNumber);
                if (!string.IsNullOrEmpty(result) && result != User.TaxNumber)
                {
                    User.TaxNumber = result; 
                } 
                result = await DisplayPromptAsync("Editar horas de trabajo", "Por favor introduce un nuevo horario laboral", initialValue: User.WorkDuration.ToString());
                if (!string.IsNullOrEmpty(result) && result != User.WorkDuration.ToString() && int.TryParse(result,out var workDuration))
                {
                    User.WorkDuration = workDuration; 
                } 
                await _fsql!.Update<User>().SetSource(User).ExecuteAffrowsAsync();
                LoadUsers();
            }
        }
    }

    private async void OnDeleteUserClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is User User)
        {
            if (await DisplayAlertAsync("Confirmar", $"Confirmar para eliminar empresa：{User.Name}？", "Borrar", "Cancelar"))
            {
                await _fsql!.Delete<User>().Where(t => t.Id == User.Id).ExecuteAffrowsAsync();
                LoadUsers();
            }
        }
    }

    private void OnUserSelected(object sender, SelectionChangedEventArgs e)
    {
        // 可扩展：选中公司后显示详情或关联用户
    }
}
