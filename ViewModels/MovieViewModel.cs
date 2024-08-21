using System.ComponentModel;
using System.IO;
using TheMovies_LLD_.Models;

namespace TheMovies_LLD_.ViewModels
{
    // MovieViewModel-Klassen:
    // Indkapsler egenskaberne for en enkelt film.
    // Giver mulighed for at interagere med den underliggende films data.
    // Håndterer en films egenskabsændringer og opdaterer dem i UI.

    public class MovieViewModel : INotifyPropertyChanged
    {
        private Movie _movie;
        public string Summary => _movie.Summary;

        // Property til at hente film-objektet bag ved MovieViewModellen.
        public Movie Movie => _movie;
        
        public string Title
        {
            get { return _movie.Title; }
            set
            {
                if (_movie.Title != value)
                {
                    _movie.Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public TimeSpan Duration
        {
            get { return _movie.Duration; }
            set
            {
                if (_movie.Duration != value)
                {
                    _movie.Duration = value;
                    OnPropertyChanged(nameof(Duration));
                }  
            }
        }
  
        public string Genre
        {
            get { return _movie.Genre; }
            set
            {
                if (_movie.Genre != value)
                {
                    _movie.Genre = value;
                    OnPropertyChanged(nameof(Genre));
                }
            }
        }

        public string Director
        {
            get { return _movie.Director; }
            set
            {
                if (_movie.Director != value)
                {
                    _movie.Director = value;
                    OnPropertyChanged(nameof(Director));
                }
            }
        }

        public DateTime PremiereDate
        {
            get { return _movie.PremiereDate; }
            set
            {
                if (_movie.PremiereDate != value)
                {
                    _movie.PremiereDate = value;
                    OnPropertyChanged(nameof(PremiereDate));
                }
            }
        }

        public MovieViewModel(Movie movie)
        {
            _movie = movie ?? throw new ArgumentNullException(nameof(movie));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}