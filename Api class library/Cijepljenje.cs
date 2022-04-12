using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Cijepljenje
    {
        public int IDCijepljenje { get; set; }
        public int PacijentID { get; set; }
        public Cjepivo Cjepivo { get; set; }
        public DateTime Datum { get; set; }
        public string Napomena { get; set; }

        public Cijepljenje()
        {
        }

        public Cijepljenje( int pacijentId, Cjepivo cjepivo, DateTime datum, string napomena)
        {
            PacijentID = pacijentId;
            Cjepivo = cjepivo;
            Datum = datum;
            Napomena = napomena;
        }
        public Cijepljenje(int idCijepljenje, int pacijentId, Cjepivo cjepivo, DateTime datum, string napomena)
        {
            IDCijepljenje = idCijepljenje;
            PacijentID = pacijentId;
            Cjepivo = cjepivo;
            Datum = datum;
            Napomena = napomena;
        }
    }
}
