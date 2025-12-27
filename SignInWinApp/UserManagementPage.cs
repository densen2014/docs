using AntdUI;
using SignInMauiApp.Models;

namespace SignInWinApp;

public partial class UserManagementPage : AntdUI.Window
{
    private readonly IFreeSql? _fsql;
    private List<User> _Users = new();
    private AntList<User> antList = new AntList<User>();
    private readonly Tenant _tenant;

    public UserManagementPage(Tenant tenant)
    {
        InitializeComponent();

        _fsql = Program.Fsql;
        _tenant = tenant;

        InitTableColumns();
        LoadUsers();
        UserCollectionView.CellButtonClick += Table_base_CellButtonClick;
    }

    private void InitTableColumns()
    {
        UserCollectionView.Columns =
        [
            new Column("Username", "Usuario",ColumnAlign.Center) { ColBreak=true },
            new Column("Name", "Nombre y apellido",ColumnAlign.Center) {ColBreak=true},
            new Column("TaxNumber", "NIF",ColumnAlign.Center) { ColBreak=true},
            new Column("WorkDuration", "Horas de trabajo",ColumnAlign.Center) { ColBreak=true },
        ];
    }

    //表格内部按钮事件
    private async void Table_base_CellButtonClick(object sender, TableButtonEventArgs e)
    {
        var buttontext = e.Btn.Text;

        if (e.Record is User user)
        {
            switch (buttontext)
            {
                case "编辑":
                    await OnEditUserClicked(user);
                    break;
                case "删除":
                    await OnDeleteUserClicked(user);
                    break;
            }
        }
    }
    private void LoadUsers()
    {
        _Users = _fsql!.Select<User>().Where(a => a.TenantId == _tenant.Id).ToList();
        antList.Clear();
        antList = [.. _Users];
        UserCollectionView.Binding(antList);
    }

    private async Task OnEditUserClicked(User user)
    {
        string? result = await DisplayPrompt.Show("Editar nombre de usuario", "Por favor ingresa un nuevo nombre", initialValue: user.Username);
        if (!string.IsNullOrEmpty(result))
        {
            user.Username = result!;
            result = await DisplayPrompt.Show("Editar contraseña", "Por favor ingrese nuevo contraseña");
            if (!string.IsNullOrEmpty(result) && result != user.Password)
            {
                user.Password = result!;
            }
            result = await DisplayPrompt.Show("Editar nombre y apellido", "Por favor ingrese nombre y apellido", initialValue: user.Name);
            if (!string.IsNullOrEmpty(result) && result != user.Name)
            {
                user.Name = result;
            }
            result = await DisplayPrompt.Show("Editar número de identificación fiscal", "Por favor ingrese el nuevo número de identificación fiscal", initialValue: user.TaxNumber);
            if (!string.IsNullOrEmpty(result) && result != user.TaxNumber)
            {
                user.TaxNumber = result;
            }
            result = await DisplayPrompt.Show("Editar horas de trabajo", "Por favor introduce un nuevo horario laboral", initialValue: user.WorkDuration.ToString());
            if (!string.IsNullOrEmpty(result) && result != user.WorkDuration.ToString() && int.TryParse(result, out var workDuration))
            {
                user.WorkDuration = workDuration;
            }
            await _fsql!.Update<User>().SetSource(user).ExecuteAffrowsAsync();
            LoadUsers();
        }
    }

    private async Task OnDeleteUserClicked(User user)
    {

        if (Program.DisplayAlert("Confirmar", $"Confirmar para eliminar empresa：{user.Name}？", "Borrar", "Cancelar"))
        {
            await _fsql!.Delete<User>().Where(t => t.Id == user.Id).ExecuteAffrowsAsync();
            LoadUsers();
        }
    }
}

