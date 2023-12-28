namespace ScreenShotTool
{
    partial class ImageView
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
            pictureBoxScreenshot = new PictureBox();
            panel1 = new Panel();
            pictureBoxDraw = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxScreenshot).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDraw).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxScreenshot
            // 
            pictureBoxScreenshot.BackgroundImageLayout = ImageLayout.None;
            pictureBoxScreenshot.Location = new Point(0, 0);
            pictureBoxScreenshot.Margin = new Padding(3, 4, 3, 4);
            pictureBoxScreenshot.Name = "pictureBoxScreenshot";
            pictureBoxScreenshot.Size = new Size(800, 450);
            pictureBoxScreenshot.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxScreenshot.TabIndex = 0;
            pictureBoxScreenshot.TabStop = false;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(pictureBoxDraw);
            panel1.Controls.Add(pictureBoxScreenshot);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(914, 600);
            panel1.TabIndex = 1;
            // 
            // pictureBoxDraw
            // 
            pictureBoxDraw.Location = new Point(163, 168);
            pictureBoxDraw.Margin = new Padding(3, 4, 3, 4);
            pictureBoxDraw.Name = "pictureBoxDraw";
            pictureBoxDraw.Size = new Size(114, 67);
            pictureBoxDraw.TabIndex = 1;
            pictureBoxDraw.TabStop = false;
            pictureBoxDraw.Click += pictureBoxDraw_Click;
            pictureBoxDraw.MouseDown += pictureBoxDraw_MouseDown;
            pictureBoxDraw.MouseLeave += pictureBoxDraw_MouseLeave;
            pictureBoxDraw.MouseMove += pictureBoxDraw_MouseMove;
            pictureBoxDraw.MouseUp += pictureBoxDraw_MouseUp;
            // 
            // ImageView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(914, 600);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "ImageView";
            StartPosition = FormStartPosition.Manual;
            Text = "ImageView";
            WindowState = FormWindowState.Maximized;
            Load += ImageView_Load;
            KeyDown += ImageView_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBoxScreenshot).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDraw).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxScreenshot;
        private Panel panel1;
        private PictureBox pictureBoxDraw;
    }
}