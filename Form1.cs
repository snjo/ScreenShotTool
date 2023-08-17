using Hotkeys;
using ScreenShotTool.Properties;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ScreenShotTool
{
    public partial class Form1 : Form
    {
        Settings settings = Settings.Default;
        string DestinationFolder = "";
        string DestinationFileName = "capture.png";
        int DestinationFileNumber = 0;
        ImageFormat DestinationFormat = ImageFormat.Jpeg;
        string fileExtension = ".jpg";

        private bool hotkeysSet = false;

        public Dictionary<string, Hotkey> hotkeyList = new Dictionary<string, Hotkey>
        {
            //{"UpperCase", new GlobalHotkey(new Hotkey())},
            //{"CaptureWindow", new Hotkey(new GlobalHotkey())},
        };

        public Form1()
        {
            InitializeComponent();
            hotkeyList = new Dictionary<string, Hotkey>
            {
                //{"UpperCase", new GlobalHotkey(new Hotkey())},
                {"CaptureWindow", new Hotkey(new GlobalHotkey())},
            };
            LoadHotkeys(this);
            RegisterHotkeys();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseHotkeys();
        }

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


        #region Hotkeys
        public void LoadHotkeys(Form parent)
        {
            //Settings newSettings = new();

            foreach (KeyValuePair<string, Hotkey> kvp in hotkeyList)
            {
                hotkeyList[kvp.Key] = LoadHotkey(kvp.Key, parent);
            }
        }

        private Hotkey LoadHotkey(string hotkeyName, Form parent) //char settingHotkey
        {
            //Settings.Default["hk" + hotkeyName + "Key"]
            Hotkey hotkey = new Hotkey();
            hotkey.key = Settings.Default["hk" + hotkeyName + "Key"].ToString();
            hotkey.Ctrl = (bool)Settings.Default["hk" + hotkeyName + "Ctrl"];
            hotkey.Alt = (bool)Settings.Default["hk" + hotkeyName + "Alt"];
            hotkey.Shift = (bool)Settings.Default["hk" + hotkeyName + "Shift"];
            hotkey.Win = (bool)Settings.Default["hk" + hotkeyName + "Win"];
            hotkey.ghk = new GlobalHotkey(hotkey.Modifiers(), hotkey.key, parent);

            //test
            //writeMessage("key: " + Settings.Default["hk" + hotkeyName + "Key"].ToString());

            return hotkey;
        }

        public void RegisterHotkeys()
        {
            if (!settings.RegisterHotkeys) return;

            hotkeysSet = true;

            string errorMessages = "";
            //trying to register hotkey

            foreach (KeyValuePair<string, Hotkey> ghk in hotkeyList)
            {
                RegisterHotKey(ghk.Value.ghk);
            }

            if (errorMessages.Length > 0)
            {
                //writeMessage(errorMessages);
            }
        }

        private void RegisterHotKey(GlobalHotkey ghk)
        {
            if (ghk != null)
            {
                //writeMessage("hk reg " + ghk.id);
                if (!ghk.Register())
                {
                    //writeMessage("register hotkey failed");
                }
            }
        }

        public void ReleaseHotkeys()
        {
            if (!hotkeysSet) return;

            foreach (KeyValuePair<string, Hotkey> ghk in hotkeyList)
            {
                ReleaseHotkey(ghk.Value.ghk);
            }
        }

        private void ReleaseHotkey(GlobalHotkey ghk)
        {
            if (ghk != null)
            {
                ghk.Unregister();
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
            }
        }


        #endregion


        private void writeMessage(string text)
        {
            MessageBox.Show(text);
        }

        private void buttonScreenshot_Click(object sender, EventArgs e)
        {
            CaptureAction(CaptureMode.Window);
        }

        public bool CaptureScreen()
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
            DestinationFolder = textBoxFolder.Text;
            string time = 
                DateTime.Now.Year.ToString() + "-" +
                DateTime.Now.Month.ToString() + "-" +
                DateTime.Now.Day.ToString() + " " +
                DateTime.Now.Hour.ToString() + "_" +
                DateTime.Now.Minute.ToString() + "_" +
                DateTime.Now.Second.ToString();
            //time = time.Replace(":", "");
            DestinationFileName = "capture " + time + fileExtension;
            if (mode == CaptureMode.Window)
            {
                CaptureWindow(DestinationFolder, DestinationFileName, DestinationFormat);
            }
        }

        public bool CaptureWindow(string folder, string filename, ImageFormat format)
        {
            try
            {
                //Saving the Image File (I am here Saving it in My E drive).
                //captureBitmap.Save(@"E:\Capture.jpg", ImageFormat.Jpeg);

                RECT windowRect;
                GetWindowRect(GetActiveWindow(), out windowRect);

                Bitmap capture = CaptureBitmap(windowRect.Left, windowRect.Top, windowRect.Width, windowRect.Height);

                if (folder.Length < 1)
                {
                    folder = ".";
                }

                if (Directory.Exists(folder))
                {
                    capture.Save(folder + "\\" + filename, format);
                    //writeMessage("Saved to: " + folder + "\\" + filename);
                }
                else
                {
                    writeMessage("Folder not found: " + folder);
                }

                //Displaying the Successfull Result
                //MessageBox.Show("Screen Captured");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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



        #region active window  location
        // https://stackoverflow.com/questions/5878963/getting-active-window-coordinates-and-height-width-in-c-sharp

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();


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
        #endregion

        #region alternate active window rect
        /*
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        public Rectangle GetWindowRect()
        {
            SetProcessDPIAware();
            Rectangle t2;
            GetWindowRect(GetForegroundWindow(), out t2);
            return t2;
        }
        */
        #endregion

    }
}