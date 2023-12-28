using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public ImageView(bool startCropping, Screen activeScreen, Bitmap? image)
        {
            InitializeComponent();
            this.screen = activeScreen;
            if (startCropping)
            {
                StartInCropMode = true;
            }
            if (image != null)
            {
                ImageSource = image;
                //pictureBoxScreenshot.BackgroundImage = ImageResult;
                //pictureBoxScreenshot.BackgroundImageLayout = ImageLayout.None;
                //pictureBoxScreenshot.Image = ImageResult;
            }
            pictureBoxDraw.Image = new Bitmap(1000, 1000);
        }

        public Bitmap? GetBitmap()
        {
            return ImageResult;
        }

        //public void SetBackgroundImage()
        //{
        //    pictureBoxScreenshot.BackgroundImage = ImageResult;
        //    pictureBoxScreenshot.BackgroundImageLayout = ImageLayout.None;
        //}

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
                if (ImageResult != null)
                    ImageResult.Dispose();
                if (ImageSource != null)
                    ImageSource.Dispose();
                DialogResult = DialogResult.Cancel;
            }
            if (e.KeyCode == Keys.Return)
            {
                if (ImageSource != null)
                    ImageSource.Dispose();
                DialogResult = DialogResult.Yes;
            }
            if (e.KeyCode == Keys.C)
            {
                if (ImageResult != null)
                {
                    Clipboard.SetImage(ImageResult);
                }
                if (ImageResult != null)
                    ImageResult.Dispose();
                if (ImageSource != null)
                    ImageSource.Dispose();
                DialogResult = DialogResult.No;
            }
        }

        private void pictureBoxDraw_Click(object sender, EventArgs e)
        {
            //mouseDrag = false;
            //squareCreated = false;
            //mouseRect = new Rectangle();
            //Debug.WriteLine("Clicked on Draw image");
        }

        bool squareCreated = false;
        bool mouseDrag = false;
        int mouseStartX = 0;
        int mouseStartY = 0;

        private void pictureBoxDraw_MouseDown(object sender, MouseEventArgs e)
        {
            squareCreated = false;
            mouseRect = new Rectangle();
            pictureBoxDraw.Image = new Bitmap(this.Width, this.Height);
            //Debug.WriteLine(this.Width + " " + this.Height);
            mouseStartX = Cursor.Position.X;
            mouseStartY = Cursor.Position.Y;
            Debug.WriteLine("Cursor start:" +  mouseStartX + " " + mouseStartY);
            mouseDrag = true; 
        }

        private void pictureBoxDraw_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDrag = false;
            Debug.WriteLine("Rectangle confirmed: " + mouseRect);
            if (mouseRect.Width > 0 && mouseRect.Height > 0)
            {
                Bitmap regionBmp;
                regionBmp = new Bitmap(pictureBoxScreenshot.Image);
                ImageResult = regionBmp.Clone(mouseRect, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            else
            {
                Debug.WriteLine("Region size is zero wide or high, can't save");
            }
        }

        
        public SolidBrush brush = new SolidBrush(Color.Gray);
        public SolidBrush brushFill = new SolidBrush(Color.Gray);
        Pen pen = new Pen(Color.Gray);
        Color drawColor = Color.Green;
        Rectangle mouseRect = new Rectangle();

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
                mouseRect = new Rectangle(rectX, rectY, rectWidth, rectHeight);
            }

            if (drawSquare)
            {
                graphic.DrawRectangle(pen, mouseRect);
                graphic.FillRectangle(brushFill, mouseRect);
            }

            if (drawText)
            {
                int textX = Cursor.Position.X + 20 - screen.Bounds.X;
                int textY = Cursor.Position.Y + 20 - screen.Bounds.Y;
                graphic.DrawString($"W:{mouseRect.Width} H:{mouseRect.Height}", this.Font, brush, textX, textY);
                graphic.DrawString($"Enter: Save, C: Copy, Esc: Cancel", this.Font, brush, textX, textY + 20);
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
