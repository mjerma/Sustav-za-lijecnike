using SustavZaLijecnike.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SustavZaLijecnike.Pages
{
    /// <summary>
    /// Interaction logic for PacijentiPage.xaml
    /// </summary>
    public partial class PacijentiPage : Page
    {
        private IEnumerable<Pacijent> pacijenti;
        public PacijentiPage()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            Zaposlenik lijecnik = ((MainWindow)Application.Current.MainWindow).ambulanta.Lijecnik;
            pacijenti = Repository.GetPacijenti(lijecnik.IDZaposlenik);
            dgPacijenti.ItemsSource = pacijenti;
        }

        private void btnDodajPacijenta_Click(object sender, RoutedEventArgs e)
        {
            NoviPacijentWindow window = new NoviPacijentWindow();
            
            if (window.ShowDialog() == true)
            {
                FillDataGrid();
            }
        }

        private void rowButton_Click(object sender, RoutedEventArgs e)
        {
            Pacijent tableItem = ((FrameworkElement)sender).DataContext as Pacijent;

            ((MainWindow)Application.Current.MainWindow).tempPacijent = tableItem;

            NavigationService.Navigate(new Uri("Pages/PacijentPage.xaml", UriKind.Relative));

            RadioButton radioButton = Application.Current.MainWindow.FindName("RbPacijenti") as RadioButton;
            radioButton.IsChecked = false;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text != "")
            {
                var filtered = pacijenti.Where(x => x.ToString().ToLower().Contains(textBox.Text.ToLower()));
                dgPacijenti.ItemsSource = filtered;
            }
            else
            {
                dgPacijenti.ItemsSource = pacijenti;
            }
        }
    }
}
