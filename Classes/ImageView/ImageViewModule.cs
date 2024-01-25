﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool;
[SupportedOSPlatform("windows")]

public class ImageViewModule()
{
    public Bitmap? ImageSource;
    public virtual void Update()
    {
    }

    public virtual void HandleKeys(KeyEventArgs e)
    {
        e.Handled = true;
    }

    public virtual void MouseUp(MouseEventArgs e) { }

    public virtual void MouseDown(MouseEventArgs e) { }

    public virtual void MouseMove(MouseEventArgs e) { }

    public virtual void MouseLeave(EventArgs e) { }
}
