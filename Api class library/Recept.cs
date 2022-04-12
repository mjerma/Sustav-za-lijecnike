using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Recept
    {
        public int IDRecept { get; set; }
        public Pacijent Pacijent { get; set; }
        public Dijagnoza Dijagnoza { get; set; }
        public Lijek Lijek { get; set; }
        public DateTime Datum { get; set; }
        public string Napomena { get; set; }
        public bool Ponavljajuci { get; set; }
        public bool Odobren { get; set; }

        public Recept()
        {
        }
        public Recept(int iDRecept, Pacijent pacijent, Dijagnoza dijagnoza, Lijek lijek, DateTime datum, string napomena, bool ponavljajuci, bool odobren)
        {
            IDRecept = iDRecept;
            Pacijent = pacijent;
            Dijagnoza = dijagnoza;
            Lijek = lijek;
            Datum = datum;
            Napomena = napomena;
            Ponavljajuci = ponavljajuci;
            Odobren = odobren;
        }

        public Recept(Pacijent pacijent, Dijagnoza dijagnoza, Lijek lijek, DateTime datum, string napomena, bool ponavljajuci, bool odobren)
        {
            Pacijent = pacijent;
            Dijagnoza = dijagnoza;
            Lijek = lijek;
            Datum = datum;
            Napomena = napomena;
            Ponavljajuci = ponavljajuci;
            Odobren = odobren;
        }
    }
}
