using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using TheMovies_LLD_.Models;

namespace TheMovies_LLD_.Repository
{
    public class MovieRepository
    {
        private List<Movie> _movies;
        public MovieRepository()
        {
            _movies = new List<Movie>
            {
                new Movie { Title = "Titanic", Duration = 120, Genre = "Drama, Romance" },
                new Movie { Title = "Inception", Duration = 148, Genre = "Sci-Fi" }
            };
            //SaveMovies(_movies);
            //LoadMoviesfromCSV();
        }

        public void AddMovie(Movie movie)
        {
            _movies.Add(movie);
        }

        public List<Movie> GetAllMovies()
        {
            return _movies;
        }

        public bool ContainsMovie(Movie movie)
        {
            return _movies.Contains(movie);
        }

        //public void SaveMoviesToCSV(List<Movie> movies)
        //{
        //    string filePath = "movies.csv";

        //    using (StreamWriter sw = new StreamWriter(filePath))
        //    {
        //        string header = "Title" + ";" + "Duration" + ";" + "Genre";
        //        sw.WriteLine(header);

        //        foreach (Movie movie in movies)
        //        {
        //            string movieLine = $"{movie.Title};{movie.Duration};{movie.Genre}";
        //            sw.WriteLine(movieLine);
        //        }
        //    }
        //}

        //public void LoadMoviesfromCSV()
        //{
        //    string filePath = "movies.csv";

        //    using (StreamReader sr = new StreamReader(filePath))
        //    {
        //        // SKip header
        //        string headerLine = sr.ReadLine();
        //        string line;

        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            string[] movieLine = line.Split(';');

        //            string Title = movieLine[0];
        //            int.TryParse(movieLine[1], out int Duration);
        //            string Genre = movieLine[2];

        //            if (WasMovieAlreadyAdded(Title))
        //            {
        //                continue;
        //            }
        //            else
        //            {
        //                Movie newMovie = new Movie()
        //                {
        //                    Title = Title,
        //                    Genre = Genre,
        //                    Duration = Duration
        //                };

        //                AddMovie(newMovie);
        //            }   
        //        }
        //    }
        //}

        //private bool WasMovieAlreadyAdded(string title)
        //{
        //    return _movies.Any(m => m.Title == title);
        //}
    }
}
