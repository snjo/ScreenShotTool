using ScreenShotTool.Classes;
using ScreenShotTool.Properties;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.Versioning;

namespace ScreenShotTool.Forms
{
    [SupportedOSPlatform("windows")]
    public partial class ScreenshotEditor : Form
    {
        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern bool SetForegroundWindow(IntPtr hWnd);

        #region Constructor ---------------------------------------------------------------------------------
        Image? originalImage;
        Image? overlayImage;
        Bitmap? blurImage;
        Graphics? overlayGraphics;
        int arrowWeight = 5;
        int lineWeight = 2;
        readonly int frameRate = Settings.Default.MaxFramerate;
        public readonly static int maxFontSize = 200;
        public readonly static int minimumFontSize = 5;
        public readonly static int startingFontSize = 10;
        readonly List<GraphicSymbol> symbols = [];
        private List<ImageFormatDefinition> imageFormats = [];
        readonly int blurRadius = Settings.Default.BlurSampleArea;
        int mosaicSize = Settings.Default.BlurMosaicSize;
        bool initialBlurComplete = false; // used to prevent blur from generating twice, when numeric is set initially
        readonly List<Button> toolButtons = [];
        Size CanvasSize = new(100, 100);

        private void ScreenshotEditor_Load(object sender, EventArgs e)
        {
            //SetForegroundWindow(Handle);
            //Activate();
        }

        private void SetupEditor()
        {
            FillFontFamilyBox();
            imageFormats = CreateImageFormatsList();
            numericPropertiesFontSize.Maximum = maxFontSize;
            numericPropertiesFontSize.Minimum = minimumFontSize;
            numericPropertiesFontSize.Value = startingFontSize;
            panelPropertiesPosition.Enabled = false;
            panelPropertiesFill.Visible = false;
            panelPropertiesLine.Visible = false;
            panelPropertiesText.Visible = false;
            numericBlurMosaicSize.Value = mosaicSize;
            timerAfterLoad.Start(); // turns off TopMost shortly after Load. Without TopMost the Window opens behind other forms (why? who can say)

            toolButtons.Add(buttonSelect);
            toolButtons.Add(buttonRectangle);
            toolButtons.Add(buttonCircle);
            toolButtons.Add(buttonLine);
            toolButtons.Add(buttonArrow);
            toolButtons.Add(buttonText);
            toolButtons.Add(buttonBorder);
            toolButtons.Add(buttonBlur);
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
            //SetForegroundWindow(this.Handle);
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
            CanvasSize = originalImage.Size;
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
                if (originalImage != null)
                {
                    CanvasSize = originalImage.Size;
                }
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
            CanvasSize = originalImage.Size;
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
                    using (FileStream stream = new(filename, FileMode.Open))
                    {
                        tempImage = Image.FromStream(stream);
                    }
                    DisposeAndNull(originalImage);
                    originalImage = tempImage;
                    if (originalImage != null)
                    {
                        CanvasSize = originalImage.Size;
                    }
                }
                catch
                {
                    Debug.WriteLine("Could not load file");

                }
                FlushImages();
            }
        }

        public void SaveImage(string filename, ImageFormat imgFormat)
        {
            if (originalImage != null) // uhm
            {
                Bitmap outImage = new(originalImage);
                Graphics saveGraphic = Graphics.FromImage(outImage);
                saveGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                saveGraphic.TextRenderingHint = TextRenderingHint.AntiAlias;

                DrawElements(saveGraphic);

                Debug.WriteLine($"Saving image {filename} with format {imgFormat}");

                if (imgFormat == ImageFormat.Jpeg)
                {
                    MainForm.SaveJpeg(filename, (Bitmap)outImage, Settings.Default.JpegQuality);
                }
                outImage.Save(filename, imgFormat);
                saveGraphic.Dispose();
                outImage.Dispose();
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
            if (originalImage == null)
            {
                Debug.WriteLine("Couldn't create blur image, originalImage is null");
                return new Bitmap(100, 100);
            }
            Stopwatch sw = new(); // for measuring the time it takes to create the blur image
            sw.Start();
            DisposeAndNull(blurImage);
            mosaicSize = (int)numericBlurMosaicSize.Value;

            blurImage = new Bitmap(originalImage.Width, originalImage.Height);
            Graphics graphics = Graphics.FromImage(blurImage);
            Color pixelColor = Color.Black;
            SolidBrush blurBrush = new(pixelColor);

            for (int x = 0; x < originalImage.Width; x += mosaicSize)
            {
                for (int y = 0; y < originalImage.Height; y += mosaicSize)
                {
                    pixelColor = SamplePixelArea(originalImage, blurRadius, x + blurRadius, y + blurRadius);
                    blurBrush.Color = pixelColor;
                    graphics.FillRectangle(blurBrush, new Rectangle(x, y, mosaicSize, mosaicSize));
                }
            }

            graphics.Dispose();
            sw.Stop();
            Debug.WriteLine($"Blur took {sw.ElapsedMilliseconds}");

            initialBlurComplete = true;
            return blurImage;
        }

        private static Color SamplePixelArea(Image originalImage, int blurRadius, int x, int y)
        {
            Color sampleColor;
            Color pixelColor;
            int sampleX;
            int sampleY;
            int R = 0;
            int G = 0;
            int B = 0;
            int samples = 0;
            for (int i = -blurRadius; i <= blurRadius; i++)
            {
                for (int j = -blurRadius; j <= blurRadius; j++)
                {
                    sampleX = x + i;
                    sampleY = y + j;
                    sampleX = Math.Clamp(sampleX, 0, originalImage.Width - 1);
                    sampleY = Math.Clamp(sampleY, 0, originalImage.Height - 1);
                    sampleColor = ((Bitmap)originalImage).GetPixel(sampleX, sampleY);
                    R += sampleColor.R;
                    G += sampleColor.G;
                    B += sampleColor.B;
                    samples++;
                }
            }
            pixelColor = Color.FromArgb(R / samples, G / samples, B / samples);
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

        private Bitmap DrawOverlay(GraphicSymbol? temporarySymbol = null)
        {
            //Bitmap img = ((Bitmap)(originalImage)).Clone(new Rectangle(0, 0, originalImage.Width, originalImage.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            // clone failed when reading from file, worked before... Using Crop instead, which accounts for out of area pixels.
            Bitmap img;
            if (originalImage != null)
            {
                img = CropImage((Bitmap)originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));
            }
            else
            {
                Debug.WriteLine("Couldn't create correct overlay image, originalImage is null");
                img = new Bitmap(100, 100);
            }
            DisposeAndNull(overlayGraphics);

            overlayGraphics = Graphics.FromImage(img);

            overlayGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            overlayGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            DrawElements(overlayGraphics, temporarySymbol, HighlightSelected: true);

            return img;
        }

        static Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            //https://www.codingdefined.com/2015/04/solved-bitmapclone-out-of-memory.html
            Bitmap bmp = new(cropArea.Width, cropArea.Height);

            using (Graphics gph = Graphics.FromImage(bmp))
            {
                gph.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 100, 100));
                gph.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), cropArea, GraphicsUnit.Pixel);
            }
            return bmp;
        }

        private void DrawElements(Graphics graphic, GraphicSymbol? temporarySymbol = null, bool HighlightSelected = false)
        {
            foreach (GraphicSymbol symbol in symbols)
            {
                InsertImagesInSymbol(symbol);
                symbol.DrawSymbol(graphic);
            }
            if (temporarySymbol != null)
            {
                InsertImagesInSymbol(temporarySymbol);
                temporarySymbol?.DrawSymbol(graphic);
            }
            if (HighlightSelected)
            {
                GraphicSymbol? selectedSymbol = GetSelectedSymbol();
                selectedSymbol?.DrawHitboxes(graphic);
            }
        }

        private void InsertImagesInSymbol(GraphicSymbol symbol)
        {
            if (symbol is GsBlur gsblur)
            {
                if (originalImage != null)
                {
                    gsblur.sourceImage = blurImage;
                }
            }
            else if (symbol is GsDynamicImage gsdi)
            {
                if (originalImage != null)
                {
                    gsdi.sourceImage = (Bitmap)originalImage;
                }
            }
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
            FileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Images (*.png,*.jpg,*.jpeg,*.gif,*.bmp,*.webp)|(*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp)|PNG|*.png|JPG|*.jpg|GIF|*.gif|BMP|*.bmp|All files|*.*"
            };
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadImageFromFile(fileDialog.FileName);
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
            CreateHighlight
        }

        UserActions selectedUserAction = UserActions.None;
        private void SetUserAction(UserActions action)
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
            //if (selectedUserAction == UserActions.CreateBorder) buttonCircle.BackColor = Color.Yellow; // happens right away
            if (selectedUserAction == UserActions.CreateText) buttonText.BackColor = Color.Yellow;
            if (selectedUserAction == UserActions.CreateBlur) buttonBlur.BackColor = Color.Yellow;
            if (selectedUserAction == UserActions.CreateHighlight) buttonHighlight.BackColor = Color.Yellow;

            if (selectedUserAction == UserActions.MoveSymbol)
            {
                //pictureBoxOverlay.Cursor = Cursors.SizeAll;
            }
            else if (selectedUserAction == UserActions.ScaleSymbol)
            {
                //pictureBoxOverlay.Cursor = Cursors.SizeWE;
            }
            else
            {
                pictureBoxOverlay.Cursor = Cursors.Arrow;
            }
        }

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
                    listViewSymbols.Items[^1].Focused = true; // ^1 is listViewSymbols.Items.Count - 1, https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/ranges
                    listViewSymbols.Items[^1].Selected = true;
                    listViewSymbols.Items[^1].EnsureVisible();
                    listViewSymbols.Select();
                }
            }
        }

        private GraphicSymbol? GetNewSymbol(object sender, MouseEventArgs e)
        {
            Point dragEnd = new(e.X, e.Y);
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
                Point size = new(dragWidth, dragHeight);

                //dragRect = new Rectangle(dragLeft, dragTop, dragWidth, dragHeight);

                Point upperLeft = new(dragLeft, dragTop);
                //Point bottomRight = new(dragRight, dragBottom);

                Color lineColor = buttonNewColorLine.BackColor;
                Color fillColor = Color.FromArgb((int)numericNewFillAlpha.Value, buttonNewColorFill.BackColor);

                return selectedUserAction switch
                {
                    UserActions.CreateRectangle => new GsRectangle(upperLeft, size, lineColor, fillColor, shadow, lineWeight, lineAlpha, fillAlpha),
                    UserActions.CreateCircle => new GsCircle(upperLeft, size, lineColor, fillColor, shadow, lineWeight, lineAlpha, fillAlpha),
                    UserActions.CreateLine => new GsLine(dragStart, dragEnd, lineColor, fillColor, shadow, lineWeight, lineAlpha),
                    UserActions.CreateArrow => new GsArrow(dragStart, dragEnd, lineColor, fillColor, shadow, lineWeight, lineAlpha),
                    UserActions.CreateImage => new GsImage(dragEnd, new Point(1, 1), shadow),
                    UserActions.CreateImageScaled => new GsImageScaled(upperLeft, size, shadow),
                    UserActions.CreateText => new GsText(dragStart, size, lineColor, fillColor, shadow, lineWeight, lineAlpha),
                    UserActions.CreateBlur => new GsBlur(upperLeft, size, lineColor, fillColor),
                    UserActions.CreateHighlight => new GsHighlight(upperLeft, size, lineColor, Color.Yellow, false, 0, 0, fillAlpha),
                    _ => null,
                };
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Get symbols and hitboxes --------------------------------------------------------------------

        enum HitboxDirection
        {
            None = -1,
            Center = 0,
            NW = 1,
            N = 2,
            NE = 3,
            W = 4,
            E = 5,
            SW = 6,
            S = 7,
            SE = 8,
        }
        HitboxDirection selectedHitboxIndex = HitboxDirection.None;

        private void GetHitboxUnderCursor(MouseEventArgs e)
        {
            if (currentSelectedSymbol != null)
            {
                selectedHitboxIndex = HitboxDirection.None;
                for (int i = 1; i <= 8; i++) // all scaling hitboxec, not center
                {
                    if (currentSelectedSymbol.GetHitbox(i).Contains(e.X, e.Y))
                    {
                        selectedHitboxIndex = (HitboxDirection)i;

                        break;
                    }
                }
                if (selectedHitboxIndex == HitboxDirection.None)
                {
                    if (currentSelectedSymbol.GetHitbox(0).Contains(e.X, e.Y))
                    {
                        selectedHitboxIndex = HitboxDirection.Center;
                    }
                }
            }
        }

        private void SelectSymbolUnderCursor()
        {
            List<GraphicSymbol> symbolsUnderCursor = GetSymbolsUnderCursor();
            if (symbolsUnderCursor.Count == 0 && selectedUserAction != UserActions.ScaleSymbol)
            {
                // empty stack
                stackedSymbolsIndex = -1;
                previousTopmostSymbol = null;
                listViewSymbols.SelectedItems.Clear();
                currentSelectedSymbol = null;
            }
            else
            {
                if (stackedSymbolsIndex == -1 || stackedSymbolsIndex >= symbolsUnderCursor.Count)
                {
                    stackedSymbolsIndex = Math.Max(symbolsUnderCursor.Count - 1, 0);
                }

                if (symbolsUnderCursor.Count > 0)
                {
                    currentSelectedSymbol = symbolsUnderCursor[Math.Clamp(stackedSymbolsIndex, 0, symbolsUnderCursor.Count - 1)];
                    listViewSymbols.SelectedItems.Clear();
                    ListViewItem? listFromSymbol = GetListItemFromSymbol(currentSelectedSymbol);
                    int selectedIndex = listFromSymbol != null ? listFromSymbol.Index : -1;
                    if (selectedIndex > -1 && selectedIndex < listViewSymbols.Items.Count)
                    {
                        listViewSymbols.Items[selectedIndex].Selected = true;
                    }
                    stackedSymbolsIndex--;
                }
                else
                {
                    listViewSymbols.SelectedItems.Clear();
                    currentSelectedSymbol = null;
                    stackedSymbolsIndex = -1;
                }
            }
        }

        private List<GraphicSymbol> GetSymbolsUnderCursor()
        {
            List<GraphicSymbol> symbolsUnderCursor = [];
            Point cursorPos = pictureBoxOverlay.PointToClient(Cursor.Position);
            foreach (GraphicSymbol gs in symbols)
            {
                if (gs.Bounds.Contains(cursorPos) && (gs is GsBorder) == false) // don't select the border symbol, since it covers everything
                {
                    symbolsUnderCursor.Add(gs);
                }
            }
            return symbolsUnderCursor;
        }

        private ListViewItem? GetListItemFromSymbol(GraphicSymbol symbol)
        {
            foreach (ListViewItem lvi in listViewSymbols.Items)
            {
                if (lvi.Tag == symbol)
                {
                    return lvi;
                }
            }
            return null;
        }

        private static GraphicSymbol GetSymbolFromTag(ListViewItem lvi)
        {
            object? tag = lvi.Tag;
            if (tag == null)
            {
                throw new NullReferenceException($"ListviewItem {lvi.Name} tag is null");
            }
            else if (lvi.Tag is GraphicSymbol symbol)
            {
                return symbol;
            }
            else
            {
                throw new InvalidCastException($"ListviewItem {lvi.Name} tag is not a GraphicSymbol");
            }
        }

        #endregion

        #region Symbol toolbar buttons ----------------------------------------------------------------------

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            SetUserAction(UserActions.Select);
            UpdateOverlay();
        }

        private void ButtonRectangle_Click(object sender, EventArgs e)
        {
            SetUserAction(UserActions.CreateRectangle);
            numericNewLineWeight.Value = lineWeight;
            UpdateOverlay();
        }

        private void ButtonCircle_Click(object sender, EventArgs e)
        {
            SetUserAction(UserActions.CreateCircle);
            numericNewLineWeight.Value = lineWeight;
            UpdateOverlay();
        }

        private void ButtonLine_Click(object sender, EventArgs e)
        {
            SetUserAction(UserActions.CreateLine);
            numericNewLineWeight.Value = lineWeight;
            UpdateOverlay();
        }

        private void ButtonArrow_Click(object sender, EventArgs e)
        {
            SetUserAction(UserActions.CreateArrow);
            numericNewLineWeight.Value = arrowWeight;
            UpdateOverlay();
        }


        private void ButtonNewText_Click(object sender, EventArgs e)
        {
            SetUserAction(UserActions.CreateText);
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
            Point upperLeft = new(0, 0);
            Point size = new(originalImage.Width, originalImage.Height);
            GsBorder border = new(upperLeft, size, Color.Black, Color.White, false, lineWeight, 255, 0)
            {
                Name = "Border"
            };
            AddNewSymbolToList(border);
            UpdateOverlay();
        }

        private void ButtonBlur_Click(object sender, EventArgs e)
        {
            SetUserAction(UserActions.CreateBlur);
            UpdateOverlay();
        }

        private void ButtonHighlight_Click(object sender, EventArgs e)
        {
            SetUserAction(UserActions.CreateHighlight);
            UpdateOverlay();
        }

        #endregion

        #region Mouse input ---------------------------------------------------------------------------------

        Point oldMousePosition = new(0, 0);
        //int oldMouseY = 0;
        //enum MoveType
        //{
        //    None,
        //    Start,
        //    End,
        //}
        //MoveType moveType = MoveType.None;



        private void PictureBoxOverlay_MouseLeave(object sender, EventArgs e)
        {
            //dragStarted = false;
        }

        bool dragStarted = false;
        bool dragMoved = false;
        Point dragStart = new(0, 0);
        Point dragStartOffsetFromSymbolCenter = new(0, 0);

        private void PictureBoxOverlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;
            dragStarted = true;
            dragMoved = false;
            dragStart = new Point(e.X, e.Y);
            if (currentSelectedSymbol != null)
            {
                dragStartOffsetFromSymbolCenter = new Point(e.X, e.Y).Subtract(currentSelectedSymbol.Position);
            }

            GetHitboxUnderCursor(e);
            if (selectedUserAction < UserActions.CreateRectangle) // action is none, move or scale
            {
                if (selectedHitboxIndex == HitboxDirection.Center)
                {
                    SetUserAction(UserActions.MoveSymbol);
                }
                else if (selectedHitboxIndex > HitboxDirection.Center)
                {
                    SetUserAction(UserActions.ScaleSymbol);
                }
                else
                {
                    SetUserAction(UserActions.Select);
                }
            }

            if (overlayGraphics == null)
            {
                CreateOverlay();
            }
        }

        private void PictureBoxOverlay_MouseMove(object sender, MouseEventArgs e)
        {
            Point MousePosition = new(e.X, e.Y);
            if (dragStarted == false) // don't update the selected hitbox index while a drag scale is active
            {
                GetHitboxUnderCursor(e);
            }

            pictureBoxOverlay.Cursor = Cursors.Arrow;
            if (currentSelectedSymbol != null)
            {

                if (currentSelectedSymbol.MoveAllowed)
                {
                    if (selectedHitboxIndex == HitboxDirection.Center)
                    {
                        pictureBoxOverlay.Cursor = Cursors.SizeAll;
                    }
                }
                if (currentSelectedSymbol.ScalingAllowed)
                {
                    switch (selectedHitboxIndex)
                    {
                        case HitboxDirection.NW: case HitboxDirection.SE: pictureBoxOverlay.Cursor = Cursors.SizeNWSE; break;
                        case HitboxDirection.NE: case HitboxDirection.SW: pictureBoxOverlay.Cursor = Cursors.SizeNESW; break;
                        case HitboxDirection.W: case HitboxDirection.E: pictureBoxOverlay.Cursor = Cursors.SizeWE; break;
                        case HitboxDirection.N: case HitboxDirection.S: pictureBoxOverlay.Cursor = Cursors.SizeNS; break;
                    }
                }
                if (currentSelectedSymbol is GsLine)
                {
                    if ((int)selectedHitboxIndex == 1 || (int)selectedHitboxIndex == 2)
                    {
                        pictureBoxOverlay.Cursor = Cursors.SizeAll;
                    }
                }
            }

            if (e.X != dragStart.X || e.Y != dragStart.Y)
            {
                dragMoved = true;
            }
            if (originalImage == null) return;

            //Point mouseDelta = MousePosition.Subtract(oldMousePosition);


            if (selectedUserAction >= UserActions.CreateRectangle) // any UserAction above CreateRectangle is a new symbol creation
            {
                CreateTempSymbol(sender, e);
            }
            else if (selectedUserAction == UserActions.MoveSymbol)
            {
                MoveSymbol(e);
                UpdateOverlay();
            }
            else if (selectedUserAction == UserActions.ScaleSymbol)
            {
                ScaleSymbol(e);
                UpdateOverlay();
            }

            oldMousePosition = MousePosition;
        }

        private void MoveSymbol(MouseEventArgs e)
        {
            if (currentSelectedSymbol == null) return;

            if (currentSelectedSymbol.MoveAllowed && dragStarted)
            {
                Point newPos = new(
                    Math.Clamp(e.X - dragStartOffsetFromSymbolCenter.X, -100, CanvasSize.Width + 100),
                    Math.Clamp(e.Y - dragStartOffsetFromSymbolCenter.Y, -100, CanvasSize.Height + 100)
                );
                currentSelectedSymbol.MoveTo(newPos.X, newPos.Y);
            }
        }

        private void ScaleSymbol(MouseEventArgs e)
        {
            if (currentSelectedSymbol == null) return;
            if (dragStarted == false) return;

            if (currentSelectedSymbol is GsLine)
            {
                if ((int)selectedHitboxIndex == 1)
                {
                    currentSelectedSymbol.StartPoint = new Point(e.X, e.Y);
                }
                if ((int)selectedHitboxIndex == 2)
                {
                    currentSelectedSymbol.EndPoint = new Point(e.X, e.Y);
                }
            }
            else if (currentSelectedSymbol.ScalingAllowed)
            {
                if (selectedHitboxIndex == HitboxDirection.W || selectedHitboxIndex == HitboxDirection.NW || selectedHitboxIndex == HitboxDirection.SW)
                {
                    currentSelectedSymbol.MoveLeftEdgeTo(e.X);
                }
                if (selectedHitboxIndex == HitboxDirection.E || selectedHitboxIndex == HitboxDirection.NE || selectedHitboxIndex == HitboxDirection.SE)
                {
                    currentSelectedSymbol.MoveRightEdgeTo(e.X);
                }
                if (selectedHitboxIndex == HitboxDirection.N || selectedHitboxIndex == HitboxDirection.NE || selectedHitboxIndex == HitboxDirection.NW)
                {
                    currentSelectedSymbol.MoveTopEdgeTo(e.Y);
                }
                if (selectedHitboxIndex == HitboxDirection.S || selectedHitboxIndex == HitboxDirection.SE || selectedHitboxIndex == HitboxDirection.SW)
                {
                    currentSelectedSymbol.MoveBottomEdgeTo(e.Y);
                }
            }
        }


        private void CreateTempSymbol(object sender, MouseEventArgs e)
        {
            GraphicSymbol? tempSymbol = GetNewSymbol(sender, e);
            if (tempSymbol != null)
            {
                UpdateOverlay(tempSymbol, false);
                tempSymbol.Dispose();
            }
        }

        GraphicSymbol? currentSelectedSymbol = null;
        GraphicSymbol? previousTopmostSymbol = null;
        int stackedSymbolsIndex = -1;
        private void PictureBoxOverlay_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragMoved == false) // user clicked and released mouse without moving it
            {
                SelectSymbolUnderCursor();
            }

            if (originalImage == null) return;
            GraphicSymbol? symbol = GetNewSymbol(sender, e);
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

            if (selectedUserAction != UserActions.MoveSymbol)
            {
                SetUserAction(UserActions.Select);
            }
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
        }
        #endregion

        #region Copy and Paste ------------------------------------------------------------------------------

        public void CopyToClipboard()
        {
            if (originalImage != null)
            {
                Bitmap outImage = new(originalImage);
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
            SetUserAction(UserActions.CreateImage);
            dragStarted = true;
            dragStart = new Point(0, 0);
            UpdateOverlay();
        }

        private void PasteIntoImageScaled()
        {
            SetUserAction(UserActions.CreateImageScaled);
            UpdateOverlay();
        }

        #endregion

        #region Selected symbol Properties panel ------------------------------------------------------------

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
                    panelPropertiesHighlight.Visible = false;

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


                    if (graphicSymbol is GsText gsText)
                    {
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

                    if (graphicSymbol is GsHighlight gsHL)
                    {
                        panelPropertiesHighlight.Visible = true;
                        panelPropertiesFill.Visible = true;
                        panelPropertiesLine.Visible = false;
                        panelPropertiesHighlight.Location = new Point(panelPropertiesFill.Left, panelPropertiesFill.Bottom + 5);
                        comboBoxBlendMode.Text = gsHL.blendMode.ToString();
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
            UpdateOverlay();
        }

        private void NumericPropertiesFontSize_ValueChanged(object sender, EventArgs e)
        {
            GsText? textSymbol = GetSelectedTextSymbol();
            if (textSymbol != null)
            {
                textSymbol.fontEmSize = (float)numericPropertiesFontSize.Value;
                textSymbol.UpdateFont();
            }
            UpdateOverlay();
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
            UpdateOverlay();
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
            UpdateOverlay();
        }

        private void CheckBoxPropertiesShadow_Click(object sender, EventArgs e)
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
                if (item.Tag is GraphicSymbol gs)
                {
                    currentSelectedSymbol = gs;
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
            if (currentSelectedSymbol is GsHighlight gshl)
            {
                ColorBlend.BlendModes newBlend = ColorBlend.BlendModes.Darken;
                if (Enum.TryParse<ColorBlend.BlendModes>(comboBoxBlendMode.Text, out newBlend))
                {
                    gshl.blendMode = newBlend;
                    //gshl.Update
                }
                UpdateOverlay();
            }
        }

        #endregion

        #region Top toolbar, new Symbol settings ------------------------------------------------------------

        private void NumericNewLineWeight_ValueChanged(object sender, EventArgs e)
        {
            if (selectedUserAction == UserActions.CreateArrow)
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

        private void ListViewSymbols_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedSymbol();
            }
        }

        private void NumericBlurMosaicSize_Click(object sender, EventArgs e)
        {

        }

        private void NumericBlurMosaicSize_ValueChanged(object sender, EventArgs e)
        {
            if (initialBlurComplete)
            {
                CreateBlurImage();
                UpdateOverlay();
            }
        }

        private void TimerAfterLoad_Tick(object sender, EventArgs e)
        {
            timerAfterLoad.Stop();
            this.TopMost = false;
        }
    }
}
