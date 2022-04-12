using SustavZaLijecnike.UserControls;
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
    /// Interaction logic for PacijentNarudzbaDetailsWindow.xaml
    /// </summary>
    public partial class PacijentNarudzbaDetaljiWindow : Window
    {
        private Recept recept;
        public PacijentNarudzbaDetaljiWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            cbLijek.DisplayMemberPath = "Naziv";
            cbLijek.SelectedValuePath = "IDLijek";
            cbLijek.ItemsSource = Repository.GetLijekovi();

            cbDijagnoza.DisplayMemberPath = "Naziv";
            cbDijagnoza.SelectedValuePath = "IDDijagnoza";
            cbDijagnoza.ItemsSource = Repository.GetDijagnoze();
        }

        public void SetFields(Recept recept)
        {
            this.recept = recept;
            chbPonavljajuci.IsChecked = recept.Ponavljajuci;
            tbNapomena.Text = recept.Napomena;
            dpDatum.SelectedDate = recept.Datum;
            cbLijek.SelectedValue = recept.Lijek.IDLijek;
            cbDijagnoza.SelectedValue = recept.Dijagnoza.IDDijagnoza;
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSpremi_Click(object sender, RoutedEventArgs e)
        {
            Repository.UpdateRecept(new Recept 
            { 
                IDRecept = recept.IDRecept,
                Pacijent = recept.Pacijent,
                Datum = dpDatum.SelectedDate.Value,
                Lijek = (Lijek)cbLijek.SelectedItem,
                Dijagnoza = (Dijagnoza)cbDijagnoza.SelectedItem,
                Napomena = tbNapomena.Text,
                Ponavljajuci = chbPonavljajuci.IsChecked.Value,
                Odobren = recept.Odobren
            });

            DialogResult = true;
        }
    }
}
