using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility
public class GsDrawing : GraphicSymbol
{
    public Bitmap? drawing;
    bool drawingIsCloned = false;
    bool temp = true;

    private GsDrawing(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled = false, int lineWeight = 1) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight)
    {
        ValidSymbol = true;
        Name = "Freehand";
        ScalingAllowed = false;
        
    }

    //public static GsDrawing Create(Point TopLeft, Point BottomRight, Bitmap? image, bool temp, Color lineColor)
    public static GsDrawing Create(Point TopLeft, Point BottomRight, FreehandDrawing? drawing, bool temp, Color lineColor)
    {
        //string suffix = temp ? "(temp)" : ("OK");
        if (drawing != null)
        {
            GsDrawing newSymbol = new GsDrawing(TopLeft, new Point(drawing.Bitmap.Width, drawing.Bitmap.Height), lineColor, Color.Empty, false, 1);
            if (temp)
            {
                newSymbol.drawing = drawing.Bitmap;
                newSymbol.drawingIsCloned = false;
            }
            else
            {
                newSymbol.drawing = EditorCanvas.CropImage(drawing.Bitmap, drawing.Contents);
                newSymbol.Left = drawing.Contents.Left;
                newSymbol.Top = drawing.Contents.Top;
                newSymbol.Width = drawing.Contents.Width;
                newSymbol.Height = drawing.Contents.Height;
                newSymbol.drawingIsCloned = true;
            }
            newSymbol.temp = temp;
            return newSymbol;
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
        // don't dispose the drawing, if it's used by the temp image process outside.
        if (drawingIsCloned && temp == false)
        {
            drawing.DisposeAndNull();
        }
        base.Dispose();
    }
}
