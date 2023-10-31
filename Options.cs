using ScreenShotTool.Properties;
using System.Diagnostics;

namespace ScreenShotTool
{
    public partial class Options : Form
    {
        MainForm mainForm;

        public Options(MainForm parent)
        {
            InitializeComponent();
            mainForm = parent;
            FillSettings();
        }

        private void FillSettings()
        {
            //textBoxFilename.Text = mainForm.PatternFileName;
            textBoxFilename.Text = Settings.Default.Filename;
            textBoxFolder.Text = Settings.Default.Foldername;
            comboBoxFileExtension.Text = Settings.Default.FileExtension;
            checkBoxTrim.Checked = Settings.Default.TrimChecked;
            trimTop.Value = Settings.Default.TrimTop;
            trimBottom.Value = Settings.Default.TrimBottom;
            trimLeft.Value = Settings.Default.TrimLeft;
            trimRight.Value = Settings.Default.TrimRight;
            textBoxAlternateTitle.Text = Settings.Default.AlternateTitle;
            numericUpDownTitleMaxLength.Value = Settings.Default.TitleMaxLength;
            textBoxSplitString.Text = Settings.Default.SplitTitleString;
            numericUpDownSplitIndex.Value = Settings.Default.SplitTitleIndex;
            numericUpDownJpegQuality.Value = Settings.Default.JpegQuality;
            checkBoxStartHidden.Checked = Settings.Default.StartHidden;
            checkBoxCropThumbnails.Checked = Settings.Default.CropThumbnails;
            numericThumbWidth.Value = Settings.Default.ThumbnailWidth;
            numericThumbHeight.Value = Settings.Default.ThumbnailHeight;
            checkBoxTrayTooltipInfo.Checked = Settings.Default.AllowTrayTooltipInfo;
            checkBoxTrayTooltipWarning.Checked = Settings.Default.AllowTrayTooltipWarning;
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            textBoxFolder.Text = dialog.SelectedPath;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ApplySettings();
            Close();
        }

        private void ApplySettings()
        {
            Settings.Default.Filename = textBoxFilename.Text;
            Settings.Default.Foldername = textBoxFolder.Text;
            Settings.Default.FileExtension = comboBoxFileExtension.Text;
            Settings.Default.TrimChecked = checkBoxTrim.Checked;
            Settings.Default.TrimTop = (int)trimTop.Value;
            Settings.Default.TrimBottom = (int)trimBottom.Value;
            Settings.Default.TrimLeft = (int)trimLeft.Value;
            Settings.Default.TrimRight = (int)trimRight.Value;
            Settings.Default.AlternateTitle = textBoxAlternateTitle.Text;
            Settings.Default.TitleMaxLength = (int)numericUpDownTitleMaxLength.Value;
            Settings.Default.SplitTitleString = textBoxSplitString.Text;
            Settings.Default.SplitTitleIndex = (int)numericUpDownSplitIndex.Value;
            Settings.Default.JpegQuality = (long)numericUpDownJpegQuality.Value;
            Settings.Default.StartHidden = checkBoxStartHidden.Checked;
            Settings.Default.CropThumbnails = checkBoxCropThumbnails.Checked;
            Settings.Default.ThumbnailWidth = (int)numericThumbWidth.Value;
            Settings.Default.ThumbnailHeight = (int)numericThumbHeight.Value;
            Settings.Default.AllowTrayTooltipInfo = checkBoxTrayTooltipInfo.Checked;
            Settings.Default.AllowTrayTooltipWarning = checkBoxTrayTooltipWarning.Checked;
            Settings.Default.Save();
            //mainForm.SaveSettings();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            mainForm.OpenHelp();
        }

        private void buttonBrowseFolder_Click(object sender, EventArgs e)
        {
            string folder = textBoxFolder.Text;
            if (Directory.Exists(folder))
            {
                mainForm.BrowseFolderInExplorer(folder);
            }
            else
            {
                string folderParent = Path.GetDirectoryName(textBoxFolder.Text) + "";
                Debug.WriteLine("Couldn't open directory " + folder + ", trying to open " + folderParent);
                mainForm.BrowseFolderInExplorer(folderParent);
            }
        }
    }
}
