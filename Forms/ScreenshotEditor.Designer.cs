﻿namespace ScreenShotTool.Forms
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
            buttonArrow = new Button();
            buttonLine = new Button();
            button2 = new Button();
            button1 = new Button();
            numericNewLineWeight = new NumericUpDown();
            pictureBoxOriginal = new PictureBox();
            panelImage = new Panel();
            pictureBoxOverlay = new PictureBox();
            listViewSymbols = new ListView();
            columnHeaderType = new ColumnHeader();
            columnHeaderX = new ColumnHeader();
            columnHeaderY = new ColumnHeader();
            columnHeaderColor = new ColumnHeader();
            panel1 = new Panel();
            label14 = new Label();
            numericPropertiesFillAlpha = new NumericUpDown();
            label13 = new Label();
            numericPropertiesLineAlpha = new NumericUpDown();
            numericPropertiesLineWeight = new NumericUpDown();
            buttonPropertiesColorFill = new Button();
            buttonPropertiesColorLine = new Button();
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
            buttonNewColorFill = new Button();
            buttonNewColorLine = new Button();
            label1 = new Label();
            label9 = new Label();
            label10 = new Label();
            numericNewLineAlpha = new NumericUpDown();
            label11 = new Label();
            label12 = new Label();
            numericNewFillAlpha = new NumericUpDown();
            itemNewImage = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericNewLineWeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).BeginInit();
            panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOverlay).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesFillAlpha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineAlpha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineWeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericNewLineAlpha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericNewFillAlpha).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(955, 24);
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
            itemLoadFromFile.Text = "&Load";
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
            panelButtons.Controls.Add(buttonArrow);
            panelButtons.Controls.Add(buttonLine);
            panelButtons.Controls.Add(button2);
            panelButtons.Controls.Add(button1);
            panelButtons.Location = new Point(0, 56);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(72, 403);
            panelButtons.TabIndex = 1;
            // 
            // buttonArrow
            // 
            buttonArrow.Location = new Point(3, 103);
            buttonArrow.Name = "buttonArrow";
            buttonArrow.Size = new Size(66, 23);
            buttonArrow.TabIndex = 4;
            buttonArrow.Text = "Arrow";
            buttonArrow.UseVisualStyleBackColor = true;
            buttonArrow.Click += ButtonArrow_Click;
            // 
            // buttonLine
            // 
            buttonLine.Location = new Point(3, 74);
            buttonLine.Name = "buttonLine";
            buttonLine.Size = new Size(66, 23);
            buttonLine.TabIndex = 2;
            buttonLine.Text = "Line";
            buttonLine.UseVisualStyleBackColor = true;
            buttonLine.Click += ButtonLine_Click;
            // 
            // button2
            // 
            button2.Location = new Point(3, 45);
            button2.Name = "button2";
            button2.Size = new Size(66, 23);
            button2.TabIndex = 1;
            button2.Text = "Circle";
            button2.UseVisualStyleBackColor = true;
            button2.Click += ButtonCircle_Click;
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(66, 36);
            button1.TabIndex = 0;
            button1.Text = "Rectangle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ButtonRectangle_Click;
            // 
            // numericNewLineWeight
            // 
            numericNewLineWeight.Location = new Point(330, 27);
            numericNewLineWeight.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericNewLineWeight.Name = "numericNewLineWeight";
            numericNewLineWeight.Size = new Size(66, 23);
            numericNewLineWeight.TabIndex = 3;
            numericNewLineWeight.Value = new decimal(new int[] { 2, 0, 0, 0 });
            numericNewLineWeight.ValueChanged += NumericNewLineWeight_ValueChanged;
            // 
            // pictureBoxOriginal
            // 
            pictureBoxOriginal.Location = new Point(0, 0);
            pictureBoxOriginal.Name = "pictureBoxOriginal";
            pictureBoxOriginal.Size = new Size(600, 411);
            pictureBoxOriginal.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxOriginal.TabIndex = 2;
            pictureBoxOriginal.TabStop = false;
            pictureBoxOriginal.LoadCompleted += PictureBoxOriginal_LoadCompleted;
            // 
            // panelImage
            // 
            panelImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelImage.AutoScroll = true;
            panelImage.BackColor = SystemColors.ControlDark;
            panelImage.Controls.Add(pictureBoxOverlay);
            panelImage.Controls.Add(pictureBoxOriginal);
            panelImage.Location = new Point(72, 56);
            panelImage.Name = "panelImage";
            panelImage.Size = new Size(728, 576);
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
            pictureBoxOverlay.MouseDown += PictureBoxOverlay_MouseDown;
            pictureBoxOverlay.MouseLeave += PictureBoxOverlay_MouseLeave;
            pictureBoxOverlay.MouseMove += PictureBoxOverlay_MouseMove;
            pictureBoxOverlay.MouseUp += PictureBoxOverlay_MouseUp;
            // 
            // listViewSymbols
            // 
            listViewSymbols.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            listViewSymbols.Columns.AddRange(new ColumnHeader[] { columnHeaderType, columnHeaderX, columnHeaderY, columnHeaderColor });
            listViewSymbols.Location = new Point(806, 56);
            listViewSymbols.MultiSelect = false;
            listViewSymbols.Name = "listViewSymbols";
            listViewSymbols.Size = new Size(148, 319);
            listViewSymbols.TabIndex = 0;
            listViewSymbols.UseCompatibleStateImageBehavior = false;
            listViewSymbols.View = View.Details;
            listViewSymbols.SelectedIndexChanged += ListViewSymbols_SelectedIndexChanged;
            // 
            // columnHeaderType
            // 
            columnHeaderType.Text = "Symbol";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.Controls.Add(label14);
            panel1.Controls.Add(numericPropertiesFillAlpha);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(numericPropertiesLineAlpha);
            panel1.Controls.Add(numericPropertiesLineWeight);
            panel1.Controls.Add(buttonPropertiesColorFill);
            panel1.Controls.Add(buttonPropertiesColorLine);
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
            panel1.Location = new Point(806, 381);
            panel1.Name = "panel1";
            panel1.Size = new Size(148, 251);
            panel1.TabIndex = 4;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(4, 179);
            label14.Name = "label14";
            label14.Size = new Size(56, 15);
            label14.TabIndex = 25;
            label14.Text = "Fill Alpha";
            // 
            // numericPropertiesFillAlpha
            // 
            numericPropertiesFillAlpha.Location = new Point(95, 175);
            numericPropertiesFillAlpha.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericPropertiesFillAlpha.Name = "numericPropertiesFillAlpha";
            numericPropertiesFillAlpha.Size = new Size(44, 23);
            numericPropertiesFillAlpha.TabIndex = 24;
            numericPropertiesFillAlpha.Value = new decimal(new int[] { 255, 0, 0, 0 });
            numericPropertiesFillAlpha.ValueChanged += Numeric_ValueChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(3, 104);
            label13.Name = "label13";
            label13.Size = new Size(63, 15);
            label13.TabIndex = 23;
            label13.Text = "Line Alpha";
            // 
            // numericPropertiesLineAlpha
            // 
            numericPropertiesLineAlpha.Location = new Point(94, 100);
            numericPropertiesLineAlpha.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericPropertiesLineAlpha.Name = "numericPropertiesLineAlpha";
            numericPropertiesLineAlpha.Size = new Size(44, 23);
            numericPropertiesLineAlpha.TabIndex = 22;
            numericPropertiesLineAlpha.Value = new decimal(new int[] { 255, 0, 0, 0 });
            numericPropertiesLineAlpha.ValueChanged += Numeric_ValueChanged;
            // 
            // numericPropertiesLineWeight
            // 
            numericPropertiesLineWeight.Location = new Point(94, 125);
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
            buttonPropertiesColorFill.Location = new Point(95, 154);
            buttonPropertiesColorFill.Name = "buttonPropertiesColorFill";
            buttonPropertiesColorFill.Size = new Size(43, 19);
            buttonPropertiesColorFill.TabIndex = 14;
            buttonPropertiesColorFill.Tag = "FillColor";
            buttonPropertiesColorFill.UseVisualStyleBackColor = false;
            buttonPropertiesColorFill.Click += ColorChangeClick;
            // 
            // buttonPropertiesColorLine
            // 
            buttonPropertiesColorLine.BackColor = Color.FromArgb(0, 192, 0);
            buttonPropertiesColorLine.FlatStyle = FlatStyle.Flat;
            buttonPropertiesColorLine.Location = new Point(94, 79);
            buttonPropertiesColorLine.Name = "buttonPropertiesColorLine";
            buttonPropertiesColorLine.Size = new Size(43, 19);
            buttonPropertiesColorLine.TabIndex = 13;
            buttonPropertiesColorLine.Tag = "LineColor";
            buttonPropertiesColorLine.UseVisualStyleBackColor = false;
            buttonPropertiesColorLine.Click += ColorChangeClick;
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
            numericX.Name = "numericX";
            numericX.Size = new Size(43, 23);
            numericX.TabIndex = 9;
            numericX.Tag = "X";
            numericX.ValueChanged += Numeric_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 129);
            label8.Name = "label8";
            label8.Size = new Size(70, 15);
            label8.TabIndex = 8;
            label8.Text = "Line Weigth";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 156);
            label7.Name = "label7";
            label7.Size = new Size(54, 15);
            label7.TabIndex = 7;
            label7.Text = "Fill Color";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 81);
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
            buttonDeleteSymbol.Location = new Point(3, 216);
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
            buttonNewColorFill.BackColor = Color.FromArgb(192, 255, 255);
            buttonNewColorFill.FlatStyle = FlatStyle.Flat;
            buttonNewColorFill.Location = new Point(183, 27);
            buttonNewColorFill.Name = "buttonNewColorFill";
            buttonNewColorFill.Size = new Size(43, 19);
            buttonNewColorFill.TabIndex = 18;
            buttonNewColorFill.Tag = "FillColor";
            buttonNewColorFill.UseVisualStyleBackColor = false;
            buttonNewColorFill.Click += NewColorFill_Click;
            // 
            // buttonNewColorLine
            // 
            buttonNewColorLine.BackColor = Color.Blue;
            buttonNewColorLine.FlatStyle = FlatStyle.Flat;
            buttonNewColorLine.Location = new Point(70, 27);
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
            label1.Location = new Point(123, 29);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 16;
            label1.Text = "Fill Color";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(3, 29);
            label9.Name = "label9";
            label9.Size = new Size(61, 15);
            label9.TabIndex = 15;
            label9.Text = "Line Color";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(254, 29);
            label10.Name = "label10";
            label10.Size = new Size(70, 15);
            label10.TabIndex = 19;
            label10.Text = "Line Weight";
            // 
            // numericNewLineAlpha
            // 
            numericNewLineAlpha.Location = new Point(485, 27);
            numericNewLineAlpha.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericNewLineAlpha.Name = "numericNewLineAlpha";
            numericNewLineAlpha.Size = new Size(44, 23);
            numericNewLineAlpha.TabIndex = 20;
            numericNewLineAlpha.Value = new decimal(new int[] { 255, 0, 0, 0 });
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(416, 29);
            label11.Name = "label11";
            label11.Size = new Size(63, 15);
            label11.TabIndex = 21;
            label11.Text = "Line Alpha";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(544, 31);
            label12.Name = "label12";
            label12.Size = new Size(56, 15);
            label12.TabIndex = 23;
            label12.Text = "Fill Alpha";
            // 
            // numericNewFillAlpha
            // 
            numericNewFillAlpha.Location = new Point(606, 29);
            numericNewFillAlpha.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericNewFillAlpha.Name = "numericNewFillAlpha";
            numericNewFillAlpha.Size = new Size(44, 23);
            numericNewFillAlpha.TabIndex = 22;
            numericNewFillAlpha.Value = new decimal(new int[] { 255, 0, 0, 0 });
            // 
            // itemNewImage
            // 
            itemNewImage.Name = "itemNewImage";
            itemNewImage.Size = new Size(184, 22);
            itemNewImage.Text = "&New";
            itemNewImage.Click += ItemNewImage_Click;
            // 
            // ScreenshotEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(955, 632);
            Controls.Add(label12);
            Controls.Add(numericNewFillAlpha);
            Controls.Add(label11);
            Controls.Add(numericNewLineAlpha);
            Controls.Add(buttonNewColorFill);
            Controls.Add(label10);
            Controls.Add(label1);
            Controls.Add(buttonNewColorLine);
            Controls.Add(panel1);
            Controls.Add(listViewSymbols);
            Controls.Add(label9);
            Controls.Add(panelImage);
            Controls.Add(panelButtons);
            Controls.Add(numericNewLineWeight);
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
            ((System.ComponentModel.ISupportInitialize)numericPropertiesFillAlpha).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineAlpha).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPropertiesLineWeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericNewLineAlpha).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericNewFillAlpha).EndInit();
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
    }
}