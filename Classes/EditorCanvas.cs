using ScreenShotTool.Classes;
using ScreenShotTool.Forms;
using ScreenShotTool.Properties;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.Versioning;
using static ScreenShotTool.Forms.ScreenshotEditor;

namespace ScreenShotTool;
[SupportedOSPlatform("windows")]
public class EditorCanvas(ScreenshotEditor parent, PictureBox pictureBox)
{
    public Bitmap? SourceImage;
    Image? OverlayImage;
    Bitmap? blurImage;
    Graphics? overlayGraphics;
    public int ArrowWeight = 5;
    public int LineWeight = 2;
    readonly int frameRate = Settings.Default.MaxFramerate;
    
    readonly ScreenshotEditor parentEditor = parent;

    public int blurRadius = Settings.Default.BlurSampleArea;
    public int mosaicSize = Settings.Default.BlurMosaicSize;
    public bool InitialBlurComplete = false; // used to prevent blur from generating twice, when numeric is set initially

    public int OutOfBoundsMaxPixels = 1000;
    public Size CanvasSize = new(100, 100); // will be update when the image loads
    public Rectangle CanvasRect
    {
        get
        {
            return new Rectangle(0,0, CanvasSize.Width, CanvasSize.Height);
        }
    }
    readonly PictureBox pictureBox = pictureBox;
    public GraphicSymbol? currentSelectedSymbol = null;

    public void UpdateSourceImage(Bitmap bitmap)
    {
        SourceImage = bitmap;
    }

    public List<GraphicSymbol> symbols
    {
        get
        {
            List<GraphicSymbol> gsList = new();
            foreach (ListViewItem lvi in parentEditor.GetSymbolListView().Items)
            {
                if (lvi.Tag is GraphicSymbol gs)
                {
                    gsList.Add(gs);
                }
            }
            return gsList;
        }
    }

    #region Create and destroy --------------------------------------------------------------------------

    public void FlushImages(bool deleteSymbols = false)
    {
        // used at the end of each Load/Create image
        DisposeAndNull(overlayGraphics);
        DisposeAndNull(OverlayImage);
        DisposeAndNull(blurImage);
        if (deleteSymbols)
        {
            DeleteAllSymbols();
        }
    }

    public void DeleteAllSymbols()
    {
        parentEditor.DeleteListViewSymbols();
        //foreach (GraphicSymbol symbol in symbols)
        //{
        //    parentEditor.ClearPropertyPanelValues();
        //    symbol.Dispose();
        //}
        //symbols.Clear();
        UpdateOverlay();
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

    #region Load and Save -------------------------------------------------------------------------------

    internal void CreateNewImage(int Width, int Height, Color color)
    {
        DisposeAndNull(SourceImage);
        SourceImage = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
        UpdateCanvasSize(SourceImage.Size);
        Graphics g = Graphics.FromImage(SourceImage);
        g.FillRectangle(new SolidBrush(color), 0, 0, Width, Height);
        FlushImages(true);
        CreateOverlay(pictureBox);
        UpdateOverlay();
    }

    private static Bitmap? ImageToBitmap32bppArgb(Image? img, bool disposeSource)
    {
        Bitmap? clone = null;
        if (img != null)
        {
            clone = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(img, new Rectangle(0, 0, clone.Width, clone.Height));
            }
            if (disposeSource)
            {
                img.Dispose();
            }
        }
        return clone;
    }

    public void LoadImageFromClipboard()
    {
        try
        {
            DisposeAndNull(SourceImage);
            SourceImage = ImageToBitmap32bppArgb(Clipboard.GetImage(), true); //(Bitmap)Clipboard.GetImage(); 
            if (SourceImage != null)
            {
                UpdateCanvasSize(SourceImage.Size);
            }
            else
            {
                Debug.WriteLine("Couldn't load image from clipboard");
                MessageBox.Show("Couldn't load image from clipboard.\nUsing blank image.");
                CreateNewImage(640, 480, Color.White);
            }
        }
        catch
        {
            Debug.WriteLine("Could not load from clipboard");
            return;
        }
        FlushImages(true);
        CreateOverlay(pictureBox);
        UpdateOverlay();
    }

    public void LoadImageFromImage(Image image, bool deleteAllSymbols)
    {
        DisposeAndNull(SourceImage);
        SourceImage = ImageToBitmap32bppArgb(image, true);
        if (SourceImage != null)
        {
            UpdateCanvasSize(SourceImage.Size);
        }
        else
        {
            Debug.WriteLine("Couldn't load image from other image object");
            MessageBox.Show("Error: Couldn't load image.\nUsing blank image.");
            CreateNewImage(640, 480, Color.White);
        }
        FlushImages(deleteAllSymbols);
        CreateOverlay(pictureBox);
        UpdateOverlay();
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
                DisposeAndNull(SourceImage);
                SourceImage = ImageToBitmap32bppArgb(tempImage, true);
                if (SourceImage != null)
                {
                    UpdateCanvasSize(SourceImage.Size);
                }
            }
            catch
            {
                Debug.WriteLine("Could not load file");

            }
            FlushImages(true);
            CreateOverlay(pictureBox);
            UpdateOverlay();
        }
    }

    public Bitmap? AssembleImageForSaveOrCopy()
    {
        if (SourceImage == null) return null;
        Bitmap outImage = new(SourceImage);
        Graphics saveGraphic = Graphics.FromImage(outImage);
        saveGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        saveGraphic.TextRenderingHint = TextRenderingHint.AntiAlias;
        DrawElements(saveGraphic);
        Clipboard.SetImage(outImage);
        saveGraphic.Dispose();
        return outImage;
    }

    #endregion

    #region Create and Update overlay -------------------------------------------------------------------

    private void UpdateCanvasSize(Size size)
    {
        CanvasSize = size;
        parentEditor.UpdateNumericLimits();
    }

    private void CreateOverlay(PictureBox pictureBox)
    {
        if (SourceImage != null)
        {
            OverlayImage?.Dispose();
            overlayGraphics?.Dispose();
            OverlayImage = new Bitmap(SourceImage.Width, SourceImage.Height);
            overlayGraphics = Graphics.FromImage(OverlayImage);
            //blurImage = CreateBlurImage(mosaicSize, SourceImage, CanvasRect);

            //overlayGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            //overlayGraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit; // fixes ugly aliasing on text
            overlayGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            overlayGraphics.TextRenderingHint = TextRenderingHint.AntiAlias; // fixes ugly aliasing on text

            pictureBox.Image = OverlayImage;
            pictureBox.Width = SourceImage.Width;
            pictureBox.Height = SourceImage.Height;
        }
        else
        {
            //Debug.WriteLine("Create overlay failed, original image is null");
        }
    }

    public Bitmap CreateBlurImage(int blurPixelSize, Bitmap? sourceBitmap, Rectangle blurBounds)
    {
        if (sourceBitmap == null)
        {
            Debug.WriteLine("Couldn't create blur image, originalImage is null");
            return new Bitmap(100, 100);
        }
        Stopwatch sw = new(); // for measuring the time it takes to create the blur image
        sw.Start();
        DisposeAndNull(blurImage);

        blurImage = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
        Graphics graphics = Graphics.FromImage(blurImage);
        Color pixelColor = Color.Black;
        SolidBrush blurBrush = new(pixelColor);

        

        using (var snoop = new BmpPixelSnoop((Bitmap)sourceBitmap))
        {
            int tilesDrawn = 0;
            for (int x = Math.Max(0, blurBounds.Left); x < sourceBitmap.Width && x < blurBounds.Right; x += mosaicSize)
            {
                for (int y = Math.Max(0, blurBounds.Top); y < sourceBitmap.Height && y < blurBounds.Bottom; y += mosaicSize)
                {
                    pixelColor = SamplePixelArea(snoop, blurRadius, x + blurRadius, y + blurRadius);
                    blurBrush.Color = pixelColor;
                    graphics.FillRectangle(blurBrush, new Rectangle(x, y, mosaicSize, mosaicSize));
                    tilesDrawn++;
                }
            }
            //Debug.WriteLine($"Mosaic, drew {tilesDrawn} tiles, inside {blurBounds}");
        }

        graphics.Dispose();
        sw.Stop();
        //Debug.WriteLine($"Blur took {sw.ElapsedMilliseconds}");

        InitialBlurComplete = true;
        return blurImage;
    }

    //private static Color SamplePixelArea(Image originalImage, int blurRadius, int x, int y)
    private static Color SamplePixelArea(BmpPixelSnoop sourceImage, int blurRadius, int x, int y)
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
                sampleX = Math.Clamp(sampleX, 0, sourceImage.Width - 1);
                sampleY = Math.Clamp(sampleY, 0, sourceImage.Height - 1);
                sampleColor = sourceImage.GetPixel(sampleX, sampleY);
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
    int skippedUpdates = 0;
    public bool UpdateOverlay(GraphicSymbol? temporarySymbol = null, bool forceUpdate = true)
    {
        //Stopwatch sw = Stopwatch.StartNew();
        float MilliSecondsPerFrame = (1f / frameRate) * 1000;
        TimeSpan ts = DateTime.Now - LastFrame;
        if (ts.Milliseconds < MilliSecondsPerFrame && forceUpdate == false)
        {
            skippedUpdates++;
            return false;
        }

        if (OverlayImage == null || overlayGraphics == null)
        {
            CreateOverlay(pictureBox);
        }
        if (OverlayImage != null && overlayGraphics != null)
        {
            pictureBox.Image.Dispose();
            pictureBox.Image = DrawOverlay(temporarySymbol); ;
        }
        //sw.Stop();
        //Debug.WriteLine($"MS since last frame: {ts.Milliseconds}");
        LastFrame = DateTime.Now;
        return true;
    }

    Bitmap? imageInProgress;
    private Bitmap DrawOverlay(GraphicSymbol? temporarySymbol = null)
    {
        DisposeAndNull(imageInProgress);
        //Bitmap img;
        if (SourceImage != null)
        {
            imageInProgress = CropImage((Bitmap)SourceImage, new Rectangle(0, 0, SourceImage.Width, SourceImage.Height));
        }
        else
        {
            Debug.WriteLine("Couldn't create correct overlay image, originalImage is null");
            imageInProgress = new Bitmap(100, 100);
        }
        DisposeAndNull(overlayGraphics);

        overlayGraphics = Graphics.FromImage(imageInProgress);

        overlayGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        overlayGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

        DrawElements(overlayGraphics, temporarySymbol, HighlightSelected: true);

        return imageInProgress;
    }

    public static Bitmap CropImage(Bitmap img, Rectangle cropArea)
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
        int NumberedSymbolCounter = 1;
        if (SourceImage == null) return;
        foreach (GraphicSymbol symbol in symbols)
        {
            if (symbol is GsDynamicImage gsDn)
            {
                if (gsDn is GsBlur gsBlur)
                {
                    DisposeAndNull(gsBlur.SourceImage);
                    gsBlur.SourceImage = CreateBlurImage(mosaicSize, imageInProgress, gsBlur.Bounds);
                }
                else
                {
                    gsDn.SourceImage = imageInProgress;
                }
            }
            if (symbol is GsNumbered gsNumbered)
            {
                gsNumbered.Number = NumberedSymbolCounter;
                NumberedSymbolCounter++;
            }
            symbol.ContainerBounds = new Rectangle(0, 0, SourceImage.Width, SourceImage.Height);
            //InsertImagesInSymbol(symbol);
            symbol.DrawSymbol(graphic);
        }
        if (temporarySymbol != null)
        {
            if (temporarySymbol is GsNumbered gsNumbered)
            {
                gsNumbered.Number = NumberedSymbolCounter;
            }
            if (temporarySymbol is GsDynamicImage gsDn)
            {
                if (gsDn is GsBlur gsBlur)
                {
                    DisposeAndNull(gsBlur.SourceImage);
                    gsBlur.SourceImage = CreateBlurImage(mosaicSize, imageInProgress, gsBlur.Bounds);
                }
                else
                {
                    gsDn.SourceImage = imageInProgress;
                }
            }
            //InsertImagesInSymbol(temporarySymbol);
            temporarySymbol?.DrawSymbol(graphic);
        }
        if (HighlightSelected)
        {
            GraphicSymbol? selectedSymbol = parentEditor.GetSelectedSymbol();
            selectedSymbol?.DrawHitboxes(graphic);
        }
    }

    private void InsertImagesInSymbol(GraphicSymbol symbol)
    {
        if (symbol is GsBlur gsblur)
        {
            if (SourceImage != null)
            {
                gsblur.SourceImage = blurImage;
            }
        }
        else if (symbol is GsDynamicImage gsdi)
        {
            if (SourceImage != null)
            {
                gsdi.SourceImage = (Bitmap)SourceImage;
            }
        }
    }
    #endregion

    #region Symbols -------------------------------------------------------------------------------------

    private Point RestrainToSquare(Point dragStart, Point dragEnd)
    {
        Point size = dragEnd.Subtract(dragStart);
        int width = Math.Abs(size.X);
        int height = Math.Abs(size.Y);
        int xFlip = size.X < 0 ? -1 : 1;
        int yFlip = size.Y < 0 ? -1 : 1;
        int squareSide = Math.Min(width, height);
        return new Point(dragStart.X + (xFlip * squareSide), dragStart.Y + (yFlip * squareSide));
    }

    private GraphicSymbol? GetNewSymbol(Point MousePosition, bool SquareBounds)
    {
        Point dragEnd = MousePosition;
        int lineWeight = parentEditor.GetNewSymbolProperties().lineWeight;
        //int lineAlpha = parentEditor.GetNewSymbolProperties().lineAlpha;
        //int fillAlpha = parentEditor.GetNewSymbolProperties().fillAlpha;
        bool shadow = parentEditor.GetNewSymbolProperties().shadow;

        if (SquareBounds)
        {
            dragEnd = RestrainToSquare(dragStart, dragEnd);
        }

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

            Color lineColor = parentEditor.GetNewSymbolProperties().lineColor;
            Color fillColor = parentEditor.GetNewSymbolProperties().fillColor;
            //Color.FromArgb((int)numericNewFillAlpha.Value, buttonNewColorFill.BackColor);
            int NumberedSize = Settings.Default.GsNumberedDefaultSize;

            return parentEditor.selectedUserAction switch
            {
                ScreenshotEditor.UserActions.CreateRectangle => new GsRectangle(upperLeft, size, lineColor, fillColor, shadow, lineWeight),
                ScreenshotEditor.UserActions.CreateCircle => new GsCircle(upperLeft, size, lineColor, fillColor, shadow, lineWeight),
                ScreenshotEditor.UserActions.CreateLine => new GsLine(dragStart, dragEnd, lineColor, fillColor, shadow, lineWeight),
                ScreenshotEditor.UserActions.CreateArrow => new GsArrow(dragStart, dragEnd, lineColor, fillColor, shadow, lineWeight),
                ScreenshotEditor.UserActions.CreateImage => new GsImage(dragEnd, new Point(1, 1), shadow),
                ScreenshotEditor.UserActions.CreateImageScaled => new GsImageScaled(upperLeft, size, shadow),
                ScreenshotEditor.UserActions.CreateText => new GsText(dragStart, size, lineColor, fillColor, shadow),
                ScreenshotEditor.UserActions.CreateBlur => new GsBlur(upperLeft, size, lineColor, fillColor),
                ScreenshotEditor.UserActions.CreateHighlight => new GsHighlight(upperLeft, size, lineColor, Color.Yellow, false, 0),
                ScreenshotEditor.UserActions.CreateCrop => new GsCrop(upperLeft, size, lineColor, fillColor),
                ScreenshotEditor.UserActions.CreateNumbered => new GsNumbered(new Point(dragEnd.X - (NumberedSize / 2), dragEnd.Y - (NumberedSize / 2)), new Point(NumberedSize, NumberedSize), lineColor, fillColor, shadow, lineWeight),
                _ => null,
            };
        }
        else
        {
            return null;
        }
    }

    private void CreateTempSymbol(Point MousePosition)
    {
        GraphicSymbol? tempSymbol = GetNewSymbol(MousePosition, parentEditor.GetShift());
        if (tempSymbol != null)
        {
            UpdateOverlay(tempSymbol, forceUpdate: false);
            tempSymbol.Dispose();
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

    private void GetHitboxUnderCursor(Point MousePosition)
    {
        if (currentSelectedSymbol != null)
        {
            selectedHitboxIndex = HitboxDirection.None;
            for (int i = 1; i <= 8; i++) // all scaling hitboxec, not center
            {
                if (currentSelectedSymbol.GetHitbox(i).Contains(MousePosition.X, MousePosition.Y))
                {
                    selectedHitboxIndex = (HitboxDirection)i;

                    break;
                }
            }
            if (selectedHitboxIndex == HitboxDirection.None)
            {
                if (currentSelectedSymbol.GetHitbox(0).Contains(MousePosition.X, MousePosition.Y))
                {
                    selectedHitboxIndex = HitboxDirection.Center;
                }
            }
        }
    }

    private void SelectSymbolUnderCursor()
    {
        List<GraphicSymbol> symbolsUnderCursor = GetSymbolsUnderCursor();
        if (symbolsUnderCursor.Count == 0 && parentEditor.selectedUserAction != ScreenshotEditor.UserActions.ScaleSymbol)
        {
            // empty stack
            stackedSymbolsIndex = -1;
            //previousTopmostSymbol = null;
            parentEditor.GetSymbolListView().SelectedItems.Clear();
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
                parentEditor.GetSymbolListView().SelectedItems.Clear();
                ListViewItem? listFromSymbol = GetListItemFromSymbol(currentSelectedSymbol);
                int selectedIndex = listFromSymbol != null ? listFromSymbol.Index : -1;
                if (selectedIndex > -1 && selectedIndex < parentEditor.GetSymbolListView().Items.Count)
                {
                    parentEditor.GetSymbolListView().Items[selectedIndex].Selected = true;
                }
                stackedSymbolsIndex--;
            }
            else
            {
                parentEditor.GetSymbolListView().SelectedItems.Clear();
                currentSelectedSymbol = null;
                stackedSymbolsIndex = -1;
            }
        }
    }

    private List<GraphicSymbol> GetSymbolsUnderCursor()
    {
        List<GraphicSymbol> symbolsUnderCursor = [];
        Point cursorPos = pictureBox.PointToClient(Cursor.Position);
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
        foreach (ListViewItem lvi in parentEditor.GetSymbolListView().Items)
        {
            if (lvi.Tag == symbol)
            {
                return lvi;
            }
        }
        return null;
    }

    #endregion

    #region Mouse input ---------------------------------------------------------------------------------

    Point oldMousePosition = new(0, 0);

    private void SetUserAction(ScreenshotEditor.UserActions action)
    {
        parentEditor.SetUserAction(action);
    }

    public bool dragStarted = false;
    public bool dragMoved = false;
    public Point dragStart = new(0, 0);
    Point dragStartOffsetFromSymbolCenter = new(0, 0);

    public void MouseDown(Point MousePosition)
    {
        if (SourceImage == null) return;
        dragStarted = true;
        dragMoved = false;
        dragStart = MousePosition;
        if (currentSelectedSymbol != null)
        {
            dragStartOffsetFromSymbolCenter = MousePosition.Subtract(currentSelectedSymbol.Position);
        }

        GetHitboxUnderCursor(MousePosition);
        if (parentEditor.selectedUserAction < ScreenshotEditor.UserActions.CreateRectangle) // action is none, move or scale
        {
            if (selectedHitboxIndex == HitboxDirection.Center)
            {
                SetUserAction(ScreenshotEditor.UserActions.MoveSymbol);
            }
            else if (selectedHitboxIndex > HitboxDirection.Center)
            {
                SetUserAction(ScreenshotEditor.UserActions.ScaleSymbol);
            }
            else
            {
                SetUserAction(ScreenshotEditor.UserActions.Select);
            }
        }

        if (overlayGraphics == null)
        {
            CreateOverlay(pictureBox);
        }
    }

    public void MouseMove(Point MousePosition, bool SquareBounds)
    {
        if (dragStarted == false) // don't update the selected hitbox index while a drag scale is active
        {
            GetHitboxUnderCursor(MousePosition);
        }

        if (parentEditor.selectedUserAction >= UserActions.CreateRectangle)
        {
            pictureBox.Cursor = Cursors.Cross;
        }
        else if (!dragStarted)
        {
            pictureBox.Cursor = Cursors.Arrow;
        }

        if (currentSelectedSymbol != null)
        {

            if (currentSelectedSymbol.MoveAllowed)
            {
                if (selectedHitboxIndex == HitboxDirection.Center)
                {
                    pictureBox.Cursor = Cursors.SizeAll;
                }
            }
            if (currentSelectedSymbol.ScalingAllowed)
            {
                switch (selectedHitboxIndex)
                {
                    case HitboxDirection.NW: case HitboxDirection.SE: pictureBox.Cursor = Cursors.SizeNWSE; break;
                    case HitboxDirection.NE: case HitboxDirection.SW: pictureBox.Cursor = Cursors.SizeNESW; break;
                    case HitboxDirection.W: case HitboxDirection.E: pictureBox.Cursor = Cursors.SizeWE; break;
                    case HitboxDirection.N: case HitboxDirection.S: pictureBox.Cursor = Cursors.SizeNS; break;
                }
            }
            if (currentSelectedSymbol is GsLine)
            {
                if ((int)selectedHitboxIndex == 1 || (int)selectedHitboxIndex == 2)
                {
                    pictureBox.Cursor = Cursors.SizeAll;
                }
            }
        }

        if (MousePosition.X != dragStart.X || MousePosition.Y != dragStart.Y)
        {
            dragMoved = true;
        }
        if (SourceImage == null) return;

        if (parentEditor.selectedUserAction >= ScreenshotEditor.UserActions.CreateRectangle) // any UserAction above CreateRectangle is a new symbol creation
        {
            CreateTempSymbol(MousePosition);
        }
        else if (parentEditor.selectedUserAction == ScreenshotEditor.UserActions.MoveSymbol)
        {
            MoveSymbol(MousePosition);
            UpdateOverlay(null, forceUpdate: false);//, forceUpdateDynamicImages: false);
        }
        else if (parentEditor.selectedUserAction == ScreenshotEditor.UserActions.ScaleSymbol)
        {
            ScaleSymbol(MousePosition);
            UpdateOverlay(null, false);//, forceUpdateDynamicImages: false);
        }

        oldMousePosition = MousePosition;
    }

    int stackedSymbolsIndex = -1;
    public void MouseUp(Point MousePosition)
    {     
        if (dragStarted == false) // don't bother with symbol stuff when a drag was cancelled or not actually started
        {
            //Debug.WriteLine($"Mouse up, dragStarted: {dragStarted}, cancelling any symbol placement");
            UpdateOverlay();
            parentEditor.UpdatePropertiesPanel();
            return;
        }

        //Debug.WriteLine($"Mouse up, dragStarted: {dragStarted}, placing symbol based on userAction: {parentEditor.selectedUserAction}");
        bool SymbolAllowsClickPlacement = parentEditor.selectedUserAction == ScreenshotEditor.UserActions.CreateImage || parentEditor.selectedUserAction == ScreenshotEditor.UserActions.CreateNumbered;

        if (dragMoved == false) // user clicked and released mouse without moving it.
        {
            if (SymbolAllowsClickPlacement) // Don't switch to Select if the action is inserting an unscaled image or Number, since the click place is allowed
            {
                // don't change Action, keep spamming those symbols
            }
            else
            {
                // user clicked instead of dragging, switch to Select
                SetUserAction(ScreenshotEditor.UserActions.Select);
                SelectSymbolUnderCursor();
            }
        }

        GraphicSymbol? symbol = GetNewSymbol(MousePosition, parentEditor.GetShift()); // checks current User Action and creates a symbol based on that
        if (symbol != null)
        {
            if (symbol.ValidSymbol)
            {
                parentEditor.AddNewSymbolToList(symbol);
            }
        }
        UpdateOverlay();
        parentEditor.UpdatePropertiesPanel();
        dragStarted = false;

        if (parentEditor.selectedUserAction != ScreenshotEditor.UserActions.MoveSymbol && Settings.Default.SelectAfterPlacingSymbol)
        {
            SetUserAction(ScreenshotEditor.UserActions.Select);
        }
        if (parentEditor.selectedUserAction == ScreenshotEditor.UserActions.CreateCrop || parentEditor.selectedUserAction == ScreenshotEditor.UserActions.CreateImage || parentEditor.selectedUserAction == ScreenshotEditor.UserActions.CreateImageScaled)
        {
            // there's no point in creating consecutive Crops, revert to Select, the same is probably true for images.
            SetUserAction(ScreenshotEditor.UserActions.Select);
        }
    }

    #endregion

    #region Move and Scale ------------------------------------------------------------------------------

    private void MoveSymbol(Point MousePosition)
    {
        if (currentSelectedSymbol == null) return;

        if (currentSelectedSymbol.MoveAllowed && dragStarted)
        {
            Point newPos = new(
                Math.Clamp(MousePosition.X - dragStartOffsetFromSymbolCenter.X, -OutOfBoundsMaxPixels, CanvasSize.Width + OutOfBoundsMaxPixels),
                Math.Clamp(MousePosition.Y - dragStartOffsetFromSymbolCenter.Y, -OutOfBoundsMaxPixels, CanvasSize.Height + OutOfBoundsMaxPixels)
            );
            currentSelectedSymbol.MoveTo(newPos.X, newPos.Y);
        }
    }

    private void ScaleSymbol(Point MousePosition)
    {
        if (currentSelectedSymbol == null) return;
        if (dragStarted == false) return;

        if (currentSelectedSymbol is GsLine)
        {
            if ((int)selectedHitboxIndex == 1)
            {
                currentSelectedSymbol.StartPoint = MousePosition;
            }
            if ((int)selectedHitboxIndex == 2)
            {
                currentSelectedSymbol.EndPoint = MousePosition;
            }
        }
        else if (currentSelectedSymbol.ScalingAllowed)
        {
            if (selectedHitboxIndex == HitboxDirection.W || selectedHitboxIndex == HitboxDirection.NW || selectedHitboxIndex == HitboxDirection.SW)
            {
                currentSelectedSymbol.MoveLeftEdgeTo(MousePosition.X);
            }
            if (selectedHitboxIndex == HitboxDirection.E || selectedHitboxIndex == HitboxDirection.NE || selectedHitboxIndex == HitboxDirection.SE)
            {
                currentSelectedSymbol.MoveRightEdgeTo(MousePosition.X);
            }
            if (selectedHitboxIndex == HitboxDirection.N || selectedHitboxIndex == HitboxDirection.NE || selectedHitboxIndex == HitboxDirection.NW)
            {
                currentSelectedSymbol.MoveTopEdgeTo(MousePosition.Y);
            }
            if (selectedHitboxIndex == HitboxDirection.S || selectedHitboxIndex == HitboxDirection.SE || selectedHitboxIndex == HitboxDirection.SW)
            {
                currentSelectedSymbol.MoveBottomEdgeTo(MousePosition.Y);
            }
        }
    }
    #endregion


}
