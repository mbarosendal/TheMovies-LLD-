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
            _movies = new List<Movie>();
            // testdata
            //{
            //    new Movie { Title = "Titanic", Duration = 120, Genre = "Drama, Romance" },
            //    new Movie { Title = "Inception", Duration = 148, Genre = "Sci-Fi" }
            //};
            //LoadMoviesfromCSV();
        }

        public void AddMovie(Movie movie)
        {
            _movies.Add(movie);
        }

        public void RemoveMovie(Movie movie)
        {
            _movies.Remove(movie);
        }

        public List<Movie> GetAllMovies()
        {
            return _movies;
        }

        public bool ContainsMovie(Movie movie)
        {
            return _movies.Contains(movie);
        }

        public void SaveMoviesToCSV(List<Movie> movies, string filePath = "movies.csv")
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                // Sæt headers i CSV-filen
                string header = "Title" + ";" + "Duration" + ";" + "Genre" + ";" + "Director" + ";" + "Premiere Dato" ;
                sw.WriteLine(header);

                // Skriv hver film til CSV-filen
                foreach (Movie movie in movies)
                {
                    string movieLine = $"{movie.Title};{movie.Duration};{movie.Genre};{movie.Director};{movie.PremiereDate}";
                    sw.WriteLine(movieLine);
                }
            }
        }

        public void LoadMoviesfromCSV(string filePath = "movies.csv")
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                // Læs først header (og skip den)
                string headerLine = sr.ReadLine();
                string line;

                // Læs så længe der er linjer i filen og stop ved tom linje
                while ((line = sr.ReadLine()) != null)
                {
                    string[] movieLine = line.Split(';');

                    // Henter værdierne fra CSV-filen via index i arrayet
                    string Title = movieLine[0];
                    TimeSpan.TryParse(movieLine[1], out TimeSpan Duration);
                    string Genre = movieLine[2];

                    // Tjek om filmen allerede er tilføjet via hjælpemetoden WasMovieAlreadyAdded (ud fra titel)
                    if (IsMovieAlreadyAdded(Title))
                    {
                        // Hvis filmen allerede er tilføjet, så fortsæt til næste linje i CSV-filen
                        continue;
                    }
                    else
                    {
                        // Ellers opret et nyt Movie-objekt og tilføj det til listen over film
                        Movie newMovie = new Movie()
                        {
                            Title = Title,
                            Genre = Genre,
                            Duration = Duration
                        };

                        AddMovie(newMovie);
                    }
                }
            }
        }

        // Hjælpemetode til at tjekke om filmen allerede er tilføjet
        private bool IsMovieAlreadyAdded(string title)
        {
            return _movies.Any(m => m.Title == title);
        }
    }
}
