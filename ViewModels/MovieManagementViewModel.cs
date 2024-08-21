using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TheMovies_LLD_.Commands;
using TheMovies_LLD_.Models;
using TheMovies_LLD_.Repository;

namespace TheMovies_LLD_.ViewModels
{
    // MainViewModel-klassen:
    // Håndterer de handlinger, der udføres på film-kollektionen i UI, såsom at tilføje, fjerne og rydde dem.
    // KOnverterer og behandler input fra UI til lagring af film i datalaget (MovieRepository) og viser de gemte film til UI.
    // Håndterer meddelelser om egenskabsændringer for den overordnede applikationstilstand.
    // Delegerer den specifikke filmrelaterede logik til MovieViewModel.

    public class MovieManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly MovieRepository _movieRepository;
        public ObservableCollection<MovieViewModel> Movies { get; }        
        public MovieViewModel MovieToAdd { get; set; }
        public MovieViewModel SelectedMovie { get; set; }      
        private int _selectedHours;
        private int _selectedMinutes;
        private TimeSpan _duration;
        public List<int> Hours => Enumerable.Range(0, 10).ToList();
        public List<int> Minutes => Enumerable.Range(0, 60).ToList();
        public ICommand AddCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public TimeSpan Duration
        {
            get => _duration;
            private set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        public int SelectedMinutes
        {
            get { return _selectedMinutes; }
            set 
            { 
                _selectedMinutes = value;
                OnPropertyChanged(nameof(SelectedMinutes));
                UpdateDuration();
            }
        }

        public int SelectedHours
        {
            get { return _selectedHours; }
            set 
            { 
                _selectedHours = value;
                OnPropertyChanged(nameof(SelectedHours));
                UpdateDuration();
            }
        }

        public MovieManagementViewModel()
        {
            _movieRepository = new MovieRepository();
            // Movies indeholder alle film-objekter fra MovieRepository konverteret til MovieViewModels-objekter (LINQ), så de kan vises i UI
            _movieRepository.LoadMoviesfromCSV();
            Movies = new ObservableCollection<MovieViewModel>(_movieRepository.GetAllMovies()
                                                .Select(movie => new MovieViewModel(movie)));
            MovieToAdd = new MovieViewModel(new Movie());
            AddCommand = new RelayCommand(x => AddMovie(), x => CanAddMovie());
            ClearCommand = new RelayCommand(x => ClearFields());
            RemoveCommand = new RelayCommand(x => RemoveMovie(), x => CanRemoveMovie());
        }

        // Metoder til at opdatere varigheden af den nye film ud fra valgte værdier for timer og minutter
        private void UpdateDuration()
        {
            MovieToAdd.Duration = new TimeSpan(SelectedHours, SelectedMinutes, 0);
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
                // Henter Movie-objektet som ligger bag MovieViewModellen fordi SelectedMovie er et MovieViewModel-objekt og repository.RemoveMovie() bruger Movie-objekter
                var movie = SelectedMovie.Movie;

                // Fjerner filmen fra listen over film, fra repository og gemmer ændringerne i CSV-filen
                Movies.Remove(SelectedMovie);
                _movieRepository.RemoveMovie(movie);
                _movieRepository.SaveMoviesToCSV(_movieRepository.GetAllMovies());
            }
        }

        private bool CanAddMovie()
        {
            // Kan kun trykke "Add", hvis alle værdier er udfyldt. Det tjekkes i metoden HasAllValues()
            return HasAllValues();
        }

        private bool HasAllValues()
        {
            if (MovieToAdd.Title != null &&
                MovieToAdd.Duration != null &&  
                MovieToAdd.Genre != null &&
                MovieToAdd.Director != null)
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
            //MovieToAdd.Duration = TimeSpan.Zero;
            MovieToAdd.Genre = string.Empty;
            MovieToAdd.Director = string.Empty;
            MovieToAdd.PremiereDate = DateTime.MinValue;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
