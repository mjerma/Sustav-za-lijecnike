using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SustavZaLijecnike.Pages
{
    /// <summary>
    /// Interaction logic for PacijentCijepljenjePage.xaml
    /// </summary>
    public partial class PacijentCijepljenjePage : Page
    {
        public PacijentCijepljenjePage()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            Pacijent pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;

            IEnumerable<Cijepljenje> cijepljenja = Repository.GetCijepljenja(pacijent.IDPacijent);

            DataGridCijepljenja.ItemsSource = cijepljenja;
        }
    }
}
