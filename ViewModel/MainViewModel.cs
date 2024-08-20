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
        public MovieViewModel SelectedMovie { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand RemoveCommand { get; set;}

        public MainViewModel()
        {
            // Opretter en ny instans af MovieRepository og henter alle film fra CSV-filen
            _movieRepository = new MovieRepository();
            // Opretter en ObservableCollection af MovieViewModels, som indeholder alle film-objekter konverteret til MovieViewModels-objekter (LINQ)
            Movies = new ObservableCollection<MovieViewModel>(_movieRepository.GetAllMovies()
                                                .Select(movie => new MovieViewModel(movie)));
            MovieToAdd = new MovieViewModel(new Movie());
            AddCommand = new RelayCommand(x => AddMovie(), x => CanAddMovie());
            ClearCommand = new RelayCommand(x => ClearFields());
            RemoveCommand = new RelayCommand(x => RemoveMovie(), x => CanRemoveMovie());
        }

        private bool CanRemoveMovie()
        {
            // Kan kun trykke "Remove", hvis der er valgt en film
            return SelectedMovie != null;
        }

        private void RemoveMovie()
        {
            if (SelectedMovie != null)
            {
                // Bruger property'en Movie fra MovieViewModel til at hente Movie-objektet som ligger bag MovieViewModellen
                var movie = SelectedMovie.Movie;

                // Fjerner filmen fra listen over film, fra repository og gemmer ændringerne i CSV-filen
                Movies.Remove(SelectedMovie);
                _movieRepository.RemoveMovie(movie);
                _movieRepository.SaveMoviesToCSV(_movieRepository.GetAllMovies());
            }
        }

        private bool CanAddMovie()
        {
            // Tjekker om alle værdier er udfyldt via hjælpemetode
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
            // Opretter et nyt Movie-objekt ud fra værdierne i MovieToAdd-instanset
            var newMovie = new Movie
            {
                Title = MovieToAdd.Title,
                Duration = MovieToAdd.Duration,
                Genre = MovieToAdd.Genre,
                Director = MovieToAdd.Director,
                PremiereDate = MovieToAdd.PremiereDate
            };

            _movieRepository.AddMovie(newMovie);

            Movies.Add(new MovieViewModel(newMovie));

            _movieRepository.SaveMoviesToCSV(_movieRepository.GetAllMovies());
        }

        private void ClearFields()
        {
            // Nulstiller værdierne i MovieToAdd
            MovieToAdd.Title = string.Empty;
            MovieToAdd.Duration = null;
            MovieToAdd.Genre = string.Empty;
            MovieToAdd.Director = string.Empty;
            MovieToAdd.PremiereDate = DateTime.MinValue;
        }
    }
}
