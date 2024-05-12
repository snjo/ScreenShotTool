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
            string version = ProductVersion;

            if (version == "1.0.0.0")
            {
                version = "ClickOnce (Check .exe details)";
            }
            labelVersion.Text = "Version: " + version;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenLink("https://github.com/snjo/ScreenShotTool/");
        }
    }
}
