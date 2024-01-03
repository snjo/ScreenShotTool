using Hotkeys;
using ScreenShotTool.Forms;
using ScreenShotTool.Properties;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

[assembly: AssemblyVersion("1.1.*")]

#pragma warning disable IDE0090 // Use 'new(...)'

namespace ScreenShotTool
{
    [SupportedOSPlatform("windows")]
    public partial class MainForm : Form
    {
        readonly Settings settings = Settings.Default;
        HelpForm? helpWindow;
        ImageFormat DestinationFormat = ImageFormat.Jpeg;
        //int counter = 0;
        string lastFolder = ".";
        string lastSavedFile = "";
        Options? options;
        bool showLog = false;
        private int Counter = 0;
        private readonly int CounterMax = 9999;

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
            "CaptureRegion",
            "CaptureWindow",
            "CaptureCurrentScreen",
            "CaptureAllScreens",
            "BrowseFolder",
        };

        #region form open and close
        public MainForm()
        {
            InitializeComponent();
            UpgradeSettings();
            LoadHotkeysFromSettings();

            UpdateTrimStatus();

            if (Settings.Default.StartHidden)
            {
                HideApplication();
            }
            else
            {
                ShowApplication();
            }

            UpdateLogVisible();

            SetInfoText();
        }

        private static void UpgradeSettings()
        {
            if (Settings.Default.UpgradeSettings)
            {
                Debug.WriteLine("Upgrading settings");
                Settings.Default.Upgrade();
                Settings.Default.UpgradeSettings = false;
            }
            else
            {
                Debug.WriteLine("Not upgrading settings");
            }
        }

        public void LoadHotkeysFromSettings()
        {
            HotkeyList = HotkeyTools.LoadHotkeys(HotkeyList, HotkeyNames, this);

            if (settings.RegisterHotkeys)
            {
                string[] warningKeys = HotkeyTools.RegisterHotkeys(HotkeyList);

                if (warningKeys.Length > 0)
                {
                    string warningText = "";
                    if (warningKeys.Length > 0)
                    {
                        foreach (string key in warningKeys)
                        {
                            warningText += Environment.NewLine + key;
                        }
                    }
                    ShowBalloonToolTip("Could not register hotkeys", warningText, ToolTipIcon.Warning, BalloonTipType.HotkeyError);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (settings.ThumbnailWidth > 256) settings.ThumbnailWidth = 256;
            if (settings.ThumbnailHeight > 256) settings.ThumbnailHeight = 256;
            imageList.ImageSize = new Size(settings.ThumbnailWidth, settings.ThumbnailHeight);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            listViewThumbnails.LargeImageList = imageList;
            SetCounter(settings.Counter);
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
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.
                HandleHotkey(id);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            }
        }

        private void HandleHotkey(int id)
        {
            //if (!HotkeyList.ContainsKey("CaptureWindow") || !HotkeyList.ContainsKey("BrowseFolder"))
            //{
            //    writeMessage("hotkey invalid: " + id);
            //    return;
            //}

            if (id == HotkeyList["CaptureWindow"].ghk.id)
            {
                CaptureAction(CaptureMode.Window);
            }
            if (id == HotkeyList["CaptureRegion"].ghk.id)
            {
                CaptureAction(CaptureMode.Region);
            }
            if (id == HotkeyList["CaptureCurrentScreen"].ghk.id)
            {
                CaptureAction(CaptureMode.SingleScreen);
            }
            if (id == HotkeyList["CaptureAllScreens"].ghk.id)
            {
                CaptureAction(CaptureMode.AllScreens);
            }
            else if (id == HotkeyList["BrowseFolder"].ghk.id)
            {
                BrowseFolderInExplorer(lastFolder);
            }
        }

        #endregion

        #region capture
        private void WriteMessage(string text)
        {
            textBoxLog.Text = text + Environment.NewLine + textBoxLog.Text;
        }

        private void ButtonScreenshot_Click(object sender, EventArgs e)
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
            string DestinationFolder = ComposeFileName(settings.Foldername);
            string DestinationFileName = ComposeFileName(settings.Filename);
            string DestinationFileExtension = settings.FileExtension;
            bool savedToFile = false;
            SetImageFormat();

            if (mode == CaptureMode.Window)
            {
                savedToFile = CaptureWindow(DestinationFolder, DestinationFileName + DestinationFileExtension, DestinationFormat);
            }
            else if (mode == CaptureMode.SingleScreen)
            {
                DestinationFolder = ComposeFileName(settings.Foldername, "Screen");
                DestinationFileName = ComposeFileName(settings.Filename, "Screen");
                savedToFile = CaptureSingleScreen(DestinationFolder, DestinationFileName + DestinationFileExtension, DestinationFormat);
            }
            else if (mode == CaptureMode.AllScreens)
            {
                DestinationFolder = ComposeFileName(settings.Foldername, "Screen");
                DestinationFileName = ComposeFileName(settings.Filename, "Screen");
                savedToFile = CaptureAllScreens(DestinationFolder, DestinationFileName + DestinationFileExtension, DestinationFormat);
            }
            else if (mode == CaptureMode.Region)
            {
                DestinationFolder = ComposeFileName(settings.Foldername, "Region");
                DestinationFileName = ComposeFileName(settings.Filename, "Region");
                savedToFile = CaptureRegion(DestinationFolder, DestinationFileName + DestinationFileExtension, DestinationFormat);
            }

            if (settings.Filename.Contains("$c") && savedToFile)
            {
                IncrementCounter();
            }

            if (showThumbnails && savedToFile)
            {
                AddThumbnail(DestinationFileName + DestinationFileExtension);
                UpdateInfoLabelVisibility();
            }

            bitmap?.Dispose();
        }

        private void UpdateInfoLabelVisibility()
        {
            if (listViewThumbnails.Items.Count > 0)
            {
                labelInfo.Visible = false;
            }
            else
            {
                labelInfo.Visible = true;
            }
        }

        public void SetInfoText()
        {
            labelInfo.Text = "To take a screenshot press:\n";
            //List<string> hotkeyLines = new List<string>();
            foreach (KeyValuePair<string, Hotkey> entry in HotkeyList)
            {
                //hotkeyLines.Add(entry.Key + ": " + entry.Value.ToString());
                string keyName = CamelCaseToSpaces(entry.Key).PadRight(25);
                labelInfo.Text += $"{keyName} :  {entry.Value} \n";
            }
            labelInfo.Text += "\nChange hotkeys in Options.";
        }

        public static string CamelCaseToSpaces(string text)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && i > 0)
                {
                    sb.Append(' ');
                }
                sb.Append(text[i]);
            }
            return sb.ToString();
        }

        public void SetCounter(int num, bool saveSetting = true)
        {
            if (num > CounterMax)
            {
                num = 1;
            }
            Counter = num;

            if (saveSetting)
            {
                settings.Counter = Counter;
                settings.Save();
            }
        }

        public int GetCounter()
        {
            return Counter;
        }

        private void IncrementCounter()
        {
            SetCounter(Counter + 1);
        }

        //private void numericUpDownCounter_ValueChanged(object sender, EventArgs e)
        //{
        //    settings.Counter = (int)numericUpDownCounter.Value;
        //    settings.Save();
        //}

        public string ComposeFileName(string text, string overrideTitle = "")
        {
            string splitTitleString = settings.SplitTitleString;
            int titleMaxLength = settings.TitleMaxLength;
            int splitTitleIndex = settings.SplitTitleIndex;
            string alternateTitle = settings.AlternateTitle;
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string fullDateISO = $"{year}-{month}-{day}";

            string hour = DateTime.Now.Hour.ToString();
            string minute = DateTime.Now.Minute.ToString();
            string second = DateTime.Now.Second.ToString();
            string millisecond = DateTime.Now.Millisecond.ToString();
            string time = $"{hour}-{minute}-{second}";

            string counter = Counter.ToString().PadLeft(3, '0');

            string user = System.Environment.UserName;
            string domain = System.Environment.UserDomainName;
            string hostname = System.Environment.MachineName;

            string windowTitle;
            if (overrideTitle.Length > 0)
            {
                windowTitle = overrideTitle;
            }
            else
            {
                windowTitle = MakeValidFileName(GetActiveWindowTitle());
                windowTitle = ShortenString(windowTitle, titleMaxLength);
            }
            if (splitTitleString.Length > 0)
            {
                string[] titleSplit = windowTitle.Split(splitTitleString);
                int splitIndex = Math.Min(splitTitleIndex, titleSplit.Length - 1);
                if (titleSplit.Length > 1) windowTitle = titleSplit[splitIndex];
            }
            if (windowTitle.Length == 0) { windowTitle = alternateTitle; }

            // short replacement strings
            text = text.Replace("$d", fullDateISO);
            text = text.Replace("$t", time);
            text = text.Replace("$ms", millisecond);
            text = text.Replace("$w", windowTitle);
            text = text.Replace("$c", counter);
            // incrementing the counter happens in CaptureAction if the file is actually saved

            // full set of replacement string used by Greenshot
            /*
                ${YYYY} year, 4 digits
                ${MM} month, 2 digits
                ${DD} day, 2 digits
                ${hh} hour, 2 digits
                ${mm} minute, 2 digits
                ${ss} second, 2 digits
                ${NUM} incrementing number, 6 digits
                ${title} Window title
                ${user} Windows user
                ${domain} Windows domain
                ${hostname} PC name
             */

            if (text.Contains("${")) // do Greenshot style text replacements
            {
                text = text.Replace("${DATE}", fullDateISO);
                text = text.Replace("${YYYY}", year);
                text = text.Replace("${MM}", month);
                text = text.Replace("${DD}", day);
                text = text.Replace("${hh}", hour);
                text = text.Replace("${mm}", minute);
                text = text.Replace("${ss}", second);
                text = text.Replace("${ms}", millisecond);
                text = text.Replace("${NUM}", counter);
                text = text.Replace("${title}", windowTitle);
                text = text.Replace("${user}", user);
                text = text.Replace("${domain}", domain);
                text = text.Replace("${hostname}", hostname);
            }

            return text;
        }

        private void AddThumbnail(string DestinationFileName)
        {
            Debug.WriteLine("Add thumbnail: " + DestinationFileName);
            if (bitmap == null) return;
            try
            {
                Debug.WriteLine("AddThumbnail, bitmap: " + bitmap.Size);
            }
            catch
            {
                Debug.WriteLine("AddThumbnail, bitmap error");
                return;
            }


            //int width = imageList.ImageSize.Width;

            Image thumbImg = ResizeImage(bitmap, settings.ThumbnailWidth, settings.ThumbnailHeight, Settings.Default.CropThumbnails);
            imageList.Images.Add(thumbImg);
            thumbImg.Dispose();
            ListViewItem thumb = listViewThumbnails.Items.Add(DestinationFileName);
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
            //Debug.WriteLine("Resizing image from: " + image.Width + "x" + image.Height + " to " + width + "x" + height);
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            Image cropped;
            if (crop)
            {
                //cropped = cropImageSquare(image);
                cropped = CropImageToAspectRatio(image, Settings.Default.ThumbnailWidth, Settings.Default.ThumbnailHeight);
                Debug.WriteLine("Crop to square: " + image.Width + "x" + image.Height + " to" + cropped.Width + "x" + cropped.Height);
            }
            else
            {
                cropped = image;
            }

            destImage.SetResolution(cropped.HorizontalResolution, cropped.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(cropped, destRect, 0, 0, cropped.Width, cropped.Height, GraphicsUnit.Pixel, wrapMode);
            }

            Debug.WriteLine("Resize image returns:" + destImage.Width + "x" + destImage.Height);
            return destImage;
        }

        private static Image CropImageToAspectRatio(Image img, int aspectRatioWidth, int aspectRatioHeight)
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

        /*
        private static Image CropImageSquare(Image img)
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
                //int overflowRight = overflow - overflowLeft;
                cropArea = new Rectangle(overflowLeft, 0, width - overflow, height);
            }
            else
            {
                int overflow = img.Height - img.Width;
                int overflowTop = overflow / 2;
                //int overflowBottom = overflow - overflowTop;
                cropArea = new Rectangle(0, overflowTop, width, height - overflow);
            }
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }*/

        private void SetImageFormat()
        {
            string DestinationFileExtension = settings.FileExtension;
            DestinationFormat = DestinationFileExtension switch
            {
                ".jpg" => ImageFormat.Jpeg,
                ".png" => ImageFormat.Png,
                ".gif" => ImageFormat.Gif,
                ".bmp" => ImageFormat.Bmp,
                ".tiff" => ImageFormat.Tiff,
                _ => ImageFormat.Jpeg,
            };
        }

        private static string ShortenString(string input, int maxLength)
        {
            return input.Substring(0, Math.Min(maxLength, input.Length)).Trim();
        }

        public bool CaptureWindow(string folder, string filename, ImageFormat format)
        {
            bool saved = false;
            GetWindowRect(GetActiveWindow(), out RECT windowRect);
            if (settings.TrimChecked)
            {
                windowRect.Left += settings.TrimLeft;
                windowRect.Right -= settings.TrimRight;
                windowRect.Top += settings.TrimTop;
                windowRect.Bottom -= settings.TrimBottom;
            }
            if (windowRect.Width > 0 && windowRect.Height > 0)
            {
                bitmap?.Dispose();
                bitmap = CaptureBitmap(windowRect.Left, windowRect.Top, windowRect.Width, windowRect.Height);
                if (settings.WindowToFile)
                {
                    saved = SaveBitmap(folder, filename, format, bitmap);
                }
                if (settings.WindowToClipboard)
                {
                    Clipboard.SetImage(bitmap);
                }
                return saved;
            }
            else
            {
                Debug.WriteLine("Capture size is less than zero. Capture aborted.");
                ShowBalloonToolTip("Capture error", "Capture size is less than zero. Capture aborted.", ToolTipIcon.Warning, BalloonTipType.ScreenshotError);
                return false;
            }
        }

        public bool CaptureRegion(string folder, string filename, ImageFormat format)
        {
            bool saved = false;
            Screen screen = Screen.FromPoint(Cursor.Position);
            Bitmap bmp = GetScreenImage(screen);
            ImageView imgView = new ImageView(true, screen, bmp);

            imgView.Location = new Point(screen.Bounds.X, screen.Bounds.Y);
            imgView.SetImage();
            imgView.CompleteCaptureOnMoureRelease = settings.RegionCompletesOnMouseRelease;
            imgView.SaveToFile = settings.RegionToFile;
            imgView.SendToEditor = settings.RegionToEditor;
            imgView.SendToClipboard = settings.RegionToClipboard;
            DialogResult result = imgView.ShowDialog();
            if (result == DialogResult.Yes) // Yes means save to file
            {
                Bitmap? bmpResult = imgView.GetBitmap();
                if (bmpResult != null)
                {
                    //save to file or copy to clipboard is handled inside the viewer
                    saved = SaveBitmap(folder, filename, format, bmpResult);

                    bitmap?.Dispose();
                    bitmap = bmpResult;
                }
                else
                {
                    Debug.WriteLine("ImageView result image was null");
                    saved = false;
                }
            }
            return saved;
        }

        public bool CaptureSingleScreen(string folder, string filename, ImageFormat format)
        {
            bool saved = false;
            Screen screen = Screen.FromPoint(Cursor.Position);
            bitmap?.Dispose();
            bitmap = GetScreenImage(screen);

            if (settings.ScreenToFile)
            {
                saved = SaveBitmap(folder, filename, format, bitmap);
            }
            if (settings.ScreenToClipboard)
            {
                Clipboard.SetImage(bitmap);
            }
            return saved;
        }

        public bool CaptureAllScreens(string folder, string filename, ImageFormat format)
        {
            bool saved = false;
            bitmap?.Dispose();
            bitmap = GetAllScreensImage();

            if (settings.ScreenToFile)
            {
                saved = SaveBitmap(folder, filename, format, bitmap);
            }
            if (settings.ScreenToClipboard)
            {
                Clipboard.SetImage(bitmap);
            }
            return saved;
        }


        private Bitmap GetAllScreensImage()
        {
            Rectangle screenBound = SystemInformation.VirtualScreen;
            if (screenBound.Width > 0 && screenBound.Height > 0)
            {
                return CaptureBitmap(screenBound.Left, screenBound.Top, screenBound.Width, screenBound.Height);
            }
            else
            {
                Debug.WriteLine("Screen bounds size is less than zero. Capture aborted.");
                ShowBalloonToolTip("Capture error", "Screen bounds  size is less than zero. Capture aborted.", ToolTipIcon.Warning, BalloonTipType.ScreenshotError);
            }
            return new Bitmap(0, 0);
        }

        private Bitmap GetScreenImage(Screen screen)
        {
            Rectangle screenBound = screen.Bounds;//SystemInformation.VirtualScreen;
            if (screenBound.Width > 0 && screenBound.Height > 0)
            {
                return CaptureBitmap(screenBound.Left, screenBound.Top, screenBound.Width, screenBound.Height);
            }
            else
            {
                Debug.WriteLine("Screen bounds size is less than zero. Capture aborted.");
                ShowBalloonToolTip("Capture error", "Screen bounds  size is less than zero. Capture aborted.", ToolTipIcon.Warning, BalloonTipType.ScreenshotError);
            }

            return new Bitmap(0, 0);
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

                    ShowBalloonToolTip("Folder Created", "Selected folder " + folder + " did not already exist.", ToolTipIcon.Info, BalloonTipType.FolderCreated);

                }
                catch
                {
                    WriteMessage("Couldn't find or create folder " + folder);

                    ShowBalloonToolTip("Capture error", "Couldn't find or create folder." + folder, ToolTipIcon.Warning, BalloonTipType.FolderError);

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
                        Debug.WriteLine("Saving image with format " + format.ToString() + " to " + folder + "\\" + filename);
                        capture.Save(folder + "\\" + filename, format);
                    }
                    WriteMessage("Saved " + folder + "\\" + filename);

                    ShowBalloonToolTip("Capture saved", folder + Environment.NewLine + filename, ToolTipIcon.Info, BalloonTipType.ScreenshotSaved);

                    lastSavedFile = folder + "\\" + filename;
                    lastFolder = folder;
                }
                catch (Exception ex)
                {
                    WriteMessage("Could not save file to:\n"
                        + folder + "\\" + filename + "\n"
                        + "Check that you have write permission for this folder\n"
                        + "\n"
                        + ex.Message);

                    ShowBalloonToolTip("Capture error", "Couldn't save to folder." + folder + "\nCheck permission for this folder\n", ToolTipIcon.Warning, BalloonTipType.FolderError);

                    return false;
                }
            }
            else
            {
                //this shouldn't be reachable
                WriteMessage("Folder not found: " + folder);

                ShowBalloonToolTip("Capture error", "Folder not found: " + folder, ToolTipIcon.Warning, BalloonTipType.FolderError);

                return false;
            }
            return true;
        }

        //https://stackoverflow.com/questions/1484759/quality-of-a-saved-jpg-in-c-sharp
        public static void SaveJpeg(string path, Bitmap image, long quality = 95L)
        {
            Debug.WriteLine("Saving JPEG with quality " + quality);
            using EncoderParameters encoderParameters = new EncoderParameters(1);
            using EncoderParameter encoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ImageCodecInfo codecInfo = ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
            encoderParameters.Param[0] = encoderParameter;
            image.Save(path, codecInfo, encoderParameters);
        }

        public static Bitmap CaptureBitmap(int x, int y, int width, int height)
        {
            Bitmap captureBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);

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

        private static IntPtr GetActiveWindow()
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
                WriteMessage("Can't open folder " + folder);
            }

            return folder;
        }

        public void OpenHelp()
        {
            if (helpWindow == null)
            {
                helpWindow = new HelpForm();
            }
            if (helpWindow.IsDisposed)
            {
                helpWindow = new HelpForm();
            }
            helpWindow.Show();
            helpWindow.WindowState = FormWindowState.Normal;
        }

        private void OpenOptions()
        {
            if (options == null || options.IsDisposed)
                options = new Options(this);
            options.Show();
            options.WindowState = FormWindowState.Normal;
            SetForegroundWindow(options.Handle);
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedImageExternal();
        }

        private void OpenSelectedImageExternal()
        {
            if (listViewThumbnails.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewThumbnails.SelectedItems[0];
                if (item != null)
                {
                    string itemFile = item.Tag.ToString() + "";
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
            {
                Process.Start(new ProcessStartInfo() { FileName = file, UseShellExecute = true });
            }
            else
            {
                WriteMessage("Can't open file, it no longer exists: " + file);
            }
        }

        #region click events --------------------------------------------------------
        private void ButtonOpenLastFolder_Click(object sender, EventArgs e)
        {
            BrowseFolderInExplorer(lastFolder);
        }

        private void ButtonOptions_Click(object sender, EventArgs e)
        {
            OpenOptions();
        }


        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedFiles();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                OpenSelectedImageExternal();
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }

        private void DeleteSelectedFiles()
        {
            if (listViewThumbnails.SelectedItems.Count > 0)
            {
                Debug.WriteLine("deleting " + listViewThumbnails.SelectedItems[0].Text);
                foreach (ListViewItem item in listViewThumbnails.SelectedItems)
                {
                    
                        string deleteFile = item.Tag.ToString() + "";
                        if (File.Exists(deleteFile))
                        {
                            try
                            {
                                File.Delete(deleteFile);
                                item.Remove();
                                WriteMessage("Deleted file " + deleteFile);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Couldn't delete file: " + item.Tag.ToString());
                                Debug.WriteLine(ex.ToString());
                            }  
                        }
                        else
                        {
                            WriteMessage("File no longer exists, removing from list: " + deleteFile);
                        }
                        //item.Remove();
                    
                }

            }
            UpdateInfoLabelVisibility();
        }

        private void ButtonClearList_Click(object sender, EventArgs e)
        {

            foreach (Image img in imageList.Images)
            {
                img?.Dispose();
            }
            imageList.Images.Clear();
            listViewThumbnails.Clear();
            UpdateInfoLabelVisibility();
        }



        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowApplication();
        }

        private void ButtonHide_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void TimerHide_Tick(object sender, EventArgs e)
        {
            Hide();
            timerHide.Stop();
        }

        private void OpenProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowApplication();
        }

        private void ExitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileExternal(lastSavedFile);
        }

        private void EnableCroppingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.TrimChecked = !settings.TrimChecked;
            settings.Save();
            UpdateTrimStatus();
        }

        #endregion



        public void UpdateTrimStatus()
        {
            string disableCrop = "Disable &Cropping";
            string enableCrop = "Enable &Cropping";
            if (settings.TrimChecked)
            {
                enableCroppingToolStripMenuItem.Text = disableCrop;
                toggleCropToolStripMenuItem.Text = disableCrop;


            }
            else
            {
                enableCroppingToolStripMenuItem.Text = enableCrop;
                toggleCropToolStripMenuItem.Text = enableCrop;
            }
            if (options != null)
            {
                if (!options.IsDisposed)
                {
                    options.UpdateTrimCheck();
                }
            }
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
            UpdateLogVisible();
        }

        #region Balloon Tip ----------------------------------------------------------------

        private BalloonTipType lastBallonTip = BalloonTipType.NotSet;

        private void ShowBalloonToolTip(string title, string text, ToolTipIcon icon, BalloonTipType tipType)
        {

            int timeout = 1000;


            bool showToolTip = false;

            if (tipType == BalloonTipType.ScreenshotSaved && settings.AllowTrayTooltipInfoCapture)
                showToolTip = true;
            else if (tipType == BalloonTipType.FolderCreated && settings.AllowTrayTooltipInfoFolder)
                showToolTip = true;
            else if (tipType >= BalloonTipType.Error && settings.AllowTrayTooltipWarning)
                showToolTip = true;

            if (showToolTip)
            {
                Debug.WriteLine("Showing Balloon tip: " + tipType.ToString());
                lastBallonTip = tipType;
                notifyIcon1.ShowBalloonTip(timeout, title, text, icon);
            }
            else
            {
                Debug.WriteLine("Supressing Balloon tip: " + tipType.ToString());
            }
        }

        private enum BalloonTipType
        {
            NotSet,
            ScreenshotSaved,
            FolderCreated,
            Error, // only errors below this enum
            FolderError,
            ScreenshotError,
            HotkeyError
        }

        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Balloon clicked, last type: " + lastBallonTip.ToString());
            if (lastBallonTip == BalloonTipType.FolderCreated)
            {
                Debug.WriteLine("Balloon click, Open last image: " + lastFolder);
                BrowseFolderInExplorer(lastFolder);
            }
            if (lastBallonTip == BalloonTipType.ScreenshotSaved)
            {
                Debug.WriteLine("Balloon click, Open last image: " + lastSavedFile);
                OpenFileExternal(lastSavedFile);
            }
            if (lastBallonTip >= BalloonTipType.Error)
            {
                Debug.WriteLine("Balloon click, Error, open Options");
                OpenOptions();
            }
        }

        #endregion

        private void Label1_Click(object sender, EventArgs e)
        {
            showLog = !showLog;
            UpdateLogVisible();
        }

        private void UpdateLogVisible()
        {
            int logHeight = 100;
            int logMargin = 10;
            int internalWidth = this.ClientSize.Width;
            int internalCenter = internalWidth / 2;
            int internalHeight = this.ClientSize.Height;

            if (showLog)
            {
                labelShowLog.Text = "Hide log";
                labelShowLog.Location = new Point((internalCenter) - (labelShowLog.Width / 2), internalHeight - logHeight - labelShowLog.Height - 5);
                textBoxLog.Visible = true;
                textBoxLog.Location = new Point(logMargin, internalHeight - logHeight);
                textBoxLog.Size = new Size(internalWidth - (logMargin * 2), logHeight - 5);
                listViewThumbnails.Size = new Size(internalWidth - (logMargin * 2), internalHeight - logHeight - labelShowLog.Height - 40);
            }
            else
            {
                labelShowLog.Text = "Show log";
                labelShowLog.Location = new Point((internalCenter) - (labelShowLog.Width / 2), internalHeight - labelShowLog.Height - 5);
                textBoxLog.Visible = false;
                listViewThumbnails.Size = new Size(internalWidth - (logMargin * 2), internalHeight - labelShowLog.Height - 40);
            }
        }

        private void ItemOpenImage_Click(object sender, EventArgs e)
        {
            // open first selected image in external viewer
            if (listViewThumbnails.SelectedItems.Count > 0)
            {
                OpenFileExternal(listViewThumbnails.SelectedItems[0].Tag.ToString() + "");
            }
        }

        private void ItemRemove_Click(object sender, EventArgs e)
        {
            // remove all selected items from list, without deleting the file
            foreach (ListViewItem item in listViewThumbnails.SelectedItems)
            {
                item.Remove();
            }
            UpdateInfoLabelVisibility();
        }

        private void ItemDeleteFile_Click(object sender, EventArgs e)
        {
            // delete selected files
            DeleteSelectedFiles();
        }

        private void ItemOpenFolder_Click(object sender, EventArgs e)
        {
            // open folder of first selected file
            if (listViewThumbnails.SelectedItems.Count > 0)
            {
                string? file = listViewThumbnails.SelectedItems[0].Tag.ToString();
                if (file == null) return;
                string? folder = Path.GetDirectoryName(file);
                if (folder == null) return;
                BrowseFolderInExplorer(folder);
            }
        }

        private void CopyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewThumbnails.SelectedItems.Count > 0)
            {
                string? file = listViewThumbnails.SelectedItems[0].Tag.ToString();
                if (file == null) return;
                if (File.Exists(file))
                {
                    try
                    {
                        Image toClipboard = Image.FromFile(file);
                        Clipboard.SetImage(toClipboard);
                        toClipboard.Dispose();
                    }
                    catch
                    {
                        WriteMessage("Copy to clipboard failed, error while opening file " + file);
                    }
                }
                else
                {
                    WriteMessage("Copy to clipboard failed, file no longer exists " + file);
                }
            }
        }

        private void ListViewThumbnails_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = listViewThumbnails.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuListView.Show(Cursor.Position);
                }
            }
        }

        private void ResetCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetCounter(1);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HelpofflineCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHelp();
        }

        private void HelponGithubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLink("https://github.com/snjo/ScreenShotTool/blob/master/README.md");
        }

        private void WebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLink("https://github.com/snjo/ScreenShotTool/");
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenAbout();
        }

        public static void OpenLink(string url)
        {
            Process.Start(new ProcessStartInfo() { FileName = url, UseShellExecute = true });
        }

        About aboutWindow = new About();
        public void OpenAbout()
        {
            if (aboutWindow == null)
            {
                aboutWindow = new About();
            }
            if (aboutWindow.IsDisposed)
            {
                aboutWindow = new About();
            }
            aboutWindow.Show();
            aboutWindow.WindowState = FormWindowState.Normal;
        }

        private void CopyFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewThumbnails.SelectedItems.Count > 0)
            {
                Debug.WriteLine("Copying files to clipboard");

                List<string> fileList = new List<string>();

                foreach (ListViewItem item in listViewThumbnails.SelectedItems)
                {

                    string file = item.Tag.ToString() + "";
                    if (File.Exists(file))
                    {
                        fileList.Add(file);
                    }
                    else
                    {
                        WriteMessage("File no longer exists, can't copy: " + file);
                    }
                }
                Clipboard.Clear();
                Clipboard.SetData(DataFormats.FileDrop, fileList.ToArray());
            }
        }

        private void EditImageFromFile_Click(object sender, EventArgs e)
        {
            if (listViewThumbnails.SelectedItems.Count > 0)
            {
                string? file = listViewThumbnails.SelectedItems[0].Tag.ToString();
                if (File.Exists(file))
                {
                    ScreenshotEditor imageEditor = new ScreenshotEditor(file);
                    imageEditor.Show();
                }
            }
        }

        private void EditImageNoFile_Click(object sender, EventArgs e)
        {
            ScreenshotEditor imageEditor = new ScreenshotEditor();
            imageEditor.Show();
        }

        private void EditImageFromClipboard_Click(object sender, EventArgs e)
        {
            ScreenshotEditor imageEditor = new ScreenshotEditor(true);
            imageEditor.Show();
        }
    }
}