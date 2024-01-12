using ScreenShotTool.Properties;
using System.Diagnostics;
using System.Drawing.Text;
using System.Numerics;
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
        Bitmap? blurImage;
        Graphics? overlayGraphics;
        int arrowWeight = 5;
        int lineWeight = 2;
        int frameRate = Settings.Default.MaxFramerate;
        public static int maxFontSize = 200;
        public static int minimumFontSize = 5;
        public static int startingFontSize = 10;
        List<GraphicSymbol> symbols = new();


        private void SetupEditor()
        {
            fillFontFamilyBox();
            numericPropertiesFontSize.Maximum = maxFontSize;
            numericPropertiesFontSize.Minimum = minimumFontSize;
            numericPropertiesFontSize.Value = startingFontSize;
            panelPropertiesPosition.Enabled = false;
            panelPropertiesFill.Visible = false;
            panelPropertiesLine.Visible = false;
            panelPropertiesText.Visible = false;
        }

        public ScreenshotEditor()
        {
            InitializeComponent();
            SetupEditor();

            CreateNewImage(640, 480, Color.White);
        }

        public ScreenshotEditor(string file)
        {
            InitializeComponent();
            SetupEditor();

            LoadImageFromFile(file);
        }

        public ScreenshotEditor(bool fromClipboard)
        {
            InitializeComponent();
            SetupEditor();

            if (fromClipboard)
            {
                LoadImageFromClipboard();
            }
        }

        public ScreenshotEditor(Image loadImage)
        {
            InitializeComponent();
            SetupEditor();
            LoadImageFromImage(loadImage);
            SetForegroundWindow(this.Handle);
        }

        #endregion

        #region Load and Save -------------------------------------------------------------------------------

        private void FlushImages()
        {
            // used at the end of each Load/Create image
            DisposeAndNull(overlayGraphics);
            DisposeAndNull(overlayImage);
            DisposeAndNull(blurImage);
            DeleteAllSymbols();
        }

        private void CreateNewImage(int Width, int Height, Color color)
        {
            DisposeAndNull(originalImage);
            originalImage = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(originalImage);
            g.FillRectangle(new SolidBrush(color), 0, 0, Width, Height);
            FlushImages();
            DeleteAllSymbols();
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
            FlushImages();
        }

        private void LoadImageFromImage(Image image)
        {
            DisposeAndNull(originalImage);
            originalImage = image;
            FlushImages();
        }

        public void LoadImageFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                Debug.WriteLine("Loading file: " + filename);

                try
                {
                    // Using and closing filestream, so the file isn't reserved by the process

                    Image? tempImage = null;
                    using (FileStream stream = new FileStream(filename, FileMode.Open))
                    {
                        tempImage = Image.FromStream(stream);
                    }
                    DisposeAndNull(originalImage);
                    originalImage = tempImage;
                }
                catch
                {
                    Debug.WriteLine("Could not load file");

                }
                FlushImages();
            }
        }

        public void SaveImage(string filename)
        {
            if (originalImage != null)
            {
                Graphics saveGraphic = Graphics.FromImage(originalImage);
                saveGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                saveGraphic.TextRenderingHint = TextRenderingHint.AntiAlias;

                DrawElements(saveGraphic);
                originalImage.Save(filename);
                saveGraphic.Dispose();
            }
        }

        #endregion

        #region Create and Update overlay -------------------------------------------------------------------
        private void CreateOverlay()
        {
            if (originalImage != null)
            {
                overlayImage?.Dispose();
                overlayGraphics?.Dispose();
                overlayImage = new Bitmap(originalImage.Width, originalImage.Height);
                overlayGraphics = Graphics.FromImage(overlayImage);
                blurImage = CreateBlurImage();

                //overlayGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                //overlayGraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit; // fixes ugly aliasing on text
                overlayGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                overlayGraphics.TextRenderingHint = TextRenderingHint.AntiAlias; // fixes ugly aliasing on text

                pictureBoxOverlay.Image = overlayImage;
                pictureBoxOverlay.Width = originalImage.Width;
                pictureBoxOverlay.Height = originalImage.Height;
            }
            else
            {
                //Debug.WriteLine("Create overlay failed, original image is null");
            }
        }

        private Bitmap CreateBlurImage()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            DisposeAndNull(blurImage);
            int blurSize = 5;
            blurImage = new Bitmap(originalImage.Width, originalImage.Height);
            Graphics graphics = Graphics.FromImage(blurImage);
            Color pixelColor = Color.Black;

            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    //if (x % blurSize == 0 && y % blurSize == 0)
                    //{
                    //    pixelColor = SamplePixelArea(originalImage, blurSize, x, y);
                    //}
                    pixelColor = SamplePixelArea(originalImage, blurSize, x, y);
                    blurImage.SetPixel(x, y, pixelColor);
                }
            }

            graphics.Dispose();
            sw.Stop();

            Debug.WriteLine($"Blur took {sw.ElapsedMilliseconds}");

            return blurImage;
        }

        private Color SamplePixelArea(Image originalImage, int blurSize, int x, int y)
        {
            Color sampleColor;
            Color pixelColor;
            int sampleX = x;
            int sampleY = y;
            int R = 0;
            int G = 0;
            int B = 0;
            int samples = 0;
            //if (x == 12 && y == 12) Debug.WriteLine($"Sample from {x}, {y}");
            //for (int i = -blurSize/2; i <= blurSize/2; i+=3)
            for (int i = -blurSize; i <= blurSize; i++)
            {
                //for (int j = -blurSize / 2; j <= blurSize / 2; j+=3)
                for (int j = -blurSize; j <= blurSize; j++)
                {
                    // sampleX = sampleX - (sampleX % blurSize) + i;
                    //sampleY = sampleY - (sampleY % blurSize) + j;
                    sampleX = x + i;
                    sampleY = y + j;
                    sampleX = Math.Clamp(sampleX, 0, originalImage.Width - 1);
                    sampleY = Math.Clamp(sampleY, 0, originalImage.Height - 1);
                    sampleColor = ((Bitmap)originalImage).GetPixel(sampleX, sampleY);
                    R += sampleColor.R;
                    G += sampleColor.G;
                    B += sampleColor.B;
                    samples++;
                    //if (x == 12 && y == 12) Debug.WriteLine($"check {sampleX}, {sampleY}");
                }
            }
            pixelColor = Color.FromArgb(R / samples, G / samples, B / samples);
            //Debug.WriteLine($"Samples: {samples}");
            return pixelColor;
        }

        DateTime LastFrame = DateTime.Now;

        private bool UpdateOverlay(GraphicSymbol? temporarySymbol = null, bool forceUpdate = true)
        {
            float MilliSecondsPerFrame = (1f / frameRate) * 1000;
            TimeSpan ts = DateTime.Now - LastFrame;
            if (ts.Milliseconds < MilliSecondsPerFrame && forceUpdate == false)
            {
                return false;
            }

            if (overlayImage == null || overlayGraphics == null)
            {
                CreateOverlay();
            }
            if (overlayImage != null && overlayGraphics != null)
            {
                pictureBoxOverlay.Image.Dispose();
                pictureBoxOverlay.Image = DrawOverlay(temporarySymbol); ;
            }

            LastFrame = DateTime.Now;
            return true;
        }

        private Image DrawOverlay(GraphicSymbol? temporarySymbol = null)
        {

            Bitmap img = ((Bitmap)(originalImage)).Clone(new Rectangle(0, 0, originalImage.Width, originalImage.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            DisposeAndNull(overlayGraphics);
            overlayGraphics = Graphics.FromImage(img);

            overlayGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            overlayGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            DrawElements(overlayGraphics, temporarySymbol);

            return img;
        }

        private void DrawElements(Graphics graphic, GraphicSymbol? temporarySymbol = null)
        {
            foreach (GraphicSymbol symbol in symbols)
            {
                if (symbol is GsBlur)
                {
                    if (originalImage != null)
                    {
                        ((GsBlur)symbol).blurredImage = blurImage;
                    }
                }
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
            DeleteAllSymbols();
        }

        private void DeleteAllSymbols()
        {
            listViewSymbols.Items.Clear();
            foreach (GraphicSymbol symbol in symbols)
            {
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
            MoveSymbol,
            Rectangle,
            Circle,
            Text,
            Arrow,
            Line,
            Image,
            ImageScaled,
            Blur
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
                symbol.ListViewItem = newItem;
                listViewSymbols.Update();
                if (listViewSymbols.Items.Count > 0)
                {
                    listViewSymbols.Items[listViewSymbols.Items.Count - 1].Focused = true;
                    listViewSymbols.Items[listViewSymbols.Items.Count - 1].Selected = true;
                    listViewSymbols.Items[listViewSymbols.Items.Count - 1].EnsureVisible();
                    listViewSymbols.Select();
                }
            }
        }

        private GraphicSymbol? GetSymbol(object sender, MouseEventArgs e)
        {
            Point dragEnd = new Point(e.X, e.Y);
            int lineWeight = (int)numericNewLineWeight.Value;
            int lineAlpha = (int)numericNewLineAlpha.Value;
            int fillAlpha = (int)numericNewFillAlpha.Value;
            bool shadow = checkBoxNewShadow.Checked;

            if (dragStarted)
            {
                int dragLeft = Math.Min(dragStart.X, dragEnd.X);
                int dragRight = Math.Max(dragStart.X, dragEnd.X);
                int dragTop = Math.Min(dragStart.Y, dragEnd.Y);
                int dragBottom = Math.Max(dragStart.Y, dragEnd.Y);
                int dragWidth = dragRight - dragLeft;
                int dragHeight = dragBottom - dragTop;
                Point size = new Point(dragWidth, dragHeight);

                dragRect = new Rectangle(dragLeft, dragTop, dragWidth, dragHeight);

                Point upperLeft = new Point(dragLeft, dragTop);
                Point bottomRight = new Point(dragRight, dragBottom);

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
                    SymbolType.Rectangle => new GsRectangle(lineColor, fillColor, shadow, upperLeft, size, lineWeight, lineAlpha, fillAlpha),
                    SymbolType.Circle => new GsCircle(lineColor, fillColor, shadow, upperLeft, size, lineWeight, lineAlpha, fillAlpha),
                    SymbolType.Line => new GsLine(lineColor, fillColor, shadow, dragStart, dragEnd, lineWeight, lineAlpha),
                    SymbolType.Arrow => new GsArrow(lineColor, fillColor, shadow, dragStart, dragEnd, lineWeight, lineAlpha),
                    SymbolType.Image => new GsImage(lineColor, fillColor, shadow, dragEnd, new Point(1, 1)),
                    SymbolType.ImageScaled => new GsImageScaled(lineColor, fillColor, shadow, upperLeft, size),
                    SymbolType.Text => new GsText(lineColor, fillColor, shadow, dragStart, size, lineWeight, lineAlpha),
                    SymbolType.Blur => new GsBlur(lineColor, fillColor, shadow, upperLeft, size, lineWeight, lineAlpha, fillAlpha),
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


        private void ButtonNewText_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Text;
            numericNewLineWeight.Value = lineWeight;
            UpdateOverlay();
        }

        private void ButtonBorder_Click(object sender, EventArgs e)
        {
            lineWeight = (int)numericNewLineWeight.Value;
            //Point upperLeft = new Point(0 + (lineWeight / 2), 0 + (lineWeight /2 ));
            //Point size = new Point(originalImage.Width - lineWeight, originalImage.Height - lineWeight);
            if (originalImage == null)
            {
                return;
            }
            Point upperLeft = new Point(0, 0);
            Point size = new Point(originalImage.Width, originalImage.Height);
            GsBorder border = new GsBorder(Color.Black, Color.White, false, upperLeft, size, lineWeight, 255, 0);
            border.Name = "Border";
            AddNewSymbolToList(border);
            UpdateOverlay();
        }

        private void buttonBlur_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Blur;
            UpdateOverlay();
        }

        #endregion

        #region Mouse input ---------------------------------------------------------------------------------

        bool dragStarted = false;
        Point dragStart = new Point(0, 0);
        //int dragStartX = 0;
        //int dragStartY = 0;
        Rectangle dragRect = new();

        private void PictureBoxOverlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;
            dragStarted = true;
            dragStart = new Point(e.X, e.Y);
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
            UpdatePropertiesPanel();
            dragStarted = false;
            //if (newSymbolType == SymbolType.Image || newSymbolType == SymbolType.ImageScaled)
            //{
            //    // don't repeatedly add pasted images
            //    newSymbolType = SymbolType.None;
            //}
            if (newSymbolType != SymbolType.MoveSymbol)
            {
                newSymbolType = SymbolType.None;
            }
        }

        int oldMouseX = 0;
        int oldMouseY = 0;

        enum MoveType
        {
            None,
            Start,
            End,
        }
        MoveType moveType = MoveType.None;

        private void PictureBoxOverlay_MouseMove(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;
            GraphicSymbol? tempSymbol = GetSymbol(sender, e);
            if (tempSymbol != null)
            {
                UpdateOverlay(tempSymbol, false);
                tempSymbol.Dispose();
            }
            else if (newSymbolType == SymbolType.MoveSymbol)
            {
                int mouseDeltaX = Cursor.Position.X - oldMouseX;
                int mouseDeltaY = Cursor.Position.Y - oldMouseY;
                if (listViewSymbols.SelectedItems.Count > 0)
                {
                    ListViewItem item = listViewSymbols.SelectedItems[0];
                    //Debug.WriteLine($"Mouse delta {mouseDeltaX} {mouseDeltaY}");
                    GraphicSymbol symbol = (GraphicSymbol)item.Tag;
                    if (dragStarted && symbol.MoveAllowed == true)
                    {
                        //GraphicSymbol symbol = (GraphicSymbol)item.Tag;
                        if (moveType == MoveType.None)
                        {
                            Vector2 cursorPosInternal = new Vector2(e.X, e.Y);
                            float distanceToStart = Vector2.Distance(cursorPosInternal, symbol.StartPointV2);
                            float distanceToEnd = Vector2.Distance(cursorPosInternal, symbol.EndPointV2);
                            if (distanceToStart < distanceToEnd || symbol.ScalingAllowed == false)
                            {
                                moveType = MoveType.Start;
                            }
                            else
                            {
                                moveType = MoveType.End;
                            }
                        }
                        if (moveType == MoveType.Start)
                        {
                            int newX = symbol.StartPoint.X + mouseDeltaX;
                            int newY = symbol.StartPoint.Y + mouseDeltaY;
                            newX = Math.Max(-100, newX);
                            newY = Math.Max(-100, newY);
                            newX = Math.Min(newX, originalImage.Width);
                            newY = Math.Min(newY, originalImage.Height);

                            symbol.StartPoint = new Point(newX, newY);
                        }
                        else if (moveType == MoveType.End)
                        {
                            int newX = symbol.EndPoint.X + mouseDeltaX;
                            int newY = Math.Max(0, symbol.EndPoint.Y + mouseDeltaY);
                            newX = Math.Max(0, newX);
                            newY = Math.Max(0, newY);
                            newX = Math.Min(newX, originalImage.Width);
                            newY = Math.Min(newY, originalImage.Height);

                            symbol.EndPoint = new Point(newX, newY);
                        }
                        UpdateOverlay(null, false);
                        oldMouseX = Cursor.Position.X;
                        oldMouseY = Cursor.Position.Y;
                    }
                    else
                    {
                        moveType = MoveType.None;
                        oldMouseX = Cursor.Position.X;
                        oldMouseY = Cursor.Position.Y;
                    }
                }
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
                saveGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                saveGraphic.TextRenderingHint = TextRenderingHint.AntiAlias;
                DrawElements(saveGraphic);
                Clipboard.SetImage(outImage);
                saveGraphic.Dispose();
            }
        }

        private void PasteIntoImage()
        {
            newSymbolType = SymbolType.Image;
            dragStarted = true;
            dragStart = new Point(0, 0);
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
                newSymbolType = SymbolType.MoveSymbol;

            }
            else
            {
                if (newSymbolType == SymbolType.MoveSymbol)
                {
                    newSymbolType = SymbolType.None;
                }
            }
            UpdatePropertiesPanel();
        }

        private void UpdatePropertiesPanel()
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                //newSymbolType = SymbolType.MoveSymbol;
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is GraphicSymbol graphicSymbol)
                {
                    panelPropertiesPosition.Enabled = true;
                    panelPropertiesPosition.Visible = true;
                    panelPropertiesFill.Visible = true;
                    panelPropertiesLine.Visible = true;
                    panelPropertiesText.Visible = false;

                    panelPropertiesFill.Location = new Point(panelPropertiesPosition.Left, panelPropertiesPosition.Bottom + 5);
                    panelPropertiesLine.Location = new Point(panelPropertiesFill.Left, panelPropertiesFill.Bottom + 5);
                    numericPropertiesLineWeight.Enabled = true;

                    numericWidth.Enabled = true;
                    numericHeight.Enabled = true;
                    buttonPropertiesColorLine.Enabled = true;
                    numericPropertiesLineAlpha.Enabled = true;

                    labelSymbolType.Text = "Symbol: " + graphicSymbol.Name;
                    numericX.Value = graphicSymbol.Left;
                    numericY.Value = graphicSymbol.Top;
                    numericWidth.Value = Math.Clamp(graphicSymbol.Width, numericWidth.Minimum, numericWidth.Maximum);
                    numericHeight.Value = Math.Clamp(graphicSymbol.Height, numericHeight.Minimum, numericHeight.Maximum);
                    buttonPropertiesColorLine.BackColor = graphicSymbol.ForegroundColor;
                    buttonPropertiesColorFill.BackColor = graphicSymbol.BackgroundColor;
                    numericPropertiesLineWeight.Value = graphicSymbol.LineWeight;
                    checkBoxPropertiesShadow.Checked = graphicSymbol.ShadowEnabled;
                    buttonDeleteSymbol.Tag = graphicSymbol;

                    if (graphicSymbol is GsImage)
                    {
                        numericWidth.Enabled = false;
                        numericHeight.Enabled = false;
                        panelPropertiesLine.Visible = false;
                        panelPropertiesFill.Visible = false;
                    }

                    if (graphicSymbol is GsImageScaled)
                    {
                        panelPropertiesLine.Visible = false;
                        panelPropertiesFill.Visible = false;
                    }


                    if (graphicSymbol is GsText)
                    {
                        GsText gsText = (GsText)graphicSymbol;

                        panelPropertiesFill.Visible = false;
                        panelPropertiesLine.Visible = true;
                        panelPropertiesText.Visible = true;
                        panelPropertiesLine.Location = new Point(panelPropertiesPosition.Left, panelPropertiesPosition.Bottom + 5);
                        panelPropertiesText.Location = new Point(panelPropertiesLine.Left, panelPropertiesLine.Bottom + 5);

                        numericWidth.Enabled = false;
                        numericHeight.Enabled = false;
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
                    else
                    {
                        textBoxSymbolText.Text = "";
                    }

                    numericPropertiesLineAlpha.Value = graphicSymbol.lineAlpha;
                    numericPropertiesFillAlpha.Value = graphicSymbol.fillAlpha;
                }

            }
            else
            {
                panelPropertiesPosition.Enabled = false;
                panelPropertiesFill.Visible = false;
                panelPropertiesText.Visible = false;
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
            numericPropertiesFontSize.Value = 10;
            buttonDeleteSymbol.Tag = null;
        }

        private void Numeric_ValueChanged(object sender, EventArgs e)
        {
            //if (dragStarted) return;

            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is not GraphicSymbol gs) return;

                if (sender == numericX)
                {
                    gs.Left = (int)numericX.Value;
                }
                if (sender == numericY)
                {
                    gs.Top = (int)numericY.Value;
                }
                if (sender == numericWidth)
                {
                    gs.Width = (int)numericWidth.Value;
                }
                if (sender == numericHeight)
                {
                    gs.Height = (int)numericHeight.Value;
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
            UpdateOverlay();
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
            UpdateOverlay();
        }

        private void textBoxSymbolText_TextChanged(object sender, EventArgs e)
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
            UpdateOverlay();
        }

        private void numericPropertiesFontSize_ValueChanged(object sender, EventArgs e)
        {
            GsText? textSymbol = GetSelectedTextSymbol();
            if (textSymbol != null)
            {
                textSymbol.fontEmSize = (float)numericPropertiesFontSize.Value;
                textSymbol.UpdateFont();
            }
            UpdateOverlay();
        }

        private void comboBoxFontFamily_ValueMemberChanged(object sender, EventArgs e)
        {
            GsText? textSymbol = GetSelectedTextSymbol();
            if (textSymbol != null)
            {
                string selectedFont = comboBoxFontFamily.Text;
                if (fontDictionary.ContainsKey(selectedFont))
                {
                    textSymbol.fontFamily = fontDictionary[selectedFont];
                    textSymbol.UpdateFont();
                }
            }
            UpdateOverlay();
        }

        Dictionary<string, FontFamily> fontDictionary = new();
        private void fillFontFamilyBox()
        {
            List<FontFamily> fontList = System.Drawing.FontFamily.Families.ToList();
            List<string> fontNames = new List<string>();

            foreach (FontFamily font in fontList)
            {
                fontNames.Add(font.Name);
                fontDictionary.Add(font.Name, font);
            }
            comboBoxFontFamily.DataSource = fontNames;
        }

        private void fontStyle_CheckedChanged(object sender, EventArgs e)
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
            UpdateOverlay();
        }

        private void checkBoxPropertiesShadow_Click(object sender, EventArgs e)
        {
            GraphicSymbol? symbol = GetSelectedSymbol();
            if (symbol != null)
            {
                symbol.ShadowEnabled = checkBoxPropertiesShadow.Checked;
            }
            UpdateOverlay();
        }

        private GraphicSymbol? GetSelectedSymbol()
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is GraphicSymbol gs) return gs;
            }
            return null;
        }

        private GsText? GetSelectedTextSymbol()
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is GsText gs) return gs;
            }
            return null;
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



        private void listViewSymbols_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedSymbol();
            }
        }
    }
}
