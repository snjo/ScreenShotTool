using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool.Classes;
[SupportedOSPlatform("windows")]

internal class PropertyPanels
{
    public List<Control> SymbolControls = [];

    internal void Add(Control control, Control parentPanel)//, Point groupLocation)
    {
        parentPanel.Controls.Add(control);
        SymbolControls.Add(control);
        //control.Left = groupLocation.X;
        //if (SymbolControls.Last() != null)
        //{
        //    control.Top = SymbolControls.Last().Bottom + 5 + (groupLocation.Y);
        //}

        foreach (Control c in control.Controls)
        {
            c.KeyPress += SupressEnterDing;
        }
    }

    private static void SupressEnterDing(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            e.Handled = true;
            e.KeyChar = '0';
            // this character doesn't actually get sent, since it's after Handled, but will prevent the keypress reacting to Enter, since it no longer is Enter
        }
    }
}
