using System.Diagnostics;

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
            Contrast = 9,
            InvertBrightness = 10,
            TintBrightColors = 11,
        }

        public static Color BlendColors(Color color1, Color color2, BlendModes blendmode) //, float AffectChannelRed = 1f, float AffectChannelGreen = 1f, float AffectChannelBlue = 1f)
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
                BlendModes.InvertBrightness => InvertBrighness(color1, color2),
                BlendModes.TintBrightColors => TintBrightColors(color1, color2),
                BlendModes.Average => Average(color1, color2), // same as Normal with 50% opacity
                BlendModes.Contrast => Contrast(color1, color2),
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

        public static int ContrastColorChannel(int channel1, int channel2)
        {
            // https://www.dfstudios.co.uk/articles/programming/image-programming-algorithms/image-processing-algorithms-part-5-contrast-adjustment/
            // channel2 is used for contrast amount, gray 128 is unchanged, less becomes grayer, greater is more contrast. Color hues are valid for per-channel contrast.
            // Shifting the channel2/contrast value up so 0 becomes -255, and 255 becomes remains 255, to allow for more or less contrast
            channel2 = (channel2 * 2) - 255;
            float contrast = channel2;
            float factor = (259f * (contrast + 255f)) / (255f * (259f - contrast));
            return (int)Math.Clamp(factor * (channel1 - 128) + 128, 0, 255);
        }

        private static Color Contrast(Color color1, Color color2)
        {
            int Alpha = CombineTransparencies(color1, color2);
            return Color.FromArgb(Alpha, ContrastColorChannel(color1.R, color2.R), ContrastColorChannel(color1.G, color2.G), ContrastColorChannel(color1.B, color2.B));
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
            // uses perceptual brightness to desaturate, blues are much darker than greens.

            int Alpha = CombineTransparencies(color1, color2);
            int desaturated = (int)(ColorTools.ColorBrightness(color1) * 255f);
            return Color.FromArgb(Alpha, desaturated, desaturated, desaturated);
        }

        public static Color DesaturateAverageRGB(Color color1, Color color2)
        {
            // uses an average color, not perceptual, so pure greens are just as dark as blues and reds
            // not in use since there's seldom a desire for this effect.

            int Alpha = CombineTransparencies(color1, color2);
            int avgColor = Math.Clamp((int)((color1.R + color1.G + color1.B) / 3f), 0, 255);
            return Color.FromArgb(Alpha, avgColor, avgColor, avgColor);
        }

        public static Color Invert(Color color1)
        {
            //Inverts the brightness, also inverts the hues, a classic.
            //int Alpha = CombineTransparencies(color1, color2);
            return Color.FromArgb(color1.A, 255 - color1.R, 255 - color1.G, 255 - color1.B);
        }

        public static Color InvertBrighness(Color color1, Color color2)
        {
            // Inverts the image brighness, while maintaining the hue.
            //color1 is the underlying image
            //color2 is used to adjust the brightness of the extremes (dark to light as whites or pure color)
            //color2 RED channel is used to transition between using BRIGHTNESS and PERCEPTUAL BRIGHTNESS
            //color2 GREEN channel is used to transition between using SOURCE SATURATION and IMPROVED SATURATION, allowing for bright colors to move toward whites
            //color2 BLUE channel is not used
            // using RED and GREEN because that matches the default Yellow highlighter values and gives the prettiest result.
            double hue;
            double saturation;
            double brightness;
            ColorTools.ColorToHSV(color1, out hue, out saturation, out brightness);
            double perceptualBrightness = ColorTools.ColorBrightness(color1);
            double brightnessInvert1 = 1 - brightness;
            double brightnessInvert2 = 1 - perceptualBrightness;

            double finalBrightness = Double.Lerp(brightnessInvert1, brightnessInvert2, color2.R / 255d); // fade between Color.Brightness and perceptual brightness
            double finalSaturation = Double.Lerp(saturation, brightness, color2.G / 255d); // using brightness for saturation allows dark>bright colors to fade into whites

            Color outColor = ColorTools.ColorFromHSV(hue, finalSaturation, finalBrightness);

            return outColor;
        }

        public static Color TintBrightColors(Color color1, Color color2)
        {
            int darkest = Math.Min(color1.R, color1.G);
            darkest = 64; // don't tint the darkest pixels

            int R2 = Math.Max(darkest, color2.R);
            int G2 = Math.Max(darkest, color2.G);
            int B2 = Math.Max(darkest, color2.B);

            int R = Math.Min(color1.R, R2);
            int G = Math.Min(color1.G, G2);
            int B = Math.Min(color1.B, B2);

            int Alpha = CombineTransparencies(color1, color2);
            return Color.FromArgb(Alpha, R, G, B);
        }
    }
}
