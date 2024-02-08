using System.Runtime.Versioning;

namespace ScreenShotTool.Forms;
[SupportedOSPlatform("windows")]

public partial class TextEntryDialog : Form
{
    public string TextResult = string.Empty;
    public TextEntryDialog(string text)
    {
        InitializeComponent();
        textBox1.Text = text;
    }

    private void TextBox1_TextChanged(object sender, EventArgs e)
    {
        TextResult = textBox1.Text;
    }

    private void ButtonOK_Click(object sender, EventArgs e)
    {
        TextResult = textBox1.Text;
        DialogResult = DialogResult.OK;
    }

    private void TextEntryDialog_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
