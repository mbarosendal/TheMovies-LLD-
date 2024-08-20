using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private readonly MovieRepository _movieRepository;
        public ObservableCollection<MovieViewModel> Movies { get; } // + full property med OnPropChanged og backing field i stedet for movieviewmodel?
        public MovieViewModel MovieToAdd { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public MainViewModel()
        {
            _movieRepository = new MovieRepository();
            Movies = new ObservableCollection<MovieViewModel>(_movieRepository.GetAllMovies()
                                                .Select(movie => new MovieViewModel(movie)));
            MovieToAdd = new MovieViewModel(new Movie());
            AddCommand = new RelayCommand(x => AddMovie(), x => CanAddMovie());
            ClearCommand = new RelayCommand(x => ClearFields());
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
            var newMovie = new Movie
            {
                Title = MovieToAdd.Title,
                Duration = MovieToAdd.Duration,
                Genre = MovieToAdd.Genre,
            };

            _movieRepository.AddMovie(newMovie);

            Movies.Add(new MovieViewModel(newMovie));
        }

        private void ClearFields()
        {
            MovieToAdd.Title = string.Empty;
            MovieToAdd.Duration = null;
            MovieToAdd.Genre = string.Empty;
        }
    }
}
