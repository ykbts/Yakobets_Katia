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
using System.Net;

namespace APImovies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        [HttpGet(Name = "Rating")]
        public RatingData Rating(string name)
        {

            MovieClient movie = new MovieClient();
            if (movie.Search(name).Result.Results.Count != 0)
            {
                var id = movie.Search(name).Result.Results.FirstOrDefault().Id;
                if (id != null)
                    return movie.GetRating(id).Result;
                else return movie.GetRating("").Result;
            }

            else return movie.GetRating("").Result;
           
            
           

        }
    }
}
