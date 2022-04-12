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
    /// Interaction logic for PacijentReceptDetaljiWindow.xaml
    /// </summary>
    public partial class PacijentTerapijaDetaljiWindow : Window
    {
        private Recept recept;
        public PacijentTerapijaDetaljiWindow()
        {
            InitializeComponent();
            SetControlVisibility();
        }

        private void SetControlVisibility()
        {
            Zaposlenik currentUser = ((MainWindow)Application.Current.MainWindow).currentUser;
            if (currentUser.TipZaposlenikaID == 2)
            {
                chbPonavljajuci.IsHitTestVisible = false;
            }
        }

        public void SetFields(Recept recept)
        {
            this.recept = recept;
            tbLijek.Text = recept.Lijek.Naziv;
            tbDijagnoza.Text = recept.Dijagnoza.Naziv;
            tbDatum.Text = recept.Datum.Date.ToShortDateString();
            chbPonavljajuci.IsChecked = recept.Ponavljajuci;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void chbPonavljajuci_Click(object sender, RoutedEventArgs e)
        {
            recept.Ponavljajuci = chbPonavljajuci.IsChecked.Value;
            Repository.UpdateRecept(recept);
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
