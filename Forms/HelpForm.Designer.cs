namespace ScreenShotTool
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            richTextBox1 = new RichTextBox();
            buttonSave = new Button();
            button1 = new Button();
            linkLabelDocumentation = new LinkLabel();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.BackColor = Color.White;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Location = new Point(12, 36);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(1006, 579);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // buttonSave
            // 
            buttonSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSave.Location = new Point(943, 7);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 1;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += Save_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(816, 7);
            button1.Name = "button1";
            button1.Size = new Size(121, 23);
            button1.TabIndex = 2;
            button1.Text = "Copy to clipboard";
            button1.UseVisualStyleBackColor = true;
            button1.Click += CopyToClipboard_Click;
            // 
            // linkLabelDocumentation
            // 
            linkLabelDocumentation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkLabelDocumentation.AutoSize = true;
            linkLabelDocumentation.Location = new Point(684, 11);
            linkLabelDocumentation.Name = "linkLabelDocumentation";
            linkLabelDocumentation.Size = new Size(126, 15);
            linkLabelDocumentation.TabIndex = 3;
            linkLabelDocumentation.TabStop = true;
            linkLabelDocumentation.Text = "Documentation online";
            linkLabelDocumentation.LinkClicked += LinkLabelDocumentation_LinkClicked;
            // 
            // HelpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1030, 627);
            Controls.Add(linkLabelDocumentation);
            Controls.Add(button1);
            Controls.Add(buttonSave);
            Controls.Add(richTextBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HelpForm";
            Text = "Screenshot Tool Help";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button buttonSave;
        private Button button1;
        private LinkLabel linkLabelDocumentation;
    }
}