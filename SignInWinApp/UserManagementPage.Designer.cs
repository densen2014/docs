using AntdUI;

namespace SignInWinApp
{
    partial class UserManagementPage
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
            Header = new PageHeader();
            btnRegister = new AntdUI.Button();
            labelTitle = new AntdUI.Label();
            UserCollectionView = new Table();
            SuspendLayout();
            // 
            // Header
            // 
            Header.DividerShow = true;
            Header.Dock = DockStyle.Top;
            Header.Font = new Font("Microsoft YaHei UI", 11F);
            Header.Location = new Point(0, 0);
            Header.Margin = new Padding(4);
            Header.Name = "Header";
            Header.ShowBack = true;
            Header.ShowButton = true;
            Header.Size = new Size(970, 35);
            Header.TabIndex = 15;
            Header.Text = "Fichaje";
            Header.UseForeColorDrawIcons = true;
            Header.UseLeftMargin = false;
            // 
            // btnRegister
            // 
            btnRegister.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRegister.Location = new Point(826, 45);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(137, 40);
            btnRegister.TabIndex = 0;
            btnRegister.Text = "Nuevo usuario";
            // 
            // labelTitle
            // 
            labelTitle.Dock = DockStyle.Top;
            labelTitle.Font = new Font("Microsoft YaHei UI", 18F);
            labelTitle.Location = new Point(0, 35);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(970, 55);
            labelTitle.TabIndex = 20;
            labelTitle.Text = "Gestión de usuarios";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UserCollectionView
            // 
            UserCollectionView.Dock = DockStyle.Fill;
            UserCollectionView.Gap = 12;
            UserCollectionView.Location = new Point(0, 90);
            UserCollectionView.Name = "UserCollectionView";
            UserCollectionView.Size = new Size(970, 560);
            UserCollectionView.TabIndex = 21;
            UserCollectionView.Text = "table1";
            // 
            // UserManagementPage
            // 
            ClientSize = new Size(970, 650);
            Controls.Add(btnRegister);
            Controls.Add(UserCollectionView);
            Controls.Add(labelTitle);
            Controls.Add(Header);
            Font = new Font("Microsoft YaHei UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            Mode = TAMode.Light;
            Name = "UserManagementPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fichaje";
            ResumeLayout(false);
        }

        #endregion
        private PageHeader Header;
        private AntdUI.Label labelTitle;
        private Table UserCollectionView;
        private AntdUI.Button btnRegister;
    }
}
