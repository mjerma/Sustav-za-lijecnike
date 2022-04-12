using SustavZaLijecnike.DAL;
using SustavZaLijecnike.Model;
using SustavZaLijecnike.Model.Enum;
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
    /// Interaction logic for DodajPacijenta.xaml
    /// </summary>
    public partial class NoviPacijentWindow : Window
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Ulica { get; set; }
        public string Grad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string MaticniBroj { get; set; }

        public NoviPacijentWindow()
        {
            InitializeComponent();
            FillComboBox();
        }

        private void FillComboBox()
        {
            cbSpol.ItemsSource = Enum.GetNames(typeof(Spol));
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textBox = { tbIme, tbPrezime, tbUlica, tbGrad, tbTelefon, tbEmail, tbKorisnickoIme, tbMaticniBroj };

            foreach (TextBox txt in textBox)
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    MessageBox.Show("Unesite podatke u sva polja!");
                    return;
                }
            }
            if (string.IsNullOrWhiteSpace(pbLozinka.Password) || cbSpol.SelectedItem == null || dpDatumRodenja.SelectedDate == null)
            {
                MessageBox.Show("Unesite podatke u sva polja!");
                return;
            }

            SavePacijent();
        }

        private void SavePacijent()
        {
            Zaposlenik lijecnik = ((MainWindow)Application.Current.MainWindow).ambulanta.Lijecnik;

            Prebivaliste prebivaliste = new Prebivaliste()
            {
                UlicaBroj = tbUlica.Text,
                Grad = tbGrad.Text
            };

            string salt = HashSalt.CreateSalt(32);
            string hash = HashSalt.GenerateHash(pbLozinka.Password, salt);

            Kredencijal kredencijal = new Kredencijal()
            {
                KorisnickoIme = tbKorisnickoIme.Text,
                Salt = salt,
                Hash = hash
            };

            Pacijent pacijent = new Pacijent()
            {
                Ime = tbIme.Text,
                Prezime = tbPrezime.Text,
                DatumRodenja = dpDatumRodenja.SelectedDate.Value,
                SpolID = (int)Enum.Parse<Spol>(cbSpol.Text),
                Prebivaliste = prebivaliste,
                Telefon = tbTelefon.Text,
                Email = tbEmail.Text,
                Kredencijal = kredencijal,
                MBO = tbMaticniBroj.Text,
                Lijecnik = lijecnik
            };

            Repository.AddPacijent(pacijent);

            DialogResult = true;
        }
    }
}
