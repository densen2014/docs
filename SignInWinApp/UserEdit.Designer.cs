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
        WorkDurationEntry = new AntdUI.InputNumber();
        label5 = new AntdUI.Label();
        TaxNumberEntry = new AntdUI.Input();
        label4 = new AntdUI.Label();
        NameEntry = new AntdUI.Input();
        label2 = new AntdUI.Label();
        PasswordEntry = new AntdUI.Input();
        label1 = new AntdUI.Label();
        UsernameEntry = new AntdUI.Input();
        label3 = new AntdUI.Label();
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
        panel1.Controls.Add(WorkDurationEntry);
        panel1.Controls.Add(label5);
        panel1.Controls.Add(TaxNumberEntry);
        panel1.Controls.Add(label4);
        panel1.Controls.Add(NameEntry);
        panel1.Controls.Add(label2);
        panel1.Controls.Add(PasswordEntry);
        panel1.Controls.Add(label1);
        panel1.Controls.Add(UsernameEntry);
        panel1.Controls.Add(label3);
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
        // WorkDurationEntry
        // 
        WorkDurationEntry.Dock = DockStyle.Top;
        WorkDurationEntry.Font = new Font("Microsoft YaHei UI", 11F);
        WorkDurationEntry.Location = new Point(18, 346);
        WorkDurationEntry.Name = "WorkDurationEntry";
        WorkDurationEntry.PlaceholderText = "Horas de trabajo";
        WorkDurationEntry.Radius = 3;
        WorkDurationEntry.Size = new Size(464, 38);
        WorkDurationEntry.TabIndex = 24;
        WorkDurationEntry.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
        WorkDurationEntry.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // label5
        // 
        label5.Dock = DockStyle.Top;
        label5.Font = new Font("Microsoft YaHei UI", 11F);
        label5.Location = new Point(18, 322);
        label5.Name = "label5";
        label5.Size = new Size(464, 24);
        label5.TabIndex = 23;
        label5.Text = "Horas de trabajo";
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
        TaxNumberEntry.TabIndex = 22;
        // 
        // label4
        // 
        label4.Dock = DockStyle.Top;
        label4.Font = new Font("Microsoft YaHei UI", 11F);
        label4.Location = new Point(18, 260);
        label4.Name = "label4";
        label4.Size = new Size(464, 24);
        label4.TabIndex = 21;
        label4.Text = "NIF(Número de identificación fiscal)";
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
        NameEntry.TabIndex = 20;
        // 
        // label2
        // 
        label2.Dock = DockStyle.Top;
        label2.Font = new Font("Microsoft YaHei UI", 11F);
        label2.Location = new Point(18, 198);
        label2.Name = "label2";
        label2.Size = new Size(464, 24);
        label2.TabIndex = 19;
        label2.Text = "Nombre y apellido";
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
        PasswordEntry.TabIndex = 18;
        // 
        // label1
        // 
        label1.Dock = DockStyle.Top;
        label1.Font = new Font("Microsoft YaHei UI", 11F);
        label1.Location = new Point(18, 136);
        label1.Name = "label1";
        label1.Size = new Size(464, 24);
        label1.TabIndex = 17;
        label1.Text = "Contraseña";
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
        UsernameEntry.TabIndex = 16;
        // 
        // label3
        // 
        label3.Dock = DockStyle.Top;
        label3.Font = new Font("Microsoft YaHei UI", 11F);
        label3.Location = new Point(18, 74);
        label3.Name = "label3";
        label3.Size = new Size(464, 24);
        label3.TabIndex = 15;
        label3.Text = "Usuario";
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
    private AntdUI.Label label2;
    private AntdUI.Input PasswordEntry;
    private AntdUI.Label label1;
    private AntdUI.Input UsernameEntry;
    private AntdUI.Label label3;
    private AntdUI.InputNumber WorkDurationEntry;
    private AntdUI.Label label5;
    private AntdUI.Input TaxNumberEntry;
    private AntdUI.Label label4;
}
