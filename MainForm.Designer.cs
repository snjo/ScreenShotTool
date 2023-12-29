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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            textBoxLog = new TextBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            buttonOpenLastFolder = new Button();
            label2 = new Label();
            numericUpDownCounter = new NumericUpDown();
            buttonOptions = new Button();
            listViewThumbnails = new ListView();
            contextMenuListView = new ContextMenuStrip(components);
            itemOpenImage = new ToolStripMenuItem();
            itemOpenFolder = new ToolStripMenuItem();
            itemDeleteFile = new ToolStripMenuItem();
            itemRemove = new ToolStripMenuItem();
            buttonClearList = new Button();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuSysTray = new ContextMenuStrip(components);
            openProgramToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            enableCroppingToolStripMenuItem = new ToolStripMenuItem();
            openLastUsedFolderToolStripMenuItem = new ToolStripMenuItem();
            pToolStripMenuItem = new ToolStripMenuItem();
            exitApplicationToolStripMenuItem = new ToolStripMenuItem();
            buttonHide = new Button();
            timerHide = new System.Windows.Forms.Timer(components);
            labelShowLog = new Label();
            copyToClipboardToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCounter).BeginInit();
            contextMenuListView.SuspendLayout();
            contextMenuSysTray.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxLog
            // 
            textBoxLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxLog.Location = new Point(12, 299);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ScrollBars = ScrollBars.Vertical;
            textBoxLog.Size = new Size(519, 108);
            textBoxLog.TabIndex = 5;
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
            // listViewThumbnails
            // 
            listViewThumbnails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewThumbnails.ContextMenuStrip = contextMenuListView;
            listViewThumbnails.Location = new Point(12, 32);
            listViewThumbnails.Name = "listViewThumbnails";
            listViewThumbnails.Size = new Size(520, 249);
            listViewThumbnails.TabIndex = 0;
            listViewThumbnails.UseCompatibleStateImageBehavior = false;
            listViewThumbnails.DoubleClick += listView1_DoubleClick;
            listViewThumbnails.KeyDown += listView1_KeyDown;
            listViewThumbnails.MouseDown += listViewThumbnails_MouseDown;
            // 
            // contextMenuListView
            // 
            contextMenuListView.Items.AddRange(new ToolStripItem[] { itemOpenImage, itemOpenFolder, itemDeleteFile, itemRemove, copyToClipboardToolStripMenuItem });
            contextMenuListView.Name = "contextMenuListView";
            contextMenuListView.Size = new Size(199, 136);
            // 
            // itemOpenImage
            // 
            itemOpenImage.Name = "itemOpenImage";
            itemOpenImage.Size = new Size(198, 22);
            itemOpenImage.Text = "&Open Image";
            itemOpenImage.Click += itemOpenImage_Click;
            // 
            // itemOpenFolder
            // 
            itemOpenFolder.Name = "itemOpenFolder";
            itemOpenFolder.Size = new Size(198, 22);
            itemOpenFolder.Text = "Open &Folder in Explorer";
            itemOpenFolder.Click += itemOpenFolder_Click;
            // 
            // itemDeleteFile
            // 
            itemDeleteFile.Name = "itemDeleteFile";
            itemDeleteFile.Size = new Size(198, 22);
            itemDeleteFile.Text = "&Delete File";
            itemDeleteFile.Click += itemDeleteFile_Click;
            // 
            // itemRemove
            // 
            itemRemove.Name = "itemRemove";
            itemRemove.Size = new Size(198, 22);
            itemRemove.Text = "&Remove (don't delete)";
            itemRemove.Click += itemRemove_Click;
            // 
            // buttonClearList
            // 
            buttonClearList.Location = new Point(135, 3);
            buttonClearList.Name = "buttonClearList";
            buttonClearList.Size = new Size(75, 23);
            buttonClearList.TabIndex = 2;
            buttonClearList.Text = "Clear List";
            buttonClearList.UseVisualStyleBackColor = true;
            buttonClearList.Click += buttonClearList_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
            notifyIcon1.BalloonTipText = "No capture done";
            notifyIcon1.BalloonTipTitle = "Problem";
            notifyIcon1.ContextMenuStrip = contextMenuSysTray;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Screenshot Tool";
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipClicked += notifyIcon1_BalloonTipClicked;
            notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;
            // 
            // contextMenuSysTray
            // 
            contextMenuSysTray.Items.AddRange(new ToolStripItem[] { openProgramToolStripMenuItem, optionsToolStripMenuItem, enableCroppingToolStripMenuItem, openLastUsedFolderToolStripMenuItem, pToolStripMenuItem, exitApplicationToolStripMenuItem });
            contextMenuSysTray.Name = "contextMenuSysTray";
            contextMenuSysTray.ShowImageMargin = false;
            contextMenuSysTray.Size = new Size(164, 136);
            // 
            // openProgramToolStripMenuItem
            // 
            openProgramToolStripMenuItem.Name = "openProgramToolStripMenuItem";
            openProgramToolStripMenuItem.Size = new Size(163, 22);
            openProgramToolStripMenuItem.Text = "Open &Program";
            openProgramToolStripMenuItem.Click += openProgramToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.ShortcutKeyDisplayString = "";
            optionsToolStripMenuItem.Size = new Size(163, 22);
            optionsToolStripMenuItem.Text = "&Options";
            optionsToolStripMenuItem.Click += buttonOptions_Click;
            // 
            // enableCroppingToolStripMenuItem
            // 
            enableCroppingToolStripMenuItem.Name = "enableCroppingToolStripMenuItem";
            enableCroppingToolStripMenuItem.Size = new Size(163, 22);
            enableCroppingToolStripMenuItem.Text = "Enable &Cropping";
            enableCroppingToolStripMenuItem.Click += enableCroppingToolStripMenuItem_Click;
            // 
            // openLastUsedFolderToolStripMenuItem
            // 
            openLastUsedFolderToolStripMenuItem.Name = "openLastUsedFolderToolStripMenuItem";
            openLastUsedFolderToolStripMenuItem.Size = new Size(163, 22);
            openLastUsedFolderToolStripMenuItem.Text = "Open last used &Folder";
            openLastUsedFolderToolStripMenuItem.Click += buttonOpenLastFolder_Click;
            // 
            // pToolStripMenuItem
            // 
            pToolStripMenuItem.Name = "pToolStripMenuItem";
            pToolStripMenuItem.Size = new Size(163, 22);
            pToolStripMenuItem.Text = "Open last &Screenshot";
            pToolStripMenuItem.Click += openFileToolStripMenuItem_Click;
            // 
            // exitApplicationToolStripMenuItem
            // 
            exitApplicationToolStripMenuItem.Name = "exitApplicationToolStripMenuItem";
            exitApplicationToolStripMenuItem.Size = new Size(163, 22);
            exitApplicationToolStripMenuItem.Text = "E&xit application";
            exitApplicationToolStripMenuItem.Click += exitApplicationToolStripMenuItem_Click;
            // 
            // buttonHide
            // 
            buttonHide.Location = new Point(216, 3);
            buttonHide.Name = "buttonHide";
            buttonHide.Size = new Size(75, 23);
            buttonHide.TabIndex = 34;
            buttonHide.Text = "Hide";
            buttonHide.UseVisualStyleBackColor = true;
            buttonHide.Click += buttonHide_Click;
            // 
            // timerHide
            // 
            timerHide.Tick += timerHide_Tick;
            // 
            // labelShowLog
            // 
            labelShowLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelShowLog.AutoSize = true;
            labelShowLog.Location = new Point(239, 281);
            labelShowLog.Name = "labelShowLog";
            labelShowLog.Size = new Size(52, 15);
            labelShowLog.TabIndex = 35;
            labelShowLog.Text = "Hide log";
            labelShowLog.Click += label1_Click;
            // 
            // copyToClipboardToolStripMenuItem
            // 
            copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            copyToClipboardToolStripMenuItem.Size = new Size(198, 22);
            copyToClipboardToolStripMenuItem.Text = "&Copy to Clipboard";
            copyToClipboardToolStripMenuItem.Click += copyToClipboardToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 419);
            Controls.Add(labelShowLog);
            Controls.Add(buttonHide);
            Controls.Add(buttonClearList);
            Controls.Add(listViewThumbnails);
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
            SizeChanged += MainForm_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)numericUpDownCounter).EndInit();
            contextMenuListView.ResumeLayout(false);
            contextMenuSysTray.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxLog;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button buttonOpenLastFolder;
        private Label label2;
        private NumericUpDown numericUpDownCounter;
        private Button buttonOptions;
        private ListView listViewThumbnails;
        private Button buttonClearList;
        private NotifyIcon notifyIcon1;
        private Button buttonHide;
        private System.Windows.Forms.Timer timerHide;
        private ContextMenuStrip contextMenuSysTray;
        private ToolStripMenuItem openProgramToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem openLastUsedFolderToolStripMenuItem;
        private ToolStripMenuItem pToolStripMenuItem;
        private ToolStripMenuItem exitApplicationToolStripMenuItem;
        private ToolStripMenuItem enableCroppingToolStripMenuItem;
        private Label labelShowLog;
        private ContextMenuStrip contextMenuListView;
        private ToolStripMenuItem itemOpenImage;
        private ToolStripMenuItem itemOpenFolder;
        private ToolStripMenuItem itemDeleteFile;
        private ToolStripMenuItem itemRemove;
        private ToolStripMenuItem copyToClipboardToolStripMenuItem;
    }
}