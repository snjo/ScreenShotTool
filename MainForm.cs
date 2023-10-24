using Hotkeys;
using ScreenShotTool.Properties;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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

        public string helpText =
            "Default filename values:\r" +
            "\n$w $c\r\n\r" +
            "\n$w: Active Window Title\r" +
            "\n$d/t/ms: Date, Time, Milliseconds\r" +
            "\n$c: Counter number (auto increments)";

        public bool showThumbnails = true;
        Bitmap? bitmap;
        ImageList imageList = new ImageList();

        public Dictionary<string, Hotkey> hotkeyList = new Dictionary<string, Hotkey>
        {
            {"CaptureWindow", new Hotkey(new GlobalHotkey())},
            //{"CaptureRegion", new Hotkey(new GlobalHotkey())},
            {"BrowseFolder", new Hotkey(new GlobalHotkey())},
        };

        #region form open and close
        public MainForm()
        {
            InitializeComponent();

            hotkeyList = HotkeyTools.LoadHotkeys(hotkeyList, this);

            if (settings.RegisterHotkeys)
            {
                HotkeyTools.RegisterHotkeys(hotkeyList);
            }
            //LoadSettings();
            //if (settings.StartHidden) Hide();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            imageList.ImageSize = new Size(100, 100);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            listView1.LargeImageList = imageList;
            if (Settings.Default.StartHidden)
            {
                //WindowState = FormWindowState.Minimized;
                //this.Hide();
                timerHide.Start();
                Debug.WriteLine("Hiding application");
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                Debug.WriteLine("Starting in non-hidden state");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HotkeyTools.ReleaseHotkeys(hotkeyList);
            //SaveSettings();
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
                //MessageBox.Show("Hotkey " + id + " has been pressed!");
                HandleHotkey(id);
            }
        }

        private void HandleHotkey(int id)
        {
            if (!hotkeyList.ContainsKey("CaptureWindow") || !hotkeyList.ContainsKey("BrowseFolder")) return;

            if (id == hotkeyList["CaptureWindow"].ghk.id)
            {
                CaptureAction(CaptureMode.Window);
            }
            else if (id == hotkeyList["BrowseFolder"].ghk.id)
            {
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
            Image thumbImg = ResizeImage(bitmap, width, width, Settings.Default.CropThumbnails);
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
        public static Bitmap ResizeImage(Image image, int width, int height, bool crop=false)
        {
            //https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
            Debug.WriteLine("Resizing image from: " + image.Width + "x" + image.Height + " to " + width + "x" + height);
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            Image cropped;
            if (crop)
            {
                cropped = cropImageSquare(image);
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

        private static Image cropImageSquare(Image img)
        {
            int width = img.Width;
            int height = img.Height;
            Rectangle cropArea;
            if (width == height)
            {
                return img;
            }
            else if (width>height)
            {
                int overflow = img.Width - img.Height;
                int overflowLeft = overflow / 2;
                int overflowRight = overflow - overflowLeft;
                cropArea = new Rectangle(overflowLeft, 0, width-overflow, height);
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
            bitmap = CaptureBitmap(windowRect.Left, windowRect.Top, windowRect.Width, windowRect.Height);
            counter++;
            SaveBitmap(folder, filename, format, bitmap);
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
                }
                catch
                {
                    writeMessage("Couldn't find or create folder " + folder);
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
                    return false;
                }
            }
            else
            {
                writeMessage("Folder not found: " + folder);
                return false;
            }
            return true;
        }

        //https://stackoverflow.com/questions/1484759/quality-of-a-saved-jpg-in-c-sharp
        public static void SaveJpeg(string path, Bitmap image, long quality=95L)
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
            Debug.WriteLine("Handle:" + handle);
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

        private string BrowseFolderInExplorer(string folder)
        {
            if (folder.Length < 1)
            {
                folder = ".";
            }
            if (Directory.Exists(folder))
            {
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
            Options options = new Options(this);
            options.Show();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            OpenImageExternal();
        }

        private void OpenImageExternal()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                if (item != null)
                {
                    string itemFile = item.Tag.ToString()+"";
                    //MessageBox.Show(item.Tag.ToString());
                    if (itemFile.Length>0)
                    {
                        if (File.Exists(itemFile))
                            Process.Start(new ProcessStartInfo() { FileName = itemFile, UseShellExecute = true });
                    }
                }
            }
        }


        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    //MessageBox.Show("deleting " + listView1.SelectedItems[0].Text);
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
                OpenImageExternal();
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
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            BringToFront();
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
    }
}