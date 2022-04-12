using SustavZaLijecnike.Windows;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SustavZaLijecnike.Pages
{
    /// <summary>
    /// Interaction logic for PacijentPreglediPage.xaml
    /// </summary>
    public partial class PacijentPreglediPage : Page
    {
        public PacijentPreglediPage()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            Pacijent pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;
            IEnumerable<Pregled> pregledi = Repository.GetPregledi(pacijent.IDPacijent);
            DataGridPregledi.ItemsSource = pregledi;
        }

        private void RowButton_Click(object sender, RoutedEventArgs e)
        {
            Pregled pregled = ((FrameworkElement)sender).DataContext as Pregled;

            PacijentPregledDetaljiWindow window = new PacijentPregledDetaljiWindow(pregled.IDPregled);
            window.ShowDialog();
        }
    }
}
