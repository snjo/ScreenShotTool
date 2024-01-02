namespace ScreenShotTool.Forms
{
    partial class ScreenshotEditor
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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteIntoThisImageToolStripMenuItem = new ToolStripMenuItem();
            pasteAndReplaceImageToolStripMenuItem = new ToolStripMenuItem();
            deleteOverlayElementsToolStripMenuItem = new ToolStripMenuItem();
            panelButtons = new Panel();
            button2 = new Button();
            button1 = new Button();
            pictureBoxOriginal = new PictureBox();
            panelImage = new Panel();
            pictureBoxOverlay = new PictureBox();
            buttonLine = new Button();
            menuStrip1.SuspendLayout();
            panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).BeginInit();
            panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOverlay).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(107, 22);
            saveToolStripMenuItem.Text = "&Save...";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(107, 22);
            loadToolStripMenuItem.Text = "&Load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(107, 22);
            exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { copyToolStripMenuItem, pasteIntoThisImageToolStripMenuItem, pasteAndReplaceImageToolStripMenuItem, deleteOverlayElementsToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(202, 22);
            copyToolStripMenuItem.Text = "&Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteIntoThisImageToolStripMenuItem
            // 
            pasteIntoThisImageToolStripMenuItem.Name = "pasteIntoThisImageToolStripMenuItem";
            pasteIntoThisImageToolStripMenuItem.Size = new Size(202, 22);
            pasteIntoThisImageToolStripMenuItem.Text = "Paste &into this image";
            // 
            // pasteAndReplaceImageToolStripMenuItem
            // 
            pasteAndReplaceImageToolStripMenuItem.Name = "pasteAndReplaceImageToolStripMenuItem";
            pasteAndReplaceImageToolStripMenuItem.Size = new Size(202, 22);
            pasteAndReplaceImageToolStripMenuItem.Text = "&Paste and replace image";
            // 
            // deleteOverlayElementsToolStripMenuItem
            // 
            deleteOverlayElementsToolStripMenuItem.Name = "deleteOverlayElementsToolStripMenuItem";
            deleteOverlayElementsToolStripMenuItem.Size = new Size(202, 22);
            deleteOverlayElementsToolStripMenuItem.Text = "Delete overlay elements";
            deleteOverlayElementsToolStripMenuItem.Click += deleteOverlayElementsToolStripMenuItem_Click;
            // 
            // panelButtons
            // 
            panelButtons.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panelButtons.Controls.Add(buttonLine);
            panelButtons.Controls.Add(button2);
            panelButtons.Controls.Add(button1);
            panelButtons.Location = new Point(0, 27);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(72, 422);
            panelButtons.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(3, 45);
            button2.Name = "button2";
            button2.Size = new Size(66, 23);
            button2.TabIndex = 1;
            button2.Text = "Circle";
            button2.UseVisualStyleBackColor = true;
            button2.Click += buttonCircle_Click;
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(66, 36);
            button1.TabIndex = 0;
            button1.Text = "Rectangle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonRectangle_Click;
            // 
            // pictureBoxOriginal
            // 
            pictureBoxOriginal.Location = new Point(0, 0);
            pictureBoxOriginal.Name = "pictureBoxOriginal";
            pictureBoxOriginal.Size = new Size(700, 411);
            pictureBoxOriginal.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxOriginal.TabIndex = 2;
            pictureBoxOriginal.TabStop = false;
            pictureBoxOriginal.LoadCompleted += pictureBoxOriginal_LoadCompleted;
            // 
            // panelImage
            // 
            panelImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelImage.AutoScroll = true;
            panelImage.Controls.Add(pictureBoxOverlay);
            panelImage.Controls.Add(pictureBoxOriginal);
            panelImage.Location = new Point(78, 27);
            panelImage.Name = "panelImage";
            panelImage.Size = new Size(722, 422);
            panelImage.TabIndex = 3;
            // 
            // pictureBoxOverlay
            // 
            pictureBoxOverlay.BackColor = Color.Transparent;
            pictureBoxOverlay.Location = new Point(0, 0);
            pictureBoxOverlay.Name = "pictureBoxOverlay";
            pictureBoxOverlay.Size = new Size(136, 89);
            pictureBoxOverlay.TabIndex = 1;
            pictureBoxOverlay.TabStop = false;
            pictureBoxOverlay.MouseDown += pictureBoxOverlay_MouseDown;
            pictureBoxOverlay.MouseLeave += pictureBoxOverlay_MouseLeave;
            pictureBoxOverlay.MouseMove += pictureBoxOverlay_MouseMove;
            pictureBoxOverlay.MouseUp += pictureBoxOverlay_MouseUp;
            // 
            // buttonLine
            // 
            buttonLine.Location = new Point(3, 74);
            buttonLine.Name = "buttonLine";
            buttonLine.Size = new Size(66, 23);
            buttonLine.TabIndex = 2;
            buttonLine.Text = "Line";
            buttonLine.UseVisualStyleBackColor = true;
            buttonLine.Click += buttonLine_Click;
            // 
            // ScreenshotEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelImage);
            Controls.Add(panelButtons);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "ScreenshotEditor";
            Text = "ImageEditor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).EndInit();
            panelImage.ResumeLayout(false);
            panelImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOverlay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private Panel panelButtons;
        private PictureBox pictureBoxOriginal;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private Panel panelImage;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteIntoThisImageToolStripMenuItem;
        private Button button1;
        private ToolStripMenuItem pasteAndReplaceImageToolStripMenuItem;
        private PictureBox pictureBoxOverlay;
        private ToolStripMenuItem deleteOverlayElementsToolStripMenuItem;
        private Button button2;
        private Button buttonLine;
    }
}