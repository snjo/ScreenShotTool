using Microsoft.Win32;
using ScreenShotTool.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool.Classes;
[SupportedOSPlatform("windows")]



public class SettingsRegistry : IDisposable
{
    string RegKeyName = @"SOFTWARE\ScreenshotTool";
    public SettingsRegistry()
    {
    }

    public void LoadSettingsFromRegistry()
    {
        RegistryKey? RegKey = Registry.CurrentUser.OpenSubKey(RegKeyName);
        Debug.WriteLine("Loading fallback values from registry");
        if (RegKey == null)
        {
            Debug.WriteLine("RegKey is null");
            return;
        }
        
        string? foldername = RegKey.GetValue("Foldername")?.ToString();
        string? filename = RegKey.GetValue("Filename")?.ToString();
        string? fileextension = RegKey.GetValue("Fileextension")?.ToString();
        if (string.IsNullOrEmpty(foldername) == false)
        {
            Settings.Default.Foldername = foldername;
        }
        if (string.IsNullOrEmpty(filename) == false)
        {
            Settings.Default.Filename = filename;
        }
        if (string.IsNullOrEmpty(fileextension) == false)
        {
            Settings.Default.FileExtension = fileextension;
        }


        foreach (string hotkey in MainForm.HotkeyNames)
        {
            string? v = RegKey.GetValue(hotkey)?.ToString();
            if (v != null)
            {
                (string key, bool Ctrl, bool Alt, bool Shift, bool Win) = HotkeyFromRegistryString(v);
                Settings.Default["hk" + hotkey + "Key"] = key;
                Settings.Default["hk" + hotkey + "Ctrl"] = Ctrl;
                Settings.Default["hk" + hotkey + "Alt"] = Alt;
                Settings.Default["hk" + hotkey + "Shift"] = Shift;
                Settings.Default["hk" + hotkey + "Win"] = Win;
            }
        }

        RegKey.Dispose();
    }

    public void SaveSettingsToRegistry()
    {
        RegistryKey RegKey = Registry.CurrentUser.CreateSubKey(RegKeyName);
        //RegKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ScreenshotTool");
        Debug.WriteLine("Saving fallback values to registry");
        if (RegKey == null)
        {
            Debug.WriteLine("RegKey is null");
            return;
        }
        Debug.WriteLine("Writing to registry, foldername: " + Settings.Default.Foldername);
        RegKey.SetValue("Foldername", Settings.Default.Foldername);
        RegKey.SetValue("Filename", Settings.Default.Filename);
        RegKey.SetValue("Fileextension", Settings.Default.FileExtension);

        foreach (string hotkey in MainForm.HotkeyNames)
        {
            string v = HotkeyToRegistryString(
                Settings.Default["hk" + hotkey + "Key"].ToString() + "",
                bool.Parse(Settings.Default["hk" + hotkey + "Ctrl"].ToString() + ""),
                bool.Parse(Settings.Default["hk" + hotkey + "Alt"].ToString() + ""),
                bool.Parse(Settings.Default["hk" + hotkey + "Shift"].ToString() + ""),
                bool.Parse(Settings.Default["hk" + hotkey + "Win"].ToString() + "")
                );
            Debug.WriteLine($"Setting key {hotkey} to {v}");
            RegKey.SetValue(hotkey, v);
        }

        RegKey.Close();
        RegKey.Dispose();
    }

    private string HotkeyToRegistryString(string hotkeyKey, bool Ctrl, bool Alt, bool Shift, bool Win)
    { 
        StringBuilder keyValue = new StringBuilder();
        keyValue.Append(Ctrl ? "1" : "0");
        keyValue.Append(Alt ? "1" : "0");
        keyValue.Append(Shift ? "1" : "0");
        keyValue.Append(Win ? "1" : "0");
        keyValue.Append(" ");
        keyValue.Append(hotkeyKey);
        return keyValue.ToString();
    }

    private (string hotkeyKey, bool Ctrl, bool Alt, bool Shift, bool Win) HotkeyFromRegistryString(string RegistryString)
    {
        if (RegistryString.Length < 6) return ("", false, false, false, false);

        bool Ctrl = RegistryString[0] == '1';
        bool Alt = RegistryString[1] == '1';
        bool Shift = RegistryString[2] == '1';
        bool Win = RegistryString[3] == '1';
        string key = RegistryString.Substring(5);
        return (key, Ctrl, Alt, Shift, Win);
    }

    public void Dispose()
    {
        //RegKey?.Dispose();
    }
}
