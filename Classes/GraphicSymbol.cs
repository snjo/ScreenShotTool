using System.Drawing.Drawing2D;
using System.Numerics;

namespace ScreenShotTool.Forms
{
#pragma warning disable CA1416 // Validate platform compatibility
    public class GraphicSymbol
    {
        public Pen pen = new(Color.Gray);
        public Brush brush = new SolidBrush(Color.Gray);
        public Brush fillBrush = new SolidBrush(Color.Gray);
        public Color foregroundColor;
        public Color backgroundColor;
        public bool ScalingAllowed = true;

        private int _x;
        private int _y;
        private int _width;
        private int _height;
        public virtual int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public virtual int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public virtual Point StartPoint
        {
            get { return new Point(_x, _y); }
            set
            {
                _x = (int)value.X;
                _y = (int)value.Y;
            }
        }
        public virtual Point EndPoint
        {
            get { return new Point(_width, _height); }
            set
            {
                _width = (int)value.X;
                _height = (int)value.Y;
            }
        }

        public virtual int Top { get { return _y; } set { _y = value; } }
        public virtual int Bottom { get { return _y + _height; } set { _height = value - _y; } } // ?
        public virtual int Left { get { return _x; } set { _x = value; } }
        public virtual int Right { get { return _x + _width; } set { _width = value - _x; } }

        public Vector2 StartPointV2
        {
            get { return new Vector2(StartPoint.X, StartPoint.Y); }
        }

        public Vector2 EndPointV2
        {
            get { return new Vector2(EndPoint.X, EndPoint.Y); }
        }

        public int lineWeight;
        public int fillAlpha;
        public int lineAlpha;
        public string Name = "Blank";

        public bool ValidSymbol = false;

        public GraphicSymbol(Color foregroundColor, Color backgroundColor, Point startPoint, Point endPoint, int lineWeight = 1, int lineAlpha = 255, int fillAlpha = 255)
        {
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.Left = startPoint.X;
            this.Top = startPoint.Y;
            this.Right = endPoint.X;
            this.Bottom = endPoint.Y;
            this.lineWeight = lineWeight;
            this.lineAlpha = lineAlpha;
            this.fillAlpha = fillAlpha;
        }

        public GraphicSymbol(GraphicSymbol clonedSymbol)
        {
            this.brush = clonedSymbol.brush;
            this.pen = clonedSymbol.pen;
            this.foregroundColor = clonedSymbol.foregroundColor;
            this.backgroundColor = clonedSymbol.backgroundColor;
            this.StartPoint = clonedSymbol.StartPoint; // coordinate 1
            this.EndPoint = clonedSymbol.EndPoint; // width or coordinate 2
        }

        public virtual void DrawSymbol(Graphics graphic)
        {
        }

        internal void UpdatePen()
        {
            brush = new SolidBrush(Color.FromArgb(lineAlpha, foregroundColor.R, foregroundColor.G, foregroundColor.B));
            pen.Brush = brush;
            pen.Width = lineWeight;

            fillBrush = new SolidBrush(Color.FromArgb(fillAlpha, backgroundColor.R, backgroundColor.G, backgroundColor.B));
        }

        internal void UpdateColors()
        {
            foregroundColor = Color.FromArgb(lineAlpha, foregroundColor.R, foregroundColor.G, foregroundColor.B);
            backgroundColor = Color.FromArgb(lineAlpha, backgroundColor.R, backgroundColor.G, backgroundColor.B);
        }

        public virtual void Dispose()
        {
        }
    }

    public class GsBoundingBox : GraphicSymbol
    {
        public delegate void DrawShapeDelegate(Pen pen, Brush brush, Rectangle rect, Graphics graphic);

        public GsBoundingBox(Color foregroundColor, Color backgroundColor, Point startPoint, Point endPoint, int lineWeight, int lineAlpha, int fillAlpha) : base(foregroundColor, backgroundColor, startPoint, endPoint, lineWeight, lineAlpha, fillAlpha)
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

        public override void DrawSymbol(Graphics graphic)
        {
            UpdatePen();
            UpdateColors();
            if (fillAlpha > 0)
            {
                drawFill(pen, fillBrush, new Rectangle(Left, Top, Width, Height), graphic);
            }
            if (lineAlpha > 0 && lineWeight > 0)
            {
                drawLine(pen, brush, new Rectangle(Left, Top, Width, Height), graphic);
            }
        }


        internal DrawShapeDelegate drawLine = NoDrawing;
        internal DrawShapeDelegate drawFill = NoDrawing;
        public static void NoDrawing(Pen pen, Brush b, Rectangle r, Graphics graphic)
        {
        }
    }

    public class GsRectangle : GsBoundingBox
    {
        public GsRectangle(Color foregroundColor, Color backgroundColor, Point startPoint, Point endPoint, int lineWeight, int lineAlpha, int fillAlpha) : base(foregroundColor, backgroundColor, startPoint, endPoint, lineWeight, lineAlpha, fillAlpha)
        {
            Name = "Rectangle";
            drawFill = DrawFill;
            drawLine = DrawLine;
        }


        public static void DrawLine(Pen pen, Brush lineBrush, Rectangle rect, Graphics graphic)
        {
            graphic.DrawRectangle(pen, rect);
        }

        public static void DrawFill(Pen pen, Brush fillBrush, Rectangle rect, Graphics graphic)
        {
            graphic.FillRectangle(fillBrush, rect);
        }
    }

    public class GsCircle : GsBoundingBox
    {
        public GsCircle(Color foregroundColor, Color backgroundColor, Point startPoint, Point endPoint, int lineWeight, int lineAlpha, int fillAlpha) : base(foregroundColor, backgroundColor, startPoint, endPoint, lineWeight, lineAlpha, fillAlpha)
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

    public class GsText : GraphicSymbol
    {
        public FontFamily fontFamily = FontFamily.GenericSansSerif;
        public float fontEmSize = 10f;
        //public float fontSize = 10f;
        public FontStyle fontStyle = FontStyle.Regular;
        Font font;
        public string text = "Text";
        readonly int maxFontSize;
        readonly int minFontSize;

        public GsText(Color foregroundColor, Color backgroundColor, Point startPoint, Point endPoint, int lineWeight, int lineAlpha) : base(foregroundColor, backgroundColor, startPoint, endPoint, lineWeight, lineAlpha)
        {
            Name = "Text";
            Width = endPoint.X;
            Height = endPoint.Y;
            if (Width > 0 && Height > 0)
            {
                ValidSymbol = true;
            }

            maxFontSize = ScreenshotEditor.maxFontSize;
            minFontSize = ScreenshotEditor.minimumFontSize;
            fontEmSize = Math.Clamp((Math.Abs(Width) + Math.Abs(Height)) / 4f, minFontSize, maxFontSize);
            font = CreateFont();
            ScalingAllowed = false;

        }

        private Font CreateFont()
        {
            return new Font(fontFamily, fontEmSize, fontStyle);
        }

        public void UpdateFont()
        {
            font = CreateFont();
        }

        public override void DrawSymbol(Graphics graphic)
        {
            UpdatePen();
            UpdateColors();
            if (lineAlpha > 0)
            {
                UpdateFont();
                graphic.DrawString(text, font, brush, new PointF(Left, Top));
            }
        }
    }

    public class GsLine : GraphicSymbol
    {
        public Point lineEnd;

        public GsLine(Color foregroundColor, Color backgroundColor, Point startPoint, Point endPoint, int lineWeight, int lineAlpha) : base(foregroundColor, backgroundColor, startPoint, endPoint, lineWeight, lineAlpha)
        {
            Name = "Line";
            CheckValid(StartPointV2, EndPointV2);
            EndPoint = endPoint;
        }

        protected void CheckValid(Vector2 start, Vector2 end)
        {
            float lineLength = Vector2.Distance(start, end);
            if (lineLength > 1)
            {
                ValidSymbol = true;
            }
        }

        public override Point EndPoint { get { return lineEnd; } set { lineEnd = value; } }
        public override int Width { get { return 1; } }
        public override int Height { get { return 1; } }

        public override void DrawSymbol(Graphics graphic)
        {
            UpdatePen();
            UpdateColors();
            if (lineWeight < 1) { lineWeight = 1; }
            if (lineAlpha > 0)
            {
                graphic.DrawLine(pen, new Point((int)StartPoint.X, (int)StartPoint.Y), new Point((int)EndPoint.X, (int)EndPoint.Y));
            }
        }
    }

    public class GsArrow : GsLine
    {


        public GsArrow(Color foregroundColor, Color backgroundColor, Point startPoint, Point endPoint, int lineWeight, int lineAlpha) : base(foregroundColor, backgroundColor, startPoint, endPoint, lineWeight, lineAlpha)
        {
            Name = "Arrow";
            int arrowSize = 5;
            AdjustableArrowCap bigArrow = new(arrowSize, arrowSize);
            pen.CustomEndCap = bigArrow;
            CheckValid(StartPointV2, EndPointV2);
        }
    }

    public class GsImage : GraphicSymbol
    {
        readonly Image? image;

        public GsImage(Color foregroundColor, Color backgroundColor, Point startPoint, Point endPoint) : base(foregroundColor, backgroundColor, startPoint, endPoint)
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

        public override void DrawSymbol(Graphics graphic)
        {
            if (image != null)
            {
                graphic.DrawImageUnscaled(image, Left, Top);
            }
        }

        public override void Dispose()
        {
            image?.Dispose();
        }
    }

    public class GsImageScaled : GraphicSymbol
    {
        readonly Image? image;

        public GsImageScaled(Color foregroundColor, Color backgroundColor, Point startPoint, Point endPoint) : base(foregroundColor, backgroundColor, startPoint, endPoint)
        {
            Name = "Scaled Image";
            Width = endPoint.X;
            Height = endPoint.Y;

            if (image == null)
            {
                image = Clipboard.GetImage();
            }
            if (image == null)
            {
                ValidSymbol = false;
                //Debug.WriteLine("Image load from clipboard failed, not a valid symbol");
            }
            else if (Width < 1 || Height < 1)
            {
                ValidSymbol = false;
                //Debug.WriteLine("Size is 0, not a valid symbol");
            }
            else
            {
                ValidSymbol = true;
            }
        }

        public override Point StartPoint { get { return new Point(Left, Top); } set { Left = value.X; Top = value.Y; } }
        public override Point EndPoint { get { return new Point(Right, Bottom); } set { Right = value.X; Bottom = value.Y; } }

        public override void DrawSymbol(Graphics graphic)
        {
            if (image != null)
            {
                graphic.DrawImage(image, Left, Top, Width, Height);
            }
        }

        public override void Dispose()
        {
            image?.Dispose();
        }
    }
}
