using System.Numerics;

namespace ScreenShotTool.Classes
{
    public static class ExtensionMethods
    {
        public static Point Subtract(this Point point1, Point point2)
        {
            return new Point(point1.X - point2.X, point1.Y - point2.Y);
        }

        public static Point Add(this Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }

        public static float DistanceTo(this Point point1, Point point2)
        {
            Vector2 v1 = new(point1.X, point1.Y);
            Vector2 v2 = new(point2.X, point2.Y);
            return Vector2.Distance(v1, v2);
        }
    }
}
