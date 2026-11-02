using System;
using System.IO;
using System.Windows.Forms;
using Lab09_DataReaderSearch.GUI;

namespace Lab09_DataReaderSearch
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            Application.ThreadException += (s, e) =>
            {
                Console.WriteLine("ThreadException: " + e.Exception.ToString());
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Console.WriteLine("UnhandledException: " + e.ExceptionObject.ToString());
            };

            if (args != null && args.Length > 0 && args[0] == "--capture")
            {
                try
                {
                    string outDir = args.Length > 1 ? args[1] : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "screenshots");
                    ScreenshotCapture.RunCapture(outDir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("RunCapture Exception: " + ex.ToString());
                }
                return;
            }

            Application.Run(new frmMain());
        }
    }
}
