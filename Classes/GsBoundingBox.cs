namespace ScreenShotTool.Forms
{
    public class GsBoundingBox : GraphicSymbol
    {
        public delegate void DrawShapeDelegate(Pen pen, Brush brush, Rectangle rect, Graphics graphic);

        public GsBoundingBox(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled = false, int lineWeight = 0, int lineAlpha = 255, int fillAlpha = 255) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight, lineAlpha, fillAlpha)
        {
            Name = "Rectangle";
            Width = endPoint.X;
            Height = endPoint.Y;
            if (Width > 0 && Height > 0)
            {
                ValidSymbol = true;
            }
        }

        public GsBoundingBox(Point startPoint, Point endPoint) : base(startPoint, endPoint)
        {
            Name = "Rectangle";
            Width = endPoint.X;
            Height = endPoint.Y;
            if (Width > 0 && Height > 0)
            {
                ValidSymbol = true;
            }
        }

        public override Point StartPoint { get { return new Point(Left, Top); } set { Left = value.X; Top = value.Y; } }
        public override Point EndPoint { get { return new Point(Right, Bottom); } set { Right = value.X; Bottom = value.Y; } }

        internal override void DrawShape(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = true, bool outline = true)
        {
            if (fillAlpha > 0 && fill)
            {
                drawFill(drawPen, drawBrush, new Rectangle(Left + offset.X, Top + offset.Y, Width, Height), graphic);
            }
            if (lineAlpha > 0 && LineWeight > 0 && outline)
            {
                drawLine(drawPen, drawBrush, new Rectangle(Left + offset.X, Top + offset.Y, Width, Height), graphic);
            }
        }

        internal DrawShapeDelegate drawLine = NoDrawing;
        internal DrawShapeDelegate drawFill = NoDrawing;
        public static void NoDrawing(Pen pen, Brush b, Rectangle r, Graphics graphic)
        {
        }
    }
}

