using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{
    public class Pacijent
    {
        public int IDPacijent { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodenja { get; set; }
        public string MBO { get; set; }
        public Prebivaliste Prebivaliste { get; set; }
        public int SpolID { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public DateTime DatumUpisa { get; set; }
        public Zaposlenik Lijecnik { get; set; }
        public Kredencijal Kredencijal { get; set; }
        public bool Aktivan { get; set; }

        public override string ToString()
        {
            return $"{Ime} {Prezime}";
        }

        public Pacijent()
        {
        }

        public Pacijent(string ime, string prezime, DateTime datumRodenja, string mBO, Prebivaliste prebivaliste, int spolID, string telefon, string email, DateTime datumUpisa, Kredencijal kredencijal, bool aktivan)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodenja = datumRodenja;
            MBO = mBO;
            Prebivaliste = prebivaliste;
            SpolID = spolID;
            Telefon = telefon;
            Email = email;
            DatumUpisa = datumUpisa;
            Kredencijal = kredencijal;
            Aktivan = aktivan;
        }

        public Pacijent(string ime, string prezime, DateTime datumRodenja, string mbo, Prebivaliste prebivaliste, int spolId, string telefon, string email, DateTime datumUpisa, Zaposlenik lijecnik, Kredencijal kredencijal, bool aktivan)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodenja = datumRodenja;
            MBO = mbo;
            Prebivaliste = prebivaliste;
            SpolID = spolId;
            Telefon = telefon;
            Email = email;
            DatumUpisa = datumUpisa;
            Lijecnik = lijecnik;
            Kredencijal = kredencijal;
            Aktivan = aktivan;
        }

        public Pacijent(int iDPacijent, string ime, string prezime, DateTime datumRodenja, string mbo, Prebivaliste prebivaliste, int spolID, string telefon, string email, DateTime datumUpisa, Zaposlenik lijecnik, Kredencijal kredencijal, bool aktivan)
        {
            IDPacijent = iDPacijent;
            Ime = ime;
            Prezime = prezime;
            DatumRodenja = datumRodenja;
            MBO = mbo;
            Prebivaliste = prebivaliste;
            SpolID = spolID;
            Telefon = telefon;
            Email = email;
            DatumUpisa = datumUpisa;
            Lijecnik = lijecnik;
            Kredencijal = kredencijal;
            Aktivan = aktivan;
        }
    }
}
