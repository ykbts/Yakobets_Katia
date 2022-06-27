using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using APImovies.Constatnt;
using APImovies.Model;
using System.Net;
using System.Web.Http;

namespace APImovies.Client
{
    public class MovieClient
    {
        private HttpClient _client;
        private static string _address;
        private static string _apikey;
        int message;
        public MovieClient()
        {
            _address = Const.address;
            _apikey = Const.apikey;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }


        public async Task<MovieInfo> Search(string expression)
        {
            var response = await _client.GetAsync($"/API/Search/{Const.apikey}/{expression}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {  
               
                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<MovieInfo>(content);
                
                return result;
            }

            else throw new Exception(response.ReasonPhrase);
            

        }
      

        public async Task<TitleData> Get_Movie_Serial(string lang, string id)
        {
           
         var response = await _client.GetAsync($"/{lang}/API/Title/{Const.apikey}/{id}");
         response.EnsureSuccessStatusCode();
       
                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<TitleData>(content);
                return result;
           
        }

        public async Task<YouTubeTrailerData> Trailer( string id)
        {

            var response = await _client.GetAsync($"/API/YouTubeTrailer/{Const.apikey}/{id}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<YouTubeTrailerData>(content);
            return result;
        }
        public async Task<RatingData> GetRating( string id)
        {

            var response = await _client.GetAsync($"/API/Ratings/{Const.apikey}/{id}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<RatingData>(content);
            return result;
        }
        
    }
}
