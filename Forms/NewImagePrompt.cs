namespace ScreenShotTool.Forms
{
    public partial class NewImagePrompt : Form
    {
        public Color color = Color.White;
        public int imageWidth = 500;
        public int imageHeight = 500;

        public NewImagePrompt()
        {
            InitializeComponent();
            numericWidth.Value = imageWidth;
            numericHeight.Value = imageHeight;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                buttonColor.BackColor = colorDialog1.Color;
                color = colorDialog1.Color;
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            imageWidth = (int)numericWidth.Value;
            imageHeight = (int)numericHeight.Value;
        }
    }
}
