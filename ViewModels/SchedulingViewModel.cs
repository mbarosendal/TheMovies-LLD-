using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TheMovies_LLD_.Commands;
using TheMovies_LLD_.Models;
using TheMovies_LLD_.Repository;

// SchedulingViewModel-klassen:
// Behandler data fra forskellige model-klasser (Biograf, Movie, Forestilling, Biografsal og Spilletid)
// Står for at tilføje en forestilling til datalaget (ForestillingRepository) og vise forestillinger i UI

namespace ViewModels.SchedulingViewModel.cs
{
    public class SchedulingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private BiografRepository _biografRepository;
        private MovieRepository _movieRepository;
        private ForestillingRepository _forestillingRepository;
        public ObservableCollection<Biograf> Biografer { get; }
        private Biograf _selectedBiograf;
        public ObservableCollection<Movie> Movies { get; }
        private Movie _selectedMovie;
        private ObservableCollection<Biografsal> _biografsale;
        private Biografsal _selectedBiografsal;
        private ObservableCollection<Spilletid> _spilletider;
        private Spilletid _selectedSpilletid;
        public ObservableCollection<Forestilling> Forestillinger { get; set; }
        public ICommand AddForestillingCommand { get; set; }

        public ObservableCollection<Spilletid> Spilletider
        {
            get { return _spilletider; }
            set
            {
                _spilletider = value;
                OnPropertyChanged(nameof(Spilletider));
            }
        }

        public ObservableCollection<Biografsal> Biografsale
        {
            get => _biografsale;
            set
            {
                _biografsale = value;
                OnPropertyChanged(nameof(Biografsale));
            }
        }

        public Biografsal SelectedBiografsal
        {
            get { return _selectedBiografsal; }
            set
            {
                _selectedBiografsal = value;
                // Fyld spilletiderne ud når en biografsal vælges
                GetSpilletider();
                OnPropertyChanged(nameof(SelectedBiografsal));
            }
        }

        public Biograf SelectedBiograf
        {
            get => _selectedBiograf;
            set
            {
                if (_selectedBiograf != value)
                {
                    _selectedBiograf = value;
                    // Fyld biografsale ud når en biograf vælges
                    GetBiografsale();
                    OnPropertyChanged(nameof(SelectedBiograf));
                }
            }
        }

        public Spilletid SelectedSpilletid
        {
            get { return _selectedSpilletid; }
            set
            {
                _selectedSpilletid = value;
            }
        }

        public Movie SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }

        public SchedulingViewModel()
        {
            _biografRepository = new BiografRepository();
            _movieRepository = new MovieRepository();
            _movieRepository.LoadMoviesfromCSV();
            _forestillingRepository = new ForestillingRepository();
            Biografer = new ObservableCollection<Biograf>(_biografRepository.GetAllBiografer());
            Movies = new ObservableCollection<Movie>(_movieRepository.GetAllMovies());
            Forestillinger = new ObservableCollection<Forestilling>(_forestillingRepository.GetAllForestillinger());
            Spilletider = new ObservableCollection<Spilletid>();
            Biografsale = new ObservableCollection<Biografsal>();
            AddForestillingCommand = new RelayCommand(x => AddForestilling(), x => CanAddForestilling());
        }

        private bool CanAddForestilling()
        {
            // Tjekker om alle værdier er valgt i UI før der kan tilføjes en forestilling
            if (SelectedBiograf != null &&
                SelectedBiografsal != null &&
                SelectedMovie != null &&
                SelectedBiografsal.Spilletider.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Tilføjer en forestilling til listen og til databasen ud fra de valgte værdier
        // og tjekker for overlappende forestillinger via hjælpemetoden AreForestillingerOverlapping()
        private void AddForestilling()
        {
            DateTime forestillingStartTime = SelectedSpilletid.StartTid;
            TimeSpan movieDuration = SelectedMovie.Duration;
            DateTime calculatedForestillingEndTime = CalculateEndTimeWithCleaningAndCommercials(forestillingStartTime, movieDuration);

            var newForestilling = new Forestilling
            {
                Biograf = SelectedBiograf.Biografkæde,
                By = SelectedBiograf.By,
                Biografsal = SelectedBiografsal.Id,
                Dag = SelectedSpilletid.Dag.ToString(),
                Starttid = SelectedSpilletid.StartTid,
                Sluttid = calculatedForestillingEndTime,
                Movie = SelectedMovie
            };

            // Check for overlapping forestilling
            if (_forestillingRepository.AreForestillingerOverlapping(newForestilling, forestillingStartTime, calculatedForestillingEndTime))
            {
                MessageBox.Show("Fejl: Forestilling overlapper med en anden forestilling.");
                return;
            }

            _forestillingRepository.AddForestilling(newForestilling);

            Forestillinger.Add(newForestilling);
        }

        private DateTime CalculateEndTimeWithCleaningAndCommercials(DateTime startTime, TimeSpan playTime)
        {
            TimeSpan cleaningAndCommercialsTime = new TimeSpan(0, 30, 0);

            // Tilføj filmens playtime og spilletid til rengøring og reklamer til starttidspunktet for at finde sluttidspunktet for forestillingen
            DateTime endTime = startTime.Add(playTime).Add(cleaningAndCommercialsTime);

            return endTime;
        }

        private void GetBiografsale()
        {
            if (_selectedBiograf != null)
            {
                // Fyld biografsale ud når en biograf vælges
                Biografsale = new ObservableCollection<Biografsal>(SelectedBiograf.Biografsale);
            }
            else
            {
                // Clear biografsale hvis der ikke er valgt en biograf
                Biografsale.Clear();
            }
        }

        private void GetSpilletider()
        {
            if (_selectedBiografsal != null)
            {
                // Fyld spilletiderne ud når en biografsal vælges
                Spilletider = new ObservableCollection<Spilletid>(SelectedBiografsal.Spilletider);
            }
            else
            {
                // Clear spilletider hvis der ikke er valgt en biografsal
                Spilletider.Clear();
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}