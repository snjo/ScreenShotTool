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
            Draw(graphics, -LeftMostPixel, -TopMostPixel);
        }
        return bitmap;
    }

    public void Draw(Graphics graphics, int offsetX = 0, int offsetY = 0)
    {
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        Point[] offsetPoints = new Point[PointList.Count];
        for (int i = 0; i < PointList.Count; i++)
        {
            offsetPoints[i] = new Point(PointList[i].X + offsetX, PointList[i].Y + offsetY);
        }

        if (offsetPoints.Length > 1)
        {
            graphics.DrawLines(pen, offsetPoints);
        }
    }

    public void Dispose()
    {

    }
}
