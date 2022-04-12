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
    /// Interaction logic for PacijentUputnicePage.xaml
    /// </summary>
    public partial class PacijentUputnicePage : Page
    {
        public PacijentUputnicePage()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            Pacijent pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;
            IEnumerable<Uputnica> uputnice = Repository.GetUputnice(pacijent.IDPacijent);
            dgUputnice.ItemsSource = uputnice;
        }
    }
}
