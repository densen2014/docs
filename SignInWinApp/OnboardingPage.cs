using AntdUI;
using SignInMauiApp.Models;
using System.ComponentModel;
using Button = AntdUI.Button;
using Label = AntdUI.Label;

namespace SignInWinApp;

public partial class OnboardingPage : AntdUI.Window
{
    private readonly IFreeSql? _fsql;
    private Label _labelHeader;
    private Input _tenantEntry;
    private Input _adminUserEntry;
    private Input _adminPassEntry;
    private Input _firstUserEntry;
    private Input _firstUserPassEntry;
    private Button _submitButton;
    private StackPanel VerticalStackLayout;
    private PageHeader? Header;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Action<object, object>? OnboardingCompleted { get; internal set; }

    private IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    public OnboardingPage(IFreeSql? fsql)
    {

        Header = new PageHeader();

        SuspendLayout();
        // 
        // Header
        // 
        Header.DividerShow = true;
        Header.Dock = DockStyle.Top;
        Header.Font = new Font("Microsoft YaHei UI", 11F);
        Header.Location = new Point(0, 0);
        Header.Margin = new Padding(4);
        Header.Name = "Header";
        Header.ShowButton = true;
        Header.Size = new Size(655, 35);
        Header.TabIndex = 16;
        Header.Text = "Fichaje";
        Header.UseForeColorDrawIcons = true;
        Header.UseLeftMargin = false;
        // 
        // OnboardingPage
        //
        ClientSize = new Size(970, 650);
        Controls.Add(Header);
        Font = new Font("Microsoft YaHei UI", 11F);
        FormBorderStyle = FormBorderStyle.Sizable;
        Margin = new Padding(3, 4, 3, 4);
        Mode = TAMode.Light;
        Name = "OnboardingPage";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Fichaje";

        var padding = new Padding(3);
        var size = new Size(500, 39);
        _tenantEntry = new Input
        {
            PlaceholderText = "Nombre de la empresa, la predeterminada es [Mi empresa]",
            Width = 600,
            Margin = padding,
            Size = size
        };
        _adminUserEntry = new Input
        {
            PlaceholderText = "Cuenta de administrador, la predeterminada es [admin]",
            Width = 600,
            Margin = padding,
            Size = size
        };
        _adminPassEntry = new Input
        {
            PlaceholderText = "Contraseña de administrador, la predeterminada es [123456]",
            PasswordChar = '*',
            Width = 600,
            Margin = padding,
            Size = size
        };
        _firstUserEntry = new Input
        {
            PlaceholderText = "El primer nombre de usuario, el predeterminado es [demo]",
            Width = 600,
            Margin = padding,
            Size = size
        };
        _firstUserPassEntry = new Input
        {
            PlaceholderText = "La contraseña del primer usuario, la predeterminada es [0]",
            PasswordChar = '*',
            Width = 600,
            Margin = padding,
            Size = size
        };
        _submitButton = new Button
        {
            Text = "Configuración completa",
            Width = 600,
            Margin = new Padding(0, 50, 0, 50),
            Size = size
        };
        _submitButton.Click += OnSubmitClicked;

        _labelHeader = new Label
        {
            Text = "Bienvenido, complete la configuración inicial：",
            Font = new Font("Microsoft YaHei UI", 18F),
            Margin = new Padding(0, 50, 0, 50),
            TextAlign = ContentAlignment.MiddleCenter,
            Size = new Size(500, 55)
        };
        VerticalStackLayout = new StackPanel
        {
            Vertical = true,
            Padding = new Padding(30),
            Dock = DockStyle.Fill,
        };

        Control[] controls = [
                _submitButton,
                _firstUserPassEntry,
                _firstUserEntry,
                _adminPassEntry,
                _adminUserEntry,
                _tenantEntry,
                _labelHeader,
                ];
        VerticalStackLayout.Controls.AddRange(controls);


        Controls.Add(VerticalStackLayout);
        ResumeLayout(false);

        _fsql = fsql;
        Text = "Configuración por primera vez";

        Icon = Program.GetAppIcon();
    }

    private async void OnSubmitClicked(object? sender, EventArgs e)
    {
        var tenantName = _tenantEntry.Text.IsNull("Mi empresa");
        var adminUser = _adminUserEntry.Text.IsNull("admin");
        var adminPass = _adminPassEntry.Text.IsNull("123456");
        var firstUser = _firstUserEntry.Text.IsNull("demo");
        var firstUserPass = _firstUserPassEntry.Text.IsNull("0");
        if (string.IsNullOrEmpty(tenantName) || string.IsNullOrEmpty(adminUser) || string.IsNullOrEmpty(adminPass) || string.IsNullOrEmpty(firstUser))
        {
            Program.DisplayAlert("Aviso", "Por favor complete toda la información", "Aceptar");
            return;
        }
        // 创建租户
        var tenant = new Tenant { Name = tenantName! };
        tenant.Id = (int)_fsql!.Insert(tenant).ExecuteIdentity();
        // 创建管理员
        var admin = new User { Username = adminUser!, Password = adminPass!, IsAdmin = true, TenantId = tenant.Id };
        _fsql?.Insert(admin).ExecuteAffrows();
        // 创建第一个普通用户
        var user = new User { Username = firstUser!, Password = firstUserPass!, IsAdmin = false, TenantId = tenant.Id };
        _fsql?.Insert(user).ExecuteAffrows();
        Program.DisplayAlert("Finalizar", "Inicialización completada！", "Ingrese al sistema");
        Close();
        OnboardingCompleted?.Invoke(this, EventArgs.Empty);
    }


}
