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
        public Pen HightlightSymbolPen = new(Color.Red);
        public SolidBrush LineBrush = new SolidBrush(Color.Gray);
        public SolidBrush FillBrush = new SolidBrush(Color.Pink);
        public SolidBrush TextBrush = new SolidBrush(Color.Black);
        public SolidBrush ShadowBrush = new SolidBrush(Color.FromArgb(20, Color.Black));
        public SolidBrush HighlightBrush = new SolidBrush(Color.Red);
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

        public virtual Rectangle Bounds
        {
            get { return new Rectangle(Left, Top, Width, Height); }
        }

        public virtual int LineWeight
        {
            get; set;
        }
        public int fillAlpha;
        public int lineAlpha;
        public string Name = "Blank";

        public bool ValidSymbol = false;

        public GraphicSymbol(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled = false, int lineWeight = 1, int lineAlpha = 255, int fillAlpha = 255)
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
            this.ShadowEnabled = shadowEnabled;
            HightlightSymbolPen.DashPattern = new[] { 2f, 8f };
            //Hitboxes = CreateHitboxList();
        }

        public GraphicSymbol(Point startPoint, Point endPoint, bool shadow = false)
        {
            this.ForegroundColor = Color.Black;
            this.BackgroundColor = Color.White;
            this.TextColor = Color.Black;
            this.Left = startPoint.X;
            this.Top = startPoint.Y;
            this.Right = endPoint.X;
            this.Bottom = endPoint.Y;
            this.LineWeight = 1;
            this.lineAlpha = 255;
            this.fillAlpha = 255;
            this.ShadowEnabled = shadow;
            HightlightSymbolPen.DashPattern = new[] { 2f, 8f };
            //Hitboxes = CreateHitboxList();
        }

        public virtual void DrawSymbol(Graphics graphic)
        {
            UpdateColors();
            UpdatePen();
            DrawShadow(graphic);
            DrawShape(graphic, LinePen, FillBrush, new Point(0, 0), true, true);
        }

        public virtual void DrawShadow(Graphics graphic)
        {
            if (ShadowEnabled)
            {
                for (int i = 1; i < ShadowDistance; i++)
                {
                    // fill
                    DrawShape(graphic, ShadowPen, ShadowBrush, new Point(i, i), true, false);
                }
                for (int i = 1; i < ShadowDistance && i < LineWeight; i++)
                {
                    // line
                    DrawShape(graphic, ShadowPen, ShadowBrush, new Point(i, i), false, true);
                }
            }
        }

        internal virtual void DrawShape(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = true, bool outline = true)
        {
        }

        private int anchorsize = 8;
        private int anchorHalf = 4;
        Rectangle boundsShifted
        {
            get
            {
                return new Rectangle(Bounds.X - anchorHalf, Bounds.Y - anchorHalf, Bounds.Width, Bounds.Height);
            }
        }

        //public List<Rectangle> Hitboxes;
        //private List<Rectangle> CreateHitboxList()
        //{
        //    List<Rectangle> hitboxes = new();
        //    hitboxes.Add(HitboxUpperLeft);
        //    hitboxes.Add(HitboxUpperCenter);
        //    hitboxes.Add(HitboxUpperRight);
        //    hitboxes.Add(HitboxCenterLeft);
        //    hitboxes.Add(HitboxCenterRight);
        //    hitboxes.Add(HitboxLowerLeft);
        //    hitboxes.Add(HitboxLowerCenter);
        //    hitboxes.Add(HitboxLowerRight);
        //    return hitboxes;
        //}

        public Rectangle GetHitbox(int index)
        {
            switch (index)
            {
                case 0:
                    return HitboxUpperLeft;
                case 1:
                    return HitboxUpperCenter;
                case 2:
                    return HitboxUpperRight;
                case 3:
                    return HitboxCenterLeft;
                case 4:
                    return HitboxCenterRight;
                case 5:
                    return HitboxLowerLeft;
                case 6:
                    return HitboxLowerCenter;
                case 7:
                    return HitboxLowerRight;
                default:
                    return new Rectangle(0,0,0,0);
            }
        }
        public Rectangle HitboxUpperLeft { get { return new Rectangle(boundsShifted.Left, boundsShifted.Top, anchorsize, anchorsize); }}
        public Rectangle HitboxUpperCenter { get { return new Rectangle(boundsShifted.Left + (boundsShifted.Right - Bounds.Left) / 2, boundsShifted.Top, anchorsize, anchorsize); }}
        public Rectangle HitboxUpperRight { get { return new Rectangle(boundsShifted.Right, boundsShifted.Top, anchorsize, anchorsize); }}
        public Rectangle HitboxCenterLeft { get { return new Rectangle(boundsShifted.Left, boundsShifted.Top + (boundsShifted.Bottom - Bounds.Top) / 2, anchorsize, anchorsize); } }
        public Rectangle HitboxCenterRight { get { return new Rectangle(boundsShifted.Right, boundsShifted.Top + (boundsShifted.Bottom - Bounds.Top)/2, anchorsize, anchorsize); } }
        public Rectangle HitboxLowerLeft { get { return new Rectangle(boundsShifted.Left, boundsShifted.Bottom, anchorsize, anchorsize); } }
        public Rectangle HitboxLowerCenter { get { return new Rectangle(boundsShifted.Left + (boundsShifted.Right - Bounds.Left) / 2, boundsShifted.Bottom, anchorsize, anchorsize); } }
        public Rectangle HitboxLowerRight { get { return new Rectangle(boundsShifted.Right, boundsShifted.Bottom, anchorsize, anchorsize);  } }
        public Rectangle HitboxCenter { get { return new Rectangle(boundsShifted.Left + (boundsShifted.Right - Bounds.Left) / 2, boundsShifted.Top + (boundsShifted.Bottom - Bounds.Top) / 2, anchorsize, anchorsize); } }

        public virtual void DrawHighlight(Graphics graphic)
        {
            int anchorsize = 4;
            int anchorHalf = anchorsize / 2;
            Rectangle boundsShifted = new Rectangle(Bounds.X - anchorHalf, Bounds.Y - anchorHalf, Bounds.Width, Bounds.Height);
            
            int BoundsWidth = (boundsShifted.Right - Bounds.Left);
            int BoundsHeight = (boundsShifted.Bottom - Bounds.Top);
            int HalfWidth = BoundsWidth / 2;
            int HalfHeight = BoundsHeight / 2;

            graphic.FillRectangle(HighlightBrush, HitboxUpperLeft); // Upper Left
            graphic.FillRectangle(HighlightBrush, HitboxUpperCenter); // Upper Center
            graphic.FillRectangle(HighlightBrush, HitboxUpperRight); // Upper Right
            graphic.FillRectangle(HighlightBrush, HitboxCenterLeft); // Center Left
            graphic.FillRectangle(HighlightBrush, HitboxCenterRight); // Center Right
            graphic.FillRectangle(HighlightBrush, HitboxLowerLeft); // Lower Left
            graphic.FillRectangle(HighlightBrush, HitboxLowerCenter); // Lower Center
            graphic.FillRectangle(HighlightBrush, HitboxLowerRight); // Lower Right
            graphic.FillRectangle(HighlightBrush, HitboxCenter); // Center of symbol
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

        public GsBoundingBox(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled, int lineWeight, int lineAlpha, int fillAlpha) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight, lineAlpha, fillAlpha)
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

    public class GsRectangle : GsBoundingBox
    {
        public GsRectangle(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled, int lineWeight, int lineAlpha, int fillAlpha) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight, lineAlpha, fillAlpha)
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

                Right = originalWidth  - (int)Math.Ceiling(borderWeight / 2f) ;
                Bottom = originalHeight  - (int)Math.Ceiling(borderWeight / 2f);
            }
        }
    }

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

    public class GsBlur : GsBoundingBox
    {
        public Bitmap? blurredImage;
        private Bitmap? blurred;
        //public GsBlur(Color foregroundColor, Color backgroundColor, bool shadowEnabled, Point startPoint, Point endPoint, int lineWeight, int lineAlpha, int fillAlpha) : base(foregroundColor, backgroundColor, shadowEnabled, startPoint, endPoint, lineWeight, lineAlpha, fillAlpha)
        public GsBlur(Point startPoint, Point endPoint) : base(startPoint, endPoint)
        {
            Name = "Blur";
            drawFill = DrawFill;
        }

        public void DrawFill(Pen pen, Brush fillBrush, Rectangle rect, Graphics graphic)
        {
            if (blurredImage == null)
            {
                graphic.DrawRectangle(LinePen, Left, Top, Width, Height);

            }
            else
            {
                blurred = CropImage(blurredImage, new Rectangle(Left, Top, Width, Height));
                graphic.DrawImage(blurred, Left, Top, Width, Height);
                blurred.Dispose();
            }
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

        public GsText(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled, int lineWeight, int lineAlpha) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight, lineAlpha)
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

        internal override void DrawShape(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = true, bool outline = true)
        {   
            if (lineAlpha > 0)
            {
                Brush tempBrush = TextBrush;
                if (drawBrush == ShadowBrush)
                {
                    tempBrush = ShadowBrush;
                }
                UpdateFont();
                //graphic.PageUnit = GraphicsUnit.Pixel;
                //StringFormat stringFormat = new StringFormat();
                //stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                graphic.DrawString(text, font, tempBrush, new PointF(Left + offset.X, Top + offset.Y));
                SizeF sizeInPixels = graphic.MeasureString(text, font);
                Width = (int)sizeInPixels.Width;
                Height = (int)sizeInPixels.Height;
            }
            
        }

        public override void DrawHighlight(Graphics graphic)
        {
            graphic.DrawRectangle(HightlightSymbolPen, Bounds);
        }
    }

    public class GsLine : GraphicSymbol
    {
        public Point lineEnd;

        public GsLine(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled, int lineWeight, int lineAlpha) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight, lineAlpha)
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

        public override Rectangle Bounds
        {
            get
            {
                int _left = Math.Min(StartPoint.X, EndPoint.X);
                int _right = Math.Max(StartPoint.X, EndPoint.X);
                int _top = Math.Min(StartPoint.Y, EndPoint.Y);
                int _bottom = Math.Max(StartPoint.Y, EndPoint.Y);
                int _width = _right - _left;
                int _height = _bottom - _top;
                return new Rectangle(_left, _top, _width, _height);
            }
        }

        internal override void DrawShape(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = true, bool outline = true)
        {
            if (LineWeight < 1) { LineWeight = 1; }
            if (lineAlpha > 0)
            {
                graphic.DrawLine(drawPen, new Point(StartPoint.X + offset.X, StartPoint.Y + offset.Y) , new Point(EndPoint.X + offset.X, EndPoint.Y + offset.Y));
            }
        }

        public override void DrawShadow(Graphics graphic)
        {
            if (ShadowEnabled)
            {
                for (int i = 1; i < ShadowDistance && i < LineWeight; i++)
                {
                    DrawShape(graphic, ShadowPen, ShadowBrush, new Point(i, i));
                }
            }
        }

        //public override void DrawHighlight(Graphics graphic)
        //{
            
        //    graphic.DrawRectangle(HightlightSymbolPen, Bounds);
        //}
    }

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

    public class GsImage : GraphicSymbol
    {
        readonly Image? image;

        public GsImage(Point startPoint, Point endPoint, bool shadow) : base(startPoint, endPoint, shadow)
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

        internal override void DrawShape(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = true, bool outline = true)
        {
            if (image != null)
            {
                graphic.DrawImageUnscaled(image, Left, Top);
            }
        }

        public override void DrawShadow(Graphics graphic)
        {
            if (ShadowEnabled)
            {
                for (int i = 1; i < ShadowDistance; i++)
                {
                    // fill
                    //DrawShape(graphic, ShadowPen, ShadowBrush, new Point(i, i), true, false);
                    graphic.FillRectangle(ShadowBrush, new Rectangle(Left + i, Top + i, Width, Height));
                }
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

        public GsImageScaled(Point startPoint, Point endPoint, bool shadow) : base(startPoint, endPoint, shadow)
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

        //public override void DrawSymbol(Graphics graphic)
        internal override void DrawShape(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = true, bool outline = true)
        {
            if (image != null)
            {
                graphic.DrawImage(image, Left, Top, Width, Height);
            }
        }

        public override void DrawShadow(Graphics graphic)
        {
            if (ShadowEnabled)
            {
                for (int i = 1; i < ShadowDistance; i++)
                {
                    graphic.FillRectangle(ShadowBrush, new Rectangle(Left + i, Top + i, Width, Height));
                }
            }
        }

        public override void Dispose()
        {
            image?.Dispose();
        }
    }
}
