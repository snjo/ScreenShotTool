namespace ScreenShotTool.Forms
{
    public class GsBorder : GsRectangle
    {
        private int borderWeight;
        private readonly int originalWidth = 0;
        private readonly int originalHeight = 0;
        public GsBorder(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled, int lineWeight, int lineAlpha, int fillAlpha) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight, lineAlpha, fillAlpha)
        {
            Name = "Border";
            drawFill = DrawFill;
            drawLine = DrawLine;
            originalWidth = endPoint.X;
            originalHeight = endPoint.Y;
            ScalingAllowed = false;
            MoveAllowed = false;
        }

        public override int LineWeight
        {
            get
            {
                return borderWeight;
            }
            set
            {
                borderWeight = value;
                Left = 0 + (borderWeight / 2);
                Top = 0 + (borderWeight / 2);

                Right = originalWidth - (int)Math.Ceiling(borderWeight / 2f);
                Bottom = originalHeight - (int)Math.Ceiling(borderWeight / 2f);
            }
        }
    }
}

