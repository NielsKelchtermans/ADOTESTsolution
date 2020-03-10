using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADOTESTWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VideotheekWPF : Window
    {
        public VideotheekWPF()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource filmViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("filmViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // filmViewSource.Source = [generic data source]
        }
    }
}
