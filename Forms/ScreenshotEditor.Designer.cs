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
            buttonDraw = new Button();
            buttonNumbered = new Button();
            buttonCrop = new Button();
            buttonHighlight = new Button();
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
            columnHeaderColor = new ColumnHeader();
            panelPropertiesPosition = new Panel();
            numericPropertiesHeight = new NumericUpDown();
            numericPropertiesY = new NumericUpDown();
            numericPropertiesWidth = new NumericUpDown();
            numericPropertiesX = new NumericUpDown();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            labelSymbolType = new Label();
            buttonPropertiesColorLine = new Button();
            label6 = new Label();
            textBoxSymbolText = new TextBox();
            numericPropertiesLineWeight = new NumericUpDown();
            buttonPropertiesColorFill = new Button();
            label8 = new Label();
            label7 = new Label();
            buttonDeleteSymbol = new Button();
            buttonNewColorFill = new Button();
            buttonNewColorLine = new Button();
            label1 = new Label();
            label9 = new Label();
            label10 = new Label();
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
            panelPropertiesHighlight = new Panel();
            label18 = new Label();
            comboBoxBlendMode = new ComboBox();
            panel1 = new Panel();
            label19 = new Label();
            checkBoxNewShadow = new CheckBox();
            label17 = new Label();
            numericBlurMosaicSize = new NumericUpDown();
            panelPropertiesLine = new Panel();
            checkBoxPropertiesShadow = new CheckBox();
            timerAfterLoad = new System.Windows.Forms.Timer(components);
            panelPropertiesShadow = new Panel();
            panelPropertiesDelete = new Panel();
            buttonToBack = new Button();
            buttonToFront = new Button();
            TimerUpdateOverlay = new System.Windows.Forms.Timer(components);
            panelPropertiesCrop = new Panel();
            buttonPropertyCopyCrop = new Button();
            buttonPropertyCrop = new Button();
            panelPropertiesBlur = new Panel();
            panelPropertiesFillShape = new Panel();
            checkBoxPropertiesFillShape = new CheckBox();
            menuStrip1.SuspendLayout();
            panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericNewLineWeight).BeginInit();
            panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOverlay).BeginInit();
            panelPropertiesPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineWeight).BeginInit();
            panelPropertiesFill.SuspendLayout();
            panelPropertiesText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesFontSize).BeginInit();
            panelPropertiesHighlight.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericBlurMosaicSize).BeginInit();
            panelPropertiesLine.SuspendLayout();
            panelPropertiesShadow.SuspendLayout();
            panelPropertiesDelete.SuspendLayout();
            panelPropertiesCrop.SuspendLayout();
            panelPropertiesBlur.SuspendLayout();
            panelPropertiesFillShape.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(952, 24);
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
            itemLoadFromFile.Text = "&Open...";
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
            panelButtons.Controls.Add(buttonDraw);
            panelButtons.Controls.Add(buttonNumbered);
            panelButtons.Controls.Add(buttonCrop);
            panelButtons.Controls.Add(buttonHighlight);
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
            panelButtons.Size = new Size(46, 667);
            panelButtons.TabIndex = 1;
            // 
            // buttonDraw
            // 
            buttonDraw.AccessibleName = "Draw freehand";
            buttonDraw.FlatAppearance.BorderSize = 0;
            buttonDraw.FlatStyle = FlatStyle.Flat;
            buttonDraw.Image = Properties.Resources.pencil;
            buttonDraw.Location = new Point(4, 241);
            buttonDraw.Margin = new Padding(1);
            buttonDraw.Name = "buttonDraw";
            buttonDraw.Size = new Size(36, 36);
            buttonDraw.TabIndex = 13;
            buttonDraw.UseVisualStyleBackColor = true;
            buttonDraw.Click += buttonDraw_Click;
            // 
            // buttonNumbered
            // 
            buttonNumbered.AccessibleName = "Number markers";
            buttonNumbered.FlatAppearance.BorderSize = 0;
            buttonNumbered.FlatStyle = FlatStyle.Flat;
            buttonNumbered.Image = Properties.Resources.numbered;
            buttonNumbered.Location = new Point(4, 355);
            buttonNumbered.Margin = new Padding(1);
            buttonNumbered.Name = "buttonNumbered";
            buttonNumbered.Size = new Size(36, 36);
            buttonNumbered.TabIndex = 12;
            buttonNumbered.UseVisualStyleBackColor = true;
            buttonNumbered.Click += ButtonNumbered_Click;
            // 
            // buttonCrop
            // 
            buttonCrop.AccessibleName = "Crop";
            buttonCrop.FlatAppearance.BorderSize = 0;
            buttonCrop.FlatStyle = FlatStyle.Flat;
            buttonCrop.Image = Properties.Resources.crop;
            buttonCrop.Location = new Point(4, 429);
            buttonCrop.Margin = new Padding(1);
            buttonCrop.Name = "buttonCrop";
            buttonCrop.Size = new Size(36, 36);
            buttonCrop.TabIndex = 11;
            buttonCrop.UseVisualStyleBackColor = true;
            buttonCrop.Click += ButtonCrop_Click;
            // 
            // buttonHighlight
            // 
            buttonHighlight.AccessibleName = "Highlighter";
            buttonHighlight.FlatAppearance.BorderSize = 0;
            buttonHighlight.FlatStyle = FlatStyle.Flat;
            buttonHighlight.Image = Properties.Resources.highlight1;
            buttonHighlight.Location = new Point(4, 317);
            buttonHighlight.Margin = new Padding(1);
            buttonHighlight.Name = "buttonHighlight";
            buttonHighlight.Size = new Size(36, 36);
            buttonHighlight.TabIndex = 10;
            buttonHighlight.UseVisualStyleBackColor = true;
            buttonHighlight.Click += ButtonHighlight_Click;
            // 
            // buttonBlur
            // 
            buttonBlur.AccessibleName = "Blur Mosaic";
            buttonBlur.FlatAppearance.BorderSize = 0;
            buttonBlur.FlatStyle = FlatStyle.Flat;
            buttonBlur.Image = Properties.Resources.blur1;
            buttonBlur.Location = new Point(4, 279);
            buttonBlur.Margin = new Padding(1);
            buttonBlur.Name = "buttonBlur";
            buttonBlur.Size = new Size(36, 36);
            buttonBlur.TabIndex = 9;
            buttonBlur.UseVisualStyleBackColor = true;
            buttonBlur.Click += ButtonBlur_Click;
            // 
            // buttonSelect
            // 
            buttonSelect.AccessibleName = "Select";
            buttonSelect.FlatAppearance.BorderSize = 0;
            buttonSelect.FlatStyle = FlatStyle.Flat;
            buttonSelect.Image = Properties.Resources.cursor_select;
            buttonSelect.Location = new Point(4, 5);
            buttonSelect.Margin = new Padding(1);
            buttonSelect.Name = "buttonSelect";
            buttonSelect.Size = new Size(36, 36);
            buttonSelect.TabIndex = 8;
            buttonSelect.UseVisualStyleBackColor = true;
            buttonSelect.Click += ButtonSelect_Click;
            // 
            // buttonBorder
            // 
            buttonBorder.AccessibleName = "Border Frame";
            buttonBorder.FlatAppearance.BorderSize = 0;
            buttonBorder.FlatStyle = FlatStyle.Flat;
            buttonBorder.Image = Properties.Resources.frame;
            buttonBorder.Location = new Point(4, 393);
            buttonBorder.Margin = new Padding(1);
            buttonBorder.Name = "buttonBorder";
            buttonBorder.Size = new Size(36, 36);
            buttonBorder.TabIndex = 6;
            buttonBorder.UseVisualStyleBackColor = true;
            buttonBorder.Click += ButtonBorder_Click;
            // 
            // buttonText
            // 
            buttonText.AccessibleName = "Text";
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
            buttonArrow.AccessibleName = "Arrow";
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
            buttonLine.AccessibleName = "Line";
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
            buttonCircle.AccessibleName = "Ellipse";
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
            buttonRectangle.AccessibleName = "Rectangle";
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
            numericNewLineWeight.AccessibleName = "New symbol line weight";
            numericNewLineWeight.Location = new Point(463, 4);
            numericNewLineWeight.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericNewLineWeight.Name = "numericNewLineWeight";
            numericNewLineWeight.Size = new Size(48, 23);
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
            panelImage.Size = new Size(745, 667);
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
            pictureBoxOverlay.MouseMove += PictureBoxOverlay_MouseMove;
            pictureBoxOverlay.MouseUp += PictureBoxOverlay_MouseUp;
            // 
            // listViewSymbols
            // 
            listViewSymbols.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            listViewSymbols.Columns.AddRange(new ColumnHeader[] { columnHeaderType, columnHeaderColor });
            listViewSymbols.Location = new Point(797, 61);
            listViewSymbols.MultiSelect = false;
            listViewSymbols.Name = "listViewSymbols";
            listViewSymbols.Size = new Size(153, 80);
            listViewSymbols.TabIndex = 0;
            listViewSymbols.UseCompatibleStateImageBehavior = false;
            listViewSymbols.View = View.List;
            listViewSymbols.SelectedIndexChanged += ListViewSymbols_SelectedIndexChanged;
            // 
            // columnHeaderType
            // 
            columnHeaderType.Text = "Symbol";
            // 
            // columnHeaderColor
            // 
            columnHeaderColor.Text = "Color";
            // 
            // panelPropertiesPosition
            // 
            panelPropertiesPosition.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelPropertiesPosition.Controls.Add(numericPropertiesHeight);
            panelPropertiesPosition.Controls.Add(numericPropertiesY);
            panelPropertiesPosition.Controls.Add(numericPropertiesWidth);
            panelPropertiesPosition.Controls.Add(numericPropertiesX);
            panelPropertiesPosition.Controls.Add(label5);
            panelPropertiesPosition.Controls.Add(label4);
            panelPropertiesPosition.Controls.Add(label3);
            panelPropertiesPosition.Controls.Add(label2);
            panelPropertiesPosition.Controls.Add(labelSymbolType);
            panelPropertiesPosition.Location = new Point(797, 147);
            panelPropertiesPosition.Name = "panelPropertiesPosition";
            panelPropertiesPosition.Size = new Size(153, 78);
            panelPropertiesPosition.TabIndex = 4;
            // 
            // numericPropertiesHeight
            // 
            numericPropertiesHeight.Location = new Point(94, 53);
            numericPropertiesHeight.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericPropertiesHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericPropertiesHeight.Name = "numericPropertiesHeight";
            numericPropertiesHeight.Size = new Size(43, 23);
            numericPropertiesHeight.TabIndex = 12;
            numericPropertiesHeight.Tag = "Height";
            numericPropertiesHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericPropertiesHeight.ValueChanged += Numeric_ValueChanged;
            // 
            // numericPropertiesY
            // 
            numericPropertiesY.Location = new Point(94, 24);
            numericPropertiesY.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericPropertiesY.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericPropertiesY.Name = "numericPropertiesY";
            numericPropertiesY.Size = new Size(43, 23);
            numericPropertiesY.TabIndex = 11;
            numericPropertiesY.Tag = "Y";
            numericPropertiesY.ValueChanged += Numeric_ValueChanged;
            // 
            // numericPropertiesWidth
            // 
            numericPropertiesWidth.Location = new Point(21, 53);
            numericPropertiesWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericPropertiesWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericPropertiesWidth.Name = "numericPropertiesWidth";
            numericPropertiesWidth.Size = new Size(43, 23);
            numericPropertiesWidth.TabIndex = 10;
            numericPropertiesWidth.Tag = "Width";
            numericPropertiesWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericPropertiesWidth.ValueChanged += Numeric_ValueChanged;
            // 
            // numericPropertiesX
            // 
            numericPropertiesX.Location = new Point(21, 24);
            numericPropertiesX.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericPropertiesX.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericPropertiesX.Name = "numericPropertiesX";
            numericPropertiesX.Size = new Size(43, 23);
            numericPropertiesX.TabIndex = 9;
            numericPropertiesX.Tag = "X";
            numericPropertiesX.ValueChanged += Numeric_ValueChanged;
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
            // buttonPropertiesColorLine
            // 
            buttonPropertiesColorLine.BackColor = Color.FromArgb(0, 192, 0);
            buttonPropertiesColorLine.FlatAppearance.BorderColor = Color.Black;
            buttonPropertiesColorLine.FlatStyle = FlatStyle.Flat;
            buttonPropertiesColorLine.Location = new Point(79, 2);
            buttonPropertiesColorLine.Name = "buttonPropertiesColorLine";
            buttonPropertiesColorLine.Size = new Size(60, 23);
            buttonPropertiesColorLine.TabIndex = 13;
            buttonPropertiesColorLine.Tag = "LineColor";
            buttonPropertiesColorLine.UseVisualStyleBackColor = false;
            buttonPropertiesColorLine.Click += ColorChangeClick;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(4, 6);
            label6.Name = "label6";
            label6.Size = new Size(61, 15);
            label6.TabIndex = 6;
            label6.Text = "Line Color";
            // 
            // textBoxSymbolText
            // 
            textBoxSymbolText.Location = new Point(4, 23);
            textBoxSymbolText.Name = "textBoxSymbolText";
            textBoxSymbolText.Size = new Size(135, 23);
            textBoxSymbolText.TabIndex = 26;
            textBoxSymbolText.TextChanged += TextBoxSymbolText_TextChanged;
            // 
            // numericPropertiesLineWeight
            // 
            numericPropertiesLineWeight.Location = new Point(95, 30);
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
            buttonPropertiesColorFill.FlatAppearance.BorderColor = Color.Black;
            buttonPropertiesColorFill.FlatStyle = FlatStyle.Flat;
            buttonPropertiesColorFill.Location = new Point(79, 2);
            buttonPropertiesColorFill.Name = "buttonPropertiesColorFill";
            buttonPropertiesColorFill.Size = new Size(60, 23);
            buttonPropertiesColorFill.TabIndex = 14;
            buttonPropertiesColorFill.Tag = "FillColor";
            buttonPropertiesColorFill.UseVisualStyleBackColor = false;
            buttonPropertiesColorFill.Click += ColorChangeClick;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(4, 32);
            label8.Name = "label8";
            label8.Size = new Size(70, 15);
            label8.TabIndex = 8;
            label8.Text = "Line Weigth";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(4, 6);
            label7.Name = "label7";
            label7.Size = new Size(54, 15);
            label7.TabIndex = 7;
            label7.Text = "Fill Color";
            // 
            // buttonDeleteSymbol
            // 
            buttonDeleteSymbol.Location = new Point(4, 28);
            buttonDeleteSymbol.Name = "buttonDeleteSymbol";
            buttonDeleteSymbol.Size = new Size(94, 23);
            buttonDeleteSymbol.TabIndex = 0;
            buttonDeleteSymbol.Text = "Delete Symbol";
            buttonDeleteSymbol.UseVisualStyleBackColor = true;
            buttonDeleteSymbol.Click += ButtonDeleteSymbol_Click;
            // 
            // buttonNewColorFill
            // 
            buttonNewColorFill.AccessibleName = "New symbol fill color";
            buttonNewColorFill.BackColor = Color.LightCyan;
            buttonNewColorFill.FlatAppearance.BorderColor = Color.Black;
            buttonNewColorFill.FlatStyle = FlatStyle.Flat;
            buttonNewColorFill.Location = new Point(321, 2);
            buttonNewColorFill.Name = "buttonNewColorFill";
            buttonNewColorFill.Size = new Size(60, 23);
            buttonNewColorFill.TabIndex = 18;
            buttonNewColorFill.Tag = "FillColor";
            buttonNewColorFill.UseVisualStyleBackColor = false;
            buttonNewColorFill.Click += NewSymbolColor_Click;
            // 
            // buttonNewColorLine
            // 
            buttonNewColorLine.AccessibleName = "New symbol line color";
            buttonNewColorLine.BackColor = Color.Orange;
            buttonNewColorLine.FlatAppearance.BorderColor = Color.Black;
            buttonNewColorLine.FlatStyle = FlatStyle.Flat;
            buttonNewColorLine.Location = new Point(195, 2);
            buttonNewColorLine.Name = "buttonNewColorLine";
            buttonNewColorLine.Size = new Size(60, 23);
            buttonNewColorLine.TabIndex = 17;
            buttonNewColorLine.Tag = "LineColor";
            buttonNewColorLine.UseVisualStyleBackColor = false;
            buttonNewColorLine.Click += NewSymbolColor_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(261, 6);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 16;
            label1.Text = "Fill Color";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(130, 6);
            label9.Name = "label9";
            label9.Size = new Size(61, 15);
            label9.TabIndex = 15;
            label9.Text = "Line Color";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(389, 6);
            label10.Name = "label10";
            label10.Size = new Size(70, 15);
            label10.TabIndex = 19;
            label10.Text = "Line Weight";
            // 
            // panelPropertiesFill
            // 
            panelPropertiesFill.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelPropertiesFill.Controls.Add(label7);
            panelPropertiesFill.Controls.Add(buttonPropertiesColorFill);
            panelPropertiesFill.Location = new Point(797, 229);
            panelPropertiesFill.Name = "panelPropertiesFill";
            panelPropertiesFill.Size = new Size(153, 27);
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
            panelPropertiesText.Location = new Point(797, 323);
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
            checkBoxUnderline.Click += FontStyle_CheckedChanged;
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
            checkBoxStrikeout.Click += FontStyle_CheckedChanged;
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
            checkBoxFontItalic.Click += FontStyle_CheckedChanged;
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
            checkBoxFontBold.Click += FontStyle_CheckedChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(4, 5);
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
            numericPropertiesFontSize.ValueChanged += NumericPropertiesFontSize_ValueChanged;
            // 
            // comboBoxFontFamily
            // 
            comboBoxFontFamily.FormattingEnabled = true;
            comboBoxFontFamily.Location = new Point(3, 77);
            comboBoxFontFamily.Name = "comboBoxFontFamily";
            comboBoxFontFamily.Size = new Size(135, 23);
            comboBoxFontFamily.TabIndex = 27;
            comboBoxFontFamily.TextChanged += ComboBoxFontFamily_ValueMemberChanged;
            // 
            // panelPropertiesHighlight
            // 
            panelPropertiesHighlight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelPropertiesHighlight.Controls.Add(label18);
            panelPropertiesHighlight.Controls.Add(comboBoxBlendMode);
            panelPropertiesHighlight.Location = new Point(797, 584);
            panelPropertiesHighlight.Name = "panelPropertiesHighlight";
            panelPropertiesHighlight.Size = new Size(153, 47);
            panelPropertiesHighlight.TabIndex = 31;
            panelPropertiesHighlight.Visible = false;
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
            comboBoxBlendMode.Size = new Size(135, 23);
            comboBoxBlendMode.TabIndex = 0;
            comboBoxBlendMode.Text = "Multiply";
            comboBoxBlendMode.SelectedIndexChanged += ComboBoxBlendMode_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label19);
            panel1.Controls.Add(checkBoxNewShadow);
            panel1.Controls.Add(buttonNewColorLine);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(buttonNewColorFill);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(numericNewLineWeight);
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(951, 31);
            panel1.TabIndex = 29;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label19.Location = new Point(4, 6);
            label19.Name = "label19";
            label19.Size = new Size(125, 15);
            label19.TabIndex = 25;
            label19.Text = "New symbol settings:";
            // 
            // checkBoxNewShadow
            // 
            checkBoxNewShadow.AccessibleName = "New symbol Shadow toggle";
            checkBoxNewShadow.AutoSize = true;
            checkBoxNewShadow.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxNewShadow.Location = new Point(517, 6);
            checkBoxNewShadow.Name = "checkBoxNewShadow";
            checkBoxNewShadow.Size = new Size(68, 19);
            checkBoxNewShadow.TabIndex = 24;
            checkBoxNewShadow.Text = "Shadow";
            checkBoxNewShadow.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(4, 5);
            label17.Name = "label17";
            label17.Size = new Size(68, 15);
            label17.TabIndex = 26;
            label17.Text = "Mosaic Size";
            // 
            // numericBlurMosaicSize
            // 
            numericBlurMosaicSize.AccessibleName = "Mosaic pixel size (applies to all blur symbols)";
            numericBlurMosaicSize.Location = new Point(92, 3);
            numericBlurMosaicSize.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericBlurMosaicSize.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            numericBlurMosaicSize.Name = "numericBlurMosaicSize";
            numericBlurMosaicSize.Size = new Size(47, 23);
            numericBlurMosaicSize.TabIndex = 25;
            numericBlurMosaicSize.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericBlurMosaicSize.ValueChanged += NumericBlurMosaicSize_ValueChanged;
            // 
            // panelPropertiesLine
            // 
            panelPropertiesLine.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelPropertiesLine.Controls.Add(numericPropertiesLineWeight);
            panelPropertiesLine.Controls.Add(label6);
            panelPropertiesLine.Controls.Add(buttonPropertiesColorLine);
            panelPropertiesLine.Controls.Add(label8);
            panelPropertiesLine.Location = new Point(797, 261);
            panelPropertiesLine.Name = "panelPropertiesLine";
            panelPropertiesLine.Size = new Size(153, 55);
            panelPropertiesLine.TabIndex = 2;
            // 
            // checkBoxPropertiesShadow
            // 
            checkBoxPropertiesShadow.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            checkBoxPropertiesShadow.AutoSize = true;
            checkBoxPropertiesShadow.Location = new Point(3, 4);
            checkBoxPropertiesShadow.Name = "checkBoxPropertiesShadow";
            checkBoxPropertiesShadow.Size = new Size(68, 19);
            checkBoxPropertiesShadow.TabIndex = 30;
            checkBoxPropertiesShadow.Text = "Shadow";
            checkBoxPropertiesShadow.UseVisualStyleBackColor = true;
            checkBoxPropertiesShadow.Click += CheckBoxPropertiesShadow_Click;
            // 
            // timerAfterLoad
            // 
            timerAfterLoad.Interval = 50;
            timerAfterLoad.Tick += TimerAfterLoad_Tick;
            // 
            // panelPropertiesShadow
            // 
            panelPropertiesShadow.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelPropertiesShadow.Controls.Add(checkBoxPropertiesShadow);
            panelPropertiesShadow.Location = new Point(797, 468);
            panelPropertiesShadow.Name = "panelPropertiesShadow";
            panelPropertiesShadow.Size = new Size(153, 26);
            panelPropertiesShadow.TabIndex = 32;
            // 
            // panelPropertiesDelete
            // 
            panelPropertiesDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelPropertiesDelete.Controls.Add(buttonToBack);
            panelPropertiesDelete.Controls.Add(buttonToFront);
            panelPropertiesDelete.Controls.Add(buttonDeleteSymbol);
            panelPropertiesDelete.Location = new Point(797, 637);
            panelPropertiesDelete.Name = "panelPropertiesDelete";
            panelPropertiesDelete.Size = new Size(153, 55);
            panelPropertiesDelete.TabIndex = 33;
            // 
            // buttonToBack
            // 
            buttonToBack.Location = new Point(70, 3);
            buttonToBack.Name = "buttonToBack";
            buttonToBack.Size = new Size(69, 23);
            buttonToBack.TabIndex = 2;
            buttonToBack.Text = "To Back";
            buttonToBack.UseVisualStyleBackColor = true;
            buttonToBack.Click += buttonToBack_Click;
            // 
            // buttonToFront
            // 
            buttonToFront.Location = new Point(4, 3);
            buttonToFront.Name = "buttonToFront";
            buttonToFront.Size = new Size(65, 23);
            buttonToFront.TabIndex = 1;
            buttonToFront.Text = "To Front";
            buttonToFront.UseVisualStyleBackColor = true;
            buttonToFront.Click += buttonToFront_Click;
            // 
            // TimerUpdateOverlay
            // 
            TimerUpdateOverlay.Interval = 2;
            TimerUpdateOverlay.Tick += TimerUpdateOverlay_Tick;
            // 
            // panelPropertiesCrop
            // 
            panelPropertiesCrop.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelPropertiesCrop.Controls.Add(buttonPropertyCopyCrop);
            panelPropertiesCrop.Controls.Add(buttonPropertyCrop);
            panelPropertiesCrop.Location = new Point(797, 495);
            panelPropertiesCrop.Name = "panelPropertiesCrop";
            panelPropertiesCrop.Size = new Size(153, 54);
            panelPropertiesCrop.TabIndex = 34;
            // 
            // buttonPropertyCopyCrop
            // 
            buttonPropertyCopyCrop.Location = new Point(3, 2);
            buttonPropertyCopyCrop.Name = "buttonPropertyCopyCrop";
            buttonPropertyCopyCrop.Size = new Size(95, 23);
            buttonPropertyCopyCrop.TabIndex = 2;
            buttonPropertyCopyCrop.Text = "Copy selection";
            buttonPropertyCopyCrop.UseVisualStyleBackColor = true;
            buttonPropertyCopyCrop.Click += ButtonPropertyCopyCrop_Click;
            // 
            // buttonPropertyCrop
            // 
            buttonPropertyCrop.Location = new Point(4, 27);
            buttonPropertyCrop.Name = "buttonPropertyCrop";
            buttonPropertyCrop.Size = new Size(94, 23);
            buttonPropertyCrop.TabIndex = 1;
            buttonPropertyCrop.Text = "Crop image";
            buttonPropertyCrop.UseVisualStyleBackColor = true;
            buttonPropertyCrop.Click += ButtonPropertyCrop_Click;
            // 
            // panelPropertiesBlur
            // 
            panelPropertiesBlur.Controls.Add(label17);
            panelPropertiesBlur.Controls.Add(numericBlurMosaicSize);
            panelPropertiesBlur.Location = new Point(798, 547);
            panelPropertiesBlur.Name = "panelPropertiesBlur";
            panelPropertiesBlur.Size = new Size(153, 28);
            panelPropertiesBlur.TabIndex = 35;
            // 
            // panelPropertiesFillShape
            // 
            panelPropertiesFillShape.Controls.Add(checkBoxPropertiesFillShape);
            panelPropertiesFillShape.Location = new Point(797, 698);
            panelPropertiesFillShape.Name = "panelPropertiesFillShape";
            panelPropertiesFillShape.Size = new Size(153, 26);
            panelPropertiesFillShape.TabIndex = 36;
            // 
            // checkBoxPropertiesFillShape
            // 
            checkBoxPropertiesFillShape.AutoSize = true;
            checkBoxPropertiesFillShape.Location = new Point(3, 4);
            checkBoxPropertiesFillShape.Name = "checkBoxPropertiesFillShape";
            checkBoxPropertiesFillShape.Size = new Size(75, 19);
            checkBoxPropertiesFillShape.TabIndex = 0;
            checkBoxPropertiesFillShape.Text = "Fill shape";
            checkBoxPropertiesFillShape.UseVisualStyleBackColor = true;
            checkBoxPropertiesFillShape.Click += checkBoxPropertiesFillShape_Click;
            // 
            // ScreenshotEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(952, 732);
            Controls.Add(panelPropertiesFillShape);
            Controls.Add(panelPropertiesBlur);
            Controls.Add(panelPropertiesCrop);
            Controls.Add(panelPropertiesHighlight);
            Controls.Add(panelPropertiesDelete);
            Controls.Add(panelPropertiesText);
            Controls.Add(panelPropertiesLine);
            Controls.Add(panelPropertiesFill);
            Controls.Add(panelPropertiesShadow);
            Controls.Add(panel1);
            Controls.Add(panelPropertiesPosition);
            Controls.Add(listViewSymbols);
            Controls.Add(panelImage);
            Controls.Add(panelButtons);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(700, 550);
            Name = "ScreenshotEditor";
            Text = "ImageEditor";
            TopMost = true;
            Deactivate += ScreenshotEditor_Deactivate;
            KeyDown += ScreenshotEditor_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericNewLineWeight).EndInit();
            panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxOverlay).EndInit();
            panelPropertiesPosition.ResumeLayout(false);
            panelPropertiesPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineWeight).EndInit();
            panelPropertiesFill.ResumeLayout(false);
            panelPropertiesFill.PerformLayout();
            panelPropertiesText.ResumeLayout(false);
            panelPropertiesText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesFontSize).EndInit();
            panelPropertiesHighlight.ResumeLayout(false);
            panelPropertiesHighlight.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericBlurMosaicSize).EndInit();
            panelPropertiesLine.ResumeLayout(false);
            panelPropertiesLine.PerformLayout();
            panelPropertiesShadow.ResumeLayout(false);
            panelPropertiesShadow.PerformLayout();
            panelPropertiesDelete.ResumeLayout(false);
            panelPropertiesCrop.ResumeLayout(false);
            panelPropertiesBlur.ResumeLayout(false);
            panelPropertiesBlur.PerformLayout();
            panelPropertiesFillShape.ResumeLayout(false);
            panelPropertiesFillShape.PerformLayout();
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
        private NumericUpDown numericPropertiesX;
        private NumericUpDown numericPropertiesHeight;
        private NumericUpDown numericPropertiesY;
        private NumericUpDown numericPropertiesWidth;
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
        private Button buttonHighlight;
        private Panel panelPropertiesHighlight;
        private ComboBox comboBoxBlendMode;
        private Label label18;
        private Panel panelPropertiesShadow;
        private Panel panelPropertiesDelete;
        private System.Windows.Forms.Timer TimerUpdateOverlay;
        private Button buttonCrop;
        private Panel panelPropertiesCrop;
        private Button buttonPropertyCopyCrop;
        private Button buttonPropertyCrop;
        private Panel panelPropertiesBlur;
        private Label label19;
        private Button buttonNumbered;
        private Button buttonToBack;
        private Button buttonToFront;
        private Button buttonDraw;
        private Panel panelPropertiesFillShape;
        private CheckBox checkBoxPropertiesFillShape;
    }
}