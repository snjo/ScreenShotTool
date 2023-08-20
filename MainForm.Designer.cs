namespace ScreenShotTool
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxFolder = new TextBox();
            label1 = new Label();
            textBoxLog = new TextBox();
            trimLeft = new NumericUpDown();
            trimRight = new NumericUpDown();
            trimBottom = new NumericUpDown();
            trimTop = new NumericUpDown();
            buttonSelectFolder = new Button();
            textBoxFilename = new TextBox();
            comboBoxFileType = new ComboBox();
            label3 = new Label();
            numericUpDownCounter = new NumericUpDown();
            label2 = new Label();
            buttonHelp = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            buttonBrowseFolder = new Button();
            buttonOpenLastFolder = new Button();
            checkBoxTrim = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)trimLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimBottom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCounter).BeginInit();
            SuspendLayout();
            // 
            // textBoxFolder
            // 
            textBoxFolder.Location = new Point(11, 22);
            textBoxFolder.Name = "textBoxFolder";
            textBoxFolder.Size = new Size(292, 23);
            textBoxFolder.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 4);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 2;
            label1.Text = "Folder";
            // 
            // textBoxLog
            // 
            textBoxLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxLog.Location = new Point(12, 254);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ScrollBars = ScrollBars.Vertical;
            textBoxLog.Size = new Size(413, 143);
            textBoxLog.TabIndex = 3;
            textBoxLog.Text = "Default filename values:\r\n$w $c\r\n\r\n$w: Active Window Title\r\n$d/t/ms: Date, Time, Milliseconds\r\n$c: Counter number (auto increments)";
            // 
            // trimLeft
            // 
            trimLeft.Location = new Point(11, 196);
            trimLeft.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimLeft.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimLeft.Name = "trimLeft";
            trimLeft.Size = new Size(70, 23);
            trimLeft.TabIndex = 4;
            // 
            // trimRight
            // 
            trimRight.Location = new Point(133, 196);
            trimRight.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimRight.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimRight.Name = "trimRight";
            trimRight.Size = new Size(70, 23);
            trimRight.TabIndex = 5;
            // 
            // trimBottom
            // 
            trimBottom.Location = new Point(69, 225);
            trimBottom.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimBottom.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimBottom.Name = "trimBottom";
            trimBottom.Size = new Size(70, 23);
            trimBottom.TabIndex = 6;
            // 
            // trimTop
            // 
            trimTop.Location = new Point(69, 171);
            trimTop.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimTop.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimTop.Name = "trimTop";
            trimTop.Size = new Size(70, 23);
            trimTop.TabIndex = 7;
            // 
            // buttonSelectFolder
            // 
            buttonSelectFolder.Location = new Point(309, 22);
            buttonSelectFolder.Name = "buttonSelectFolder";
            buttonSelectFolder.Size = new Size(53, 23);
            buttonSelectFolder.TabIndex = 9;
            buttonSelectFolder.Text = "Select";
            buttonSelectFolder.UseVisualStyleBackColor = true;
            buttonSelectFolder.Click += buttonSelectFolder_Click;
            // 
            // textBoxFilename
            // 
            textBoxFilename.Location = new Point(12, 73);
            textBoxFilename.Name = "textBoxFilename";
            textBoxFilename.Size = new Size(291, 23);
            textBoxFilename.TabIndex = 10;
            textBoxFilename.Text = "$w - $c";
            // 
            // comboBoxFileType
            // 
            comboBoxFileType.FormattingEnabled = true;
            comboBoxFileType.Items.AddRange(new object[] { ".png", ".jpg", ".gif", ".bmp", ".tiff" });
            comboBoxFileType.Location = new Point(309, 73);
            comboBoxFileType.Name = "comboBoxFileType";
            comboBoxFileType.Size = new Size(53, 23);
            comboBoxFileType.TabIndex = 11;
            comboBoxFileType.Text = ".png";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 55);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 13;
            label3.Text = "Filename";
            // 
            // numericUpDownCounter
            // 
            numericUpDownCounter.Location = new Point(309, 102);
            numericUpDownCounter.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownCounter.Name = "numericUpDownCounter";
            numericUpDownCounter.Size = new Size(67, 23);
            numericUpDownCounter.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(253, 104);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 15;
            label2.Text = "Counter";
            // 
            // buttonHelp
            // 
            buttonHelp.Location = new Point(368, 73);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.Size = new Size(26, 23);
            buttonHelp.TabIndex = 16;
            buttonHelp.Text = "?";
            buttonHelp.UseVisualStyleBackColor = true;
            buttonHelp.Click += buttonHelp_Click;
            // 
            // buttonBrowseFolder
            // 
            buttonBrowseFolder.Location = new Point(368, 22);
            buttonBrowseFolder.Name = "buttonBrowseFolder";
            buttonBrowseFolder.Size = new Size(57, 23);
            buttonBrowseFolder.TabIndex = 17;
            buttonBrowseFolder.Text = "Browse";
            buttonBrowseFolder.UseVisualStyleBackColor = true;
            buttonBrowseFolder.Click += buttonBrowseFolder_Click;
            // 
            // buttonOpenLastFolder
            // 
            buttonOpenLastFolder.Location = new Point(268, 225);
            buttonOpenLastFolder.Name = "buttonOpenLastFolder";
            buttonOpenLastFolder.Size = new Size(156, 23);
            buttonOpenLastFolder.TabIndex = 18;
            buttonOpenLastFolder.Text = "Open Last Folder Used";
            buttonOpenLastFolder.UseVisualStyleBackColor = true;
            buttonOpenLastFolder.Click += buttonOpenLastFolder_Click;
            // 
            // checkBoxTrim
            // 
            checkBoxTrim.AutoSize = true;
            checkBoxTrim.Location = new Point(11, 146);
            checkBoxTrim.Name = "checkBoxTrim";
            checkBoxTrim.Size = new Size(142, 19);
            checkBoxTrim.TabIndex = 19;
            checkBoxTrim.Text = "Crop Window capture";
            checkBoxTrim.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 408);
            Controls.Add(checkBoxTrim);
            Controls.Add(buttonOpenLastFolder);
            Controls.Add(buttonBrowseFolder);
            Controls.Add(buttonHelp);
            Controls.Add(label2);
            Controls.Add(numericUpDownCounter);
            Controls.Add(label3);
            Controls.Add(comboBoxFileType);
            Controls.Add(textBoxFilename);
            Controls.Add(buttonSelectFolder);
            Controls.Add(trimTop);
            Controls.Add(trimBottom);
            Controls.Add(trimRight);
            Controls.Add(trimLeft);
            Controls.Add(textBoxLog);
            Controls.Add(label1);
            Controls.Add(textBoxFolder);
            Name = "MainForm";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)trimLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)trimRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)trimBottom).EndInit();
            ((System.ComponentModel.ISupportInitialize)trimTop).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCounter).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonScreenshot;
        private TextBox textBoxFolder;
        private Label label1;
        private TextBox textBoxLog;
        private NumericUpDown trimLeft;
        private NumericUpDown trimRight;
        private NumericUpDown trimBottom;
        private NumericUpDown trimTop;
        private Button buttonSelectFolder;
        private TextBox textBoxFilename;
        private ComboBox comboBoxFileType;
        private Label label3;
        private NumericUpDown numericUpDownCounter;
        private Label label2;
        private Button buttonHelp;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button buttonBrowseFolder;
        private Button buttonOpenLastFolder;
        private CheckBox checkBoxTrim;
    }
}