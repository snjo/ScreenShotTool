using System.Runtime.Versioning;

namespace ScreenShotTool
{
    internal static class Program
    {
        [SupportedOSPlatform("windows")]
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            //Application.SetHighDpiMode(HighDpiMode.PerMonitorV2); // set in app.manifest
            ApplicationConfiguration.Initialize(); // this also sets the highdpimode, but the above line still seems to happen
            
            Application.Run(new MainForm());
        }
    }
}