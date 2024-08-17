using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies_LLD_.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public int? Duration { get; set; } = null;
        public string Genre { get; set; }
        public string Summary => $"{Title} ({Duration}m, {Genre})";

        //public Movie (string title, int? duration, string genre)
        //{
        //    Title = title;
        //    Duration = duration;
        //    Genre = genre;
        //}
    }
}
