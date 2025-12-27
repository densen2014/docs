using SignInMauiApp.Models;

namespace SignInWinApp;

public partial class TenantEdit : UserControl
{
    private AntdUI.Window window;
    private Tenant tenant;
    public bool submit;
    public TenantEdit(AntdUI.Window _window, Tenant _tenant)
    {
        this.window = _window;
        tenant = _tenant;
        InitializeComponent();
        //设置默认值
        InitData();
        // 绑定事件
        BindEventHandler();
    }

    private void BindEventHandler()
    {
        button_ok.Click += Button_ok_Click;
        button_cancel.Click += Button_cancel_Click;
    }

    private void Button_cancel_Click(object? sender, EventArgs e)
    {
        submit = false;
        this.Dispose();
    }

    private void Button_ok_Click(object? sender, EventArgs e)
    {
        TenantEntry.Status = AntdUI.TType.None;
        //检查输入内容
        if (string.IsNullOrEmpty(TenantEntry.Text))
        {
            TenantEntry.Status = AntdUI.TType.Error;
            AntdUI.Message.warn(window, "El nombre no puede estar vacío！", autoClose: 3);
            return;
        }
        tenant.Name = TenantEntry.Text; 
        tenant.TaxNumber = TaxNumberEntry.Text; 
        submit = true;
        this.Dispose();
    }

    private void InitData()
    {
        TenantEntry.Text = tenant.Name; 
        TaxNumberEntry.Text = tenant.TaxNumber!; 
    }
}
