using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using TM.Model;


namespace TM
{

    public class MovieClient
    {
        private HttpClient _client;
        private static string _address;
        private static string _apikey;
        public MovieClient()
        {
            _address = Const.address;
            _apikey = Const.apikey;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }


       
        public async Task<TitleData> Get_Movie_Serial(string lang, string name)
        {

            var response = await _client.GetAsync($"/Movie?lang={lang}&name={name}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<TitleData>(content);
            return result;
        }

        public async Task<YouTubeTrailerData> Trailer(string name)
        {

            var response = await _client.GetAsync($"/Trailer?name={name}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<YouTubeTrailerData>(content);
            return result;
        }
        public async Task<RatingData> GetRating(string name)
        {

            var response = await _client.GetAsync($"/Rating?name={name}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<RatingData>(content);
            return result;
        }

      
        public async Task<MovieURL[]> GetAllMovies()
        {
            var response = await _client.GetAsync($"/api/Data");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<MovieURL[]>(content);
            return result;
        }
        public async Task PostMovieURL(MovieURL movieURL)
        {
            var add = JsonConvert.SerializeObject(movieURL);
            var httpcontent = new StringContent(add, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/Data", httpcontent);
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<MovieURL>(content);
        }
        public async Task<MovieURL> GetMovieURL(string id)
        {
            var response = await _client.GetAsync($"/api/Data/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MovieURL>(content);
            return result;
        }

        public async Task DeleteMovieURL(string IMDbId)
        {
            var response = await _client.DeleteAsync($"/api/Data?IMDbId={IMDbId}");

        }

        public async Task PutMovieURL(string IMDbId, MovieURL movieURL)
        {
            var add = JsonConvert.SerializeObject(movieURL);
            var httpcontent = new StringContent(add, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/Data/Update?IMDbId={IMDbId}", httpcontent);
        }

       
    }

}
