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

namespace VlcWithWpf
{
    /// <summary>
    /// Interaction logic for VlcPlayer.xaml
    /// </summary>
    public partial class VlcPlayer 
    {
        IMediaPlayerFactory m_factory;
        IVideoPlayer m_player;
        IMediaFromFile m_media;
        private volatile bool m_isDrag;
        public VlcPlayer()
        {
            InitializeComponent();

            
            System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();

            p.BackColor = System.Drawing.Color.Transparent;
            windowsFormsHost1.Opacity = 0.5;
            windowsFormsHost1.Child = p;


            m_factory = new MediaPlayerFactory(true);
            m_player = m_factory.CreatePlayer<IVideoPlayer>();
          
            m_media = m_factory.CreateMedia<IMediaFromFile>(@"E:\Render Library\Movie paths\Dance+SFS2.avi");
            // m_media.Events.DurationChanged += new EventHandler<MediaDurationChange>(Events_DurationChanged);
            //m_media.Events.StateChanged += new EventHandler<MediaStateChange>(Events_StateChanged);

            m_player.Open(m_media);
            m_media.Parse(true);
            m_player.Play();

          
        }
    }
}
