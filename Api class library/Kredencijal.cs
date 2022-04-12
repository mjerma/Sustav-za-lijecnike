using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike
{

    public class Kredencijal
    {
        public int IDKredencijal { get; set; }
        public string KorisnickoIme { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public Kredencijal()
        {
        }
        public Kredencijal(int iDKredencijal, string korisnickoIme, string hash, string salt)
        {
            IDKredencijal = iDKredencijal;
            KorisnickoIme = korisnickoIme;
            Hash = hash;
            Salt = salt;
        }

        public Kredencijal(string korisnickoIme, string hash, string salt)
        {
            KorisnickoIme = korisnickoIme;
            Hash = hash;
            Salt = salt;
        }
    }
}
