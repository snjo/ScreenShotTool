using Hotkeys;
using ScreenShotTool.Properties;
using System.Drawing.Imaging;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace ScreenShotTool
{
    public partial class MainForm : Form
    {
        Settings settings = Settings.Default;
        string DestinationFolder = "";
        string DestinationFileName = "capture.png";
        //int DestinationFileNumber = 0;
        ImageFormat DestinationFormat;// = ImageFormat.Jpeg;
        //string fileExtension = ".jpg";
        int maxApplicationNameLength = 16;
        int counter = 0;

        public Dictionary<string, Hotkey> hotkeyList = new Dictionary<string, Hotkey>
        {
            {"CaptureWindow", new Hotkey(new GlobalHotkey())},
            {"CaptureRegion", new Hotkey(new GlobalHotkey())},
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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HotkeyTools.ReleaseHotkeys(hotkeyList);
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
                else if (id == hotkeyList["CaptureRegion"].ghk.id)
                {
                    CaptureAction(CaptureMode.Region);
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
            DestinationFolder = textBoxFolder.Text;
            SetImageFormat();
            //time = time.Replace(":", "");
            //DestinationFileName = time + comboBoxFileType.Text;
            if (mode == CaptureMode.Window)
            {
                DestinationFolder = ComposeFileName(textBoxFolder.Text);
                DestinationFileName = ComposeFileName(textBoxFilename.Text);
                CaptureWindow(DestinationFolder, DestinationFileName + comboBoxFileType.Text, DestinationFormat);
                numericUpDownCounter.Value++;
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



        private void SetImageFormat()
        {
            switch (comboBoxFileType.Text)
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
            windowRect.Left += (int)trimLeft.Value;
            windowRect.Right -= (int)trimRight.Value;
            windowRect.Top += (int)trimTop.Value;
            windowRect.Bottom -= (int)trimBottom.Value;
            Bitmap bitmap = CaptureBitmap(windowRect.Left, windowRect.Top, windowRect.Width, windowRect.Height);

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

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            textBoxFolder.Text = dialog.SelectedPath;
        }
    }
}