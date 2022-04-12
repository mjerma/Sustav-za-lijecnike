using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Zaposlenik
    {
        public int IDZaposlenik { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int TipZaposlenikaID { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public Kredencijal Kredencijal { get; set; }

        public override string ToString()
        {
            return $"{Ime} {Prezime}";
        }

        public Zaposlenik()
        {
        }

        public Zaposlenik(int iDZaposlenik, string ime, string prezime, int tipZaposlenikaID, DateTime datumZaposlenja, Kredencijal kredencijal)
        {
            IDZaposlenik = iDZaposlenik;
            Ime = ime;
            Prezime = prezime;
            TipZaposlenikaID = tipZaposlenikaID;
            DatumZaposlenja = datumZaposlenja;
            Kredencijal = kredencijal;
        }

        public Zaposlenik(string ime, string prezime, int tipZaposlenikaID, DateTime datumZaposlenja, Kredencijal kredencijal)
        {
            Ime = ime;
            Prezime = prezime;
            TipZaposlenikaID = tipZaposlenikaID;
            DatumZaposlenja = datumZaposlenja;
            Kredencijal = kredencijal;
        }
    }
}
