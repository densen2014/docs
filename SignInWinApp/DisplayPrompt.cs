public static class DisplayPrompt
{
    public static Task<string?> Show(string title, string prompt, string? initialValue = null)
    {
        var tcs = new TaskCompletionSource<string?>();
        var form = new Form()
        {
            Width = 400,
            Height = 160,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = title,
            StartPosition = FormStartPosition.CenterParent,
            MinimizeBox = false,
            MaximizeBox = false
        };

        var label = new Label() { Left = 10, Top = 15, Text = prompt, AutoSize = true };
        var textBox = new TextBox() { Left = 10, Top = 40, Width = 360, Text = initialValue ?? "" };
        var buttonOk = new Button() { Text = "OK", Left = 210, Width = 75, Top = 75, DialogResult = DialogResult.OK };
        var buttonCancel = new Button() { Text = "Cancel", Left = 295, Width = 75, Top = 75, DialogResult = DialogResult.Cancel };

        buttonOk.Click += (sender, e) => { form.DialogResult = DialogResult.OK; form.Close(); };
        buttonCancel.Click += (sender, e) => { form.DialogResult = DialogResult.Cancel; form.Close(); };

        form.Controls.Add(label);
        form.Controls.Add(textBox);
        form.Controls.Add(buttonOk);
        form.Controls.Add(buttonCancel);
        form.AcceptButton = buttonOk;
        form.CancelButton = buttonCancel;

        string? result = null;
        form.Shown += (s, e) => textBox.Focus();

        Task.Run(() =>
        {
            if (form.ShowDialog() == DialogResult.OK)
                result = textBox.Text;
            tcs.SetResult(result);
        });

        return tcs.Task;
    }
}