using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Termin
    {
        public int IDTermin { get; set; }
        public Pacijent Pacijent { get; set; }
        public Zaposlenik Lijecnik { get; set; }
        public DateTime Datum { get; set; }
        public string Vrijeme { get; set; }
        public string Napomena { get; set; }

        public Termin()
        {
        }

        public Termin(int iDTermin, Pacijent pacijent, Zaposlenik lijecnik, DateTime datum, string vrijeme, string napomena)
        {
            IDTermin = iDTermin;
            Pacijent = pacijent;
            Lijecnik = lijecnik;
            Datum = datum;
            Vrijeme = vrijeme;
            Napomena = napomena;
        }

        public Termin(Pacijent pacijent, Zaposlenik lijecnik, DateTime datum, string vrijeme, string napomena)
        {
            Pacijent = pacijent;
            Lijecnik = lijecnik;
            Datum = datum;
            Vrijeme = vrijeme;
            Napomena = napomena;
        }
    }
}
