using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Lijek
    {
        public int IDLijek { get; set; }
        public Proizvodac Proizvodac { get; set; }
        public string Naziv { get; set; }
        public string Sastav { get; set; }

        public override string ToString()
        {
            return Naziv;
        }

        public Lijek(Proizvodac proizvodac, string naziv, string sastav)
        {
            this.Proizvodac = proizvodac;
            this.Naziv = naziv;
            this.Sastav = sastav;
        }
        public Lijek(int idLijek, Proizvodac proizvodac, string naziv, string sastav)
        {
            this.IDLijek = idLijek;
            this.Proizvodac = proizvodac;
            this.Naziv = naziv;
            this.Sastav = sastav;
        }
    }
}
