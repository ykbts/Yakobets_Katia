using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APImovies.Model
{

    public class MovieInfo
    {
        public List<SearchResult> Results { get; set; }
       

    }


    public class SearchResult
    {
        public string Id { get; set; }

    }
   
   



}
