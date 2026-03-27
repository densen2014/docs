namespace SignInWinApp;

partial class UserEdit
{
    /// <summary> 
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region 组件设计器生成的代码

    /// <summary> 
    /// 设计器支持所需的方法 - 不要修改
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
        panel1 = new AntdUI.Panel();
        labelSplitTime = new AntdUI.Label();
        SplitTimeEntry = new AntdUI.Input();
        WorkDurationEntry = new AntdUI.Input();
        TaxNumberEntry = new AntdUI.Input();
        labelTaxNumber = new AntdUI.Label();
        NameEntry = new AntdUI.Input();
        labelName = new AntdUI.Label();
        PasswordEntry = new AntdUI.Input();
        labelPassword = new AntdUI.Label();
        UsernameEntry = new AntdUI.Input();
        labelUsername = new AntdUI.Label();
        divider1 = new AntdUI.Divider();
        stackPanel1 = new AntdUI.StackPanel();
        button_cancel = new AntdUI.Button();
        button_ok = new AntdUI.Button();
        panel1.SuspendLayout();
        stackPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.Controls.Add(SplitTimeEntry);
        panel1.Controls.Add(labelSplitTime);
        panel1.Controls.Add(WorkDurationEntry);
        panel1.Controls.Add(labelWorkDuration);
        panel1.Controls.Add(TaxNumberEntry);
        panel1.Controls.Add(labelTaxNumber);
        panel1.Controls.Add(NameEntry);
        panel1.Controls.Add(labelName);
        panel1.Controls.Add(PasswordEntry);
        panel1.Controls.Add(labelPassword);
        panel1.Controls.Add(UsernameEntry);
        panel1.Controls.Add(labelUsername);
        panel1.Controls.Add(divider1);
        panel1.Controls.Add(stackPanel1);
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Padding = new Padding(12);
        panel1.Shadow = 6;
        panel1.Size = new Size(500, 408);
        panel1.TabIndex = 0;
        panel1.Text = "panel1";
        // 
        // SplitTimeEntry
        // 
        SplitTimeEntry.Dock = DockStyle.Top;
        SplitTimeEntry.Font = new Font("Microsoft YaHei UI", 11F);
        SplitTimeEntry.Location = new Point(18, 346);
        SplitTimeEntry.Name = "SplitTimeEntry";
        SplitTimeEntry.PlaceholderText = "Hora de entrada la tarde, la predeterminada es [16,45]";
        SplitTimeEntry.Radius = 3;
        SplitTimeEntry.Size = new Size(464, 38);
        SplitTimeEntry.TabIndex = 4;
        // 
        // labelSplitTime
        // 
        labelSplitTime.Dock = DockStyle.Top;
        labelSplitTime.Font = new Font("Microsoft YaHei UI", 11F);
        labelSplitTime.Location = new Point(18, 322);
        labelSplitTime.Name = "labelSplitTime";
        labelSplitTime.Size = new Size(464, 24);
        labelSplitTime.TabIndex = 23;
        labelSplitTime.Text = "Hora de entrada la tarde";
        // 
        // WorkDurationEntry
        // 
        WorkDurationEntry.Dock = DockStyle.Top;
        WorkDurationEntry.Font = new Font("Microsoft YaHei UI", 11F);
        WorkDurationEntry.Location = new Point(18, 346);
        WorkDurationEntry.Name = "WorkDurationEntry";
        WorkDurationEntry.PlaceholderText = "Horas de trabajo, la predeterminada es [7,5]";
        WorkDurationEntry.Radius = 3;
        WorkDurationEntry.Size = new Size(464, 38);
        WorkDurationEntry.TabIndex = 4;
        // 
        // TaxNumberEntry
        // 
        TaxNumberEntry.Dock = DockStyle.Top;
        TaxNumberEntry.Font = new Font("Microsoft YaHei UI", 11F);
        TaxNumberEntry.Location = new Point(18, 284);
        TaxNumberEntry.Name = "TaxNumberEntry";
        TaxNumberEntry.PlaceholderText = "Número de identificación fiscal";
        TaxNumberEntry.Radius = 3;
        TaxNumberEntry.Size = new Size(464, 38);
        TaxNumberEntry.TabIndex = 3;
        // 
        // label4
        // 
        labelTaxNumber.Dock = DockStyle.Top;
        labelTaxNumber.Font = new Font("Microsoft YaHei UI", 11F);
        labelTaxNumber.Location = new Point(18, 260);
        labelTaxNumber.Name = "label4";
        labelTaxNumber.Size = new Size(464, 24);
        labelTaxNumber.TabIndex = 21;
        labelTaxNumber.Text = "NIF(Número de identificación fiscal)";
        // 
        // NameEntry
        // 
        NameEntry.Dock = DockStyle.Top;
        NameEntry.Font = new Font("Microsoft YaHei UI", 11F);
        NameEntry.Location = new Point(18, 222);
        NameEntry.Name = "NameEntry";
        NameEntry.PlaceholderText = "Nombre de usuario";
        NameEntry.Radius = 3;
        NameEntry.Size = new Size(464, 38);
        NameEntry.TabIndex = 2;
        // 
        // label2
        // 
        labelName.Dock = DockStyle.Top;
        labelName.Font = new Font("Microsoft YaHei UI", 11F);
        labelName.Location = new Point(18, 198);
        labelName.Name = "label2";
        labelName.Size = new Size(464, 24);
        labelName.TabIndex = 19;
        labelName.Text = "Nombre y apellido";
        // 
        // PasswordEntry
        // 
        PasswordEntry.Dock = DockStyle.Top;
        PasswordEntry.Font = new Font("Microsoft YaHei UI", 11F);
        PasswordEntry.Location = new Point(18, 160);
        PasswordEntry.Name = "PasswordEntry";
        PasswordEntry.PlaceholderText = "Contraseña";
        PasswordEntry.Radius = 3;
        PasswordEntry.Size = new Size(464, 38);
        PasswordEntry.TabIndex = 1;
        PasswordEntry.UseSystemPasswordChar = true;
        // 
        // label1
        // 
        labelPassword.Dock = DockStyle.Top;
        labelPassword.Font = new Font("Microsoft YaHei UI", 11F);
        labelPassword.Location = new Point(18, 136);
        labelPassword.Name = "label1";
        labelPassword.Size = new Size(464, 24);
        labelPassword.TabIndex = 17;
        labelPassword.Text = "Contraseña";
        // 
        // UsernameEntry
        // 
        UsernameEntry.Dock = DockStyle.Top;
        UsernameEntry.Font = new Font("Microsoft YaHei UI", 11F);
        UsernameEntry.Location = new Point(18, 98);
        UsernameEntry.Name = "UsernameEntry";
        UsernameEntry.PlaceholderText = "Nombre de usuario";
        UsernameEntry.Radius = 3;
        UsernameEntry.Size = new Size(464, 38);
        UsernameEntry.TabIndex = 0;
        // 
        // label3
        // 
        labelUsername.Dock = DockStyle.Top;
        labelUsername.Font = new Font("Microsoft YaHei UI", 11F);
        labelUsername.Location = new Point(18, 74);
        labelUsername.Name = "label3";
        labelUsername.Size = new Size(464, 24);
        labelUsername.TabIndex = 15;
        labelUsername.Text = "Usuario";
        // 
        // divider1
        // 
        divider1.Dock = DockStyle.Top;
        divider1.Font = new Font("Microsoft YaHei UI", 11F);
        divider1.Location = new Point(18, 62);
        divider1.Name = "divider1";
        divider1.Size = new Size(464, 12);
        divider1.TabIndex = 14;
        // 
        // stackPanel1
        // 
        stackPanel1.Controls.Add(button_cancel);
        stackPanel1.Controls.Add(button_ok);
        stackPanel1.Dock = DockStyle.Top;
        stackPanel1.Font = new Font("Microsoft YaHei UI", 11F);
        stackPanel1.Location = new Point(18, 18);
        stackPanel1.Name = "stackPanel1";
        stackPanel1.Size = new Size(464, 44);
        stackPanel1.TabIndex = 3;
        stackPanel1.Text = "stackPanel1";
        // 
        // button_cancel
        // 
        button_cancel.BorderWidth = 1F;
        button_cancel.Font = new Font("Microsoft YaHei UI", 9F);
        button_cancel.Ghost = true;
        button_cancel.Location = new Point(84, 3);
        button_cancel.Name = "button_cancel";
        button_cancel.Size = new Size(75, 38);
        button_cancel.TabIndex = 1;
        button_cancel.Text = "Cancelar";
        // 
        // button_ok
        // 
        button_ok.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
        button_ok.Location = new Point(3, 3);
        button_ok.Name = "button_ok";
        button_ok.Size = new Size(75, 38);
        button_ok.TabIndex = 0;
        button_ok.Text = "Aceptar";
        button_ok.Type = AntdUI.TTypeMini.Primary;
        // 
        // UserEdit
        // 
        Controls.Add(panel1);
        Font = new Font("Microsoft YaHei UI", 11F);
        Name = "UserEdit";
        Size = new Size(500, 408);
        panel1.ResumeLayout(false);
        stackPanel1.ResumeLayout(false);
        ResumeLayout(false);

    }

    #endregion

    private AntdUI.Panel panel1;
    private AntdUI.StackPanel stackPanel1;
    private AntdUI.Button button_cancel;
    private AntdUI.Button button_ok;
    private AntdUI.Divider divider1;
    private AntdUI.Input NameEntry;
    private AntdUI.Label labelName;
    private AntdUI.Input PasswordEntry;
    private AntdUI.Label labelPassword;
    private AntdUI.Input UsernameEntry;
    private AntdUI.Label labelUsername;
    private AntdUI.Input WorkDurationEntry;
    private AntdUI.Label labelWorkDuration;
    private AntdUI.Input SplitTimeEntry;
    private AntdUI.Label labelSplitTime;
    private AntdUI.Input TaxNumberEntry;
    private AntdUI.Label labelTaxNumber;
}
