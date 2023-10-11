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
            ((System.ComponentModel.ISupportInitialize)trimTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimBottom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTitleMaxLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSplitIndex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownJpegQuality).BeginInit();
            SuspendLayout();
            // 
            // checkBoxTrim
            // 
            checkBoxTrim.AutoSize = true;
            checkBoxTrim.Location = new Point(221, 173);
            checkBoxTrim.Name = "checkBoxTrim";
            checkBoxTrim.Size = new Size(142, 19);
            checkBoxTrim.TabIndex = 34;
            checkBoxTrim.Text = "Crop Window capture";
            checkBoxTrim.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseFolder
            // 
            buttonBrowseFolder.Location = new Point(384, 30);
            buttonBrowseFolder.Name = "buttonBrowseFolder";
            buttonBrowseFolder.Size = new Size(57, 23);
            buttonBrowseFolder.TabIndex = 33;
            buttonBrowseFolder.Text = "Browse";
            buttonBrowseFolder.UseVisualStyleBackColor = true;
            // 
            // buttonHelp
            // 
            buttonHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonHelp.Location = new Point(13, 379);
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
            label3.Location = new Point(12, 63);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 29;
            label3.Text = "Filename";
            // 
            // comboBoxFileExtension
            // 
            comboBoxFileExtension.FormattingEnabled = true;
            comboBoxFileExtension.Items.AddRange(new object[] { ".png", ".jpg", ".gif", ".bmp", ".tiff" });
            comboBoxFileExtension.Location = new Point(310, 81);
            comboBoxFileExtension.Name = "comboBoxFileExtension";
            comboBoxFileExtension.Size = new Size(53, 23);
            comboBoxFileExtension.TabIndex = 28;
            comboBoxFileExtension.Text = ".png";
            // 
            // textBoxFilename
            // 
            textBoxFilename.Location = new Point(12, 82);
            textBoxFilename.Name = "textBoxFilename";
            textBoxFilename.Size = new Size(291, 23);
            textBoxFilename.TabIndex = 27;
            textBoxFilename.Text = "$w - $c";
            // 
            // buttonSelectFolder
            // 
            buttonSelectFolder.Location = new Point(310, 30);
            buttonSelectFolder.Name = "buttonSelectFolder";
            buttonSelectFolder.Size = new Size(68, 23);
            buttonSelectFolder.TabIndex = 26;
            buttonSelectFolder.Text = "Select";
            buttonSelectFolder.UseVisualStyleBackColor = true;
            // 
            // trimTop
            // 
            trimTop.Location = new Point(279, 198);
            trimTop.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimTop.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimTop.Name = "trimTop";
            trimTop.Size = new Size(70, 23);
            trimTop.TabIndex = 25;
            // 
            // trimBottom
            // 
            trimBottom.Location = new Point(279, 252);
            trimBottom.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimBottom.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimBottom.Name = "trimBottom";
            trimBottom.Size = new Size(70, 23);
            trimBottom.TabIndex = 24;
            // 
            // trimRight
            // 
            trimRight.Location = new Point(343, 223);
            trimRight.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimRight.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimRight.Name = "trimRight";
            trimRight.Size = new Size(70, 23);
            trimRight.TabIndex = 23;
            // 
            // trimLeft
            // 
            trimLeft.Location = new Point(221, 223);
            trimLeft.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimLeft.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimLeft.Name = "trimLeft";
            trimLeft.Size = new Size(70, 23);
            trimLeft.TabIndex = 22;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 13);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 21;
            label1.Text = "Folder";
            // 
            // textBoxFolder
            // 
            textBoxFolder.Location = new Point(12, 31);
            textBoxFolder.Name = "textBoxFolder";
            textBoxFolder.Size = new Size(292, 23);
            textBoxFolder.TabIndex = 20;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.Location = new Point(370, 379);
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
            buttonApply.Location = new Point(289, 379);
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
            buttonCancel.Location = new Point(208, 379);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 37;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // textBoxAlternateTitle
            // 
            textBoxAlternateTitle.Location = new Point(12, 134);
            textBoxAlternateTitle.Name = "textBoxAlternateTitle";
            textBoxAlternateTitle.Size = new Size(292, 23);
            textBoxAlternateTitle.TabIndex = 38;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 116);
            label2.Name = "label2";
            label2.Size = new Size(206, 15);
            label2.TabIndex = 39;
            label2.Text = "Alternate title if $w returns empty title";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 173);
            label4.Name = "label4";
            label4.Size = new Size(137, 15);
            label4.TabIndex = 40;
            label4.Text = "Window title max length";
            // 
            // numericUpDownTitleMaxLength
            // 
            numericUpDownTitleMaxLength.Location = new Point(12, 191);
            numericUpDownTitleMaxLength.Name = "numericUpDownTitleMaxLength";
            numericUpDownTitleMaxLength.Size = new Size(70, 23);
            numericUpDownTitleMaxLength.TabIndex = 41;
            numericUpDownTitleMaxLength.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 225);
            label5.Name = "label5";
            label5.Size = new Size(165, 15);
            label5.TabIndex = 42;
            label5.Text = "Split Window title using string";
            // 
            // numericUpDownSplitIndex
            // 
            numericUpDownSplitIndex.Location = new Point(12, 287);
            numericUpDownSplitIndex.Name = "numericUpDownSplitIndex";
            numericUpDownSplitIndex.Size = new Size(70, 23);
            numericUpDownSplitIndex.TabIndex = 45;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 269);
            label6.Name = "label6";
            label6.Size = new Size(126, 15);
            label6.TabIndex = 44;
            label6.Text = "Keep split text in index";
            // 
            // textBoxSplitString
            // 
            textBoxSplitString.Location = new Point(12, 243);
            textBoxSplitString.Name = "textBoxSplitString";
            textBoxSplitString.Size = new Size(70, 23);
            textBoxSplitString.TabIndex = 46;
            // 
            // numericUpDownJpegQuality
            // 
            numericUpDownJpegQuality.Location = new Point(369, 81);
            numericUpDownJpegQuality.Name = "numericUpDownJpegQuality";
            numericUpDownJpegQuality.Size = new Size(70, 23);
            numericUpDownJpegQuality.TabIndex = 48;
            numericUpDownJpegQuality.Value = new decimal(new int[] { 95, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(369, 63);
            label7.Name = "label7";
            label7.Size = new Size(72, 15);
            label7.TabIndex = 47;
            label7.Text = "Jpeg Quality";
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 410);
            Controls.Add(numericUpDownJpegQuality);
            Controls.Add(label7);
            Controls.Add(textBoxSplitString);
            Controls.Add(numericUpDownSplitIndex);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(numericUpDownTitleMaxLength);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(textBoxAlternateTitle);
            Controls.Add(buttonCancel);
            Controls.Add(buttonApply);
            Controls.Add(buttonOK);
            Controls.Add(checkBoxTrim);
            Controls.Add(buttonBrowseFolder);
            Controls.Add(buttonHelp);
            Controls.Add(label3);
            Controls.Add(comboBoxFileExtension);
            Controls.Add(textBoxFilename);
            Controls.Add(buttonSelectFolder);
            Controls.Add(trimTop);
            Controls.Add(trimBottom);
            Controls.Add(trimRight);
            Controls.Add(trimLeft);
            Controls.Add(label1);
            Controls.Add(textBoxFolder);
            Name = "Options";
            Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)trimTop).EndInit();
            ((System.ComponentModel.ISupportInitialize)trimBottom).EndInit();
            ((System.ComponentModel.ISupportInitialize)trimRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)trimLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTitleMaxLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSplitIndex).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownJpegQuality).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
    }
}