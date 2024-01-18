using ScreenShotTool.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility

public class GsCrop : GsBoundingBox
{
    public bool showOutline = true;
    Pen cropPenBase;
    Pen cropPenDots;
    public GsCrop(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor) : base(startPoint, endPoint, foregroundColor, backgroundColor)
    {
        Name = "Crop";
        cropPenBase = new Pen(Color.Black, 1);
        cropPenDots = new Pen(Color.White, 1);
        cropPenDots.DashPattern = [3f, 3f];
        drawLine = DrawLine;
        LineWeight = 1;
    }

    internal void DrawLine(Pen pen, Brush b, Rectangle r, Graphics graphic)
    {
        if (showOutline == false) return; 
        Rectangle BoundsOutside = new Rectangle(Bounds.Left -1, Bounds.Top -1, Bounds.Width + 1, Bounds.Height + 1);
        graphic.DrawRectangle(cropPenBase, BoundsOutside);
        graphic.DrawRectangle(cropPenDots, BoundsOutside);
    }
}
