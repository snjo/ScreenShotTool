using Hotkeys;
using ScreenShotTool.Properties;
using System.Configuration;
using System.Diagnostics;
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
        public string PatternFolder = "";
        public string PatternFileName = "";
        public string PatternFileExtension = "";

        private string DestinationFolder = "";
        private string DestinationFileName = "capture.png";
        private string DestinationFileExtension = ".jpg";
        //int DestinationFileNumber = 0;
        ImageFormat DestinationFormat;// = ImageFormat.Jpeg;
        //string fileExtension = ".jpg";
        int maxApplicationNameLength = 30;
        int counter = 0;
        string lastFolder = ".";
        string lastSavedFile = "";
        public int trimTop = 0;
        public int trimBottom = 0;
        public int trimLeft = 0;
        public int trimRight = 0;
        public bool trim = false;

        public bool showThumbnails = true;
        Bitmap bitmap;
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
            LoadSettings();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            imageList.ImageSize = new Size(100, 100);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            listView1.LargeImageList = imageList;
            //listView1.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HotkeyTools.ReleaseHotkeys(hotkeyList);
            SaveSettings();
        }
        #endregion

        #region Settings
        public void SaveSettings()
        {
            settings.Filename = PatternFileName;
            settings.Foldername = PatternFolder;
            settings.FileExtension = PatternFileExtension;
            settings.TrimTop = trimTop;
            settings.TrimBottom = trimBottom;
            settings.TrimLeft = trimLeft;
            settings.TrimRight = trimRight;
            settings.trimChecked = trim;
            settings.Save();
        }

        private void LoadSettings()
        {
            PatternFileName = settings.Filename;
            PatternFolder = settings.Foldername;
            PatternFileExtension = settings.FileExtension;
            trimTop = settings.TrimTop;
            trimBottom = settings.TrimBottom;
            trimLeft = settings.TrimLeft;
            trimRight = settings.TrimRight;
            trim = settings.trimChecked;
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

            if (hotkeyList["CaptureWindow"] != null)
            {
                if (id == hotkeyList["CaptureWindow"].ghk.id)
                {
                    CaptureAction(CaptureMode.Window);
                }
                else if (id == hotkeyList["BrowseFolder"].ghk.id)
                {
                    BrowseFolderInExplorer(lastFolder);
                }

                /*
                else if (id == hotkeyList["CaptureWindow"].ghk.id)
                {
                    CaptureAction(CaptureMode.Window);
                }
                else if (id == hotkeyList["CaptureAllscreens"].ghk.id)
                {
                    CaptureAction(CaptureMode.AllScreens);
                }
                */
            }
        }

        #endregion

        #region capture
        private void writeMessage(string text)
        {
            //MessageBox.Show(text);
            textBoxLog.Text = text + Environment.NewLine + textBoxLog.Text;
        }

        private void buttonScreenshot_Click(object sender, EventArgs e)
        {
            CaptureAction(CaptureMode.Window);
        }


        /* public bool CaptureScreen()
        {
            try
            {
                //Creating a new Bitmap object
                Bitmap captureBitmap = new Bitmap(1024, 768, PixelFormat.Format32bppArgb);
                //Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);
                //Creating a Rectangle object which will
                //capture our Current Screen
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                //Creating a New Graphics Object
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                //Copying Image from The Screen
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

                //Saving the Image File (I am here Saving it in My E drive).
                //captureBitmap.Save(@"E:\Capture.jpg", ImageFormat.Jpeg);
                captureBitmap.Save(@"E:\Capture.png", ImageFormat.Png);
                captureBitmap.Save(@"E:\Capture.jpg", ImageFormat.Jpeg);

                //Displaying the Successfull Result
                MessageBox.Show("Screen Captured");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }*/


        public enum CaptureMode
        {
            SingleScreen,
            AllScreens,
            Window,
            Region,
        }

        public void CaptureAction(CaptureMode mode)
        {
            DestinationFolder = PatternFolder;
            SetImageFormat();

            if (mode == CaptureMode.Window)
            {
                DestinationFolder = ComposeFileName(PatternFolder);
                DestinationFileName = ComposeFileName(PatternFileName);
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
        }

        private string ComposeFileName(string text)
        {
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
            windowTitle = ShortenString(windowTitle, maxApplicationNameLength);
            if (windowTitle.Length == 0) { windowTitle = "capture"; }
            /*
            string folder = textBoxFolder.Text;
            string file = textBoxFilename.Text;
            string extension = comboBoxFileType.Text;
            string finalname = folder + "\\" + file + extension;
            */


            text = text.Replace("$d", date);
            text = text.Replace("$t", time);
            text = text.Replace("$ms", millisecond);
            text = text.Replace("$w", windowTitle);
            text = text.Replace("$c", numericUpDownCounter.Value.ToString().PadLeft(3, '0'));

            return text;
        }

        private void UpdateThumbnails()
        {
            //listView1.View = View.LargeIcon;
            //Image img = bitmap;
            imageList.Images.Add(getThumbnailImage(imageList.ImageSize.Width, bitmap));
            ListViewItem thumb = listView1.Items.Add(DestinationFileName);
            thumb.Text = DestinationFileName;
            thumb.Tag = lastSavedFile;
            thumb.ImageIndex = imageList.Images.Count - 1;

            //listView1.Items.Add(DestinationFileName, )
        }

        private Image getThumbnailImage(int width, Image img)
        {
            Image thumb = new Bitmap(width, width);
            Image tmp = null;

            if (img.Width < width && img.Height < width)
            {
                using (Graphics g = Graphics.FromImage(thumb))
                {
                    int xoffset = (int)((width - img.Width) / 2);
                    int yoffset = (int)((width - img.Height) / 2);
                    g.DrawImage(img, xoffset, yoffset, img.Width, img.Height);
                }
            }
            else
            {
                Image.GetThumbnailImageAbort myCallback = new
                    Image.GetThumbnailImageAbort(ThumbnailCallback);

                if (img.Width == img.Height)
                {
                    thumb = img.GetThumbnailImage(
                             width, width,
                             myCallback, IntPtr.Zero);
                }
                else
                {
                    int k = 0;
                    int xoffset = 0;
                    int yoffset = 0;

                    if (img.Width < img.Height)
                    {
                        k = (int)(width * img.Width / img.Height);
                        tmp = img.GetThumbnailImage(k, width, myCallback, IntPtr.Zero);
                        xoffset = (int)((width - k) / 2);

                    }

                    if (img.Width > img.Height)
                    {
                        k = (int)(width * img.Height / img.Width);
                        tmp = img.GetThumbnailImage(width, k, myCallback, IntPtr.Zero);
                        yoffset = (int)((width - k) / 2);
                    }

                    using (Graphics g = Graphics.FromImage(thumb))
                    {
                        g.DrawImage(tmp, xoffset, yoffset, tmp.Width, tmp.Height);
                    }
                }
            }

            using (Graphics g = Graphics.FromImage(thumb))
            {
                g.DrawRectangle(Pens.Green, 0, 0, thumb.Width - 1, thumb.Height - 1);
            }

            return thumb;
        }

        public bool ThumbnailCallback()
        {
            return true;
        }

        private void SetImageFormat()
        {
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
            if (trim)
            {
                windowRect.Left += trimLeft;
                windowRect.Right -= trimRight;
                windowRect.Top += trimTop;
                windowRect.Bottom -= trimBottom;
            }
            bitmap = CaptureBitmap(windowRect.Left, windowRect.Top, windowRect.Width, windowRect.Height);

            //textBoxLog.Text = textBoxLog.Text + "Saving " + counter + Environment.NewLine;
            counter++;
            //Task.Run(() => SaveBitmap(folder, filename, format, capture));
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
                    capture.Save(folder + "\\" + filename, format);
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
            //writeMessage("ok");
            return true;
        }

        public Bitmap CaptureBitmap(int x, int y, int width, int height)
        {
            Bitmap captureBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);

            //Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
            Rectangle captureRectangle = new Rectangle(x, y, width, height);

            //Copying Image from The Screen

            //captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
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
            return null;
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

        /*
        private void buttonBrowseFolder_Click(object sender, EventArgs e)
        {
            //string file = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            //string folder = Path.GetDirectoryName(file);
            string folder = textBoxFolder.Text;
            folder = BrowseFolderInExplorer(folder);
        }*/

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
            options.ShowDialog();
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
                    string itemFile = item.Tag.ToString();
                    //MessageBox.Show(item.Tag.ToString());
                    if (File.Exists(itemFile))
                    {
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
                            File.Delete(item.Tag.ToString());
                            item.Remove();
                            //listView1.AutoArrange = true;                            
                        }
                        catch
                        {
                            //MessageBox.Show("No file to delete: " + item.Tag.ToString());
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
            listView1.Clear();
        }
    }
}