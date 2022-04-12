using SustavZaLijecnike.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SustavZaLijecnike.DAL;

namespace SustavZaLijecnike.Pages
{
    /// <summary>
    /// Interaction logic for PacijentPage.xaml
    /// </summary>
    public partial class PacijentPage : Page
    {
        public PacijentPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Pacijent pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;
            Zaposlenik currentUser = ((MainWindow)Application.Current.MainWindow).currentUser;

            tbImePrezime.Text = pacijent.ToString();
            if (currentUser.TipZaposlenikaID == 2)
            {
                menuUnesi.Visibility = Visibility.Collapsed;
            }

            if (!pacijent.Aktivan)
            {
                tbStatus.Text = "Odjavljen";
                tbStatus.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (((Spol)pacijent.SpolID).ToString() == "Ženski")
            {
                pacijentImage.Source = new BitmapImage(new Uri("/Images/female.png", UriKind.Relative));
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (PacijentFrame == null) return;
            string rbContent = radioButton.Content.ToString();

            switch (rbContent)
            {
                case "Podatci":
                    PacijentFrame.Navigate(new Uri("Pages/PacijentPodatciPage.xaml", UriKind.Relative));
                    break;
                case "Pregledi":
                    PacijentFrame.Navigate(new Uri("Pages/PacijentPreglediPage.xaml", UriKind.Relative));
                    break;
                case "Terapija":
                    PacijentFrame.Navigate(new Uri("Pages/PacijentTerapijaPage.xaml", UriKind.Relative));
                    break;
                case "Cijepljenje":
                    PacijentFrame.Navigate(new Uri("Pages/PacijentCijepljenjePage.xaml", UriKind.Relative));
                    break;
                case "Uputnice":
                    PacijentFrame.Navigate(new Uri("Pages/PacijentUputnicePage.xaml", UriKind.Relative));
                    break;
            }
        }

        private void miPregled_Click(object sender, RoutedEventArgs e)
        {
            PacijentUnosPregledaWindow unosPregledaWindow = new PacijentUnosPregledaWindow();
            if (unosPregledaWindow.ShowDialog() == true)
            {
                PacijentFrame.NavigationService.Refresh();
            }
        }

        private void miTerapija_Click(object sender, RoutedEventArgs e)
        {
            PacijentIzdavanjeTerapijeWindow izdavanjeTerapijeWindow = new PacijentIzdavanjeTerapijeWindow();
            if (izdavanjeTerapijeWindow.ShowDialog() == true)
            {
                PacijentFrame.NavigationService.Refresh();
            }
        }

        private void miCijepljenje_Click(object sender, RoutedEventArgs e)
        {
            PacijentUnosCijepljenjaWindow unosCijepljenjaWindow = new PacijentUnosCijepljenjaWindow();
            if (unosCijepljenjaWindow.ShowDialog() == true)
            {
                PacijentFrame.NavigationService.Refresh();
            }
        }

        private void miUputnica_Click(object sender, RoutedEventArgs e)
        {
            PacijentIzdavanjeUputnicaWindow izdavanjeUputniceWindow = new PacijentIzdavanjeUputnicaWindow();
            if (izdavanjeUputniceWindow.ShowDialog() == true)
            {
                PacijentFrame.NavigationService.Refresh();
            }
        }
    }
}
