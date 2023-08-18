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
            buttonScreenshot = new Button();
            textBoxFolder = new TextBox();
            label1 = new Label();
            textBoxLog = new TextBox();
            trimLeft = new NumericUpDown();
            trimRight = new NumericUpDown();
            trimBottom = new NumericUpDown();
            trimTop = new NumericUpDown();
            labelTrim = new Label();
            button1 = new Button();
            textBoxFilename = new TextBox();
            comboBoxFileType = new ComboBox();
            label3 = new Label();
            numericUpDownCounter = new NumericUpDown();
            label2 = new Label();
            buttonHelp = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)trimLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimBottom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCounter).BeginInit();
            SuspendLayout();
            // 
            // buttonScreenshot
            // 
            buttonScreenshot.Location = new Point(345, 225);
            buttonScreenshot.Name = "buttonScreenshot";
            buttonScreenshot.Size = new Size(48, 23);
            buttonScreenshot.TabIndex = 0;
            buttonScreenshot.Text = "Test";
            buttonScreenshot.UseVisualStyleBackColor = true;
            buttonScreenshot.Click += buttonScreenshot_Click;
            // 
            // textBoxFolder
            // 
            textBoxFolder.Location = new Point(11, 22);
            textBoxFolder.Name = "textBoxFolder";
            textBoxFolder.Size = new Size(302, 23);
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
            textBoxLog.Size = new Size(382, 143);
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
            // labelTrim
            // 
            labelTrim.AutoSize = true;
            labelTrim.Location = new Point(11, 153);
            labelTrim.Name = "labelTrim";
            labelTrim.Size = new Size(75, 15);
            labelTrim.TabIndex = 8;
            labelTrim.Text = "Trim window";
            // 
            // button1
            // 
            button1.Location = new Point(319, 22);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "Open";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBoxFilename
            // 
            textBoxFilename.Location = new Point(12, 73);
            textBoxFilename.Name = "textBoxFilename";
            textBoxFilename.Size = new Size(272, 23);
            textBoxFilename.TabIndex = 10;
            textBoxFilename.Text = "$w $c";
            // 
            // comboBoxFileType
            // 
            comboBoxFileType.FormattingEnabled = true;
            comboBoxFileType.Items.AddRange(new object[] { ".png", ".jpg", ".gif", ".bmp", ".tiff" });
            comboBoxFileType.Location = new Point(290, 73);
            comboBoxFileType.Name = "comboBoxFileType";
            comboBoxFileType.Size = new Size(75, 23);
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
            numericUpDownCounter.Location = new Point(12, 120);
            numericUpDownCounter.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownCounter.Name = "numericUpDownCounter";
            numericUpDownCounter.Size = new Size(67, 23);
            numericUpDownCounter.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 102);
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(405, 408);
            Controls.Add(buttonHelp);
            Controls.Add(label2);
            Controls.Add(numericUpDownCounter);
            Controls.Add(label3);
            Controls.Add(comboBoxFileType);
            Controls.Add(textBoxFilename);
            Controls.Add(button1);
            Controls.Add(labelTrim);
            Controls.Add(trimTop);
            Controls.Add(trimBottom);
            Controls.Add(trimRight);
            Controls.Add(trimLeft);
            Controls.Add(textBoxLog);
            Controls.Add(label1);
            Controls.Add(textBoxFolder);
            Controls.Add(buttonScreenshot);
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
        private Label labelTrim;
        private Button button1;
        private TextBox textBoxFilename;
        private ComboBox comboBoxFileType;
        private Label label3;
        private NumericUpDown numericUpDownCounter;
        private Label label2;
        private Button buttonHelp;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}