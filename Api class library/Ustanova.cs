using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Ustanova
    {
        public int IDUstanova { get; set; }
        public string Naziv { get; set; }

        public override string ToString()
        {
            return Naziv;
        }
        public Ustanova()
        {
        }
        public Ustanova(string naziv)
        {
            this.Naziv = naziv;
        }
        public Ustanova(int idUstanova, string naziv)
        {
            this.IDUstanova = idUstanova;
            this.Naziv = naziv;
        }
    }
}
