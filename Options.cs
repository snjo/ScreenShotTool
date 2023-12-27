using Hotkeys;
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
            checkBoxTrayTooltipInfoCapture.Checked = Settings.Default.AllowTrayTooltipInfoCapture;
            checkBoxTrayTooltipInfoFolder.Checked = Settings.Default.AllowTrayTooltipInfoFolder;
            checkBoxTrayTooltipWarning.Checked = Settings.Default.AllowTrayTooltipWarning;
            fillHotkeyGrid();
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
            Settings.Default.AllowTrayTooltipInfoCapture = checkBoxTrayTooltipInfoCapture.Checked;
            Settings.Default.AllowTrayTooltipInfoFolder = checkBoxTrayTooltipInfoFolder.Checked;
            Settings.Default.AllowTrayTooltipWarning = checkBoxTrayTooltipWarning.Checked;

            int i = 0;
            foreach (KeyValuePair<string, Hotkey> kvp in mainForm.HotkeyList)
            {
                string keyName = kvp.Key;
                if (HotkeyGrid.Rows[i].Cells[1].Value == null)
                {
                    HotkeyGrid.Rows[i].Cells[1].Value = "";
                }
                Properties.Settings.Default["hk" + keyName + "Key"] = HotkeyGrid.Rows[i].Cells[1].Value.ToString();

                Properties.Settings.Default["hk" + keyName + "Ctrl"] = Convert.ToBoolean(HotkeyGrid.Rows[i].Cells[2].Value);
                Properties.Settings.Default["hk" + keyName + "Alt"] = Convert.ToBoolean(HotkeyGrid.Rows[i].Cells[3].Value);
                Properties.Settings.Default["hk" + keyName + "Shift"] = Convert.ToBoolean(HotkeyGrid.Rows[i].Cells[4].Value);
                Properties.Settings.Default["hk" + keyName + "Win"] = Convert.ToBoolean(HotkeyGrid.Rows[i].Cells[5].Value);

                mainForm.HotkeyList[keyName] = GetHotkeyFromGrid(mainForm.HotkeyList[keyName], HotkeyGrid.Rows[i].Cells);

                i++;
            }

            Settings.Default.Save();

            reloadHotkeys();
            mainForm.updateTrimStatus();
            //mainForm.SaveSettings();
        }

        private Hotkey GetHotkeyFromGrid(Hotkey hotkey, DataGridViewCellCollection settingRow)
        {
            string settingKey = string.Empty;
            DataGridViewCell cell = settingRow[1];
            if (cell != null)
            {
                if (cell.Value != null)
                    settingKey = (string)cell.Value;
                if (settingKey == null) settingKey = string.Empty;
            }

            if (settingKey.Length > 0)
                hotkey.Key = settingKey;
            else
                hotkey.Key = new string("");

            hotkey.Ctrl = Convert.ToBoolean(settingRow[2].Value);
            hotkey.Alt = Convert.ToBoolean(settingRow[3].Value);
            hotkey.Shift = Convert.ToBoolean(settingRow[4].Value);
            hotkey.Win = Convert.ToBoolean(settingRow[5].Value);

            return hotkey;
        }

        private void fillHotkeyGrid()
        {
            HotkeyGrid.Rows.Clear();
            HotkeyGrid.Rows.Add(mainForm.HotkeyList.Count);

            int i = 0;
            foreach (KeyValuePair<string, Hotkey> kvp in mainForm.HotkeyList)
            {
                string keyName = kvp.Key;
                Hotkey hotkey = kvp.Value;
                HotkeyGrid.Rows[i].Cells[0].Value = keyName;
                HotkeyGrid.Rows[i].Cells[1].Value = hotkey.Key;
                HotkeyGrid.Rows[i].Cells[2].Value = hotkey.Ctrl;
                HotkeyGrid.Rows[i].Cells[3].Value = hotkey.Alt;
                HotkeyGrid.Rows[i].Cells[4].Value = hotkey.Shift;
                HotkeyGrid.Rows[i].Cells[5].Value = hotkey.Win;
                i++;
            }
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

        private void buttonRegisterHotkeys_Click(object sender, EventArgs e)
        {
        }

        private void reloadHotkeys()
        {
            HotkeyTools.UpdateHotkeys(mainForm.HotkeyList, mainForm.HotkeyNames, mainForm);
            Debug.WriteLine("Released and re-registered hotkeys");
        }

        public void updateTrimCheck()
        {
            checkBoxTrim.Checked = Settings.Default.TrimChecked;
        }
    }
}
