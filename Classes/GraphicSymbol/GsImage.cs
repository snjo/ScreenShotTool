using ScreenShotTool.Classes;
using System.Diagnostics;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility

public class GsImage : GraphicSymbol
{
    Bitmap? image;
    Bitmap? rotatedImage;
    Bitmap? shadowImage;
    bool useAdvancedShadow = true;
    float oldRotation = 0f;
    int oldWidth = 0;
    int oldHeight = 0;
    Point corner1;
    Point corner2;
    Point corner3;
    Point corner4;
    int RotatedWidthOverflow;
    int RotatedHeightOverflow;
    public Rectangle RotatedBounds;
    //bool isDisposed = false;

    public static GsImage Create(Point startPoint, Bitmap bitmap)
    {
        GsImage gsImage = new GsImage(startPoint, Point.Empty, false)
        {
            image = bitmap,
            Width = bitmap.Width,
            Height = bitmap.Height,
            ValidSymbol = true,
            RotationAllowed = true,
        };
        gsImage.UpdateRotatedBounds();
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

    public override void DrawSymbol(Graphics graphic)
    {
        UpdateColors();
        UpdatePen();
        UpdateRotatedBounds();
        if (Rotation != 0 && image != null)
        {
            if (Rotation != oldRotation || Width != oldWidth || Height != oldHeight)
            {
                CreateRotatedImage(image);
                oldRotation = Rotation;
                oldHeight = Height;
                oldWidth = Width;
            }
        }
        UpdateShadows();
        DrawShadow(graphic);
        DrawShape(graphic, LinePen, FillBrush, new Point(0, 0), true, true);
    }

    internal override void DrawShape(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = true, bool outline = true)
    {
        if (image != null)// && isDisposed == false)
        {
            if (Rotation != 0)
            {
                if (rotatedImage != null)
                {
                    graphic.DrawImage(rotatedImage, RotatedBounds.Left, RotatedBounds.Top, RotatedBounds.Width, RotatedBounds.Height);
                    //UpdateCorners();                    
                }
            }
            else
            {
                graphic.DrawImage(image, Left, Top, Width, Height);
            }
        }
    }

    private void CreateRotatedImage(Bitmap image)
    {
        //Bitmap tempRotated = new Bitmap(image.Width, image.Height);
        Bitmap tempRotated = new Bitmap(RotatedBounds.Width, RotatedBounds.Height);
        using (Graphics rotateG = Graphics.FromImage(tempRotated))
        {
            rotateG.ResetTransform();
            float pivotX = RotatedBounds.Width / 2;
            float pivotY = RotatedBounds.Height / 2;

            rotateG.TranslateTransform(pivotX, pivotY);
            rotateG.RotateTransform(Rotation);
            rotateG.TranslateTransform(-pivotX, -pivotY);
            //rotateG.DrawImage(image, 0, 0, Width, Height);
            rotateG.DrawImage(image, RotatedWidthOverflow, RotatedHeightOverflow, Width, Height);
        }
        rotatedImage.DisposeAndNull();
        rotatedImage = tempRotated;
    }

    private void UpdateRotatedBounds()
    {
        if (Rotation == 0)
        {
            RotatedBounds = Bounds;
        }
        else
        {
            UpdateCorners();
            int minX = Math.Min(corner1.X, corner2.X);
            minX = Math.Min(minX, corner3.X);
            minX = Math.Min(minX, corner4.X);
            RotatedWidthOverflow = Left - minX;
            int minY = Math.Min(corner1.Y, corner2.Y);
            minY = Math.Min(minY, corner3.Y);
            minY = Math.Min(minY, corner4.Y);
            RotatedHeightOverflow = Top - minY;
            RotatedBounds = new Rectangle(Left - RotatedWidthOverflow, Top - RotatedHeightOverflow, Width + (RotatedWidthOverflow * 2), Height + (RotatedHeightOverflow * 2));
        }
    }

    private void UpdateCorners()
    {
        Point pivot = new Point(Left + Width / 2, Top + Height / 2);
        corner1 = getCorner(pivot.X, pivot.Y, Left, Top, Rotation);
        corner2 = getCorner(pivot.X, pivot.Y, Right, Top, Rotation);
        corner3 = getCorner(pivot.X, pivot.Y, Right, Bottom, Rotation);
        corner4 = getCorner(pivot.X, pivot.Y, Left, Bottom, Rotation);
    }



    private Point getCorner(float pivotX, float pivotY, float cornerX, float cornerY, double angle)
    {
        double radians = (Math.PI / 180) * angle;
        //https://jsfiddle.net/w8r/9rnnk545/
        double x, y, distance, diffX, diffY;

        /// get distance from center to point
        diffX = cornerX - pivotX;
        diffY = cornerY - pivotY;
        distance = Math.Sqrt(diffX * diffX + diffY * diffY);

        /// find angle from pivot to corner
        radians += Math.Atan2(diffY, diffX);

        /// get new x and y and round it off to integer
        x = pivotX + distance * Math.Cos(radians);
        y = pivotY + distance * Math.Sin(radians);

        return new Point((int)x,(int)y);
    }

    public void UpdateShadows()
    {
        if (ShadowEnabled && useAdvancedShadow)
        {
            Debug.WriteLine("Update Shadows");
            if (Rotation == 0)
            {
                Debug.WriteLine("Update Shadows, rotation is none");
                if (shadowImage == null && image != null)
                {
                    try
                    {
                        shadowImage.DisposeAndNull();
                        shadowImage = CreateAlphaShadow(image); //new Bitmap(image.Width, image.Height);   
                    }
                    catch
                    {
                        useAdvancedShadow = false;
                        //DrawSimpleShadow(graphic, adjustedShadowDistance);
                    }
                }
            }
            else 
            {
                Debug.WriteLine("Update Shadows, rotation is " + Rotation);
                //if (Rotation != oldRotation || Width != oldWidth || Height != oldHeight)
                //{
                    Debug.WriteLine("Udate shadow at angle" + Rotation);
                    shadowImage.DisposeAndNull();
                    if (rotatedImage != null)
                    {
                        shadowImage = CreateAlphaShadow(rotatedImage);
                    }
                //}
            }
            
        }
    }

    public override void DrawShadow(Graphics graphic)
    {
        int smallestSide = Math.Min(Width, Height);
        int adjustedShadowDistance = Math.Min(ShadowDistance, smallestSide / 10);
        if (ShadowEnabled)
        {    
            if (shadowImage != null)
            {
                //graphic.DrawImage(shadowImage, new Rectangle(Left + adjustedShadowDistance, Top + adjustedShadowDistance, Width, Height));
                graphic.DrawImage(shadowImage, new Rectangle(RotatedBounds.Left + adjustedShadowDistance, RotatedBounds.Top + adjustedShadowDistance, RotatedBounds.Width, RotatedBounds.Height));
            }
            else if (Rotation == 0)
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

