using SustavZaLijecnike.Model;
using SustavZaLijecnike.UserControls;
using SustavZaLijecnike.Windows;
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
    /// Interaction logic for PacijentTerapijaPage.xaml
    /// </summary>
    public partial class PacijentTerapijaPage : Page
    {
        private static IEnumerable<Recept> recepti;
        private static Pacijent pacijent;
        public PacijentTerapijaPage()
        {
            InitializeComponent();
            pacijent = ((MainWindow)Application.Current.MainWindow).tempPacijent;
            LoadData();
        }

        private void LoadData()
        {
            recepti = Repository.GetRecepti(pacijent.IDPacijent).OrderByDescending(x => x.Datum.Date).ThenByDescending(x => x.Datum.TimeOfDay);

            foreach (var item in recepti)
            {
                if (item.Odobren && spTerapija.Children.Count < 5)
                {
                    TerapijaDatumControl terapijaControl = new TerapijaDatumControl();
                    terapijaControl.Recept = item;
                    terapijaControl.Lijek = item.Lijek.Naziv;
                    terapijaControl.Datum = item.Datum.Date.ToShortDateString();
                    terapijaControl.MouseLeftButtonUp += TerapijaControl_MouseLeftButtonUp;
                    spTerapija.Children.Add(terapijaControl);
                }
                if(!item.Odobren && spNarudzbe.Children.Count < 5)
                {
                    TerapijaNarudzbaControl narudzbaControl = new TerapijaNarudzbaControl(this);
                    narudzbaControl.Recept = item;
                    narudzbaControl.Lijek = item.Lijek.Naziv;
                    narudzbaControl.MouseLeftButtonUp += NarudzbaControl_MouseLeftButtonUp;
                    spNarudzbe.Children.Add(narudzbaControl);
                }
            }
            if (spNarudzbe.Children.Count == 0)
            {
                spNarudzbe.Children.Add(new TextBlock
                {
                    Text = "Nema novih narudžbi za recept :)"
                });
                spNarudzbe.HorizontalAlignment = HorizontalAlignment.Center;
                spNarudzbe.VerticalAlignment = VerticalAlignment.Center;
            }
            if (spTerapija.Children.Count == 0)
            {
                dpTerapija.Children.Clear();
                dpTerapija.HorizontalAlignment = HorizontalAlignment.Center;
                dpTerapija.VerticalAlignment = VerticalAlignment.Center;
                dpTerapija.Children.Add(new TextBlock
                {
                    Text = "Pacijent nema propisane terapije",
                    Margin = new Thickness(0, 20, 0, 20)
                });
            }
        }

        public void ClearAndLoadData() 
        {
            spTerapija.Children.Clear();
            spNarudzbe.Children.Clear();
            LoadData();
        }

        private void NarudzbaControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PacijentNarudzbaDetaljiWindow narudzbaDetalji = new PacijentNarudzbaDetaljiWindow();
            Recept recept = ((TerapijaNarudzbaControl)sender).Recept;

            narudzbaDetalji.SetFields(recept);
            if (narudzbaDetalji.ShowDialog() == true)
            {
                ClearAndLoadData();
            }
        }

        private void TerapijaControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PacijentTerapijaDetaljiWindow terapijaDetalji = new PacijentTerapijaDetaljiWindow();
            Recept recept = ((TerapijaDatumControl)sender).Recept;

            terapijaDetalji.SetFields(recept);
            terapijaDetalji.ShowDialog();
        }

        private void BtnPovijestTerapija_Click(object sender, RoutedEventArgs e)
        {
            PacijentPovijestTerapija povijestTerapijaWindow = new PacijentPovijestTerapija();
            povijestTerapijaWindow.ShowDialog();
        }
    }
}
