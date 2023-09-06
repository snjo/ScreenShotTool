namespace ScreenShotTool
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            textBoxLog = new TextBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            buttonOpenLastFolder = new Button();
            label2 = new Label();
            numericUpDownCounter = new NumericUpDown();
            buttonOptions = new Button();
            listView1 = new ListView();
            buttonClearList = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCounter).BeginInit();
            SuspendLayout();
            // 
            // textBoxLog
            // 
            textBoxLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxLog.Location = new Point(12, 276);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ScrollBars = ScrollBars.Vertical;
            textBoxLog.Size = new Size(517, 119);
            textBoxLog.TabIndex = 5;
            textBoxLog.Text = "Default filename values:\r\n$w $c\r\n\r\n$w: Active Window Title\r\n$d/t/ms: Date, Time, Milliseconds\r\n$c: Counter number (auto increments)";
            // 
            // buttonOpenLastFolder
            // 
            buttonOpenLastFolder.Location = new Point(394, 3);
            buttonOpenLastFolder.Name = "buttonOpenLastFolder";
            buttonOpenLastFolder.Size = new Size(136, 23);
            buttonOpenLastFolder.TabIndex = 4;
            buttonOpenLastFolder.Text = "Open Last Folder Used";
            buttonOpenLastFolder.UseVisualStyleBackColor = true;
            buttonOpenLastFolder.Click += buttonOpenLastFolder_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 5);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 33;
            label2.Text = "Counter";
            // 
            // numericUpDownCounter
            // 
            numericUpDownCounter.Location = new Point(68, 3);
            numericUpDownCounter.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownCounter.Name = "numericUpDownCounter";
            numericUpDownCounter.Size = new Size(50, 23);
            numericUpDownCounter.TabIndex = 1;
            // 
            // buttonOptions
            // 
            buttonOptions.Location = new Point(325, 3);
            buttonOptions.Name = "buttonOptions";
            buttonOptions.Size = new Size(63, 23);
            buttonOptions.TabIndex = 3;
            buttonOptions.Text = "Options";
            buttonOptions.UseVisualStyleBackColor = true;
            buttonOptions.Click += buttonOptions_Click;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Location = new Point(12, 32);
            listView1.Name = "listView1";
            listView1.Size = new Size(518, 237);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.DoubleClick += listView1_DoubleClick;
            listView1.KeyDown += listView1_KeyDown;
            // 
            // buttonClearList
            // 
            buttonClearList.Location = new Point(184, 3);
            buttonClearList.Name = "buttonClearList";
            buttonClearList.Size = new Size(75, 23);
            buttonClearList.TabIndex = 2;
            buttonClearList.Text = "Clear List";
            buttonClearList.UseVisualStyleBackColor = true;
            buttonClearList.Click += buttonClearList_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(541, 407);
            Controls.Add(buttonClearList);
            Controls.Add(listView1);
            Controls.Add(buttonOptions);
            Controls.Add(label2);
            Controls.Add(numericUpDownCounter);
            Controls.Add(buttonOpenLastFolder);
            Controls.Add(textBoxLog);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Screenshot Tool";
            FormClosing += Form1_FormClosing;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDownCounter).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonScreenshot;
        private TextBox textBoxLog;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button buttonOpenLastFolder;
        private Label label2;
        private NumericUpDown numericUpDownCounter;
        private Button buttonOptions;
        private ListView listView1;
        private Button buttonClearList;
    }
}