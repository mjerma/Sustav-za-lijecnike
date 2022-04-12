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
    /// Interaction logic for PacijentNovaTerapijaWindow.xaml
    /// </summary>
    public partial class PacijentIzdavanjeTerapijeWindow : Window
    {
        public PacijentIzdavanjeTerapijeWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            cbDijagnoza.ItemsSource = Repository.GetDijagnoze();
            cbLijek.ItemsSource = Repository.GetLijekovi();
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
            if (cbDijagnoza.SelectedItem == null || cbLijek.SelectedItem == null)
            {
                MessageBox.Show("Izaberite dijagnozu i lijek!");
                return;
            }

            Pacijent pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;
            Dijagnoza dijagnoza = (Dijagnoza)cbDijagnoza.SelectedItem;
            Lijek lijek = (Lijek)cbLijek.SelectedItem;
            Recept recept = new Recept 
            { 
                Pacijent = pacijent,
                Dijagnoza = dijagnoza,
                Lijek = lijek,
                Napomena = tbNapomena.Text,
                Ponavljajuci = chbPonavljajuci.IsChecked.Value,
                Odobren = true
            };
            Repository.AddRecept(recept);

            DialogResult = true;
        }
    }
}
