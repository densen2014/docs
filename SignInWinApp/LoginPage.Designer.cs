using AntdUI;

namespace SignInWinApp
{
    partial class LoginPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblResult = new AntdUI.Label();
            WelcomeLabel = new AntdUI.Label();
            btnLogin = new AntdUI.Button();
            Logo = new PictureBox();
            ErrorLabel = new AntdUI.Label();
            TenantPicker = new Select();
            UsernameEntry = new Input();
            PasswordEntry = new Input();
            btnRegister = new AntdUI.Button();
            Header = new PageHeader();
            stackPanel1 = new StackPanel();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            stackPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblResult
            // 
            lblResult.Location = new Point(337, 251);
            lblResult.Margin = new Padding(3, 4, 3, 4);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(225, 32);
            lblResult.TabIndex = 3;
            lblResult.Text = "";
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WelcomeLabel
            // 
            WelcomeLabel.Location = new Point(3, 2);
            WelcomeLabel.Margin = new Padding(3, 2, 3, 2);
            WelcomeLabel.Name = "WelcomeLabel";
            WelcomeLabel.Size = new Size(578, 50);
            WelcomeLabel.TabIndex = 4;
            WelcomeLabel.Text = "签到系统登录";
            WelcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(20, 295);
            btnLogin.Margin = new Padding(20, 2, 20, 2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(544, 39);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "登录";
            // 
            // Logo
            // 
            Logo.Location = new Point(3, 56);
            Logo.Margin = new Padding(3, 2, 3, 2);
            Logo.Name = "Logo";
            Logo.Size = new Size(578, 106);
            Logo.SizeMode = PictureBoxSizeMode.Zoom;
            Logo.TabIndex = 5;
            Logo.TabStop = false;
            // 
            // ErrorLabel
            // 
            ErrorLabel.ForeColor = Color.Red;
            ErrorLabel.Location = new Point(3, 381);
            ErrorLabel.Margin = new Padding(3, 2, 3, 2);
            ErrorLabel.Name = "ErrorLabel";
            ErrorLabel.Size = new Size(578, 39);
            ErrorLabel.TabIndex = 7;
            ErrorLabel.Text = "ErrorLabel";
            ErrorLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TenantPicker
            // 
            TenantPicker.Location = new Point(20, 166);
            TenantPicker.Margin = new Padding(20, 2, 20, 2);
            TenantPicker.Name = "TenantPicker";
            TenantPicker.Size = new Size(544, 39);
            TenantPicker.TabIndex = 8;
            TenantPicker.Text = "选择租户";
            // 
            // UsernameEntry
            // 
            UsernameEntry.Location = new Point(20, 209);
            UsernameEntry.Margin = new Padding(20, 2, 20, 2);
            UsernameEntry.Name = "UsernameEntry";
            UsernameEntry.PlaceholderText = "用户名";
            UsernameEntry.Size = new Size(544, 39);
            UsernameEntry.TabIndex = 9;
            // 
            // PasswordEntry
            // 
            PasswordEntry.Location = new Point(20, 252);
            PasswordEntry.Margin = new Padding(20, 2, 20, 2);
            PasswordEntry.Name = "PasswordEntry";
            PasswordEntry.PasswordChar = '*';
            PasswordEntry.PlaceholderText = "密码";
            PasswordEntry.Size = new Size(544, 39);
            PasswordEntry.TabIndex = 10;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(20, 338);
            btnRegister.Margin = new Padding(20, 2, 20, 2);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(544, 39);
            btnRegister.TabIndex = 11;
            btnRegister.Text = "注册新用户";
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
            Header.Size = new Size(584, 35);
            Header.TabIndex = 15;
            Header.Text = "APP";
            Header.UseForeColorDrawIcons = true;
            Header.UseLeftMargin = false;
            // 
            // stackPanel1
            // 
            stackPanel1.Controls.Add(ErrorLabel);
            stackPanel1.Controls.Add(btnRegister);
            stackPanel1.Controls.Add(btnLogin);
            stackPanel1.Controls.Add(PasswordEntry);
            stackPanel1.Controls.Add(UsernameEntry);
            stackPanel1.Controls.Add(TenantPicker);
            stackPanel1.Controls.Add(Logo);
            stackPanel1.Controls.Add(WelcomeLabel);
            stackPanel1.Dock = DockStyle.Fill;
            stackPanel1.Location = new Point(0, 35);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Size = new Size(584, 445);
            stackPanel1.TabIndex = 0;
            stackPanel1.Text = "stackPanel1";
            stackPanel1.Vertical = true;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 480);
            Controls.Add(stackPanel1);
            Controls.Add(lblResult);
            Controls.Add(Header);
            Font = new Font("Microsoft YaHei UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            Mode = TAMode.Light;
            Name = "LoginPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "签到";
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            stackPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Label lblResult;
        private AntdUI.Label WelcomeLabel;
        private AntdUI.Button btnLogin;
        private PictureBox Logo;
        private AntdUI.Label ErrorLabel;
        private Select TenantPicker;
        private Input UsernameEntry;
        private Input PasswordEntry;
        private AntdUI.Button btnRegister;
        private PageHeader Header;
        private StackPanel stackPanel1;
    }
}
