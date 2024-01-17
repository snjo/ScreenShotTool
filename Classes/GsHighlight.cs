using System.Diagnostics;

namespace ScreenShotTool.Forms;
#pragma warning disable CA1416 // Validate platform compatibility
public class GsHighlight : GsDynamicImage
{
    //public Bitmap? originalImage;
    Bitmap? highlightedBmp;
    public ColorBlend.BlendModes blendMode = ColorBlend.BlendModes.Darken;
    private Color previousColor = Color.White;
    private ColorBlend.BlendModes previousBlendMode = ColorBlend.BlendModes.Darken;
    public GsHighlight(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadow, int lineWidth, int lineAlpha, int fillAlpha) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadow, lineWidth, lineAlpha, fillAlpha)
    {
        Name = "Highlight";
        drawFill = DrawFill;
        base.BackgroundColor = backgroundColor;
    }

    public void DrawFill(Pen pen, Brush fillBrush, Rectangle rect, Graphics graphic)
    {
        if (rect.Width < 1 || rect.Height < 1) return;
        
        if (sourceImage != null)
        {
            if (highlightedBmp == null || RectChanged(rect) || previousColor != BackgroundColor || previousBlendMode != blendMode)
            {
                UpdateHighlightBmp(rect);
                previousPosition = new Point(rect.Left, rect.Top);
                previousSize = new Size(rect.Width, rect.Height);
                previousColor = BackgroundColor;
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
        if (sourceImage == null) return;
        if (highlightedBmp != null) { 
            highlightedBmp.Dispose();
            highlightedBmp = null;
        }
        highlightedBmp = new Bitmap(rect.Width, rect.Height);
        //int bmpLeft = Math.Max(0, Left);
        //int bmpTop = Math.Max(0, Top);
        int bmpLeft = Left;
        int bmpTop = Top;
        int bmpRight = Math.Min(sourceImage.Width, Right);
        int bmpBottom = Math.Min(sourceImage.Height, Bottom);
        int bmpWidth = bmpRight - bmpLeft;
        int bmpHeight = bmpBottom - bmpTop;
        for (int x = 0; x < bmpWidth; x++)
        {
            for (int y = 0; y < bmpHeight; y++)
            {
                int sampleX = bmpLeft + x;
                int sampleY = bmpTop + y;
                if (sampleX < 0 || sampleY < 0 || sampleX >= sourceImage.Width || sampleY >= sourceImage.Height) continue;
                Color sourcePixel = sourceImage.GetPixel(sampleX, sampleY);
                //highlightedBmp.SetPixel(x, y, Color.FromArgb(Math.Min((int)sourcePixel.R, BackgroundColor.R), Math.Min((int)sourcePixel.G, BackgroundColor.G), Math.Min((int)sourcePixel.B, BackgroundColor.B)));
                highlightedBmp.SetPixel(x, y, ColorBlend.BlendColors(sourcePixel, BackgroundColor, blendMode));
                //if (x == 100) Debug.WriteLine("color at 100: " +ColorBlend.BlendColors(sourcePixel, BackgroundColor, blendMode));
            }
        }
    }
}

