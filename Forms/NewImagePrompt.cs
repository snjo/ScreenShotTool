namespace ScreenShotTool.Forms;
#pragma warning disable CA1416 // Validate platform compatibility
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

    private void SelectColor_Click(object sender, EventArgs e)
    {
        ColorDialogAlpha colorDialogAlpha = new ColorDialogAlpha(Color.White);
        DialogResult result = colorDialogAlpha.ShowDialog();
        //DialogResult result = colorDialog1.ShowDialog();
        if (result == DialogResult.OK)
        {
            buttonColor.BackColor = colorDialogAlpha.Color;
            color = colorDialogAlpha.Color;
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
