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
            panel1 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            Header = new PageHeader();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
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
            WelcomeLabel.Dock = DockStyle.Top;
            WelcomeLabel.Location = new Point(0, 35);
            WelcomeLabel.Margin = new Padding(3, 2, 3, 2);
            WelcomeLabel.Name = "WelcomeLabel";
            WelcomeLabel.Size = new Size(643, 47);
            WelcomeLabel.TabIndex = 4;
            WelcomeLabel.Text = "签到系统登录";
            WelcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLogin
            // 
            btnLogin.Dock = DockStyle.Top;
            btnLogin.Location = new Point(96, 319);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(451, 39);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "登录";
            // 
            // Logo
            // 
            Logo.Dock = DockStyle.Top;
            Logo.Location = new Point(96, 82);
            Logo.Margin = new Padding(3, 2, 3, 2);
            Logo.Name = "Logo";
            Logo.Size = new Size(451, 120);
            Logo.SizeMode = PictureBoxSizeMode.Zoom;
            Logo.TabIndex = 5;
            Logo.TabStop = false;
            // 
            // ErrorLabel
            // 
            ErrorLabel.Dock = DockStyle.Top;
            ErrorLabel.ForeColor = Color.Red;
            ErrorLabel.Location = new Point(96, 397);
            ErrorLabel.Margin = new Padding(3, 2, 3, 2);
            ErrorLabel.Name = "ErrorLabel";
            ErrorLabel.Size = new Size(451, 39);
            ErrorLabel.TabIndex = 7;
            ErrorLabel.Text = "ErrorLabel";
            ErrorLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TenantPicker
            // 
            TenantPicker.Dock = DockStyle.Top;
            TenantPicker.Location = new Point(96, 202);
            TenantPicker.Margin = new Padding(3, 2, 3, 2);
            TenantPicker.Name = "TenantPicker";
            TenantPicker.Size = new Size(451, 39);
            TenantPicker.TabIndex = 8;
            TenantPicker.Text = "选择租户";
            // 
            // UsernameEntry
            // 
            UsernameEntry.Dock = DockStyle.Top;
            UsernameEntry.Location = new Point(96, 241);
            UsernameEntry.Margin = new Padding(3, 2, 3, 2);
            UsernameEntry.Name = "UsernameEntry";
            UsernameEntry.PlaceholderText = "用户名";
            UsernameEntry.Size = new Size(451, 39);
            UsernameEntry.TabIndex = 9;
            // 
            // PasswordEntry
            // 
            PasswordEntry.Dock = DockStyle.Top;
            PasswordEntry.Location = new Point(96, 280);
            PasswordEntry.Margin = new Padding(3, 2, 3, 2);
            PasswordEntry.Name = "PasswordEntry";
            PasswordEntry.PasswordChar = '*';
            PasswordEntry.PlaceholderText = "密码";
            PasswordEntry.Size = new Size(451, 39);
            PasswordEntry.TabIndex = 10;
            // 
            // btnRegister
            // 
            btnRegister.Dock = DockStyle.Top;
            btnRegister.Location = new Point(96, 358);
            btnRegister.Margin = new Padding(3, 4, 3, 4);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(451, 39);
            btnRegister.TabIndex = 11;
            btnRegister.Text = "注册新用户";
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(547, 82);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(96, 402);
            panel1.TabIndex = 12;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 82);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(96, 402);
            panel2.TabIndex = 13;
            // 
            // Header
            // 
            Header.DividerShow = true;
            Header.Dock = DockStyle.Top;
            Header.Font = new Font("Microsoft YaHei UI", 11F);
            Header.Location = new Point(0, 0);
            Header.Margin = new Padding(4, 4, 4, 4);
            Header.Name = "Header";
            Header.ShowButton = true;
            Header.Size = new Size(643, 35);
            Header.TabIndex = 15;
            Header.Text = "APP";
            Header.UseForeColorDrawIcons = true;
            Header.UseLeftMargin = false;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(643, 484);
            Controls.Add(ErrorLabel);
            Controls.Add(btnRegister);
            Controls.Add(btnLogin);
            Controls.Add(PasswordEntry);
            Controls.Add(UsernameEntry);
            Controls.Add(TenantPicker);
            Controls.Add(lblResult);
            Controls.Add(Logo);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(WelcomeLabel);
            Controls.Add(Header);
            Font = new Font("Microsoft YaHei UI", 11F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Mode = TAMode.Light;
            Name = "LoginPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "签到";
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private PageHeader Header;
    }
}
