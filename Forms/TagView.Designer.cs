using ScreenShotTool.Classes;

namespace ScreenShotTool.Forms
{
    partial class TagView
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
            components = new System.ComponentModel.Container();
            dataGridView1 = new DataGridSpecial(components);
            buttonAddTag = new Button();
            buttonSaveTags = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 35);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(459, 407);
            dataGridView1.TabIndex = 0;
            // 
            // buttonAddTag
            // 
            buttonAddTag.Location = new Point(6, 6);
            buttonAddTag.Name = "buttonAddTag";
            buttonAddTag.Size = new Size(75, 23);
            buttonAddTag.TabIndex = 1;
            buttonAddTag.Text = "Add tag";
            buttonAddTag.UseVisualStyleBackColor = true;
            buttonAddTag.Click += buttonAddTag_Click;
            // 
            // buttonSaveTags
            // 
            buttonSaveTags.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSaveTags.Location = new Point(390, 6);
            buttonSaveTags.Name = "buttonSaveTags";
            buttonSaveTags.Size = new Size(75, 23);
            buttonSaveTags.TabIndex = 2;
            buttonSaveTags.Text = "Save";
            buttonSaveTags.UseVisualStyleBackColor = true;
            buttonSaveTags.Click += buttonSaveTags_Click;
            // 
            // TagView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(471, 443);
            Controls.Add(buttonSaveTags);
            Controls.Add(buttonAddTag);
            Controls.Add(dataGridView1);
            Name = "TagView";
            Text = "TagView";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonAddTag;
        private DataGridSpecial dataGridView1;
        private Button buttonSaveTags;
    }
}