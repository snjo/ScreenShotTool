namespace ScreenShotTool.Forms
{
    partial class ColorDialogAlpha
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
            buttonOK = new Button();
            buttonCancel = new Button();
            trackBar1 = new TrackBar();
            panelColorSampleSolid = new Panel();
            numericUpDown1 = new NumericUpDown();
            labelRed = new Label();
            label1 = new Label();
            numericUpDown2 = new NumericUpDown();
            trackBar2 = new TrackBar();
            label2 = new Label();
            numericUpDown3 = new NumericUpDown();
            trackBar3 = new TrackBar();
            label3 = new Label();
            numericUpDown4 = new NumericUpDown();
            trackBar4 = new TrackBar();
            panelColorSampleAlpha = new Panel();
            labelColorSolid = new Label();
            label4 = new Label();
            panelSwatches = new Panel();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar4).BeginInit();
            SuspendLayout();
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.Location = new Point(213, 415);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 0;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(294, 414);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // trackBar1
            // 
            trackBar1.AutoSize = false;
            trackBar1.Location = new Point(118, 271);
            trackBar1.Maximum = 255;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(122, 28);
            trackBar1.TabIndex = 2;
            trackBar1.TickStyle = TickStyle.None;
            // 
            // panelColorSampleSolid
            // 
            panelColorSampleSolid.BackColor = Color.Red;
            panelColorSampleSolid.Location = new Point(260, 276);
            panelColorSampleSolid.Name = "panelColorSampleSolid";
            panelColorSampleSolid.Size = new Size(54, 34);
            panelColorSampleSolid.TabIndex = 6;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(60, 271);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(52, 23);
            numericUpDown1.TabIndex = 7;
            // 
            // labelRed
            // 
            labelRed.AutoSize = true;
            labelRed.Location = new Point(27, 273);
            labelRed.Name = "labelRed";
            labelRed.Size = new Size(27, 15);
            labelRed.TabIndex = 8;
            labelRed.Text = "Red";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 302);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 11;
            label1.Text = "Blue";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(60, 300);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(52, 23);
            numericUpDown2.TabIndex = 10;
            // 
            // trackBar2
            // 
            trackBar2.AutoSize = false;
            trackBar2.Location = new Point(118, 300);
            trackBar2.Maximum = 255;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(122, 28);
            trackBar2.TabIndex = 9;
            trackBar2.TickStyle = TickStyle.None;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 360);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 17;
            label2.Text = "Alpha";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(60, 358);
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(52, 23);
            numericUpDown3.TabIndex = 16;
            // 
            // trackBar3
            // 
            trackBar3.AutoSize = false;
            trackBar3.Location = new Point(118, 358);
            trackBar3.Maximum = 255;
            trackBar3.Name = "trackBar3";
            trackBar3.Size = new Size(122, 28);
            trackBar3.TabIndex = 15;
            trackBar3.TickStyle = TickStyle.None;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 331);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 14;
            label3.Text = "Green";
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(60, 329);
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(52, 23);
            numericUpDown4.TabIndex = 13;
            // 
            // trackBar4
            // 
            trackBar4.AutoSize = false;
            trackBar4.Location = new Point(118, 329);
            trackBar4.Maximum = 255;
            trackBar4.Name = "trackBar4";
            trackBar4.Size = new Size(122, 28);
            trackBar4.TabIndex = 12;
            trackBar4.TickStyle = TickStyle.None;
            // 
            // panelColorSampleAlpha
            // 
            panelColorSampleAlpha.BackColor = Color.Red;
            panelColorSampleAlpha.Location = new Point(260, 347);
            panelColorSampleAlpha.Name = "panelColorSampleAlpha";
            panelColorSampleAlpha.Size = new Size(54, 34);
            panelColorSampleAlpha.TabIndex = 18;
            // 
            // labelColorSolid
            // 
            labelColorSolid.AutoSize = true;
            labelColorSolid.Location = new Point(260, 313);
            labelColorSolid.Name = "labelColorSolid";
            labelColorSolid.Size = new Size(73, 15);
            labelColorSolid.TabIndex = 19;
            labelColorSolid.Text = "Color (Solid)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(260, 384);
            label4.Name = "label4";
            label4.Size = new Size(106, 15);
            label4.TabIndex = 20;
            label4.Text = "Color (With Alpha)";
            // 
            // panelSwatches
            // 
            panelSwatches.BackColor = SystemColors.ScrollBar;
            panelSwatches.Location = new Point(12, 12);
            panelSwatches.Name = "panelSwatches";
            panelSwatches.Size = new Size(354, 237);
            panelSwatches.TabIndex = 22;
            // 
            // ColorDialogAlpha
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 450);
            Controls.Add(panelSwatches);
            Controls.Add(label4);
            Controls.Add(labelColorSolid);
            Controls.Add(panelColorSampleAlpha);
            Controls.Add(label2);
            Controls.Add(numericUpDown3);
            Controls.Add(trackBar3);
            Controls.Add(label3);
            Controls.Add(numericUpDown4);
            Controls.Add(trackBar4);
            Controls.Add(label1);
            Controls.Add(numericUpDown2);
            Controls.Add(trackBar2);
            Controls.Add(labelRed);
            Controls.Add(numericUpDown1);
            Controls.Add(panelColorSampleSolid);
            Controls.Add(trackBar1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Name = "ColorDialogAlpha";
            Text = "ColorDialogAlpha";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonOK;
        private Button buttonCancel;
        private TrackBar trackBar1;
        private Panel panelColorSampleSolid;
        private NumericUpDown numericUpDown1;
        private Label labelRed;
        private Label label1;
        private NumericUpDown numericUpDown2;
        private TrackBar trackBar2;
        private Label label2;
        private NumericUpDown numericUpDown3;
        private TrackBar trackBar3;
        private Label label3;
        private NumericUpDown numericUpDown4;
        private TrackBar trackBar4;
        private Panel panelColorSampleAlpha;
        private Label labelColorSolid;
        private Label label4;
        private Panel panelSwatches;
    }
}