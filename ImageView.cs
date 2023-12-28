using System.Diagnostics;
using System.Drawing.Text;

namespace ScreenShotTool
{
    public partial class ImageView : Form
    {
        public bool StartInCropMode = false;
        private Bitmap? ImageSource;
        private Bitmap? ImageResult;
        public int X = 0;
        public int Y = 0;
        Screen screen;
        public bool CompleteCaptureOnMoureRelease = false;
        public bool SaveToFile = false;
        public bool SendToClipboard = false;
        public bool ShowAdjustmentArrows = true;
        private bool showHelp = false;

        private enum AdjustMode
        {
            None,
            Position,
            Size
        }
        private AdjustMode adjustMode = AdjustMode.Size;

        public ImageView(bool startCropping, Screen activeScreen, Bitmap? image)
        {
            InitializeComponent();
            BringToFront();
            this.screen = activeScreen;
            if (startCropping)
            {
                StartInCropMode = true;
            }
            if (image != null)
            {
                ImageSource = image;
            }
            pictureBoxDraw.Image = new Bitmap(1000, 1000);
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
            if (e.KeyCode == Keys.Escape)
            {
                DisposeAllImages();
                DialogResult = DialogResult.Cancel;
            }
            if (e.KeyCode == Keys.Return)
            {
                DisposeSourceImage();
                DialogResult = DialogResult.Yes;
            }
            if (e.KeyCode == Keys.C)
            {
                if (ImageResult != null)
                {
                    Clipboard.SetImage(ImageResult);
                }
                DisposeAllImages();
                DialogResult = DialogResult.No;
            }
            if (e.KeyCode == Keys.S)
            {
                adjustMode = AdjustMode.Size;
            }
            if (e.KeyCode == Keys.P)
            {
                adjustMode = AdjustMode.Position;
            }
            if (e.KeyCode == Keys.H)
            {
                showHelp = !showHelp;
            }

            if (e.Modifiers == Keys.Shift)
            {
                boostMultiplier = 10;
            }
            else
            {
                boostMultiplier = 1;
            }

            if (e.KeyCode == Keys.Left)
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
            if (e.KeyCode == Keys.Right)
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
            if (e.KeyCode == Keys.Up)
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
            if (e.KeyCode == Keys.Down)
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

            pictureBoxDraw.Image = DrawSquare(pictureBoxDraw.Image, true, true);
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
            Debug.WriteLine($"Adjusting region: {x}, {y}");
            Debug.WriteLine(regionRect.ToString());

            regionRect.X += x * AdjustLeftMultiplier * boostMultiplier;
            regionRect.Width += ((x * AdjustRightMultiplier) - (x * AdjustLeftMultiplier)) * boostMultiplier; //if Left is changed, width must update to keep size
            regionRect.Y += y * AdjustTopMultiplier * boostMultiplier;
            regionRect.Height += ((y * AdjustBottomMultiplier) - (y * AdjustTopMultiplier)) * boostMultiplier;

            Debug.WriteLine(regionRect.ToString());
        }

        private void AdjustRegionPosition(int x, int y)
        {
            regionRect.X += x * boostMultiplier;
            regionRect.Y += y * boostMultiplier;
        }

        #endregion

        private void DisposeAllImages()
        {
            if (ImageResult != null)
                ImageResult.Dispose();
            if (ImageSource != null)
                ImageSource.Dispose();
        }
        private void DisposeSourceImage()
        {
            if (ImageSource != null)
                ImageSource.Dispose();
        }

        private void pictureBoxDraw_Click(object sender, EventArgs e)
        {

        }

        bool squareCreated = false;
        bool mouseDrag = false;
        int mouseStartX = 0;
        int mouseStartY = 0;

        private void pictureBoxDraw_MouseDown(object sender, MouseEventArgs e)
        {
            squareCreated = false;
            regionRect = new Rectangle();
            pictureBoxDraw.Image = new Bitmap(this.Width, this.Height);
            mouseStartX = Cursor.Position.X;
            mouseStartY = Cursor.Position.Y;
            mouseDrag = true;
        }

        private void pictureBoxDraw_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDrag = false;

            CloneRegionImage();

            if (CompleteCaptureOnMoureRelease)
            {
                if (SendToClipboard)
                {
                    if (ImageResult != null)
                    {
                        Clipboard.SetImage(ImageResult);
                    }
                }
                if (SaveToFile)
                {
                    DisposeSourceImage();
                    DialogResult = DialogResult.Yes;
                }
                else
                {
                    DisposeAllImages();
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

        public SolidBrush brush = new SolidBrush(Color.Gray);
        public SolidBrush brushFill = new SolidBrush(Color.Gray);
        Pen pen = new Pen(Color.Gray);
        Pen arrowPen = new Pen(Color.Yellow);
        Color drawColor = Color.Green;
        Rectangle regionRect = new Rectangle();

        private void pictureBoxDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDrag || squareCreated)
            {
                pictureBoxDraw.Image = DrawSquare(pictureBoxDraw.Image, true, true);
            }
            else
            {
                pictureBoxDraw.Image = DrawSquare(pictureBoxDraw.Image, false, true);

            }
        }

        private Image DrawSquare(Image outputImage, bool drawSquare, bool drawText)
        {
            Graphics graphic;
            outputImage = new Bitmap(this.Width, this.Height);
            graphic = Graphics.FromImage(outputImage);
            graphic.TextRenderingHint = TextRenderingHint.AntiAliasGridFit; // fixes ugly aliasing on text

            brush = new SolidBrush(drawColor);
            brushFill = new SolidBrush(Color.FromArgb(100, drawColor.R, drawColor.G, drawColor.B));
            Pen pen = new Pen(brush);

            int rectX = mouseStartX - screen.Bounds.X;
            int rectY = mouseStartY - screen.Bounds.Y;
            int rectWidth = Cursor.Position.X - mouseStartX;
            int rectHeight = Cursor.Position.Y - mouseStartY;
            if (rectWidth < 0)
            {
                rectX = rectX + rectWidth;
                rectWidth = rectWidth * -1;
            }
            if (rectHeight < 0)
            {
                rectY = rectY + rectHeight;
                rectHeight = rectHeight * -1;
            }

            if (mouseDrag)
            {
                regionRect = new Rectangle(rectX, rectY, rectWidth, rectHeight);
            }

            if (drawSquare)
            {
                graphic.DrawRectangle(pen, regionRect);
                graphic.FillRectangle(brushFill, regionRect);
            }

            if (drawText)
            {
                int textX = Cursor.Position.X + 20 - screen.Bounds.X;
                int textY = Cursor.Position.Y + 20 - screen.Bounds.Y;
                graphic.DrawString($"W:{regionRect.Width} H:{regionRect.Height}\nEnter: Save, C: Clipboard, H: Help, Esc: Exit", this.Font, brush, textX, textY);
                if (showHelp)
                {
                    graphic.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)), new Rectangle(screen.Bounds.X + 10, screen.Bounds.Y + 10, 250, 200));
                    graphic.DrawString($"Enter: Save\nC: Copy\nEsc: Cancel\nS: Size\nP: Position\nArrows: Move\nCtrl+Arrows: Select adjust side\nShift+Arrows: Fast adjust", this.Font, brush, screen.Bounds.X + 20, screen.Bounds.Y + 20);
                }

            }

            if (ShowAdjustmentArrows)
            {
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

            if (rectWidth > 0 && rectHeight > 0)
            {
                squareCreated = true;
            }
            return outputImage;
        }

        private void pictureBoxDraw_MouseLeave(object sender, EventArgs e)
        {
            mouseDrag = false;
        }
    }
}
