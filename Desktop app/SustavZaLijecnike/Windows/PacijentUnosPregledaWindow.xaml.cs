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
    /// Interaction logic for PacijentNoviPregledWindow.xaml
    /// </summary>
    public partial class PacijentUnosPregledaWindow : Window
    {
        public PacijentUnosPregledaWindow()
        {
            InitializeComponent();
            cbDijagnoza.ItemsSource = Repository.GetDijagnoze();
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
            if (cbDijagnoza.SelectedItem == null)
            {
                MessageBox.Show("Izaberite dijagnozu!");
                return;
            }
            Pacijent pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;
            Dijagnoza dijagnoza = (Dijagnoza)cbDijagnoza.SelectedItem;
            Repository.AddPregled(pacijent.IDPacijent, dijagnoza.IDDijagnoza, tbNapomena.Text);
            DialogResult = true;
        }
    }
}
