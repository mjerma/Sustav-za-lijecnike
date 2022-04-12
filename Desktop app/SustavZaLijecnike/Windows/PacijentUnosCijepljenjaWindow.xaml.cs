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
    /// Interaction logic for PacijentZakazivanjeCijepljenjaWindow.xaml
    /// </summary>
    public partial class PacijentUnosCijepljenjaWindow : Window
    {
        public PacijentUnosCijepljenjaWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            cbCjepivo.ItemsSource = Repository.GetCjepiva();
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
            if (cbCjepivo.SelectedItem == null || dpDatum.SelectedDate == null)
            {
                MessageBox.Show("Unesite podatke o cjepivu i datumu!");
                return;
            }

            Pacijent pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;
            Cijepljenje cijepljenje = new Cijepljenje
            {
                PacijentID = pacijent.IDPacijent,
                Cjepivo = (Cjepivo)cbCjepivo.SelectedItem,
                Datum = dpDatum.DisplayDate,
                Napomena = tbNapomena.Text
            };
            Repository.AddCijepljenje(cijepljenje);

            DialogResult = true;
        }
    }
}
