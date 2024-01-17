namespace ScreenShotTool.Forms;

public class GsDynamicImage(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor) : GsBoundingBox(startPoint, endPoint, foregroundColor, backgroundColor)
{
    internal Point previousPosition = new(0, 0);
    internal Size previousSize = new(0, 0);
    public Bitmap? sourceImage;

    internal bool RectChanged(Rectangle newRect)
    {
        if (previousPosition.X != newRect.X) return true;
        if (previousPosition.Y != newRect.Y) return true;
        if (previousSize.Width != newRect.Width) return true;
        if (previousSize.Height != newRect.Height) return true;
        return false;
    }
}

