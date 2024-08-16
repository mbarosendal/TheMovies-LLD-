using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                //testmovies
                new Movie { Title = "Titanic", Duration = 120, Genre = "Drama" },
                new Movie { Title = "Inception", Duration = 148, Genre = "Sci-Fi" }
            };
        }

        public void AddMovie(Movie movie)
        {
            _movies.Add(movie);
        }

        public List<Movie> GetAllMovies() 
        { 
            return _movies;
        }

    }
}
