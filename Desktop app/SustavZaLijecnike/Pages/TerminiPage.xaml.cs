using SustavZaLijecnike.Windows;
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

namespace SustavZaLijecnike.Pages
{
    /// <summary>
    /// Interaction logic for TerminiPage.xaml
    /// </summary>
    public partial class TerminiPage : Page
    {
        private IEnumerable<Termin> termini;
        private IEnumerable<Termin> terminiFiltered;
        public TerminiPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Ambulanta ambulanta = ((MainWindow)Application.Current.MainWindow).ambulanta;
            termini = Repository.GetTerminiLijecnika(ambulanta.Lijecnik.IDZaposlenik);
            terminiFiltered = termini.Where(x => x.Datum.Date == DateTime.Today.Date);
            dpDatum.SelectedDate = DateTime.Today.Date;
            DataGridTermini.ItemsSource = terminiFiltered;
        }

        private void btnZakaziTermin_Click(object sender, RoutedEventArgs e)
        {
            ZakaziTerminWindow terminWindow = new ZakaziTerminWindow();
            if (terminWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text != "")
            {
                var filtered = terminiFiltered.Where(x => x.Pacijent.ToString().ToLower().Contains(textBox.Text.ToLower()));
                DataGridTermini.ItemsSource = filtered;
            }
            else
            {
                DataGridTermini.ItemsSource = terminiFiltered;
            }
        }

        private void dpDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datepicker = (DatePicker)sender;
            terminiFiltered = termini.Where(x => x.Datum.Date == datepicker.SelectedDate);
            DataGridTermini.ItemsSource = null;
            DataGridTermini.ItemsSource = terminiFiltered;
        }
    }
}
