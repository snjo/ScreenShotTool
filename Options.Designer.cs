namespace ScreenShotTool
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            checkBoxTrim = new CheckBox();
            buttonBrowseFolder = new Button();
            buttonHelp = new Button();
            label3 = new Label();
            comboBoxFileExtension = new ComboBox();
            textBoxFilename = new TextBox();
            buttonSelectFolder = new Button();
            trimTop = new NumericUpDown();
            trimBottom = new NumericUpDown();
            trimRight = new NumericUpDown();
            trimLeft = new NumericUpDown();
            label1 = new Label();
            textBoxFolder = new TextBox();
            buttonOK = new Button();
            buttonApply = new Button();
            buttonCancel = new Button();
            textBoxAlternateTitle = new TextBox();
            label2 = new Label();
            label4 = new Label();
            numericUpDownTitleMaxLength = new NumericUpDown();
            label5 = new Label();
            numericUpDownSplitIndex = new NumericUpDown();
            label6 = new Label();
            textBoxSplitString = new TextBox();
            numericUpDownJpegQuality = new NumericUpDown();
            label7 = new Label();
            checkBoxStartHidden = new CheckBox();
            checkBoxCropThumbnails = new CheckBox();
            numericThumbWidth = new NumericUpDown();
            numericThumbHeight = new NumericUpDown();
            label8 = new Label();
            label9 = new Label();
            checkBoxTrayTooltipInfoCapture = new CheckBox();
            checkBoxTrayTooltipWarning = new CheckBox();
            tabControl1 = new TabControl();
            tabPageOutput = new TabPage();
            tabPageModes = new TabPage();
            checkBoxMaskRegion = new CheckBox();
            label17 = new Label();
            label13 = new Label();
            label12 = new Label();
            numericUpDownFramerate = new NumericUpDown();
            checkBoxRegionComplete = new CheckBox();
            checkBoxAllScreensToClipboard = new CheckBox();
            checkBoxAllScreensToFile = new CheckBox();
            label10 = new Label();
            checkBoxScreenToClipboard = new CheckBox();
            checkBoxScreenToFile = new CheckBox();
            label11 = new Label();
            checkBoxRegionToClipboard = new CheckBox();
            checkBoxRegionToFile = new CheckBox();
            labelRegion = new Label();
            checkBoxWindowToClipboard = new CheckBox();
            checkBoxWindowToFile = new CheckBox();
            labelWindow = new Label();
            tabPageApplication = new TabPage();
            label16 = new Label();
            label15 = new Label();
            label14 = new Label();
            checkBoxTrayTooltipInfoFolder = new CheckBox();
            tabPageHotkeys = new TabPage();
            HotkeyGrid = new DataGridView();
            ColumnFunction = new DataGridViewTextBoxColumn();
            ColumnKey = new DataGridViewTextBoxColumn();
            ColumnCtrl = new DataGridViewCheckBoxColumn();
            ColumnAlt = new DataGridViewCheckBoxColumn();
            ColumnShift = new DataGridViewCheckBoxColumn();
            ColumnWin = new DataGridViewCheckBoxColumn();
            buttonResetOptions = new Button();
            ((System.ComponentModel.ISupportInitialize)trimTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimBottom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTitleMaxLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSplitIndex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownJpegQuality).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericThumbWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericThumbHeight).BeginInit();
            tabControl1.SuspendLayout();
            tabPageOutput.SuspendLayout();
            tabPageModes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFramerate).BeginInit();
            tabPageApplication.SuspendLayout();
            tabPageHotkeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)HotkeyGrid).BeginInit();
            SuspendLayout();
            // 
            // checkBoxTrim
            // 
            checkBoxTrim.AutoSize = true;
            checkBoxTrim.Location = new Point(6, 328);
            checkBoxTrim.Name = "checkBoxTrim";
            checkBoxTrim.Size = new Size(142, 19);
            checkBoxTrim.TabIndex = 34;
            checkBoxTrim.Text = "Crop Window capture";
            checkBoxTrim.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseFolder
            // 
            buttonBrowseFolder.Location = new Point(378, 26);
            buttonBrowseFolder.Name = "buttonBrowseFolder";
            buttonBrowseFolder.Size = new Size(57, 23);
            buttonBrowseFolder.TabIndex = 33;
            buttonBrowseFolder.Text = "Browse";
            buttonBrowseFolder.UseVisualStyleBackColor = true;
            buttonBrowseFolder.Click += buttonBrowseFolder_Click;
            // 
            // buttonHelp
            // 
            buttonHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonHelp.Location = new Point(13, 494);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.Size = new Size(54, 23);
            buttonHelp.TabIndex = 32;
            buttonHelp.Text = "Help";
            buttonHelp.UseVisualStyleBackColor = true;
            buttonHelp.Click += buttonHelp_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 59);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 29;
            label3.Text = "Filename";
            // 
            // comboBoxFileExtension
            // 
            comboBoxFileExtension.FormattingEnabled = true;
            comboBoxFileExtension.Items.AddRange(new object[] { ".png", ".jpg", ".gif", ".bmp", ".tiff" });
            comboBoxFileExtension.Location = new Point(304, 77);
            comboBoxFileExtension.Name = "comboBoxFileExtension";
            comboBoxFileExtension.Size = new Size(53, 23);
            comboBoxFileExtension.TabIndex = 28;
            comboBoxFileExtension.Text = ".png";
            // 
            // textBoxFilename
            // 
            textBoxFilename.Location = new Point(6, 78);
            textBoxFilename.Name = "textBoxFilename";
            textBoxFilename.Size = new Size(291, 23);
            textBoxFilename.TabIndex = 27;
            textBoxFilename.Text = "$w - $c";
            // 
            // buttonSelectFolder
            // 
            buttonSelectFolder.Location = new Point(304, 26);
            buttonSelectFolder.Name = "buttonSelectFolder";
            buttonSelectFolder.Size = new Size(68, 23);
            buttonSelectFolder.TabIndex = 26;
            buttonSelectFolder.Text = "Select";
            buttonSelectFolder.UseVisualStyleBackColor = true;
            buttonSelectFolder.Click += buttonSelectFolder_Click;
            // 
            // trimTop
            // 
            trimTop.Location = new Point(64, 353);
            trimTop.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimTop.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimTop.Name = "trimTop";
            trimTop.Size = new Size(70, 23);
            trimTop.TabIndex = 25;
            // 
            // trimBottom
            // 
            trimBottom.Location = new Point(64, 407);
            trimBottom.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimBottom.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimBottom.Name = "trimBottom";
            trimBottom.Size = new Size(70, 23);
            trimBottom.TabIndex = 24;
            // 
            // trimRight
            // 
            trimRight.Location = new Point(128, 378);
            trimRight.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimRight.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimRight.Name = "trimRight";
            trimRight.Size = new Size(70, 23);
            trimRight.TabIndex = 23;
            // 
            // trimLeft
            // 
            trimLeft.Location = new Point(6, 378);
            trimLeft.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimLeft.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimLeft.Name = "trimLeft";
            trimLeft.Size = new Size(70, 23);
            trimLeft.TabIndex = 22;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 9);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 21;
            label1.Text = "Folder";
            // 
            // textBoxFolder
            // 
            textBoxFolder.Location = new Point(6, 27);
            textBoxFolder.Name = "textBoxFolder";
            textBoxFolder.Size = new Size(292, 23);
            textBoxFolder.TabIndex = 20;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.Location = new Point(410, 494);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 35;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonApply
            // 
            buttonApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonApply.Location = new Point(329, 494);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(75, 23);
            buttonApply.TabIndex = 36;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(248, 494);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 37;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // textBoxAlternateTitle
            // 
            textBoxAlternateTitle.Location = new Point(6, 130);
            textBoxAlternateTitle.Name = "textBoxAlternateTitle";
            textBoxAlternateTitle.Size = new Size(292, 23);
            textBoxAlternateTitle.TabIndex = 38;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 112);
            label2.Name = "label2";
            label2.Size = new Size(206, 15);
            label2.TabIndex = 39;
            label2.Text = "Alternate title if $w returns empty title";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 163);
            label4.Name = "label4";
            label4.Size = new Size(137, 15);
            label4.TabIndex = 40;
            label4.Text = "Window title max length";
            // 
            // numericUpDownTitleMaxLength
            // 
            numericUpDownTitleMaxLength.Location = new Point(6, 181);
            numericUpDownTitleMaxLength.Name = "numericUpDownTitleMaxLength";
            numericUpDownTitleMaxLength.Size = new Size(70, 23);
            numericUpDownTitleMaxLength.TabIndex = 41;
            numericUpDownTitleMaxLength.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 215);
            label5.Name = "label5";
            label5.Size = new Size(165, 15);
            label5.TabIndex = 42;
            label5.Text = "Split Window title using string";
            // 
            // numericUpDownSplitIndex
            // 
            numericUpDownSplitIndex.Location = new Point(6, 283);
            numericUpDownSplitIndex.Name = "numericUpDownSplitIndex";
            numericUpDownSplitIndex.Size = new Size(70, 23);
            numericUpDownSplitIndex.TabIndex = 45;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 265);
            label6.Name = "label6";
            label6.Size = new Size(126, 15);
            label6.TabIndex = 44;
            label6.Text = "Keep split text in index";
            // 
            // textBoxSplitString
            // 
            textBoxSplitString.Location = new Point(6, 233);
            textBoxSplitString.Name = "textBoxSplitString";
            textBoxSplitString.Size = new Size(70, 23);
            textBoxSplitString.TabIndex = 46;
            // 
            // numericUpDownJpegQuality
            // 
            numericUpDownJpegQuality.Location = new Point(363, 77);
            numericUpDownJpegQuality.Name = "numericUpDownJpegQuality";
            numericUpDownJpegQuality.Size = new Size(70, 23);
            numericUpDownJpegQuality.TabIndex = 48;
            numericUpDownJpegQuality.Value = new decimal(new int[] { 95, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(363, 59);
            label7.Name = "label7";
            label7.Size = new Size(72, 15);
            label7.TabIndex = 47;
            label7.Text = "Jpeg Quality";
            // 
            // checkBoxStartHidden
            // 
            checkBoxStartHidden.AutoSize = true;
            checkBoxStartHidden.Location = new Point(16, 25);
            checkBoxStartHidden.Name = "checkBoxStartHidden";
            checkBoxStartHidden.Size = new Size(92, 19);
            checkBoxStartHidden.TabIndex = 49;
            checkBoxStartHidden.Text = "Start Hidden";
            checkBoxStartHidden.UseVisualStyleBackColor = true;
            // 
            // checkBoxCropThumbnails
            // 
            checkBoxCropThumbnails.AutoSize = true;
            checkBoxCropThumbnails.Location = new Point(16, 155);
            checkBoxCropThumbnails.Name = "checkBoxCropThumbnails";
            checkBoxCropThumbnails.Size = new Size(234, 19);
            checkBoxCropThumbnails.TabIndex = 50;
            checkBoxCropThumbnails.Text = "Crop thumbnails (instead of stretching)";
            checkBoxCropThumbnails.UseVisualStyleBackColor = true;
            // 
            // numericThumbWidth
            // 
            numericThumbWidth.Location = new Point(122, 175);
            numericThumbWidth.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numericThumbWidth.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            numericThumbWidth.Name = "numericThumbWidth";
            numericThumbWidth.Size = new Size(55, 23);
            numericThumbWidth.TabIndex = 51;
            numericThumbWidth.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // numericThumbHeight
            // 
            numericThumbHeight.Location = new Point(122, 203);
            numericThumbHeight.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numericThumbHeight.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            numericThumbHeight.Name = "numericThumbHeight";
            numericThumbHeight.Size = new Size(54, 23);
            numericThumbHeight.TabIndex = 52;
            numericThumbHeight.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(16, 177);
            label8.Name = "label8";
            label8.Size = new Size(99, 15);
            label8.TabIndex = 53;
            label8.Text = "Thumbnail Width";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(16, 205);
            label9.Name = "label9";
            label9.Size = new Size(103, 15);
            label9.TabIndex = 54;
            label9.Text = "Thumbnail Height";
            // 
            // checkBoxTrayTooltipInfoCapture
            // 
            checkBoxTrayTooltipInfoCapture.AutoSize = true;
            checkBoxTrayTooltipInfoCapture.Location = new Point(16, 65);
            checkBoxTrayTooltipInfoCapture.Name = "checkBoxTrayTooltipInfoCapture";
            checkBoxTrayTooltipInfoCapture.Size = new Size(211, 19);
            checkBoxTrayTooltipInfoCapture.TabIndex = 55;
            checkBoxTrayTooltipInfoCapture.Text = "System tray tooltip when capturing";
            checkBoxTrayTooltipInfoCapture.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrayTooltipWarning
            // 
            checkBoxTrayTooltipWarning.AutoSize = true;
            checkBoxTrayTooltipWarning.Location = new Point(16, 115);
            checkBoxTrayTooltipWarning.Name = "checkBoxTrayTooltipWarning";
            checkBoxTrayTooltipWarning.Size = new Size(176, 19);
            checkBoxTrayTooltipWarning.TabIndex = 56;
            checkBoxTrayTooltipWarning.Text = "System tray tooltip warnings";
            checkBoxTrayTooltipWarning.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPageOutput);
            tabControl1.Controls.Add(tabPageModes);
            tabControl1.Controls.Add(tabPageApplication);
            tabControl1.Controls.Add(tabPageHotkeys);
            tabControl1.Location = new Point(3, 5);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(490, 483);
            tabControl1.TabIndex = 57;
            // 
            // tabPageOutput
            // 
            tabPageOutput.Controls.Add(textBoxFolder);
            tabPageOutput.Controls.Add(textBoxSplitString);
            tabPageOutput.Controls.Add(numericUpDownJpegQuality);
            tabPageOutput.Controls.Add(numericUpDownSplitIndex);
            tabPageOutput.Controls.Add(checkBoxTrim);
            tabPageOutput.Controls.Add(label1);
            tabPageOutput.Controls.Add(trimTop);
            tabPageOutput.Controls.Add(label6);
            tabPageOutput.Controls.Add(trimBottom);
            tabPageOutput.Controls.Add(label7);
            tabPageOutput.Controls.Add(trimRight);
            tabPageOutput.Controls.Add(label5);
            tabPageOutput.Controls.Add(trimLeft);
            tabPageOutput.Controls.Add(buttonSelectFolder);
            tabPageOutput.Controls.Add(numericUpDownTitleMaxLength);
            tabPageOutput.Controls.Add(label4);
            tabPageOutput.Controls.Add(textBoxFilename);
            tabPageOutput.Controls.Add(comboBoxFileExtension);
            tabPageOutput.Controls.Add(label3);
            tabPageOutput.Controls.Add(buttonBrowseFolder);
            tabPageOutput.Controls.Add(textBoxAlternateTitle);
            tabPageOutput.Controls.Add(label2);
            tabPageOutput.Location = new Point(4, 24);
            tabPageOutput.Name = "tabPageOutput";
            tabPageOutput.Padding = new Padding(3);
            tabPageOutput.Size = new Size(482, 455);
            tabPageOutput.TabIndex = 0;
            tabPageOutput.Text = "Capture Output";
            tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // tabPageModes
            // 
            tabPageModes.Controls.Add(checkBoxMaskRegion);
            tabPageModes.Controls.Add(label17);
            tabPageModes.Controls.Add(label13);
            tabPageModes.Controls.Add(label12);
            tabPageModes.Controls.Add(numericUpDownFramerate);
            tabPageModes.Controls.Add(checkBoxRegionComplete);
            tabPageModes.Controls.Add(checkBoxAllScreensToClipboard);
            tabPageModes.Controls.Add(checkBoxAllScreensToFile);
            tabPageModes.Controls.Add(label10);
            tabPageModes.Controls.Add(checkBoxScreenToClipboard);
            tabPageModes.Controls.Add(checkBoxScreenToFile);
            tabPageModes.Controls.Add(label11);
            tabPageModes.Controls.Add(checkBoxRegionToClipboard);
            tabPageModes.Controls.Add(checkBoxRegionToFile);
            tabPageModes.Controls.Add(labelRegion);
            tabPageModes.Controls.Add(checkBoxWindowToClipboard);
            tabPageModes.Controls.Add(checkBoxWindowToFile);
            tabPageModes.Controls.Add(labelWindow);
            tabPageModes.Location = new Point(4, 24);
            tabPageModes.Name = "tabPageModes";
            tabPageModes.Size = new Size(482, 455);
            tabPageModes.TabIndex = 3;
            tabPageModes.Text = "Modes";
            tabPageModes.UseVisualStyleBackColor = true;
            // 
            // checkBoxMaskRegion
            // 
            checkBoxMaskRegion.AutoSize = true;
            checkBoxMaskRegion.Location = new Point(36, 164);
            checkBoxMaskRegion.Name = "checkBoxMaskRegion";
            checkBoxMaskRegion.Size = new Size(219, 19);
            checkBoxMaskRegion.TabIndex = 69;
            checkBoxMaskRegion.Text = "Mask areas outside region with color";
            checkBoxMaskRegion.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(241, 104);
            label17.Name = "label17";
            label17.Size = new Size(23, 15);
            label17.TabIndex = 68;
            label17.Text = "fps";
            // 
            // label13
            // 
            label13.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            label13.Location = new Point(24, 128);
            label13.Name = "label13";
            label13.Size = new Size(435, 40);
            label13.TabIndex = 67;
            label13.Text = "If the framerate is higher than your sytem can handle, there will be additional lag to catch up.";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(24, 104);
            label12.Name = "label12";
            label12.Size = new Size(157, 15);
            label12.TabIndex = 66;
            label12.Text = "Region select max framerate";
            label12.Click += label12_Click;
            // 
            // numericUpDownFramerate
            // 
            numericUpDownFramerate.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownFramerate.Location = new Point(186, 102);
            numericUpDownFramerate.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            numericUpDownFramerate.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownFramerate.Name = "numericUpDownFramerate";
            numericUpDownFramerate.Size = new Size(54, 23);
            numericUpDownFramerate.TabIndex = 65;
            numericUpDownFramerate.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // checkBoxRegionComplete
            // 
            checkBoxRegionComplete.AutoSize = true;
            checkBoxRegionComplete.Location = new Point(24, 30);
            checkBoxRegionComplete.Name = "checkBoxRegionComplete";
            checkBoxRegionComplete.Size = new Size(242, 19);
            checkBoxRegionComplete.TabIndex = 12;
            checkBoxRegionComplete.Text = "Complete capture when releasing mouse";
            checkBoxRegionComplete.UseVisualStyleBackColor = true;
            checkBoxRegionComplete.CheckedChanged += checkBoxRegionComplete_CheckedChanged;
            // 
            // checkBoxAllScreensToClipboard
            // 
            checkBoxAllScreensToClipboard.AutoSize = true;
            checkBoxAllScreensToClipboard.Location = new Point(24, 368);
            checkBoxAllScreensToClipboard.Name = "checkBoxAllScreensToClipboard";
            checkBoxAllScreensToClipboard.Size = new Size(123, 19);
            checkBoxAllScreensToClipboard.TabIndex = 11;
            checkBoxAllScreensToClipboard.Text = "Copy to Clipboard";
            checkBoxAllScreensToClipboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllScreensToFile
            // 
            checkBoxAllScreensToFile.AutoSize = true;
            checkBoxAllScreensToFile.Location = new Point(24, 343);
            checkBoxAllScreensToFile.Name = "checkBoxAllScreensToFile";
            checkBoxAllScreensToFile.Size = new Size(85, 19);
            checkBoxAllScreensToFile.TabIndex = 10;
            checkBoxAllScreensToFile.Text = "Save to File";
            checkBoxAllScreensToFile.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(6, 325);
            label10.Name = "label10";
            label10.Size = new Size(106, 15);
            label10.TabIndex = 9;
            label10.Text = "Mode: All Screens";
            // 
            // checkBoxScreenToClipboard
            // 
            checkBoxScreenToClipboard.AutoSize = true;
            checkBoxScreenToClipboard.Location = new Point(24, 303);
            checkBoxScreenToClipboard.Name = "checkBoxScreenToClipboard";
            checkBoxScreenToClipboard.Size = new Size(123, 19);
            checkBoxScreenToClipboard.TabIndex = 8;
            checkBoxScreenToClipboard.Text = "Copy to Clipboard";
            checkBoxScreenToClipboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxScreenToFile
            // 
            checkBoxScreenToFile.AutoSize = true;
            checkBoxScreenToFile.Location = new Point(24, 278);
            checkBoxScreenToFile.Name = "checkBoxScreenToFile";
            checkBoxScreenToFile.Size = new Size(85, 19);
            checkBoxScreenToFile.TabIndex = 7;
            checkBoxScreenToFile.Text = "Save to File";
            checkBoxScreenToFile.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(6, 260);
            label11.Name = "label11";
            label11.Size = new Size(130, 15);
            label11.TabIndex = 6;
            label11.Text = "Mode: Current Screen";
            // 
            // checkBoxRegionToClipboard
            // 
            checkBoxRegionToClipboard.AutoSize = true;
            checkBoxRegionToClipboard.Location = new Point(36, 80);
            checkBoxRegionToClipboard.Name = "checkBoxRegionToClipboard";
            checkBoxRegionToClipboard.Size = new Size(123, 19);
            checkBoxRegionToClipboard.TabIndex = 5;
            checkBoxRegionToClipboard.Text = "Copy to Clipboard";
            checkBoxRegionToClipboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegionToFile
            // 
            checkBoxRegionToFile.AutoSize = true;
            checkBoxRegionToFile.Location = new Point(36, 55);
            checkBoxRegionToFile.Name = "checkBoxRegionToFile";
            checkBoxRegionToFile.Size = new Size(85, 19);
            checkBoxRegionToFile.TabIndex = 4;
            checkBoxRegionToFile.Text = "Save to File";
            checkBoxRegionToFile.UseVisualStyleBackColor = true;
            // 
            // labelRegion
            // 
            labelRegion.AutoSize = true;
            labelRegion.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelRegion.Location = new Point(6, 12);
            labelRegion.Name = "labelRegion";
            labelRegion.Size = new Size(84, 15);
            labelRegion.TabIndex = 3;
            labelRegion.Text = "Mode: Region";
            // 
            // checkBoxWindowToClipboard
            // 
            checkBoxWindowToClipboard.AutoSize = true;
            checkBoxWindowToClipboard.Location = new Point(24, 238);
            checkBoxWindowToClipboard.Name = "checkBoxWindowToClipboard";
            checkBoxWindowToClipboard.Size = new Size(123, 19);
            checkBoxWindowToClipboard.TabIndex = 2;
            checkBoxWindowToClipboard.Text = "Copy to Clipboard";
            checkBoxWindowToClipboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxWindowToFile
            // 
            checkBoxWindowToFile.AutoSize = true;
            checkBoxWindowToFile.Location = new Point(24, 213);
            checkBoxWindowToFile.Name = "checkBoxWindowToFile";
            checkBoxWindowToFile.Size = new Size(85, 19);
            checkBoxWindowToFile.TabIndex = 1;
            checkBoxWindowToFile.Text = "Save to File";
            checkBoxWindowToFile.UseVisualStyleBackColor = true;
            // 
            // labelWindow
            // 
            labelWindow.AutoSize = true;
            labelWindow.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelWindow.Location = new Point(6, 193);
            labelWindow.Name = "labelWindow";
            labelWindow.Size = new Size(91, 15);
            labelWindow.TabIndex = 0;
            labelWindow.Text = "Mode: Window";
            // 
            // tabPageApplication
            // 
            tabPageApplication.Controls.Add(label16);
            tabPageApplication.Controls.Add(label15);
            tabPageApplication.Controls.Add(label14);
            tabPageApplication.Controls.Add(checkBoxTrayTooltipInfoFolder);
            tabPageApplication.Controls.Add(checkBoxStartHidden);
            tabPageApplication.Controls.Add(label9);
            tabPageApplication.Controls.Add(checkBoxTrayTooltipWarning);
            tabPageApplication.Controls.Add(label8);
            tabPageApplication.Controls.Add(checkBoxTrayTooltipInfoCapture);
            tabPageApplication.Controls.Add(numericThumbHeight);
            tabPageApplication.Controls.Add(checkBoxCropThumbnails);
            tabPageApplication.Controls.Add(numericThumbWidth);
            tabPageApplication.Location = new Point(4, 24);
            tabPageApplication.Name = "tabPageApplication";
            tabPageApplication.Padding = new Padding(3);
            tabPageApplication.Size = new Size(482, 455);
            tabPageApplication.TabIndex = 1;
            tabPageApplication.Text = "Application";
            tabPageApplication.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label16.Location = new Point(5, 137);
            label16.Name = "label16";
            label16.Size = new Size(70, 15);
            label16.TabIndex = 63;
            label16.Text = "Thumbnails";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label15.Location = new Point(5, 47);
            label15.Name = "label15";
            label15.Size = new Size(73, 15);
            label15.TabIndex = 62;
            label15.Text = "System tray";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label14.Location = new Point(5, 7);
            label14.Name = "label14";
            label14.Size = new Size(49, 15);
            label14.TabIndex = 61;
            label14.Text = "Startup";
            // 
            // checkBoxTrayTooltipInfoFolder
            // 
            checkBoxTrayTooltipInfoFolder.AutoSize = true;
            checkBoxTrayTooltipInfoFolder.Location = new Point(16, 90);
            checkBoxTrayTooltipInfoFolder.Name = "checkBoxTrayTooltipInfoFolder";
            checkBoxTrayTooltipInfoFolder.Size = new Size(237, 19);
            checkBoxTrayTooltipInfoFolder.TabIndex = 57;
            checkBoxTrayTooltipInfoFolder.Text = "System tray tooltip when creating folder";
            checkBoxTrayTooltipInfoFolder.UseVisualStyleBackColor = true;
            // 
            // tabPageHotkeys
            // 
            tabPageHotkeys.Controls.Add(HotkeyGrid);
            tabPageHotkeys.Location = new Point(4, 24);
            tabPageHotkeys.Name = "tabPageHotkeys";
            tabPageHotkeys.Padding = new Padding(3);
            tabPageHotkeys.Size = new Size(482, 455);
            tabPageHotkeys.TabIndex = 2;
            tabPageHotkeys.Text = "Hotkeys";
            tabPageHotkeys.UseVisualStyleBackColor = true;
            // 
            // HotkeyGrid
            // 
            HotkeyGrid.AllowUserToAddRows = false;
            HotkeyGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            HotkeyGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            HotkeyGrid.Columns.AddRange(new DataGridViewColumn[] { ColumnFunction, ColumnKey, ColumnCtrl, ColumnAlt, ColumnShift, ColumnWin });
            HotkeyGrid.Location = new Point(6, 6);
            HotkeyGrid.Name = "HotkeyGrid";
            HotkeyGrid.RowHeadersVisible = false;
            HotkeyGrid.RowTemplate.Height = 25;
            HotkeyGrid.Size = new Size(470, 443);
            HotkeyGrid.TabIndex = 0;
            // 
            // ColumnFunction
            // 
            ColumnFunction.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColumnFunction.HeaderText = "Function";
            ColumnFunction.Name = "ColumnFunction";
            ColumnFunction.ReadOnly = true;
            // 
            // ColumnKey
            // 
            ColumnKey.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColumnKey.HeaderText = "Key";
            ColumnKey.Name = "ColumnKey";
            // 
            // ColumnCtrl
            // 
            ColumnCtrl.HeaderText = "Ctrl";
            ColumnCtrl.Name = "ColumnCtrl";
            ColumnCtrl.Width = 50;
            // 
            // ColumnAlt
            // 
            ColumnAlt.HeaderText = "Alt";
            ColumnAlt.Name = "ColumnAlt";
            ColumnAlt.Width = 50;
            // 
            // ColumnShift
            // 
            ColumnShift.HeaderText = "Shift";
            ColumnShift.Name = "ColumnShift";
            ColumnShift.Width = 50;
            // 
            // ColumnWin
            // 
            ColumnWin.HeaderText = "Win";
            ColumnWin.Name = "ColumnWin";
            ColumnWin.Width = 50;
            // 
            // buttonResetOptions
            // 
            buttonResetOptions.Location = new Point(73, 494);
            buttonResetOptions.Name = "buttonResetOptions";
            buttonResetOptions.Size = new Size(97, 23);
            buttonResetOptions.TabIndex = 58;
            buttonResetOptions.Text = "Reset options";
            buttonResetOptions.UseVisualStyleBackColor = true;
            buttonResetOptions.Click += buttonResetOptions_Click;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(495, 525);
            Controls.Add(buttonResetOptions);
            Controls.Add(tabControl1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonApply);
            Controls.Add(buttonOK);
            Controls.Add(buttonHelp);
            Name = "Options";
            Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)trimTop).EndInit();
            ((System.ComponentModel.ISupportInitialize)trimBottom).EndInit();
            ((System.ComponentModel.ISupportInitialize)trimRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)trimLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTitleMaxLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSplitIndex).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownJpegQuality).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericThumbWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericThumbHeight).EndInit();
            tabControl1.ResumeLayout(false);
            tabPageOutput.ResumeLayout(false);
            tabPageOutput.PerformLayout();
            tabPageModes.ResumeLayout(false);
            tabPageModes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFramerate).EndInit();
            tabPageApplication.ResumeLayout(false);
            tabPageApplication.PerformLayout();
            tabPageHotkeys.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)HotkeyGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CheckBox checkBoxTrim;
        private Button buttonBrowseFolder;
        private Button buttonHelp;
        private Label label3;
        private ComboBox comboBoxFileExtension;
        private TextBox textBoxFilename;
        private Button buttonSelectFolder;
        private NumericUpDown trimTop;
        private NumericUpDown trimBottom;
        private NumericUpDown trimRight;
        private NumericUpDown trimLeft;
        private Label label1;
        private TextBox textBoxFolder;
        private Button buttonOK;
        private Button buttonApply;
        private Button buttonCancel;
        private TextBox textBoxAlternateTitle;
        private Label label2;
        private Label label4;
        private NumericUpDown numericUpDownTitleMaxLength;
        private Label label5;
        private NumericUpDown numericUpDownSplitIndex;
        private Label label6;
        private TextBox textBoxSplitString;
        private NumericUpDown numericUpDownJpegQuality;
        private Label label7;
        private CheckBox checkBoxStartHidden;
        private CheckBox checkBoxCropThumbnails;
        private NumericUpDown numericThumbWidth;
        private NumericUpDown numericThumbHeight;
        private Label label8;
        private Label label9;
        private CheckBox checkBoxTrayTooltipInfoCapture;
        private CheckBox checkBoxTrayTooltipWarning;
        private TabControl tabControl1;
        private TabPage tabPageOutput;
        private TabPage tabPageApplication;
        private TabPage tabPageHotkeys;
        private DataGridView HotkeyGrid;
        private DataGridViewTextBoxColumn ColumnFunction;
        private DataGridViewTextBoxColumn ColumnKey;
        private DataGridViewCheckBoxColumn ColumnCtrl;
        private DataGridViewCheckBoxColumn ColumnAlt;
        private DataGridViewCheckBoxColumn ColumnShift;
        private DataGridViewCheckBoxColumn ColumnWin;
        private CheckBox checkBoxTrayTooltipInfoFolder;
        private TabPage tabPageModes;
        private CheckBox checkBoxWindowToClipboard;
        private CheckBox checkBoxWindowToFile;
        private Label labelWindow;
        private CheckBox checkBoxRegionComplete;
        private CheckBox checkBoxAllScreensToClipboard;
        private CheckBox checkBoxAllScreensToFile;
        private Label label10;
        private CheckBox checkBoxScreenToClipboard;
        private CheckBox checkBoxScreenToFile;
        private Label label11;
        private CheckBox checkBoxRegionToClipboard;
        private CheckBox checkBoxRegionToFile;
        private Label labelRegion;
        private Label label13;
        private Label label12;
        private NumericUpDown numericUpDownFramerate;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label17;
        private CheckBox checkBoxMaskRegion;
        private Button buttonResetOptions;
    }
}