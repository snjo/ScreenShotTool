using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ScreenShotTool.Classes
{
    [SupportedOSPlatform("windows")]

    public class InfoTagCategory (bool IsChecked, string DisplayName, string Description)
    {
        public bool Enabled { get; set; } = IsChecked;
        public string Name { get; set; } = DisplayName;
        public string Description { get; set; } = Description;

        public override string ToString()
        {
            return Name;
        }

        public CheckState CheckState
        {
            get
            {
                if (Enabled)
                    return CheckState.Checked;
                else
                    return CheckState.Unchecked;
            }
            set
            {
                Debug.WriteLine($"Setting check state to {value}");
                Enabled = value == CheckState.Checked;
            }
        }
    }


}
