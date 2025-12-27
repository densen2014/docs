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
            VerticalStackLayout = new StackPanel();
            SignInResultLabel = new AntdUI.Label();
            Grid = new GridPanel();
            ImageQR = new ImagePreview();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            VerticalStackLayout.SuspendLayout();
            Grid.SuspendLayout();
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
            WelcomeLabel.Font = new Font("Microsoft YaHei UI", 18F);
            WelcomeLabel.Location = new Point(0, 35);
            WelcomeLabel.Margin = new Padding(3, 2, 3, 2);
            WelcomeLabel.Name = "WelcomeLabel";
            WelcomeLabel.Size = new Size(970, 55);
            WelcomeLabel.TabIndex = 4;
            WelcomeLabel.Text = "Registro de Jornada Laboral";
            WelcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(20, 228);
            btnLogin.Margin = new Padding(20, 2, 20, 2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(439, 39);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Iniciar sesión";
            // 
            // Logo
            // 
            Logo.Location = new Point(3, 2);
            Logo.Margin = new Padding(3, 2, 3, 2);
            Logo.Name = "Logo";
            Logo.Size = new Size(473, 93);
            Logo.SizeMode = PictureBoxSizeMode.Zoom;
            Logo.TabIndex = 5;
            Logo.TabStop = false;
            // 
            // ErrorLabel
            // 
            ErrorLabel.ForeColor = Color.Red;
            ErrorLabel.Location = new Point(3, 357);
            ErrorLabel.Margin = new Padding(3, 2, 3, 2);
            ErrorLabel.Name = "ErrorLabel";
            ErrorLabel.Size = new Size(473, 39);
            ErrorLabel.TabIndex = 7;
            ErrorLabel.Text = "ErrorLabel";
            ErrorLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TenantPicker
            // 
            TenantPicker.Location = new Point(20, 99);
            TenantPicker.Margin = new Padding(20, 2, 20, 2);
            TenantPicker.Name = "TenantPicker";
            TenantPicker.PlaceholderText = "Seleccionar empresa";
            TenantPicker.Size = new Size(439, 39);
            TenantPicker.TabIndex = 8;
            TenantPicker.Text = "Seleccionar empresa";
            // 
            // UsernameEntry
            // 
            UsernameEntry.Location = new Point(20, 142);
            UsernameEntry.Margin = new Padding(20, 2, 20, 2);
            UsernameEntry.Name = "UsernameEntry";
            UsernameEntry.PlaceholderText = "Usuario";
            UsernameEntry.Size = new Size(439, 39);
            UsernameEntry.TabIndex = 9;
            // 
            // PasswordEntry
            // 
            PasswordEntry.Location = new Point(20, 185);
            PasswordEntry.Margin = new Padding(20, 2, 20, 2);
            PasswordEntry.Name = "PasswordEntry";
            PasswordEntry.PasswordChar = '*';
            PasswordEntry.PlaceholderText = "Contraseña";
            PasswordEntry.Size = new Size(439, 39);
            PasswordEntry.TabIndex = 10;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(20, 271);
            btnRegister.Margin = new Padding(20, 2, 20, 2);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(439, 39);
            btnRegister.TabIndex = 11;
            btnRegister.Text = "Registrar nuevo usuario";
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
            Header.Size = new Size(970, 35);
            Header.TabIndex = 15;
            Header.Text = "Fichaje";
            Header.UseForeColorDrawIcons = true;
            Header.UseLeftMargin = false;
            // 
            // VerticalStackLayout
            // 
            VerticalStackLayout.Controls.Add(ErrorLabel);
            VerticalStackLayout.Controls.Add(SignInResultLabel);
            VerticalStackLayout.Controls.Add(btnRegister);
            VerticalStackLayout.Controls.Add(btnLogin);
            VerticalStackLayout.Controls.Add(PasswordEntry);
            VerticalStackLayout.Controls.Add(UsernameEntry);
            VerticalStackLayout.Controls.Add(TenantPicker);
            VerticalStackLayout.Controls.Add(Logo);
            VerticalStackLayout.Dock = DockStyle.Fill;
            VerticalStackLayout.Location = new Point(3, 3);
            VerticalStackLayout.Name = "VerticalStackLayout";
            VerticalStackLayout.Size = new Size(479, 554);
            VerticalStackLayout.TabIndex = 0;
            VerticalStackLayout.Text = "VerticalStackLayout";
            VerticalStackLayout.Vertical = true;
            // 
            // SignInResultLabel
            // 
            SignInResultLabel.ForeColor = Color.Green;
            SignInResultLabel.Location = new Point(3, 314);
            SignInResultLabel.Margin = new Padding(3, 2, 3, 2);
            SignInResultLabel.Name = "SignInResultLabel";
            SignInResultLabel.Size = new Size(473, 39);
            SignInResultLabel.TabIndex = 12;
            SignInResultLabel.Text = "SignInResultLabel";
            SignInResultLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Grid
            // 
            Grid.Controls.Add(ImageQR);
            Grid.Controls.Add(VerticalStackLayout);
            Grid.Dock = DockStyle.Fill;
            Grid.Location = new Point(0, 90);
            Grid.Name = "Grid";
            Grid.Size = new Size(970, 560);
            Grid.Span = "50% 50%";
            Grid.TabIndex = 0;
            Grid.Text = "gridPanel1";
            // 
            // ImageQR
            // 
            ImageQR.Dock = DockStyle.Fill;
            ImageQR.Location = new Point(488, 3);
            ImageQR.Name = "ImageQR";
            ImageQR.Padding = new Padding(20);
            ImageQR.ShowDefaultBtn = false;
            ImageQR.Size = new Size(479, 554);
            ImageQR.TabIndex = 1;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(970, 650);
            Controls.Add(Grid);
            Controls.Add(lblResult);
            Controls.Add(WelcomeLabel);
            Controls.Add(Header);
            Font = new Font("Microsoft YaHei UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            Mode = TAMode.Light;
            Name = "LoginPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fichaje";
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            VerticalStackLayout.ResumeLayout(false);
            Grid.ResumeLayout(false);
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
        private StackPanel VerticalStackLayout;
        private GridPanel Grid;
        private ImagePreview ImageQR;
        private AntdUI.Label SignInResultLabel;
    }
}
