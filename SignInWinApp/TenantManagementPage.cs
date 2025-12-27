using AntdUI;
using SignInMauiApp.Models;

namespace SignInWinApp;

public partial class TenantManagementPage : AntdUI.Window
{
    private readonly IFreeSql? _fsql;
    private List<Tenant> _tenants = new();
    private AntList<Tenant> antList = new AntList<Tenant>();
    public TenantManagementPage()
    {
        InitializeComponent();

        btnAddTenant.Click += OnAddTenantClicked;
        _fsql = Program.Fsql;
        InitTableColumns();
        LoadTenants();
        TenantCollectionView.CellButtonClick += Table_base_CellButtonClick;
    }

    private void InitTableColumns()
    {
        TenantCollectionView.Columns =
        [
            new Column("Name", "Nombre de empresa",ColumnAlign.Center) {ColBreak=true},
            new Column("TaxNumber", "NIF",ColumnAlign.Center) { ColBreak=true}, 
        ];
    }

    //表格内部按钮事件
    private async void Table_base_CellButtonClick(object sender, TableButtonEventArgs e)
    {
        var buttontext = e.Btn.Text;

        if (e.Record is Tenant tenant)
        {
            switch (buttontext)
            {
                case "编辑":
                    await OnEditTenantClicked(tenant);
                    break;
                case "删除":
                    await OnDeleteTenantClicked(tenant);
                    break;
            }
        }
    }

    private void LoadTenants()
    {
        _tenants = _fsql!.Select<Tenant>().ToList();
        antList.Clear();
        antList = [.. _tenants];
        TenantCollectionView.Binding(antList);
    }

    private async void OnAddTenantClicked(object? sender, EventArgs e)
    {
        var name = NewTenantEntry.Text?.Trim();
        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        if (_fsql!.Select<Tenant>().Any(t => t.Name == name))
        {
            Program.DisplayAlert("Aviso", "La empresa ya existe.", "Aceptar");
            return;
        }
        var tenant = new Tenant
        {
            Name = name!,
            TaxNumber = NewTenantTaxNumberEntry.Text
        };
        await _fsql!.Insert(tenant).ExecuteAffrowsAsync();
        NewTenantEntry.Text = string.Empty;
        NewTenantTaxNumberEntry.Text = string.Empty;
        LoadTenants();
    }

    private async Task OnEditTenantClicked(Tenant tenant)
    {
        string? result = await DisplayPrompt.Show("Editar nombre de la empresa", "Por favor ingresa un nuevo nombre", initialValue: tenant.Name);
        if (!string.IsNullOrEmpty(result))
        {
            tenant.Name = result!;
            result = await DisplayPrompt.Show("Editar el número de identificación fiscal de la empresa", "Por favor ingrese el nuevo número de identificación fiscal", initialValue: tenant.TaxNumber);
            if (!string.IsNullOrEmpty(result) && result != tenant.TaxNumber)
            {
                tenant.TaxNumber = result;
            }
            await _fsql!.Update<Tenant>().SetSource(tenant).ExecuteAffrowsAsync();
            LoadTenants();
        }
    }

    private async Task OnDeleteTenantClicked(Tenant tenant)
    {
        if (Program.DisplayAlert("Confirmar", $"Confirmar para eliminar empresa：{tenant.Name}？", "Borrar", "Cancelar"))
        {
            await _fsql!.Delete<Tenant>().Where(t => t.Id == tenant.Id).ExecuteAffrowsAsync();
            LoadTenants();
        }
    }

}

