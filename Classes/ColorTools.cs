namespace ScreenShotTool;
#pragma warning disable CA1416 // Validate platform compatibility

public static class ColorTools
{
    public static float ColorBrightness(Color color)
    {
        // returns a value of 0-1f based on the total brightness of the input color
        //https://stackoverflow.com/questions/596216/formula-to-determine-perceived-brightness-of-rgb-color
        //https://en.wikipedia.org/wiki/Relative_luminance
        //perceived value (0.2126 * R + 0.7152 * G + 0.0722 * B)
        float pR = 0.2126f;
        float pG = 0.7152f;
        float pB = 0.0722f;
        float result = ((color.R * pR) + (color.G * pG) + (color.B * pB)) / 256f;
        return result;
    }

    public static Color GetTextColorFromBackground(Color backColor, float brightnessThreshold = 0.6f, int alphaThreshold = 128)
    {
        return GetTextColorFromBackground(backColor, Color.Black, Color.White, brightnessThreshold, alphaThreshold);
    }

    public static Color GetTextColorFromBackground(Color backColor, Color DarkColor, Color BrightColor, float brightnessThreshold = 0.6f, int alphaThreshold = 128)
    {
        if (ColorTools.ColorBrightness(backColor) < brightnessThreshold && backColor.A > alphaThreshold)
        {
            return BrightColor;
        }
        else
        {
            return DarkColor;
        }
    }

    public static void SetButtonColors(Button button, Color BackColor)
    {
        button.BackColor = BackColor;
        button.ForeColor = ColorTools.GetTextColorFromBackground(BackColor);
    }
}
