using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Prebivaliste
    {
        public int IDPrebivaliste { get; set; }
        public string UlicaBroj { get; set; }
        public string Grad { get; set; }

        public Prebivaliste()
        {
        }

        public Prebivaliste(string ulicaBroj, string grad)
        {
            UlicaBroj = ulicaBroj;
            Grad = grad;
        }

        public Prebivaliste(int idPrebivaliste, string ulicaBroj, string grad)
        {
            IDPrebivaliste = idPrebivaliste;
            UlicaBroj = ulicaBroj;
            Grad = grad;
        }
    }
}
