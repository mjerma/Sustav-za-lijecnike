using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Dijagnoza
    {
        public int IDDijagnoza { get; set; }
        public string MKBSifra { get; set; }
        public string Naziv { get; set; }
        public string NazivLatinski { get; set; }

        public override string ToString()
        {
            return Naziv;
        }
        public Dijagnoza()
        {
        }

        public Dijagnoza(string mkbSifra, string naziv, string nazivLatinski)
        {
            this.MKBSifra = mkbSifra;
            this.Naziv = naziv;
            this.NazivLatinski = nazivLatinski;
        }
        public Dijagnoza(int idDijagnoza, string mkbSifra, string naziv, string nazivLatinski)
        {
            this.IDDijagnoza = idDijagnoza;
            this.MKBSifra = mkbSifra;
            this.Naziv = naziv;
            this.NazivLatinski = nazivLatinski;
        }
    }
}
