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
            buttonMoveUp = new Button();
            buttonMoveDown = new Button();
            buttonDelete = new Button();
            buttonOnTop = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 35);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(459, 407);
            dataGridView1.TabIndex = 0;
            // 
            // buttonAddTag
            // 
            buttonAddTag.Location = new Point(6, 6);
            buttonAddTag.Name = "buttonAddTag";
            buttonAddTag.Size = new Size(35, 23);
            buttonAddTag.TabIndex = 1;
            buttonAddTag.Text = "➕";
            buttonAddTag.UseVisualStyleBackColor = true;
            buttonAddTag.Click += buttonAddTag_Click;
            // 
            // buttonSaveTags
            // 
            buttonSaveTags.Location = new Point(205, 6);
            buttonSaveTags.Name = "buttonSaveTags";
            buttonSaveTags.Size = new Size(35, 23);
            buttonSaveTags.TabIndex = 2;
            buttonSaveTags.Text = "💾";
            buttonSaveTags.UseVisualStyleBackColor = true;
            buttonSaveTags.Click += buttonSaveTags_Click;
            // 
            // buttonMoveUp
            // 
            buttonMoveUp.Location = new Point(47, 6);
            buttonMoveUp.Name = "buttonMoveUp";
            buttonMoveUp.Size = new Size(35, 23);
            buttonMoveUp.TabIndex = 3;
            buttonMoveUp.Text = "🠉";
            buttonMoveUp.UseVisualStyleBackColor = true;
            buttonMoveUp.Click += ButtonMoveUp_Click;
            // 
            // buttonMoveDown
            // 
            buttonMoveDown.Location = new Point(88, 6);
            buttonMoveDown.Name = "buttonMoveDown";
            buttonMoveDown.Size = new Size(35, 23);
            buttonMoveDown.TabIndex = 4;
            buttonMoveDown.Text = "🠋";
            buttonMoveDown.UseVisualStyleBackColor = true;
            buttonMoveDown.Click += ButtonMoveDown_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(129, 6);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(35, 23);
            buttonDelete.TabIndex = 5;
            buttonDelete.Text = "🗑";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonOnTop
            // 
            buttonOnTop.Location = new Point(246, 6);
            buttonOnTop.Name = "buttonOnTop";
            buttonOnTop.Size = new Size(35, 23);
            buttonOnTop.TabIndex = 6;
            buttonOnTop.Text = "📌";
            buttonOnTop.UseVisualStyleBackColor = true;
            buttonOnTop.Click += buttonOnTop_Click;
            // 
            // TagView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(471, 443);
            Controls.Add(buttonOnTop);
            Controls.Add(buttonDelete);
            Controls.Add(buttonMoveDown);
            Controls.Add(buttonMoveUp);
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
        private Button buttonMoveUp;
        private Button buttonMoveDown;
        private Button buttonDelete;
        private Button buttonOnTop;
    }
}