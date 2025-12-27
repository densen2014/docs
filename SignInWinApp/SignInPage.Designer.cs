using AntdUI;

namespace SignInWinApp
{
    partial class SignInPage
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
            btnSignIn = new AntdUI.Button();
            Logo = new PictureBox();
            SignInResultLabel = new AntdUI.Label();
            Header = new TabHeader();
            MenuTop = new AntdUI.Menu();
            btnSignOut = new AntdUI.Button();
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
            WelcomeLabel.Font = new Font("Microsoft YaHei UI", 18F);
            WelcomeLabel.Location = new Point(0, 193);
            WelcomeLabel.Margin = new Padding(3, 2, 3, 2);
            WelcomeLabel.Name = "WelcomeLabel";
            WelcomeLabel.Size = new Size(970, 55);
            WelcomeLabel.TabIndex = 4;
            WelcomeLabel.Text = "WelcomeLabel";
            WelcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSignIn
            // 
            btnSignIn.Dock = DockStyle.Top;
            btnSignIn.Location = new Point(0, 248);
            btnSignIn.Margin = new Padding(3, 4, 3, 4);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Padding = new Padding(10);
            btnSignIn.Size = new Size(970, 70);
            btnSignIn.TabIndex = 2;
            btnSignIn.Text = "Entrada";
            // 
            // Logo
            // 
            Logo.Dock = DockStyle.Top;
            Logo.Location = new Point(0, 73);
            Logo.Margin = new Padding(3, 2, 3, 2);
            Logo.Name = "Logo";
            Logo.Size = new Size(970, 120);
            Logo.SizeMode = PictureBoxSizeMode.Zoom;
            Logo.TabIndex = 5;
            Logo.TabStop = false;
            // 
            // SignInResultLabel
            // 
            SignInResultLabel.Dock = DockStyle.Top;
            SignInResultLabel.ForeColor = Color.Green;
            SignInResultLabel.Location = new Point(0, 388);
            SignInResultLabel.Margin = new Padding(3, 2, 3, 2);
            SignInResultLabel.Name = "SignInResultLabel";
            SignInResultLabel.Size = new Size(970, 38);
            SignInResultLabel.TabIndex = 7;
            SignInResultLabel.Text = "SignInResultLabel";
            SignInResultLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Header
            // 
            Header.DividerShow = true;
            Header.Dock = DockStyle.Top;
            Header.Font = new Font("Microsoft YaHei UI", 11F);
            Header.LeftGap = 40;
            Header.Location = new Point(0, 0);
            Header.Margin = new Padding(4);
            Header.Name = "Header";
            Header.ShowBack = true;
            Header.ShowButton = true;
            Header.Size = new Size(970, 35);
            Header.TabIndex = 8;
            Header.Text = "Fichaje";
            Header.UseForeColorDrawIcons = true;
            Header.UseLeftMargin = false;
            // 
            // MenuTop
            // 
            MenuTop.Dock = DockStyle.Top;
            MenuTop.Location = new Point(0, 35);
            MenuTop.Mode = TMenuMode.Horizontal;
            MenuTop.Name = "MenuTop";
            MenuTop.ScrollBarBlock = true;
            MenuTop.Size = new Size(970, 38);
            MenuTop.TabIndex = 9;
            MenuTop.Text = "menu1";
            // 
            // btnSignOut
            // 
            btnSignOut.Dock = DockStyle.Top;
            btnSignOut.Location = new Point(0, 318);
            btnSignOut.Margin = new Padding(3, 4, 3, 4);
            btnSignOut.Name = "btnSignOut";
            btnSignOut.Padding = new Padding(10);
            btnSignOut.Size = new Size(970, 70);
            btnSignOut.TabIndex = 10;
            btnSignOut.Text = "Salida";
            // 
            // SignInPage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(970, 650);
            Controls.Add(SignInResultLabel);
            Controls.Add(btnSignOut);
            Controls.Add(btnSignIn);
            Controls.Add(WelcomeLabel);
            Controls.Add(lblResult);
            Controls.Add(Logo);
            Controls.Add(MenuTop);
            Controls.Add(Header);
            Font = new Font("Microsoft YaHei UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            Mode = TAMode.Light;
            Name = "SignInPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fichaje";
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Label lblResult;
        private AntdUI.Label WelcomeLabel;
        private AntdUI.Button btnSignIn;
        private PictureBox Logo;
        private AntdUI.Label SignInResultLabel;
        private TabHeader Header;
        private AntdUI.Menu MenuTop;
        private AntdUI.Button btnSignOut;
    }
}
