using SustavZaLijecnike.DAL;
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
    /// Interaction logic for PacijentPromijeniZaporkuPage.xaml
    /// </summary>
    public partial class PacijentPromijeniLozinkuPage : Page
    {
        public PacijentPromijeniLozinkuPage()
        {
            InitializeComponent();
        }
        
        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("../Pages/PacijentUrediPodatkePage.xaml", UriKind.Relative));
        }

        private void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(pbNovaLozinka.Password) || string.IsNullOrEmpty(pbNovaLozinkaPotvrda.Password))
            {
                MessageBox.Show("Ispunite oba polja za lozinke!");
                return;
            }
            if (pbNovaLozinka.Password != pbNovaLozinkaPotvrda.Password)
            {
                MessageBox.Show("Lozinke nisu jednake!");
                return;
            }
            Pacijent pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;

            string salt = HashSalt.CreateSalt(32);
            string hash = HashSalt.GenerateHash(pbNovaLozinkaPotvrda.Password, salt);

            Kredencijal kredencijal = new Kredencijal(pacijent.Kredencijal.IDKredencijal, pacijent.Kredencijal.KorisnickoIme, hash, salt);

            Repository.UpdateKredencijal(kredencijal);

            NavigationService.Navigate(new Uri("../Pages/PacijentUrediPodatkePage.xaml", UriKind.Relative));
        }
    }
}
