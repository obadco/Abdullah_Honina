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
using System.Windows.Forms.Integration;
using System.Windows.Forms;
using System.Data;
using Declarations;
using Declarations.Events;
using Declarations.Media;
using Declarations.Players;
using Implementation;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using LibVlcWrapper;
using System.Windows.Threading;

namespace VlcWithWpf
{
    /// <summary>
    /// Interaction logic for PlayerVlcWpf.xaml
    /// </summary>
    /// 

    public partial class PlayerVlcWpf 
    {
        public IMediaPlayerFactory m_factory;
       public IVideoPlayer m_player;
       public  IMediaFromFile m_media;
        private volatile bool m_isDrag;
        public PlayerVlcWpf()
        {


           
            InitializeComponent();
           
         
          timer = new DispatcherTimer();
          timer.Interval = TimeSpan.FromSeconds(1);
          timer.Tick += new EventHandler(timer_Tick);
       
             InitializePropertyValues();


             System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();

             p.BackColor = System.Drawing.Color.Transparent;
             windowsFormsHost1.Opacity = 0.5;
             windowsFormsHost1.Child = p;




             m_factory = new MediaPlayerFactory(true);
             m_player = m_factory.CreatePlayer<IVideoPlayer>();
             m_player.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
             m_player.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
             m_player.Events.MediaEnded += new EventHandler(Events_MediaEnded);
             m_player.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);

        
             m_player.WindowHandle = p.Handle;
     


            

          
        }



        void Events_PlayerStopped(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                
            }));
        }

        void Events_MediaEnded(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
               
            }));
            
                PlayVideoPlaylist();
        }

      

        void Events_TimeChanged(object sender, MediaPlayerTimeChanged e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                //label1.Content = TimeSpan.FromMilliseconds(e.NewTime).ToString().Substring(0, 8);
            }));
        }

        void Events_PlayerPositionChanged(object sender, MediaPlayerPositionChanged e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                if (!m_isDrag)
                {
                    
                }
            }));
        }


        private void playvideo(string path)
        {

           m_media = m_factory.CreateMedia<IMediaFromFile>(path);
          

           m_player.Open(m_media);
           m_media.Parse(true);

           m_player.Play();
           
        }





        public string path = "";
        public string srtPath = "";
        DataTable dt = new DataTable();
        Functions_cls.SsrtMaker ssrt = new Functions_cls.SsrtMaker();
        SubtitleManagement submg = new SubtitleManagement();
        DataTable subDt = new DataTable();

        private Queue<string> playList = new Queue<string>();
        //Var used to change the play and pause btns
        Boolean isDragging;

         private DispatcherTimer timer;



        public void play()
         {
             mdElm.LoadedBehavior = System.Windows.Controls.MediaState.Play;
             if (m_player.PlayerWillPlay)
             {
                 m_player.Play();
                 paused = false;
             }

         }

        bool paused = false;
        public void pause()
        {
            mdElm.LoadedBehavior = System.Windows.Controls.MediaState.Pause;
            if (m_player.IsPlaying)
            {
                m_player.Pause();
                paused = true;
            }
            
        }

        public void stop()
        {
            mdElm.LoadedBehavior = System.Windows.Controls.MediaState.Stop;
            if (m_player.IsPlaying)
                m_player.Stop();

        }
        //Enables the user to stop the video
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

            stop();
           
        }



      
        private void ucMediaPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
           // this.Width = mw.tbMediaPlayer.Width;
            //this.Height = mw.tbMediaPlayer.Height;

        }

        public void Element_MediaOpened(object sender, RoutedEventArgs e)
        {

            try
            {
                
                if (mdElm.NaturalDuration.HasTimeSpan)
                {
                    TimeSpan ts = mdElm.NaturalDuration.TimeSpan;

                    timelineSlider.Maximum = ts.TotalSeconds;
                    timelineSlider.SmallChange = 1;
                    timelineSlider.LargeChange = Math.Min(12, ts.Seconds / 10);
                    lblTotal.Content = (DateTime.Today + mdElm.NaturalDuration.TimeSpan).ToString("HH:mm:ss");
                   // System.Windows.MessageBox.Show(path);
                    dt = submg.ExtractSsrt(path);
                    subDt = submg.Readsrt(srtPath);
                   
                }
                timer.Start();
            }
            catch (Exception)
            {
               
                throw;
            }
            


         

            //lblPeriod.Content = String.Format("{0} / {1}", mdElm.Position.ToString(@"mm\:ss"), mdElm.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
        }


        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, RoutedEventArgs e)
        {
                    stop();
        }


        // When the media opens, initialize the "Seek To" slider maximum value
        // to the total number of miliseconds in the length of the media clip.

        // Function  to set the sound of the played videos to the sound set by the user
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            try
            {
                mdElm.Volume = (double)volumeSlider.Value;

                //mdElmDf.Volume = (double)volumeSlider.Value;

            }
            catch (Exception)
            {

                throw;
            }


        }


        private void timelineSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isDragging = true;
        }

        private void timelineSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            mdElm.Position = TimeSpan.FromSeconds(timelineSlider.Value);
            isDragging = false;
        }
        //Play the video after it's been loaded
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

            play();

        }

        //Initialize the volum and time of the videos to the default values
      public  void InitializePropertyValues()
        {
            // Set the media's starting Volume  to the current value of the
            // their respective slider controls.
              mdElm.Volume = (double)0.5;
             volumeSlider.Value = 0.5;



        }

      private void timer_Tick(object sender, EventArgs e)
      {

          if (!isDragging)
          {
              timelineSlider.Value = mdElm.Position.TotalSeconds;

              string formatted = (DateTime.Today + mdElm.Position).ToString("HH:mm:ss");

              lblPeriod.Content = formatted;
          }

        
         
      }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            pause();
        }

        SubtitleManagement subcls = new SubtitleManagement();
       cls_searchSigns srchDt = new cls_searchSigns();
        //Enables the user to move the videos to any position he wants
           
             List<string> Lines = new List<string>();
             List<string> words = new List<string>();
           //  Functions_cls.Bind_Items bind = new Functions_cls.Bind_Items();
             int index = 0;
             int counter = 0;
           
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            int SliderValue = (int)timelineSlider.Value;


            mdElm.Position = TimeSpan.FromSeconds(timelineSlider.Value);
            TimeSpan startTime = TimeSpan.Zero;
            TimeSpan endTime = TimeSpan.Zero;
           
                 
           

             for (int m = index; m < dt.Rows.Count; m++)
             {
                 counter = 0;
                 if (!TimeSpan.TryParse(dt.Rows[m]["BeginTime"].ToString().Replace(",", "."), out startTime)) ;
                 if (!TimeSpan.TryParse(dt.Rows[m]["EndTime"].ToString().Replace(",", "."), out endTime)) ;

                 //if(dt.Rows[m]["Text"].ToString()!=" ")
                 
                  if (((DateTime.Today + mdElm.Position).ToString("HH:mm:ss") == (DateTime.Today + startTime).ToString("HH:mm:ss")) )
                    {
                        
                     

                      
                  try
                    {
                        if (subDt.Rows.Count>0)
                            lblSub.Text = subDt.Rows[m]["srtText"].ToString();
                      
                        playList.Clear();
                        string[] paths = dt.Rows[m]["path"].ToString().Split(';');
                       //PlayAudioPlaylist();
                       for (int i = 0; i < paths.Length-1; i++)
                       {
                          
                           playList.Enqueue(@paths[i]);
                       }


                       PlayVideoPlaylist();
                      

                 }
                  catch (Exception)
                  {
                      System.Windows.MessageBox.Show("Unknown problem occured");
                      throw ;
                  }
           

             }
                  
            }
           

                    
                     
        }





      


        private void PlayVideoPlaylist()
        {
          
            if (playList.Count > 0 && !paused)
                 playvideo(playList.Dequeue()); 
        }


              
                
                
        

        private void mdElm_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            
        }

        private void mdElm_Drop(object sender, System.Windows.DragEventArgs e)
        {
         
        }

        private void mdElmDf_MediaEnded(object sender, RoutedEventArgs e)
        {
            PlayVideoPlaylist();
           
        }

        

      
        private void UserControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
          

        }

       
       
       

       
          

    
    }
}
