using SustavZaLijecnike.DAL;
using SustavZaLijecnike.Model.Enum;
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
    /// Interaction logic for PacijentPodatciPage.xaml
    /// </summary>
    public partial class PacijentPodatciPage : Page
    {
        private Pacijent pacijent;
        public PacijentPodatciPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;

            tbDatumRodenja.Text = pacijent.DatumRodenja.Date.ToShortDateString();
            tbSpol.Text = ((Spol)pacijent.SpolID).ToString();
            tbAdresa.Text = pacijent.Prebivaliste.UlicaBroj;
            tbGrad.Text = pacijent.Prebivaliste.Grad;
            tbTelefon.Text = pacijent.Telefon;
            tbEmail.Text = pacijent.Email;
            tbMaticniBroj.Text = pacijent.MBO;
        }

        private void btnUrediPodatke_Click(object sender, RoutedEventArgs e)
        {
            PacijentUrediPodatkeWindow window = new PacijentUrediPodatkeWindow();
            if (window.ShowDialog() == true)
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.tempPacijent = Repository.GetPacijentByID(pacijent.IDPacijent);
                mainWindow.MainFrame.NavigationService.Refresh();
                LoadData();
            }
        }
    }
}
