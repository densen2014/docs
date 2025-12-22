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
            var tagTabItem4 = new TagTabItem();
            var tagTabItem5 = new TagTabItem();
            var tagTabItem6 = new TagTabItem();
            lblResult = new AntdUI.Label();
            WelcomeLabel = new AntdUI.Label();
            btnSignIn = new AntdUI.Button();
            Logo = new PictureBox();
            SignInResultLabel = new AntdUI.Label();
            ToolbarItems = new TabHeader();
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
            WelcomeLabel.Location = new Point(0, 155);
            WelcomeLabel.Margin = new Padding(3, 2, 3, 2);
            WelcomeLabel.Name = "WelcomeLabel";
            WelcomeLabel.Size = new Size(643, 65);
            WelcomeLabel.TabIndex = 4;
            WelcomeLabel.Text = "WelcomeLabel";
            WelcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSignIn
            // 
            btnSignIn.Dock = DockStyle.Top;
            btnSignIn.Location = new Point(0, 220);
            btnSignIn.Margin = new Padding(3, 4, 3, 4);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new Size(643, 42);
            btnSignIn.TabIndex = 2;
            btnSignIn.Text = "签到";
            btnSignIn.Click += OnSignInClicked;
            // 
            // Logo
            // 
            Logo.Dock = DockStyle.Top;
            Logo.Location = new Point(0, 35);
            Logo.Margin = new Padding(3, 2, 3, 2);
            Logo.Name = "Logo";
            Logo.Size = new Size(643, 120);
            Logo.SizeMode = PictureBoxSizeMode.Zoom;
            Logo.TabIndex = 5;
            Logo.TabStop = false;
            // 
            // SignInResultLabel
            // 
            SignInResultLabel.Dock = DockStyle.Top;
            SignInResultLabel.ForeColor = Color.Green;
            SignInResultLabel.Location = new Point(0, 262);
            SignInResultLabel.Margin = new Padding(3, 2, 3, 2);
            SignInResultLabel.Name = "SignInResultLabel";
            SignInResultLabel.Size = new Size(643, 38);
            SignInResultLabel.TabIndex = 7;
            SignInResultLabel.Text = "SignInResultLabel";
            SignInResultLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ToolbarItems
            // 
            ToolbarItems.DividerShow = true;
            ToolbarItems.Dock = DockStyle.Top;
            ToolbarItems.Font = new Font("Microsoft YaHei UI", 11F);
            tagTabItem4.Tag = null;
            tagTabItem4.Text = "签到";
            tagTabItem5.Tag = null;
            tagTabItem5.Text = "签到报表";
            tagTabItem6.Tag = null;
            tagTabItem6.Text = "Text";
            ToolbarItems.Items.Add(tagTabItem4);
            ToolbarItems.Items.Add(tagTabItem5);
            ToolbarItems.Items.Add(tagTabItem6);
            ToolbarItems.LeftGap = 40;
            ToolbarItems.Location = new Point(0, 0);
            ToolbarItems.Margin = new Padding(4, 4, 4, 4);
            ToolbarItems.Name = "ToolbarItems";
            ToolbarItems.ShowBack = true;
            ToolbarItems.ShowButton = true;
            ToolbarItems.Size = new Size(643, 35);
            ToolbarItems.TabIndex = 8;
            ToolbarItems.Text = "返回";
            ToolbarItems.UseForeColorDrawIcons = true;
            ToolbarItems.UseLeftMargin = false;
            ToolbarItems.TabChanged += ToolbarItems_TabChanged;
            // 
            // SignInPage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(643, 484);
            Controls.Add(SignInResultLabel);
            Controls.Add(btnSignIn);
            Controls.Add(WelcomeLabel);
            Controls.Add(lblResult);
            Controls.Add(Logo);
            Controls.Add(ToolbarItems);
            Font = new Font("Microsoft YaHei UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            Mode = TAMode.Light;
            Name = "SignInPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "签到";
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Label lblResult;
        private AntdUI.Label WelcomeLabel;
        private AntdUI.Button btnSignIn;
        private PictureBox Logo;
        private AntdUI.Label SignInResultLabel;
        private TabHeader ToolbarItems;
    }
}
