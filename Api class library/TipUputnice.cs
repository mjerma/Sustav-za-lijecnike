using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustavZaLijecnike.Model
{
    public class TipUputnice
    {
        public int IDTipUputnice { get; set; }
        public string Tip { get; set; }

        public override string ToString()
        {
            return Tip;
        }
        public TipUputnice()
        {
        }
        public TipUputnice(int iDTipUputnice, string tip)
        {
            IDTipUputnice = iDTipUputnice;
            Tip = tip;
        }

        public TipUputnice(string tip)
        {
            Tip = tip;
        }
    }
}
