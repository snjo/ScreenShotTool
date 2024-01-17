namespace ScreenShotTool.Forms;
#pragma warning disable CA1416 // Validate platform compatibility
public class GsCircle : GsBoundingBox
{
    public GsCircle(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled, int lineWeight, int lineAlpha, int fillAlpha) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight, lineAlpha, fillAlpha)
    {
        Name = "Circle";
        drawFill = DrawFill;
        drawLine = DrawLine;
    }

    public static void DrawLine(Pen pen, Brush lineBrush, Rectangle rect, Graphics graphic)
    {
        graphic.DrawEllipse(pen, rect);
    }

    public static void DrawFill(Pen pen, Brush fillBrush, Rectangle rect, Graphics graphic)
    {
        graphic.FillEllipse(fillBrush, rect);
    }
}

