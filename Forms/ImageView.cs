using ScreenShotTool.Forms;
using ScreenShotTool.Properties;
using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace ScreenShotTool
{
#pragma warning disable IDE0090 // Use 'new(...)'

    [SupportedOSPlatform("windows")]
    public partial class ImageView : Form
    {
        public bool StartInCropMode = false;
        private readonly Bitmap? ImageSource;
        private Bitmap? ImageResult;
        public int X = 0;
        public int Y = 0;
        private readonly Screen screen;
        public bool CompleteCaptureOnMoureRelease = false;
        public bool SaveToFile = false;
        public bool SendToClipboard = false;
        public bool SendToEditor = false;
        public bool ShowAdjustmentArrows = true;
        private bool showHelp = false;
        public float frameRate = 60f;

        private readonly SolidBrush brushZoomRegion;
        private readonly SolidBrush brushFill;
        private readonly SolidBrush brushHelpBG;
        private readonly SolidBrush blackBrush;
        private readonly SolidBrush brushText;
        private readonly Pen linePen;
        private readonly Pen arrowPen;
        private readonly Pen zoomRegionPen;
        public Color lineColor = Color.Green;
        public Color arrowColor = Color.Yellow;
        public Color maskColor = Color.FromArgb(120, 0, 0, 0);
        public Color textColor = Color.LightGreen;
        Rectangle regionRect = new();

        bool isClosing = false; // if true, don't update the view, prevent referencing disposed images.

        private enum AdjustMode
        {
            None,
            Position,
            Size
        }
        private AdjustMode adjustMode = AdjustMode.Size;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public ImageView(bool startCropping, Screen activeScreen, Bitmap? image)
        {
            frameRate = Settings.Default.MaxFramerate;
            InitializeComponent();

            brushZoomRegion = new SolidBrush(lineColor);
            brushFill = new SolidBrush(maskColor);
            brushHelpBG = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
            brushText = new SolidBrush(textColor);
            blackBrush = new SolidBrush(Color.Black);
            linePen = new Pen(lineColor);
            arrowPen = new Pen(arrowColor);
            zoomRegionPen = new Pen(brushZoomRegion);

            this.screen = activeScreen;
            if (startCropping)
            {
                StartInCropMode = true;
            }
            if (image != null)
            {
                ImageSource = image;
            }
            pictureBoxDraw.Image = new Bitmap(10, 10);
        }

        private void ImageView_Load(object sender, EventArgs e)
        {
            SetForegroundWindow(Handle);
        }

        public Bitmap? GetBitmap()
        {
            return ImageResult;
        }

        public void SetImage()
        {
            pictureBoxScreenshot.Image = ImageSource;
            pictureBoxDraw.Parent = pictureBoxScreenshot;
            pictureBoxDraw.BackColor = Color.Transparent;
            pictureBoxDraw.Location = pictureBoxScreenshot.Location;
            pictureBoxDraw.Size = pictureBoxScreenshot.Size;
        }

        private void ImageView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift)
            {
                boostMultiplier = 10;
            }
            else
            {
                boostMultiplier = 1;
            }

            if (e.KeyCode == Keys.Escape)
            {
                Debug.WriteLine("Exiting ImageView");
                isClosing = true;
                DisposeAllImages();
                DialogResult = DialogResult.Cancel;
            }
            else if (e.KeyCode == Keys.Return)
            {
                DisposeSourceImage();
                isClosing = true;
                DialogResult = DialogResult.Yes;
            }
            else if (e.KeyCode == Keys.C)
            {
                if (ImageResult != null)
                {
                    Clipboard.SetImage(ImageResult);
                    isClosing = true;
                    DisposeAllImages();
                    DialogResult = DialogResult.No;
                }
            }
            else if (e.KeyCode == Keys.E)
            {
                if (ImageResult != null)
                {
                    OpenImageInEditor(ImageResult);
                    isClosing = true;
                    DisposeSourceImage();
                    DialogResult = DialogResult.No;
                }
            }
            else if (e.KeyCode == Keys.S)
            {
                adjustMode = AdjustMode.Size;
            }
            else if (e.KeyCode == Keys.P)
            {
                adjustMode = AdjustMode.Position;
            }
            else if (e.KeyCode == Keys.H)
            {
                showHelp = !showHelp;
            }

            else if (e.KeyCode == Keys.Left)
            {
                if (e.Modifiers == Keys.Control)
                {
                    AdjustLeftMultiplier = 1;
                    AdjustRightMultiplier = 0;
                }
                else
                {
                    AdjustRegion(-1, 0);
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (e.Modifiers == Keys.Control)
                {
                    AdjustLeftMultiplier = 0;
                    AdjustRightMultiplier = 1;
                }
                else
                {
                    AdjustRegion(1, 0);
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (e.Modifiers == Keys.Control)
                {
                    AdjustTopMultiplier = 1;
                    AdjustBottomMultiplier = 0;
                }
                else
                {
                    AdjustRegion(0, -1);
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (e.Modifiers == Keys.Control)
                {
                    AdjustTopMultiplier = 0;
                    AdjustBottomMultiplier = 1;
                }
                else
                {
                    AdjustRegion(0, 1);
                }
            }

            UpdateOverlay();
        }

        private static void OpenImageInEditor(Image img)
        {
            ScreenshotEditor imageEditor = new ScreenshotEditor(img);
            imageEditor.Show();
        }

        #region Adjust Region bounds -------------------
        int AdjustLeftMultiplier = 0;
        int AdjustRightMultiplier = 1;
        int AdjustTopMultiplier = 0;
        int AdjustBottomMultiplier = 1;
        int boostMultiplier = 1;
        private void AdjustRegion(int x, int y)
        {
            if (adjustMode == AdjustMode.Size)
            {
                AdjustRegionSize(x, y);
            }
            else if (adjustMode == AdjustMode.Position)
            {
                AdjustRegionPosition(x, y);
            }
            CloneRegionImage();
        }

        private void AdjustRegionSize(int x, int y)
        {
            regionRect.X += x * AdjustLeftMultiplier * boostMultiplier;
            regionRect.Width += ((x * AdjustRightMultiplier) - (x * AdjustLeftMultiplier)) * boostMultiplier; //if Left is changed, width must update to keep size
            regionRect.Y += y * AdjustTopMultiplier * boostMultiplier;
            regionRect.Height += ((y * AdjustBottomMultiplier) - (y * AdjustTopMultiplier)) * boostMultiplier;
        }

        private void AdjustRegionPosition(int x, int y)
        {
            regionRect.X += x * boostMultiplier;
            regionRect.Y += y * boostMultiplier;
        }

        #endregion

        private void DisposeAllImages()
        {
            ImageResult?.Dispose();
            ImageSource?.Dispose();
        }
        private void DisposeSourceImage()
        {
            ImageSource?.Dispose();
        }

        private void PictureBoxDraw_Click(object sender, EventArgs e)
        {

        }

        bool mouseDrag = false;
        int mouseStartX = 0;
        int mouseStartY = 0;

        Bitmap? screenSizedBitmap;

        private void PictureBoxDraw_MouseDown(object sender, MouseEventArgs e)
        {
            regionRect = new Rectangle();
            DisposeAndNull(screenSizedBitmap);
            screenSizedBitmap = new Bitmap(this.Width, this.Height);
            pictureBoxDraw.Image = screenSizedBitmap;
            mouseStartX = Cursor.Position.X;
            mouseStartY = Cursor.Position.Y;
            mouseDrag = true;
        }

        private void PictureBoxDraw_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDrag = false;

            CloneRegionImage();

            if (CompleteCaptureOnMoureRelease)
            {
                bool disposeAll = true;
                isClosing = true;

                if (SendToClipboard)
                {
                    if (ImageResult != null)
                    {
                        Clipboard.SetImage(ImageResult);
                    }
                }

                if (SendToEditor)
                {
                    if (ImageResult != null)
                    {
                        Bitmap clonedForEditor = ImageResult.Clone(new Rectangle(0, 0, ImageResult.Width, ImageResult.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb); //(regionRect, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        OpenImageInEditor(clonedForEditor);
                    }
                    disposeAll = false;
                }

                if (SaveToFile)
                {
                    DisposeSourceImage();
                    DialogResult = DialogResult.Yes;
                }
                else
                {
                    if (disposeAll)
                    {
                        DisposeAllImages();
                    }
                    else
                    {
                        DisposeSourceImage();
                    }
                    DialogResult = DialogResult.No;
                }
            }
        }

        private void CloneRegionImage()
        {
            if (regionRect.Width > 0 && regionRect.Height > 0)
            {
                Bitmap regionBmp;
                regionBmp = new Bitmap(pictureBoxScreenshot.Image);
                ImageResult = regionBmp.Clone(regionRect, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                regionBmp.Dispose();
            }
        }



        private void PictureBoxDraw_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateOverlay();
        }


        private void PictureBoxDraw_MouseLeave(object sender, EventArgs e)
        {
            mouseDrag = false;
        }

        #region draw Overlay -------------------------------------------------

        DateTime LastFrame = DateTime.Now;

        Image? tempImage;
        private void UpdateOverlay()
        {
            float MilliSecondsPerFrame = (1f / frameRate) * 1000;
            TimeSpan ts = DateTime.Now - LastFrame;
            if (ts.Milliseconds >= MilliSecondsPerFrame)
            {
                DisposeAndNull(tempImage);
                //tempImage = DrawOverlay(pictureBoxDraw.Image, true, true);
                tempImage = DrawOverlay(true, true);
                pictureBoxDraw.Image = tempImage;
                LastFrame = DateTime.Now;
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

        private static void DisposeAndNull(Graphics? graphic)
        {
            if (graphic != null)
            {
                graphic.Dispose();
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                graphic = null;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            }
        }

        Graphics? drawGraphic;
        Bitmap? DrawOutputBmp;
        //private Image DrawOverlay(Image outputImage, bool drawSquare, bool drawText, bool drawZoom = true)
        private Image DrawOverlay(bool drawSquare, bool drawText, bool drawZoom = true)
        {
            //outputImage.Dispose();
            DisposeAndNull(DrawOutputBmp);
            DrawOutputBmp = new Bitmap(this.Width, this.Height);

            DisposeAndNull(this.drawGraphic);
            drawGraphic = Graphics.FromImage(DrawOutputBmp);
            drawGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            drawGraphic.TextRenderingHint = TextRenderingHint.AntiAliasGridFit; // fixes ugly aliasing on text

            int rectX = mouseStartX - screen.Bounds.X;
            int rectY = mouseStartY - screen.Bounds.Y;
            int rectWidth = Cursor.Position.X - mouseStartX;
            int rectHeight = Cursor.Position.Y - mouseStartY;
            if (rectWidth < 0)
            {
                rectX += rectWidth;
                rectWidth *= -1;
            }
            if (rectHeight < 0)
            {
                rectY += rectHeight;
                rectHeight *= -1;
            }

            if (mouseDrag)
            {
                regionRect = new Rectangle(rectX, rectY, rectWidth, rectHeight);
            }

            if (drawSquare)
            {
                DrawSelectionBox(drawGraphic, linePen);
                if (Settings.Default.MaskRegion && regionRect.Width > 0 && regionRect.Height > 0)
                {
                    MaskRectangle(drawGraphic, new Rectangle(0, 0, screen.Bounds.Width, screen.Bounds.Height), regionRect, brushFill);
                }
            }

            if (ShowAdjustmentArrows)
            {
                DrawAdjustmentArrows(drawGraphic);
            }

            if (drawZoom)
            {
                DrawZoomView(drawGraphic, pictureBoxScreenshot.Image);
            }

            if (drawText)
            {
                DrawInfoText(drawGraphic);
            }

            if (rectWidth > 0 && rectHeight > 0)
            {
                //squareCreated = true;
            }
            return DrawOutputBmp;
        }

        int zoomPositionH = 30; // move the zoom box around the cursor to avoid the edges of the screen
        int zoomPositionV = 70;
        readonly int zoomRadius = 20;
        readonly float zoomLevel = 10;
        int zoomSize = 30;
        //Color testColor = Color.Fuchsia;

        Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            //https://www.codingdefined.com/2015/04/solved-bitmapclone-out-of-memory.html
            Bitmap bmp = new Bitmap(cropArea.Width, cropArea.Height);

            using (Graphics gph = Graphics.FromImage(bmp))
            {
                gph.FillRectangle(blackBrush, new Rectangle(0, 0, 100, 100));
                gph.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), cropArea, GraphicsUnit.Pixel);
            }
            return bmp;
        }

        Bitmap? screenshotBmp;
        private void DrawZoomView(Graphics graphic, Image screenshotInput)
        {
            if (isClosing) { return; }

            try
            {
                DisposeAndNull(screenshotBmp);
                screenshotBmp = new Bitmap(screenshotInput);
            }
            catch
            {
                Debug.WriteLine("Error updating Zoom view. Possibly when Disposing and closing form.");
            }

            if (screenshotBmp == null) { return; }

            zoomSize = (int)(zoomRadius * zoomLevel);
            float cursorX = Cursor.Position.X - screen.Bounds.X;
            float cursorY = Cursor.Position.Y - screen.Bounds.Y;

            Rectangle zoomRect = new Rectangle((int)cursorX - zoomRadius, (int)cursorY - zoomRadius, zoomRadius * 2, zoomRadius * 2);

            Bitmap zoomImage = CropImage(screenshotBmp, zoomRect);

            //move zoom viewer around the cursor
            if (Cursor.Position.X + zoomSize + zoomPositionH > screen.Bounds.Right)
            {
                zoomPositionH = -zoomSize - 30;
            }
            if (Cursor.Position.Y + zoomSize + zoomPositionV > screen.Bounds.Bottom)
            {
                zoomPositionV = -zoomSize - 30;
            }
            if (Cursor.Position.X - zoomSize < screen.Bounds.Left)
            {
                zoomPositionH = 30;
            }
            if (Cursor.Position.Y - zoomSize < screen.Bounds.Top)
            {
                zoomPositionV = 70;
            }

            Rectangle zoomBorder = new Rectangle(
                Cursor.Position.X - screen.Bounds.X + zoomPositionH,
                Cursor.Position.Y - screen.Bounds.Y + zoomPositionV,
                zoomSize,
                zoomSize
            );


            graphic.DrawImage(zoomImage, zoomBorder.X, zoomBorder.Y, zoomBorder.Width, zoomBorder.Height);

            graphic.DrawRectangle(linePen, zoomBorder);

            graphic.DrawLine(linePen,
                zoomBorder.X + (zoomBorder.Width / 2) - (zoomLevel / 4),
                zoomBorder.Y,
                zoomBorder.X + (zoomBorder.Width / 2) - (zoomLevel / 4),
                zoomBorder.Y + zoomBorder.Height);
            graphic.DrawLine(linePen,
                zoomBorder.X,
                zoomBorder.Y + (zoomBorder.Height / 2) - (zoomLevel / 4),
                zoomBorder.X + zoomBorder.Width,
                zoomBorder.Y + (zoomBorder.Height / 2) - (zoomLevel / 4));

            Rectangle testDisplayRect = new Rectangle(
                (int)(-(cursorX * 4f)),
                (int)(-(cursorY * 4f)),
                regionRect.Width,
                regionRect.Height
            );

            testDisplayRect.X += (int)(regionRect.X * 5f) + zoomPositionH;
            testDisplayRect.Y += (int)(regionRect.Y * 5f) + zoomPositionV;

            Rectangle ActiveRegionRect = new Rectangle(
                testDisplayRect.X + (zoomBorder.Height / 2) - 3,
                testDisplayRect.Y + (zoomBorder.Width / 2) - 3,
                testDisplayRect.Width * 5,
                testDisplayRect.Height * 5
            );

            // crop region marker rectangle if it's outside the zoom rectangle
            bool drawActiveRegion = true;

            int leftCorrection = ActiveRegionRect.Left - zoomBorder.Left;
            if (ActiveRegionRect.Right < zoomBorder.Left)
            {
                drawActiveRegion = false;
            }
            else if (leftCorrection < 0)
            {
                ActiveRegionRect.X = zoomBorder.X;
                ActiveRegionRect.Width += leftCorrection;
            }

            int rightCorrection = ActiveRegionRect.Right - zoomBorder.Right;
            if (ActiveRegionRect.Left > zoomBorder.Right)
            {
                drawActiveRegion = false;
            }
            else if (ActiveRegionRect.Right > zoomBorder.Right)
            {
                ActiveRegionRect.Width -= rightCorrection;
            }

            int topCorrection = ActiveRegionRect.Top - zoomBorder.Top;
            if (ActiveRegionRect.Top > zoomBorder.Bottom)
            {
                drawActiveRegion = false;
            }
            else if (topCorrection < 0)
            {
                ActiveRegionRect.Y = zoomBorder.Y;
                ActiveRegionRect.Height += topCorrection;
            }

            int bottomCorrection = ActiveRegionRect.Bottom - zoomBorder.Bottom;
            if (ActiveRegionRect.Top > zoomBorder.Bottom)
            {
                drawActiveRegion = false;
            }
            else if (ActiveRegionRect.Bottom > zoomBorder.Bottom)
            {
                ActiveRegionRect.Height -= bottomCorrection;
            }

            if (drawActiveRegion)
            {
                graphic.DrawRectangle(zoomRegionPen, ActiveRegionRect);
            }

            MaskRectangle(graphic, zoomBorder, ActiveRegionRect, brushFill);

            screenshotBmp.Dispose();
            zoomImage.Dispose();
        }

        private static void MaskRectangle(Graphics graphic, Rectangle ContainerRegion, Rectangle ActiveRegion, Brush maskingBrush)
        {
            int LeftSide = Math.Clamp(ActiveRegion.Left, ContainerRegion.Left, ContainerRegion.Right);
            int RightSide = Math.Clamp(ActiveRegion.Right, ContainerRegion.Left, ContainerRegion.Right);
            int TopSide = Math.Clamp(ActiveRegion.Top, ContainerRegion.Top, ContainerRegion.Bottom);
            int BottomSide = Math.Clamp(ActiveRegion.Bottom, ContainerRegion.Top, ContainerRegion.Bottom);
            int LeftSpace = LeftSide - ContainerRegion.Left;
            int RightSpace = ContainerRegion.Right - RightSide;
            int TopSpace = TopSide - ContainerRegion.Top;
            int BottomSpace = ContainerRegion.Bottom - BottomSide;
            int width = RightSide - LeftSide;
            //int height = BottomSide - TopSide;

            if (ActiveRegion.Left > ContainerRegion.Left)
            {
                graphic.FillRectangle(maskingBrush, new Rectangle(ContainerRegion.Left, ContainerRegion.Top, LeftSpace, ContainerRegion.Height));
            }
            if (ActiveRegion.Right < ContainerRegion.Right)
            {
                graphic.FillRectangle(maskingBrush, new Rectangle(RightSide, ContainerRegion.Top, RightSpace, ContainerRegion.Height));
            }


            if (ActiveRegion.Top > ContainerRegion.Top)
            {
                graphic.FillRectangle(maskingBrush, new Rectangle(LeftSide, ContainerRegion.Top, width, TopSpace));
            }

            if (ActiveRegion.Bottom < ContainerRegion.Bottom)
            {
                graphic.FillRectangle(maskingBrush, new Rectangle(LeftSide, BottomSide, width, BottomSpace));
            }
        }

        private void DrawInfoText(Graphics graphic)
        {
            int textX = Cursor.Position.X + zoomPositionH - screen.Bounds.X;
            int textY = Cursor.Position.Y + zoomPositionV + zoomSize + 3 - screen.Bounds.Y;
            graphic.FillRectangle(brushHelpBG, new Rectangle(textX, textY, zoomSize, 40));
            graphic.DrawString($"W:{regionRect.Width,4} H:{regionRect.Height,4} Esc: Exit, H: Help\nEnter: Save, C: Clipboard, E: Edit", this.Font, brushText, textX, textY);
            if (showHelp)
            {
                graphic.FillRectangle(brushHelpBG, new Rectangle(10, 10, 250, 200));
                graphic.DrawString($"Enter: Save\nC: Copy\n E: Open in Editor\nEsc: Cancel\nS: Size\nP: Position\nArrows: Move\nCtrl+Arrows: Select adjust side\nShift+Arrows: Fast adjust\nH: Toggle help", this.Font, brushText, 20, 20);
            }
        }

        private void DrawSelectionBox(Graphics graphic, Pen pen)
        {
            if (regionRect.Width == 0 || regionRect.Height == 0) return;
            graphic.DrawRectangle(pen, regionRect);
            //graphic.FillRectangle(brushFill, regionRect);
        }

        private void DrawAdjustmentArrows(Graphics graphic)
        {
            if (regionRect.Width == 0 || regionRect.Height == 0) return;
            int RightSide = regionRect.Right;
            int LeftSide = regionRect.Left;
            int TopSide = regionRect.Top;
            int BottomSide = regionRect.Bottom;
            int HalfHeight = regionRect.Height / 2;
            int HalfWidth = regionRect.Width / 2;
            int MiddleVertical = TopSide + HalfHeight;
            int MiddleHorizontal = LeftSide + HalfWidth;

            if (AdjustRightMultiplier != 0 || adjustMode == AdjustMode.Position)
            {
                graphic.DrawPolygon(arrowPen, new Point[] { new Point(RightSide + 5, MiddleVertical - 5), new Point(RightSide + 10, MiddleVertical), new Point(RightSide + 5, MiddleVertical + 5) });
            }
            if (AdjustLeftMultiplier != 0 || adjustMode == AdjustMode.Position)
            {
                graphic.DrawPolygon(arrowPen, new Point[] { new Point(LeftSide - 5, MiddleVertical - 5), new Point(LeftSide - 10, MiddleVertical), new Point(LeftSide - 5, MiddleVertical + 5) });
            }

            if (AdjustTopMultiplier != 0 || adjustMode == AdjustMode.Position)
            {
                graphic.DrawPolygon(arrowPen, new Point[] { new Point(MiddleHorizontal - 5, TopSide - 5), new Point(MiddleHorizontal, TopSide - 10), new Point(MiddleHorizontal + 5, TopSide - 5) });
            }
            if (AdjustBottomMultiplier != 0 || adjustMode == AdjustMode.Position)
            {
                graphic.DrawPolygon(arrowPen, new Point[] { new Point(MiddleHorizontal - 5, BottomSide + 5), new Point(MiddleHorizontal, BottomSide + 10), new Point(MiddleHorizontal + 5, BottomSide + 5) });
            }
        }

        #endregion

        private void ImageView_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
        }
    }
}
