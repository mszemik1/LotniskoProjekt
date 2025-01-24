using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lotnisko
{
    class BlednaGodzinaException : Exception
    {
        public BlednaGodzinaException() : base() { }
        public BlednaGodzinaException(string message) : base(message) { }

    }
    public class Lot
    {
        public TimeSpan? godzinaWylotu;
        public TimeSpan? godzinaPrzylotu;
        //public TimeSpan czasDoOdlotu;
        string miastoWylotu;
        string miastoPrzylotu;
        public double czasLotu;
        public double bazowaCena;


        // <to powinno być w klasie lotnisko>
        private static TimeSpan MinCzas = TimeSpan.FromMinutes(1);  
        private static TimeSpan MaxCzas = TimeSpan.FromMinutes(14); 
        
        public TimeSpan czasNaLotnisku = new TimeSpan(2, 0, 0);
        //<>

        public TimeSpan? GodzinaWylotu => godzinaWylotu;
        public TimeSpan? GodzinaPrzylotu => godzinaPrzylotu;
        public TimeSpan CzasDoOdlotu => czasDoOdlotu;

        public string faza { get; set; }
        public TimeSpan czasDoOdlotu { get; set; }

        // konstruktor parametryczny do generowania samolotu w fazie przylotu lub przygotowania do lotu
        public Lot(string miasto, double czasLotu, double bazowaCena, string faza)
        {
            if (faza == "Przylot")
            {
                godzinaPrzylotu = AktualnyCzas.aktualnyCzas + LosowanieCzasu.LosujCzasMinuty(MinCzas, MaxCzas);
                //godzinaWylotu = GodzinaPrzylotu + czasNaLotnisku;
                this.czasDoOdlotu = TimeSpan.FromMinutes(135);
                this.miastoWylotu = miasto;
                this.miastoPrzylotu = "Kraków";
                this.czasLotu = czasLotu;
                this.bazowaCena = bazowaCena;
            }
            else 
            {
                godzinaWylotu = AktualnyCzas.aktualnyCzas + TimeSpan.FromMinutes(75);
                this.czasDoOdlotu = TimeSpan.FromMinutes(90);
                this.miastoWylotu = "Kraków";
                this.miastoPrzylotu = miasto;
                this.czasLotu = czasLotu;
                this.bazowaCena = bazowaCena;
            }
            this.faza = faza;
            //godzinaPrzylotu = AktualnyCzas.aktualnyCzas + LosowanieCzasu.LosujCzasMinuty(MinCzas, MaxCzas
        }

        public override string ToString()
        {
            if (faza == "Przylot" | faza == "Lot zakonczony" | faza == "Kolowanie do bramki")
            {
                return $"Lot {miastoWylotu}-Kraków, godzina przylotu: {godzinaPrzylotu:hh\\:mm}, faza lotu: {faza}";
            }
            else
            {
                return $"Lot Kraków-{miastoPrzylotu}, godzina wylotu: {godzinaWylotu:hh\\:mm},czas do odlotu: {czasDoOdlotu:hh\\:mm}, faza lotu: {faza}";
            }
           
               
           
        } // godzina przylotu = aktualny czas -> odloty 
    }
}
