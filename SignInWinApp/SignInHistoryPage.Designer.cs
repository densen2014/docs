using AntdUI;

namespace SignInWinApp
{
    partial class SignInHistoryPage
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
            TitleLabel = new AntdUI.Label();
            Header = new PageHeader();
            VerticalStackLayout = new StackPanel();
            HistoryCollectionView = new Table();
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
            // TitleLabel
            // 
            TitleLabel.Location = new Point(3, 2);
            TitleLabel.Margin = new Padding(3, 2, 3, 2);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(578, 50);
            TitleLabel.TabIndex = 4;
            TitleLabel.Text = "历史";
            TitleLabel.TextAlign = ContentAlignment.MiddleCenter;
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
            VerticalStackLayout.Controls.Add(HistoryCollectionView);
            VerticalStackLayout.Controls.Add(TitleLabel);
            VerticalStackLayout.Dock = DockStyle.Fill;
            VerticalStackLayout.Location = new Point(0, 35);
            VerticalStackLayout.Name = "VerticalStackLayout";
            VerticalStackLayout.Size = new Size(584, 445);
            VerticalStackLayout.TabIndex = 0;
            VerticalStackLayout.Text = "VerticalStackLayout";
            VerticalStackLayout.Vertical = true;
            // 
            // HistoryCollectionView
            // 
            HistoryCollectionView.Dock = DockStyle.Fill;
            HistoryCollectionView.Gap = 12;
            HistoryCollectionView.Location = new Point(3, 57);
            HistoryCollectionView.Name = "HistoryCollectionView";
            HistoryCollectionView.Size = new Size(578, 385);
            HistoryCollectionView.TabIndex = 5;
            HistoryCollectionView.Text = "table1";
            // 
            // SignInHistoryPage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 480);
            Controls.Add(VerticalStackLayout);
            Controls.Add(lblResult);
            Controls.Add(Header);
            Font = new Font("Microsoft YaHei UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            Mode = TAMode.Light;
            Name = "SignInHistoryPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "签到";
            VerticalStackLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Label lblResult;
        private AntdUI.Label TitleLabel;
        private PageHeader Header;
        private StackPanel VerticalStackLayout;
        private Table HistoryCollectionView;
    }
}
