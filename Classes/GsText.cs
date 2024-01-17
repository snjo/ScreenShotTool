namespace ScreenShotTool.Forms;
#pragma warning disable CA1416 // Validate platform compatibility
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

    public override void DrawHitboxes(Graphics graphic)
    {
        graphic.DrawRectangle(HightlightSymbolPen, Bounds);
    }
}

