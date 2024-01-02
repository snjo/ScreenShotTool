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
            itemSave = new ToolStripMenuItem();
            itemLoadFromFile = new ToolStripMenuItem();
            itemLoadFromClipboard = new ToolStripMenuItem();
            itemExit = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteIntoThisImageToolStripMenuItem = new ToolStripMenuItem();
            itemPasteScaled = new ToolStripMenuItem();
            deleteOverlayElementsToolStripMenuItem = new ToolStripMenuItem();
            panelButtons = new Panel();
            numericNewLineWeight = new NumericUpDown();
            buttonLine = new Button();
            button2 = new Button();
            button1 = new Button();
            pictureBoxOriginal = new PictureBox();
            panelImage = new Panel();
            pictureBoxOverlay = new PictureBox();
            listViewSymbols = new ListView();
            columnHeaderType = new ColumnHeader();
            columnHeaderX = new ColumnHeader();
            columnHeaderY = new ColumnHeader();
            columnHeaderColor = new ColumnHeader();
            panel1 = new Panel();
            numericPropertiesLineWeight = new NumericUpDown();
            buttonColorFill = new Button();
            buttonColorLine = new Button();
            numericHeight = new NumericUpDown();
            numericY = new NumericUpDown();
            numericWidth = new NumericUpDown();
            numericX = new NumericUpDown();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            labelSymbolType = new Label();
            buttonDeleteSymbol = new Button();
            colorDialog1 = new ColorDialog();
            menuStrip1.SuspendLayout();
            panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericNewLineWeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).BeginInit();
            panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOverlay).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineWeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericX).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(876, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { itemSave, itemLoadFromFile, itemLoadFromClipboard, itemExit });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // itemSave
            // 
            itemSave.Name = "itemSave";
            itemSave.Size = new Size(182, 22);
            itemSave.Text = "&Save...";
            itemSave.Click += saveToolStripMenuItem_Click;
            // 
            // itemLoadFromFile
            // 
            itemLoadFromFile.Name = "itemLoadFromFile";
            itemLoadFromFile.Size = new Size(182, 22);
            itemLoadFromFile.Text = "&Load";
            itemLoadFromFile.Click += loadToolStripMenuItem_Click;
            // 
            // itemLoadFromClipboard
            // 
            itemLoadFromClipboard.Name = "itemLoadFromClipboard";
            itemLoadFromClipboard.Size = new Size(182, 22);
            itemLoadFromClipboard.Text = "Load from clipboard";
            itemLoadFromClipboard.Click += itemLoadFromClipboard_Click;
            // 
            // itemExit
            // 
            itemExit.Name = "itemExit";
            itemExit.Size = new Size(182, 22);
            itemExit.Text = "E&xit";
            itemExit.Click += itemExit_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { copyToolStripMenuItem, pasteIntoThisImageToolStripMenuItem, itemPasteScaled, deleteOverlayElementsToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(190, 22);
            copyToolStripMenuItem.Text = "&Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteIntoThisImageToolStripMenuItem
            // 
            pasteIntoThisImageToolStripMenuItem.Name = "pasteIntoThisImageToolStripMenuItem";
            pasteIntoThisImageToolStripMenuItem.Size = new Size(190, 22);
            pasteIntoThisImageToolStripMenuItem.Text = "&Paste (Real size)";
            pasteIntoThisImageToolStripMenuItem.Click += pasteIntoThisImage_Click;
            // 
            // itemPasteScaled
            // 
            itemPasteScaled.Name = "itemPasteScaled";
            itemPasteScaled.Size = new Size(190, 22);
            itemPasteScaled.Text = "Paste (&Scaled)";
            itemPasteScaled.Click += itemPasteScaled_Click;
            // 
            // deleteOverlayElementsToolStripMenuItem
            // 
            deleteOverlayElementsToolStripMenuItem.Name = "deleteOverlayElementsToolStripMenuItem";
            deleteOverlayElementsToolStripMenuItem.Size = new Size(190, 22);
            deleteOverlayElementsToolStripMenuItem.Text = "&Delete added symbols";
            deleteOverlayElementsToolStripMenuItem.Click += deleteOverlayElementsToolStripMenuItem_Click;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(numericNewLineWeight);
            panelButtons.Controls.Add(buttonLine);
            panelButtons.Controls.Add(button2);
            panelButtons.Controls.Add(button1);
            panelButtons.Dock = DockStyle.Left;
            panelButtons.Location = new Point(0, 24);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(72, 435);
            panelButtons.TabIndex = 1;
            // 
            // numericNewLineWeight
            // 
            numericNewLineWeight.Location = new Point(3, 135);
            numericNewLineWeight.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericNewLineWeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericNewLineWeight.Name = "numericNewLineWeight";
            numericNewLineWeight.Size = new Size(66, 23);
            numericNewLineWeight.TabIndex = 3;
            numericNewLineWeight.Value = new decimal(new int[] { 2, 0, 0, 0 });
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
            pictureBoxOriginal.Size = new Size(600, 411);
            pictureBoxOriginal.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxOriginal.TabIndex = 2;
            pictureBoxOriginal.TabStop = false;
            pictureBoxOriginal.LoadCompleted += pictureBoxOriginal_LoadCompleted;
            // 
            // panelImage
            // 
            panelImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelImage.AutoScroll = true;
            panelImage.BackColor = SystemColors.ControlDark;
            panelImage.Controls.Add(pictureBoxOverlay);
            panelImage.Controls.Add(pictureBoxOriginal);
            panelImage.Location = new Point(72, 24);
            panelImage.Name = "panelImage";
            panelImage.Size = new Size(649, 435);
            panelImage.TabIndex = 3;
            // 
            // pictureBoxOverlay
            // 
            pictureBoxOverlay.BackColor = Color.Transparent;
            pictureBoxOverlay.Location = new Point(0, 0);
            pictureBoxOverlay.Name = "pictureBoxOverlay";
            pictureBoxOverlay.Size = new Size(558, 375);
            pictureBoxOverlay.TabIndex = 1;
            pictureBoxOverlay.TabStop = false;
            pictureBoxOverlay.MouseDown += pictureBoxOverlay_MouseDown;
            pictureBoxOverlay.MouseLeave += pictureBoxOverlay_MouseLeave;
            pictureBoxOverlay.MouseMove += pictureBoxOverlay_MouseMove;
            pictureBoxOverlay.MouseUp += pictureBoxOverlay_MouseUp;
            // 
            // listViewSymbols
            // 
            listViewSymbols.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            listViewSymbols.Columns.AddRange(new ColumnHeader[] { columnHeaderType, columnHeaderX, columnHeaderY, columnHeaderColor });
            listViewSymbols.Location = new Point(727, 27);
            listViewSymbols.MultiSelect = false;
            listViewSymbols.Name = "listViewSymbols";
            listViewSymbols.Size = new Size(148, 255);
            listViewSymbols.TabIndex = 0;
            listViewSymbols.UseCompatibleStateImageBehavior = false;
            listViewSymbols.View = View.Details;
            listViewSymbols.SelectedIndexChanged += listViewSymbols_SelectedIndexChanged;
            // 
            // columnHeaderType
            // 
            columnHeaderType.Text = "Symbol";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.Controls.Add(numericPropertiesLineWeight);
            panel1.Controls.Add(buttonColorFill);
            panel1.Controls.Add(buttonColorLine);
            panel1.Controls.Add(numericHeight);
            panel1.Controls.Add(numericY);
            panel1.Controls.Add(numericWidth);
            panel1.Controls.Add(numericX);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(labelSymbolType);
            panel1.Controls.Add(buttonDeleteSymbol);
            panel1.Location = new Point(727, 288);
            panel1.Name = "panel1";
            panel1.Size = new Size(148, 171);
            panel1.TabIndex = 4;
            // 
            // numericPropertiesLineWeight
            // 
            numericPropertiesLineWeight.Location = new Point(94, 123);
            numericPropertiesLineWeight.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericPropertiesLineWeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericPropertiesLineWeight.Name = "numericPropertiesLineWeight";
            numericPropertiesLineWeight.Size = new Size(43, 23);
            numericPropertiesLineWeight.TabIndex = 15;
            numericPropertiesLineWeight.Tag = "LineWeight";
            numericPropertiesLineWeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericPropertiesLineWeight.ValueChanged += numeric_ValueChanged;
            // 
            // buttonColorFill
            // 
            buttonColorFill.BackColor = Color.FromArgb(0, 192, 0);
            buttonColorFill.FlatStyle = FlatStyle.Flat;
            buttonColorFill.Location = new Point(94, 100);
            buttonColorFill.Name = "buttonColorFill";
            buttonColorFill.Size = new Size(43, 19);
            buttonColorFill.TabIndex = 14;
            buttonColorFill.Tag = "FillColor";
            buttonColorFill.UseVisualStyleBackColor = false;
            buttonColorFill.Click += colorChangeClick;
            // 
            // buttonColorLine
            // 
            buttonColorLine.BackColor = Color.FromArgb(0, 192, 0);
            buttonColorLine.FlatStyle = FlatStyle.Flat;
            buttonColorLine.Location = new Point(94, 79);
            buttonColorLine.Name = "buttonColorLine";
            buttonColorLine.Size = new Size(43, 19);
            buttonColorLine.TabIndex = 13;
            buttonColorLine.Tag = "LineColor";
            buttonColorLine.UseVisualStyleBackColor = false;
            buttonColorLine.Click += colorChangeClick;
            // 
            // numericHeight
            // 
            numericHeight.Location = new Point(94, 53);
            numericHeight.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericHeight.Name = "numericHeight";
            numericHeight.Size = new Size(43, 23);
            numericHeight.TabIndex = 12;
            numericHeight.Tag = "Height";
            numericHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericHeight.ValueChanged += numeric_ValueChanged;
            // 
            // numericY
            // 
            numericY.Location = new Point(94, 24);
            numericY.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericY.Name = "numericY";
            numericY.Size = new Size(43, 23);
            numericY.TabIndex = 11;
            numericY.Tag = "Y";
            numericY.ValueChanged += numeric_ValueChanged;
            // 
            // numericWidth
            // 
            numericWidth.Location = new Point(21, 53);
            numericWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericWidth.Name = "numericWidth";
            numericWidth.Size = new Size(43, 23);
            numericWidth.TabIndex = 10;
            numericWidth.Tag = "Width";
            numericWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericWidth.ValueChanged += numeric_ValueChanged;
            // 
            // numericX
            // 
            numericX.Location = new Point(21, 24);
            numericX.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericX.Name = "numericX";
            numericX.Size = new Size(43, 23);
            numericX.TabIndex = 9;
            numericX.Tag = "X";
            numericX.ValueChanged += numeric_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 125);
            label8.Name = "label8";
            label8.Size = new Size(70, 15);
            label8.TabIndex = 8;
            label8.Text = "Line Weigth";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 104);
            label7.Name = "label7";
            label7.Size = new Size(54, 15);
            label7.TabIndex = 7;
            label7.Text = "Fill Color";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 83);
            label6.Name = "label6";
            label6.Size = new Size(61, 15);
            label6.TabIndex = 6;
            label6.Text = "Line Color";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(74, 55);
            label5.Name = "label5";
            label5.Size = new Size(16, 15);
            label5.TabIndex = 5;
            label5.Text = "H";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 55);
            label4.Name = "label4";
            label4.Size = new Size(18, 15);
            label4.TabIndex = 4;
            label4.Text = "W";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(74, 26);
            label3.Name = "label3";
            label3.Size = new Size(14, 15);
            label3.TabIndex = 3;
            label3.Text = "Y";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 26);
            label2.Name = "label2";
            label2.Size = new Size(14, 15);
            label2.TabIndex = 2;
            label2.Text = "X";
            // 
            // labelSymbolType
            // 
            labelSymbolType.AutoSize = true;
            labelSymbolType.Location = new Point(3, 4);
            labelSymbolType.Name = "labelSymbolType";
            labelSymbolType.Size = new Size(53, 15);
            labelSymbolType.TabIndex = 1;
            labelSymbolType.Text = "Symbol: ";
            // 
            // buttonDeleteSymbol
            // 
            buttonDeleteSymbol.Location = new Point(3, 143);
            buttonDeleteSymbol.Name = "buttonDeleteSymbol";
            buttonDeleteSymbol.Size = new Size(48, 23);
            buttonDeleteSymbol.TabIndex = 0;
            buttonDeleteSymbol.Text = "Delete";
            buttonDeleteSymbol.UseVisualStyleBackColor = true;
            buttonDeleteSymbol.Click += buttonDeleteSymbol_Click;
            // 
            // ScreenshotEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(876, 459);
            Controls.Add(panel1);
            Controls.Add(listViewSymbols);
            Controls.Add(panelImage);
            Controls.Add(panelButtons);
            Controls.Add(menuStrip1);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "ScreenshotEditor";
            Text = "ImageEditor";
            KeyDown += ScreenshotEditor_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericNewLineWeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).EndInit();
            panelImage.ResumeLayout(false);
            panelImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOverlay).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineWeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericX).EndInit();
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
        private ToolStripMenuItem itemSave;
        private ToolStripMenuItem itemLoadFromFile;
        private ToolStripMenuItem itemExit;
        private ToolStripMenuItem copyToolStripMenuItem;
        private Button button1;
        private PictureBox pictureBoxOverlay;
        private ToolStripMenuItem deleteOverlayElementsToolStripMenuItem;
        private Button button2;
        private Button buttonLine;
        private ToolStripMenuItem itemLoadFromClipboard;
        private ToolStripMenuItem pasteIntoThisImageToolStripMenuItem;
        private ToolStripMenuItem itemPasteScaled;
        private ListView listViewSymbols;
        private ColumnHeader columnHeaderType;
        private ColumnHeader columnHeaderX;
        private ColumnHeader columnHeaderY;
        private ColumnHeader columnHeaderColor;
        private Panel panel1;
        private Label labelSymbolType;
        private Button buttonDeleteSymbol;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private NumericUpDown numericX;
        private NumericUpDown numericHeight;
        private NumericUpDown numericY;
        private NumericUpDown numericWidth;
        private ColorDialog colorDialog1;
        private NumericUpDown numericPropertiesLineWeight;
        private Button buttonColorFill;
        private Button buttonColorLine;
        private NumericUpDown numericNewLineWeight;
    }
}