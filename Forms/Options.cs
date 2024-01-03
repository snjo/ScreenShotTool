using Hotkeys;
using ScreenShotTool.Properties;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace ScreenShotTool
{
    [SupportedOSPlatform("windows")]
    public partial class Options : Form
    {
        readonly MainForm mainForm;
        readonly Settings settings = Settings.Default;

        public Options(MainForm parent)
        {
            InitializeComponent();
            mainForm = parent;
            FillSettings();
        }

        private void FillSettings()
        {
            //Tab: Capture Output
            textBoxFolder.Text = settings.Foldername;

            textBoxFilename.Text = settings.Filename;
            comboBoxFileExtension.Text = settings.FileExtension;
            numericUpDownJpegQuality.Value = settings.JpegQuality;

            textBoxAlternateTitle.Text = settings.AlternateTitle;
            numericUpDownTitleMaxLength.Value = settings.TitleMaxLength;
            textBoxSplitString.Text = settings.SplitTitleString;
            numericUpDownSplitIndex.Value = settings.SplitTitleIndex;

            checkBoxTrim.Checked = settings.TrimChecked;
            trimTop.Value = settings.TrimTop;
            trimBottom.Value = settings.TrimBottom;
            trimLeft.Value = settings.TrimLeft;
            trimRight.Value = settings.TrimRight;

            numericUpDownCounter.Value = settings.Counter;

            //Tab: Modes
            checkBoxRegionComplete.Checked = settings.RegionCompletesOnMouseRelease;
            checkBoxRegionToFile.Checked = settings.RegionToFile;
            checkBoxRegionToEditor.Checked = settings.RegionToEditor;
            checkBoxRegionToClipboard.Checked = settings.RegionToClipboard;
            if (settings.RegionCompletesOnMouseRelease == false)
            {
                checkBoxRegionToFile.Enabled = false;
                checkBoxRegionToClipboard.Enabled = false;
                checkBoxRegionToEditor.Enabled = false;
            }
            checkBoxMaskRegion.Checked = settings.MaskRegion;

            checkBoxWindowToFile.Checked = settings.WindowToFile;
            checkBoxWindowToClipboard.Checked = settings.WindowToClipboard;
            checkBoxWindowToEditor.Checked = settings.WindowToEditor;

            checkBoxScreenToFile.Checked = settings.ScreenToFile;
            checkBoxScreenToClipboard.Checked = settings.ScreenToClipboard;
            checkBoxScreenToEditor.Checked = settings.ScreenToEditor;

            checkBoxAllScreensToFile.Checked = settings.AllScreensToFile;
            checkBoxAllScreensToClipboard.Checked = settings.AllScreensToClipboard;
            checkBoxAllScreensToEditor.Checked = settings.AllScreensToEditor;

            //Tab: Application
            checkBoxStartHidden.Checked = settings.StartHidden;
            checkBoxTrayTooltipInfoCapture.Checked = settings.AllowTrayTooltipInfoCapture;
            checkBoxTrayTooltipInfoFolder.Checked = settings.AllowTrayTooltipInfoFolder;
            checkBoxTrayTooltipWarning.Checked = settings.AllowTrayTooltipWarning;

            checkBoxCropThumbnails.Checked = settings.CropThumbnails;
            numericThumbWidth.Value = settings.ThumbnailWidth;
            numericThumbHeight.Value = settings.ThumbnailHeight;

            numericUpDownFramerate.Value = settings.MaxFramerate;

            //Tab: Hotkeys

            FillHotkeyGrid();
        }

        private void ButtonSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new();
            dialog.ShowDialog();
            textBoxFolder.Text = dialog.SelectedPath;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            ApplySettings();
            Close();
        }

        private void ApplySettings()
        {
            //Tab: Capture Output
            settings.Filename = textBoxFilename.Text;
            settings.Foldername = textBoxFolder.Text;
            settings.FileExtension = comboBoxFileExtension.Text;
            settings.JpegQuality = (long)numericUpDownJpegQuality.Value;

            settings.AlternateTitle = textBoxAlternateTitle.Text;
            settings.TitleMaxLength = (int)numericUpDownTitleMaxLength.Value;
            settings.SplitTitleString = textBoxSplitString.Text;
            settings.SplitTitleIndex = (int)numericUpDownSplitIndex.Value;

            settings.TrimChecked = checkBoxTrim.Checked;
            settings.TrimTop = (int)trimTop.Value;
            settings.TrimBottom = (int)trimBottom.Value;
            settings.TrimLeft = (int)trimLeft.Value;
            settings.TrimRight = (int)trimRight.Value;


            mainForm.SetCounter((int)numericUpDownCounter.Value, false);
            settings.Counter = mainForm.GetCounter();

            //Tab: Modes
            settings.RegionToFile = checkBoxRegionToFile.Checked;
            settings.RegionToClipboard = checkBoxRegionToClipboard.Checked;
            settings.RegionToEditor = checkBoxRegionToEditor.Checked;
            settings.RegionCompletesOnMouseRelease = checkBoxRegionComplete.Checked;
            settings.MaskRegion = checkBoxMaskRegion.Checked;

            settings.WindowToFile = checkBoxWindowToFile.Checked;
            settings.WindowToClipboard = checkBoxWindowToClipboard.Checked;
            settings.WindowToEditor = checkBoxWindowToEditor.Checked;

            settings.ScreenToFile = checkBoxScreenToFile.Checked;
            settings.ScreenToClipboard = checkBoxScreenToClipboard.Checked;
            settings.ScreenToEditor = checkBoxScreenToEditor.Checked;

            settings.AllScreensToFile = checkBoxAllScreensToFile.Checked;
            settings.AllScreensToClipboard = checkBoxAllScreensToClipboard.Checked;
            settings.AllScreensToEditor = checkBoxAllScreensToEditor.Checked;

            //Tab: Application
            settings.StartHidden = checkBoxStartHidden.Checked;
            settings.CropThumbnails = checkBoxCropThumbnails.Checked;
            settings.ThumbnailWidth = (int)numericThumbWidth.Value;
            settings.ThumbnailHeight = (int)numericThumbHeight.Value;
            settings.AllowTrayTooltipInfoCapture = checkBoxTrayTooltipInfoCapture.Checked;
            settings.AllowTrayTooltipInfoFolder = checkBoxTrayTooltipInfoFolder.Checked;
            settings.AllowTrayTooltipWarning = checkBoxTrayTooltipWarning.Checked;

            settings.MaxFramerate = (int)numericUpDownFramerate.Value;

            //Tab: Hotkeys
            int i = 0;
            foreach (KeyValuePair<string, Hotkey> kvp in mainForm.HotkeyList)
            {
                string keyName = kvp.Key;
                if (HotkeyGrid.Rows[i].Cells[1].Value == null)
                {
                    HotkeyGrid.Rows[i].Cells[1].Value = "";
                }
                Properties.Settings.Default["hk" + keyName + "Key"] = HotkeyGrid.Rows[i].Cells[1].Value.ToString();

                settings["hk" + keyName + "Ctrl"] = Convert.ToBoolean(HotkeyGrid.Rows[i].Cells[2].Value);
                settings["hk" + keyName + "Alt"] = Convert.ToBoolean(HotkeyGrid.Rows[i].Cells[3].Value);
                settings["hk" + keyName + "Shift"] = Convert.ToBoolean(HotkeyGrid.Rows[i].Cells[4].Value);
                settings["hk" + keyName + "Win"] = Convert.ToBoolean(HotkeyGrid.Rows[i].Cells[5].Value);

                mainForm.HotkeyList[keyName] = GetHotkeyFromGrid(mainForm.HotkeyList[keyName], HotkeyGrid.Rows[i].Cells);

                i++;
            }


            Settings.Default.Save();

            ReloadHotkeys();
            mainForm.UpdateTrimStatus();
            mainForm.SetInfoText();
            //mainForm.SaveSettings();
        }

        private static Hotkey GetHotkeyFromGrid(Hotkey hotkey, DataGridViewCellCollection settingRow)
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

        private void FillHotkeyGrid()
        {
            HotkeyGrid.Rows.Clear();
            HotkeyGrid.Rows.Add(mainForm.HotkeyList.Count);

            int i = 0;
            foreach (KeyValuePair<string, Hotkey> kvp in mainForm.HotkeyList)
            {
                string keyName = MainForm.CamelCaseToSpaces(kvp.Key);
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

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonHelp_Click(object sender, EventArgs e)
        {
            mainForm.OpenHelp();
        }

        private void ButtonBrowseFolder_Click(object sender, EventArgs e)
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

        private void ButtonRegisterHotkeys_Click(object sender, EventArgs e)
        {
        }

        private void ReloadHotkeys()
        {
            HotkeyTools.UpdateHotkeys(mainForm.HotkeyList, mainForm.HotkeyNames, mainForm);
            Debug.WriteLine("Released and re-registered hotkeys");
        }

        public void UpdateTrimCheck()
        {
            checkBoxTrim.Checked = settings.TrimChecked;
        }

        private void CheckBoxRegionComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRegionComplete.Checked)
            {
                checkBoxRegionToFile.Enabled = true;
                checkBoxRegionToClipboard.Enabled = true;
                checkBoxRegionToEditor.Enabled = true;
            }
            else
            {
                checkBoxRegionToFile.Enabled = false;
                checkBoxRegionToClipboard.Enabled = false;
                checkBoxRegionToEditor.Enabled = false;
            }
        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }

        private void ButtonResetOptions_Click(object sender, EventArgs e)
        {
            settings.Reset();
            HotkeyTools.ReleaseHotkeys(mainForm.HotkeyList);
            mainForm.LoadHotkeysFromSettings();
            FillSettings();
        }

        private void ButtonResetCounter_Click(object sender, EventArgs e)
        {
            mainForm.SetCounter(1, true);
            numericUpDownCounter.Value = 1;
        }

        private void TextBoxFilename_TextChanged(object sender, EventArgs e)
        {
            labelFileNameResult.Text = mainForm.ComposeFileName(textBoxFilename.Text, "Title");
        }
    }
}
