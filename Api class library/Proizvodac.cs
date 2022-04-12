using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Proizvodac
    {
        public int IDProizvodac { get; set; }
        public string Naziv { get; set; }

        public Proizvodac(string naziv)
        {
            this.Naziv = naziv;
        }
        public Proizvodac(int idProizvodac, string naziv)
        {
            this.IDProizvodac = idProizvodac;
            this.Naziv = naziv;
        }
    }
}
