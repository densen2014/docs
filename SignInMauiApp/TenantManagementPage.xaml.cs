using SignInMauiApp.Models;

namespace SignInMauiApp;

public partial class TenantManagementPage : ContentPage
{
    private readonly IFreeSql? _fsql;
    private List<Tenant> _tenants = new();

    public TenantManagementPage()
    {
        InitializeComponent();
        _fsql = IPlatformApplication.Current?.Services.GetService<IFreeSql>();
        LoadTenants();
    }

    private void LoadTenants()
    {
        _tenants = _fsql!.Select<Tenant>().ToList();
        TenantCollectionView.ItemsSource = _tenants;
    }

    private async void OnAddTenantClicked(object sender, EventArgs e)
    {
        var name = NewTenantEntry.Text?.Trim();
        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        if (_fsql!.Select<Tenant>().Any(t => t.Name == name))
        {
            await DisplayAlertAsync("Aviso", "La empresa ya existe.", "Aceptar");
            return;
        }
        var tenant = new Tenant
        {
            Name = name,
            TaxNumber = NewTenantTaxNumberEntry.Text
        };
        await _fsql!.Insert(tenant).ExecuteAffrowsAsync();
        NewTenantEntry.Text = string.Empty;
        NewTenantTaxNumberEntry.Text = string.Empty;
        LoadTenants();
    }

    private async void OnEditTenantClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Tenant tenant)
        {
            string result = await DisplayPromptAsync("Editar nombre de la empresa", "Por favor ingresa un nuevo nombre", initialValue: tenant.Name);
            if (!string.IsNullOrEmpty(result))
            {
                tenant.Name = result;
                result = await DisplayPromptAsync("Editar el número de identificación fiscal de la empresa", "Por favor ingrese el nuevo número de identificación fiscal", initialValue: tenant.TaxNumber);
                if (!string.IsNullOrEmpty(result) && result != tenant.TaxNumber)
                {
                    tenant.TaxNumber = result; 
                } 
                await _fsql!.Update<Tenant>().SetSource(tenant).ExecuteAffrowsAsync();
                LoadTenants();
            }
        }
    }

    private async void OnDeleteTenantClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Tenant tenant)
        {
            if (await DisplayAlertAsync("Confirmar", $"Confirmar para eliminar empresa：{tenant.Name}？", "Borrar", "Cancelar"))
            {
                await _fsql!.Delete<Tenant>().Where(t => t.Id == tenant.Id).ExecuteAffrowsAsync();
                LoadTenants();
            }
        }
    } 
   
}
