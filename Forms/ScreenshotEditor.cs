using ScreenShotTool.Classes;
using ScreenShotTool.Properties;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.Versioning;
using static ScreenShotTool.EditorCanvas;

namespace ScreenShotTool.Forms;

[SupportedOSPlatform("windows")]
public partial class ScreenshotEditor : Form
{

    #region Constructor ---------------------------------------------------------------------------------
    EditorCanvas editorCanvas;
    private List<ImageFormatDefinition> imageFormats = [];
    public readonly static int maxFontSize = 200;
    public readonly static int minimumFontSize = 5;
    public readonly static int startingFontSize = 10;
    readonly List<Button> toolButtons = [];

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
        FillFontFamilyBox();
        imageFormats = CreateImageFormatsList();
        numericPropertiesFontSize.Maximum = maxFontSize;
        numericPropertiesFontSize.Minimum = minimumFontSize;
        numericPropertiesFontSize.Value = startingFontSize;
        DisableAllPanels();
        numericBlurMosaicSize.Value = editorCanvas.mosaicSize;
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

        listViewSymbols.Height = 200;
        UpdatePropertiesPanel();
    }

    private List<ImageFormatDefinition> CreateImageFormatsList()
    {
        List<ImageFormatDefinition> list = [];
        list.Add(new ImageFormatDefinition("All files", "*.*", ImageFormat.Png));
        list.Add(new ImageFormatDefinition("Images (*.png,*.jpg,*.jpeg,*.gif,*.bmp,*.webp)", "(*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp)", ImageFormat.Png));
        list.Add(new ImageFormatDefinition("PNG", "*.png", ImageFormat.Png));
        list.Add(new ImageFormatDefinition("JPG", "*.jpg", ImageFormat.Jpeg));
        list.Add(new ImageFormatDefinition("GIF", "*.gif", ImageFormat.Gif));
        list.Add(new ImageFormatDefinition("BMP", "*.bmp", ImageFormat.Bmp));
        return list;
    }

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
        CreateImageScaled,
        CreateBlur,
        CreateHighlight,
        CreateCrop
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
            Filter = "Images (*.png,*.jpg,*.jpeg,*.gif,*.bmp,*.webp)|(*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp)|PNG|*.png|JPG|*.jpg|GIF|*.gif|BMP|*.bmp|All files|*.*"
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
        FileDialog fileDialog = new SaveFileDialog();

        string filter = "";
        for (int i = 0; i < imageFormats.Count; i++)
        {
            filter += imageFormats[i].FilterString;
            if (i < imageFormats.Count - 1)
                filter += "|";
        }

        fileDialog.Filter = filter;
        fileDialog.FileName = "";
        fileDialog.FilterIndex = 3;
        DialogResult result = fileDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            string filename = fileDialog.FileName;
            ImageFormat imgFormat = ImageFormat.Png;
            int selectedFormat = fileDialog.FilterIndex - 1; // filter start with 1, correcting to 0-based
            if (selectedFormat < 2) // all or multi-filter images
            {
                imgFormat = ImageFormatFromExtension(filename);
                Debug.WriteLine($"Guessed file format from file name ({filename}): {imgFormat} ");
            }
            else
            {
                if (selectedFormat < imageFormats.Count)
                {
                    imgFormat = imageFormats[selectedFormat].Format;
                    Debug.WriteLine($"Using format from index {selectedFormat}: {imgFormat} ");
                }
            }
            SaveImage(fileDialog.FileName, imgFormat);
        }
    }

    private void SaveImage(string filename, ImageFormat imgFormat)
    {
        Debug.WriteLine($"Saving image {filename} with format {imgFormat}");
        Bitmap? outImage = editorCanvas.AssembleImageForSaveOrCopy();

        if (outImage != null && imgFormat == ImageFormat.Jpeg)
        {
            MainForm.SaveJpeg(filename, (Bitmap)outImage, Settings.Default.JpegQuality);
            outImage.Dispose();
        }
        else if (outImage != null)
        {
            outImage.Save(filename, imgFormat);
            outImage.Dispose();
        }
    }

    private static ImageFormat ImageFormatFromExtension(string filename)
    {
        string extension = Path.GetExtension(filename);
        return extension switch
        {
            ".png" => ImageFormat.Png,
            ".jpg" => ImageFormat.Jpeg,
            ".bmp" => ImageFormat.Bmp,
            ".gif" => ImageFormat.Gif,
            _ => ImageFormat.Png,
        };
    }

    private void DeleteOverlayElementsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        editorCanvas.DeleteAllSymbols();
    }

    public void DeleteListViewSymbols()
    {
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

    private void ItemPasteScaled_Click(object sender, EventArgs e)
    {
        PasteIntoImageScaled();
    }

    private void ItemNewImage_Click(object sender, EventArgs e)
    {
        NewImagePrompt imagePrompt = new();
        DialogResult result = imagePrompt.ShowDialog();
        if (result == DialogResult.OK)
        {
            editorCanvas.CreateNewImage(imagePrompt.imageWidth, imagePrompt.imageHeight, imagePrompt.color);
        }
        imagePrompt.Dispose();
    }

    #endregion

    #region User Actions --------------------------------------------------------------------------------

    public UserActions selectedUserAction = UserActions.None;
    public void SetUserAction(UserActions action)
    {
        selectedUserAction = action;
        foreach (Button b in toolButtons)
        {
            b.BackColor = Color.Transparent;
        }
        if (selectedUserAction == UserActions.Select) buttonSelect.BackColor = Color.Yellow;
        if (selectedUserAction == UserActions.CreateRectangle) buttonRectangle.BackColor = Color.Yellow;
        if (selectedUserAction == UserActions.CreateCircle) buttonCircle.BackColor = Color.Yellow;
        if (selectedUserAction == UserActions.CreateLine) buttonLine.BackColor = Color.Yellow;
        if (selectedUserAction == UserActions.CreateArrow) buttonArrow.BackColor = Color.Yellow;
        if (selectedUserAction == UserActions.CreateText) buttonText.BackColor = Color.Yellow;
        if (selectedUserAction == UserActions.CreateBlur) buttonBlur.BackColor = Color.Yellow;
        if (selectedUserAction == UserActions.CreateHighlight) buttonHighlight.BackColor = Color.Yellow;
        if (selectedUserAction == UserActions.CreateCrop) buttonCrop.BackColor = Color.Yellow;
        // UserActions.CreateBorder is not in list since it happens right away without mouse drag

        if (selectedUserAction != UserActions.MoveSymbol && selectedUserAction != UserActions.ScaleSymbol)
        {
            pictureBoxOverlay.Cursor = Cursors.Arrow;
        }
        editorCanvas.UpdateOverlay();
    }
    #endregion

    #region Mouse events --------------------------------------------------------------------------------
    private void PictureBoxOverlay_MouseDown(object sender, MouseEventArgs e)
    {
        editorCanvas.MouseDown(new Point(e.X, e.Y));
    }
    private void PictureBoxOverlay_MouseMove(object sender, MouseEventArgs e)
    {
        editorCanvas.MouseMove(sender, new Point(e.X, e.Y));
    }
    private void PictureBoxOverlay_MouseUp(object sender, MouseEventArgs e)
    {
        editorCanvas.MouseUp(sender, new Point(e.X, e.Y));
    }
    #endregion

    #region Key input -----------------------------------------------------------------------------------
    private void ScreenshotEditor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
        {
            PasteIntoImage();
        }
        if (e.KeyCode == Keys.V && e.Modifiers == (Keys.Control | Keys.Shift))
        {
            PasteIntoImageScaled();
        }
        if ((e.KeyCode == Keys.C && e.Modifiers == Keys.Control))
        {
            CopyToClipboard();
        }
        if ((e.KeyCode == Keys.S && e.Modifiers == Keys.Control))
        {
            SaveFileAction();
        }
        if ((e.KeyCode == Keys.O && e.Modifiers == Keys.Control))
        {
            OpenFileAction();
        }
    }
    #endregion

    #region Copy and Paste ------------------------------------------------------------------------------

    public void CopyToClipboard()
    {
        Bitmap? result = editorCanvas.AssembleImageForSaveOrCopy();
        if (result != null)
        {
            Clipboard.SetImage(result);
            result.Dispose();
        }
    }

    private void PasteIntoImage()
    {
        SetUserAction(UserActions.CreateImage);
        editorCanvas.dragStarted = true;
        editorCanvas.dragStart = new Point(0, 0);
        editorCanvas.UpdateOverlay();
    }

    private void PasteIntoImageScaled()
    {
        SetUserAction(UserActions.CreateImageScaled);
        editorCanvas.UpdateOverlay();
    }

    #endregion

    #region Symbol Properties panel ---------------------------------------------------------------------

    public void AddNewSymbolToList(GraphicSymbol symbol)
    {
        if (symbol != null)
        {
            editorCanvas.symbols.Add(symbol);
            ListViewItem newItem = listViewSymbols.Items.Add(symbol.Name);
            newItem.Text = symbol.Name;
            newItem.Tag = symbol;
            symbol.ListViewItem = newItem;
            listViewSymbols.Update();
            if (listViewSymbols.Items.Count > 0)
            {
                listViewSymbols.Items[^1].Focused = true; // ^1 is listViewSymbols.Items.Count - 1, https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/ranges
                listViewSymbols.Items[^1].Selected = true;
                listViewSymbols.Items[^1].EnsureVisible();
                listViewSymbols.Select();
            }
        }
    }

    private void ListViewSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listViewSymbols.SelectedItems.Count > 0)
        {
            //SetUserAction(UserActions.MoveSymbol);
            SetUserAction(UserActions.None);

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
        top += panel.Height + 5;
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
    }

    private void SetNumericClamp(NumericUpDown numericUpDown, int value)
    {
        numericUpDown.Value = Math.Clamp(value, numericUpDown.Minimum, numericUpDown.Maximum);
        if (value < numericUpDown.Minimum) Debug.WriteLine($"Value is below {numericUpDown.Name} Minimum");
        if (value > numericUpDown.Maximum) Debug.WriteLine($"Value is above {numericUpDown.Name} Maximum");
    }

    public void UpdatePropertiesPanel()
    {
        int panelLeft = listViewSymbols.Left;
        int lastPanelBottom = listViewSymbols.Bottom;
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
                numericPropertiesLineAlpha.Enabled = true;

                labelSymbolType.Text = "Symbol: " + graphicSymbol.Name;
                SetNumericClamp(numericPropertiesX, graphicSymbol.Left);
                SetNumericClamp(numericPropertiesY, graphicSymbol.Top);
                SetNumericClamp(numericPropertiesWidth, graphicSymbol.Width);
                SetNumericClamp(numericPropertiesHeight, graphicSymbol.Height);
                buttonPropertiesColorLine.BackColor = graphicSymbol.ForegroundColor;
                buttonPropertiesColorFill.BackColor = graphicSymbol.BackgroundColor;
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

                    textBoxSymbolText.Text = gsText.text;
                    if (gsText.ListViewItem != null)
                    {
                        gsText.ListViewItem.Text = "Text: " + gsText.text;
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

                numericPropertiesLineAlpha.Value = graphicSymbol.lineAlpha;
                numericPropertiesFillAlpha.Value = graphicSymbol.fillAlpha;
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
        DeleteSelectedSymbol();
    }

    private void DeleteSelectedSymbol()
    {
        if (listViewSymbols.SelectedItems.Count > 0)
        {
            ListViewItem item = listViewSymbols.SelectedItems[0];
            if (buttonDeleteSymbol.Tag is GraphicSymbol gs)
            {
                listViewSymbols.Items.Remove(item);
                gs.Dispose();
                editorCanvas.symbols.Remove(gs);
            }
            listViewSymbols.Update();
            ClearPropertyPanelValues();
            editorCanvas.UpdateOverlay();
        }
    }

    public void ClearPropertyPanelValues()
    {
        labelSymbolType.Text = "Symbol: ";
        numericPropertiesX.Value = 0;
        numericPropertiesY.Value = 0;
        numericPropertiesWidth.Value = 1;
        numericPropertiesHeight.Value = 1;
        buttonPropertiesColorLine.BackColor = Color.Gray;
        buttonPropertiesColorFill.BackColor = Color.Gray;
        numericPropertiesLineAlpha.Value = 255;
        numericPropertiesFillAlpha.Value = 255;
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
            if (sender == numericPropertiesLineAlpha)
            {
                gs.lineAlpha = (int)numericPropertiesLineAlpha.Value;
                gs.UpdateColors();
                buttonPropertiesColorLine.BackColor = gs.ForegroundColor;
            }
            if (sender == numericPropertiesFillAlpha)
            {
                gs.fillAlpha = (int)numericPropertiesFillAlpha.Value;
                gs.UpdateColors();
                buttonPropertiesColorFill.BackColor = gs.BackgroundColor;
            }
        }
        editorCanvas.UpdateOverlay();
    }

    private void ColorChangeClick(object sender, EventArgs e)
    {
        if (listViewSymbols.SelectedItems.Count > 0)
        {
            ListViewItem item = listViewSymbols.SelectedItems[0];
            if (item.Tag is not GraphicSymbol gs) return;

            colorDialog1.Color = ((Button)sender).BackColor;
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (sender == buttonPropertiesColorLine)
                {
                    buttonPropertiesColorLine.BackColor = colorDialog1.Color;
                    gs.ForegroundColor = colorDialog1.Color;

                }
                if (sender == buttonPropertiesColorFill)
                {
                    buttonPropertiesColorFill.BackColor = colorDialog1.Color;
                    gs.BackgroundColor = colorDialog1.Color;
                }
            }
        }
        editorCanvas.UpdateOverlay();
    }

    private void NumericBlurMosaicSize_ValueChanged(object sender, EventArgs e)
    {
        if (editorCanvas.InitialBlurComplete)
        {
            editorCanvas.CreateBlurImage((int)numericBlurMosaicSize.Value);
            editorCanvas.UpdateOverlay();
        }
    }

    private void TextBoxSymbolText_TextChanged(object sender, EventArgs e)
    {
        GsText? textSymbol = GetSelectedTextSymbol();
        if (textSymbol != null)
        {
            textSymbol.text = textBoxSymbolText.Text;
            if (textSymbol.ListViewItem != null)
            {
                textSymbol.ListViewItem.Text = "Text: " + textSymbol.text;
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
        List<FontFamily> fontList = FontFamily.Families.ToList();
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
        GraphicSymbol? symbol = GetSelectedSymbol();
        if (symbol != null)
        {
            symbol.ShadowEnabled = checkBoxPropertiesShadow.Checked;
        }
        editorCanvas.UpdateOverlay();
    }

    public GraphicSymbol? GetSelectedSymbol()
    {
        if (listViewSymbols.SelectedItems.Count > 0)
        {
            ListViewItem item = listViewSymbols.SelectedItems[0];
            if (item.Tag is GraphicSymbol gs)
            {
                editorCanvas.currentSelectedSymbol = gs;
                return gs;
            }
        }
        return null;
    }

    private GsText? GetSelectedTextSymbol()
    {
        if (GetSelectedSymbol() is GsText gs) return gs;
        return null;
    }

    private void ComboBoxBlendMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (editorCanvas.currentSelectedSymbol is GsHighlight gshl)
        {
            if (Enum.TryParse(comboBoxBlendMode.Text, out ColorBlend.BlendModes newBlend))
            {
                gshl.blendMode = newBlend;
            }
            editorCanvas.UpdateOverlay();
        }
    }

    private void buttonPropertyCrop_Click(object sender, EventArgs e)
    {
        if (editorCanvas.SourceImage == null) return;
        if (GetSelectedSymbol() is GsCrop gsC)
        {
            gsC.showOutline = false;
            Rectangle cropRect = gsC.Bounds;
            Bitmap outImage = CropImage(editorCanvas.SourceImage, cropRect);
            foreach (GraphicSymbol gs in editorCanvas.symbols)
            {
                gs.MoveTo(gs.Left - cropRect.Left, gs.Top - cropRect.Top);
            }
            editorCanvas.LoadImageFromImage(outImage, false);
            gsC.showOutline = true;
            DeleteSelectedSymbol();
        }
    }

    private void buttonPropertyCopyCrop_Click(object sender, EventArgs e)
    {
        if (GetSelectedSymbol() is GsCrop gsC)
        {
            gsC.showOutline = false;
            Bitmap? assembled = editorCanvas.AssembleImageForSaveOrCopy();
            if (assembled != null)
            {
                Rectangle cropRect = gsC.Bounds;
                Bitmap outImage = EditorCanvas.CropImage(assembled, cropRect);
                assembled.Dispose();
                Clipboard.SetImage(outImage);
                outImage.Dispose();
                gsC.showOutline = true;
            }
        }
    }

    private void ListViewSymbols_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Delete)
        {
            DeleteSelectedSymbol();
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

    private void NewColorLine_Click(object sender, EventArgs e)
    {
        colorDialog1.Color = ((Button)sender).BackColor;
        DialogResult result = colorDialog1.ShowDialog();
        if (result == DialogResult.OK)
        {
            buttonNewColorLine.BackColor = colorDialog1.Color;
        }
    }

    private void NewColorFill_Click(object sender, EventArgs e)
    {
        colorDialog1.Color = ((Button)sender).BackColor;
        DialogResult result = colorDialog1.ShowDialog();
        if (result == DialogResult.OK)
        {
            buttonNewColorFill.BackColor = colorDialog1.Color;
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
        GsBorder border = new(upperLeft, size, Color.Black, Color.White, false, 1, 255, 0)
        {
            Name = "Border"
        };
        AddNewSymbolToList(border);
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

    private void buttonCrop_Click(object sender, EventArgs e)
    {
        SetUserAction(UserActions.CreateCrop);
    }

    #endregion

    #region set and get properties --------------------------------------------------------------------------

    public (int lineWeight, int lineAlpha, int fillAlpha, Color lineColor, Color fillColor, bool shadow) GetNewSymbolProperties()
    {
        return ((int)numericNewLineWeight.Value, (int)numericNewLineAlpha.Value, (int)numericNewFillAlpha.Value, buttonNewColorLine.BackColor, buttonNewColorFill.BackColor, checkBoxNewShadow.Checked);
    }

    public ListView GetSybmolListView()
    {
        return listViewSymbols;
    }

    internal void SetLineWeight(int value)
    {
        numericNewLineWeight.Value = value;
    }
    #endregion
}
