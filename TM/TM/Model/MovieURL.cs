using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Model
{
    public class MovieURL
    {
       
        public string IMDbId { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }

       public MovieURL(string IMDbId, string VideoUrl, string Title)
        {
            this.IMDbId = IMDbId;
            this.VideoUrl = VideoUrl;
            this.Title = Title;
        }
        public override string ToString() {
            return Title + "; ";
        
        }
    }
}
