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

namespace ScreenShotTool
{
    [SupportedOSPlatform("windows")]
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            labelVersion.Text = "Version " + ProductVersion;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenLink("https://github.com/snjo/ScreenShotTool/");
        }
    }
}
