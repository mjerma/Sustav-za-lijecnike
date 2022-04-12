using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike.Model.Table
{
    public class PacijentPovijestTerapijaTableItem
    {
        public string Lijek { get; set; }
        public string Dijagnoza { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public string Lijecnik { get; set; }
        public string Ponavljajuci { get; set; }
    }
}
