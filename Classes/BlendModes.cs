namespace ScreenShotTool
{
    public class ColorBlend
    {
        // https://en.wikipedia.org/wiki/Blend_modes

        // Warning, none of these are accurate if the two colors have DIFFERENT alpha values

        public enum BlendModes
        {
            None = 0,
            Normal = 1,
            Multiply = 2,
            Divide = 3,
            Lighten = 4,
            Darken = 5,
            Desaturate = 6,
            Invert = 7,
            Average = 8,
        }

        public static Color BlendColors(Color color1, Color color2, BlendModes blendmode)
        {
            return blendmode switch
            {
                BlendModes.None => color1, // keeps the bottom color unchanged
                BlendModes.Normal => color2, // uses the top color unchanged
                BlendModes.Multiply => Multiply(color1, color2), // all colors are darkened by the amount in color2
                BlendModes.Divide => Divide(color1, color2), // this one gets kinda weird
                BlendModes.Lighten => Lighten(color1, color2), // only colors that are darker than color2 are lightened
                BlendModes.Darken => Darken(color1, color2), // only colors that are lighter than color2 are darkened
                BlendModes.Desaturate => Desaturate(color1, color2), // grayscale
                BlendModes.Invert => Invert(color1), // outputs the inverted color, light channels becomes dark, dark becomes light
                BlendModes.Average => Average(color1, color2), // same as Normal with 50% opacity
                _ => color1
            };
        }

        public static int CombineTransparencies(Color color1, Color color2)
        {
            float a1 = color1.A / 255f;
            float a2 = color2.A / 255f;
            float transparency = a1 * a2;
            int A = (int)(transparency * 255);
            return int.Clamp(A, 0, 255);
        }

        public static int MultiplyColorChannel(int channel1, int channel2)
        {
            float c1 = channel1 / 255f;
            float c2 = channel2 / 255f;
            float mult = c1 * c2;
            int result = (int)(mult * 255);
            return int.Clamp(result, 0, 255);
        }

        public static int DivideColorChannel(int channel1, int channel2)
        {
            float c1 = channel1 / 255f;
            float c2 = channel2 / 255f;
            if (c2 <= 0) return 255;
            float div = c1 / c2;
            int result = (int)(div * 255);
            return int.Clamp(result, 0, 255);
        }

        public static int AverageColorChannel(int channel1, int channel2)
        {
            float c1 = channel1 / 255f;
            float c2 = channel2 / 255f;
            float avg = (c1 + c2) / 2;
            int result = (int)(avg * 255);
            return int.Clamp(result, 0, 255);
        }

        public static Color Average(Color color1, Color color2)
        {
            int Alpha = CombineTransparencies(color1, color2);
            return Color.FromArgb(Alpha, AverageColorChannel(color1.R, color2.R), AverageColorChannel(color1.G, color2.G), AverageColorChannel(color1.B, color2.B));
        }

        public static Color Lighten(Color color1, Color color2)
        {
            int Alpha = CombineTransparencies(color1, color2);
            return Color.FromArgb(Alpha, Math.Max((int)color1.R, color2.R), Math.Max((int)color1.G, color2.G), Math.Max((int)color1.B, color2.B));
        }

        public static Color Darken(Color color1, Color color2)
        {
            int Alpha = CombineTransparencies(color1, color2);
            return Color.FromArgb(Alpha, Math.Min((int)color1.R, color2.R), Math.Min((int)color1.G, color2.G), Math.Min((int)color1.B, color2.B));
        }

        public static Color Multiply(Color color1, Color color2)
        {
            int Alpha = CombineTransparencies(color1, color2);
            return Color.FromArgb(Alpha, MultiplyColorChannel(color1.R, color2.R), MultiplyColorChannel(color1.G, color2.G), MultiplyColorChannel(color1.B, color2.B));
        }

        public static Color Divide(Color color1, Color color2)
        {
            int Alpha = CombineTransparencies(color1, color2);
            return Color.FromArgb(Alpha, DivideColorChannel(color1.R, color2.R), DivideColorChannel(color1.G, color2.G), DivideColorChannel(color1.B, color2.B));
        }

        public static Color Desaturate(Color color1, Color color2)
        {
            int Alpha = CombineTransparencies(color1, color2);
            int avgColor = Math.Clamp((int)((color1.R + color1.G + color1.B) / 3f), 0, 255);
            return Color.FromArgb(Alpha, avgColor, avgColor, avgColor);
        }

        public static Color Invert(Color color1)
        {
            //int Alpha = CombineTransparencies(color1, color2);
            return Color.FromArgb(color1.A, 255 - color1.R, 255 - color1.G, 255 - color1.B);
        }
    }
}
