using System.Collections.ObjectModel;
using System.ComponentModel;
using TheMovies_LLD_.Models;
using TheMovies_LLD_.Repository;

public class Scheduling : INotifyPropertyChanged
{
    private BiografRepository _biografRepository;
    private MovieRepository _movieRepository;
    public ObservableCollection<Biograf> Biografer { get; }
    public ObservableCollection<Movie> Movies { get; }
    private ObservableCollection<Biografsal> _biografsale;
    private ObservableCollection<Spilletid> _spilletider;
    private Biograf _selectedBiograf;
    private Biografsal _selectedBiografsal;
    //private Spilletid _selectedSpilletid;

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
                //SelectedBiografsal = null; // Clear  selected Biografsal
                GetBiografsale();
                OnPropertyChanged(nameof(SelectedBiograf));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Scheduling()
    {
        _biografRepository = new BiografRepository();
        _movieRepository = new MovieRepository();
        Biografer = new ObservableCollection<Biograf>(_biografRepository.GetAllBiografer());
        Movies = new ObservableCollection<Movie>(_movieRepository.GetAllMovies());
        Spilletider = new ObservableCollection<Spilletid>();
        Biografsale = new ObservableCollection<Biografsal>();
    }

    private void GetBiografsale()
    {
        if (_selectedBiograf != null)
        {
            Biografsale = new ObservableCollection<Biografsal>(_selectedBiograf.Biografsale);
        }
        else
        {
            Biografsale.Clear();
        }
    }

    private void GetSpilletider()
    {
        if (_selectedBiografsal != null)
        {
            Spilletider = new ObservableCollection<Spilletid>(_selectedBiografsal.Spilletider);
        }
        else
        {
            Spilletider.Clear();
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}