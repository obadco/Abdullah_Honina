using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace VlcWithWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
     
    public partial class App : Application
    {
        public static Process PriorProcess()
        {
            Process curr = Process.GetCurrentProcess();

            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) &&
                    (p.MainModule.FileName == curr.MainModule.FileName))
                    return p;
               
            }

            return null;
        }

        public static MainWindow mainWindow = new MainWindow();
     

   


        private void Application_Startup(object sender, StartupEventArgs e)
        {

            /*if (PriorProcess() != null)
            {
                PriorProcess().Close();
                System.Windows.MessageBox.Show("Another instance is already running.");
                return;
            }
            else
            {*/

                if (e.Args != null && e.Args.Count() > 0)
                {
                    mainWindow.StartupMovie = e.Args[0];
                    mainWindow.Show();

                }

                else
                    mainWindow.Show();
                //if (e.Args != null && e.Args.Count() > 0)
                //{
                //    // mainWindow.StartupMovie = e.Args[0];
                //    StartupMovie = e.Args[0];
                //    SubtitleManagement submg = new SubtitleManagement();
                //    Functions_cls.SsrtMaker ssrt = new Functions_cls.SsrtMaker();
                //    if (StartupMovie != null)
                //    {

                //        if (submg.find_Sign_subtitle(new System.IO.FileInfo(StartupMovie)) != string.Empty)
                //        {
                //             ssrtPath = submg.find_Sign_subtitle(new System.IO.FileInfo(StartupMovie));
                //             mainWindow.Show();
                //        }



                //        else
                //        {
                //            if (submg.find_subtitle(new System.IO.FileInfo(StartupMovie)) != string.Empty)
                //            {
                //                srtPath = submg.find_subtitle(new System.IO.FileInfo(StartupMovie));
                //                //  ssrt.file_path = @"C:\Users\Murtada Al-Junaid\Documents\Visual Studio 2013\Projects\VlcWithWpf - Copy - Copy\VlcWithWpf\bin\Debug\Dora2";
                //                ssrt.srt_path = @"C:\Users\Murtada Al-Junaid\Desktop\Sub\Dora2.srt";
                //               // System.Windows.MessageBox.Show(ssrt.srt_path);
                //                System.IO.FileInfo inf = new System.IO.FileInfo(StartupMovie);
                //                ssrt.file_path = inf.Directory+@"\" + System.IO.Path.GetFileNameWithoutExtension(StartupMovie);
                //                  System.Windows.MessageBox.Show(ssrt.video_paths());

                //                    ssrtPath = ssrt.file_path + ".ssrt";
                //                    mainWindow.Show();


                //            }
                //            else
                //            { System.Windows.MessageBox.Show("Error2"); }


                //        }


                //    }
                //    else
                //    {
                //        System.Windows.MessageBox.Show("No file foud");
                //        mainWindow.Show();
                //    }
                //}

            
        }

    }
    public static class ApplicationRunningHelper
    {
        [DllImport("user32.dll")]
        private static extern
            bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern
            bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern
            bool IsIconic(IntPtr hWnd);

        /// -------------------------------------------------------------------------------------------------
        /// <summary> check if current process already running. if running, set focus to existing process and 
        ///           returns <see langword="true"/> otherwise returns <see langword="false"/>. </summary>
        /// <returns> <see langword="true"/> if it succeeds, <see langword="false"/> if it fails. </returns>
        /// -------------------------------------------------------------------------------------------------
        public static bool AlreadyRunning()
        {
            /*
            const int SW_HIDE = 0;
            const int SW_SHOWNORMAL = 1;
            const int SW_SHOWMINIMIZED = 2;
            const int SW_SHOWMAXIMIZED = 3;
            const int SW_SHOWNOACTIVATE = 4;
            const int SW_RESTORE = 9;
            const int SW_SHOWDEFAULT = 10;
            */
            const int swRestore = 9;

            var me = Process.GetCurrentProcess();
            var arrProcesses = Process.GetProcessesByName(me.ProcessName);

            if (arrProcesses.Length > 1)
            {
                for (var i = 0; i < arrProcesses.Length; i++)
                {
                    if (arrProcesses[i].Id != me.Id)
                    {
                        // get the window handle
                        IntPtr hWnd = arrProcesses[i].MainWindowHandle;

                        // if iconic, we need to restore the window
                        if (IsIconic(hWnd))
                        {
                            ShowWindowAsync(hWnd, swRestore);
                        }

                        // bring it to the foreground
                        SetForegroundWindow(hWnd);
                        break;
                    }
                }
                return true;
            }

            return false;
        }
    }
}
