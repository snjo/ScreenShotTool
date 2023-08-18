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
            ((System.ComponentModel.ISupportInitialize)trimLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimBottom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimTop).BeginInit();
            SuspendLayout();
            // 
            // buttonScreenshot
            // 
            buttonScreenshot.Location = new Point(12, 12);
            buttonScreenshot.Name = "buttonScreenshot";
            buttonScreenshot.Size = new Size(128, 23);
            buttonScreenshot.TabIndex = 0;
            buttonScreenshot.Text = "Screenshot Test";
            buttonScreenshot.UseVisualStyleBackColor = true;
            buttonScreenshot.Click += buttonScreenshot_Click;
            // 
            // textBoxFolder
            // 
            textBoxFolder.Location = new Point(12, 66);
            textBoxFolder.Name = "textBoxFolder";
            textBoxFolder.Size = new Size(382, 23);
            textBoxFolder.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 48);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 2;
            label1.Text = "Folder";
            // 
            // textBoxLog
            // 
            textBoxLog.Location = new Point(12, 200);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.Size = new Size(382, 99);
            textBoxLog.TabIndex = 3;
            // 
            // trimLeft
            // 
            trimLeft.Location = new Point(12, 142);
            trimLeft.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimLeft.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimLeft.Name = "trimLeft";
            trimLeft.Size = new Size(70, 23);
            trimLeft.TabIndex = 4;
            // 
            // trimRight
            // 
            trimRight.Location = new Point(134, 142);
            trimRight.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimRight.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimRight.Name = "trimRight";
            trimRight.Size = new Size(70, 23);
            trimRight.TabIndex = 5;
            // 
            // trimBottom
            // 
            trimBottom.Location = new Point(70, 171);
            trimBottom.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimBottom.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimBottom.Name = "trimBottom";
            trimBottom.Size = new Size(70, 23);
            trimBottom.TabIndex = 6;
            // 
            // trimTop
            // 
            trimTop.Location = new Point(70, 117);
            trimTop.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimTop.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimTop.Name = "trimTop";
            trimTop.Size = new Size(70, 23);
            trimTop.TabIndex = 7;
            // 
            // labelTrim
            // 
            labelTrim.AutoSize = true;
            labelTrim.Location = new Point(12, 99);
            labelTrim.Name = "labelTrim";
            labelTrim.Size = new Size(75, 15);
            labelTrim.TabIndex = 8;
            labelTrim.Text = "Trim window";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(405, 310);
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
    }
}