namespace ScreenShotTool.Controls
{
    partial class SymbolBlendMode
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
            panelPropertiesHighlight = new Panel();
            label18 = new Label();
            comboBoxBlendMode = new ComboBox();
            panelPropertiesHighlight.SuspendLayout();
            SuspendLayout();
            // 
            // panelPropertiesHighlight
            // 
            panelPropertiesHighlight.BorderStyle = BorderStyle.FixedSingle;
            panelPropertiesHighlight.Controls.Add(label18);
            panelPropertiesHighlight.Controls.Add(comboBoxBlendMode);
            panelPropertiesHighlight.Dock = DockStyle.Fill;
            panelPropertiesHighlight.Location = new Point(0, 0);
            panelPropertiesHighlight.Name = "panelPropertiesHighlight";
            panelPropertiesHighlight.Size = new Size(170, 50);
            panelPropertiesHighlight.TabIndex = 105;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(3, 4);
            label18.Name = "label18";
            label18.Size = new Size(117, 15);
            label18.TabIndex = 30;
            label18.Text = "Higlight blend mode";
            // 
            // comboBoxBlendMode
            // 
            comboBoxBlendMode.FormattingEnabled = true;
            comboBoxBlendMode.Items.AddRange(new object[] { "Multiply", "Lighten", "Darken", "Desaturate", "Normal", "Divide", "Invert" });
            comboBoxBlendMode.Location = new Point(3, 22);
            comboBoxBlendMode.Name = "comboBoxBlendMode";
            comboBoxBlendMode.Size = new Size(162, 23);
            comboBoxBlendMode.TabIndex = 48;
            comboBoxBlendMode.Text = "Multiply";
            // 
            // SymbolBlendMode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelPropertiesHighlight);
            Name = "SymbolBlendMode";
            Size = new Size(170, 50);
            panelPropertiesHighlight.ResumeLayout(false);
            panelPropertiesHighlight.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelPropertiesHighlight;
        private Label label18;
        internal ComboBox comboBoxBlendMode;
    }
}
