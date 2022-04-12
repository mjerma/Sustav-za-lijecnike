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
using System.Windows.Shapes;

namespace SustavZaLijecnike.Windows
{
    /// <summary>
    /// Interaction logic for ZakaziTerminWindow.xaml
    /// </summary>
    public partial class ZakaziTerminWindow : Window
    {
        Zaposlenik lijecnik;
        IEnumerable<Termin> termini;
        List<string> predefiniraniTermini;
        public ZakaziTerminWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            lijecnik = ((MainWindow)Application.Current.MainWindow).ambulanta.Lijecnik;
            termini = Repository.GetTerminiLijecnika(lijecnik.IDZaposlenik);
            predefiniraniTermini = Repository.GetPredefiniraniTermini().ToList();

            cbPacijent.ItemsSource = Repository.GetPacijenti(lijecnik.IDZaposlenik);
            cbVrijeme.ItemsSource = predefiniraniTermini;
            cbVrijeme.IsEnabled = false;
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnSpremi_Click(object sender, RoutedEventArgs e)
        {
            if (cbPacijent.SelectedItem == null || dpDatum.SelectedDate == null || cbVrijeme.SelectedItem == null)
            {
                MessageBox.Show("Ispunite podatke o pacijentu, datumu i vremenu termina");
                return;
            }

            Termin termin = new Termin
            {
                Pacijent = (Pacijent)cbPacijent.SelectedItem,
                Lijecnik = lijecnik,
                Datum = dpDatum.SelectedDate.Value,
                Vrijeme = cbVrijeme.SelectedItem.ToString(),
                Napomena = tbNapomena.Text
            };
            Repository.AddTermin(termin);

            DialogResult = true;
        }

        private void dpDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var filteredTermini = termini.Where(x => x.Datum == dpDatum.SelectedDate);
            List<string> slobodniTermini = predefiniraniTermini.ToList();
            foreach (var item in filteredTermini)
            {
                slobodniTermini.RemoveAll(x => x.Contains(item.Vrijeme));
            }
            cbVrijeme.ItemsSource = slobodniTermini;
            cbVrijeme.IsEnabled = true;
        }
    }
}
