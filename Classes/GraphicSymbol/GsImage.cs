using ScreenShotTool.Classes;
using System.Diagnostics;

namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility

public class GsImage : GraphicSymbol
{
    Bitmap? image;
    Bitmap? shadowImage;
    bool useAdvancedShadow = true;
    //bool isDisposed = false;

    public static GsImage Create(Point startPoint, Bitmap bitmap)
    {
        GsImage gsImage = new GsImage(startPoint, Point.Empty, false)
        {
            image = bitmap,
            Width = bitmap.Width,
            Height = bitmap.Height,
            ValidSymbol = true
        };
        return gsImage;
    }

    public static GsImage Create(Point startPoint, SharedBitmap copiedImage, bool checkClipboard)
    {
        bool useClipboard = true;
        GsImage gsImage = new GsImage(startPoint, Point.Empty, false);

        Image? clipboardImage = null;
        if (checkClipboard)
        {
            clipboardImage = Clipboard.GetImage();
        }

        Debug.WriteLine($"GsImage.Create, copiedImage disposed: {copiedImage.isDisposed}");

        if (copiedImage.isDisposed == false)
        {

            if (clipboardImage != null)
            {
                int clipWidth = clipboardImage.Width;
                int clipHeight = clipboardImage.Height;

                int copiedWidth = copiedImage.Width;
                int copiedHeight = copiedImage.Height;

                if (clipWidth != copiedWidth || clipHeight != copiedHeight)
                {
                    //Debug.WriteLine("GsImage.Create: clipboard data is different, use clipboard");
                    copiedImage.DisposeImage();
                    useClipboard = true;
                }
                else
                {
                    //Debug.WriteLine("GsImage.Create: clipboard data is same, use internal with alpha");
                    useClipboard = false;
                }
            }

        }
        else
        {
            useClipboard = true;
        }

        if (copiedImage.isDisposed == false && useClipboard == false)
        {
            Debug.WriteLine("GsImage.Create: use internal copied");
            gsImage.image.DisposeAndNull();
            Bitmap? tempBmp = copiedImage.GetBitmap();
            if (tempBmp != null)
            {
                gsImage.image = EditorCanvas.CopyImage(tempBmp);
            }
        }
        else if (clipboardImage != null)
        //if (clipboardImage != null) // TODO remove, insert else above
        {
            //Debug.WriteLine("GsImage.Create: use clipboard image");
            gsImage.image = (Bitmap)clipboardImage;
        }
        else
        {
            Debug.WriteLine("GsImage.Create: Neither image was valid (clipboard or copiedImage)");
        }

        if (gsImage.image != null)
        {
            gsImage.ValidSymbol = true;
            gsImage.Width = gsImage.image.Width;
            gsImage.Height = gsImage.image.Height;
        }
        else
        {
            gsImage.ValidSymbol = false;
        }
        return gsImage;
    }

    public GsImage(Point startPoint, Point endPoint, bool shadow) : base(startPoint, endPoint, shadow)
    {
        Name = "Image";
        AllowClickPlacement = true;
    }

    internal override void DrawShape(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = true, bool outline = true)
    {
        if (image != null)// && isDisposed == false)
        {
            graphic.DrawImage(image, Left, Top, Width, Height);
        }
    }

    public override void DrawShadow(Graphics graphic)
    {
        if (ShadowEnabled)
        {
            int smallestSide = Math.Min(Width, Height);
            int adjustedShadowDistance = Math.Min(ShadowDistance, smallestSide / 10);
            // fill
            if (useAdvancedShadow) // alpha based shadow, worse performance
            {
                if (shadowImage == null && image != null)
                {
                    try
                    {
                        shadowImage = CreateAlphaShadow(image); //new Bitmap(image.Width, image.Height);
                    }
                    catch
                    {
                        useAdvancedShadow = false;
                        DrawSimpleShadow(graphic, adjustedShadowDistance);
                    }

                }
                if (shadowImage != null)
                {
                    graphic.DrawImage(shadowImage, new Rectangle(Left + adjustedShadowDistance, Top + adjustedShadowDistance, Width, Height));
                }
            }
            else
            {
                DrawSimpleShadow(graphic, adjustedShadowDistance);
            }
        }
    }

    private void DrawSimpleShadow(Graphics graphic, int adjustedShadowDistance)
    {
        for (int i = 1; i < adjustedShadowDistance; i++)
        {
            graphic.FillRectangle(ShadowBrush, new Rectangle(Left + i, Top + i, Width, Height));
        }
    }

    private static Bitmap CreateAlphaShadow(Bitmap source)
    {
        Bitmap shadow = new Bitmap(source.Width, source.Height);
        using (Graphics g = Graphics.FromImage(shadow))
        {
            using var snoop = new BmpPixelSnoop(source);
            using var target = new BmpPixelSnoop(shadow);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color sourcePixel = snoop.GetPixel(x, y);
                    target.SetPixel(x, y, Color.FromArgb((int)(sourcePixel.A * 0.2f), Color.Black));
                }
            }
        }
        return shadow;
    }

    public override void DisposeImages()
    {
        image?.DisposeAndNull();
        shadowImage?.DisposeAndNull();
    }
}

