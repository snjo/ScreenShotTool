using Hotkeys;
using ScreenShotTool.Properties;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace ScreenShotTool
{
    public partial class MainForm : Form
    {
        Settings settings = Settings.Default;
        TextWindow? helpWindow;
        ImageFormat DestinationFormat = ImageFormat.Jpeg;
        int counter = 0;
        string lastFolder = ".";
        string lastSavedFile = "";
        Options? options;

        public string helpText =
            "Default filename values:\r" +
            "\n$w $c\r\n\r" +
            "\n$w: Active Window Title\r" +
            "\n$d/t/ms: Date, Time, Milliseconds\r" +
            "\n$c: Counter number (auto increments)";

        public bool showThumbnails = true;
        Bitmap? bitmap;
        ImageList imageList = new ImageList();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public Dictionary<string, Hotkey> HotkeyList = new Dictionary<string, Hotkey>();

        public List<string> HotkeyNames = new List<string>
        {
            "CaptureWindow",
            "BrowseFolder",
        };

        #region form open and close
        public MainForm()
        {
            InitializeComponent();

            HotkeyList = HotkeyTools.LoadHotkeys(HotkeyList, HotkeyNames, this);

            if (settings.RegisterHotkeys)
            {
                string[] warningKeys = HotkeyTools.RegisterHotkeys(HotkeyList);

                if (warningKeys.Length > 0 && Settings.Default.AllowTrayTooltipWarning)
                {
                    string warningText = "";
                    if (warningKeys.Length > 0)
                    {
                        foreach (string key in warningKeys)
                        {
                            warningText += Environment.NewLine + key;
                        }
                    }
                    notifyIcon1.ShowBalloonTip(1000, "Could not register hotkeys", warningText, ToolTipIcon.Warning);
                }
            }

            updateTrimStatus();

            if (Settings.Default.StartHidden)
            {
                HideApplication();
            }
            else
            {
                ShowApplication();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (settings.ThumbnailWidth > 256) settings.ThumbnailWidth = 256;
            if (settings.ThumbnailHeight > 256) settings.ThumbnailHeight = 256;
            imageList.ImageSize = new Size(settings.ThumbnailWidth, settings.ThumbnailHeight);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            listView1.LargeImageList = imageList;
        }

        private void HideApplication()
        {
            //Don't use this.ShowInTaskbar = true/false, it breaks hotkeys
            Debug.WriteLine("Hiding application");
            this.WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void ShowApplication()
        {
            this.WindowState = FormWindowState.Normal;
            Debug.WriteLine("Showing Application");
            if (this.Size.Height < 50)
            {
                Size = DefaultSize;
            }
            Show();
            BringToFront();
            SetForegroundWindow(this.Handle);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HotkeyTools.ReleaseHotkeys(HotkeyList);
        }
        #endregion

        #region Hotkeys
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.
                HandleHotkey(id);
            }
        }

        private void HandleHotkey(int id)
        {
            //Debug.WriteLine("Handle hotkey: " + id);
            //textBoxLog.Text += Environment.NewLine + "hotkey pressed: " + id + " at " + DateTime.Now.TimeOfDay;
            if (!HotkeyList.ContainsKey("CaptureWindow") || !HotkeyList.ContainsKey("BrowseFolder"))
            {
                textBoxLog.Text += Environment.NewLine + "hotkey invalid: " + id;
                return;
            }

            if (id == HotkeyList["CaptureWindow"].ghk.id)
            {
                //textBoxLog.Text += Environment.NewLine + "hotkey Capture Window";
                CaptureAction(CaptureMode.Window);
            }
            else if (id == HotkeyList["BrowseFolder"].ghk.id)
            {
                //textBoxLog.Text += Environment.NewLine + "Browse";
                BrowseFolderInExplorer(lastFolder);
            }
        }

        #endregion

        #region capture
        private void writeMessage(string text)
        {
            textBoxLog.Text = text + Environment.NewLine + textBoxLog.Text;
        }

        private void buttonScreenshot_Click(object sender, EventArgs e)
        {
            CaptureAction(CaptureMode.Window);
        }

        public enum CaptureMode
        {
            SingleScreen,
            AllScreens,
            Window,
            Region,
        }

        public void CaptureAction(CaptureMode mode)
        {
            //Debug.WriteLine("CaptureAction started");
            string DestinationFolder = settings.Foldername;
            string DestinationFileName = settings.Filename;
            string DestinationFileExtension = settings.FileExtension;
            SetImageFormat();

            if (mode == CaptureMode.Window)
            {
                DestinationFolder = ComposeFileName(settings.Foldername);
                DestinationFileName = ComposeFileName(settings.Filename);
                CaptureWindow(DestinationFolder, DestinationFileName + DestinationFileExtension, DestinationFormat);
                numericUpDownCounter.Value++;
            }
            else if (mode == CaptureMode.Region)
            {
                writeMessage("Region capture not yet implemented");
                // ------------ TODO --------------
            }

            if (showThumbnails)
            {
                UpdateThumbnails();
            }

            if (bitmap != null) bitmap.Dispose();
        }

        private string ComposeFileName(string text)
        {
            string splitTitleString = settings.SplitTitleString;
            int titleMaxLength = settings.TitleMaxLength;
            int splitTitleIndex = settings.SplitTitleIndex;
            string alternateTitle = settings.AlternateTitle;
            string date =
                DateTime.Now.Year.ToString() + "-" +
                DateTime.Now.Month.ToString() + "-" +
                DateTime.Now.Day.ToString();
            string time =
                DateTime.Now.Hour.ToString() + "_" +
                DateTime.Now.Minute.ToString() + "_" +
                DateTime.Now.Second.ToString();
            string millisecond =
                DateTime.Now.Millisecond.ToString();

            string windowTitle = MakeValidFileName(GetActiveWindowTitle());
            windowTitle = ShortenString(windowTitle, titleMaxLength);
            if (splitTitleString.Length > 0)
            {
                string[] titleSplit = windowTitle.Split(splitTitleString);
                int splitIndex = Math.Min(splitTitleIndex, titleSplit.Length - 1);
                if (titleSplit.Length > 1) windowTitle = titleSplit[splitIndex];
            }
            if (windowTitle.Length == 0) { windowTitle = alternateTitle; }

            text = text.Replace("$d", date);
            text = text.Replace("$t", time);
            text = text.Replace("$ms", millisecond);
            text = text.Replace("$w", windowTitle);
            text = text.Replace("$c", numericUpDownCounter.Value.ToString().PadLeft(3, '0'));

            return text;
        }

        private void UpdateThumbnails()
        {
            if (bitmap == null) return;
            string DestinationFileName = settings.Filename;
            int width = imageList.ImageSize.Width;
            Image thumbImg = ResizeImage(bitmap, settings.ThumbnailWidth, settings.ThumbnailHeight, Settings.Default.CropThumbnails);
            imageList.Images.Add(thumbImg);
            thumbImg.Dispose();
            ListViewItem thumb = listView1.Items.Add(DestinationFileName);
            thumb.Text = DestinationFileName;
            thumb.Tag = lastSavedFile;
            thumb.ImageIndex = imageList.Images.Count - 1;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <param name="crop">Crop the image</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height, bool crop = false)
        {
            //https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
            Debug.WriteLine("Resizing image from: " + image.Width + "x" + image.Height + " to " + width + "x" + height);
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            Image cropped;
            if (crop)
            {
                //cropped = cropImageSquare(image);
                cropped = cropImageToAspectRatio(image, Settings.Default.ThumbnailWidth, Settings.Default.ThumbnailHeight);
                Debug.WriteLine("Crop to square: " + image.Width + "x" + image.Height + " to" + cropped.Width + "x" + cropped.Height);
            }
            else
                cropped = image;

            destImage.SetResolution(cropped.HorizontalResolution, cropped.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(cropped, destRect, 0, 0, cropped.Width, cropped.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            Debug.WriteLine("Resize image returns:" + destImage.Width + "x" + destImage.Height);
            return destImage;
        }

        private static Image cropImageToAspectRatio(Image img, int aspectRatioWidth, int aspectRatioHeight)
        {
            int width = img.Width;
            int height = img.Height;
            Rectangle cropArea;
            float aspectRatioImg = (float)width / (float)height;
            float aspectRatioCrop = (float)aspectRatioWidth / (float)aspectRatioHeight;
            float aspectRatioCropInvert = (float)aspectRatioHeight / (float)aspectRatioWidth;

            Debug.WriteLine("Crop from: " + img.Width + "x" + img.Height + " to: " + aspectRatioWidth + "x" + aspectRatioHeight);

            if (aspectRatioImg == aspectRatioCrop)
            {
                Debug.WriteLine("Crop aspectratio match: " + aspectRatioImg + " / " + aspectRatioCrop);
                return img;
            }

            if (aspectRatioImg > aspectRatioCrop) //input img is wider than output crop
            {
                int newWidth = (int)(img.Height * aspectRatioCrop); //could be 1 pixel off...
                Debug.WriteLine("Wider img, Crop width: " + newWidth + " from " + img.Height + ":" + aspectRatioCrop);
                int overflow = img.Width - newWidth;
                int overflowLeft = overflow / 2;
                cropArea = new Rectangle(overflowLeft, 0, width - overflow, height);
                Debug.WriteLine("Wider img, Crop size: " + cropArea);
            }
            else //input img is taller than output crop
            {
                int newHeight = (int)(img.Width * aspectRatioCropInvert); //could be 1 pixel off...
                Debug.WriteLine("Taller img, Crop height: " + newHeight + " from " + img.Width + ":" + aspectRatioCropInvert);
                int overflow = img.Height - newHeight;
                int overflowTop = overflow / 2;
                cropArea = new Rectangle(0, overflowTop, width, height - overflow);
                Debug.WriteLine("Taller img, Crop size: " + cropArea);
            }

            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        private static Image cropImageSquare(Image img)
        {
            int width = img.Width;
            int height = img.Height;
            Rectangle cropArea;
            if (width == height)
            {
                return img;
            }
            else if (width > height)
            {
                int overflow = img.Width - img.Height;
                int overflowLeft = overflow / 2;
                int overflowRight = overflow - overflowLeft;
                cropArea = new Rectangle(overflowLeft, 0, width - overflow, height);
            }
            else
            {
                int overflow = img.Height - img.Width;
                int overflowTop = overflow / 2;
                int overflowBottom = overflow - overflowTop;
                cropArea = new Rectangle(0, overflowTop, width, height - overflow);
            }
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        private void SetImageFormat()
        {
            string DestinationFileExtension = settings.FileExtension;
            Debug.WriteLine("Set imageformat " + DestinationFileExtension);
            switch (DestinationFileExtension)
            {
                case ".jpg":
                    DestinationFormat = ImageFormat.Jpeg;
                    break;
                case ".png":
                    DestinationFormat = ImageFormat.Png;
                    break;
                case ".gif":
                    DestinationFormat = ImageFormat.Gif;
                    break;
                case ".bmp":
                    DestinationFormat = ImageFormat.Bmp;
                    break;
                case ".tiff":
                    DestinationFormat = ImageFormat.Tiff;
                    break;
                default:
                    DestinationFormat = ImageFormat.Jpeg;
                    break;
            }
        }

        private string ShortenString(string input, int maxLength)
        {
            return input.Substring(0, Math.Min(maxLength, input.Length)).Trim();
        }

        public void CaptureWindow(string folder, string filename, ImageFormat format)
        {
            RECT windowRect;
            GetWindowRect(GetActiveWindow(), out windowRect);
            if (settings.TrimChecked)
            {
                windowRect.Left += settings.TrimLeft;
                windowRect.Right -= settings.TrimRight;
                windowRect.Top += settings.TrimTop;
                windowRect.Bottom -= settings.TrimBottom;
            }
            if (windowRect.Width > 0 && windowRect.Height > 0)
            {
                bitmap = CaptureBitmap(windowRect.Left, windowRect.Top, windowRect.Width, windowRect.Height);
                counter++;
                SaveBitmap(folder, filename, format, bitmap);
            }
            else
            {
                Debug.WriteLine("Capture size is less than zero. Capture aborted.");
                if (Settings.Default.AllowTrayTooltipWarning)
                    notifyIcon1.ShowBalloonTip(1000, "Capture error", "Capture size is less than zero. Capture aborted.", ToolTipIcon.Warning);
            }
        }

        private bool SaveBitmap(string folder, string filename, ImageFormat format, Bitmap capture)
        {
            if (folder.Length < 1)
            {
                folder = ".";
            }

            if (!Directory.Exists(folder))
            {
                try
                {
                    Directory.CreateDirectory(folder);
                    if (Settings.Default.AllowTrayTooltipWarning)
                        notifyIcon1.ShowBalloonTip(1000, "Folder Created", "Selected folder " + folder + " did not already exist.", ToolTipIcon.Info);
                }
                catch
                {
                    writeMessage("Couldn't find or create folder " + folder);
                    if (Settings.Default.AllowTrayTooltipWarning)
                        notifyIcon1.ShowBalloonTip(1000, "Capture error", "Couldn't find or create folder." + folder, ToolTipIcon.Warning);
                    return false;
                }
            }

            if (Directory.Exists(folder))
            {
                try
                {
                    if (format == ImageFormat.Jpeg)
                    {
                        SaveJpeg(folder + "\\" + filename, capture, settings.JpegQuality);
                    }
                    else
                    {
                        capture.Save(folder + "\\" + filename, format);
                        Debug.WriteLine("Saving image with format " + format.ToString());
                    }
                    writeMessage("Saved " + folder + "\\" + filename);
                    if (Settings.Default.AllowTrayTooltipInfo)
                        notifyIcon1.ShowBalloonTip(1000, "Capture saved", folder + Environment.NewLine + filename, ToolTipIcon.Info);
                    lastSavedFile = folder + "\\" + filename;
                    lastFolder = folder;
                }
                catch (Exception ex)
                {
                    writeMessage("Could not save file to:\n"
                        + folder + "\\" + filename + "\n"
                        + "Check that you have write permission for this folder\n"
                        + "\n"
                        + ex.ToString());
                    if (Settings.Default.AllowTrayTooltipWarning)
                        notifyIcon1.ShowBalloonTip(1000, "Capture error", "Couldn't save to folder." + folder + "\nCheck permission for this folder\n", ToolTipIcon.Warning);
                    return false;
                }
            }
            else
            {
                //this shouldn't be reachable
                writeMessage("Folder not found: " + folder);
                if (Settings.Default.AllowTrayTooltipWarning)
                    notifyIcon1.ShowBalloonTip(1000, "Capture error", "Folder not found: " + folder, ToolTipIcon.Warning);
                return false;
            }
            return true;
        }

        //https://stackoverflow.com/questions/1484759/quality-of-a-saved-jpg-in-c-sharp
        public static void SaveJpeg(string path, Bitmap image, long quality = 95L)
        {
            Debug.WriteLine("Saving JPEG with quality " + quality);
            using (EncoderParameters encoderParameters = new EncoderParameters(1))
            using (EncoderParameter encoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality))
            {
                ImageCodecInfo codecInfo = ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                encoderParameters.Param[0] = encoderParameter;
                image.Save(path, codecInfo, encoderParameters);
            }
        }

        public Bitmap CaptureBitmap(int x, int y, int width, int height)
        {
            Bitmap captureBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);

            //Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
            Rectangle captureRectangle = new Rectangle(x, y, width, height);

            //Copying Image from The Screen
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
            captureGraphics.Dispose();
            return captureBitmap;
        }

        //https://stackoverflow.com/questions/309485/c-sharp-sanitize-file-name
        private static string MakeValidFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }

        #endregion

        #region active window location
        // https://stackoverflow.com/questions/5878963/getting-active-window-coordinates-and-height-width-in-c-sharp

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private IntPtr GetActiveWindow()
        {
            IntPtr handle = IntPtr.Zero;
            Debug.WriteLine("ForeGround Window:" + GetForegroundWindow());
            return GetForegroundWindow();
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner  
            public int Top;         // y position of upper-left corner  
            public int Right;       // x position of lower-right corner  
            public int Bottom;      // y position of lower-right corner  
            public int Width
            {
                get
                {
                    return Right - Left;
                }
            }
            public int Height
            {
                get
                {
                    return Bottom - Top;
                }
            }
        }

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return string.Empty;
        }
        #endregion

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            textBoxLog.Text = "Default filename values:" + Environment.NewLine +
                "$w - $d $t $ms ($c)" + Environment.NewLine +
                "$w: Active Window Title" + Environment.NewLine +
                "$d/t/ms: Date, Time, Milliseconds" + Environment.NewLine +
                "$c: Counter number (auto increments)";
        }

        public string BrowseFolderInExplorer(string folder)
        {
            if (folder.Length < 1)
            {
                folder = ".";
            }
            if (Directory.Exists(folder))
            {
                Debug.WriteLine("Opening folder: " + folder);
                Process.Start(new ProcessStartInfo() { FileName = folder, UseShellExecute = true });
            }
            else
            {
                writeMessage("Can't open folder " + folder);
            }

            return folder;
        }

        private void buttonOpenLastFolder_Click(object sender, EventArgs e)
        {
            BrowseFolderInExplorer(lastFolder);
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            OpenOptions();
        }

        private void OpenOptions()
        {
            if (options == null || options.IsDisposed)
                options = new Options(this);
            options.Show();
            options.WindowState = FormWindowState.Normal;
            SetForegroundWindow(options.Handle);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedImageExternal();
        }

        private void OpenSelectedImageExternal()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item != null)
                {
                    string itemFile = item.Tag.ToString() + "";
                    //MessageBox.Show(item.Tag.ToString());
                    if (itemFile.Length > 0)
                    {
                        OpenFileExternal(itemFile);
                    }
                }
            }
        }

        private void OpenFileExternal(string file)
        {
            if (File.Exists(file))
                Process.Start(new ProcessStartInfo() { FileName = file, UseShellExecute = true });
        }


        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    Debug.WriteLine("deleting " + listView1.SelectedItems[0].Text);
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        try
                        {
                            string deleteFile = item.Tag.ToString() + "";
                            if (File.Exists(deleteFile))
                            {
                                File.Delete(deleteFile);
                            }
                            item.Remove();
                        }
                        catch
                        {
                            Debug.WriteLine("No file to delete: " + item.Tag.ToString());
                        }
                    }

                }
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                OpenSelectedImageExternal();
                e.Handled = true;
            }
            else
            {
                //MessageBox.Show("pressed" + e.KeyCode.ToString());
                e.Handled = false;
            }

        }

        private void buttonClearList_Click(object sender, EventArgs e)
        {

            foreach (Image img in imageList.Images)
            {
                if (img != null)
                {
                    img.Dispose();
                }
            }
            imageList.Images.Clear();
            listView1.Clear();
        }

        public void OpenHelp()
        {
            if (helpWindow == null)
            {
                helpWindow = new TextWindow(this, helpText);
            }
            if (helpWindow.IsDisposed)
            {
                helpWindow = new TextWindow(this, helpText);
            }
            helpWindow.Show();
            helpWindow.WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowApplication();
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void timerHide_Tick(object sender, EventArgs e)
        {
            Hide();
            timerHide.Stop();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                if (Settings.Default.StartHidden)
                {
                    HideApplication();
                }
            }
        }

        private void openProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowApplication();
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileExternal(lastSavedFile);
        }

        private void enableCroppingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.TrimChecked = !settings.TrimChecked;
            settings.Save();
            updateTrimStatus();
        }

        public void updateTrimStatus()
        {
            if (settings.TrimChecked)
            {
                enableCroppingToolStripMenuItem.Text = "Disable &Cropping";
            }
            else
            {
                enableCroppingToolStripMenuItem.Text = "Enable &Cropping";
            }
            if (options != null)
            {
                if (!options.IsDisposed)
                {
                    options.updateTrimCheck();
                }
            }
        }
    }
}