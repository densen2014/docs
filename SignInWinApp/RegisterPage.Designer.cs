using AntdUI;

namespace SignInWinApp
{
    partial class RegisterPage
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
            btnRegister = new AntdUI.Button();
            ErrorLabel = new AntdUI.Label();
            TenantPicker = new Select();
            UsernameEntry = new Input();
            PasswordEntry = new Input();
            Header = new PageHeader();
            VerticalStackLayout = new StackPanel();
            WorkDurationEntry = new Input();
            TaxNumberEntry = new Input();
            NameEntry = new Input();
            NewTenantEntry = new Input();
            WelcomeLabel = new AntdUI.Label();
            VerticalStackLayout.SuspendLayout();
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
            // btnRegister
            // 
            btnRegister.Location = new Point(20, 362);
            btnRegister.Margin = new Padding(20, 2, 20, 2);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(544, 39);
            btnRegister.TabIndex = 2;
            btnRegister.Text = "Registro";
            // 
            // ErrorLabel
            // 
            ErrorLabel.ForeColor = Color.Red;
            ErrorLabel.Location = new Point(3, 405);
            ErrorLabel.Margin = new Padding(3, 2, 3, 2);
            ErrorLabel.Name = "ErrorLabel";
            ErrorLabel.Size = new Size(578, 39);
            ErrorLabel.TabIndex = 7;
            ErrorLabel.Text = "ErrorLabel";
            ErrorLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TenantPicker
            // 
            TenantPicker.Location = new Point(20, 61);
            TenantPicker.Margin = new Padding(20, 2, 20, 2);
            TenantPicker.Name = "TenantPicker";
            TenantPicker.PlaceholderText = "Seleccionar empresa";
            TenantPicker.Size = new Size(544, 39);
            TenantPicker.TabIndex = 8;
            TenantPicker.Text = "Seleccionar empresa";
            // 
            // UsernameEntry
            // 
            UsernameEntry.Location = new Point(20, 147);
            UsernameEntry.Margin = new Padding(20, 2, 20, 2);
            UsernameEntry.Name = "UsernameEntry";
            UsernameEntry.PlaceholderText = "Nombre de usuario";
            UsernameEntry.Size = new Size(544, 39);
            UsernameEntry.TabIndex = 9;
            // 
            // PasswordEntry
            // 
            PasswordEntry.Location = new Point(20, 190);
            PasswordEntry.Margin = new Padding(20, 2, 20, 2);
            PasswordEntry.Name = "PasswordEntry";
            PasswordEntry.PasswordChar = '*';
            PasswordEntry.PlaceholderText = "Contraseña";
            PasswordEntry.Size = new Size(544, 39);
            PasswordEntry.TabIndex = 10;
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
            // VerticalStackLayout
            // 
            VerticalStackLayout.Controls.Add(ErrorLabel);
            VerticalStackLayout.Controls.Add(btnRegister);
            VerticalStackLayout.Controls.Add(WorkDurationEntry);
            VerticalStackLayout.Controls.Add(TaxNumberEntry);
            VerticalStackLayout.Controls.Add(NameEntry);
            VerticalStackLayout.Controls.Add(PasswordEntry);
            VerticalStackLayout.Controls.Add(UsernameEntry);
            VerticalStackLayout.Controls.Add(NewTenantEntry);
            VerticalStackLayout.Controls.Add(TenantPicker);
            VerticalStackLayout.Controls.Add(WelcomeLabel);
            VerticalStackLayout.Dock = DockStyle.Fill;
            VerticalStackLayout.Location = new Point(0, 35);
            VerticalStackLayout.Name = "VerticalStackLayout";
            VerticalStackLayout.Size = new Size(584, 514);
            VerticalStackLayout.TabIndex = 0;
            VerticalStackLayout.Text = "VerticalStackLayout";
            VerticalStackLayout.Vertical = true;
            // 
            // WorkDurationEntry
            // 
            WorkDurationEntry.Location = new Point(20, 319);
            WorkDurationEntry.Margin = new Padding(20, 2, 20, 2);
            WorkDurationEntry.Name = "WorkDurationEntry";
            WorkDurationEntry.PlaceholderText = "Horas de trabajo";
            WorkDurationEntry.Size = new Size(544, 39);
            WorkDurationEntry.TabIndex = 15;
            // 
            // TaxNumberEntry
            // 
            TaxNumberEntry.Location = new Point(20, 276);
            TaxNumberEntry.Margin = new Padding(20, 2, 20, 2);
            TaxNumberEntry.Name = "TaxNumberEntry";
            TaxNumberEntry.PlaceholderText = "Número de identificación fiscal";
            TaxNumberEntry.Size = new Size(544, 39);
            TaxNumberEntry.TabIndex = 14;
            // 
            // NameEntry
            // 
            NameEntry.Location = new Point(20, 233);
            NameEntry.Margin = new Padding(20, 2, 20, 2);
            NameEntry.Name = "NameEntry";
            NameEntry.PlaceholderText = "Nombre de usuario";
            NameEntry.Size = new Size(544, 39);
            NameEntry.TabIndex = 13;
            // 
            // NewTenantEntry
            // 
            NewTenantEntry.Location = new Point(20, 104);
            NewTenantEntry.Margin = new Padding(20, 2, 20, 2);
            NewTenantEntry.Name = "NewTenantEntry";
            NewTenantEntry.PlaceholderText = "Crear una nueva empresa (opcional)";
            NewTenantEntry.Size = new Size(544, 39);
            NewTenantEntry.TabIndex = 12;
            // 
            // WelcomeLabel
            // 
            WelcomeLabel.Font = new Font("Microsoft YaHei UI", 18F);
            WelcomeLabel.Location = new Point(3, 2);
            WelcomeLabel.Margin = new Padding(3, 2, 3, 2);
            WelcomeLabel.Name = "WelcomeLabel";
            WelcomeLabel.Size = new Size(578, 55);
            WelcomeLabel.TabIndex = 4;
            WelcomeLabel.Text = "Registro de usuario";
            WelcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RegisterPage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 549);
            Controls.Add(VerticalStackLayout);
            Controls.Add(lblResult);
            Controls.Add(Header);
            Font = new Font("Microsoft YaHei UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            Mode = TAMode.Light;
            Name = "RegisterPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fichaje";
            VerticalStackLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Label lblResult;
        private AntdUI.Button btnRegister;
        private AntdUI.Label ErrorLabel;
        private Select TenantPicker;
        private Input NewTenantEntry;
        private Input UsernameEntry;
        private Input PasswordEntry;
        private PageHeader Header;
        private StackPanel VerticalStackLayout;
        private AntdUI.Label WelcomeLabel;
        private Input NameEntry;
        private Input TaxNumberEntry;
        private Input WorkDurationEntry;
    }
}
