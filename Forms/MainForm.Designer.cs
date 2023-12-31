﻿namespace ScreenShotTool
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
            listViewThumbnails = new ListView();
            contextMenuListView = new ContextMenuStrip(components);
            itemOpenImage = new ToolStripMenuItem();
            itemOpenFolder = new ToolStripMenuItem();
            itemDeleteFile = new ToolStripMenuItem();
            itemRemove = new ToolStripMenuItem();
            copyToClipboardToolStripMenuItem = new ToolStripMenuItem();
            copyFileToolStripMenuItem = new ToolStripMenuItem();
            editImageToolStripMenuItem = new ToolStripMenuItem();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuSysTray = new ContextMenuStrip(components);
            openProgramToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            editorToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            enableCroppingToolStripMenuItem = new ToolStripMenuItem();
            openLastUsedFolderToolStripMenuItem = new ToolStripMenuItem();
            pToolStripMenuItem = new ToolStripMenuItem();
            exitApplicationToolStripMenuItem = new ToolStripMenuItem();
            timerHide = new System.Windows.Forms.Timer(components);
            labelShowLog = new Label();
            menuStrip1 = new MenuStrip();
            TopMenuProgram = new ToolStripMenuItem();
            clearListToolStripMenuItem = new ToolStripMenuItem();
            openLastUsedFolderToolStripMenuItem1 = new ToolStripMenuItem();
            hideApplicationToolStripMenuItem = new ToolStripMenuItem();
            resetCounterToolStripMenuItem = new ToolStripMenuItem();
            toggleCropToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            TopMenuOptions = new ToolStripMenuItem();
            topMenuEdit = new ToolStripMenuItem();
            editSelectedFileToolStripMenuItem = new ToolStripMenuItem();
            editFromClipboardToolStripMenuItem = new ToolStripMenuItem();
            openEditorToolStripMenuItem = new ToolStripMenuItem();
            TopMenuHelp = new ToolStripMenuItem();
            helpofflineCopyToolStripMenuItem = new ToolStripMenuItem();
            helponGithubToolStripMenuItem = new ToolStripMenuItem();
            websiteToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            labelInfo = new Label();
            contextMenuListView.SuspendLayout();
            contextMenuSysTray.SuspendLayout();
            menuStrip1.SuspendLayout();
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
            // listViewThumbnails
            // 
            listViewThumbnails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewThumbnails.ContextMenuStrip = contextMenuListView;
            listViewThumbnails.Location = new Point(12, 27);
            listViewThumbnails.Name = "listViewThumbnails";
            listViewThumbnails.Size = new Size(520, 254);
            listViewThumbnails.TabIndex = 0;
            listViewThumbnails.UseCompatibleStateImageBehavior = false;
            listViewThumbnails.DoubleClick += ListView1_DoubleClick;
            listViewThumbnails.KeyDown += ListView1_KeyDown;
            listViewThumbnails.MouseDown += ListViewThumbnails_MouseDown;
            // 
            // contextMenuListView
            // 
            contextMenuListView.Items.AddRange(new ToolStripItem[] { itemOpenImage, itemOpenFolder, itemDeleteFile, itemRemove, copyToClipboardToolStripMenuItem, copyFileToolStripMenuItem, editImageToolStripMenuItem });
            contextMenuListView.Name = "contextMenuListView";
            contextMenuListView.Size = new Size(199, 158);
            // 
            // itemOpenImage
            // 
            itemOpenImage.Name = "itemOpenImage";
            itemOpenImage.Size = new Size(198, 22);
            itemOpenImage.Text = "&Open Image";
            itemOpenImage.Click += ItemOpenImage_Click;
            // 
            // itemOpenFolder
            // 
            itemOpenFolder.Name = "itemOpenFolder";
            itemOpenFolder.Size = new Size(198, 22);
            itemOpenFolder.Text = "Open &Folder in Explorer";
            itemOpenFolder.Click += ItemOpenFolder_Click;
            // 
            // itemDeleteFile
            // 
            itemDeleteFile.Name = "itemDeleteFile";
            itemDeleteFile.Size = new Size(198, 22);
            itemDeleteFile.Text = "&Delete File";
            itemDeleteFile.Click += ItemDeleteFile_Click;
            // 
            // itemRemove
            // 
            itemRemove.Name = "itemRemove";
            itemRemove.Size = new Size(198, 22);
            itemRemove.Text = "&Remove (don't delete)";
            itemRemove.Click += ItemRemove_Click;
            // 
            // copyToClipboardToolStripMenuItem
            // 
            copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            copyToClipboardToolStripMenuItem.Size = new Size(198, 22);
            copyToClipboardToolStripMenuItem.Text = "&Copy to Clipboard";
            copyToClipboardToolStripMenuItem.Click += CopyToClipboardToolStripMenuItem_Click;
            // 
            // copyFileToolStripMenuItem
            // 
            copyFileToolStripMenuItem.Name = "copyFileToolStripMenuItem";
            copyFileToolStripMenuItem.Size = new Size(198, 22);
            copyFileToolStripMenuItem.Text = "Co&py Files";
            copyFileToolStripMenuItem.Click += CopyFileToolStripMenuItem_Click;
            // 
            // editImageToolStripMenuItem
            // 
            editImageToolStripMenuItem.Name = "editImageToolStripMenuItem";
            editImageToolStripMenuItem.Size = new Size(198, 22);
            editImageToolStripMenuItem.Text = "&Edit Image";
            editImageToolStripMenuItem.Click += EditImageFromFile_Click;
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
            notifyIcon1.BalloonTipClicked += NotifyIcon1_BalloonTipClicked;
            notifyIcon1.DoubleClick += NotifyIcon1_DoubleClick;
            // 
            // contextMenuSysTray
            // 
            contextMenuSysTray.Items.AddRange(new ToolStripItem[] { openProgramToolStripMenuItem, optionsToolStripMenuItem, editorToolStripMenuItem, helpToolStripMenuItem, enableCroppingToolStripMenuItem, openLastUsedFolderToolStripMenuItem, pToolStripMenuItem, exitApplicationToolStripMenuItem });
            contextMenuSysTray.Name = "contextMenuSysTray";
            contextMenuSysTray.ShowImageMargin = false;
            contextMenuSysTray.Size = new Size(164, 180);
            // 
            // openProgramToolStripMenuItem
            // 
            openProgramToolStripMenuItem.Name = "openProgramToolStripMenuItem";
            openProgramToolStripMenuItem.Size = new Size(163, 22);
            openProgramToolStripMenuItem.Text = "Open &Program";
            openProgramToolStripMenuItem.Click += OpenProgramToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.ShortcutKeyDisplayString = "";
            optionsToolStripMenuItem.Size = new Size(163, 22);
            optionsToolStripMenuItem.Text = "&Options";
            optionsToolStripMenuItem.Click += ButtonOptions_Click;
            // 
            // editorToolStripMenuItem
            // 
            editorToolStripMenuItem.Name = "editorToolStripMenuItem";
            editorToolStripMenuItem.Size = new Size(163, 22);
            editorToolStripMenuItem.Text = "Editor";
            editorToolStripMenuItem.Click += editorToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(163, 22);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // enableCroppingToolStripMenuItem
            // 
            enableCroppingToolStripMenuItem.Name = "enableCroppingToolStripMenuItem";
            enableCroppingToolStripMenuItem.Size = new Size(163, 22);
            enableCroppingToolStripMenuItem.Text = "Enable &Cropping";
            enableCroppingToolStripMenuItem.Click += EnableCroppingToolStripMenuItem_Click;
            // 
            // openLastUsedFolderToolStripMenuItem
            // 
            openLastUsedFolderToolStripMenuItem.Name = "openLastUsedFolderToolStripMenuItem";
            openLastUsedFolderToolStripMenuItem.Size = new Size(163, 22);
            openLastUsedFolderToolStripMenuItem.Text = "Open last used &Folder";
            openLastUsedFolderToolStripMenuItem.Click += ButtonOpenLastFolder_Click;
            // 
            // pToolStripMenuItem
            // 
            pToolStripMenuItem.Name = "pToolStripMenuItem";
            pToolStripMenuItem.Size = new Size(163, 22);
            pToolStripMenuItem.Text = "Open last &Screenshot";
            pToolStripMenuItem.Click += OpenFileToolStripMenuItem_Click;
            // 
            // exitApplicationToolStripMenuItem
            // 
            exitApplicationToolStripMenuItem.Name = "exitApplicationToolStripMenuItem";
            exitApplicationToolStripMenuItem.Size = new Size(163, 22);
            exitApplicationToolStripMenuItem.Text = "E&xit application";
            exitApplicationToolStripMenuItem.Click += ExitApplicationToolStripMenuItem_Click;
            // 
            // timerHide
            // 
            timerHide.Tick += TimerHide_Tick;
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
            labelShowLog.Click += Label1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { TopMenuProgram, TopMenuOptions, topMenuEdit, TopMenuHelp });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(543, 24);
            menuStrip1.TabIndex = 36;
            menuStrip1.Text = "menuStrip1";
            // 
            // TopMenuProgram
            // 
            TopMenuProgram.DropDownItems.AddRange(new ToolStripItem[] { clearListToolStripMenuItem, openLastUsedFolderToolStripMenuItem1, hideApplicationToolStripMenuItem, resetCounterToolStripMenuItem, toggleCropToolStripMenuItem, toolStripMenuItem2, exitToolStripMenuItem });
            TopMenuProgram.Name = "TopMenuProgram";
            TopMenuProgram.Size = new Size(65, 20);
            TopMenuProgram.Text = "&Program";
            // 
            // clearListToolStripMenuItem
            // 
            clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
            clearListToolStripMenuItem.Size = new Size(186, 22);
            clearListToolStripMenuItem.Text = "C&lear list";
            clearListToolStripMenuItem.Click += ButtonClearList_Click;
            // 
            // openLastUsedFolderToolStripMenuItem1
            // 
            openLastUsedFolderToolStripMenuItem1.Name = "openLastUsedFolderToolStripMenuItem1";
            openLastUsedFolderToolStripMenuItem1.Size = new Size(186, 22);
            openLastUsedFolderToolStripMenuItem1.Text = "Open last used &folder";
            openLastUsedFolderToolStripMenuItem1.Click += ButtonOpenLastFolder_Click;
            // 
            // hideApplicationToolStripMenuItem
            // 
            hideApplicationToolStripMenuItem.Name = "hideApplicationToolStripMenuItem";
            hideApplicationToolStripMenuItem.Size = new Size(186, 22);
            hideApplicationToolStripMenuItem.Text = "&Hide application";
            hideApplicationToolStripMenuItem.Click += ButtonHide_Click;
            // 
            // resetCounterToolStripMenuItem
            // 
            resetCounterToolStripMenuItem.Name = "resetCounterToolStripMenuItem";
            resetCounterToolStripMenuItem.Size = new Size(186, 22);
            resetCounterToolStripMenuItem.Text = "&Reset counter";
            resetCounterToolStripMenuItem.Click += ResetCounterToolStripMenuItem_Click;
            // 
            // toggleCropToolStripMenuItem
            // 
            toggleCropToolStripMenuItem.Name = "toggleCropToolStripMenuItem";
            toggleCropToolStripMenuItem.Size = new Size(186, 22);
            toggleCropToolStripMenuItem.Text = "Enable &Cropping";
            toggleCropToolStripMenuItem.Click += EnableCroppingToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(183, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(186, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // TopMenuOptions
            // 
            TopMenuOptions.Name = "TopMenuOptions";
            TopMenuOptions.Size = new Size(61, 20);
            TopMenuOptions.Text = "&Options";
            TopMenuOptions.Click += ButtonOptions_Click;
            // 
            // topMenuEdit
            // 
            topMenuEdit.DropDownItems.AddRange(new ToolStripItem[] { editSelectedFileToolStripMenuItem, editFromClipboardToolStripMenuItem, openEditorToolStripMenuItem });
            topMenuEdit.Name = "topMenuEdit";
            topMenuEdit.Size = new Size(39, 20);
            topMenuEdit.Text = "Edit";
            // 
            // editSelectedFileToolStripMenuItem
            // 
            editSelectedFileToolStripMenuItem.Name = "editSelectedFileToolStripMenuItem";
            editSelectedFileToolStripMenuItem.Size = new Size(176, 22);
            editSelectedFileToolStripMenuItem.Text = "Edit selected file";
            editSelectedFileToolStripMenuItem.Click += EditImageFromFile_Click;
            // 
            // editFromClipboardToolStripMenuItem
            // 
            editFromClipboardToolStripMenuItem.Name = "editFromClipboardToolStripMenuItem";
            editFromClipboardToolStripMenuItem.Size = new Size(176, 22);
            editFromClipboardToolStripMenuItem.Text = "Edit from clipboard";
            editFromClipboardToolStripMenuItem.Click += EditImageFromClipboard_Click;
            // 
            // openEditorToolStripMenuItem
            // 
            openEditorToolStripMenuItem.Name = "openEditorToolStripMenuItem";
            openEditorToolStripMenuItem.Size = new Size(176, 22);
            openEditorToolStripMenuItem.Text = "Open editor";
            openEditorToolStripMenuItem.Click += EditImageNoFile_Click;
            // 
            // TopMenuHelp
            // 
            TopMenuHelp.DropDownItems.AddRange(new ToolStripItem[] { helpofflineCopyToolStripMenuItem, helponGithubToolStripMenuItem, websiteToolStripMenuItem, aboutToolStripMenuItem });
            TopMenuHelp.Name = "TopMenuHelp";
            TopMenuHelp.Size = new Size(44, 20);
            TopMenuHelp.Text = "&Help";
            // 
            // helpofflineCopyToolStripMenuItem
            // 
            helpofflineCopyToolStripMenuItem.Name = "helpofflineCopyToolStripMenuItem";
            helpofflineCopyToolStripMenuItem.Size = new Size(220, 22);
            helpofflineCopyToolStripMenuItem.Text = "&Help";
            helpofflineCopyToolStripMenuItem.Click += HelpofflineCopyToolStripMenuItem_Click;
            // 
            // helponGithubToolStripMenuItem
            // 
            helponGithubToolStripMenuItem.Name = "helponGithubToolStripMenuItem";
            helponGithubToolStripMenuItem.Size = new Size(220, 22);
            helponGithubToolStripMenuItem.Text = "&Documentation (on github)";
            helponGithubToolStripMenuItem.Click += HelponGithubToolStripMenuItem_Click;
            // 
            // websiteToolStripMenuItem
            // 
            websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            websiteToolStripMenuItem.Size = new Size(220, 22);
            websiteToolStripMenuItem.Text = "&Website";
            websiteToolStripMenuItem.Click += WebsiteToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(220, 22);
            aboutToolStripMenuItem.Text = "&About";
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // labelInfo
            // 
            labelInfo.BackColor = Color.White;
            labelInfo.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelInfo.ForeColor = Color.Gray;
            labelInfo.Location = new Point(65, 106);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(411, 141);
            labelInfo.TabIndex = 37;
            labelInfo.Text = "To take a screenshot use hotkeys";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 419);
            Controls.Add(labelInfo);
            Controls.Add(menuStrip1);
            Controls.Add(labelShowLog);
            Controls.Add(listViewThumbnails);
            Controls.Add(textBoxLog);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Screenshot Tool";
            FormClosing += Form1_FormClosing;
            Load += MainForm_Load;
            SizeChanged += MainForm_SizeChanged;
            contextMenuListView.ResumeLayout(false);
            contextMenuSysTray.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxLog;
        private FolderBrowserDialog folderBrowserDialog1;
        private ListView listViewThumbnails;
        private NotifyIcon notifyIcon1;
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
        private MenuStrip menuStrip1;
        private ToolStripMenuItem TopMenuProgram;
        private ToolStripMenuItem clearListToolStripMenuItem;
        private ToolStripMenuItem openLastUsedFolderToolStripMenuItem1;
        private ToolStripMenuItem hideApplicationToolStripMenuItem;
        private ToolStripMenuItem TopMenuHelp;
        private ToolStripMenuItem resetCounterToolStripMenuItem;
        private ToolStripMenuItem toggleCropToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpofflineCopyToolStripMenuItem;
        private ToolStripMenuItem helponGithubToolStripMenuItem;
        private ToolStripMenuItem websiteToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem TopMenuOptions;
        private ToolStripMenuItem copyFileToolStripMenuItem;
        private Label labelInfo;
        private ToolStripMenuItem editImageToolStripMenuItem;
        private ToolStripMenuItem topMenuEdit;
        private ToolStripMenuItem editSelectedFileToolStripMenuItem;
        private ToolStripMenuItem editFromClipboardToolStripMenuItem;
        private ToolStripMenuItem openEditorToolStripMenuItem;
        private ToolStripMenuItem editorToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
    }
}