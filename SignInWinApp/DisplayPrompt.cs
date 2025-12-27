using AntdUI;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = AntdUI.Button;
using Label = AntdUI.Label;

public static class DisplayPrompt
{
    public static Task<string?> Show(string title, string prompt, string? initialValue = null)
    {
        var tcs = new TaskCompletionSource<string?>();
        var form = new AntdUI.Window()
        {
            Width = 600,
            Height = 600,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = title,
            StartPosition = FormStartPosition.CenterParent,
            MinimizeBox = false,
            MaximizeBox = false
        };

        var header = new PageHeader()
        {
            DividerShow = true,
            Dock = DockStyle.Top,
            Font = new Font("Microsoft YaHei UI", 11F),
            Location = new Point(0, 0),
            Margin = new Padding(4),
            Name = "Header",
            ShowButton = true,
            Size = new Size(655, 35),
            TabIndex = 16,
            Text = "Fichaje",
            UseForeColorDrawIcons = true,
            UseLeftMargin = false,
        };
        form.Controls.Add(header);
        var padding = new Padding(3);
        var stackPanel = new StackPanel
        {
            Vertical = true,
            Padding = new Padding(30),
            Dock = DockStyle.Fill,
        };
        var label = new Label() { Text = prompt, AutoSize = true };
        var textBox = new Input() {  Text = initialValue ?? "" };
        var buttonOk = new Button() { Text = "OK", DialogResult = DialogResult.OK };
        var buttonCancel = new Button() { Text = "Cancel", DialogResult = DialogResult.Cancel };

        buttonOk.Click += (sender, e) => { form.DialogResult = DialogResult.OK; form.Close(); };
        buttonCancel.Click += (sender, e) => { form.DialogResult = DialogResult.Cancel; form.Close(); };

        stackPanel.Controls.Add(buttonCancel);
        stackPanel.Controls.Add(buttonOk);
        stackPanel.Controls.Add(textBox);
        stackPanel.Controls.Add(label);
        form.Controls.Add(stackPanel);
        form.AcceptButton = buttonOk;
        form.CancelButton = buttonCancel;

        string? result = null;
        form.Shown += (s, e) => textBox.Focus();

        Task.Run(() =>
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                result = textBox.Text;
            }

            tcs.SetResult(result);
        });

        return tcs.Task;
    }
}
