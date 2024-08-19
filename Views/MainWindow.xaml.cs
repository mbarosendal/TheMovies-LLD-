using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheMovies_LLD_.ViewModel;

namespace TheMovies_LLD_.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new FrontPage();
        }

        private void btn_Movies_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MoviesPage();
        }

        private void btn_FrontPage_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new FrontPage();
        }

        private void btn_Schedule_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new SchedulePage();
        }
    }
}