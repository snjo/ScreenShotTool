using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility
public class GsPolygon : GraphicSymbol
{
    PolygonDrawing? Polygon;
    bool isTemporarySymbol = false;

    public GsPolygon(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled, int lineWeight) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight)
    {
        DateTime creation = DateTime.Now;
        ValidSymbol = true;
        Name = $"Polygon {creation.Second} {creation.Millisecond}";
    }

    public static GsPolygon Create(Point TopLeft, PolygonDrawing? newPolygon, bool isTemporarySymbol, Color lineColor, int lineWeight)
    {
        if (newPolygon != null)
        {
            GsPolygon newSymbol = new GsPolygon(TopLeft, new Point(newPolygon.Contents.Width, newPolygon.Contents.Height), lineColor, Color.Empty, false, lineWeight);
            if (isTemporarySymbol)
            {
                newSymbol.Polygon = newPolygon;
                //newSymbol.Left = newPolygon.LeftMostPixel;
                //newSymbol.Top = newPolygon.TopMostPixel;
            }
            else
            {
                newSymbol.Polygon = newPolygon;
                newSymbol.Left = newPolygon.Contents.Left;
                newSymbol.Top = newPolygon.Contents.Top;
                newSymbol.Width = newPolygon.Contents.Width;
                newSymbol.Height = newPolygon.Contents.Height;
            }
            newSymbol.isTemporarySymbol = isTemporarySymbol;
            return newSymbol;
        }

        GsPolygon badSymbol = new GsPolygon(new Point(0, 0), new Point(10, 10), Color.Red, Color.Green, false, 1);
        badSymbol.ValidSymbol = false;
        return badSymbol;
    }

    public override void DrawSymbol(Graphics graphic)
    {
        if (Polygon != null)
        {
            Polygon.pen.Color = LineColor;
            Polygon.pen.Width = LineWeight;
            if (isTemporarySymbol)
            {
                // don't move the symbol around during drawing
                //Polygon.Draw(graphic, 0, 0, Polygon.Contents.Width, Polygon.Contents.Height);
                Polygon.Draw(graphic, 0, 0, false, 0, 0);
            }
            else
            {
                // move the symbol into new place if it's moved from drawn location
                Polygon.Draw(graphic, Left - Polygon.LeftMostPixel, Top - Polygon.TopMostPixel, true, Width, Height);
            }
        }
    }
}
