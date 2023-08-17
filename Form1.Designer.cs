namespace ScreenShotTool
{
    partial class Form1
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
            SuspendLayout();
            // 
            // buttonScreenshot
            // 
            buttonScreenshot.Location = new Point(12, 12);
            buttonScreenshot.Name = "buttonScreenshot";
            buttonScreenshot.Size = new Size(128, 23);
            buttonScreenshot.TabIndex = 0;
            buttonScreenshot.Text = "Screenshot Window";
            buttonScreenshot.UseVisualStyleBackColor = true;
            buttonScreenshot.Click += buttonScreenshot_Click;
            // 
            // textBoxFolder
            // 
            textBoxFolder.Location = new Point(12, 66);
            textBoxFolder.Name = "textBoxFolder";
            textBoxFolder.Size = new Size(382, 23);
            textBoxFolder.TabIndex = 1;
            textBoxFolder.Text = "e:\\capture";
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(textBoxFolder);
            Controls.Add(buttonScreenshot);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonScreenshot;
        private TextBox textBoxFolder;
        private Label label1;
    }
}