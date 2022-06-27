using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using APImovies.Constatnt;
using APImovies.Model;
using APImovies.Client;


namespace APImovies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrailerController : ControllerBase
    {
        [HttpGet(Name = "Trailer")]
        public YouTubeTrailerData TrailerData(string name)
        {

            MovieClient movie = new MovieClient();
            if (movie.Search(name).Result.Results.Count != 0)
            {
                var IMDbId = movie.Search(name).Result.Results.FirstOrDefault().Id;
                if (IMDbId != null)
                    return movie.Trailer(IMDbId).Result;
                else return movie.Trailer("").Result;
            }

            else return movie.Trailer("").Result;





        }
    }
}
