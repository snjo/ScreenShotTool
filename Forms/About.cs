using System.Runtime.Versioning;

namespace ScreenShotTool
{
    [SupportedOSPlatform("windows")]
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            Font = new Font(this.Font.FontFamily, 9);
            labelVersion.Text = "Version " + ProductVersion;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenLink("https://github.com/snjo/ScreenShotTool/");
        }
    }
}
