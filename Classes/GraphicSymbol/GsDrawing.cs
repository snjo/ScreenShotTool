using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility
public class GsDrawing : GraphicSymbol
{
    public Bitmap? drawing;

    public GsDrawing(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled = false, int lineWeight = 1) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight)
    {
        ValidSymbol = true;
        Name = "Freehand";
    }

    public static GsDrawing Create(Point TopLeft, Point BottomRight, Bitmap? image, bool temp)
    {
        //string suffix = temp ? "(temp)" : ("OK");
        if (image != null)
        {
            //try
            //{
                GsDrawing newSymbol = new GsDrawing(TopLeft, new Point(image.Width, image.Height), Color.Red, Color.Green, false, 1);
                if (temp)
                {
                    newSymbol.drawing = image;
                }
                else
                {
                    newSymbol.drawing = image.Clone(new Rectangle(0, 0, image.Width, image.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                }
                return newSymbol;
            //}
            //catch
            //{
            //    Debug.WriteLine("GsDrawing.Create Exception");
            //}
        }

        GsDrawing badSymbol = new GsDrawing(new Point(0,0), new Point(10,10), Color.Red, Color.Green, false, 1);
        badSymbol.ValidSymbol = false;
        //badSymbol.Name = $"Freehand error {suffix}";
        //Debug.WriteLine($"GsDrawing.Create: can't create valid symbol, image is null, temp: {temp}");
        return badSymbol;
        
    }

    public override void DrawSymbol(Graphics graphic)
    {
        if (drawing != null)
        {
            graphic.DrawImageUnscaled(drawing, new Point(Left, Top));
        }
        else
        {
            //Debug.WriteLine("GsDrawing.DrawSymbol: Can't draw freehand drawing, bitmap is null");
        }
    }

    public override void Dispose()
    {
        //Debug.WriteLine("Disposing GsDrawing");
        // don't dispose the drawing, it's used by the temp image process outside.
    }

    //internal override void DrawShape(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = false, bool outline = true)
    //{

    //}
}
