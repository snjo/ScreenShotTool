namespace ScreenShotTool.Forms;
#pragma warning disable CA1416 // Validate platform compatibility
public class GsBlur : GsDynamicImage
{
    //public Bitmap? blurredImage;
    private Bitmap? blurredCrop;

    public GsBlur(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor) : base(startPoint, endPoint, foregroundColor, backgroundColor)
    {
        Name = "Blur";
        drawFill = DrawFill;
    }

    public void DrawFill(Pen pen, Brush fillBrush, Rectangle rect, Graphics graphic)
    {
        if (sourceImage == null)
        {
            graphic.DrawRectangle(LinePen, Left, Top, Width, Height);
        }
        else
        {
            if (RectChanged(rect) || blurredCrop == null)
            {
                UpdateBlurImage();
                previousPosition = new Point(rect.Left, rect.Top);
                previousSize = new Size(rect.Width, rect.Height);
            }
            if (blurredCrop != null)
            {
                graphic.DrawImage(blurredCrop, Left, Top, Width, Height);
            }
        }
    }

    public override void DrawShadow(Graphics graphic)
    {
        // no shadow
    }

    private void UpdateBlurImage()
    {
        if (sourceImage == null) return;
        if (Width < 1 || Height < 1) return;
        if (blurredCrop != null)
        {
            blurredCrop.Dispose();
            blurredCrop = null;
        }
        blurredCrop = CropImage(sourceImage, new Rectangle(Left, Top, Width, Height));
    }

    readonly Brush blackBrush = new SolidBrush(Color.Black);
    Bitmap CropImage(Bitmap img, Rectangle cropArea)
    {
        //https://www.codingdefined.com/2015/04/solved-bitmapclone-out-of-memory.html
        Bitmap bmp = new(cropArea.Width, cropArea.Height);

        using (Graphics gph = Graphics.FromImage(bmp))
        {
            gph.FillRectangle(blackBrush, new Rectangle(0, 0, 100, 100));
            gph.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), cropArea, GraphicsUnit.Pixel);
        }
        return bmp;
    }
}

