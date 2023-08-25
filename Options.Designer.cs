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
            ((System.ComponentModel.ISupportInitialize)trimTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimBottom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimLeft).BeginInit();
            SuspendLayout();
            // 
            // checkBoxTrim
            // 
            checkBoxTrim.AutoSize = true;
            checkBoxTrim.Location = new Point(12, 154);
            checkBoxTrim.Name = "checkBoxTrim";
            checkBoxTrim.Size = new Size(142, 19);
            checkBoxTrim.TabIndex = 34;
            checkBoxTrim.Text = "Crop Window capture";
            checkBoxTrim.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseFolder
            // 
            buttonBrowseFolder.Location = new Point(369, 30);
            buttonBrowseFolder.Name = "buttonBrowseFolder";
            buttonBrowseFolder.Size = new Size(57, 23);
            buttonBrowseFolder.TabIndex = 33;
            buttonBrowseFolder.Text = "Browse";
            buttonBrowseFolder.UseVisualStyleBackColor = true;
            // 
            // buttonHelp
            // 
            buttonHelp.Location = new Point(369, 81);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.Size = new Size(26, 23);
            buttonHelp.TabIndex = 32;
            buttonHelp.Text = "?";
            buttonHelp.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 63);
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
            textBoxFilename.Location = new Point(13, 81);
            textBoxFilename.Name = "textBoxFilename";
            textBoxFilename.Size = new Size(291, 23);
            textBoxFilename.TabIndex = 27;
            textBoxFilename.Text = "$w - $c";
            // 
            // buttonSelectFolder
            // 
            buttonSelectFolder.Location = new Point(310, 30);
            buttonSelectFolder.Name = "buttonSelectFolder";
            buttonSelectFolder.Size = new Size(53, 23);
            buttonSelectFolder.TabIndex = 26;
            buttonSelectFolder.Text = "Select";
            buttonSelectFolder.UseVisualStyleBackColor = true;
            // 
            // trimTop
            // 
            trimTop.Location = new Point(70, 179);
            trimTop.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimTop.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimTop.Name = "trimTop";
            trimTop.Size = new Size(70, 23);
            trimTop.TabIndex = 25;
            // 
            // trimBottom
            // 
            trimBottom.Location = new Point(70, 233);
            trimBottom.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimBottom.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimBottom.Name = "trimBottom";
            trimBottom.Size = new Size(70, 23);
            trimBottom.TabIndex = 24;
            // 
            // trimRight
            // 
            trimRight.Location = new Point(134, 204);
            trimRight.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimRight.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimRight.Name = "trimRight";
            trimRight.Size = new Size(70, 23);
            trimRight.TabIndex = 23;
            // 
            // trimLeft
            // 
            trimLeft.Location = new Point(12, 204);
            trimLeft.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trimLeft.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            trimLeft.Name = "trimLeft";
            trimLeft.Size = new Size(70, 23);
            trimLeft.TabIndex = 22;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 12);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 21;
            label1.Text = "Folder";
            // 
            // textBoxFolder
            // 
            textBoxFolder.Location = new Point(12, 30);
            textBoxFolder.Name = "textBoxFolder";
            textBoxFolder.Size = new Size(292, 23);
            textBoxFolder.TabIndex = 20;
            // 
            // buttonOK
            // 
            buttonOK.Location = new Point(348, 275);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 35;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonApply
            // 
            buttonApply.Location = new Point(267, 275);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(75, 23);
            buttonApply.TabIndex = 36;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(186, 275);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 37;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 310);
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
    }
}