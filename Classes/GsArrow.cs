using System.Drawing.Drawing2D;

namespace ScreenShotTool.Forms;
#pragma warning disable CA1416 // Validate platform compatibility

public class GsArrow : GsLine
{
    public GsArrow(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled, int lineWeight, int lineAlpha) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight, lineAlpha)
    {
        Name = "Arrow";
        int arrowSize = 5;
        AdjustableArrowCap bigArrow = new(arrowSize, arrowSize);
        LinePen.CustomEndCap = bigArrow;
        ShadowPen.CustomEndCap = bigArrow;
        CheckValid(StartPointV2, EndPointV2);
    }
}

