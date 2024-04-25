namespace ScreenShotTool.Controls
{
    partial class SymbolPosition
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            numericPropertiesHeight = new NumericUpDown();
            numericPropertiesY = new NumericUpDown();
            numericPropertiesWidth = new NumericUpDown();
            numericPropertiesX = new NumericUpDown();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            labelSymbolType = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesX).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(numericPropertiesHeight);
            panel1.Controls.Add(numericPropertiesY);
            panel1.Controls.Add(numericPropertiesWidth);
            panel1.Controls.Add(numericPropertiesX);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(labelSymbolType);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(170, 80);
            panel1.TabIndex = 44;
            // 
            // numericPropertiesHeight
            // 
            numericPropertiesHeight.Location = new Point(110, 52);
            numericPropertiesHeight.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericPropertiesHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericPropertiesHeight.Name = "numericPropertiesHeight";
            numericPropertiesHeight.Size = new Size(55, 23);
            numericPropertiesHeight.TabIndex = 52;
            numericPropertiesHeight.Tag = "Height";
            numericPropertiesHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericPropertiesY
            // 
            numericPropertiesY.Location = new Point(110, 23);
            numericPropertiesY.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericPropertiesY.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericPropertiesY.Name = "numericPropertiesY";
            numericPropertiesY.Size = new Size(55, 23);
            numericPropertiesY.TabIndex = 50;
            numericPropertiesY.Tag = "Y";
            // 
            // numericPropertiesWidth
            // 
            numericPropertiesWidth.Location = new Point(21, 52);
            numericPropertiesWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericPropertiesWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericPropertiesWidth.Name = "numericPropertiesWidth";
            numericPropertiesWidth.Size = new Size(55, 23);
            numericPropertiesWidth.TabIndex = 51;
            numericPropertiesWidth.Tag = "Width";
            numericPropertiesWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericPropertiesX
            // 
            numericPropertiesX.Location = new Point(21, 23);
            numericPropertiesX.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericPropertiesX.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericPropertiesX.Name = "numericPropertiesX";
            numericPropertiesX.Size = new Size(55, 23);
            numericPropertiesX.TabIndex = 49;
            numericPropertiesX.Tag = "X";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(82, 54);
            label5.Name = "label5";
            label5.Size = new Size(16, 15);
            label5.TabIndex = 48;
            label5.Text = "H";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 54);
            label4.Name = "label4";
            label4.Size = new Size(18, 15);
            label4.TabIndex = 47;
            label4.Text = "W";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(82, 25);
            label3.Name = "label3";
            label3.Size = new Size(14, 15);
            label3.TabIndex = 46;
            label3.Text = "Y";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 25);
            label2.Name = "label2";
            label2.Size = new Size(14, 15);
            label2.TabIndex = 45;
            label2.Text = "X";
            // 
            // labelSymbolType
            // 
            labelSymbolType.AutoSize = true;
            labelSymbolType.Location = new Point(3, 3);
            labelSymbolType.Name = "labelSymbolType";
            labelSymbolType.Size = new Size(53, 15);
            labelSymbolType.TabIndex = 44;
            labelSymbolType.Text = "Symbol: ";
            // 
            // SymbolPosition
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "SymbolPosition";
            Size = new Size(170, 80);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesX).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        internal NumericUpDown numericPropertiesHeight;
        internal NumericUpDown numericPropertiesY;
        internal NumericUpDown numericPropertiesWidth;
        internal NumericUpDown numericPropertiesX;
        internal Label labelSymbolType;
    }
}
