using System.Drawing;

namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility

public class PolygonDrawing
{
    public Point Location = new Point(0, 0);
    public Size Size;
    private bool noPixelsSet = true;
    public int LeftMostPixel = 0;
    public int RightMostPixel = 0;
    public int TopMostPixel = 0;
    public int BottomMostPixel = 0;

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
        Bitmap bitmap = new Bitmap(Contents.Width, Contents.Height);
        Graphics graphics = Graphics.FromImage(bitmap);

        graphics.DrawLines(pen, PointList.ToArray());

        graphics.Dispose();
        return bitmap;
    }
}
