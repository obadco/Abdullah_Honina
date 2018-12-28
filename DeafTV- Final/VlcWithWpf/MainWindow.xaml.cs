using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Forms;


using System.Data;


namespace VlcWithWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {


        // private Queue<string> playList = new Queue<string>();
        public MainWindow()
        {
            InitializeComponent();


            this.WindowStyle = WindowStyle.None;
            this.BorderThickness = new Thickness(0);
            this.tbMediaPlayer.Content = umMDp;
            this.tbDictionary.Content = umDic;
            mouseTimer = new DispatcherTimer();
            mouseTimer.Interval = TimeSpan.FromSeconds(3);
            mouseTimer.Tick += new EventHandler(mouseTimer_Tick);
            umMDp.mdElm.LoadedBehavior = MediaState.Manual;
            umMDp.mdElm.MediaOpened += new RoutedEventHandler(umMDp.Element_MediaOpened);

            SetLanguageDictionary();
            Togglemaximization();

        }


        //Var used to change the play and pause btns
        private bool fullscreen = false;
        private DispatcherTimer DoubleClickTimer = new DispatcherTimer();
        PlayerVlcWpf umMDp = new PlayerVlcWpf();
        Dictionary umDic = new Dictionary();
        private DispatcherTimer mouseTimer;
        SubtitleManagement submg = new SubtitleManagement();
        Functions_cls.SsrtMaker ssrt = new Functions_cls.SsrtMaker();
        public string StartupMovie { get; set; }
        private void Ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //This is only used to alternate btwn the media player tab and dictionary tab when clkin' on of them
            if (RbnMenu.SelectedIndex == 0)

                TCMain.SelectedItem = tbMediaPlayer;
            else
                TCMain.SelectedItem = tbDictionary;

        }

        public void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            if (Properties.Settings.Default.Language == "Arabic")
            {

                dict.Source = new Uri(@"F:\books\level4\VlcWithWpf\VlcWithWpf\ArabicLanguage.xaml");


                RbnMenu.FlowDirection = System.Windows.FlowDirection.RightToLeft;
                umDic.FlowDirection = System.Windows.FlowDirection.RightToLeft;




            }
            else if (Properties.Settings.Default.Language == "English")
            {
                dict.Source = new Uri(@"F:\books\level4\VlcWithWpf\VlcWithWpf\EnglishLanguage.xaml");

                RbnMenu.FlowDirection = System.Windows.FlowDirection.LeftToRight;
                umDic.FlowDirection = System.Windows.FlowDirection.LeftToRight;

            }
            else
            {
                dict.Source = new Uri(@"F:\books\level4\VlcWithWpf\VlcWithWpf\EnglishLanguage.xaml");
                this.FlowDirection = System.Windows.FlowDirection.LeftToRight;
            }
            this.Resources.MergedDictionaries.Add(dict);


        }

        private void check_srt(string File)
        {

            if (File != null)
            {
                System.IO.FileInfo FileInf = new System.IO.FileInfo(File);


                if (submg.find_Sign_subtitle(FileInf) != string.Empty)
                {
                    umMDp.path = (submg.find_Sign_subtitle(FileInf));
                    if (submg.find_subtitle(FileInf) != string.Empty)
                        umMDp.srtPath = submg.find_subtitle(FileInf);

                    PlayMovie(FileInf);

                }
                else
                {
                    if (submg.find_subtitle(FileInf) != string.Empty)
                    {
                        ssrt.srt_path = submg.find_subtitle(FileInf);
                        ssrt.file_path = FileInf.Directory + @"\" + System.IO.Path.GetFileNameWithoutExtension(FileInf.Name);
                        ssrt.video_paths();
                        umMDp.path = ssrt.file_path + ".ssrt";
                        umMDp.srtPath = ssrt.srt_path;
                        PlayMovie(FileInf);


                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Subtitle was not loaded");
                        PlayMovie(FileInf);
                        umMDp.mediaPlayerBorder2.Visibility = System.Windows.Visibility.Hidden;

                    }

                }

            }

            else
                System.Windows.MessageBox.Show("No file was found");
        }

        private void mouseTimer_Tick(object sender, EventArgs e)
        {
            if (fullscreen)
            {
                mouseTimer.Stop();
                //Animation.StoryboardLibrary.MenuAnim(umMDp.brdControls, Animation.StoryboardLibrary.MenuAnimOption.Hide, umMDp.brdControls.RenderSize.Height + 2, new SineEase(), EasingMode.EaseIn, Animation.StoryboardLibrary.MoveDirection.UpDown).Begin();
                Cursor = System.Windows.Input.Cursors.None;
                TCMain.Margin = new Thickness(-8, -24, -5, -77);

                umMDp.brdControls.Opacity = 0;



            }
        }





        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    if (fullscreen)
                    {
                        Cursor = System.Windows.Input.Cursors.Hand;
                        mouseTimer.Interval = TimeSpan.FromSeconds(4);
                        mouseTimer.Start();
                        //Animation.StoryboardLibrary.MenuAnim(umMDp.brdControls, Animation.StoryboardLibrary.MenuAnimOption.Show, umMDp.brdControls.RenderSize.Height , new SineEase(), EasingMode.EaseIn, Animation.StoryboardLibrary.MoveDirection.UpDown).Begin();
                        umMDp.brdControls.Opacity = 1;
                        TCMain.Margin = new Thickness(-8, -24, -5, 0);
                    }

                    break;
                case WindowState.Minimized:
                    Cursor = System.Windows.Input.Cursors.Arrow;
                    break;
                case WindowState.Normal:
                    Cursor = System.Windows.Input.Cursors.Arrow;

                    break;
                default:
                    break;

            }
        }
        private void MPTab(object sender, RoutedEventArgs e)
        {

        }


        private void DicTab(object sender, RoutedEventArgs e)
        {
            //var DictinaryTab = new Dictionary();

            //if (!clk)
            //{

            //    this.tabControl1.SelectedIndex =
            //   this.tabControl1.Items.Add(
            //   new TabItem { Header = "Dictionary", Content = DictinaryTab, Name = "DicTab" });

            //}
            //else
            //{
            //    this.tabControl1.SelectedIndex = 1;

            //}

        }

        private void Dic_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void Dic_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


        }

        private void Mediaplayer_Click(object sender, RoutedEventArgs e)
        {

            //Chng the tab to the media player tab when clkin' on this btn
            TCMain.SelectedItem = tbMediaPlayer;
            RbnMenu.SelectedItem = Mediaplayer;

        }

        private void Dictinary_Click(object sender, RoutedEventArgs e)
        {
            //Chng the tab to the dictionary tab when clkin' on this btn
            TCMain.SelectedItem = tbDictionary;
            RbnMenu.SelectedItem = Dictinary;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //Load the video that u want to run
            OpenFileDialog ofd = new OpenFileDialog(); ;
            ofd.AddExtension = true;
            ofd.DefaultExt = "*.*";
            ofd.Filter = "Media (*.*) |*.*";
            ofd.ShowDialog();
            try
            {
                check_srt(ofd.FileName);
            }
            catch (Exception )
            {

                //throw;
            }





        }



        private void Togglemaximization()
        {
            if (this.WindowState == WindowState.Maximized)
            {



                umMDp.mediaPlayerBorder2.Margin = new Thickness(620, -81, -418, 8);

                umMDp.mediaPlayerBorder2.Height = 240;
                umMDp.mediaPlayerBorder2.Width = 320;
                grdToolBar.Margin = new Thickness(0, 0, -840, 0);
                btnMin.Margin = new Thickness(1230, 0, 0, 0);
                btnMax.Margin = new Thickness(1255,0,0,0);
                btnclose.Margin = new Thickness(1275, 0, 0,0);
                umMDp.lblSub.FontSize = 35;
                umMDp.lblSub.Width =  800;
                umMDp.lblSub.Margin = new Thickness(270, 0, 0, 0);
              
            }
            else
            {
                umMDp.mediaPlayerBorder2.Margin = new Thickness(440, 0, 0, 7);
                umMDp.mediaPlayerBorder2.Height = 150;
                umMDp.mediaPlayerBorder2.Width = 198;
                grdToolBar.Margin = new Thickness(0, 0, -158, 0);
                btnMin.Margin = new Thickness(500, 0, 0, 0);
                btnMax.Margin = new Thickness(525, 0, 0, 0);
                btnclose.Margin = new Thickness(545, 0, 0, 0);
                umMDp.lblSub.FontSize = 15;
             
            }


        }

        private void toggleFulScreen()
        {
            if (!fullscreen)
            {


                this.WindowState = WindowState.Maximized;
                umMDp.mediaPlayerBorder2.Margin = new Thickness(620, -81, -418, 8);

                Togglemaximization();
                umMDp.grdMain.Width = tbMediaPlayer.Width;
                umMDp.grdMain.Height = tbMediaPlayer.Height;


            }
            else
            {
                //this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
                TCMain.Margin = new Thickness(0, 41, 0, 0);
                umMDp.grdMain.Width = tbMediaPlayer.Width;
             
                Togglemaximization();

                umMDp.grdMain.Height = tbMediaPlayer.Height;
                umMDp.brdControls.Opacity = 1;

                RbnMenu.Visibility = System.Windows.Visibility.Visible;

            }

            fullscreen = !fullscreen;


        }
        public void mdElm_MouseDown(object sender, MouseButtonEventArgs e)
        {


            if (e.ClickCount % 2 == 0)
            {
                toggleFulScreen();


            }


        }

        private void Window_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                if (files.Count() == 1)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(files[0]);
                    if (file.Extension == "avi" ||
                        file.Extension == "vmw" ||
                         file.Extension == "flv" ||
                        file.Extension == "mp4" ||
                        file.Extension == "mov")
                    {
                        e.Effects = System.Windows.DragDropEffects.All;
                    }
                }
                else
                {
                    e.Effects = System.Windows.DragDropEffects.None;
                }
            }
        }

        private void Window_Drop(object sender, System.Windows.DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
            if (files.Count() == 1)
                check_srt(files[0]);





        }


        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            umMDp.volumeSlider.Value = umMDp.volumeSlider.Value + ((e.Delta / 120) * 0.05);
        }

        private void isfullScreen()
        {
            if (fullscreen)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
                umMDp.grdMain.Width = tbMediaPlayer.Width;
                umMDp.grdMain.Height = tbMediaPlayer.Height;
                TCMain.Margin = new Thickness(0, 41, 0, 0);
                umMDp.mediaPlayerBorder2.Margin = new Thickness(428, -34, 8, 10);
                umMDp.mediaPlayerBorder2.Height = 188;
                RbnMenu.Visibility = System.Windows.Visibility.Visible;

                umMDp.brdControls.Opacity = 1;
                fullscreen = !fullscreen;


            }






        }

        private void toggleState()
        {
            if (umMDp.mdElm.LoadedBehavior != MediaState.Pause)

                umMDp.pause();
            else
                umMDp.play();






        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    isfullScreen();
                    break;
                case Key.MediaStop:
                    umMDp.stop();
                    break;
                case Key.Left:
                    umMDp.mdElm.Position += TimeSpan.FromSeconds(-15);
                    break;
                case Key.Right:
                    umMDp.mdElm.Position += TimeSpan.FromSeconds(15);
                    break;
                case Key.Up:
                    umMDp.volumeSlider.Value = umMDp.volumeSlider.Value + (0.1);
                    break;
                case Key.Down:
                    umMDp.volumeSlider.Value = umMDp.volumeSlider.Value - (0.1);
                    break;

                case Key.Enter:
                    toggleFulScreen();
                    break;
                case Key.Space:
                    toggleState();
                    break;
                case Key.VolumeDown:
                    umMDp.volumeSlider.Value = umMDp.volumeSlider.Value - (0.3);
                    break;
                case Key.VolumeUp:
                    umMDp.volumeSlider.Value = umMDp.volumeSlider.Value + (0.3);
                    break;
                case Key.MediaPlayPause:
                    umMDp.pause();
                    break;



            }
        }



        private void Window_Closed(object sender, EventArgs e)
        {
            var current = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.Process.GetProcessesByName(current.ProcessName)
                .Where(t => t.Id != current.Id)
                .ToList()
                .ForEach(t => t.Kill());

            current.Kill();
            System.Windows.Application.Current.Shutdown();

        }


        private void PlayMovie(System.IO.FileInfo VidFile)
        {

            Video openedVideo = new Video
            {
                Path = VidFile.FullName,
                LastComputer = Environment.MachineName,
                LastPosition = 0,
                MovieId = Guid.NewGuid()
            };

            //  umMDp.mdElm.Source = new Uri();
            try
            {
                //Call initialing function to set the loaded videos to a default range of time and volume
                // umMDp.InitializePropertyValues();
                // umMDp.mdElm.Source = new Uri();
                if (umMDp.mediaPlayerBorder2.Visibility == System.Windows.Visibility.Hidden)
                    umMDp.mediaPlayerBorder2.Visibility = System.Windows.Visibility.Visible;
                umMDp.mdElm.Source = new Uri(openedVideo.Path);

                umMDp.mdElm.LoadedBehavior = System.Windows.Controls.MediaState.Play;
                //Assing the loaded video to ur media player
                //
                //mdElmDf.Source = new Uri(ofd.FileName);



            }
            catch (Exception)
            {

                throw;
            }

            System.IO.FileInfo fi = null;
            if (!string.IsNullOrEmpty(openedVideo.Path))
                fi = new System.IO.FileInfo(openedVideo.Path);

            this.Title = "DEAF T.V - " + fi.Name;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrEmpty(StartupMovie))
                System.Windows.MessageBox.Show("No file opened");


            else
                check_srt(StartupMovie);


        }

        private void btnHelping_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            Settings stng = new Settings();
            stng.Show();
        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        int counter = 2;

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
           /* if (counter % 2 == 0)
            {
                //this.Height = 744;
                //this.Width = 1382;
                //this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                this.WindowState = WindowState.Maximized;
                Togglemaximization();
                counter++;
            }
            else
            {
                this.WindowState = WindowState.Normal;

                Togglemaximization();
                counter++;
            }*/
            if(this.WindowState!=WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
                Togglemaximization();
            }
            else
            {
                this.WindowState = WindowState.Normal;
                Togglemaximization();
            }

        }

    }
}
