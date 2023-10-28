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
