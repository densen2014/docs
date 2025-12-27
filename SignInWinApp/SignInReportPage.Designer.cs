using AntdUI;

namespace SignInWinApp
{
    partial class SignInReportPage
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
            Header = new PageHeader();
            HorizontalStackLayout = new AntdUI.In.FlowLayoutPanel();
            UsernameLabel = new AntdUI.Label();
            UsernamePicker = new Select();
            labelYear = new AntdUI.Label();
            YearPicker = new Select();
            labelMonth = new AntdUI.Label();
            MonthPicker = new Select();
            btnQuery = new AntdUI.Button();
            btnExportExcel = new AntdUI.Button();
            btnExportPDF = new AntdUI.Button();
            ReportCollectionView = new Table();
            labelTitle = new AntdUI.Label();
            HorizontalStackLayout.SuspendLayout();
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
            // HorizontalStackLayout
            // 
            HorizontalStackLayout.Controls.Add(UsernameLabel);
            HorizontalStackLayout.Controls.Add(UsernamePicker);
            HorizontalStackLayout.Controls.Add(labelYear);
            HorizontalStackLayout.Controls.Add(YearPicker);
            HorizontalStackLayout.Controls.Add(labelMonth);
            HorizontalStackLayout.Controls.Add(MonthPicker);
            HorizontalStackLayout.Controls.Add(btnQuery);
            HorizontalStackLayout.Controls.Add(btnExportExcel);
            HorizontalStackLayout.Controls.Add(btnExportPDF);
            HorizontalStackLayout.Dock = DockStyle.Top;
            HorizontalStackLayout.Location = new Point(0, 90);
            HorizontalStackLayout.Name = "HorizontalStackLayout";
            HorizontalStackLayout.Size = new Size(970, 46);
            HorizontalStackLayout.TabIndex = 0;
            HorizontalStackLayout.WrapContents = false;
            // 
            // UsernameLabel
            // 
            UsernameLabel.AutoSizeMode = TAutoSize.Auto;
            UsernameLabel.AutoSizePadding = true;
            UsernameLabel.Location = new Point(3, 3);
            UsernameLabel.Name = "UsernameLabel";
            UsernameLabel.Padding = new Padding(0, 8, 0, 0);
            UsernameLabel.Size = new Size(97, 27);
            UsernameLabel.TabIndex = 12;
            UsernameLabel.Text = "TRABAJADOR";
            // 
            // UsernamePicker
            // 
            UsernamePicker.Location = new Point(108, 2);
            UsernamePicker.Margin = new Padding(5, 2, 5, 2);
            UsernamePicker.Name = "UsernamePicker";
            UsernamePicker.PlaceholderText = "Usuario";
            UsernamePicker.Size = new Size(101, 39);
            UsernamePicker.TabIndex = 9;
            UsernamePicker.Text = "Usuario";
            // 
            // labelYear
            // 
            labelYear.AutoSizeMode = TAutoSize.Auto;
            labelYear.AutoSizePadding = true;
            labelYear.Location = new Point(217, 3);
            labelYear.Name = "labelYear";
            labelYear.Padding = new Padding(0, 8, 0, 0);
            labelYear.Size = new Size(36, 27);
            labelYear.TabIndex = 13;
            labelYear.Text = "Años";
            // 
            // YearPicker
            // 
            YearPicker.Location = new Point(261, 2);
            YearPicker.Margin = new Padding(5, 2, 5, 2);
            YearPicker.Name = "YearPicker";
            YearPicker.PlaceholderText = "Años";
            YearPicker.Size = new Size(83, 39);
            YearPicker.TabIndex = 10;
            YearPicker.Text = "Años";
            // 
            // labelMonth
            // 
            labelMonth.AutoSizeMode = TAutoSize.Auto;
            labelMonth.AutoSizePadding = true;
            labelMonth.Location = new Point(352, 3);
            labelMonth.Name = "labelMonth";
            labelMonth.Padding = new Padding(0, 8, 0, 0);
            labelMonth.Size = new Size(30, 27);
            labelMonth.TabIndex = 18;
            labelMonth.Text = "Mes";
            // 
            // MonthPicker
            // 
            MonthPicker.Location = new Point(390, 2);
            MonthPicker.Margin = new Padding(5, 2, 5, 2);
            MonthPicker.Name = "MonthPicker";
            MonthPicker.PlaceholderText = "Mes";
            MonthPicker.Size = new Size(77, 39);
            MonthPicker.TabIndex = 11;
            MonthPicker.Text = "Mes";
            // 
            // btnQuery
            // 
            btnQuery.Location = new Point(475, 3);
            btnQuery.Name = "btnQuery";
            btnQuery.Size = new Size(86, 39);
            btnQuery.TabIndex = 15;
            btnQuery.Text = "Consulta";
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(567, 3);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(86, 39);
            btnExportExcel.TabIndex = 16;
            btnExportExcel.Text = "Excel";
            // 
            // btnExportPDF
            // 
            btnExportPDF.Location = new Point(659, 3);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new Size(86, 39);
            btnExportPDF.TabIndex = 17;
            btnExportPDF.Text = "PDF";
            // 
            // ReportCollectionView
            // 
            ReportCollectionView.Dock = DockStyle.Fill;
            ReportCollectionView.Gap = 12;
            ReportCollectionView.Location = new Point(0, 136);
            ReportCollectionView.Name = "ReportCollectionView";
            ReportCollectionView.Size = new Size(970, 514);
            ReportCollectionView.TabIndex = 1;
            ReportCollectionView.Text = "table1";
            // 
            // labelTitle
            // 
            labelTitle.Dock = DockStyle.Top;
            labelTitle.Font = new Font("Microsoft YaHei UI", 18F);
            labelTitle.Location = new Point(0, 35);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(970, 55);
            labelTitle.TabIndex = 19;
            labelTitle.Text = "REGISTRO DIARIO DE JORNADA EN TRABAJADORES";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SignInReportPage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(970, 650);
            Controls.Add(ReportCollectionView);
            Controls.Add(HorizontalStackLayout);
            Controls.Add(lblResult);
            Controls.Add(labelTitle);
            Controls.Add(Header);
            Font = new Font("Microsoft YaHei UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            Mode = TAMode.Light;
            Name = "SignInReportPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fichaje";
            HorizontalStackLayout.ResumeLayout(false);
            HorizontalStackLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Label lblResult;
        private PageHeader Header;
        private AntdUI.In.FlowLayoutPanel HorizontalStackLayout;
        private AntdUI.Label UsernameLabel;
        private Select UsernamePicker;
        private AntdUI.Label labelYear;
        private Select YearPicker;
        private Select MonthPicker;
        private AntdUI.Button btnQuery;
        private AntdUI.Button btnExportExcel;
        private AntdUI.Button btnExportPDF;
        private AntdUI.Label labelMonth;
        private Table ReportCollectionView;
        private AntdUI.Label labelTitle;
    }
}
