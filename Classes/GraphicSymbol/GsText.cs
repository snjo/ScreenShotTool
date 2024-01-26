using ScreenShotTool.Forms;

namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility
public class GsText : GraphicSymbol
{
    public FontFamily fontFamily = FontFamily.GenericSansSerif;
    public float fontEmSize = 10f;
    //public float fontSize = 10f;
    public FontStyle fontStyle = FontStyle.Regular;
    Font font;
    public string Text = "Text";
    readonly int maxFontSize;
    readonly int minFontSize;

    public GsText(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled)
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
        if (TextColor.A > 0)
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
            graphic.DrawString(Text, font, tempBrush, new PointF(Left + offset.X, Top + offset.Y));
            SizeF sizeInPixels = graphic.MeasureString(Text, font);
            Width = (int)sizeInPixels.Width;
            Height = (int)sizeInPixels.Height;
        }
    }

    public override void DrawHitboxes(Graphics graphic)
    {
        graphic.DrawRectangle(HighlightSymbolPen, Bounds);
    }
}

