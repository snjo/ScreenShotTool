using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Autorun;
[SupportedOSPlatform("windows")]
public static class Autorun
{
    private const string autoStartKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

    public static bool IsEnabled(string ApplicationName)
    {
        using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(autoStartKey, false))
        {
            if (key != null)
            {
                return key.GetValue(ApplicationName, null) != null;
            }
            else
            {
                return false;
            }
        }
    }

    public static void Enable(string ApplicationName)
    {
        using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(autoStartKey, true))
        {
            if (key != null)
            {
                key.SetValue(ApplicationName, "\"" + Application.ExecutablePath + "\"");
            }
        }
    }    
    
    public static void Disable(string ApplicationName)
    {
        using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(autoStartKey, true))
        {
            if (key != null)
            {
                key.DeleteValue(ApplicationName, false);
            }
        }
    }
}
