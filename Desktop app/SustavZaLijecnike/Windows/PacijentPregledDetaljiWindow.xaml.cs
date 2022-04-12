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
    /// Interaction logic for PacijentPregledWindow.xaml
    /// </summary>
    public partial class PacijentPregledDetaljiWindow : Window
    {
        private static int IDPregled;
        public PacijentPregledDetaljiWindow(int PregledID)
        {
            InitializeComponent();
            IDPregled = PregledID;
            LoadData();
        }
        private void LoadData()
        {
            Pacijent pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;
            Pregled pregled = Repository.GetPregled(IDPregled);

            tbLijecnik.Text = pacijent.Lijecnik.ToString();
            tbDatum.Text = pregled.Datum.Date.ToShortDateString();
            tbDijagnoza.Text = pregled.Dijagnoza.Naziv;
            tbNapomena.Text = pregled.Napomena;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
