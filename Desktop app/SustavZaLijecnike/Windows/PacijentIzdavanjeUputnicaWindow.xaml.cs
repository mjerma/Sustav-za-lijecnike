using SustavZaLijecnike.Model;
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
    /// Interaction logic for PacijentIzdavanjeUputnicaWindow.xaml
    /// </summary>
    public partial class PacijentIzdavanjeUputnicaWindow : Window
    {
        public PacijentIzdavanjeUputnicaWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            cbUstanova.ItemsSource = Repository.GetUstanove();
            cbDijagnoza.ItemsSource = Repository.GetDijagnoze();
            cbTipUputnice.ItemsSource = Repository.GetTipUputnica();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnSpremi_Click(object sender, RoutedEventArgs e)
        {
            if (cbDijagnoza.SelectedItem == null || cbTipUputnice.SelectedItem == null || cbUstanova.SelectedItem == null)
            {
                MessageBox.Show("Izaberite ustanovu, tip uputnice i dijagnozu!");
                return;
            }

            Pacijent pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;

            Repository.AddUputnica(new Uputnica 
            { 
                PacijentID = pacijent.IDPacijent,
                Ustanova = (Ustanova)cbUstanova.SelectedItem,
                Dijagnoza = (Dijagnoza)cbDijagnoza.SelectedItem,
                TipUputnice = (TipUputnice)cbTipUputnice.SelectedItem,
                Napomena = tbNapomena.Text
            });

            DialogResult = true;
        }
    }
}
