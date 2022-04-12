using SustavZaLijecnike.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Uputnica
    {
        public int IDUputnica { get; set; }
        public TipUputnice TipUputnice{ get; set; }
        public int PacijentID { get; set; }
        public Dijagnoza Dijagnoza { get; set; }
        public Ustanova Ustanova { get; set; }
        public DateTime Datum { get; set; }
        public string Napomena { get; set; }

        public Uputnica()
        {
        }

        public Uputnica(int iDUputnica, TipUputnice tipUputnice, int pacijentID, Dijagnoza dijagnoza, Ustanova ustanova, DateTime datum, string napomena)
        {
            IDUputnica = iDUputnica;
            TipUputnice = tipUputnice;
            PacijentID = pacijentID;
            Dijagnoza = dijagnoza;
            Ustanova = ustanova;
            Datum = datum;
            Napomena = napomena;
        }

        public Uputnica(TipUputnice tipUputnice, int pacijentID, Dijagnoza dijagnoza, Ustanova ustanova, DateTime datum, string napomena)
        {
            TipUputnice = tipUputnice;
            PacijentID = pacijentID;
            Dijagnoza = dijagnoza;
            Ustanova = ustanova;
            Datum = datum;
            Napomena = napomena;
        }
    }
}
