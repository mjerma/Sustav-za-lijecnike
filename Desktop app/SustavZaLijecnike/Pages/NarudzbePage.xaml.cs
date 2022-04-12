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
    /// Interaction logic for NarudzbePage.xaml
    /// </summary>
    public partial class NarudzbePage : Page
    {
        private IEnumerable<Recept> narudzbe;
        public NarudzbePage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Zaposlenik lijecnik = ((MainWindow)Application.Current.MainWindow).ambulanta.Lijecnik;
            Zaposlenik currentUser = ((MainWindow)Application.Current.MainWindow).currentUser;

            if (currentUser.TipZaposlenikaID == 1)
            {
                btnNoviZahtjev.Visibility = Visibility.Collapsed;
            }
            narudzbe = Repository.GetReceptiNarudzbe(lijecnik.IDZaposlenik);
            dgNarudzbe.ItemsSource = narudzbe;
        }

        private void btnNoviZahtjev_Click(object sender, RoutedEventArgs e)
        {
            ReceptNoviZahtjevWindow receptZahtjevWindow = new ReceptNoviZahtjevWindow();
            if (receptZahtjevWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text != "")
            {
                var filtered = narudzbe.Where(x => x.Pacijent.ToString().ToLower().Contains(textBox.Text.ToLower()));
                dgNarudzbe.ItemsSource = filtered;
            }
            else
            {
                dgNarudzbe.ItemsSource = narudzbe;
            }
        }
    }
}
