namespace APImovies.Model
{
    public class TitleData
    {
        private DateTime dt { get; set; }
        public string Id { get; set; }
        public string FullTitle { set; get; }
        public string Year { set; get; }
        public string RuntimeStr { set; get; }
        public string Plot { set; get; }
        public string PlotLocal { set; get; }

        public string Awards { set; get; }
        public string Type { set; get; }
        public List<StarShort> DirectorList { get; set; }
        public List<StarShort> StarList { get; set; }

        public List<string> KeywordList { get; set; }
        public string Genres { set; get; }

        public string Languages { set; get; }

        public string ContentRating { get; set; }

    }


    public class StarShort
    {
        public string Name { get; set; }
    }
}
