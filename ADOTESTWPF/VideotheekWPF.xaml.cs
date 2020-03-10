using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ADOGemeenschap;
using System.Data;

namespace ADOTESTWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VideotheekWPF : Window
    {
        private CollectionViewSource filmViewSource;
        public ObservableCollection<Film> filmsOb = new ObservableCollection<Film>();

        public List<Film> OudeFilms = new List<Film>();
        public List<Film> NieuweFilms = new List<Film>();
        public List<Film> GewijzigdeFilms = new List<Film>();

        public VideotheekWPF()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VuldeGrid();
            var genres = (from filmpje in filmsOb
                           orderby filmpje.Genre
                           select filmpje.Genre).Distinct().ToList();
            genres.Insert(0, "");
            comboBoxGenres.ItemsSource = genres;

            //alle textboxen op isEnabled false zetten
            
            titelTextBox.IsEnabled = false;
            comboBoxGenres.IsEnabled = false;
            inVoorraadTextBox.IsEnabled = false;
            uitVoorraadTextBox.IsEnabled = false;
            prijsTextBox.IsEnabled = false;
            totaalVerhuurdTextBox.IsEnabled = false;


        }
        private void VuldeGrid()
        {
            filmViewSource = (CollectionViewSource)(this.FindResource("filmViewSource"));
            // Load data by setting the CollectionViewSource.Source property:
            // filmViewSource.Source = [generic data source]
            var manager = new FilmManager();
            filmsOb = manager.GetFilms();
            filmViewSource.Source = filmsOb;
        }

        private void buttonToevoegen_Click(object sender, RoutedEventArgs e)
        {
            buttonToevoegen.Content = "Bevestigen";
            buttonVerwijderen.Content = "Annuleren";
            //andere knoppen
            buttonAllesOpslaan.IsEnabled = false;
            buttonVerhuur.IsEnabled = false;
            //alle textboxen actief
            
            titelTextBox.IsEnabled = true;
            comboBoxGenres.IsEnabled = true;
            inVoorraadTextBox.IsEnabled = true;
            uitVoorraadTextBox.IsEnabled = true;
            prijsTextBox.IsEnabled = true;
            totaalVerhuurdTextBox.IsEnabled = true;
            //inhoud van de boxen;
            bandNrTextBox.Text = "0";
            titelTextBox.Text = "";
            comboBoxGenres.SelectedIndex = 0;
            inVoorraadTextBox.Text = "0";
            uitVoorraadTextBox.Text = "0";
            prijsTextBox.Text = "0";
            totaalVerhuurdTextBox.Text = "0";
            //listbox
            listBoxTitels.IsEnabled = false;
        }
    }
}
