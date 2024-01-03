using System.Drawing.Drawing2D;
using System.Numerics;

namespace ScreenShotTool.Forms
{
#pragma warning disable CA1416 // Validate platform compatibility
    public class GraphicSymbol
    {
        public Pen pen = new Pen(Color.Gray);
        public Brush brush = new SolidBrush(Color.Gray);
        public Brush fillBrush = new SolidBrush(Color.Gray);
        public Color foregroundColor;
        public Color backgroundColor;
        public virtual int X1 { get; set; }
        public virtual int Y1 { get; set; }
        public virtual int X2 { get; set; }
        public virtual int Y2 { get; set; }
        public virtual int Width { 
            get { return X2; }
            set { X2 = value; }
        }
        public virtual int Height 
        {
            get { return Y2; }
            set { Y2 = value; }
        }
        public virtual Vector2 StartPoint
        {
            get { return new Vector2(X1, Y1); }
            set
            {
                X1 = (int)value.X;
                Y1 = (int)value.Y;
            }
        }
        public virtual Vector2 EndPoint 
        {
            get { return new Vector2(X2, Y2); }
            set
            {
                X2 = (int)value.X;
                Y2 = (int)value.Y;
            }
        }
        public virtual int Top { get { return Y1; } set { Y1 = value; } }
        public virtual int Bottom { get { return Y2; } set { Y2 = value; } }
        public virtual int Left { get { return X1; } set { X1 = value; } }
        public virtual int Right { get { return X2; } set { X2 = value; } }

        public int lineWeight;
        public int fillAlpha;
        public int lineAlpha;
        public string Name = "Blank";

        public bool ValidSymbol = false;

        public GraphicSymbol(Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight = 1, int lineAlpha = 255, int fillAlpha = 255)
        {
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.X1 = X1;
            this.Y1 = Y1;
            this.X2 = X2;
            this.Y2 = Y2;
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
            this.X1 = clonedSymbol.X1; // coordinate 1
            this.Y1 = clonedSymbol.Y1; // coordinate 1
            this.X2 = clonedSymbol.X2; // width or coordinate 2
            this.Y2 = clonedSymbol.Y2; // height or coordinate 2
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

    public class GsRectangle : GraphicSymbol
    {
        public GsRectangle(Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight, int lineAlpha, int fillAlpha) : base(foregroundColor, backgroundColor, X1, Y1, X2, Y2, lineWeight, lineAlpha, fillAlpha)
        {
            Name = "Rectangle";
            if (X2 > 0 && Y2 > 0)
            {
                ValidSymbol = true;
            }
        }

        public override void DrawSymbol(Graphics graphic)
        {
            UpdatePen();
            UpdateColors();
            if (fillAlpha > 0)
            {
                //graphic.FillRectangle(fillBrush, new Rectangle(X1, Y1, X2, Y2));
                graphic.FillRectangle(fillBrush, new Rectangle(X1, Y1, X2, Y2));
            }
            if (lineAlpha > 0 && lineWeight > 0)
            {
                graphic.DrawRectangle(pen, new Rectangle(X1, Y1, X2, Y2));
            }
        }
    }

    public class GsCircle : GraphicSymbol
    {
        public GsCircle(Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight, int lineAlpha, int fillAlpha) : base(foregroundColor, backgroundColor, X1, Y1, X2, Y2, lineWeight, lineAlpha, fillAlpha)
        {
            Name = "Circle";
            if (X2 > 0 && Y2 > 0)
            {
                ValidSymbol = true;
            }
        }

        public override void DrawSymbol(Graphics graphic)
        {
            UpdatePen();
            UpdateColors();
            if (fillAlpha > 0)
            {
                graphic.FillEllipse(fillBrush, new Rectangle(X1, Y1, X2, Y2));
            }
            if (lineAlpha > 0 && lineWeight > 0)
            {
                graphic.DrawEllipse(pen, new Rectangle(X1, Y1, X2, Y2));
            }
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

        public GsText(Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight, int lineAlpha) : base(foregroundColor, backgroundColor, X1, Y1, X2, Y2, lineWeight, lineAlpha)
        {
            Name = "Text";
            if (X2 > 0 && Y2 > 0)
            {
                ValidSymbol = true;
            }
            font = UpdateFont();
        }

        public Font UpdateFont()
        {
            return new Font(fontFamily, fontEmSize, fontStyle);
        }

        public override void DrawSymbol(Graphics graphic)
        {
            UpdatePen();
            UpdateColors();
            if (lineAlpha > 0)
            {
                fontEmSize = Math.Max(5f, (Math.Abs(Y2) + Math.Abs(X2)) / 2f) / 2f;
                font = UpdateFont();
                graphic.DrawString(text, font, brush, new PointF(X1, Y1));
            }
        }
    }

    public class GsLine : GraphicSymbol
    {
        public GsLine(Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight, int lineAlpha) : base(foregroundColor, backgroundColor, X1, Y1, X2, Y2, lineWeight, lineAlpha)
        {
            Name = "Line";
            CheckValid(X1, Y1, X2, Y2);
        }

        protected void CheckValid(int X1, int Y1, int X2, int Y2)
        {
            float lineLength = Vector2.Distance(new Vector2(X1, Y1), new Vector2(X2, Y2));
            if (lineLength > 1)
            {
                ValidSymbol = true;
            }
        }

        public override void DrawSymbol(Graphics graphic)
        {
            UpdatePen();
            UpdateColors();
            if (lineWeight < 1) { lineWeight = 1; }
            if (lineAlpha > 0)
            {
                graphic.DrawLine(pen, new Point(X1, Y1), new Point(X2, Y2));
            }
        }
    }

    public class GsArrow : GsLine
    {
        public GsArrow(Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2, int lineWeight, int lineAlpha) : base(foregroundColor, backgroundColor, X1, Y1, X2, Y2, lineWeight, lineAlpha)
        {
            Name = "Arrow";
            int arrowSize = 5;
            AdjustableArrowCap bigArrow = new AdjustableArrowCap(arrowSize, arrowSize);
            pen.CustomEndCap = bigArrow;
            CheckValid(X1, Y1, X2, Y2);
        }
    }

    public class GsImage : GraphicSymbol
    {
        Image? image;
        int posX;
        int posY;

        // X2 and Y2 are used for position when placing the image (drag end). Update X1 and Y1 with this drag end position.
        public override int X1
        {
            get { return posX; }
            set { posX = value; }
        }

        public override int Y1
        {
            get { return posY; }
            set { posY = value; }
        }

        public override int X2
        {
            get { return 1; }
            set { }
        }
        public override int Y2
        {
            get { return 1; }
            set { }
        }

        public GsImage(Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2) : base(foregroundColor, backgroundColor, X1, Y1, X2, Y2)
        {
            posX = X2;
            posY = Y2;
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
                //Debug.WriteLine("Image load from clipboard failed, not a valid symbol");
            }
        }
        public override void DrawSymbol(Graphics graphic)
        {
            if (image != null)
            {
                graphic.DrawImageUnscaled(image, posX, posY);
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

        public GsImageScaled(Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2, int Y2) : base(foregroundColor, backgroundColor, X1, Y1, X2, Y2)
        {
            Name = "Scaled Image";
            if (image == null)
            {
                image = Clipboard.GetImage();
            }
            if (image == null)
            {
                ValidSymbol = false;
                //Debug.WriteLine("Image load from clipboard failed, not a valid symbol");
            }
            else if (X2 < 1 || Y2 < 1)
            {
                ValidSymbol = false;
                //Debug.WriteLine("Size is 0, not a valid symbol");
            }
            else
            {
                ValidSymbol = true;
            }
        }

        public override void DrawSymbol(Graphics graphic)
        {
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
