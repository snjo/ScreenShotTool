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
            textBoxFilename.Text = mainForm.PatternFileName;
            textBoxFolder.Text = mainForm.PatternFolder;
            comboBoxFileExtension.Text = mainForm.PatternFileExtension;
            checkBoxTrim.Checked = mainForm.trim;
            trimTop.Value = mainForm.trimTop;
            trimBottom.Value = mainForm.trimBottom;
            trimLeft.Value = mainForm.trimLeft;
            trimRight.Value = mainForm.trimRight;
            textBoxAlternateTitle.Text = mainForm.alternateTitle;
            numericUpDownTitleMaxLength.Value = mainForm.titleMaxLength;
            textBoxSplitString.Text = mainForm.splitTitleString;
            numericUpDownSplitIndex.Value = mainForm.splitTitleIndex;
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
            mainForm.PatternFileName = textBoxFilename.Text;
            mainForm.PatternFolder = textBoxFolder.Text;
            mainForm.PatternFileExtension = comboBoxFileExtension.Text;
            mainForm.trim = checkBoxTrim.Checked;
            mainForm.trimTop = (int)trimTop.Value;
            mainForm.trimBottom = (int)trimBottom.Value;
            mainForm.trimLeft = (int)trimLeft.Value;
            mainForm.trimRight = (int)trimRight.Value;
            mainForm.alternateTitle = textBoxAlternateTitle.Text;
            mainForm.titleMaxLength = (int)numericUpDownTitleMaxLength.Value;
            mainForm.splitTitleString = textBoxSplitString.Text;
            mainForm.splitTitleIndex = (int)numericUpDownSplitIndex.Value;
            mainForm.SaveSettings();
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
    }
}
