using ScreenShotTool.Classes;
using System.Diagnostics;
using System.Net;

namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility

public class GsImage : GraphicSymbol
{
    Bitmap? image;
    //bool isDisposed = false;

    public static GsImage Create(Point startPoint, Bitmap bitmap)
    {
        GsImage gsImage = new GsImage(startPoint, Point.Empty, false);
        gsImage.image = bitmap;
        gsImage.Width = bitmap.Width;
        gsImage.Height = bitmap.Height;
        gsImage.ValidSymbol = true;
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
            for (int i = 1; i < ShadowDistance; i++)
            {
                // fill
                graphic.FillRectangle(ShadowBrush, new Rectangle(Left + i, Top + i, Width, Height));
            }
        }
    }

    public override void DisposeImages()
    {
        image?.DisposeAndNull();
    }
}

