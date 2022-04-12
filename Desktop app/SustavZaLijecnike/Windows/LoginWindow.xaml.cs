using SustavZaLijecnike.DAL;
using System.Collections.Generic;
using System.Windows;

namespace SustavZaLijecnike.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public Zaposlenik zaposlenik;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnPrijava_Click(object sender, RoutedEventArgs e)
        {
            string salt = HashSalt.CreateSalt(32);
            string hash = HashSalt.GenerateHash(pbLozinka.Password, salt);
            if (string.IsNullOrEmpty(tbKorisnickoIme.Text) || string.IsNullOrEmpty(pbLozinka.Password))
            {
                MessageBox.Show("Unesite korisničko ime i lozinku!");
                return;
            }

            Kredencijal kredencijal = Repository.GetKredencijali(tbKorisnickoIme.Text);
            if (kredencijal != null && HashSalt.IsEqual(pbLozinka.Password, kredencijal.Hash, kredencijal.Salt))
            {
                zaposlenik = Repository.GetZaposlenik(kredencijal.IDKredencijal);
                if (zaposlenik != null)
                {
                    DialogResult = true;
                }
            }
            else
            {
                MessageBox.Show("Kombinacija korisničkog imena i lozinke je neispravna!");
                return;
            }
        }
    }
}
