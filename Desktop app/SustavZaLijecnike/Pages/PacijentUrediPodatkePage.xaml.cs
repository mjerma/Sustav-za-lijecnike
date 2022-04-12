using SustavZaLijecnike.DAL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SustavZaLijecnike.Pages
{
    /// <summary>
    /// Interaction logic for PacijentUrediPodatkePage.xaml
    /// </summary>
    public partial class PacijentUrediPodatkePage : Page
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Ulica { get; set; }
        public string Grad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string MaticniBroj { get; set; }

        private Pacijent pacijent;
        public PacijentUrediPodatkePage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;
            tbIme.Text = pacijent.Ime;
            tbPrezime.Text = pacijent.Prezime;
            dpDatumRodenja.SelectedDate = pacijent.DatumRodenja.Date;
            tbUlica.Text = pacijent.Prebivaliste.UlicaBroj;
            tbGrad.Text = pacijent.Prebivaliste.Grad;
            tbEmail.Text = pacijent.Email;
            tbTelefon.Text = pacijent.Telefon;
            tbMaticniBroj.Text = pacijent.MBO;

            cbSpol.ItemsSource = Enum.GetNames(typeof(Spol));
            cbStatus.ItemsSource = Enum.GetNames(typeof(Status));

            cbLijecnik.SelectedValuePath = "IDZaposlenik";
            cbLijecnik.ItemsSource = Repository.GetLijecnici();

            cbSpol.SelectedValue = ((Spol)pacijent.SpolID).ToString();
            cbStatus.SelectedValue = ((Status)Convert.ToInt32(pacijent.Aktivan)).ToString();
            cbLijecnik.SelectedValue = pacijent.Lijecnik.IDZaposlenik;
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void btnSpremi_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textBox = { tbIme, tbPrezime, tbUlica, tbGrad, tbTelefon, tbEmail, tbMaticniBroj };

            foreach (TextBox txt in textBox)
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    MessageBox.Show("Unesite podatke u sva polja!");
                    return;
                }
            }
            if (cbSpol.SelectedItem == null || dpDatumRodenja.SelectedDate == null)
            {
                MessageBox.Show("Unesite podatke u sva polja!");
                return;
            }
            UpdatePacijent();

            Window window = Window.GetWindow(this);
            window.DialogResult = true;
        }

        private void UpdatePacijent()
        {
            Prebivaliste prebivaliste = new Prebivaliste
            {
                IDPrebivaliste = pacijent.Prebivaliste.IDPrebivaliste,
                UlicaBroj = tbUlica.Text,
                Grad = tbGrad.Text
            };

            Repository.UpdatePacijent(new Pacijent
            {
                IDPacijent = pacijent.IDPacijent,
                Ime = tbIme.Text,
                Prezime = tbPrezime.Text,
                SpolID = (int)Enum.Parse<Spol>(cbSpol.Text),
                DatumRodenja = dpDatumRodenja.DisplayDate,
                DatumUpisa = pacijent.DatumUpisa,
                Prebivaliste = prebivaliste,
                Email = tbEmail.Text,
                Telefon = tbTelefon.Text,
                MBO = tbMaticniBroj.Text,
                Aktivan = Convert.ToBoolean((int)Enum.Parse<Status>(cbStatus.Text)),
                Lijecnik = pacijent.Lijecnik
            });
        }

        private void btnPromijeniLozinku_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("../Pages/PacijentPromijeniLozinkuPage.xaml", UriKind.Relative));
        }
    }
}
