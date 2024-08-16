using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheMovies_LLD_.Commands;
using TheMovies_LLD_.Models;
using TheMovies_LLD_.Repository;

namespace TheMovies_LLD_.ViewModel
{
    public class MainViewModel
    {
        private MovieRepository _movieRepository { get; set; }
        public ObservableCollection<Movie> Movies { get; set; }
        public Movie MovieToAdd { get; set; }
        public ICommand AddCommand { get; set; }

        public MainViewModel()
        {
            _movieRepository = new MovieRepository();
            Movies = new ObservableCollection<Movie>(_movieRepository.GetAllMovies());
            MovieToAdd = new Movie();
            AddCommand = new RelayCommand(x => AddMovie(), x => CanAddMovie());
        }

        private bool CanAddMovie()
        {
            return HasAllValues();         
        }

        private bool HasAllValues()
        {
            if (MovieToAdd.Title != null &&
                MovieToAdd.Duration != null &&
                MovieToAdd.Genre != null)
            {
                return true;
            }
            return false;
        }

        private void AddMovie()
        {
            // Lav et nyt Film objekt til lagring ud fra FilmToAdd
            var newMovie = new Movie
            {
                Title = MovieToAdd.Title,
                Duration = MovieToAdd.Duration,
                Genre = MovieToAdd.Genre,
            };

            _movieRepository.AddMovie(newMovie);

            // Opdater ObservableCollection med det nye data
            Movies.Add(newMovie);
        }
    }
}
