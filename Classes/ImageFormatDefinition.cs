using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool
{
    public class ImageFormatDefinition(string name, string extension, ImageFormat format)
    {
        /// <summary>
        /// The display name of the file type, used in the FileDialog filter
        /// </summary>
        public string Name = name;
        /// <summary>
        /// File Extension, including "."
        /// </summary>
        public string Extension = extension;
        /// <summary>
        /// Returns name + |* + extension
        /// </summary>
        public string FilterString = name + "|" + extension;
        public ImageFormat Format = format;
    }
}
