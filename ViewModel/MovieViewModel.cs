using System.ComponentModel;
using TheMovies_LLD_.Models;

namespace TheMovies_LLD_.ViewModel
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        private Movie _movie;
        public string Summary => $"{Title} ({Duration}m, {Genre})";

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

        public int? Duration
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