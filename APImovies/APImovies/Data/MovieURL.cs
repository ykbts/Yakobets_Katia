using System.ComponentModel.DataAnnotations;

namespace APImovies.Data
{
    public class MovieURL
    {
        [Key]
        public string IMDbId { get; set; }
        public string Title { get; set; }

        public string VideoUrl { get; set; }
    }
}
