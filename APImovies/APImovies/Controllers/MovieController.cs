using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using APImovies.Constatnt;
using APImovies.Model;
using APImovies.Client;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;




namespace APImovies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {

        [HttpGet(Name = "Movie_Series")]

        public TitleData Movie_Series(string lang, string name)
        {

            MovieClient movie = new MovieClient();
            if (movie.Search(name).Result.Results.Count != 0)
            {
                var id = movie.Search(name).Result.Results.FirstOrDefault().Id;
                if (id != null)
                    return movie.Get_Movie_Serial(lang, id).Result;
                else return movie.Get_Movie_Serial(lang, "").Result;

            }

            else return movie.Get_Movie_Serial(lang, "").Result; 


        }
    }
}

