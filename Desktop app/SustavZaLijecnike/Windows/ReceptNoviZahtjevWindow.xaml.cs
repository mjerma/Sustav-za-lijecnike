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
    /// Interaction logic for ReceptNoviZahtjevWindow.xaml
    /// </summary>
    public partial class ReceptNoviZahtjevWindow : Window
    {
        public ReceptNoviZahtjevWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Zaposlenik lijecnik = ((MainWindow)Application.Current.MainWindow).ambulanta.Lijecnik;

            cbPacijent.ItemsSource = Repository.GetPacijenti(lijecnik.IDZaposlenik);
            cbLijek.ItemsSource = Repository.GetLijekovi();
            cbDijagnoza.ItemsSource = Repository.GetDijagnoze();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSpremi_Click(object sender, RoutedEventArgs e)
        {
            if (cbDijagnoza.SelectedItem == null || cbLijek.SelectedItem == null || cbPacijent.SelectedItem == null)
            {
                MessageBox.Show("Ispunite podatke o pacijentu, dijagnozi i lijeku");
                return;
            }

            Pacijent pacijent = (Pacijent)cbPacijent.SelectedItem;
            Dijagnoza dijagnoza = (Dijagnoza)cbDijagnoza.SelectedItem;
            Lijek lijek = (Lijek)cbLijek.SelectedItem;
            Recept recept = new Recept
            {
                Pacijent = pacijent,
                Dijagnoza = dijagnoza,
                Lijek = lijek,
                Napomena = tbNapomena.Text,
                Ponavljajuci = chbPonavljajuci.IsChecked.Value,
                Odobren = false
            };
            Repository.AddRecept(recept);

            DialogResult = true;
        }
    }
}
