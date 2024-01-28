using System.Diagnostics;
using System.Drawing;

namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility

public class PolygonDrawing
{
    Bitmap? bitmap;
    public Point Location = new Point(0, 0);
    public Size Size;
    private bool noPixelsSet = true;
    public int LeftMostPixel = 0;
    public int RightMostPixel = 0;
    public int TopMostPixel = 0;
    public int BottomMostPixel = 0;
    DateTime lastUpdate = DateTime.MinValue;

    public List<Point> PointList = new();
    public Pen pen;

    public PolygonDrawing(Pen pen)
    {
        this.pen = pen;
    }

    private void UpdateContentBounds(Pen pen, Point point)
    {
        int penRadius = (int)Math.Ceiling(pen.Width / 2f);
        int left = point.X - penRadius;
        int right = point.X + penRadius;
        int top = point.Y - penRadius;
        int bottom = point.Y + penRadius;
        if (noPixelsSet)
        {
            LeftMostPixel = left;
            RightMostPixel = right;
            TopMostPixel = top;
            BottomMostPixel = bottom;
            noPixelsSet = false;
        }
        else
        {
            LeftMostPixel = Math.Min(left, LeftMostPixel);
            RightMostPixel = Math.Max(right, RightMostPixel);
            TopMostPixel = Math.Min(top, TopMostPixel);
            BottomMostPixel = Math.Max(bottom, BottomMostPixel);
        }
    }

    public void AddLine(Point point1, Point point2)
    {
        UpdateContentBounds(pen, point1);
        UpdateContentBounds(pen, point2);
        PointList.Add(point1);
    }

    public void AddPoint(Point point)
    {
        UpdateContentBounds(pen, point);
        PointList.Add(point);
    }

    public Rectangle Contents
    {
        get
        {
            return new Rectangle(LeftMostPixel, TopMostPixel, RightMostPixel - LeftMostPixel, BottomMostPixel - TopMostPixel);
        }
    }

    public Bitmap ToBitmap()
    {
        bitmap.DisposeAndNull();
        bitmap = new Bitmap(Math.Max(1,Contents.Width), Math.Max(1,Contents.Height));
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            //Draw(graphics, pen, -LeftMostPixel, -TopMostPixel, false, Contents.Width, Contents.Height);
        }
        return bitmap;
    }

    public void Draw(Graphics graphics, Pen drawPen, Brush drawBrush, int offsetX, int offsetY, bool scale, float Width, float Height, bool closed = false, bool fill = false, float curveTension = 0.5f)
    {
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //Pen tempPen = new Pen(drawPen.Color, drawPen.Width);
        //tempPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
        //tempPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        //tempPen.MiterLimit = tempPen.Width;
        //tempPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
        Point[] offsetPoints = new Point[PointList.Count];
        float scaleWidth = Width / Contents.Width;
        float scaleHeight = Height / Contents.Height;
        for (int i = 0; i < PointList.Count; i++)
        {
            if (scale)
            {
                Point zeroed = PointList[i].Subtract(new Point(Contents.Left, Contents.Top));
                Point scaledPoint = new Point((int)(zeroed.X * scaleWidth), (int)(zeroed.Y * scaleHeight));
                offsetPoints[i] = scaledPoint.Add(new Point(offsetX, offsetY)).Add(new Point(Contents.Left, Contents.Top));
            }
            else
            {
                offsetPoints[i] = PointList[i].Add(new Point(offsetX, offsetY));
            }
            
        }

        if (fill)
        {
            if (offsetPoints.Length > 2)
            {
                graphics.FillClosedCurve(drawBrush, offsetPoints, System.Drawing.Drawing2D.FillMode.Winding, curveTension);

            }
        }

        if (drawPen.Width > 0)
        {
            if (closed && offsetPoints.Length > 2)
            {
                graphics.DrawClosedCurve(drawPen, offsetPoints, curveTension, System.Drawing.Drawing2D.FillMode.Winding);
            }
            else if (offsetPoints.Length > 1)
            {
                graphics.DrawCurve(drawPen, offsetPoints);
            }
        }
    }

    public void Dispose()
    {

    }
}
