using System.Diagnostics;

namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility
public class GsHighlight : GsDynamicImage
{
    //public Bitmap? originalImage;
    private Bitmap? highlightedBmp;
    public ColorBlend.BlendModes blendMode = ColorBlend.BlendModes.Multiply;
    private Color previousColor = Color.White;
    private ColorBlend.BlendModes previousBlendMode = ColorBlend.BlendModes.Multiply;
    DateTime lastLocalUpdate = DateTime.MinValue;
    public GsHighlight(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadow, int lineWidth) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadow, lineWidth)
    {
        Name = "Highlight";
        drawFill = DrawFill;
        base.FillColor = backgroundColor;
    }

    public void DrawFill(Pen pen, Brush fillBrush, Rectangle rect, Graphics graphic)
    {
        if (rect.Width < 1 || rect.Height < 1) return;

        if (SourceImage != null)
        {
            if (highlightedBmp == null || RectChanged(rect) || previousColor != FillColor || previousBlendMode != blendMode || lastLocalUpdate < LastSourceUpdate)
            {
                //Debug.WriteLine($"highlight update, Last source: {LastSourceUpdate.Millisecond}, local: {lastLocalUpdate.Millisecond}");
                UpdateHighlightBmp(rect);
                lastLocalUpdate = DateTime.Now;
                previousPosition = new Point(rect.Left, rect.Top);
                previousSize = new Size(rect.Width, rect.Height);
                previousColor = FillColor;
                previousBlendMode = blendMode;
            }
            if (highlightedBmp != null)
            {
                graphic.DrawImage(highlightedBmp, rect);
            }
        }
    }

    public override void DrawShadow(Graphics graphic)
    {
        // no shadow;
    }

    private void UpdateHighlightBmp(Rectangle rect)
    {
        if (SourceImage == null) return;
        highlightedBmp.DisposeAndNull();
        highlightedBmp = new Bitmap(rect.Width, rect.Height);
        //int bmpLeft = Math.Max(0, Left);
        //int bmpTop = Math.Max(0, Top);
        int bmpLeft = Left;
        int bmpTop = Top;
        int bmpRight = Math.Min(SourceImage.Width, Right);
        int bmpBottom = Math.Min(SourceImage.Height, Bottom);
        int bmpWidth = bmpRight - bmpLeft;
        int bmpHeight = bmpBottom - bmpTop;

        using var snoop = new BmpPixelSnoop(SourceImage);
        using var target = new BmpPixelSnoop(highlightedBmp);
        for (int x = 0; x < bmpWidth; x++)
        {
            for (int y = 0; y < bmpHeight; y++)
            {
                int sampleX = bmpLeft + x;
                int sampleY = bmpTop + y;
                if (sampleX < 0 || sampleY < 0 || sampleX >= snoop.Width || sampleY >= snoop.Height) continue;
                Color sourcePixel = snoop.GetPixel(sampleX, sampleY);
                target.SetPixel(x, y, ColorBlend.BlendColors(sourcePixel, FillColor, blendMode));

            }
        }
    }

    public override void Dispose()
    {
        highlightedBmp.DisposeAndNull();
        base.Dispose();
    }
}

