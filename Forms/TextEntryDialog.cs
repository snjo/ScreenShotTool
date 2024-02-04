using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        TextResult = textBox1.Text;
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
        TextResult = textBox1.Text;
        DialogResult = DialogResult.OK;
    }
}
