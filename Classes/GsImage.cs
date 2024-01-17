namespace ScreenShotTool.Forms;
#pragma warning disable CA1416 // Validate platform compatibility

public class GsImage : GraphicSymbol
{
    readonly Image? image;

    public GsImage(Point startPoint, Point endPoint, bool shadow) : base(startPoint, endPoint, shadow)
    {
        Name = "Image";
        if (image == null)
        {
            image = Clipboard.GetImage();
        }
        if (image != null)
        {
            ValidSymbol = true;
        }
        else
        {
            ValidSymbol = false;
        }
        ScalingAllowed = false;
    }

    public override int Width
    {
        get
        {
            return image != null ? image.Width : 1;
        }
    }

    public override int Height
    {
        get
        {
            return image != null ? image.Height : 1;
        }
    }

    internal override void DrawShape(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = true, bool outline = true)
    {
        if (image != null)
        {
            graphic.DrawImageUnscaled(image, Left, Top);
        }
    }

    public override void DrawShadow(Graphics graphic)
    {
        if (ShadowEnabled)
        {
            for (int i = 1; i < ShadowDistance; i++)
            {
                // fill
                //DrawShape(graphic, ShadowPen, ShadowBrush, new Point(i, i), true, false);
                graphic.FillRectangle(ShadowBrush, new Rectangle(Left + i, Top + i, Width, Height));
            }
        }
    }

    public override void Dispose()
    {
        image?.Dispose();
    }
}

