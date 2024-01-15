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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenshotEditor));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            itemNewImage = new ToolStripMenuItem();
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
            buttonBlur = new Button();
            buttonSelect = new Button();
            buttonBorder = new Button();
            buttonText = new Button();
            buttonArrow = new Button();
            buttonLine = new Button();
            buttonCircle = new Button();
            buttonRectangle = new Button();
            numericNewLineWeight = new NumericUpDown();
            panelImage = new Panel();
            pictureBoxOverlay = new PictureBox();
            listViewSymbols = new ListView();
            columnHeaderType = new ColumnHeader();
            columnHeaderX = new ColumnHeader();
            columnHeaderY = new ColumnHeader();
            columnHeaderColor = new ColumnHeader();
            panelPropertiesPosition = new Panel();
            numericHeight = new NumericUpDown();
            numericY = new NumericUpDown();
            numericWidth = new NumericUpDown();
            numericX = new NumericUpDown();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            labelSymbolType = new Label();
            label13 = new Label();
            numericPropertiesLineAlpha = new NumericUpDown();
            buttonPropertiesColorLine = new Button();
            label6 = new Label();
            textBoxSymbolText = new TextBox();
            label14 = new Label();
            numericPropertiesFillAlpha = new NumericUpDown();
            numericPropertiesLineWeight = new NumericUpDown();
            buttonPropertiesColorFill = new Button();
            label8 = new Label();
            label7 = new Label();
            buttonDeleteSymbol = new Button();
            colorDialog1 = new ColorDialog();
            buttonNewColorFill = new Button();
            buttonNewColorLine = new Button();
            label1 = new Label();
            label9 = new Label();
            label10 = new Label();
            numericNewLineAlpha = new NumericUpDown();
            label11 = new Label();
            label12 = new Label();
            numericNewFillAlpha = new NumericUpDown();
            panelPropertiesFill = new Panel();
            panelPropertiesText = new Panel();
            checkBoxUnderline = new CheckBox();
            checkBoxStrikeout = new CheckBox();
            checkBoxFontItalic = new CheckBox();
            checkBoxFontBold = new CheckBox();
            label16 = new Label();
            label15 = new Label();
            numericPropertiesFontSize = new NumericUpDown();
            comboBoxFontFamily = new ComboBox();
            panel1 = new Panel();
            label17 = new Label();
            numericBlurMosaicSize = new NumericUpDown();
            checkBoxNewShadow = new CheckBox();
            panelPropertiesLine = new Panel();
            checkBoxPropertiesShadow = new CheckBox();
            timerAfterLoad = new System.Windows.Forms.Timer(components);
            menuStrip1.SuspendLayout();
            panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericNewLineWeight).BeginInit();
            panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOverlay).BeginInit();
            panelPropertiesPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineAlpha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesFillAlpha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineWeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericNewLineAlpha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericNewFillAlpha).BeginInit();
            panelPropertiesFill.SuspendLayout();
            panelPropertiesText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesFontSize).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericBlurMosaicSize).BeginInit();
            panelPropertiesLine.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(950, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { itemNewImage, itemSave, itemLoadFromFile, itemLoadFromClipboard, itemExit });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // itemNewImage
            // 
            itemNewImage.Name = "itemNewImage";
            itemNewImage.Size = new Size(184, 22);
            itemNewImage.Text = "&New...";
            itemNewImage.Click += ItemNewImage_Click;
            // 
            // itemSave
            // 
            itemSave.Name = "itemSave";
            itemSave.Size = new Size(184, 22);
            itemSave.Text = "&Save...";
            itemSave.Click += SaveToolStripMenuItem_Click;
            // 
            // itemLoadFromFile
            // 
            itemLoadFromFile.Name = "itemLoadFromFile";
            itemLoadFromFile.Size = new Size(184, 22);
            itemLoadFromFile.Text = "&Load...";
            itemLoadFromFile.Click += LoadToolStripMenuItem_Click;
            // 
            // itemLoadFromClipboard
            // 
            itemLoadFromClipboard.Name = "itemLoadFromClipboard";
            itemLoadFromClipboard.Size = new Size(184, 22);
            itemLoadFromClipboard.Text = "Load from &Clipboard";
            itemLoadFromClipboard.Click += ItemLoadFromClipboard_Click;
            // 
            // itemExit
            // 
            itemExit.Name = "itemExit";
            itemExit.Size = new Size(184, 22);
            itemExit.Text = "E&xit";
            itemExit.Click += ItemExit_Click;
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
            copyToolStripMenuItem.Click += CopyToolStripMenuItem_Click;
            // 
            // pasteIntoThisImageToolStripMenuItem
            // 
            pasteIntoThisImageToolStripMenuItem.Name = "pasteIntoThisImageToolStripMenuItem";
            pasteIntoThisImageToolStripMenuItem.Size = new Size(190, 22);
            pasteIntoThisImageToolStripMenuItem.Text = "&Paste (Real size)";
            pasteIntoThisImageToolStripMenuItem.Click += PasteIntoThisImage_Click;
            // 
            // itemPasteScaled
            // 
            itemPasteScaled.Name = "itemPasteScaled";
            itemPasteScaled.Size = new Size(190, 22);
            itemPasteScaled.Text = "Paste (&Scaled)";
            itemPasteScaled.Click += ItemPasteScaled_Click;
            // 
            // deleteOverlayElementsToolStripMenuItem
            // 
            deleteOverlayElementsToolStripMenuItem.Name = "deleteOverlayElementsToolStripMenuItem";
            deleteOverlayElementsToolStripMenuItem.Size = new Size(190, 22);
            deleteOverlayElementsToolStripMenuItem.Text = "&Delete added symbols";
            deleteOverlayElementsToolStripMenuItem.Click += DeleteOverlayElementsToolStripMenuItem_Click;
            // 
            // panelButtons
            // 
            panelButtons.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panelButtons.BorderStyle = BorderStyle.FixedSingle;
            panelButtons.Controls.Add(buttonBlur);
            panelButtons.Controls.Add(buttonSelect);
            panelButtons.Controls.Add(buttonBorder);
            panelButtons.Controls.Add(buttonText);
            panelButtons.Controls.Add(buttonArrow);
            panelButtons.Controls.Add(buttonLine);
            panelButtons.Controls.Add(buttonCircle);
            panelButtons.Controls.Add(buttonRectangle);
            panelButtons.Location = new Point(0, 61);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(46, 617);
            panelButtons.TabIndex = 1;
            // 
            // buttonBlur
            // 
            buttonBlur.FlatAppearance.BorderSize = 0;
            buttonBlur.FlatStyle = FlatStyle.Flat;
            buttonBlur.Image = Properties.Resources.blur;
            buttonBlur.Location = new Point(4, 279);
            buttonBlur.Margin = new Padding(1);
            buttonBlur.Name = "buttonBlur";
            buttonBlur.Size = new Size(36, 36);
            buttonBlur.TabIndex = 9;
            buttonBlur.UseVisualStyleBackColor = true;
            buttonBlur.Click += buttonBlur_Click;
            // 
            // buttonSelect
            // 
            buttonSelect.FlatAppearance.BorderSize = 0;
            buttonSelect.FlatStyle = FlatStyle.Flat;
            buttonSelect.Image = Properties.Resources.cursor_select;
            buttonSelect.Location = new Point(4, 5);
            buttonSelect.Margin = new Padding(1);
            buttonSelect.Name = "buttonSelect";
            buttonSelect.Size = new Size(36, 36);
            buttonSelect.TabIndex = 8;
            buttonSelect.UseVisualStyleBackColor = true;
            buttonSelect.Click += buttonSelect_Click;
            // 
            // buttonBorder
            // 
            buttonBorder.FlatAppearance.BorderSize = 0;
            buttonBorder.FlatStyle = FlatStyle.Flat;
            buttonBorder.Image = Properties.Resources.frame;
            buttonBorder.Location = new Point(4, 241);
            buttonBorder.Margin = new Padding(1);
            buttonBorder.Name = "buttonBorder";
            buttonBorder.Size = new Size(36, 36);
            buttonBorder.TabIndex = 6;
            buttonBorder.UseVisualStyleBackColor = true;
            buttonBorder.Click += ButtonBorder_Click;
            // 
            // buttonText
            // 
            buttonText.FlatAppearance.BorderSize = 0;
            buttonText.FlatStyle = FlatStyle.Flat;
            buttonText.Image = Properties.Resources.toolText;
            buttonText.Location = new Point(4, 203);
            buttonText.Margin = new Padding(1);
            buttonText.Name = "buttonText";
            buttonText.Size = new Size(36, 36);
            buttonText.TabIndex = 5;
            buttonText.UseVisualStyleBackColor = true;
            buttonText.Click += ButtonNewText_Click;
            // 
            // buttonArrow
            // 
            buttonArrow.FlatAppearance.BorderSize = 0;
            buttonArrow.FlatStyle = FlatStyle.Flat;
            buttonArrow.Image = Properties.Resources.toolArrow;
            buttonArrow.Location = new Point(4, 163);
            buttonArrow.Margin = new Padding(1);
            buttonArrow.Name = "buttonArrow";
            buttonArrow.Size = new Size(36, 36);
            buttonArrow.TabIndex = 4;
            buttonArrow.UseVisualStyleBackColor = true;
            buttonArrow.Click += ButtonArrow_Click;
            // 
            // buttonLine
            // 
            buttonLine.FlatAppearance.BorderSize = 0;
            buttonLine.FlatStyle = FlatStyle.Flat;
            buttonLine.Image = Properties.Resources.toolLine;
            buttonLine.Location = new Point(4, 123);
            buttonLine.Margin = new Padding(1);
            buttonLine.Name = "buttonLine";
            buttonLine.Size = new Size(36, 36);
            buttonLine.TabIndex = 2;
            buttonLine.UseVisualStyleBackColor = true;
            buttonLine.Click += ButtonLine_Click;
            // 
            // buttonCircle
            // 
            buttonCircle.FlatAppearance.BorderSize = 0;
            buttonCircle.FlatStyle = FlatStyle.Flat;
            buttonCircle.Image = Properties.Resources.toolEllipse;
            buttonCircle.Location = new Point(4, 83);
            buttonCircle.Margin = new Padding(1);
            buttonCircle.Name = "buttonCircle";
            buttonCircle.Size = new Size(36, 36);
            buttonCircle.TabIndex = 1;
            buttonCircle.UseVisualStyleBackColor = true;
            buttonCircle.Click += ButtonCircle_Click;
            // 
            // buttonRectangle
            // 
            buttonRectangle.FlatAppearance.BorderSize = 0;
            buttonRectangle.FlatStyle = FlatStyle.Flat;
            buttonRectangle.Image = Properties.Resources.toolRectangle2;
            buttonRectangle.Location = new Point(4, 43);
            buttonRectangle.Margin = new Padding(1);
            buttonRectangle.Name = "buttonRectangle";
            buttonRectangle.Size = new Size(36, 36);
            buttonRectangle.TabIndex = 0;
            buttonRectangle.UseVisualStyleBackColor = true;
            buttonRectangle.Click += ButtonRectangle_Click;
            // 
            // numericNewLineWeight
            // 
            numericNewLineWeight.Location = new Point(306, 3);
            numericNewLineWeight.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericNewLineWeight.Name = "numericNewLineWeight";
            numericNewLineWeight.Size = new Size(66, 23);
            numericNewLineWeight.TabIndex = 3;
            numericNewLineWeight.Value = new decimal(new int[] { 2, 0, 0, 0 });
            numericNewLineWeight.ValueChanged += NumericNewLineWeight_ValueChanged;
            // 
            // panelImage
            // 
            panelImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelImage.AutoScroll = true;
            panelImage.BackColor = SystemColors.ControlDark;
            panelImage.Controls.Add(pictureBoxOverlay);
            panelImage.Location = new Point(49, 61);
            panelImage.Name = "panelImage";
            panelImage.Size = new Size(743, 617);
            panelImage.TabIndex = 3;
            // 
            // pictureBoxOverlay
            // 
            pictureBoxOverlay.BackColor = Color.Black;
            pictureBoxOverlay.Location = new Point(0, 0);
            pictureBoxOverlay.Name = "pictureBoxOverlay";
            pictureBoxOverlay.Size = new Size(558, 375);
            pictureBoxOverlay.TabIndex = 1;
            pictureBoxOverlay.TabStop = false;
            pictureBoxOverlay.MouseDown += PictureBoxOverlay_MouseDown;
            pictureBoxOverlay.MouseLeave += PictureBoxOverlay_MouseLeave;
            pictureBoxOverlay.MouseMove += PictureBoxOverlay_MouseMove;
            pictureBoxOverlay.MouseUp += PictureBoxOverlay_MouseUp;
            // 
            // listViewSymbols
            // 
            listViewSymbols.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            listViewSymbols.Columns.AddRange(new ColumnHeader[] { columnHeaderType, columnHeaderX, columnHeaderY, columnHeaderColor });
            listViewSymbols.Location = new Point(795, 61);
            listViewSymbols.MultiSelect = false;
            listViewSymbols.Name = "listViewSymbols";
            listViewSymbols.Size = new Size(153, 196);
            listViewSymbols.TabIndex = 0;
            listViewSymbols.UseCompatibleStateImageBehavior = false;
            listViewSymbols.View = View.List;
            listViewSymbols.SelectedIndexChanged += ListViewSymbols_SelectedIndexChanged;
            listViewSymbols.KeyDown += listViewSymbols_KeyDown;
            // 
            // columnHeaderType
            // 
            columnHeaderType.Text = "Symbol";
            // 
            // panelPropertiesPosition
            // 
            panelPropertiesPosition.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelPropertiesPosition.Controls.Add(numericHeight);
            panelPropertiesPosition.Controls.Add(numericY);
            panelPropertiesPosition.Controls.Add(numericWidth);
            panelPropertiesPosition.Controls.Add(numericX);
            panelPropertiesPosition.Controls.Add(label5);
            panelPropertiesPosition.Controls.Add(label4);
            panelPropertiesPosition.Controls.Add(label3);
            panelPropertiesPosition.Controls.Add(label2);
            panelPropertiesPosition.Controls.Add(labelSymbolType);
            panelPropertiesPosition.Location = new Point(795, 263);
            panelPropertiesPosition.Name = "panelPropertiesPosition";
            panelPropertiesPosition.Size = new Size(153, 81);
            panelPropertiesPosition.TabIndex = 4;
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
            numericHeight.ValueChanged += Numeric_ValueChanged;
            // 
            // numericY
            // 
            numericY.Location = new Point(94, 24);
            numericY.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericY.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            numericY.Name = "numericY";
            numericY.Size = new Size(43, 23);
            numericY.TabIndex = 11;
            numericY.Tag = "Y";
            numericY.ValueChanged += Numeric_ValueChanged;
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
            numericWidth.ValueChanged += Numeric_ValueChanged;
            // 
            // numericX
            // 
            numericX.Location = new Point(21, 24);
            numericX.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericX.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            numericX.Name = "numericX";
            numericX.Size = new Size(43, 23);
            numericX.TabIndex = 9;
            numericX.Tag = "X";
            numericX.ValueChanged += Numeric_ValueChanged;
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
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(3, 28);
            label13.Name = "label13";
            label13.Size = new Size(63, 15);
            label13.TabIndex = 23;
            label13.Text = "Line Alpha";
            // 
            // numericPropertiesLineAlpha
            // 
            numericPropertiesLineAlpha.Location = new Point(94, 24);
            numericPropertiesLineAlpha.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericPropertiesLineAlpha.Name = "numericPropertiesLineAlpha";
            numericPropertiesLineAlpha.Size = new Size(44, 23);
            numericPropertiesLineAlpha.TabIndex = 22;
            numericPropertiesLineAlpha.Value = new decimal(new int[] { 255, 0, 0, 0 });
            numericPropertiesLineAlpha.ValueChanged += Numeric_ValueChanged;
            // 
            // buttonPropertiesColorLine
            // 
            buttonPropertiesColorLine.BackColor = Color.FromArgb(0, 192, 0);
            buttonPropertiesColorLine.FlatStyle = FlatStyle.Flat;
            buttonPropertiesColorLine.Location = new Point(94, 3);
            buttonPropertiesColorLine.Name = "buttonPropertiesColorLine";
            buttonPropertiesColorLine.Size = new Size(43, 19);
            buttonPropertiesColorLine.TabIndex = 13;
            buttonPropertiesColorLine.Tag = "LineColor";
            buttonPropertiesColorLine.UseVisualStyleBackColor = false;
            buttonPropertiesColorLine.Click += ColorChangeClick;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 5);
            label6.Name = "label6";
            label6.Size = new Size(61, 15);
            label6.TabIndex = 6;
            label6.Text = "Line Color";
            // 
            // textBoxSymbolText
            // 
            textBoxSymbolText.Location = new Point(4, 24);
            textBoxSymbolText.Name = "textBoxSymbolText";
            textBoxSymbolText.Size = new Size(135, 23);
            textBoxSymbolText.TabIndex = 26;
            textBoxSymbolText.TextChanged += textBoxSymbolText_TextChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(4, 28);
            label14.Name = "label14";
            label14.Size = new Size(56, 15);
            label14.TabIndex = 25;
            label14.Text = "Fill Alpha";
            // 
            // numericPropertiesFillAlpha
            // 
            numericPropertiesFillAlpha.Location = new Point(95, 24);
            numericPropertiesFillAlpha.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericPropertiesFillAlpha.Name = "numericPropertiesFillAlpha";
            numericPropertiesFillAlpha.Size = new Size(44, 23);
            numericPropertiesFillAlpha.TabIndex = 24;
            numericPropertiesFillAlpha.Value = new decimal(new int[] { 255, 0, 0, 0 });
            numericPropertiesFillAlpha.ValueChanged += Numeric_ValueChanged;
            // 
            // numericPropertiesLineWeight
            // 
            numericPropertiesLineWeight.Location = new Point(94, 49);
            numericPropertiesLineWeight.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericPropertiesLineWeight.Name = "numericPropertiesLineWeight";
            numericPropertiesLineWeight.Size = new Size(43, 23);
            numericPropertiesLineWeight.TabIndex = 15;
            numericPropertiesLineWeight.Tag = "LineWeight";
            numericPropertiesLineWeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericPropertiesLineWeight.ValueChanged += Numeric_ValueChanged;
            // 
            // buttonPropertiesColorFill
            // 
            buttonPropertiesColorFill.BackColor = Color.FromArgb(0, 192, 0);
            buttonPropertiesColorFill.FlatStyle = FlatStyle.Flat;
            buttonPropertiesColorFill.Location = new Point(95, 3);
            buttonPropertiesColorFill.Name = "buttonPropertiesColorFill";
            buttonPropertiesColorFill.Size = new Size(43, 19);
            buttonPropertiesColorFill.TabIndex = 14;
            buttonPropertiesColorFill.Tag = "FillColor";
            buttonPropertiesColorFill.UseVisualStyleBackColor = false;
            buttonPropertiesColorFill.Click += ColorChangeClick;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 53);
            label8.Name = "label8";
            label8.Size = new Size(70, 15);
            label8.TabIndex = 8;
            label8.Text = "Line Weigth";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 5);
            label7.Name = "label7";
            label7.Size = new Size(54, 15);
            label7.TabIndex = 7;
            label7.Text = "Fill Color";
            // 
            // buttonDeleteSymbol
            // 
            buttonDeleteSymbol.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonDeleteSymbol.Location = new Point(795, 655);
            buttonDeleteSymbol.Name = "buttonDeleteSymbol";
            buttonDeleteSymbol.Size = new Size(48, 23);
            buttonDeleteSymbol.TabIndex = 0;
            buttonDeleteSymbol.Text = "Delete";
            buttonDeleteSymbol.UseVisualStyleBackColor = true;
            buttonDeleteSymbol.Click += ButtonDeleteSymbol_Click;
            // 
            // colorDialog1
            // 
            colorDialog1.AnyColor = true;
            colorDialog1.FullOpen = true;
            // 
            // buttonNewColorFill
            // 
            buttonNewColorFill.BackColor = Color.DimGray;
            buttonNewColorFill.FlatStyle = FlatStyle.Flat;
            buttonNewColorFill.Location = new Point(181, 5);
            buttonNewColorFill.Name = "buttonNewColorFill";
            buttonNewColorFill.Size = new Size(43, 19);
            buttonNewColorFill.TabIndex = 18;
            buttonNewColorFill.Tag = "FillColor";
            buttonNewColorFill.UseVisualStyleBackColor = false;
            buttonNewColorFill.Click += NewColorFill_Click;
            // 
            // buttonNewColorLine
            // 
            buttonNewColorLine.BackColor = Color.Red;
            buttonNewColorLine.FlatStyle = FlatStyle.Flat;
            buttonNewColorLine.Location = new Point(72, 5);
            buttonNewColorLine.Name = "buttonNewColorLine";
            buttonNewColorLine.Size = new Size(43, 19);
            buttonNewColorLine.TabIndex = 17;
            buttonNewColorLine.Tag = "LineColor";
            buttonNewColorLine.UseVisualStyleBackColor = false;
            buttonNewColorLine.Click += NewColorLine_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(121, 5);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 16;
            label1.Text = "Fill Color";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(5, 5);
            label9.Name = "label9";
            label9.Size = new Size(61, 15);
            label9.TabIndex = 15;
            label9.Text = "Line Color";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(230, 5);
            label10.Name = "label10";
            label10.Size = new Size(70, 15);
            label10.TabIndex = 19;
            label10.Text = "Line Weight";
            // 
            // numericNewLineAlpha
            // 
            numericNewLineAlpha.Location = new Point(447, 3);
            numericNewLineAlpha.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericNewLineAlpha.Name = "numericNewLineAlpha";
            numericNewLineAlpha.Size = new Size(44, 23);
            numericNewLineAlpha.TabIndex = 20;
            numericNewLineAlpha.Value = new decimal(new int[] { 255, 0, 0, 0 });
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(378, 5);
            label11.Name = "label11";
            label11.Size = new Size(63, 15);
            label11.TabIndex = 21;
            label11.Text = "Line Alpha";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(497, 5);
            label12.Name = "label12";
            label12.Size = new Size(56, 15);
            label12.TabIndex = 23;
            label12.Text = "Fill Alpha";
            // 
            // numericNewFillAlpha
            // 
            numericNewFillAlpha.Location = new Point(559, 3);
            numericNewFillAlpha.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericNewFillAlpha.Name = "numericNewFillAlpha";
            numericNewFillAlpha.Size = new Size(44, 23);
            numericNewFillAlpha.TabIndex = 22;
            numericNewFillAlpha.Value = new decimal(new int[] { 255, 0, 0, 0 });
            // 
            // panelPropertiesFill
            // 
            panelPropertiesFill.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelPropertiesFill.Controls.Add(label14);
            panelPropertiesFill.Controls.Add(numericPropertiesFillAlpha);
            panelPropertiesFill.Controls.Add(label7);
            panelPropertiesFill.Controls.Add(buttonPropertiesColorFill);
            panelPropertiesFill.Location = new Point(795, 348);
            panelPropertiesFill.Name = "panelPropertiesFill";
            panelPropertiesFill.Size = new Size(153, 51);
            panelPropertiesFill.TabIndex = 27;
            // 
            // panelPropertiesText
            // 
            panelPropertiesText.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelPropertiesText.Controls.Add(checkBoxUnderline);
            panelPropertiesText.Controls.Add(checkBoxStrikeout);
            panelPropertiesText.Controls.Add(checkBoxFontItalic);
            panelPropertiesText.Controls.Add(checkBoxFontBold);
            panelPropertiesText.Controls.Add(label16);
            panelPropertiesText.Controls.Add(label15);
            panelPropertiesText.Controls.Add(numericPropertiesFontSize);
            panelPropertiesText.Controls.Add(comboBoxFontFamily);
            panelPropertiesText.Controls.Add(textBoxSymbolText);
            panelPropertiesText.Location = new Point(795, 482);
            panelPropertiesText.Name = "panelPropertiesText";
            panelPropertiesText.Size = new Size(153, 144);
            panelPropertiesText.TabIndex = 28;
            // 
            // checkBoxUnderline
            // 
            checkBoxUnderline.AutoSize = true;
            checkBoxUnderline.Location = new Point(73, 121);
            checkBoxUnderline.Name = "checkBoxUnderline";
            checkBoxUnderline.Size = new Size(77, 19);
            checkBoxUnderline.TabIndex = 34;
            checkBoxUnderline.Text = "Underline";
            checkBoxUnderline.UseVisualStyleBackColor = true;
            checkBoxUnderline.Click += fontStyle_CheckedChanged;
            // 
            // checkBoxStrikeout
            // 
            checkBoxStrikeout.AutoSize = true;
            checkBoxStrikeout.Location = new Point(3, 121);
            checkBoxStrikeout.Name = "checkBoxStrikeout";
            checkBoxStrikeout.Size = new Size(73, 19);
            checkBoxStrikeout.TabIndex = 33;
            checkBoxStrikeout.Text = "Strikeout";
            checkBoxStrikeout.UseVisualStyleBackColor = true;
            checkBoxStrikeout.Click += fontStyle_CheckedChanged;
            // 
            // checkBoxFontItalic
            // 
            checkBoxFontItalic.AutoSize = true;
            checkBoxFontItalic.Location = new Point(73, 102);
            checkBoxFontItalic.Name = "checkBoxFontItalic";
            checkBoxFontItalic.Size = new Size(51, 19);
            checkBoxFontItalic.TabIndex = 32;
            checkBoxFontItalic.Text = "Italic";
            checkBoxFontItalic.UseVisualStyleBackColor = true;
            checkBoxFontItalic.Click += fontStyle_CheckedChanged;
            // 
            // checkBoxFontBold
            // 
            checkBoxFontBold.AutoSize = true;
            checkBoxFontBold.Location = new Point(3, 102);
            checkBoxFontBold.Name = "checkBoxFontBold";
            checkBoxFontBold.Size = new Size(50, 19);
            checkBoxFontBold.TabIndex = 31;
            checkBoxFontBold.Text = "Bold";
            checkBoxFontBold.UseVisualStyleBackColor = true;
            checkBoxFontBold.Click += fontStyle_CheckedChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(4, 6);
            label16.Name = "label16";
            label16.Size = new Size(77, 15);
            label16.TabIndex = 30;
            label16.Text = "Text contents";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(4, 53);
            label15.Name = "label15";
            label15.Size = new Size(54, 15);
            label15.TabIndex = 29;
            label15.Text = "Font Size";
            // 
            // numericPropertiesFontSize
            // 
            numericPropertiesFontSize.Location = new Point(74, 51);
            numericPropertiesFontSize.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericPropertiesFontSize.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericPropertiesFontSize.Name = "numericPropertiesFontSize";
            numericPropertiesFontSize.Size = new Size(65, 23);
            numericPropertiesFontSize.TabIndex = 28;
            numericPropertiesFontSize.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericPropertiesFontSize.ValueChanged += numericPropertiesFontSize_ValueChanged;
            // 
            // comboBoxFontFamily
            // 
            comboBoxFontFamily.FormattingEnabled = true;
            comboBoxFontFamily.Location = new Point(3, 77);
            comboBoxFontFamily.Name = "comboBoxFontFamily";
            comboBoxFontFamily.Size = new Size(135, 23);
            comboBoxFontFamily.TabIndex = 27;
            comboBoxFontFamily.TextChanged += comboBoxFontFamily_ValueMemberChanged;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label17);
            panel1.Controls.Add(numericBlurMosaicSize);
            panel1.Controls.Add(checkBoxNewShadow);
            panel1.Controls.Add(numericNewFillAlpha);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(numericNewLineAlpha);
            panel1.Controls.Add(buttonNewColorLine);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(buttonNewColorFill);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(numericNewLineWeight);
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(949, 31);
            panel1.TabIndex = 29;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(694, 6);
            label17.Name = "label17";
            label17.Size = new Size(92, 15);
            label17.TabIndex = 26;
            label17.Text = "Blur Mosaic Size";
            // 
            // numericBlurMosaicSize
            // 
            numericBlurMosaicSize.Location = new Point(792, 3);
            numericBlurMosaicSize.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericBlurMosaicSize.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            numericBlurMosaicSize.Name = "numericBlurMosaicSize";
            numericBlurMosaicSize.Size = new Size(55, 23);
            numericBlurMosaicSize.TabIndex = 25;
            numericBlurMosaicSize.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericBlurMosaicSize.ValueChanged += numericBlurMosaicSize_ValueChanged;
            numericBlurMosaicSize.Click += numericBlurMosaicSize_Click;
            // 
            // checkBoxNewShadow
            // 
            checkBoxNewShadow.AutoSize = true;
            checkBoxNewShadow.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxNewShadow.Location = new Point(609, 5);
            checkBoxNewShadow.Name = "checkBoxNewShadow";
            checkBoxNewShadow.Size = new Size(68, 19);
            checkBoxNewShadow.TabIndex = 24;
            checkBoxNewShadow.Text = "Shadow";
            checkBoxNewShadow.UseVisualStyleBackColor = true;
            // 
            // panelPropertiesLine
            // 
            panelPropertiesLine.Controls.Add(label13);
            panelPropertiesLine.Controls.Add(numericPropertiesLineWeight);
            panelPropertiesLine.Controls.Add(label6);
            panelPropertiesLine.Controls.Add(numericPropertiesLineAlpha);
            panelPropertiesLine.Controls.Add(buttonPropertiesColorLine);
            panelPropertiesLine.Controls.Add(label8);
            panelPropertiesLine.Location = new Point(795, 402);
            panelPropertiesLine.Name = "panelPropertiesLine";
            panelPropertiesLine.Size = new Size(153, 77);
            panelPropertiesLine.TabIndex = 2;
            // 
            // checkBoxPropertiesShadow
            // 
            checkBoxPropertiesShadow.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            checkBoxPropertiesShadow.AutoSize = true;
            checkBoxPropertiesShadow.Location = new Point(798, 632);
            checkBoxPropertiesShadow.Name = "checkBoxPropertiesShadow";
            checkBoxPropertiesShadow.Size = new Size(68, 19);
            checkBoxPropertiesShadow.TabIndex = 30;
            checkBoxPropertiesShadow.Text = "Shadow";
            checkBoxPropertiesShadow.UseVisualStyleBackColor = true;
            checkBoxPropertiesShadow.Click += checkBoxPropertiesShadow_Click;
            // 
            // timerAfterLoad
            // 
            timerAfterLoad.Interval = 50;
            timerAfterLoad.Tick += timerAfterLoad_Tick;
            // 
            // ScreenshotEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 682);
            Controls.Add(checkBoxPropertiesShadow);
            Controls.Add(panelPropertiesFill);
            Controls.Add(panelPropertiesLine);
            Controls.Add(panel1);
            Controls.Add(panelPropertiesText);
            Controls.Add(panelPropertiesPosition);
            Controls.Add(listViewSymbols);
            Controls.Add(panelImage);
            Controls.Add(panelButtons);
            Controls.Add(menuStrip1);
            Controls.Add(buttonDeleteSymbol);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "ScreenshotEditor";
            Text = "ImageEditor";
            TopMost = true;
            Load += ScreenshotEditor_Load;
            KeyDown += ScreenshotEditor_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericNewLineWeight).EndInit();
            panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxOverlay).EndInit();
            panelPropertiesPosition.ResumeLayout(false);
            panelPropertiesPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineAlpha).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesFillAlpha).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineWeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericNewLineAlpha).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericNewFillAlpha).EndInit();
            panelPropertiesFill.ResumeLayout(false);
            panelPropertiesFill.PerformLayout();
            panelPropertiesText.ResumeLayout(false);
            panelPropertiesText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesFontSize).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericBlurMosaicSize).EndInit();
            panelPropertiesLine.ResumeLayout(false);
            panelPropertiesLine.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private Panel panelButtons;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private Panel panelImage;
        private ToolStripMenuItem itemSave;
        private ToolStripMenuItem itemLoadFromFile;
        private ToolStripMenuItem itemExit;
        private ToolStripMenuItem copyToolStripMenuItem;
        private Button buttonRectangle;
        private PictureBox pictureBoxOverlay;
        private ToolStripMenuItem deleteOverlayElementsToolStripMenuItem;
        private Button buttonCircle;
        private Button buttonLine;
        private ToolStripMenuItem itemLoadFromClipboard;
        private ToolStripMenuItem pasteIntoThisImageToolStripMenuItem;
        private ToolStripMenuItem itemPasteScaled;
        private ListView listViewSymbols;
        private ColumnHeader columnHeaderType;
        private ColumnHeader columnHeaderX;
        private ColumnHeader columnHeaderY;
        private ColumnHeader columnHeaderColor;
        private Panel panelPropertiesPosition;
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
        private Button buttonPropertiesColorFill;
        private Button buttonPropertiesColorLine;
        private NumericUpDown numericNewLineWeight;
        private Button buttonArrow;
        private Button buttonNewColorFill;
        private Button buttonNewColorLine;
        private Label label1;
        private Label label9;
        private Label label10;
        private NumericUpDown numericNewLineAlpha;
        private Label label11;
        private Label label12;
        private NumericUpDown numericNewFillAlpha;
        private Label label14;
        private NumericUpDown numericPropertiesFillAlpha;
        private Label label13;
        private NumericUpDown numericPropertiesLineAlpha;
        private ToolStripMenuItem itemNewImage;
        private TextBox textBoxSymbolText;
        private Button buttonText;
        private Panel panelPropertiesFill;
        private Panel panelPropertiesText;
        private NumericUpDown numericPropertiesFontSize;
        private ComboBox comboBoxFontFamily;
        private CheckBox checkBoxFontItalic;
        private CheckBox checkBoxFontBold;
        private Label label16;
        private Label label15;
        private CheckBox checkBoxUnderline;
        private CheckBox checkBoxStrikeout;
        private Panel panel1;
        private Button buttonBorder;
        private CheckBox checkBoxNewShadow;
        private Panel panelPropertiesLine;
        private CheckBox checkBoxPropertiesShadow;
        private Button buttonBlur;
        private Label label17;
        private NumericUpDown numericBlurMosaicSize;
        private System.Windows.Forms.Timer timerAfterLoad;
        private Button buttonSelect;
        private Button button1;
    }
}