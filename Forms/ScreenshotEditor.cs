﻿using ScreenShotTool.Classes;
using ScreenShotTool.Properties;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace ScreenShotTool.Forms;

[SupportedOSPlatform("windows")]
public partial class ScreenshotEditor : Form
{

    #region Constructor ---------------------------------------------------------------------------------
    readonly EditorCanvas editorCanvas;
    public readonly static int maxFontSize = 200;
    public readonly static int minimumFontSize = 5;
    public readonly static int startingFontSize = 10;
    readonly List<Button> toolButtons = [];
    public SharedBitmap copiedBitmap = new();

    public ScreenshotEditor()
    {
        InitializeComponent();
        editorCanvas = new EditorCanvas(this, pictureBoxOverlay);
        SetupEditor();

        editorCanvas.CreateNewImage(640, 480, Color.White);
    }

    public ScreenshotEditor(string file)
    {
        InitializeComponent();
        editorCanvas = new EditorCanvas(this, pictureBoxOverlay);
        SetupEditor();

        editorCanvas.LoadImageFromFile(file);
    }

    public ScreenshotEditor(bool fromClipboard)
    {
        InitializeComponent();
        editorCanvas = new EditorCanvas(this, pictureBoxOverlay);
        SetupEditor();

        if (fromClipboard)
        {
            editorCanvas.LoadImageFromClipboard();
        }
    }

    public ScreenshotEditor(Image loadImage)
    {
        InitializeComponent();
        editorCanvas = new EditorCanvas(this, pictureBoxOverlay);
        SetupEditor();
        editorCanvas.LoadImageFromImage(loadImage, true);
    }

    private void SetupEditor()
    {
        if (Settings.Default.PreventDpiRescale)
        {
            AutoScaleMode = AutoScaleMode.None;
        }
        Font = new Font(Font.FontFamily, 9);

        FillFontFamilyBox();
        numericPropertiesFontSize.Maximum = maxFontSize;
        numericPropertiesFontSize.Minimum = minimumFontSize;
        numericPropertiesFontSize.Value = startingFontSize;
        DisableAllPanels();
        numericBlurMosaicSize.Value = Settings.Default.BlurMosaicSize;
        timerAfterLoad.Start(); // turns off TopMost shortly after Load. Without TopMost the Window opens behind other forms (why? who can say)

        toolButtons.Add(buttonSelect);
        toolButtons.Add(buttonRectangle);
        toolButtons.Add(buttonCircle);
        toolButtons.Add(buttonLine);
        toolButtons.Add(buttonArrow);
        toolButtons.Add(buttonText);
        toolButtons.Add(buttonBorder);
        toolButtons.Add(buttonBlur);
        toolButtons.Add(buttonHighlight);
        toolButtons.Add(buttonCrop);
        toolButtons.Add(buttonNumbered);
        toolButtons.Add(buttonDraw);
        toolButtons.Add(buttonFilledCurve);

        listViewSymbols.Height = 200;

        ColorTools.SetButtonColors(buttonNewColorFill, Settings.Default.NewSymbolFillColor, "X");
        ColorTools.SetButtonColors(buttonNewColorLine, Settings.Default.NewSymbolLineColor, "X");
        numericNewLineWeight.Value = Settings.Default.NewSymbolLineWeight;

        UpdatePropertiesPanel();
        this.pictureBoxOverlay.MouseWheel += PictureBoxOverlay_MouseWheel;
    }

    public void UpdateNumericLimits()
    {
        numericPropertiesX.Minimum = -editorCanvas.OutOfBoundsMaxPixels;
        numericPropertiesX.Maximum = editorCanvas.CanvasSize.Width + editorCanvas.OutOfBoundsMaxPixels;
        numericPropertiesY.Minimum = -editorCanvas.OutOfBoundsMaxPixels;
        numericPropertiesY.Maximum = editorCanvas.CanvasSize.Height + editorCanvas.OutOfBoundsMaxPixels;
    }
    #endregion

    #region Form events ---------------------------------------------------------------------------------

    private void TimerAfterLoad_Tick(object sender, EventArgs e)
    {
        timerAfterLoad.Stop();
        this.TopMost = false;
    }

    private void TimerUpdateOverlay_Tick(object sender, EventArgs e)
    {
        if (editorCanvas.dragStarted)
        {
            //UpdateOverlay();
        }
    }

    private void ScreenshotEditor_Deactivate(object sender, EventArgs e)
    {
        editorCanvas.dragStarted = false;
    }

    #endregion

    #region Menu items ----------------------------------------------------------------------------------

    private void ItemExit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenFileAction();
    }

    private void OpenFileAction()
    {
        FileDialog fileDialog = new OpenFileDialog
        {
            Filter = ImageOutput.FilterLoadImage
        };
        DialogResult result = fileDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            editorCanvas.LoadImageFromFile(fileDialog.FileName);
        }
    }

    private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveFileAction();
    }


    private void SaveFileAction()
    {
        Bitmap? outImage = editorCanvas.AssembleImageForSaveOrCopy();
        if (outImage != null)
        {
            ImageOutput.SaveWithDialog(outImage, ImageOutput.FilterSaveImage);
        }
    }

    private void DeleteOverlayElementsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        editorCanvas.DeleteAllSymbols();
    }

    public void DeleteListViewSymbols()
    {
        foreach (ListViewItem item in listViewSymbols.Items)
        {
            if (item.Tag is GraphicSymbol gs)
            {
                gs.DisposeImages();
            }
        }
        listViewSymbols.Items.Clear();
    }

    private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
    {
        CopyToClipboard();
    }

    private void ItemLoadFromClipboard_Click(object sender, EventArgs e)
    {
        editorCanvas.LoadImageFromClipboard();
    }

    private void PasteIntoThisImage_Click(object sender, EventArgs e)
    {
        PasteIntoImage();
    }

    private void ItemPasteFromFile(object sender, EventArgs e)
    {
        Point location = MousePositionLocal;
        bool badLocation = false;
        if (location.X < 0 || location.X > editorCanvas.CanvasRect.Width - 10)
        {
            badLocation = true;
        }
        if (location.Y < 0 || location.Y > editorCanvas.CanvasRect.Height - 10)
        {
            badLocation = true;
        }
        if (badLocation)
        {
            location = new Point(10, 10);
        }
        InsertImageFromFileDialog(location);
    }

    private bool InsertImageFromFileDialog(Point location, string Folder = "")
    {
        bool InsertSuccessful;

        FileDialog fileDialog = new OpenFileDialog
        {
            Filter = ImageOutput.FilterLoadImage
        };
        if (Folder.Length > 0 && Directory.Exists(Folder))
        {
            Debug.WriteLine($"Setting InitialDirectory {Folder}");
            fileDialog.InitialDirectory = Folder;
        }
        else
        {
            Debug.WriteLine($"Folder {Folder}, length {Folder.Length}, exists: {Directory.Exists(Folder)}");
        }
        DialogResult result = fileDialog.ShowDialog();
        string fileName = fileDialog.FileName;
        fileDialog.Dispose();

        if (result != DialogResult.OK)
        {
            return false;
        }

        InsertSuccessful = InsertImageFromFile(location, fileName);

        return InsertSuccessful;
    }

    private bool InsertImageFromFile(Point location, string fileName, bool center = false)
    {
        Image? loadedImage;
        try
        {

            Image? tempImage = null;
            using (FileStream stream = new(fileName, FileMode.Open))
            {
                tempImage = Image.FromStream(stream);
            }

            loadedImage = ImageProcessing.ImageToBitmap32bppArgb(tempImage, true);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Couldn't load file.\n" + ex.Message);
            return false;
        }


        if (loadedImage != null)
        {
            GsImage gsImage = GsImage.Create(location, (Bitmap)loadedImage);//GsImage.Create(location, copiedBitmap, true);
            if (center)
            {
                gsImage.Left -= gsImage.Width / 2;
                gsImage.Top -= gsImage.Height / 2;
            }
            if (gsImage.ValidSymbol)
            {
                AddNewSymbolToList(gsImage, -1, fileName);
            }
            else
            {
                gsImage.Dispose();
                return false;
            }
        }
        editorCanvas.UpdateOverlay();
        return true;
    }

    private void ItemNewImage_Click(object sender, EventArgs e)
    {
        NewImagePrompt imagePrompt = new();
        DialogResult result = imagePrompt.ShowDialog();
        if (result == DialogResult.OK)
        {
            editorCanvas.CreateNewImage(imagePrompt.ImageWidth, imagePrompt.ImageHeight, imagePrompt.BackgroundColor);
        }
        imagePrompt.Dispose();
    }

    private void ToolStripMenuItemPrint_Click(object sender, EventArgs e)
    {
        PrintImage();
    }

    private void PrintImage()
    {
        Bitmap? outImage = editorCanvas.AssembleImageForSaveOrCopy();
        if (outImage != null)
        {
            Print print = new();
            PrintDialog printDialog = new(print, outImage);
            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                print.PrintImage(outImage);
            }
            print.Dispose();
        }
    }

    HelpForm? helpWindow;
    private void Help_Click(object sender, EventArgs e)
    {
        helpWindow ??= new HelpForm();
        if (helpWindow.IsDisposed)
        {
            helpWindow = new HelpForm();
        }
        helpWindow.Show();
        helpWindow.WindowState = FormWindowState.Normal;

        helpWindow.ScrollToText("Screenshot Editor");

    }

    private void Documentation_Click(object sender, EventArgs e)
    {
        MainForm.OpenDocumentation();
    }

    private void Website_Click(object sender, EventArgs e)
    {
        MainForm.OpenWebsite();
    }

    private void About_Click(object sender, EventArgs e)
    {
        MainForm.OpenAbout();
    }

    #endregion

    #region User Actions --------------------------------------------------------------------------------

    public enum UserActions
    {
        None,
        Select,
        MoveSymbol,
        ScaleSymbol,
        CreateRectangle,
        CreateCircle,
        CreateLine,
        CreateArrow,
        CreateText,
        CreateImage,
        CreateBlur,
        CreateHighlight,
        CreateCrop,
        CreateNumbered,
        DrawFreehand,
        DrawFilledCurve,
    }

    public UserActions selectedUserAction = UserActions.Select;
    public void SetUserAction(UserActions action)
    {
        Color buttonSelectedColor = Color.PaleTurquoise;
        selectedUserAction = action;
        foreach (Button b in toolButtons)
        {
            b.BackColor = Color.Transparent;
        }
        if (selectedUserAction == UserActions.Select) buttonSelect.BackColor = buttonSelectedColor;
        if (selectedUserAction == UserActions.CreateRectangle) buttonRectangle.BackColor = buttonSelectedColor;
        if (selectedUserAction == UserActions.CreateCircle) buttonCircle.BackColor = buttonSelectedColor;
        if (selectedUserAction == UserActions.CreateLine) buttonLine.BackColor = buttonSelectedColor;
        if (selectedUserAction == UserActions.CreateArrow) buttonArrow.BackColor = buttonSelectedColor;
        if (selectedUserAction == UserActions.CreateText) buttonText.BackColor = buttonSelectedColor;
        if (selectedUserAction == UserActions.CreateBlur) buttonBlur.BackColor = buttonSelectedColor;
        if (selectedUserAction == UserActions.CreateHighlight) buttonHighlight.BackColor = buttonSelectedColor;
        if (selectedUserAction == UserActions.CreateCrop) buttonCrop.BackColor = buttonSelectedColor;
        if (selectedUserAction == UserActions.CreateNumbered) buttonNumbered.BackColor = buttonSelectedColor;
        if (selectedUserAction == UserActions.DrawFreehand) buttonDraw.BackColor = buttonSelectedColor;
        if (selectedUserAction == UserActions.DrawFilledCurve) buttonFilledCurve.BackColor = buttonSelectedColor;
        // UserActions.CreateBorder is not in list since it happens right away without mouse drag

        //if (selectedUserAction != UserActions.MoveSymbol && selectedUserAction != UserActions.ScaleSymbol)
        if (selectedUserAction == UserActions.Select || selectedUserAction == UserActions.None)
        {
            pictureBoxOverlay.Cursor = Cursors.Arrow;
        }
        else if (selectedUserAction >= UserActions.CreateRectangle)
        {
            pictureBoxOverlay.Cursor = Cursors.Cross;
        }
        editorCanvas.UpdateOverlay();
    }
    #endregion

    #region Mouse events --------------------------------------------------------------------------------

    Point MousePositionLocal = Point.Empty;
    private void PictureBoxOverlay_MouseDown(object sender, MouseEventArgs e)
    {
        MousePositionLocal = new Point(e.X, e.Y);
        //Debug.WriteLine($"MouseDown {e.Button}");
        pictureBoxOverlay.Focus();
        if (e.Button == MouseButtons.Left)
        {
            editorCanvas.MouseDown(MousePositionLocal);
        }
        else if (e.Button == MouseButtons.Right)
        {
            CancelAction();
        }
    }


    private void PictureBoxOverlay_MouseMove(object sender, MouseEventArgs e)
    {
        MousePositionLocal = new Point(e.X, e.Y);
        //bool shiftHeld = ModifierKeys == Keys.Shift;
        editorCanvas.MouseMove(MousePositionLocal);
    }
    private void PictureBoxOverlay_MouseUp(object sender, MouseEventArgs e)
    {
        MousePositionLocal = new Point(e.X, e.Y);
        editorCanvas.MouseUp(MousePositionLocal);
    }

    private void PictureBoxOverlay_MouseWheel(object? sender, MouseEventArgs e)
    {
        Debug.WriteLine($"Mousewheel: {e.Delta}");
        if (editorCanvas.dragStarted && e.Delta != 0)
        {
            int change = e.Delta > 0 ? 1 : -1;
            SetNumericClamp(numericNewLineWeight, (int)numericNewLineWeight.Value + change);
        }
    }
    #endregion

    #region Key input -----------------------------------------------------------------------------------

    public static bool GetShift()
    {
        return ModifierKeys == Keys.Shift;
    }

    public static bool GetControl()
    {
        return ModifierKeys == Keys.Control;
    }

    private void ScreenshotEditor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.G && e.Modifiers == Keys.Control)
        {
            GC.Collect(); // Garbage collection for testing purposes to identify memory leaks
            GC.WaitForPendingFinalizers();
        }

        if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
        {
            if (Clipboard.ContainsImage())
            {
                PasteIntoImage();
            }
            else if (Clipboard.ContainsText())
            {
                PasteTextIntoImage();
            }
        }
        if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
        {
            if (GetSelectedSymbolFirst() is GsCrop gsC)
            {
                CopySelectionToClipboard(gsC);
                DeleteSelectedSymbol(gsC);
            }
            else
            {
                CopyToClipboard();
            }
        }
        if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
        {
            SaveFileAction();
        }
        if (e.KeyCode == Keys.O && e.Modifiers == Keys.Control)
        {
            OpenFileAction();
        }
        if (e.KeyCode == Keys.P && e.Modifiers == Keys.Control)
        {
            PrintImage();
        }
        if (e.KeyCode == Keys.Escape)
        {
            CancelAction();
            if (GetSelectedSymbolFirst() is GsCrop gsC)
            {
                DeleteSelectedSymbol(gsC);
            }
        }

        // Only do these if there's a selected symbol, AND focus is on the canvas. Not used if the list is focused, since arrow keys etc. alters the selection
        if (pictureBoxOverlay.Focused)
        {
            int inputMultiplied = GetShift() ? 10 : 1;
            if (e.KeyCode == Keys.Home)
            {
                if (listViewSymbols.SelectedItems.Count > 0)
                {
                    MoveSymbolToFront(listViewSymbols.SelectedItems[0]);
                    editorCanvas.UpdateOverlay();
                }

            }
            if (e.KeyCode == Keys.End)
            {
                if (listViewSymbols.SelectedItems.Count > 0)
                {
                    MoveSymbolToBack(listViewSymbols.SelectedItems[0]);
                    editorCanvas.UpdateOverlay();
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                MoveSelectedSymbols(-1 * inputMultiplied, 0);
            }
            if (e.KeyCode == Keys.Right)
            {
                MoveSelectedSymbols(1 * inputMultiplied, 0);
            }
            if (e.KeyCode == Keys.Up)
            {
                MoveSelectedSymbols(0, -1 * inputMultiplied);
            }
            if (e.KeyCode == Keys.Down)
            {
                MoveSelectedSymbols(0, 1 * inputMultiplied);
            }
        }

        // Only do these if there's a selected symbol, AND focus is on the canvas or symbol list
        if (pictureBoxOverlay.Focused || listViewSymbols.Focused)
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    DeleteSelectedSymbols();
                }
                if (e.KeyCode == Keys.Enter) // Confirm default action on selected symbol
                {
                    if (GetSelectedSymbolFirst() is GsCrop crop && editorCanvas.SourceImage != null)
                    {
                        CropImageAction(editorCanvas.SourceImage, crop);
                    }
                }
            }
        }
        //e.Handled = true;
        //e.SuppressKeyPress = true;
    }

    private void CancelAction()
    {
        editorCanvas.dragStarted = false;
        editorCanvas.dragMoved = false;
        //if (deselectSymbol) editorCanvas.currentSelectedSymbol = null;
        SetUserAction(UserActions.Select);
    }
    #endregion

    #region Copy and Paste ------------------------------------------------------------------------------

    public void CopyToClipboard()
    {
        Bitmap? result = editorCanvas.AssembleImageForSaveOrCopy();
        if (result != null)
        {
            //copiedBitmap.DisposeImage();
            copiedBitmap.SetImage(ImageProcessing.CopyImage(result)); // disposes and refills
            Debug.WriteLine($"Set copiedImage to result {copiedBitmap.Width}");
            Clipboard.SetImage(result);
            result.Dispose();
        }
    }

    private void PasteIntoImage()
    {
        GsImage gsImage = GsImage.Create(GetPastePosition(), copiedBitmap, true);
        if (gsImage.ValidSymbol)
        {
            AddNewSymbolToList(gsImage);
        }
        else
        {
            gsImage.Dispose();
        }

        editorCanvas.UpdateOverlay();
    }

    private void PasteTextIntoImage()
    {
        AddNewSymbolToList(GsText.Create(GetPastePosition(), buttonNewColorLine.BackColor, Clipboard.GetText(), 10));
    }

    private Point GetPastePosition()
    {
        Point location = MousePositionLocal;
        bool badLocation = false;
        if (location.X < 0 || location.X > editorCanvas.CanvasRect.Width - 10)
        {
            badLocation = true;
        }
        if (location.Y < 0 || location.Y > editorCanvas.CanvasRect.Height - 10)
        {
            badLocation = true;
        }
        if (badLocation)
        {
            location = new Point(10, 10);
        }

        return location;
    }

    #endregion

    #region Symbol Properties panel ---------------------------------------------------------------------

    public void AddNewSymbolToList(GraphicSymbol symbol, int index = -1, string name = "")
    {
        if (symbol != null)
        {
            ListViewItem newItem;
            if (index == -1)
            {
                //editorCanvas.symbols.Add(symbol);
                newItem = listViewSymbols.Items.Add(symbol.Name);
            }
            else
            {
                //editorCanvas.symbols.Insert(index, symbol);
                newItem = listViewSymbols.Items.Insert(index, symbol.Name);
            }

            newItem.Text = symbol.Name;
            newItem.Tag = symbol;
            symbol.ListViewItem = newItem;
            if (name.Length > 0)
            {
                symbol.Name = Path.GetFileNameWithoutExtension(name);
                newItem.Text = Path.GetFileNameWithoutExtension(name);
            }

            listViewSymbols.Update();
            if (listViewSymbols.Items.Count > 0 && selectedUserAction != UserActions.DrawFreehand)
            {
                listViewSymbols.SelectedItems.Clear();
                listViewSymbols.Items[^1].Selected = true;
                //listViewSymbols.Items[^1].Focused = true;
                //listViewSymbols.Items[^1].EnsureVisible();
                //listViewSymbols.Select();
            }
        }
    }

    private void ListViewSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listViewSymbols.SelectedItems.Count > 0)
        {
            bool allowSelect = true;
            if (selectedUserAction == UserActions.DrawFreehand)
            {
                if (Settings.Default.SelectAfterFreehand == false)
                {
                    allowSelect = false;
                }
            }
            if (Settings.Default.SelectAfterPlacingSymbol && allowSelect) // special exception for freehand draw multiple lines
            {
                SetUserAction(UserActions.Select);
            }
            else
            {
                if (selectedUserAction < UserActions.CreateRectangle)
                {
                    SetUserAction(UserActions.Select);
                }
            }
        }
        else
        {
            if (selectedUserAction == UserActions.MoveSymbol)
            {
                SetUserAction(UserActions.Select);
            }
        }
        UpdatePropertiesPanel();
    }

    private static void EnablePanel(Panel panel, int left, ref int top)
    {
        panel.Enabled = true;
        panel.Visible = true;
        panel.Location = new Point(left, top);
        top += panel.Height + 3;
    }

    private static void DisablePanel(Panel panel)
    {
        panel.Enabled = false;
        panel.Visible = false;
    }

    private void DisableAllPanels()
    {
        panelPropertiesPosition.Location = new Point(listViewSymbols.Left, listViewSymbols.Bottom + 5);
        panelPropertiesPosition.Visible = true;
        panelPropertiesPosition.Enabled = false;
        DisablePanel(panelPropertiesFill);
        DisablePanel(panelPropertiesLine);
        DisablePanel(panelPropertiesText);
        DisablePanel(panelPropertiesHighlight);
        DisablePanel(panelPropertiesShadow);
        DisablePanel(panelPropertiesDelete);
        DisablePanel(panelPropertiesCrop);
        DisablePanel(panelPropertiesBlur);
        DisablePanel(panelPropertiesPolygon);
        DisablePanel(panelPropertiesImage);
    }

    private static void SetNumericClamp(NumericUpDown numericUpDown, decimal value)
    {
        numericUpDown.Value = Math.Clamp(value, numericUpDown.Minimum, numericUpDown.Maximum);
        if (value < numericUpDown.Minimum) Debug.WriteLine($"Value is below {numericUpDown.Name} Minimum");
        if (value > numericUpDown.Maximum) Debug.WriteLine($"Value is above {numericUpDown.Name} Maximum");
    }

    public void UpdatePropertiesPanel()
    {
        int panelLeft = listViewSymbols.Left;
        int lastPanelBottom = listViewSymbols.Bottom + 3;
        if (listViewSymbols.SelectedItems.Count > 0)
        {
            ListViewItem item = listViewSymbols.SelectedItems[0];
            if (item.Tag is GraphicSymbol graphicSymbol)
            {
                DisableAllPanels();
                EnablePanel(panelPropertiesPosition, panelLeft, ref lastPanelBottom);

                numericPropertiesLineWeight.Enabled = true;
                numericPropertiesWidth.Enabled = true;
                numericPropertiesHeight.Enabled = true;
                buttonPropertiesColorLine.Enabled = true;
                //numericPropertiesLineAlpha.Enabled = true;

                labelSymbolType.Text = "Symbol: " + graphicSymbol.Name;
                SetNumericClamp(numericPropertiesX, graphicSymbol.Left);
                SetNumericClamp(numericPropertiesY, graphicSymbol.Top);
                SetNumericClamp(numericPropertiesWidth, graphicSymbol.Width);
                SetNumericClamp(numericPropertiesHeight, graphicSymbol.Height);
                ColorTools.SetButtonColors(buttonPropertiesColorLine, graphicSymbol.LineColor, "X");
                ColorTools.SetButtonColors(buttonPropertiesColorFill, graphicSymbol.FillColor, "X");
                //buttonPropertiesColorLine.BackColor = graphicSymbol.LineColor;
                //buttonPropertiesColorFill.BackColor = graphicSymbol.FillColor;
                numericPropertiesLineWeight.Value = graphicSymbol.LineWeight;
                checkBoxPropertiesShadow.Checked = graphicSymbol.ShadowEnabled;
                buttonDeleteSymbol.Tag = graphicSymbol;

                if (graphicSymbol is GsText gsText)
                {
                    EnablePanel(panelPropertiesLine, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesText, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesShadow, panelLeft, ref lastPanelBottom);

                    numericPropertiesWidth.Enabled = false;
                    numericPropertiesHeight.Enabled = false;
                    numericPropertiesLineWeight.Enabled = false;

                    textBoxSymbolText.Text = gsText.Text;
                    if (gsText.ListViewItem != null)
                    {
                        string displayText = gsText.Text[..Math.Min(gsText.Text.Length, 20)];
                        gsText.ListViewItem.Text = "Text: " + displayText;
                    }
                    numericPropertiesFontSize.Value = (int)Math.Clamp(gsText.fontEmSize, minimumFontSize, maxFontSize);
                    checkBoxFontBold.Checked = (gsText.fontStyle & FontStyle.Bold) != 0;
                    checkBoxFontItalic.Checked = (gsText.fontStyle & FontStyle.Italic) != 0;
                    checkBoxStrikeout.Checked = (gsText.fontStyle & FontStyle.Strikeout) != 0;
                    checkBoxUnderline.Checked = (gsText.fontStyle & FontStyle.Underline) != 0;
                }
                else if (graphicSymbol is GsHighlight gsHL)
                {
                    EnablePanel(panelPropertiesFill, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesHighlight, panelLeft, ref lastPanelBottom);
                    comboBoxBlendMode.Text = gsHL.blendMode.ToString();
                }
                else if (graphicSymbol is GsBlur)
                {
                    EnablePanel(panelPropertiesBlur, panelLeft, ref lastPanelBottom);
                }
                else if (graphicSymbol is GsCrop)
                {
                    EnablePanel(panelPropertiesCrop, panelLeft, ref lastPanelBottom);
                }
                else if (graphicSymbol is GsNumbered gsNumbered)
                {
                    EnablePanel(panelPropertiesFill, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesLine, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesShadow, panelLeft, ref lastPanelBottom);
                    numericPropertiesHeight.Enabled = false;
                    if (gsNumbered.ListViewItem != null)
                    {
                        gsNumbered.ListViewItem.Text = "Number: " + gsNumbered.Number;
                    }
                }
                else if (graphicSymbol is GsImage gsI)
                {
                    EnablePanel(panelPropertiesImage, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesShadow, panelLeft, ref lastPanelBottom);
                    SetNumericClamp(numericPropertiesRotation, (decimal)gsI.Rotation % 360);
                }
                else if (graphicSymbol is GsPolygon gsP)
                {
                    EnablePanel(panelPropertiesFill, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesLine, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesPolygon, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesShadow, panelLeft, ref lastPanelBottom);
                    checkBoxPropertiesCloseCurve.Checked = gsP.closedCurve;
                    numericPropertiesCurveTension.Value = (decimal)gsP.curveTension;
                }
                else if (graphicSymbol is GsBoundingBox) // must be after all other symbols that inherit from GsBoundingBox
                {
                    EnablePanel(panelPropertiesFill, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesLine, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesShadow, panelLeft, ref lastPanelBottom);
                }
                else if (graphicSymbol is GsLine)
                {
                    EnablePanel(panelPropertiesLine, panelLeft, ref lastPanelBottom);
                    EnablePanel(panelPropertiesShadow, panelLeft, ref lastPanelBottom);
                }


                EnablePanel(panelPropertiesDelete, panelLeft, ref lastPanelBottom);

                if (graphicSymbol is GsText == false)
                {
                    textBoxSymbolText.Text = "";
                }

                //numericPropertiesLineAlpha.Value = graphicSymbol.lineAlpha;
                //numericPropertiesFillAlpha.Value = graphicSymbol.fillAlpha;
            }
        }
        else
        {
            DisableAllPanels();
            ClearPropertyPanelValues();
        }
    }

    private void ButtonDeleteSymbol_Click(object sender, EventArgs e)
    {
        DeleteSelectedSymbols();
    }

    private void DeleteSelectedSymbols()
    {
        List<GraphicSymbol> symbols = GetSelectedSymbols();
        foreach (GraphicSymbol symbol in symbols)
        {
            DeleteSelectedSymbol(symbol);
        }
    }

    private void DeleteSelectedSymbol(GraphicSymbol gs)
    {
        //GraphicSymbol? gs = GetSelectedSymbolFirst();
        if (gs != null)
        {
            if (gs.ListViewItem != null)
            {
                listViewSymbols.Items.Remove(gs.ListViewItem);
            }
            gs.DisposeImages();
            //editorCanvas.symbols.Remove(gs);   
            listViewSymbols.Update();
            ClearPropertyPanelValues();
            editorCanvas.UpdateOverlay();
        }
    }

    private void MoveSymbolToFront(ListViewItem item)
    {
        listViewSymbols.Items.Remove(item);
        listViewSymbols.Items.Add(item);
        listViewSymbols.Update();
        listViewSymbols.Items[^1].Selected = true;
    }

    private void MoveSymbolToBack(ListViewItem item)
    {
        listViewSymbols.Items.Remove(item);
        listViewSymbols.Items.Insert(0, item);
        listViewSymbols.Update();
        listViewSymbols.Items[0].Selected = true;
    }

    private void MoveSelectedSymbols(int X, int Y)
    {
        foreach (ListViewItem item in listViewSymbols.SelectedItems)
        {
            if (item.Tag is GraphicSymbol gs)
            {
                gs.Move(X, Y);
            }
        }
        editorCanvas.UpdateOverlay();
    }

    private ListViewItem? GetSelecteListItem()
    {
        if (listViewSymbols.SelectedItems.Count > 0)
        {
            return listViewSymbols.SelectedItems[0];
        }
        else return null;
    }

    public void ClearPropertyPanelValues()
    {
        labelSymbolType.Text = "Symbol: ";
        numericPropertiesX.Value = 0;
        numericPropertiesY.Value = 0;
        numericPropertiesWidth.Value = 1;
        numericPropertiesHeight.Value = 1;
        ColorTools.SetButtonColors(buttonPropertiesColorLine, Color.Gray, "X");
        ColorTools.SetButtonColors(buttonPropertiesColorFill, Color.Gray, "X");
        //buttonPropertiesColorLine.BackColor = Color.Gray;
        //buttonPropertiesColorFill.BackColor = Color.Gray;
        //numericPropertiesLineAlpha.Value = 255;
        //numericPropertiesFillAlpha.Value = 255;
        numericPropertiesLineWeight.Value = 1;
        numericPropertiesFontSize.Value = 10;
        buttonDeleteSymbol.Tag = null;
    }

    private void Numeric_ValueChanged(object sender, EventArgs e)
    {
        if (listViewSymbols.SelectedItems.Count > 0)
        {
            ListViewItem item = listViewSymbols.SelectedItems[0];
            if (item.Tag is not GraphicSymbol gs) return;

            if (sender == numericPropertiesX)
            {
                gs.Left = (int)numericPropertiesX.Value;
            }
            if (sender == numericPropertiesY)
            {
                gs.Top = (int)numericPropertiesY.Value;
            }
            if (sender == numericPropertiesWidth)
            {
                gs.Width = (int)numericPropertiesWidth.Value;
            }
            if (sender == numericPropertiesHeight)
            {
                gs.Height = (int)numericPropertiesHeight.Value;
            }
            if (sender == numericPropertiesLineWeight)
            {
                gs.LineWeight = (int)numericPropertiesLineWeight.Value;
            }
            //if (sender == numericPropertiesLineAlpha)
            //{
            //    //gs.lineAlpha = (int)numericPropertiesLineAlpha.Value;
            //    gs.UpdateColors();
            //    buttonPropertiesColorLine.BackColor = gs.LineColor;
            //}
            //if (sender == numericPropertiesFillAlpha)
            //{
            //    gs.fillAlpha = (int)numericPropertiesFillAlpha.Value;
            //    gs.UpdateColors();
            //    buttonPropertiesColorFill.BackColor = gs.BackgroundColor;
            //}
        }
        editorCanvas.UpdateOverlay();
    }

    private void ColorChangeClick(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is not GraphicSymbol gs) return;

                ColorDialogAlpha colorDialogAlpha = new(button.BackColor)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
                DialogResult result = colorDialogAlpha.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ColorTools.SetButtonColors(button, colorDialogAlpha.Color, "X");
                    if (sender == buttonPropertiesColorLine)
                    {
                        gs.LineColor = colorDialogAlpha.Color;
                    }
                    if (sender == buttonPropertiesColorFill)
                    {
                        gs.FillColor = colorDialogAlpha.Color;
                    }
                }
                colorDialogAlpha.Dispose();
            }
            editorCanvas.UpdateOverlay();
        }
    }

    private void NumericBlurMosaicSize_ValueChanged(object sender, EventArgs e)
    {
        editorCanvas.MosaicSize = numericBlurMosaicSize.ValueInt();
        if (GetSelectedSymbolFirst() is GsBlur gsB)
        {
            gsB.MosaicSize = numericBlurMosaicSize.ValueInt();
        }
        editorCanvas.UpdateOverlay();
    }

    private void TextBoxSymbolText_TextChanged(object sender, EventArgs e)
    {
        GsText? textSymbol = GetSelectedTextSymbol();
        if (textSymbol != null)
        {
            textSymbol.Text = textBoxSymbolText.Text;
            if (textSymbol.ListViewItem != null)
            {
                textSymbol.ListViewItem.Text = "Text: " + textSymbol.Text;
            }
        }
        editorCanvas.UpdateOverlay();
    }

    private void NumericPropertiesFontSize_ValueChanged(object sender, EventArgs e)
    {
        GsText? textSymbol = GetSelectedTextSymbol();
        if (textSymbol != null)
        {
            textSymbol.fontEmSize = (float)numericPropertiesFontSize.Value;
            textSymbol.UpdateFont();
        }
        editorCanvas.UpdateOverlay();
    }

    private void ComboBoxFontFamily_ValueMemberChanged(object sender, EventArgs e)
    {
        GsText? textSymbol = GetSelectedTextSymbol();
        if (textSymbol != null)
        {
            string selectedFont = comboBoxFontFamily.Text;
            if (fontDictionary.TryGetValue(selectedFont, out FontFamily? value))
            {
                textSymbol.fontFamily = value;
                textSymbol.UpdateFont();
            }
        }
        editorCanvas.UpdateOverlay();
    }

    readonly Dictionary<string, FontFamily> fontDictionary = [];
    private void FillFontFamilyBox()
    {
        List<FontFamily> fontList = [.. FontFamily.Families];
        List<string> fontNames = [];

        foreach (FontFamily font in fontList)
        {
            fontNames.Add(font.Name);
            fontDictionary.Add(font.Name, font);
        }
        comboBoxFontFamily.DataSource = fontNames;
    }

    private void FontStyle_CheckedChanged(object sender, EventArgs e)
    {
        UpdateFontStyle();
    }

    private void UpdateFontStyle()
    {
        bool bold = checkBoxFontBold.Checked;
        bool italic = checkBoxFontItalic.Checked;
        bool strikeout = checkBoxStrikeout.Checked;
        bool underline = checkBoxUnderline.Checked;
        GsText? textSymbol = GetSelectedTextSymbol();
        if (textSymbol != null)
        {
            FontStyle fontStyle = FontStyle.Regular;
            if (bold)
            {
                fontStyle |= FontStyle.Bold;
            }
            if (italic)
            {
                fontStyle |= FontStyle.Italic;
            }
            if (strikeout)
            {
                fontStyle |= FontStyle.Strikeout;
            }
            if (underline)
            {
                fontStyle |= FontStyle.Underline;
            }
            textSymbol.fontStyle = fontStyle;
        }
        editorCanvas.UpdateOverlay();
    }

    private void CheckBoxPropertiesShadow_Click(object sender, EventArgs e)
    {
        GraphicSymbol? symbol = GetSelectedSymbolFirst();
        if (symbol != null)
        {
            symbol.ShadowEnabled = checkBoxPropertiesShadow.Checked;
        }
        editorCanvas.UpdateOverlay();
    }

    private void CheckBoxPropertiesCloseCurve_Click(object sender, EventArgs e)
    {
        GraphicSymbol? symbol = GetSelectedSymbolFirst();
        if (symbol != null)
        {
            if (symbol is GsPolygon gsP)
            {
                gsP.closedCurve = checkBoxPropertiesCloseCurve.Checked;
            }
        }
        editorCanvas.UpdateOverlay();
    }

    public List<GraphicSymbol> GetSelectedSymbols()
    {
        List<GraphicSymbol> symbols = [];
        if (listViewSymbols.SelectedItems.Count > 0)
        {
            var items = listViewSymbols.SelectedItems;

            if (items != null && items.Count > 0)
            {
                List<ListViewItem> listViewItems = items.Cast<ListViewItem>().ToList();

                foreach (var item in listViewItems)
                {
                    if (item.Tag is GraphicSymbol gs)
                    {
                        symbols.Add(gs);
                    }
                }
            }
        }
        return symbols;
    }

    public GraphicSymbol? GetSelectedSymbolFirst()
    {
        List<GraphicSymbol> symbols = GetSelectedSymbols();
        if (symbols.Count > 0)
        {
            return symbols[0];
        }
        else return null;
    }

    private GsText? GetSelectedTextSymbol()
    {
        if (GetSelectedSymbolFirst() is GsText gs) return gs;
        return null;
    }

    private void ComboBoxBlendMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (editorCanvas.CurrentSelectedSymbol is GsHighlight gshl)
        {
            if (Enum.TryParse(comboBoxBlendMode.Text, out ColorBlend.BlendModes newBlend))
            {
                gshl.blendMode = newBlend;
            }
            editorCanvas.UpdateOverlay();
        }
    }

    private void ButtonPropertyCrop_Click(object sender, EventArgs e)
    {
        if (editorCanvas.SourceImage == null) return;
        if (GetSelectedSymbolFirst() is GsCrop gsC)
        {
            CropImageAction(editorCanvas.SourceImage, gsC);
        }
    }

    private void CropImageAction(Bitmap image, GsCrop gsC)
    {
        gsC.showOutline = false;
        Rectangle cropRect = gsC.Bounds;
        Bitmap outImage = ImageProcessing.CropImage(image, cropRect);
        foreach (GraphicSymbol gs in editorCanvas.Symbols)
        {
            gs.MoveTo(gs.Left - cropRect.Left, gs.Top - cropRect.Top);
        }
        editorCanvas.LoadImageFromImage(outImage, false);
        gsC.showOutline = true;
        DeleteSelectedSymbol(gsC);
    }

    private void ButtonPropertyCopyCrop_Click(object sender, EventArgs e)
    {
        if (GetSelectedSymbolFirst() is GsCrop gsC)
        {
            CopySelectionToClipboard(gsC);
        }
    }

    private void CopySelectionToClipboard(GsCrop gsC)
    {
        gsC.showOutline = false;
        Bitmap? assembled = editorCanvas.AssembleImageForSaveOrCopy();
        if (assembled != null)
        {
            Rectangle cropRect = gsC.Bounds;
            Bitmap outImage = ImageProcessing.CropImage(assembled, cropRect);
            assembled.Dispose();
            copiedBitmap.DisposeImage();
            copiedBitmap.SetImage(ImageProcessing.CopyImage(outImage));
            Clipboard.SetImage(outImage);
            outImage.Dispose();
            gsC.showOutline = true;
        }
    }

    private void NumericPropertiesCurveTension_ValueChanged(object sender, EventArgs e)
    {
        if (GetSelectedSymbolFirst() is GsPolygon gsP)
        {
            gsP.curveTension = (float)numericPropertiesCurveTension.Value;
            editorCanvas.UpdateOverlay();
        }
    }

    private void NumericPropertiesRotation_ValueChanged(object sender, EventArgs e)
    {
        SetNumericClamp(numericPropertiesRotation, numericPropertiesRotation.Value % 360);
        if (GetSelectedSymbolFirst() is GraphicSymbol gs)
        {
            gs.Rotation = (float)numericPropertiesRotation.Value;

            editorCanvas.UpdateOverlay();
        }
    }

    private void ButtonToFront_Click(object sender, EventArgs e)
    {
        if (listViewSymbols.SelectedItems.Count > 0)
        {
            MoveSymbolToFront(listViewSymbols.SelectedItems[0]);
        }
    }
    private void ButtonToBack_Click(object sender, EventArgs e)
    {
        if (listViewSymbols.SelectedItems.Count > 0)
        {
            MoveSymbolToBack(listViewSymbols.SelectedItems[0]);
        }
    }

    private void ButtonResetImageSize_Click(object sender, EventArgs e)
    {
        if (editorCanvas.CurrentSelectedSymbol != null)
        {
            if (editorCanvas.CurrentSelectedSymbol is GsImage gsI)
            {
                if (gsI.image != null)
                {
                    gsI.Width = gsI.image.Width;
                    gsI.Height = gsI.image.Height;
                    editorCanvas.UpdateOverlay();
                }
            }
        }
    }
    private void ButtonPropertiesEditText_Click(object sender, EventArgs e)
    {
        EditSelectedText();
    }

    private void EditSelectedText()
    {
        if (GetSelectedSymbolFirst() is GsText gsT)
        {
            TextEntryDialog textEntry = new(gsT.Text);
            DialogResult result = textEntry.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                textBoxSymbolText.Text = textEntry.TextResult;
            }
            textEntry.Dispose();
        }
    }

    private void PictureBoxOverlay_DoubleClick(object sender, EventArgs e)
    {
        // edit text in popup if the selected symbol is GsText
        if (GetSelectedSymbolFirst() is GsText)
        {
            EditSelectedText();
        }
    }

    #endregion

    #region Top toolbar, new Symbol settings ------------------------------------------------------------

    private void NumericNewLineWeight_ValueChanged(object sender, EventArgs e)
    {
        if (selectedUserAction == UserActions.CreateArrow)
        {
            editorCanvas.ArrowWeight = (int)numericNewLineWeight.Value;
        }
        else
        {
            editorCanvas.LineWeight = (int)numericNewLineWeight.Value;
        }
    }

    private void NewSymbolColor_Click(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            ColorDialogAlpha colorDialogAlpha = new(button.BackColor);
            DialogResult result = colorDialogAlpha.ShowDialog();
            if (result == DialogResult.OK)
            {
                ColorTools.SetButtonColors(button, colorDialogAlpha.Color, "X");
                //button.BackColor = colorDialogAlpha.Color;
            }
            colorDialogAlpha.Dispose();
        }
    }

    #endregion

    #region Symbol toolbar buttons ----------------------------------------------------------------------

    private void ButtonSelect_Click(object sender, EventArgs e)
    {
        SetUserAction(ScreenshotEditor.UserActions.Select);
    }

    private void ButtonRectangle_Click(object sender, EventArgs e)
    {
        SetUserAction(ScreenshotEditor.UserActions.CreateRectangle);
        numericNewLineWeight.Value = editorCanvas.LineWeight;
    }

    private void ButtonCircle_Click(object sender, EventArgs e)
    {
        SetUserAction(ScreenshotEditor.UserActions.CreateCircle);
        numericNewLineWeight.Value = editorCanvas.LineWeight;
    }

    private void ButtonLine_Click(object sender, EventArgs e)
    {
        SetUserAction(UserActions.CreateLine);
        numericNewLineWeight.Value = editorCanvas.LineWeight;
    }

    private void ButtonArrow_Click(object sender, EventArgs e)
    {
        SetUserAction(UserActions.CreateArrow);
        numericNewLineWeight.Value = editorCanvas.ArrowWeight;
    }


    private void ButtonNewText_Click(object sender, EventArgs e)
    {
        SetUserAction(UserActions.CreateText);
        numericNewLineWeight.Value = editorCanvas.LineWeight;
        editorCanvas.UpdateOverlay();
    }

    private void ButtonBorder_Click(object sender, EventArgs e)
    {
        editorCanvas.LineWeight = (int)numericNewLineWeight.Value;
        //Point upperLeft = new Point(0 + (lineWeight / 2), 0 + (lineWeight /2 ));
        //Point size = new Point(originalImage.Width - lineWeight, originalImage.Height - lineWeight);
        Point upperLeft = new(0, 0);
        Point size = new(editorCanvas.CanvasSize.Width, editorCanvas.CanvasSize.Height);
        GsBorder border = new(upperLeft, size, Color.Black, Color.White, false, 1)
        {
            Name = "Border"
        };
        AddNewSymbolToList(border);
        SetUserAction(UserActions.Select);
        editorCanvas.UpdateOverlay();
    }

    private void ButtonBlur_Click(object sender, EventArgs e)
    {
        SetUserAction(UserActions.CreateBlur);
    }

    private void ButtonHighlight_Click(object sender, EventArgs e)
    {
        SetUserAction(UserActions.CreateHighlight);
    }

    private void ButtonCrop_Click(object sender, EventArgs e)
    {
        SetUserAction(UserActions.CreateCrop);
    }

    private void ButtonStickers_Click(object sender, EventArgs e)
    {
        string stickerFolder = Path.GetFullPath(Settings.Default.StickerFolder);
        InsertImageFromFileDialog(new Point(editorCanvas.CanvasSize.Width / 2, editorCanvas.CanvasSize.Height / 2), stickerFolder);
    }

    private void ButtonNumbered_Click(object sender, EventArgs e)
    {
        SetUserAction(UserActions.CreateNumbered);
    }

    private void ButtonDraw_Click(object sender, EventArgs e)
    {
        SetUserAction(UserActions.DrawFreehand);
    }

    private void ButtonFillCurve_Click(object sender, EventArgs e)
    {
        SetUserAction(UserActions.DrawFilledCurve);
    }

    #endregion

    #region set and get properties --------------------------------------------------------------------------

    public (int lineWeight, Color lineColor, Color fillColor, bool shadow) GetNewSymbolProperties()
    {
        return ((int)numericNewLineWeight.Value, buttonNewColorLine.BackColor, buttonNewColorFill.BackColor, checkBoxNewShadow.Checked);
    }

    public ListView GetSymbolListView()
    {
        return listViewSymbols;
    }

    internal void SetLineWeight(int value)
    {
        numericNewLineWeight.Value = value;
    }
    #endregion

    #region NumericUpDowns Supress ding

    private static void SupressEnterDing(KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            e.Handled = true;
            e.KeyChar = '0';
            // this character doesn't actually get sent, since it's after Handled, but will prevent the keypress reacting to Enter, since it no longer is Enter
        }
    }

    private void NumericPropertiesRotation_KeyPress(object sender, KeyPressEventArgs e)
    {
        SupressEnterDing(e);
    }

    private void NumericNewLineWeight_KeyPress(object sender, KeyPressEventArgs e)
    {
        SupressEnterDing(e);
    }

    private void NumericPropertiesX_KeyPress(object sender, KeyPressEventArgs e)
    {
        SupressEnterDing(e);
    }

    private void NumericPropertiesY_KeyPress(object sender, KeyPressEventArgs e)
    {
        SupressEnterDing(e);
    }

    private void NumericPropertiesWidth_KeyPress(object sender, KeyPressEventArgs e)
    {
        SupressEnterDing(e);
    }

    private void NumericPropertiesHeight_KeyPress(object sender, KeyPressEventArgs e)
    {
        SupressEnterDing(e);
    }
    #endregion

    #region Drag and Drop -------------------------------------------------------------------------------

    private List<string> GetDroppedFileNames(DragEventArgs e)
    {
        if (e.Data == null) return [];
        if (e.Data.GetData(DataFormats.FileDrop, true) is not string[] fileNames) return [];
        return [.. fileNames];
    }

    private static Point GetDropLocation(DragEventArgs e, Control control)
    {
        return control.PointToClient(new Point(e.X, e.Y));
    }

    private void ScreenshotEditor_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data == null) return;
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effect = DragDropEffects.Copy;
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private void ScreenshotEditor_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data == null) return;
        List<string> fileNames = GetDroppedFileNames(e);
        Point location = GetDropLocation(e, pictureBoxOverlay);
        for (int i = 0; i < fileNames.Count; i++)
        {
            Point dropOffset = new(i * 20, i * 20);
            Point dropLocation = location.Addition(dropOffset);
            if (pictureBoxOverlay.Bounds.Contains(dropLocation) == false)
            {
                dropLocation = new Point(pictureBoxOverlay.Width / 2, pictureBoxOverlay.Height / 2).Addition(dropOffset);
            }
            InsertImageFromFile(dropLocation, fileNames[i], center: true);
        }
    }
    #endregion

    #region adjust Form on DPI change
    private void ScreenshotEditor_DpiChanged(object sender, DpiChangedEventArgs e)
    {
        Debug.WriteLine($"DPI changed, device dpi: {DeviceDpi} old: {e.DeviceDpiOld} new: {e.DeviceDpiNew}");
        //e.Cancel = true; // will stop window from rescaling, but fonts and controls etc still change size, so stuff is messed up

        //Use this to prevent all rescale of fonts and controls
        if (Settings.Default.PreventDpiRescale)
        {
            int newFontSize = 9;
            Font = new Font(this.Font.FontFamily, newFontSize);

            foreach (Control control in Controls)
            {
                SetFontInControls(control, newFontSize);
            }

            if (this.Width < 750) this.Width = 750;
            if (this.Height < 550) this.Height = 550;
        }

        timerFixDPI.Start();
    }

    private void SetFontInControls(Control control, int newFontSize)
    {
        control.Font = new Font(this.Font.FontFamily, newFontSize);
        foreach (Control subcontrol in control.Controls)
        {
            SetFontInControls(subcontrol, newFontSize);
        }
    }

    private void TimerFixDPI_Tick(object sender, EventArgs e)
    {
        timerFixDPI.Stop();
        if (pictureBoxOverlay.Image != null)
        {
            pictureBoxOverlay.Size = pictureBoxOverlay.Image.Size;
        }
    }

    #endregion


}
