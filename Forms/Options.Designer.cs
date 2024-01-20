﻿namespace ScreenShotTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
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
            labelFileNameResult = new Label();
            label23 = new Label();
            label22 = new Label();
            label21 = new Label();
            label20 = new Label();
            label19 = new Label();
            label18 = new Label();
            numericUpDownCounter = new NumericUpDown();
            buttonResetCounter = new Button();
            tabPageModes = new TabPage();
            checkBoxAllScreensToEditor = new CheckBox();
            checkBoxScreenToEditor = new CheckBox();
            checkBoxWindowToEditor = new CheckBox();
            checkBoxRegionToEditor = new CheckBox();
            checkBoxMaskRegion = new CheckBox();
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
            checkBoxSelectAfterPlacingSymbol = new CheckBox();
            checkBoxMinimizeOnClose = new CheckBox();
            label30 = new Label();
            label28 = new Label();
            label29 = new Label();
            numericBlurSampleArea = new NumericUpDown();
            label27 = new Label();
            label26 = new Label();
            numericBlurMosaicSize = new NumericUpDown();
            label25 = new Label();
            label24 = new Label();
            label13 = new Label();
            label17 = new Label();
            label12 = new Label();
            numericUpDownFramerate = new NumericUpDown();
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
            ((System.ComponentModel.ISupportInitialize)numericUpDownCounter).BeginInit();
            tabPageModes.SuspendLayout();
            tabPageApplication.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericBlurSampleArea).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericBlurMosaicSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFramerate).BeginInit();
            tabPageHotkeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)HotkeyGrid).BeginInit();
            SuspendLayout();
            // 
            // checkBoxTrim
            // 
            checkBoxTrim.AutoSize = true;
            checkBoxTrim.Location = new Point(6, 360);
            checkBoxTrim.Name = "checkBoxTrim";
            checkBoxTrim.Size = new Size(281, 19);
            checkBoxTrim.TabIndex = 20;
            checkBoxTrim.Text = "Crop Window capture (remove window borders)";
            checkBoxTrim.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseFolder
            // 
            buttonBrowseFolder.Location = new Point(378, 26);
            buttonBrowseFolder.Name = "buttonBrowseFolder";
            buttonBrowseFolder.Size = new Size(57, 23);
            buttonBrowseFolder.TabIndex = 12;
            buttonBrowseFolder.Text = "Browse";
            buttonBrowseFolder.UseVisualStyleBackColor = true;
            buttonBrowseFolder.Click += ButtonBrowseFolder_Click;
            // 
            // buttonHelp
            // 
            buttonHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonHelp.Location = new Point(13, 608);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.Size = new Size(54, 23);
            buttonHelp.TabIndex = 90;
            buttonHelp.Text = "Help";
            buttonHelp.UseVisualStyleBackColor = true;
            buttonHelp.Click += ButtonHelp_Click;
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
            comboBoxFileExtension.TabIndex = 14;
            comboBoxFileExtension.Text = ".png";
            // 
            // textBoxFilename
            // 
            textBoxFilename.Location = new Point(6, 78);
            textBoxFilename.Name = "textBoxFilename";
            textBoxFilename.Size = new Size(291, 23);
            textBoxFilename.TabIndex = 13;
            textBoxFilename.Text = "$w - $c";
            textBoxFilename.TextChanged += TextBoxFilename_TextChanged;
            // 
            // buttonSelectFolder
            // 
            buttonSelectFolder.Location = new Point(304, 26);
            buttonSelectFolder.Name = "buttonSelectFolder";
            buttonSelectFolder.Size = new Size(68, 23);
            buttonSelectFolder.TabIndex = 11;
            buttonSelectFolder.Text = "Select";
            buttonSelectFolder.UseVisualStyleBackColor = true;
            buttonSelectFolder.Click += ButtonSelectFolder_Click;
            // 
            // trimTop
            // 
            trimTop.Location = new Point(78, 400);
            trimTop.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimTop.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimTop.Name = "trimTop";
            trimTop.Size = new Size(70, 23);
            trimTop.TabIndex = 21;
            // 
            // trimBottom
            // 
            trimBottom.Location = new Point(78, 454);
            trimBottom.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimBottom.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimBottom.Name = "trimBottom";
            trimBottom.Size = new Size(70, 23);
            trimBottom.TabIndex = 24;
            // 
            // trimRight
            // 
            trimRight.Location = new Point(146, 425);
            trimRight.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimRight.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimRight.Name = "trimRight";
            trimRight.Size = new Size(70, 23);
            trimRight.TabIndex = 23;
            // 
            // trimLeft
            // 
            trimLeft.Location = new Point(8, 429);
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
            textBoxFolder.TabIndex = 10;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.Location = new Point(410, 608);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 94;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += ButtonOK_Click;
            // 
            // buttonApply
            // 
            buttonApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonApply.Location = new Point(329, 608);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(75, 23);
            buttonApply.TabIndex = 93;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += ButtonApply_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(248, 608);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 92;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // textBoxAlternateTitle
            // 
            textBoxAlternateTitle.Location = new Point(6, 162);
            textBoxAlternateTitle.Name = "textBoxAlternateTitle";
            textBoxAlternateTitle.Size = new Size(292, 23);
            textBoxAlternateTitle.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 144);
            label2.Name = "label2";
            label2.Size = new Size(206, 15);
            label2.TabIndex = 39;
            label2.Text = "Alternate title if $w returns empty title";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 195);
            label4.Name = "label4";
            label4.Size = new Size(137, 15);
            label4.TabIndex = 40;
            label4.Text = "Window title max length";
            // 
            // numericUpDownTitleMaxLength
            // 
            numericUpDownTitleMaxLength.Location = new Point(6, 213);
            numericUpDownTitleMaxLength.Name = "numericUpDownTitleMaxLength";
            numericUpDownTitleMaxLength.Size = new Size(70, 23);
            numericUpDownTitleMaxLength.TabIndex = 17;
            numericUpDownTitleMaxLength.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 247);
            label5.Name = "label5";
            label5.Size = new Size(165, 15);
            label5.TabIndex = 42;
            label5.Text = "Split Window title using string";
            // 
            // numericUpDownSplitIndex
            // 
            numericUpDownSplitIndex.Location = new Point(6, 315);
            numericUpDownSplitIndex.Name = "numericUpDownSplitIndex";
            numericUpDownSplitIndex.Size = new Size(70, 23);
            numericUpDownSplitIndex.TabIndex = 19;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 297);
            label6.Name = "label6";
            label6.Size = new Size(201, 15);
            label6.TabIndex = 44;
            label6.Text = "Keep split text from position number";
            // 
            // textBoxSplitString
            // 
            textBoxSplitString.Location = new Point(6, 265);
            textBoxSplitString.Name = "textBoxSplitString";
            textBoxSplitString.Size = new Size(70, 23);
            textBoxSplitString.TabIndex = 18;
            // 
            // numericUpDownJpegQuality
            // 
            numericUpDownJpegQuality.Location = new Point(363, 77);
            numericUpDownJpegQuality.Name = "numericUpDownJpegQuality";
            numericUpDownJpegQuality.Size = new Size(70, 23);
            numericUpDownJpegQuality.TabIndex = 15;
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
            checkBoxStartHidden.Size = new Size(232, 19);
            checkBoxStartHidden.TabIndex = 20;
            checkBoxStartHidden.Text = "Start Hidden and hide when minimized";
            checkBoxStartHidden.UseVisualStyleBackColor = true;
            // 
            // checkBoxCropThumbnails
            // 
            checkBoxCropThumbnails.AutoSize = true;
            checkBoxCropThumbnails.Location = new Point(17, 194);
            checkBoxCropThumbnails.Name = "checkBoxCropThumbnails";
            checkBoxCropThumbnails.Size = new Size(234, 19);
            checkBoxCropThumbnails.TabIndex = 24;
            checkBoxCropThumbnails.Text = "Crop thumbnails (instead of stretching)";
            checkBoxCropThumbnails.UseVisualStyleBackColor = true;
            // 
            // numericThumbWidth
            // 
            numericThumbWidth.Location = new Point(123, 214);
            numericThumbWidth.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numericThumbWidth.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            numericThumbWidth.Name = "numericThumbWidth";
            numericThumbWidth.Size = new Size(55, 23);
            numericThumbWidth.TabIndex = 25;
            numericThumbWidth.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // numericThumbHeight
            // 
            numericThumbHeight.Location = new Point(123, 242);
            numericThumbHeight.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numericThumbHeight.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            numericThumbHeight.Name = "numericThumbHeight";
            numericThumbHeight.Size = new Size(54, 23);
            numericThumbHeight.TabIndex = 26;
            numericThumbHeight.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(17, 216);
            label8.Name = "label8";
            label8.Size = new Size(99, 15);
            label8.TabIndex = 53;
            label8.Text = "Thumbnail Width";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(17, 244);
            label9.Name = "label9";
            label9.Size = new Size(103, 15);
            label9.TabIndex = 54;
            label9.Text = "Thumbnail Height";
            // 
            // checkBoxTrayTooltipInfoCapture
            // 
            checkBoxTrayTooltipInfoCapture.AutoSize = true;
            checkBoxTrayTooltipInfoCapture.Location = new Point(17, 93);
            checkBoxTrayTooltipInfoCapture.Name = "checkBoxTrayTooltipInfoCapture";
            checkBoxTrayTooltipInfoCapture.Size = new Size(211, 19);
            checkBoxTrayTooltipInfoCapture.TabIndex = 21;
            checkBoxTrayTooltipInfoCapture.Text = "System tray tooltip when capturing";
            checkBoxTrayTooltipInfoCapture.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrayTooltipWarning
            // 
            checkBoxTrayTooltipWarning.AutoSize = true;
            checkBoxTrayTooltipWarning.Location = new Point(17, 143);
            checkBoxTrayTooltipWarning.Name = "checkBoxTrayTooltipWarning";
            checkBoxTrayTooltipWarning.Size = new Size(176, 19);
            checkBoxTrayTooltipWarning.TabIndex = 23;
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
            tabControl1.Size = new Size(490, 597);
            tabControl1.TabIndex = 1;
            // 
            // tabPageOutput
            // 
            tabPageOutput.Controls.Add(labelFileNameResult);
            tabPageOutput.Controls.Add(label23);
            tabPageOutput.Controls.Add(label22);
            tabPageOutput.Controls.Add(label21);
            tabPageOutput.Controls.Add(label20);
            tabPageOutput.Controls.Add(label19);
            tabPageOutput.Controls.Add(label18);
            tabPageOutput.Controls.Add(numericUpDownCounter);
            tabPageOutput.Controls.Add(buttonResetCounter);
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
            tabPageOutput.Size = new Size(482, 569);
            tabPageOutput.TabIndex = 0;
            tabPageOutput.Text = "Capture Output";
            tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // labelFileNameResult
            // 
            labelFileNameResult.AutoSize = true;
            labelFileNameResult.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            labelFileNameResult.Location = new Point(18, 109);
            labelFileNameResult.Name = "labelFileNameResult";
            labelFileNameResult.Size = new Size(90, 15);
            labelFileNameResult.TabIndex = 57;
            labelFileNameResult.Text = "File name result";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label23.Location = new Point(88, 317);
            label23.Name = "label23";
            label23.Size = new Size(120, 15);
            label23.TabIndex = 56;
            label23.Text = "(0 is the first element)";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label22.Location = new Point(156, 407);
            label22.Name = "label22";
            label22.Size = new Size(35, 15);
            label22.TabIndex = 55;
            label22.Text = "Right";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label21.Location = new Point(18, 411);
            label21.Name = "label21";
            label21.Size = new Size(27, 15);
            label21.TabIndex = 54;
            label21.Text = "Left";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label20.Location = new Point(88, 436);
            label20.Name = "label20";
            label20.Size = new Size(44, 15);
            label20.TabIndex = 53;
            label20.Text = "Bottom";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label19.Location = new Point(88, 382);
            label19.Name = "label19";
            label19.Size = new Size(26, 15);
            label19.TabIndex = 52;
            label19.Text = "Top";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(6, 496);
            label18.Name = "label18";
            label18.Size = new Size(204, 15);
            label18.TabIndex = 51;
            label18.Text = "Counter number used by $c / ${num}";
            // 
            // numericUpDownCounter
            // 
            numericUpDownCounter.Location = new Point(6, 514);
            numericUpDownCounter.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownCounter.Name = "numericUpDownCounter";
            numericUpDownCounter.Size = new Size(70, 23);
            numericUpDownCounter.TabIndex = 25;
            // 
            // buttonResetCounter
            // 
            buttonResetCounter.Location = new Point(82, 514);
            buttonResetCounter.Name = "buttonResetCounter";
            buttonResetCounter.Size = new Size(142, 23);
            buttonResetCounter.TabIndex = 26;
            buttonResetCounter.Text = "Reset Counter number";
            buttonResetCounter.UseVisualStyleBackColor = true;
            buttonResetCounter.Click += ButtonResetCounter_Click;
            // 
            // tabPageModes
            // 
            tabPageModes.Controls.Add(checkBoxAllScreensToEditor);
            tabPageModes.Controls.Add(checkBoxScreenToEditor);
            tabPageModes.Controls.Add(checkBoxWindowToEditor);
            tabPageModes.Controls.Add(checkBoxRegionToEditor);
            tabPageModes.Controls.Add(checkBoxMaskRegion);
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
            tabPageModes.Size = new Size(482, 569);
            tabPageModes.TabIndex = 3;
            tabPageModes.Text = "Modes";
            tabPageModes.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllScreensToEditor
            // 
            checkBoxAllScreensToEditor.AutoSize = true;
            checkBoxAllScreensToEditor.Location = new Point(24, 431);
            checkBoxAllScreensToEditor.Name = "checkBoxAllScreensToEditor";
            checkBoxAllScreensToEditor.Size = new Size(102, 19);
            checkBoxAllScreensToEditor.TabIndex = 72;
            checkBoxAllScreensToEditor.Text = "Open in Editor";
            checkBoxAllScreensToEditor.UseVisualStyleBackColor = true;
            // 
            // checkBoxScreenToEditor
            // 
            checkBoxScreenToEditor.AutoSize = true;
            checkBoxScreenToEditor.Location = new Point(24, 331);
            checkBoxScreenToEditor.Name = "checkBoxScreenToEditor";
            checkBoxScreenToEditor.Size = new Size(102, 19);
            checkBoxScreenToEditor.TabIndex = 71;
            checkBoxScreenToEditor.Text = "Open in Editor";
            checkBoxScreenToEditor.UseVisualStyleBackColor = true;
            // 
            // checkBoxWindowToEditor
            // 
            checkBoxWindowToEditor.AutoSize = true;
            checkBoxWindowToEditor.Location = new Point(24, 231);
            checkBoxWindowToEditor.Name = "checkBoxWindowToEditor";
            checkBoxWindowToEditor.Size = new Size(102, 19);
            checkBoxWindowToEditor.TabIndex = 70;
            checkBoxWindowToEditor.Text = "Open in Editor";
            checkBoxWindowToEditor.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegionToEditor
            // 
            checkBoxRegionToEditor.AutoSize = true;
            checkBoxRegionToEditor.Location = new Point(36, 105);
            checkBoxRegionToEditor.Name = "checkBoxRegionToEditor";
            checkBoxRegionToEditor.Size = new Size(102, 19);
            checkBoxRegionToEditor.TabIndex = 69;
            checkBoxRegionToEditor.Text = "Open in Editor";
            checkBoxRegionToEditor.UseVisualStyleBackColor = true;
            // 
            // checkBoxMaskRegion
            // 
            checkBoxMaskRegion.AutoSize = true;
            checkBoxMaskRegion.Location = new Point(24, 130);
            checkBoxMaskRegion.Name = "checkBoxMaskRegion";
            checkBoxMaskRegion.Size = new Size(219, 19);
            checkBoxMaskRegion.TabIndex = 24;
            checkBoxMaskRegion.Text = "Mask areas outside region with color";
            checkBoxMaskRegion.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegionComplete
            // 
            checkBoxRegionComplete.AutoSize = true;
            checkBoxRegionComplete.Location = new Point(24, 30);
            checkBoxRegionComplete.Name = "checkBoxRegionComplete";
            checkBoxRegionComplete.Size = new Size(242, 19);
            checkBoxRegionComplete.TabIndex = 20;
            checkBoxRegionComplete.Text = "Complete capture when releasing mouse";
            checkBoxRegionComplete.UseVisualStyleBackColor = true;
            checkBoxRegionComplete.CheckedChanged += CheckBoxRegionComplete_CheckedChanged;
            // 
            // checkBoxAllScreensToClipboard
            // 
            checkBoxAllScreensToClipboard.AutoSize = true;
            checkBoxAllScreensToClipboard.Location = new Point(24, 406);
            checkBoxAllScreensToClipboard.Name = "checkBoxAllScreensToClipboard";
            checkBoxAllScreensToClipboard.Size = new Size(123, 19);
            checkBoxAllScreensToClipboard.TabIndex = 30;
            checkBoxAllScreensToClipboard.Text = "Copy to Clipboard";
            checkBoxAllScreensToClipboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllScreensToFile
            // 
            checkBoxAllScreensToFile.AutoSize = true;
            checkBoxAllScreensToFile.Location = new Point(24, 381);
            checkBoxAllScreensToFile.Name = "checkBoxAllScreensToFile";
            checkBoxAllScreensToFile.Size = new Size(85, 19);
            checkBoxAllScreensToFile.TabIndex = 29;
            checkBoxAllScreensToFile.Text = "Save to File";
            checkBoxAllScreensToFile.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label10.Location = new Point(6, 363);
            label10.Name = "label10";
            label10.Size = new Size(106, 15);
            label10.TabIndex = 9;
            label10.Text = "Mode: All Screens";
            // 
            // checkBoxScreenToClipboard
            // 
            checkBoxScreenToClipboard.AutoSize = true;
            checkBoxScreenToClipboard.Location = new Point(24, 306);
            checkBoxScreenToClipboard.Name = "checkBoxScreenToClipboard";
            checkBoxScreenToClipboard.Size = new Size(123, 19);
            checkBoxScreenToClipboard.TabIndex = 28;
            checkBoxScreenToClipboard.Text = "Copy to Clipboard";
            checkBoxScreenToClipboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxScreenToFile
            // 
            checkBoxScreenToFile.AutoSize = true;
            checkBoxScreenToFile.Location = new Point(24, 281);
            checkBoxScreenToFile.Name = "checkBoxScreenToFile";
            checkBoxScreenToFile.Size = new Size(85, 19);
            checkBoxScreenToFile.TabIndex = 27;
            checkBoxScreenToFile.Text = "Save to File";
            checkBoxScreenToFile.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label11.Location = new Point(6, 263);
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
            checkBoxRegionToClipboard.TabIndex = 22;
            checkBoxRegionToClipboard.Text = "Copy to Clipboard";
            checkBoxRegionToClipboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegionToFile
            // 
            checkBoxRegionToFile.AutoSize = true;
            checkBoxRegionToFile.Location = new Point(36, 55);
            checkBoxRegionToFile.Name = "checkBoxRegionToFile";
            checkBoxRegionToFile.Size = new Size(85, 19);
            checkBoxRegionToFile.TabIndex = 21;
            checkBoxRegionToFile.Text = "Save to File";
            checkBoxRegionToFile.UseVisualStyleBackColor = true;
            // 
            // labelRegion
            // 
            labelRegion.AutoSize = true;
            labelRegion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelRegion.Location = new Point(6, 12);
            labelRegion.Name = "labelRegion";
            labelRegion.Size = new Size(84, 15);
            labelRegion.TabIndex = 3;
            labelRegion.Text = "Mode: Region";
            // 
            // checkBoxWindowToClipboard
            // 
            checkBoxWindowToClipboard.AutoSize = true;
            checkBoxWindowToClipboard.Location = new Point(24, 206);
            checkBoxWindowToClipboard.Name = "checkBoxWindowToClipboard";
            checkBoxWindowToClipboard.Size = new Size(123, 19);
            checkBoxWindowToClipboard.TabIndex = 26;
            checkBoxWindowToClipboard.Text = "Copy to Clipboard";
            checkBoxWindowToClipboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxWindowToFile
            // 
            checkBoxWindowToFile.AutoSize = true;
            checkBoxWindowToFile.Location = new Point(24, 181);
            checkBoxWindowToFile.Name = "checkBoxWindowToFile";
            checkBoxWindowToFile.Size = new Size(85, 19);
            checkBoxWindowToFile.TabIndex = 25;
            checkBoxWindowToFile.Text = "Save to File";
            checkBoxWindowToFile.UseVisualStyleBackColor = true;
            // 
            // labelWindow
            // 
            labelWindow.AutoSize = true;
            labelWindow.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelWindow.Location = new Point(6, 161);
            labelWindow.Name = "labelWindow";
            labelWindow.Size = new Size(91, 15);
            labelWindow.TabIndex = 0;
            labelWindow.Text = "Mode: Window";
            // 
            // tabPageApplication
            // 
            tabPageApplication.Controls.Add(checkBoxSelectAfterPlacingSymbol);
            tabPageApplication.Controls.Add(checkBoxMinimizeOnClose);
            tabPageApplication.Controls.Add(label30);
            tabPageApplication.Controls.Add(label28);
            tabPageApplication.Controls.Add(label29);
            tabPageApplication.Controls.Add(numericBlurSampleArea);
            tabPageApplication.Controls.Add(label27);
            tabPageApplication.Controls.Add(label26);
            tabPageApplication.Controls.Add(numericBlurMosaicSize);
            tabPageApplication.Controls.Add(label25);
            tabPageApplication.Controls.Add(label24);
            tabPageApplication.Controls.Add(label13);
            tabPageApplication.Controls.Add(label17);
            tabPageApplication.Controls.Add(label12);
            tabPageApplication.Controls.Add(numericUpDownFramerate);
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
            tabPageApplication.Size = new Size(482, 569);
            tabPageApplication.TabIndex = 1;
            tabPageApplication.Text = "Application";
            tabPageApplication.UseVisualStyleBackColor = true;
            // 
            // checkBoxSelectAfterPlacingSymbol
            // 
            checkBoxSelectAfterPlacingSymbol.AutoSize = true;
            checkBoxSelectAfterPlacingSymbol.Location = new Point(17, 501);
            checkBoxSelectAfterPlacingSymbol.Name = "checkBoxSelectAfterPlacingSymbol";
            checkBoxSelectAfterPlacingSymbol.Size = new Size(374, 19);
            checkBoxSelectAfterPlacingSymbol.TabIndex = 83;
            checkBoxSelectAfterPlacingSymbol.Text = "After placing a symbol, change to Select (instead of placing more)";
            checkBoxSelectAfterPlacingSymbol.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinimizeOnClose
            // 
            checkBoxMinimizeOnClose.AutoSize = true;
            checkBoxMinimizeOnClose.Location = new Point(16, 50);
            checkBoxMinimizeOnClose.Name = "checkBoxMinimizeOnClose";
            checkBoxMinimizeOnClose.Size = new Size(184, 19);
            checkBoxMinimizeOnClose.TabIndex = 82;
            checkBoxMinimizeOnClose.Text = "Minimize when closing with X";
            checkBoxMinimizeOnClose.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            label30.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label30.Location = new Point(17, 458);
            label30.Name = "label30";
            label30.Size = new Size(435, 40);
            label30.TabIndex = 81;
            label30.Text = "A blur image is generated when loading an image in the editor. If the settings are too aggressive, it will take longer to open the editor or image";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(182, 432);
            label28.Name = "label28";
            label28.Size = new Size(244, 15);
            label28.TabIndex = 80;
            label28.Text = "pixels (lower is faster, but less accurate color)";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(17, 432);
            label29.Name = "label29";
            label29.Size = new Size(94, 15);
            label29.TabIndex = 79;
            label29.Text = "Blur sample area";
            // 
            // numericBlurSampleArea
            // 
            numericBlurSampleArea.Location = new Point(123, 430);
            numericBlurSampleArea.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericBlurSampleArea.Name = "numericBlurSampleArea";
            numericBlurSampleArea.Size = new Size(54, 23);
            numericBlurSampleArea.TabIndex = 78;
            numericBlurSampleArea.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(182, 405);
            label27.Name = "label27";
            label27.Size = new Size(232, 15);
            label27.TabIndex = 77;
            label27.Text = "pixels (higher is faster, adjustable in editor)";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(17, 405);
            label26.Name = "label26";
            label26.Size = new Size(91, 15);
            label26.TabIndex = 76;
            label26.Text = "Blur mosaic size";
            // 
            // numericBlurMosaicSize
            // 
            numericBlurMosaicSize.Location = new Point(123, 403);
            numericBlurMosaicSize.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numericBlurMosaicSize.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            numericBlurMosaicSize.Name = "numericBlurMosaicSize";
            numericBlurMosaicSize.Size = new Size(54, 23);
            numericBlurMosaicSize.TabIndex = 75;
            numericBlurMosaicSize.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label25.Location = new Point(6, 377);
            label25.Name = "label25";
            label25.Size = new Size(87, 15);
            label25.TabIndex = 74;
            label25.Text = "Editor settings";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label24.Location = new Point(6, 280);
            label24.Name = "label24";
            label24.Size = new Size(80, 15);
            label24.TabIndex = 73;
            label24.Text = "Performance";
            // 
            // label13
            // 
            label13.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label13.Location = new Point(17, 328);
            label13.Name = "label13";
            label13.Size = new Size(435, 40);
            label13.TabIndex = 72;
            label13.Text = "Used in Region capture and Editor. If the framerate is higher than your sytem can handle, there will be additional lag to catch up.";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(182, 286);
            label17.Name = "label17";
            label17.Size = new Size(23, 15);
            label17.TabIndex = 71;
            label17.Text = "fps";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(17, 304);
            label12.Name = "label12";
            label12.Size = new Size(84, 15);
            label12.TabIndex = 70;
            label12.Text = "Max framerate";
            // 
            // numericUpDownFramerate
            // 
            numericUpDownFramerate.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownFramerate.Location = new Point(123, 302);
            numericUpDownFramerate.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            numericUpDownFramerate.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownFramerate.Name = "numericUpDownFramerate";
            numericUpDownFramerate.Size = new Size(54, 23);
            numericUpDownFramerate.TabIndex = 69;
            numericUpDownFramerate.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label16.Location = new Point(6, 176);
            label16.Name = "label16";
            label16.Size = new Size(70, 15);
            label16.TabIndex = 63;
            label16.Text = "Thumbnails";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label15.Location = new Point(6, 75);
            label15.Name = "label15";
            label15.Size = new Size(73, 15);
            label15.TabIndex = 62;
            label15.Text = "System tray";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label14.Location = new Point(5, 7);
            label14.Name = "label14";
            label14.Size = new Size(130, 15);
            label14.TabIndex = 61;
            label14.Text = "Startup and shutdown";
            // 
            // checkBoxTrayTooltipInfoFolder
            // 
            checkBoxTrayTooltipInfoFolder.AutoSize = true;
            checkBoxTrayTooltipInfoFolder.Location = new Point(17, 118);
            checkBoxTrayTooltipInfoFolder.Name = "checkBoxTrayTooltipInfoFolder";
            checkBoxTrayTooltipInfoFolder.Size = new Size(237, 19);
            checkBoxTrayTooltipInfoFolder.TabIndex = 22;
            checkBoxTrayTooltipInfoFolder.Text = "System tray tooltip when creating folder";
            checkBoxTrayTooltipInfoFolder.UseVisualStyleBackColor = true;
            // 
            // tabPageHotkeys
            // 
            tabPageHotkeys.Controls.Add(HotkeyGrid);
            tabPageHotkeys.Location = new Point(4, 24);
            tabPageHotkeys.Name = "tabPageHotkeys";
            tabPageHotkeys.Padding = new Padding(3);
            tabPageHotkeys.Size = new Size(482, 569);
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
            HotkeyGrid.Size = new Size(470, 443);
            HotkeyGrid.TabIndex = 20;
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
            buttonResetOptions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonResetOptions.Location = new Point(73, 608);
            buttonResetOptions.Name = "buttonResetOptions";
            buttonResetOptions.Size = new Size(97, 23);
            buttonResetOptions.TabIndex = 91;
            buttonResetOptions.Text = "Reset options";
            buttonResetOptions.UseVisualStyleBackColor = true;
            buttonResetOptions.Click += ButtonResetOptions_Click;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(495, 639);
            Controls.Add(buttonResetOptions);
            Controls.Add(tabControl1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonApply);
            Controls.Add(buttonOK);
            Controls.Add(buttonHelp);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Options";
            Text = "Options";
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
            ((System.ComponentModel.ISupportInitialize)numericUpDownCounter).EndInit();
            tabPageModes.ResumeLayout(false);
            tabPageModes.PerformLayout();
            tabPageApplication.ResumeLayout(false);
            tabPageApplication.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericBlurSampleArea).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericBlurMosaicSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFramerate).EndInit();
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
        private Label label16;
        private Label label15;
        private Label label14;
        private CheckBox checkBoxMaskRegion;
        private Button buttonResetOptions;
        private Button buttonResetCounter;
        private Label label18;
        private NumericUpDown numericUpDownCounter;
        private Label label22;
        private Label label21;
        private Label label20;
        private Label label19;
        private Label label23;
        private Label labelFileNameResult;
        private CheckBox checkBoxAllScreensToEditor;
        private CheckBox checkBoxScreenToEditor;
        private CheckBox checkBoxWindowToEditor;
        private CheckBox checkBoxRegionToEditor;
        private Label label24;
        private Label label13;
        private Label label17;
        private Label label12;
        private NumericUpDown numericUpDownFramerate;
        private Label label26;
        private NumericUpDown numericBlurMosaicSize;
        private Label label25;
        private Label label30;
        private Label label28;
        private Label label29;
        private NumericUpDown numericBlurSampleArea;
        private Label label27;
        private CheckBox checkBoxMinimizeOnClose;
        private CheckBox checkBoxSelectAfterPlacingSymbol;
    }
}