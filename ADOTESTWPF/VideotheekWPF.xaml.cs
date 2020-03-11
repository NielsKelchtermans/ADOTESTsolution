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

        public List<Film> TeVerwijderenFilms = new List<Film>();
        public List<Film> NieuweFilms = new List<Film>();
        public List<Film> GewijzigdeFilms = new List<Film>();
        public bool foutGevonden;

        public VideotheekWPF()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VuldeGrid();


        }
        private void VuldeGrid()
        {
            filmViewSource = (CollectionViewSource)(this.FindResource("filmViewSource"));
            var manager = new FilmManager();
            filmsOb = manager.GetFilms();
            filmViewSource.Source = filmsOb;
            var genres = (from filmpje in filmsOb
                          orderby filmpje.Genre
                          select filmpje.Genre).Distinct().ToList();
            genres.Insert(0, "");
            comboBoxGenres.ItemsSource = genres;
            buttonAllesOpslaan.IsEnabled = true;
            buttonVerhuur.IsEnabled = true;
            buttonToevoegen.Content = "Toevoegen";
            buttonVerwijderen.Content = "Verwijderen";
            listBoxTitels.SelectedIndex = 0;
            //scrollview
            listBoxTitels.ScrollIntoView(listBoxTitels.SelectedItem);


        }
        public void VuldeGridLokaal()
        {
            var genres = (from filmpje in filmsOb
                          orderby filmpje.Genre
                          select filmpje.Genre).Distinct().ToList();
            genres.Insert(0, "");
            comboBoxGenres.ItemsSource = genres;
            buttonAllesOpslaan.IsEnabled = true;
            buttonVerhuur.IsEnabled = true;
            buttonToevoegen.Content = "Toevoegen";
            buttonVerwijderen.Content = "Verwijderen";
            listBoxTitels.SelectedIndex = 0;

            //binding vinden
            Binding binding2 = BindingOperations.GetBinding(inVoorraadTextBox, TextBox.TextProperty);
            //verwijder validationRules
            binding2.ValidationRules.Clear();
            //toevoegen nieuwe validionRule:
            binding2.ValidationRules.Add(new InVoorraadValidation());
            //scrollview
            listBoxTitels.ScrollIntoView(listBoxTitels.SelectedItem);
        }

        private void buttonToevoegen_Click(object sender, RoutedEventArgs e)
        {
            
            if (buttonToevoegen.Content.ToString() == "Toevoegen")
            {

                //een nieuwe filmobject aanmaken en toevoegen aan listbox(op achtergrond)
                Film eenNieuwFilm = new Film();
                eenNieuwFilm.BandNr = 0;
                eenNieuwFilm.Titel = "";
                eenNieuwFilm.Genre = "";
                eenNieuwFilm.InVoorraad = 0;
                eenNieuwFilm.UitVoorraad = 0;
                eenNieuwFilm.Prijs = 0;
                eenNieuwFilm.TotaalVerhuurd = 0;
                filmsOb.Add(eenNieuwFilm);
                listBoxTitels.SelectedItem = eenNieuwFilm;
                buttonToevoegen.Content = "Bevestigen";
                buttonVerwijderen.Content = "Annuleren";
                //andere knoppen
                buttonAllesOpslaan.IsEnabled = false;
                buttonVerhuur.IsEnabled = false;
                
                
                titelTextBox.Text = "";
                comboBoxGenres.SelectedValue = "";
                inVoorraadTextBox.Text = "0";
                uitVoorraadTextBox.Text = "0";
                prijsTextBox.Text = "0";
                totaalVerhuurdTextBox.Text = "0";
                //listbox
                //listBoxTitels.IsEnabled = false;
            }
            else
            {
                foutGevonden = false;
                foreach (var  kind in gridDetail.Children)
                {
                    if (Validation.GetHasError((DependencyObject)kind))
                    {
                        foutGevonden = true;
                    }
                }
                if (foutGevonden)
                {
                    MessageBox.Show("Er is nog iets niet in orde!");
                }
                else
                {
                    Film eenNieuweFilm = filmsOb[filmsOb.Count - 1];
                    var manager = new FilmManager();

                    Int32 genreNrAanvullen = manager.GeeftGenreNr(eenNieuweFilm.Genre);
                    eenNieuweFilm.GenreNr = genreNrAanvullen;
                    NieuweFilms.Add(eenNieuweFilm);
                    //filmsOb.Clear();
                    VuldeGridLokaal();
                    //MessageBox.Show(eenNieuweFilm.Titel + NieuweFilms[0].Genre + NieuweFilms[0].GenreNr);
                    
                }
            }
            
            

        }

        private void buttonVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (buttonVerwijderen.Content.ToString()=="Verwijderen")
            {
                if ((MessageBox.Show("Ben je zeker dat je deze film wil verwijderen?", 
                    "Verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No)==MessageBoxResult.Yes))
                {
                    string teVerwijderenTitel = titelTextBox.Text;
                    foreach (Film eenFilm in filmsOb)
                    {
                        if (teVerwijderenTitel == eenFilm.Titel)
                        {
                            filmsOb.Remove(eenFilm);
                            TeVerwijderenFilms.Add(eenFilm);
                            //MessageBox.Show(TeVerwijderenFilms.Count.ToString());
                            break;
                        }
                    }
                    VuldeGridLokaal();
                }  

            }
            else
            {
                Film localTeVerwijderenFilm = filmsOb[filmsOb.Count - 1];
                filmsOb.Remove(localTeVerwijderenFilm);
                VuldeGridLokaal();
            }
        }

        private void buttonVerhuur_Click(object sender, RoutedEventArgs e)
        {
            var InVoorraad = Convert.ToInt32(inVoorraadTextBox.Text.ToString());
            var UitVoorraad = Convert.ToInt32(uitVoorraadTextBox.Text.ToString());
            var TotaalVerhuurd = Convert.ToInt32(totaalVerhuurdTextBox.Text.ToString());
            if (bandNrTextBox.Text.ToString()=="0")
            {
                foreach (Film eenFilmNieuw in filmsOb)
                {
                    if (eenFilmNieuw.Titel == titelTextBox.Text.ToString())
                    {
                        eenFilmNieuw.Changed = true;
                        break;
                    }
                }
                
            }
            if ( InVoorraad> 1)
            {
                inVoorraadTextBox.Text = (InVoorraad - 1).ToString();
                uitVoorraadTextBox.Text = (UitVoorraad + 1).ToString();
                totaalVerhuurdTextBox.Text = (TotaalVerhuurd + 1).ToString();
            }
            else if (InVoorraad == 1)
            {

                //binding vinden
                Binding binding1 = BindingOperations.GetBinding(inVoorraadTextBox, TextBox.TextProperty);
                //verwijder validationRules
                binding1.ValidationRules.Clear();
                //toevoegen nieuwe validionRule:
                binding1.ValidationRules.Add(new InVoorraadValidation2());

                inVoorraadTextBox.Text = "0";
                uitVoorraadTextBox.Text = (UitVoorraad + 1).ToString();
                totaalVerhuurdTextBox.Text = (TotaalVerhuurd + 1).ToString();
            }
            else
            {
                MessageBox.Show("Alle films zijn verhuurd!", "TotaalVerhuurd", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void buttonAllesOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Wilt u alles wegschrijven naar de database?",
                    "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes))
            {

                var manager = new FilmManager();
                if (NieuweFilms.Count() != 0)
                {
                    try
                    {
                        manager.SchrijfToevoegingen(NieuweFilms);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                }
                if (TeVerwijderenFilms.Count() !=0)
                {
                    try
                    {
                        manager.SchrijfVerwijderingen(TeVerwijderenFilms);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
                //de aanpassingen
                foreach (Film f in filmsOb)
                {
                    if ((f.Changed == true))
                    {
                        GewijzigdeFilms.Add(f);
                        f.Changed = false;
                    }
                }
                if (GewijzigdeFilms.Count()!=0)
                {
                    try
                    {
                        manager.Schrijfwijzigingen(GewijzigdeFilms);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        
                    }
                }
                TeVerwijderenFilms.Clear();
                NieuweFilms.Clear();
                GewijzigdeFilms.Clear();
                VuldeGrid(); 

            }
        }






        
    }
}
