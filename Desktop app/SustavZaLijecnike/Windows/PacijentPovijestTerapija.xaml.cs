using SustavZaLijecnike.Model.Table;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SustavZaLijecnike.Windows
{
    /// <summary>
    /// Interaction logic for PacijentPovijestTerapija.xaml
    /// </summary>
    public partial class PacijentPovijestTerapija : Window
    {
        public PacijentPovijestTerapija()
        {
            InitializeComponent();
            LoadContent();
        }

        private void LoadContent()
        {
            Pacijent pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;
            IEnumerable<Recept> recepti = Repository.GetRecepti(pacijent.IDPacijent);

            List<PacijentPovijestTerapijaTableItem> pacijentPovijestTerapijaTable = new List<PacijentPovijestTerapijaTableItem>();

            foreach (var item in recepti)
            {
                string ponavljajuci = "Da";
                if (!item.Ponavljajuci)
                {
                    ponavljajuci = "Ne";
                }
                pacijentPovijestTerapijaTable.Add(new PacijentPovijestTerapijaTableItem
                {
                    Lijek = item.Lijek.Naziv,
                    Dijagnoza = item.Dijagnoza.Naziv,
                    DatumIzdavanja = item.Datum,
                    Lijecnik = pacijent.Lijecnik.ToString(),
                    Ponavljajuci = ponavljajuci
                });
            }

            dgPovijestTerapija.ItemsSource = pacijentPovijestTerapijaTable;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = MouseWheelEvent;
            eventArg.Source = e.Source;

            ScrollViewer scv = (ScrollViewer)sender;
            scv.RaiseEvent(eventArg);
            e.Handled = true;
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
