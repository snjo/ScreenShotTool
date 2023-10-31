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
            checkBoxTrayTooltipInfo = new CheckBox();
            checkBoxTrayTooltipWarning = new CheckBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            HotkeyGrid = new DataGridView();
            ColumnFunction = new DataGridViewTextBoxColumn();
            ColumnKey = new DataGridViewTextBoxColumn();
            ColumnCtrl = new DataGridViewCheckBoxColumn();
            ColumnAlt = new DataGridViewCheckBoxColumn();
            ColumnShift = new DataGridViewCheckBoxColumn();
            ColumnWin = new DataGridViewCheckBoxColumn();
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
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
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
            buttonHelp.Location = new Point(13, 497);
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
            buttonOK.Location = new Point(410, 497);
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
            buttonApply.Location = new Point(329, 497);
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
            buttonCancel.Location = new Point(248, 497);
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
            checkBoxStartHidden.Location = new Point(6, 14);
            checkBoxStartHidden.Name = "checkBoxStartHidden";
            checkBoxStartHidden.Size = new Size(92, 19);
            checkBoxStartHidden.TabIndex = 49;
            checkBoxStartHidden.Text = "Start Hidden";
            checkBoxStartHidden.UseVisualStyleBackColor = true;
            // 
            // checkBoxCropThumbnails
            // 
            checkBoxCropThumbnails.AutoSize = true;
            checkBoxCropThumbnails.Location = new Point(6, 98);
            checkBoxCropThumbnails.Name = "checkBoxCropThumbnails";
            checkBoxCropThumbnails.Size = new Size(115, 19);
            checkBoxCropThumbnails.TabIndex = 50;
            checkBoxCropThumbnails.Text = "Crop thumbnails";
            checkBoxCropThumbnails.UseVisualStyleBackColor = true;
            // 
            // numericThumbWidth
            // 
            numericThumbWidth.Location = new Point(113, 120);
            numericThumbWidth.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numericThumbWidth.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            numericThumbWidth.Name = "numericThumbWidth";
            numericThumbWidth.Size = new Size(55, 23);
            numericThumbWidth.TabIndex = 51;
            numericThumbWidth.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // numericThumbHeight
            // 
            numericThumbHeight.Location = new Point(113, 148);
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
            label8.Location = new Point(7, 122);
            label8.Name = "label8";
            label8.Size = new Size(99, 15);
            label8.TabIndex = 53;
            label8.Text = "Thumbnail Width";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(7, 150);
            label9.Name = "label9";
            label9.Size = new Size(103, 15);
            label9.TabIndex = 54;
            label9.Text = "Thumbnail Height";
            // 
            // checkBoxTrayTooltipInfo
            // 
            checkBoxTrayTooltipInfo.AutoSize = true;
            checkBoxTrayTooltipInfo.Location = new Point(5, 39);
            checkBoxTrayTooltipInfo.Name = "checkBoxTrayTooltipInfo";
            checkBoxTrayTooltipInfo.Size = new Size(211, 19);
            checkBoxTrayTooltipInfo.TabIndex = 55;
            checkBoxTrayTooltipInfo.Text = "System tray tooltip when capturing";
            checkBoxTrayTooltipInfo.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrayTooltipWarning
            // 
            checkBoxTrayTooltipWarning.AutoSize = true;
            checkBoxTrayTooltipWarning.Location = new Point(5, 64);
            checkBoxTrayTooltipWarning.Name = "checkBoxTrayTooltipWarning";
            checkBoxTrayTooltipWarning.Size = new Size(176, 19);
            checkBoxTrayTooltipWarning.TabIndex = 56;
            checkBoxTrayTooltipWarning.Text = "System tray tooltip warnings";
            checkBoxTrayTooltipWarning.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(3, 5);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(490, 486);
            tabControl1.TabIndex = 57;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(textBoxFolder);
            tabPage1.Controls.Add(textBoxSplitString);
            tabPage1.Controls.Add(numericUpDownJpegQuality);
            tabPage1.Controls.Add(numericUpDownSplitIndex);
            tabPage1.Controls.Add(checkBoxTrim);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(trimTop);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(trimBottom);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(trimRight);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(trimLeft);
            tabPage1.Controls.Add(buttonSelectFolder);
            tabPage1.Controls.Add(numericUpDownTitleMaxLength);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(textBoxFilename);
            tabPage1.Controls.Add(comboBoxFileExtension);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(buttonBrowseFolder);
            tabPage1.Controls.Add(textBoxAlternateTitle);
            tabPage1.Controls.Add(label2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(482, 458);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Capture";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(checkBoxStartHidden);
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(checkBoxTrayTooltipWarning);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(checkBoxTrayTooltipInfo);
            tabPage2.Controls.Add(numericThumbHeight);
            tabPage2.Controls.Add(checkBoxCropThumbnails);
            tabPage2.Controls.Add(numericThumbWidth);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(482, 458);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Application";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(HotkeyGrid);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(482, 458);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Hotkeys";
            tabPage3.UseVisualStyleBackColor = true;
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
            HotkeyGrid.Size = new Size(470, 446);
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
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(495, 528);
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
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
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
        private CheckBox checkBoxTrayTooltipInfo;
        private CheckBox checkBoxTrayTooltipWarning;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private DataGridView HotkeyGrid;
        private DataGridViewTextBoxColumn ColumnFunction;
        private DataGridViewTextBoxColumn ColumnKey;
        private DataGridViewCheckBoxColumn ColumnCtrl;
        private DataGridViewCheckBoxColumn ColumnAlt;
        private DataGridViewCheckBoxColumn ColumnShift;
        private DataGridViewCheckBoxColumn ColumnWin;
    }
}