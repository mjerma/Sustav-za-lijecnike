using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Ambulanta
    {
        public int IDAmbulanta { get; set; }
        public Zaposlenik Lijecnik { get; set; }
        public Zaposlenik Sestra { get; set; }

        public Ambulanta(Zaposlenik lijecnik, Zaposlenik sestra)
        {
            Lijecnik = lijecnik;
            Sestra = sestra;
        }
        public Ambulanta(int idAmbulanta, Zaposlenik lijecnik, Zaposlenik sestra)
        {
            IDAmbulanta = idAmbulanta;
            Lijecnik = lijecnik;
            Sestra = sestra;
        }
    }
}
