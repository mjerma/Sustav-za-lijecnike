using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Cjepivo
    {
        public int IDCjepivo { get; set; }
        public Proizvodac Proizvodac { get; set; }
        public string Naziv { get; set; }
        public string Sastav { get; set; }

        public override string ToString()
        {
            return Naziv;
        }

        public Cjepivo()
        {
        }

        public Cjepivo(Proizvodac proizvodac, string naziv, string sastav)
        {
            this.Proizvodac = proizvodac;
            this.Naziv = naziv;
            this.Sastav = sastav;
        }
        public Cjepivo(int idCjepivo, Proizvodac proizvodac, string naziv, string sastav)
        {
            this.IDCjepivo = idCjepivo;
            this.Proizvodac = proizvodac;
            this.Naziv = naziv;
            this.Sastav = sastav;
        }
    }
}
