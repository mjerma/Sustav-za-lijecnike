using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Pregled
    {
        public int IDPregled { get; set; }
        public int PacijentId { get; set; }
        public DateTime Datum { get; set; }
        public Dijagnoza Dijagnoza { get; set; }
        public string Napomena { get; set; }

        public Pregled()
        {
        }

        public Pregled(int pacijentId, DateTime datum, Dijagnoza dijagnoza, string napomena)
        {
            PacijentId = pacijentId;
            Datum = datum;
            Dijagnoza = dijagnoza;
            Napomena = napomena;
        }

        public Pregled(int iDPregled, int pacijentId, DateTime datum, Dijagnoza dijagnoza, string napomena)
        {
            IDPregled = iDPregled;
            PacijentId = pacijentId;
            Datum = datum;
            Dijagnoza = dijagnoza;
            Napomena = napomena;
        }
    }
}
