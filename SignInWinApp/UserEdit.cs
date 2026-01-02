using SignInMauiApp.Models;

namespace SignInWinApp;

public partial class UserEdit : UserControl
{
    private AntdUI.Window window;
    private User user;
    public bool submit;
    public UserEdit(AntdUI.Window _window, User _user)
    {
        this.window = _window;
        user = _user;
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
        UsernameEntry.Status = AntdUI.TType.None;
        //检查输入内容
        if (string.IsNullOrEmpty(UsernameEntry.Text))
        {
            UsernameEntry.Status = AntdUI.TType.Error;
            AntdUI.Message.warn(window, "El nombre no puede estar vacío！", autoClose: 3);
            return;
        }
        user.Username = UsernameEntry.Text;
        user.Name = NameEntry.Text;
        user.Password = PasswordEntry.Text;
        user.TaxNumber = TaxNumberEntry.Text;
        float workDuration = 7.5f;
        float.TryParse(WorkDurationEntry.Text, out workDuration);
        if (workDuration <= 0)
        {
            workDuration = 7.5f;
        }
        user.WorkDuration = workDuration;
        submit = true;
        this.Dispose();
    }

    private void InitData()
    {
        UsernameEntry.Text = user.Username;
        NameEntry.Text = user.Name!;
        PasswordEntry.Text = user.Password!;
        TaxNumberEntry.Text = user.TaxNumber!;
        WorkDurationEntry.Text = user.WorkDuration.ToString();
    }
}
