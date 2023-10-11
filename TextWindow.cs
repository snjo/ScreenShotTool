using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShotTool
{
    public partial class TextWindow : Form
    {
        MainForm mainForm;
        public TextWindow(MainForm parent, string text)
        {
            InitializeComponent();
            mainForm = parent;
            textBox1.Text = text;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
