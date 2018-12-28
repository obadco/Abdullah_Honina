using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace VlcWithWpf
{
    class Video
    {


         public Guid MovieId { get; set; }
        public string Path { get; set; }
        public string Name { 
            get
            {
               FileInfo fi = null;
                if (!string.IsNullOrEmpty(Path))
                    fi = new FileInfo(Path);

                return fi != null ? fi.Name : "";
            }
        }
        public double LastPosition { get; set; }
        public string LastComputer { get; set; }
        public string LastPositionTime
        {
            get
            {
                string formatted = (DateTime.MinValue + TimeSpan.FromSeconds(LastPosition)).ToString("HH:mm:ss");
                return formatted;                
            }
        }

        public Video()
        {
            MovieId = Guid.NewGuid();
        }
    }
}
