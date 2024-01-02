using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace ScreenShotTool.Forms
{
#pragma warning disable CA1416 // Validate platform compatibility
    public class GraphicSymbol
    {
        public Pen pen;
        public Brush brush;
        public Color foregroundColor;
        public Color backgroundColor;
        public int X1;
        public int Y1;
        public int X2;
        public int Y2;
        public int lineWeight;
        public string Name = "Blank";

        public GraphicSymbol(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight)
        { 
            this.brush = brush;
            this.pen = pen;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.X1 = X1; 
            this.Y1 = Y1;
            this.X2 = X2;
            this.Y2 = Y2;
            this.lineWeight = lineWeight;
        }

        public GraphicSymbol(GraphicSymbol clonedSymbol)
        {
            this.brush = clonedSymbol.brush;
            this.pen = clonedSymbol.pen;
            this.foregroundColor = clonedSymbol.foregroundColor;
            this.backgroundColor = clonedSymbol.backgroundColor;
            this.X1 = clonedSymbol.X1; // coordinate 1
            this.Y1 = clonedSymbol.Y1; // coordinate 1
            this.X2 = clonedSymbol.X2; // width or coordinate 2
            this.Y2 = clonedSymbol.Y2; // height or coordinate 2
        }

        public virtual void DrawSymbol(Graphics graphic)
        {
        }

        public virtual void Dispose()
        {
        }
    }

    public class GsRectangle : GraphicSymbol
    {
        public GsRectangle(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight) : base(pen, brush, foregroundColor, backgroundColor, X1, Y1, X2, Y2, lineWeight)
        {
            Name = "Rectangle";
        }

        public override void DrawSymbol(Graphics graphic)
        {
            pen.Color = foregroundColor;
            pen.Brush = brush;
            pen.Width = lineWeight;
            graphic.DrawRectangle(pen, new Rectangle(X1, Y1, X2, Y2));
        }
    }

    public class GsCircle : GraphicSymbol
    {
        public GsCircle(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight) : base(pen, brush, foregroundColor, backgroundColor, X1, Y1, X2, Y2, lineWeight)
        {
            Name = "Circle";
        }

        public override void DrawSymbol(Graphics graphic)
        {
            pen.Color = foregroundColor;
            pen.Brush = brush;
            pen.Width = lineWeight;
            graphic.DrawEllipse(pen, new Rectangle(X1, Y1, X2, Y2));
        }
    }

    public class GsLine : GraphicSymbol
    {
        public GsLine(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight) : base(pen, brush, foregroundColor, backgroundColor, X1, Y1, X2, Y2, lineWeight)
        {
            Name = "Line";
        }

        public override void DrawSymbol(Graphics graphic)
        {
            pen.Color = foregroundColor;
            pen.Brush = brush;
            pen.Width = lineWeight;
            //pen.StartCap = System.Drawing.Drawing2D.LineCap.Triangle;
            //pen.EndCap = System.Drawing.Drawing2D.LineCap.Triangle;
            //int arrowSize = Math.Max(Math.Min(3, lineWeight), 4);
            
            int arrowSize = 5;
            AdjustableArrowCap bigArrow = new AdjustableArrowCap(arrowSize, arrowSize);
            pen.CustomEndCap = bigArrow;
            graphic.DrawLine(pen, new Point(X1, Y1), new Point(X2, Y2));
        }
    }

    public class GsArrow : GraphicSymbol
    {
        public GsArrow(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight) : base(pen, brush, foregroundColor, backgroundColor, X1, Y1, X2, Y2, lineWeight)
        {
            Name = "Arrow";
        }

        public override void DrawSymbol(Graphics graphic)
        {
            AdjustableArrowCap bigArrow = new AdjustableArrowCap(lineWeight - 3, lineWeight * 3);
            //PointF mid = new PointF((X1 + X2) / 2, (Y1 + Y2) / 2);
            pen.Color = foregroundColor;
            pen.Brush = brush;
            pen.Width = lineWeight;
            pen.CustomEndCap = bigArrow;
            graphic.DrawLine(pen, new Point(X1, Y1), new Point(X2, Y2));

            //graphic.TranslateTransform(mid.X, mid.Y);
            //graphic.RotateTransform(45f);
            //graphic.DrawPath(pen, new GraphicsPath());
        }
    }

    public class GsImage : GraphicSymbol
    {
        Image? image;

        public GsImage(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight) : base(pen, brush, foregroundColor, backgroundColor, X1, Y1, X2, Y2, lineWeight)
        {
            Name = "Image";
            if (image == null)
            {
                image = Clipboard.GetImage();
            }
        }
        public override void DrawSymbol(Graphics graphic)
        {
            if (image != null)
            {
                graphic.DrawImageUnscaled(image, X2, Y2);
            }
        }

        public override void Dispose()
        {
            if (image != null)
            {
                image.Dispose();
            }
        }
    }

    public class GsImageScaled : GraphicSymbol
    {
        Image? image;

        public GsImageScaled(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight) : base(pen, brush, foregroundColor, backgroundColor, X1, Y1, X2, Y2, lineWeight)
        {
            Name = "Scaled Image";
            if (image == null)
            {
                image = Clipboard.GetImage();
            }
        }

        public override void DrawSymbol(Graphics graphic)
        {
            Debug.WriteLine($"Draw scaled image x1:{X1} y1:{Y1} x2:{X2} y2:{Y2}");

            if (image != null)
            {
                graphic.DrawImage(image, X1, Y1, X2, Y2);
            }
        }

        public override void Dispose()
        {
            if (image != null)
            {
                image.Dispose();
            }
        }
    }
}
