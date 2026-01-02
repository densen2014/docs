using AntdUI;

namespace SignInWinApp
{
    partial class TenantManagementPage
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
            labelTitle = new AntdUI.Label();
            TenantCollectionView = new Table();
            ToolBar = new FlowPanel();
            btnAddTenant = new AntdUI.Button();
            NewTenantTaxNumberEntry = new Input();
            NewTenantEntry = new Input();
            ToolBar.SuspendLayout();
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
            // labelTitle
            // 
            labelTitle.Dock = DockStyle.Top;
            labelTitle.Font = new Font("Microsoft YaHei UI", 18F);
            labelTitle.Location = new Point(0, 35);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(970, 55);
            labelTitle.TabIndex = 20;
            labelTitle.Text = "Gestión de la empresa";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TenantCollectionView
            // 
            TenantCollectionView.Dock = DockStyle.Fill;
            TenantCollectionView.Gap = 12;
            TenantCollectionView.Location = new Point(0, 137);
            TenantCollectionView.Name = "TenantCollectionView";
            TenantCollectionView.Size = new Size(970, 513);
            TenantCollectionView.TabIndex = 21;
            TenantCollectionView.Text = "table1";
            // 
            // ToolBar
            // 
            ToolBar.Controls.Add(btnAddTenant);
            ToolBar.Controls.Add(NewTenantTaxNumberEntry);
            ToolBar.Controls.Add(NewTenantEntry);
            ToolBar.Dock = DockStyle.Top;
            ToolBar.Location = new Point(0, 90);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(970, 47);
            ToolBar.TabIndex = 0;
            ToolBar.Text = "flowPanel1";
            // 
            // btnAddTenant
            // 
            btnAddTenant.Location = new Point(656, 2);
            btnAddTenant.Margin = new Padding(20, 2, 20, 2);
            btnAddTenant.Name = "btnAddTenant";
            btnAddTenant.Size = new Size(166, 39);
            btnAddTenant.TabIndex = 13;
            btnAddTenant.Text = "Agregar empresa";
            // 
            // NewTenantTaxNumberEntry
            // 
            NewTenantTaxNumberEntry.Location = new Point(323, 2);
            NewTenantTaxNumberEntry.Margin = new Padding(5, 2, 5, 2);
            NewTenantTaxNumberEntry.Name = "NewTenantTaxNumberEntry";
            NewTenantTaxNumberEntry.PlaceholderText = "Nuevo número fiscal de empresa";
            NewTenantTaxNumberEntry.Size = new Size(308, 39);
            NewTenantTaxNumberEntry.TabIndex = 15;
            // 
            // NewTenantEntry
            // 
            NewTenantEntry.Location = new Point(5, 2);
            NewTenantEntry.Margin = new Padding(5, 2, 5, 2);
            NewTenantEntry.Name = "NewTenantEntry";
            NewTenantEntry.PlaceholderText = "Nuevo nombre de la empresa";
            NewTenantEntry.Size = new Size(308, 39);
            NewTenantEntry.TabIndex = 14;
            // 
            // TenantManagementPage
            // 
            ClientSize = new Size(970, 650);
            Controls.Add(TenantCollectionView);
            Controls.Add(ToolBar);
            Controls.Add(labelTitle);
            Controls.Add(Header);
            Font = new Font("Microsoft YaHei UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            Mode = TAMode.Light;
            Name = "TenantManagementPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fichaje";
            ToolBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private PageHeader Header;
        private AntdUI.Label labelTitle;
        private Table TenantCollectionView;
        private FlowPanel ToolBar;
        private Input NewTenantTaxNumberEntry;
        private AntdUI.Button btnAddTenant;
        private Input NewTenantEntry;
    }
}
