using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TheMovies_LLD_.Commands;
using TheMovies_LLD_.Models;
using TheMovies_LLD_.Repository;

public class Scheduling : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private BiografRepository _biografRepository;
    private MovieRepository _movieRepository;
    private ForestillingRepository _forestillingRepository;

    // Biografer og film skal ikke kunne ændres, så de er readonly
    public ObservableCollection<Biograf> Biografer { get; }
    public ObservableCollection<Movie> Movies { get; }
    private ObservableCollection<Biografsal> _biografsale;
    private ObservableCollection<Spilletid> _spilletider;
    public ObservableCollection<Forestilling> Forestillinger { get; set; }

    private Biograf _selectedBiograf;
    private Biografsal _selectedBiografsal;
    private Movie _selectedMovie;
    private Spilletid _selectedSpilletid;

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

    public Scheduling()
    {
        _biografRepository = new BiografRepository();
        _movieRepository = new MovieRepository();
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
        // Tjekker om alle værdier er valgt før der kan tilføjes en forestilling
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

    // tilføjer en forestilling til listen og til databasen ud fra valgte værdier
    private void AddForestilling()
    {
        DateTime startTime = SelectedSpilletid.TimeOfDay;
        TimeSpan playTime = SelectedMovie.Duration;
        string endTime = CalculateEndTimeWithCleaningAndCommercials(startTime, playTime).ToString("HH:mm");

        var newForestilling = new Forestilling
        {
            Biograf = SelectedBiograf.Biografkæde,
            By = SelectedBiograf.By,
            Biografsal = SelectedBiografsal.ID,
            Dag = SelectedSpilletid.DayOfWeek.ToString(),
            Klokken = SelectedSpilletid.TimeOfDay.ToString("HH:mm"),
            Sluttid = endTime,
            Movie = SelectedMovie.Title
        };

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