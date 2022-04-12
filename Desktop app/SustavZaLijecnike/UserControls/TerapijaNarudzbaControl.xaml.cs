using SustavZaLijecnike.Pages;
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

namespace SustavZaLijecnike.UserControls
{
    /// <summary>
    /// Interaction logic for TerapijaControl.xaml
    /// </summary>
    public partial class TerapijaNarudzbaControl : UserControl
    {
        public Recept Recept { get; set; }
        public string Lijek
        {
            get { return tbTerapija.Text; }
            set { tbTerapija.Text = value; }
        }
        private PacijentTerapijaPage page;
        public TerapijaNarudzbaControl(PacijentTerapijaPage page)
        {
            this.page = page;
            InitializeComponent();
            SetControlVisibility();
        }

        private void SetControlVisibility()
        {
            Zaposlenik currentUser = ((MainWindow)Application.Current.MainWindow).currentUser;
            if (currentUser.TipZaposlenikaID == 2)
            {
                spPrihvati.Visibility = Visibility.Collapsed;
                spOdbij.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnPrihvati_Click(object sender, RoutedEventArgs e)
        {
            Repository.OdobriRecept(Recept.IDRecept);
            //((StackPanel)Parent).Children.Remove(this);
            page.ClearAndLoadData();
        }

        private void BtnOdbij_Click(object sender, RoutedEventArgs e)
        {
            Repository.DeleteRecept(Recept.IDRecept);
            ((StackPanel)Parent).Children.Remove(this);
        }

    }
}
