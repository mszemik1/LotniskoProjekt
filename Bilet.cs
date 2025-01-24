using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lotnisko
{
    public enum EnumKlasa { ekonomiczna = 1, premium = 2, biznes =3, pierwsza =4}
    public class Bilet
    {
        public EnumKlasa klasa;
        double cena;
        Lot lot;

        public Bilet(EnumKlasa klasa, double cena, Lot lot)
        {
            this.klasa = klasa;
            this.cena = cena;
            this.lot = lot;
        }

        public override string ToString()
        {
            return $"Bilet, {lot.ToString()}, klasa {klasa}, cena biletu: {cena:c}";
        }
    }
}