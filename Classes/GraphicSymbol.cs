using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;

namespace ScreenShotTool.Forms
{
#pragma warning disable CA1416 // Validate platform compatibility
    public class GraphicSymbol
    {
        public Pen LinePen = new(Color.Gray);
        public SolidBrush LineBrush = new SolidBrush(Color.Gray);
        public SolidBrush FillBrush = new SolidBrush(Color.Pink);
        public SolidBrush TextBrush = new SolidBrush(Color.Black);
        public SolidBrush ShadowBrush = new SolidBrush(Color.FromArgb(20, Color.Black));
        public Pen ShadowPen = new Pen(Color.FromArgb(50, Color.Black));
        public Color ForegroundColor;
        public Color BackgroundColor;
        public Color TextColor;
        public bool ScalingAllowed = true;
        public bool MoveAllowed = true;
        public bool ShadowEnabled = false;
        //public Point ShadowOffset = new Point(10, 10);
        public int ShadowDistance = 10;
        public ListViewItem? ListViewItem { get; set; }

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

        public virtual int LineWeight
        {
            get; set;
        }
        public int fillAlpha;
        public int lineAlpha;
        public string Name = "Blank";

        public bool ValidSymbol = false;

        public GraphicSymbol(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint, int lineWeight = 1, int lineAlpha = 255, int fillAlpha = 255)
        {
            this.ForegroundColor = foregroundColor;
            this.BackgroundColor = backgroundColor;
            this.TextColor = foregroundColor;
            this.Left = startPoint.X;
            this.Top = startPoint.Y;
            this.Right = endPoint.X;
            this.Bottom = endPoint.Y;
            this.LineWeight = lineWeight;
            this.lineAlpha = lineAlpha;
            this.fillAlpha = fillAlpha;
            this.LineWeight = lineWeight;
            this.ShadowEnabled = shadowEnabled;
        }

        public GraphicSymbol(GraphicSymbol clonedSymbol)
        {
            this.LineBrush = clonedSymbol.LineBrush;
            this.LinePen = clonedSymbol.LinePen;
            this.ForegroundColor = clonedSymbol.ForegroundColor;
            this.BackgroundColor = clonedSymbol.BackgroundColor;
            this.StartPoint = clonedSymbol.StartPoint; // coordinate 1
            this.EndPoint = clonedSymbol.EndPoint; // width or coordinate 2
        }

        public virtual void DrawSymbol(Graphics graphic)
        {
        }

        public virtual void DrawShadow(Graphics graphic)
        {
        }

        internal void UpdatePen()
        {
            LineBrush = new SolidBrush(Color.FromArgb(lineAlpha, ForegroundColor.R, ForegroundColor.G, ForegroundColor.B));
            LinePen.Brush = LineBrush;
            LinePen.Width = LineWeight;
            
            FillBrush = new SolidBrush(Color.FromArgb(fillAlpha, BackgroundColor.R, BackgroundColor.G, BackgroundColor.B));
            ShadowBrush.Color = Color.FromArgb(FillBrush.Color.A / 8, Color.Black);
            ShadowPen.Color = Color.FromArgb(LineBrush.Color.A / 8, Color.Black);
            ShadowPen.Width = LineWeight;

            TextBrush.Color = TextColor;
        }

        internal void UpdateColors()
        {
            ForegroundColor = Color.FromArgb(lineAlpha, ForegroundColor.R, ForegroundColor.G, ForegroundColor.B);
            BackgroundColor = Color.FromArgb(lineAlpha, BackgroundColor.R, BackgroundColor.G, BackgroundColor.B);
            TextColor = Color.FromArgb(lineAlpha, ForegroundColor.R, ForegroundColor.G, ForegroundColor.B);
        }

        public virtual void Dispose()
        {
        }
    }

    public class GsBoundingBox : GraphicSymbol
    {
        public delegate void DrawShapeDelegate(Pen pen, Brush brush, Rectangle rect, Graphics graphic);

        public GsBoundingBox(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint, int lineWeight, int lineAlpha, int fillAlpha) : base(foregroundColor, backgroundColor, shadowEnabled, startPoint, endPoint, lineWeight, lineAlpha, fillAlpha)
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
            UpdateColors();
            UpdatePen();
            if (ShadowEnabled)
            {
                for (int i = 1; i < ShadowDistance; i++)
                {
                    // fill
                    draw(graphic, ShadowPen, ShadowBrush, new Point(i, i), true, false);
                }
                for (int i = 1; i < ShadowDistance && i < LineWeight; i++)
                {
                    // line
                    draw(graphic, ShadowPen, ShadowBrush, new Point(i, i), false, true);
                }
                //draw(graphic, ShadowPen, ShadowBrush, ShadowOffset, true, false);
            }
            draw(graphic, LinePen, FillBrush, new Point(0,0), true, true);
        }

        private void draw(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = true, bool outline = true)
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

    public class GsRectangle : GsBoundingBox
    {
        public GsRectangle(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint, int lineWeight, int lineAlpha, int fillAlpha) : base(foregroundColor, backgroundColor, shadowEnabled, startPoint, endPoint, lineWeight, lineAlpha, fillAlpha)
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

    public class GsBorder : GsRectangle
    {
        private int borderWeight;
        private int originalWidth = 0;
        private int originalHeight = 0;
        public GsBorder(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint, int lineWeight, int lineAlpha, int fillAlpha) : base(foregroundColor, backgroundColor, shadowEnabled, startPoint, endPoint, lineWeight, lineAlpha, fillAlpha)
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

                Right = originalWidth  - (int)Math.Ceiling(borderWeight / 2f) ;
                Bottom = originalHeight  - (int)Math.Ceiling(borderWeight / 2f);
            }
        }
    }

    public class GsCircle : GsBoundingBox
    {
        public GsCircle(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint, int lineWeight, int lineAlpha, int fillAlpha) : base(foregroundColor, backgroundColor, shadowEnabled, startPoint, endPoint, lineWeight, lineAlpha, fillAlpha)
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

    public class GsBlur : GsBoundingBox
    {
        public Bitmap? blurredImage;
        private Bitmap? blurred;
        public GsBlur(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint, int lineWeight, int lineAlpha, int fillAlpha) : base(foregroundColor, backgroundColor, shadowEnabled, startPoint, endPoint, lineWeight, lineAlpha, fillAlpha)
        {
            Name = "Blur";
            drawFill = DrawFill;
        }

        public void DrawFill(Pen pen, Brush fillBrush, Rectangle rect, Graphics graphic)
        {
            if (blurredImage == null) return;
            blurred = CropImage(blurredImage, new Rectangle(Left, Top, Width, Height));

            graphic.DrawImage(blurred, Left, Top, Width, Height);
            blurred.Dispose();
        }

        Brush blackBrush = new SolidBrush(Color.Black);
        Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            //https://www.codingdefined.com/2015/04/solved-bitmapclone-out-of-memory.html
            Bitmap bmp = new Bitmap(cropArea.Width, cropArea.Height);

            using (Graphics gph = Graphics.FromImage(bmp))
            {
                gph.FillRectangle(blackBrush, new Rectangle(0, 0, 100, 100));
                gph.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), cropArea, GraphicsUnit.Pixel);
            }
            return bmp;
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

        public GsText(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint, int lineWeight, int lineAlpha) : base(foregroundColor, backgroundColor, shadowEnabled, startPoint, endPoint, lineWeight, lineAlpha)
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
            UpdateColors();
            UpdatePen();
            if (ShadowEnabled)
            {
                for (int i = 1; i < ShadowDistance && i < fontEmSize / 3; i++)
                {
                    //Debug.WriteLine("Draw shadow");
                    draw(graphic, ShadowBrush, new Point(i, i));
                }
                //draw(graphic, ShadowBrush, ShadowOffset);
            }
            draw(graphic, TextBrush, new Point(0, 0));
        }

        private void draw(Graphics graphic, Brush drawBrush, Point offset)
        {   
            if (lineAlpha > 0)
            {
                UpdateFont();
                graphic.DrawString(text, font, drawBrush, new PointF(Left + offset.X, Top + offset.Y));
            }
        }
    }

    public class GsLine : GraphicSymbol
    {
        public Point lineEnd;

        public GsLine(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint, int lineWeight, int lineAlpha) : base(foregroundColor, backgroundColor, shadowEnabled, startPoint, endPoint, lineWeight, lineAlpha)
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
            UpdateColors();
            UpdatePen();
            if (ShadowEnabled)
            {
                for (int i = 1; i < ShadowDistance && i < LineWeight; i++)
                {
                    draw(graphic, ShadowPen, new Point(i, i));
                }
                //draw(graphic, ShadowPen, ShadowOffset);
            }
            draw(graphic, LinePen, new Point(0, 0));
        }

        private void draw(Graphics graphic, Pen drawPen, Point offset)
        {
            if (LineWeight < 1) { LineWeight = 1; }
            if (lineAlpha > 0)
            {
                graphic.DrawLine(drawPen, new Point(StartPoint.X + offset.X, StartPoint.Y + offset.Y) , new Point(EndPoint.X + offset.X, EndPoint.Y + offset.Y));
            }
        }
    }

    public class GsArrow : GsLine
    {


        public GsArrow(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint, int lineWeight, int lineAlpha) : base(foregroundColor, backgroundColor, shadowEnabled, startPoint, endPoint, lineWeight, lineAlpha)
        {
            Name = "Arrow";
            int arrowSize = 5;
            AdjustableArrowCap bigArrow = new(arrowSize, arrowSize);
            LinePen.CustomEndCap = bigArrow;
            ShadowPen.CustomEndCap = bigArrow;
            CheckValid(StartPointV2, EndPointV2);
        }
    }

    public class GsImage : GraphicSymbol
    {
        readonly Image? image;

        public GsImage(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint) : base(foregroundColor, backgroundColor, shadowEnabled, startPoint, endPoint)
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

        public GsImageScaled(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint) : base(foregroundColor, backgroundColor, shadowEnabled, startPoint, endPoint)
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
