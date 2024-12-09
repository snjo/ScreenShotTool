using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool.Classes
{
    internal class InfoTagCategory (bool IsChecked, string DisplayName, string Description)
    {
        public bool IsChecked { get; set; } = IsChecked;
        public string DisplayName { get; set; } = DisplayName;
        public string Description { get; set; } = Description;
    }


}
