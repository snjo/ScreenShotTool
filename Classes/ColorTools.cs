using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool.Classes
{
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
    }
}
