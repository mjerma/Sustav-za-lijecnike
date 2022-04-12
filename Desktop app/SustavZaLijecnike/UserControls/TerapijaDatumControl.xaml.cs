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
    /// Interaction logic for TerapijaDatumControl.xaml
    /// </summary>
    public partial class TerapijaDatumControl : UserControl
    {
        public Recept Recept { get; set; }
        public string Lijek
        {
            get { return tbTerapija.Text; }
            set { tbTerapija.Text = value; }
        }
        public string Datum
        {
            get { return tbTerapijaDatum.Text; }
            set { tbTerapijaDatum.Text = value; }
        }

        public TerapijaDatumControl()
        {
            InitializeComponent();
            
        }
    }
}
