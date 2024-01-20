using ScreenShotTool.Forms;
using ScreenShotTool.Properties;
using Font = System.Drawing.Font;

namespace ScreenShotTool.Classes;
#pragma warning disable CA1416 // Validate platform compatibility
public class GsNumbered : GraphicSymbol
{
    public int Number = 0;
    private readonly FontFamily fontFamily = FontFamily.GenericSansSerif;
    private float fontEmSize = 12f;
    private readonly FontStyle fontStyle = FontStyle.Bold;
    private Font font;
    private readonly SolidBrush fontBrush = new(Color.White);
    private readonly SolidBrush circleBrush = new(Color.Maroon);
    public int Diameter = 30; // update DefaultRadius if you change Diameter here
    public int DefaultRadius = 15;

    public GsNumbered(Point startPoint, Point endPoint, Color foregroundColor, Color backgroundColor, bool shadowEnabled, int lineWeight = 1) : base(startPoint, endPoint, foregroundColor, backgroundColor, shadowEnabled, lineWeight)
    {
        Name = "Number";
        font = CreateFont();
        FillColor = Settings.Default.GsNumberedColor; //Color.Maroon;
        LineColor = ColorTools.GetTextColorFromBackground(FillColor);
        ValidSymbol = true;
        AllowClickPlacement = true;
    }

    internal override void DrawShape(Graphics graphic, Pen drawPen, Brush drawBrush, Point offset, bool fill = true, bool outline = true)
    {
        fontEmSize = Diameter / 2.5f;
        font = CreateFont();
        circleBrush.Color = FillColor;
        fontBrush.Color = LineColor;
        string text = Number.ToString();
        graphic.FillEllipse(circleBrush, new Rectangle(Left, Top, Diameter, Diameter));
        SizeF sizeInPixels = graphic.MeasureString(text, font);
        int WidthToSpare = Diameter - (int)sizeInPixels.Width;
        int HeightToSpare = Diameter - (int)sizeInPixels.Height;
        graphic.DrawString(text, font, fontBrush, new PointF(Left + offset.X + (WidthToSpare / 2), Top + offset.Y + (HeightToSpare / 2)));
    }

    public override void DrawShadow(Graphics graphic)
    {
        if (ShadowEnabled)
        {
            for (int i = 1; i < ShadowDistance && i < DefaultRadius / 2; i++)
            {
                // fill
                graphic.FillEllipse(ShadowBrush, new Rectangle(Left + i, Top + i, Diameter, Diameter));
            }
        }

    }

    private Font CreateFont()
    {
        return new Font(fontFamily, fontEmSize, fontStyle);
    }

    public override int Width
    {
        get
        {
            return Diameter;
        }
        set
        {
            Diameter = value;
        }
    }

    public override int Height
    {
        get
        {
            return Diameter;
        }
    }
}
