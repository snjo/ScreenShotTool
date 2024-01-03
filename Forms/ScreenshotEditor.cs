using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace ScreenShotTool.Forms
{
    [SupportedOSPlatform("windows")]
    public partial class ScreenshotEditor : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #region Constructor ---------------------------------------------------------------------------------
        Image? originalImage;
        Image? overlayImage;
        Graphics? overlayGraphics;
        int arrowWeight = 5;
        int lineWeight = 2;
        List<GraphicSymbol> symbols = new();

        public ScreenshotEditor()
        {
            InitializeComponent();
            pictureBoxOverlay.Parent = pictureBoxOriginal;

            CreateNewImage(640, 480, Color.White);
            SetForegroundWindow(this.Handle);
        }

        public ScreenshotEditor(string file)
        {
            InitializeComponent();
            pictureBoxOverlay.Parent = pictureBoxOriginal;

            LoadImageFromFile(file);
            SetForegroundWindow(this.Handle);
        }

        public ScreenshotEditor(bool fromClipboard)
        {
            InitializeComponent();
            pictureBoxOverlay.Parent = pictureBoxOriginal;

            if (fromClipboard)
            {
                LoadImageFromClipboard();
            }
            SetForegroundWindow(this.Handle);
        }

        public ScreenshotEditor(Image loadImage)
        {
            InitializeComponent();
            pictureBoxOverlay.Parent = pictureBoxOriginal;
            LoadImageFromImage(loadImage);
            SetForegroundWindow(this.Handle);
        }

        #endregion

        #region Load and Save -------------------------------------------------------------------------------

        private void SetOriginalImage()
        {
            // used at the end of each Load/Create image
            pictureBoxOriginal.Image = originalImage;
            DisposeAndNull(overlayGraphics);
            DisposeAndNull(overlayImage);
            symbols.Clear();
        }

        private void CreateNewImage(int Width, int Height, Color color)
        {
            DisposeAndNull(originalImage);
            originalImage = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(originalImage);
            g.FillRectangle(new SolidBrush(color), 0, 0, Width, Height);
            SetOriginalImage();
        }

        private void LoadImageFromClipboard()
        {
            try
            {
                originalImage = Clipboard.GetImage();
            }
            catch
            {
                Debug.WriteLine("Could not load from clipboard");
                return;
            }
            DisposeAndNull(pictureBoxOriginal.Image);
            SetOriginalImage();
        }

        private void LoadImageFromImage(Image image)
        {
            DisposeAndNull(originalImage);
            originalImage = image;
            SetOriginalImage();
        }

        public void LoadImageFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                Debug.WriteLine("Loading file: " + filename);

                //CreateOverlay();
                try
                {
                    originalImage = Image.FromFile(filename);
                }
                catch
                {
                    Debug.WriteLine("Could not load file");

                }
                DisposeAndNull(pictureBoxOriginal.Image);
                SetOriginalImage();
            }
        }

        public void SaveImage(string filename)
        {
            //originalImage.
            if (originalImage != null)
            {
                Graphics saveGraphic = Graphics.FromImage(originalImage);
                DrawElements(saveGraphic);
                originalImage.Save(filename);
                saveGraphic.Dispose();
            }
        }

        private void PictureBoxOriginal_LoadCompleted(object sender, EventArgs e)
        {
            CreateOverlay();
        }

        #endregion

        #region Create and Update overlay -------------------------------------------------------------------
        private void CreateOverlay()
        {
            if (originalImage != null)
            {
                Debug.WriteLine("Creating overlay");
                overlayImage?.Dispose();
                overlayGraphics?.Dispose();
                overlayImage = new Bitmap(originalImage.Width, originalImage.Height);
                overlayGraphics = Graphics.FromImage(overlayImage);

                //overlayGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //overlayGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                //overlayGraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit; // fixes ugly aliasing on text

                pictureBoxOverlay.Image = overlayImage;
                pictureBoxOverlay.Width = pictureBoxOriginal.Width;
                pictureBoxOverlay.Height = pictureBoxOriginal.Height;
            }
            else
            {
                //Debug.WriteLine("Create overlay failed, original image is null");
            }
        }

        private void UpdateOverlay(GraphicSymbol? temporarySymbol = null)
        {
            if (overlayImage == null || overlayGraphics == null)
            {
                CreateOverlay();
            }
            if (overlayImage != null && overlayGraphics != null)
            {
                pictureBoxOverlay.Image.Dispose();
                pictureBoxOverlay.Image = DrawOverlay(temporarySymbol); ;
            }
        }

        private Image DrawOverlay(GraphicSymbol? temporarySymbol = null)
        {
            Image img = new Bitmap(this.Width, this.Height);
            DisposeAndNull(overlayGraphics);
            overlayGraphics = Graphics.FromImage(img);

            DrawElements(overlayGraphics, temporarySymbol);

            return img;
        }

        private void DrawElements(Graphics graphic, GraphicSymbol? temporarySymbol = null)
        {
            foreach (GraphicSymbol symbol in symbols)
            {
                symbol.DrawSymbol(graphic);
            }
            temporarySymbol?.DrawSymbol(graphic);
        }

        private static void DisposeAndNull(Image? image)
        {
            if (image != null)
            {
                image.Dispose();
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                image = null;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            }
        }

        private static void DisposeAndNull(Graphics? graphics)
        {
            if (graphics != null)
            {
                graphics.Dispose();
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                graphics = null;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            }
        }
        #endregion

        #region Menu items ----------------------------------------------------------------------------------

        private void ItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadImageFromFile(fileDialog.FileName);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new SaveFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SaveImage(fileDialog.FileName);
            }
        }

        private void DeleteOverlayElementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (GraphicSymbol symbol in symbols)
            {
                listViewSymbols.Items.Clear();
                ClearPropertyPanelValues();
                symbol.Dispose();
            }
            symbols.Clear();
            UpdateOverlay();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        private void ItemLoadFromClipboard_Click(object sender, EventArgs e)
        {
            LoadImageFromClipboard();
        }

        private void PasteIntoThisImage_Click(object sender, EventArgs e)
        {
            PasteIntoImage();
        }

        private void ItemPasteScaled_Click(object sender, EventArgs e)
        {
            PasteIntoImageScaled();
        }

        #endregion

        #region Symbols -------------------------------------------------------------------------------------

        public enum SymbolType
        {
            None,
            Rectangle,
            Circle,
            Text,
            Arrow,
            Line,
            Image,
            ImageScaled
        }


        private SymbolType newSymbolType = SymbolType.Rectangle;

        private void AddNewSymbolToList(GraphicSymbol symbol)
        {
            if (symbol != null)
            {
                symbols.Add(symbol);
                ListViewItem newItem = listViewSymbols.Items.Add(symbol.Name);
                newItem.Text = symbol.Name;
                newItem.Tag = symbol;
                listViewSymbols.Update();
            }
        }

        private GraphicSymbol? GetSymbol(object sender, MouseEventArgs e)
        {
            int dragEndX = e.X;
            int dragEndY = e.Y;
            int lineWeight = (int)numericNewLineWeight.Value;
            int lineAlpha = (int)numericNewLineAlpha.Value;
            int fillAlpha = (int)numericNewFillAlpha.Value;

            if (dragStarted)
            {
                int dragLeft = Math.Min(dragStartX, dragEndX);
                int dragRight = Math.Max(dragStartX, dragEndX);
                int dragTop = Math.Min(dragStartY, dragEndY);
                int dragBottom = Math.Max(dragStartY, dragEndY);
                int dragWidth = dragRight - dragLeft;
                int dragHeight = dragBottom - dragTop;

                dragRect = new Rectangle(dragLeft, dragTop, dragWidth, dragHeight);


                Color lineColor = buttonNewColorLine.BackColor;
                Color fillColor = buttonNewColorFill.BackColor;

                /* switch (newSymbolType)
                {
                    case SymbolType.Rectangle:
                        return new GsRectangle(lineColor, fillColor, dragRect.X, dragRect.Y, dragRect.Width, dragRect.Height, lineWeight, lineAlpha, fillAlpha);
                    case SymbolType.Circle:
                        return new GsCircle(lineColor, fillColor, dragRect.X, dragRect.Y, dragRect.Width, dragRect.Height, lineWeight, lineAlpha, fillAlpha);
                    case SymbolType.Line:
                        return new GsLine(lineColor, fillColor, dragStartX, dragStartY, dragEndX, dragEndY, lineWeight, lineAlpha);
                    case SymbolType.Arrow:
                        return new GsArrow(lineColor, fillColor, dragStartX, dragStartY, dragEndX, dragEndY, lineWeight, lineAlpha);
                    case SymbolType.Image:
                        return new GsImage(lineColor, fillColor, dragStartX, dragStartY, dragEndX, dragEndY);
                    case SymbolType.ImageScaled:
                        return new GsImageScaled(lineColor, fillColor, dragLeft, dragTop, dragWidth, dragHeight);
                    default:
                        return null;
                }
                */

                return newSymbolType switch
                {
                    SymbolType.Rectangle => new GsRectangle(lineColor, fillColor, dragRect.X, dragRect.Y, dragRect.Width, dragRect.Height, lineWeight, lineAlpha, fillAlpha),
                    SymbolType.Circle => new GsCircle(lineColor, fillColor, dragRect.X, dragRect.Y, dragRect.Width, dragRect.Height, lineWeight, lineAlpha, fillAlpha),
                    SymbolType.Line => new GsLine(lineColor, fillColor, dragStartX, dragStartY, dragEndX, dragEndY, lineWeight, lineAlpha),
                    SymbolType.Arrow => new GsArrow(lineColor, fillColor, dragStartX, dragStartY, dragEndX, dragEndY, lineWeight, lineAlpha),
                    SymbolType.Image => new GsImage(lineColor, fillColor, dragStartX, dragStartY, dragEndX, dragEndY),
                    SymbolType.ImageScaled => new GsImageScaled(lineColor, fillColor, dragLeft, dragTop, dragWidth, dragHeight),
                    _ => null,
                };
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Symbol toolbar buttons ----------------------------------------------------------------------

        private void ButtonRectangle_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Rectangle;
            numericNewLineWeight.Value = lineWeight;
            UpdateOverlay();
        }

        private void ButtonCircle_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Circle;
            numericNewLineWeight.Value = lineWeight;
            UpdateOverlay();
        }

        private void ButtonLine_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Line;
            numericNewLineWeight.Value = lineWeight;
            UpdateOverlay();
        }

        private void ButtonArrow_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Arrow;
            numericNewLineWeight.Value = arrowWeight;
            UpdateOverlay();
        }



        #endregion

        #region Mouse input ---------------------------------------------------------------------------------

        bool dragStarted = false;
        int dragStartX = 0;
        int dragStartY = 0;
        Rectangle dragRect = new();

        private void PictureBoxOverlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;
            dragStarted = true;
            dragStartX = e.X;
            dragStartY = e.Y;
            if (overlayGraphics == null)
            {
                CreateOverlay();
            }
        }

        private void PictureBoxOverlay_MouseUp(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;
            GraphicSymbol? symbol = GetSymbol(sender, e);
            if (symbol != null)
            {
                if (symbol.ValidSymbol)
                {
                    AddNewSymbolToList(symbol);
                }
            }
            UpdateOverlay();
            dragStarted = false;
            if (newSymbolType == SymbolType.Image || newSymbolType == SymbolType.ImageScaled)
            {
                // don't repeatedly add pasted images
                newSymbolType = SymbolType.None;
            }
        }

        private void PictureBoxOverlay_MouseMove(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;
            GraphicSymbol? tempSymbol = GetSymbol(sender, e);
            if (tempSymbol != null)
            {
                UpdateOverlay(tempSymbol);
                tempSymbol.Dispose();
            }
        }

        private void PictureBoxOverlay_MouseLeave(object sender, EventArgs e)
        {
            //dragStarted = false;
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
        }
        #endregion

        #region Copy and Paste ------------------------------------------------------------------------------

        public void CopyToClipboard()
        {
            if (originalImage != null)
            {
                Bitmap outImage = new Bitmap(originalImage);
                Graphics saveGraphic = Graphics.FromImage(outImage);
                DrawElements(saveGraphic);
                Clipboard.SetImage(outImage);
                saveGraphic.Dispose();
            }
        }

        private void PasteIntoImage()
        {
            newSymbolType = SymbolType.Image;
            dragStarted = true;
            dragStartX = 0;
            dragStartY = 0;
            UpdateOverlay();
        }

        private void PasteIntoImageScaled()
        {
            newSymbolType = SymbolType.ImageScaled;
            UpdateOverlay();
        }

        #endregion

        #region Selected symbol Properties panel ------------------------------------------------------------

        private void ListViewSymbols_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is GraphicSymbol graphicSymbol)
                {
                    labelSymbolType.Text = "Symbol: " + graphicSymbol.Name;
                    numericX.Value = graphicSymbol.X1;
                    numericY.Value = graphicSymbol.Y1;
                    numericWidth.Value = graphicSymbol.X2;
                    numericHeight.Value = graphicSymbol.Y2;
                    buttonPropertiesColorLine.BackColor = graphicSymbol.foregroundColor;
                    buttonPropertiesColorFill.BackColor = graphicSymbol.backgroundColor;
                    numericPropertiesLineWeight.Value = graphicSymbol.lineWeight;
                    buttonDeleteSymbol.Tag = graphicSymbol;

                    if (graphicSymbol.Name == "Image")
                    {
                        numericWidth.Enabled = false;
                        numericHeight.Enabled = false;
                    }
                    else
                    {
                        numericWidth.Enabled = true;
                        numericHeight.Enabled = true;
                    }

                    numericPropertiesLineAlpha.Value = graphicSymbol.lineAlpha;
                    numericPropertiesFillAlpha.Value = graphicSymbol.fillAlpha;
                }

            }
            else
            {
                ClearPropertyPanelValues();
            }
        }

        private void ButtonDeleteSymbol_Click(object sender, EventArgs e)
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (buttonDeleteSymbol.Tag is GraphicSymbol gs)
                {
                    listViewSymbols.Items.Remove(item);
                    gs.Dispose();
                    symbols.Remove(gs);
                }
                listViewSymbols.Update();
                ClearPropertyPanelValues();
                UpdateOverlay();
            }
        }

        private void ClearPropertyPanelValues()
        {
            labelSymbolType.Text = "Symbol: ";
            numericX.Value = 0;
            numericY.Value = 0;
            numericWidth.Value = 1;
            numericHeight.Value = 1;
            buttonPropertiesColorLine.BackColor = Color.Gray;
            buttonPropertiesColorFill.BackColor = Color.Gray;
            numericPropertiesLineAlpha.Value = 255;
            numericPropertiesFillAlpha.Value = 255;
            numericPropertiesLineWeight.Value = 1;
            buttonDeleteSymbol.Tag = null;
        }

        private void Numeric_ValueChanged(object sender, EventArgs e)
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is not GraphicSymbol gs) return;

                if (sender == numericX)
                {
                    gs.X1 = (int)numericX.Value;
                }
                if (sender == numericY)
                {
                    gs.Y1 = (int)numericY.Value;
                }
                if (sender == numericWidth)
                {
                    gs.X2 = (int)numericWidth.Value;
                }
                if (sender == numericHeight)
                {
                    gs.Y2 = (int)numericHeight.Value;
                }
                if (sender == numericPropertiesLineWeight)
                {
                    gs.lineWeight = (int)numericPropertiesLineWeight.Value;
                }
                if (sender == numericPropertiesLineAlpha)
                {
                    gs.lineAlpha = (int)numericPropertiesLineAlpha.Value;
                    gs.UpdateColors();
                    buttonPropertiesColorLine.BackColor = gs.foregroundColor;
                }
                if (sender == numericPropertiesFillAlpha)
                {
                    gs.fillAlpha = (int)numericPropertiesFillAlpha.Value;
                    gs.UpdateColors();
                    buttonPropertiesColorFill.BackColor = gs.backgroundColor;
                }
            }
            UpdateOverlay();
        }

        private void ColorChangeClick(object sender, EventArgs e)
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is not GraphicSymbol gs) return;

                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (sender == buttonPropertiesColorLine)
                    {
                        buttonPropertiesColorLine.BackColor = colorDialog1.Color;
                        gs.foregroundColor = colorDialog1.Color;

                    }
                    if (sender == buttonPropertiesColorFill)
                    {
                        buttonPropertiesColorFill.BackColor = colorDialog1.Color;
                        gs.backgroundColor = colorDialog1.Color;
                    }
                }
            }
            UpdateOverlay();
        }

        #endregion

        #region Top toolbar, new Symbol settings ------------------------------------------------------------

        private void NumericNewLineWeight_ValueChanged(object sender, EventArgs e)
        {
            if (newSymbolType == SymbolType.Arrow)
            {
                arrowWeight = (int)numericNewLineWeight.Value;
            }
            else
            {
                lineWeight = (int)numericNewLineWeight.Value;
            }
        }

        private void NewColorLine_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                buttonNewColorLine.BackColor = colorDialog1.Color;
            }
        }

        private void NewColorFill_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                buttonNewColorFill.BackColor = colorDialog1.Color;
            }
        }

        private void ItemNewImage_Click(object sender, EventArgs e)
        {
            NewImagePrompt imagePrompt = new();
            DialogResult result = imagePrompt.ShowDialog();
            if (result == DialogResult.OK)
            {
                CreateNewImage(imagePrompt.imageWidth, imagePrompt.imageHeight, imagePrompt.color);
            }
            imagePrompt.Dispose();
        }

        #endregion
    }
}
