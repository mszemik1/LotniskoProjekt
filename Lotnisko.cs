using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lotnisko
{
    enum EnumKategoriaBramki { A, B, C, D };
    internal class Bramka
    {
        int numer;
        EnumKategoriaBramki kategoria;
        bool czyRekaw;

        public Bramka(int numer, bool czyRekaw, EnumKategoriaBramki kategoria)
        {
            this.numer = numer;
            this.czyRekaw = czyRekaw;
            this.kategoria = kategoria;
        }
    }
    public class AktualnyCzas
    {
        public static TimeSpan aktualnyCzas = new TimeSpan(6, 0, 0);
    }



    public class Lotnisko
    {
        public string nazwa;
        List<Bramka> bramki;
        public List<Samolot> odloty;
        public List<Samolot> przyloty;
        List<Samolot> historia;

        // godziny działania lotniska
        //private static readonly TimeSpan MinCzas = TimeSpan.FromHours(4);  // 4:00
        //private static readonly TimeSpan MaxCzas = TimeSpan.FromHours(22); // 22:00
     
        public Lotnisko(string nazwa)
        {
            this.nazwa = nazwa;
            bramki = new List<Bramka>();
            odloty = new List<Samolot>();
            przyloty = new List<Samolot>();
            historia = new List<Samolot>();
        }

        public void DodajSamolot(Samolot s)
        {
            przyloty.Add(s);
        }

        public void SortujLotyPoGodzinieWylotu()
        {
            odloty = odloty.OrderBy(s => s.lot.GodzinaWylotu).ToList();
            przyloty = przyloty.OrderBy(s => s.lot.GodzinaPrzylotu).ToList();
        }


        public void WyswietlLoty()
        {
            Console.WriteLine($"Arrivals / Przyloty {nazwa}:\n");
            foreach (var s in przyloty)
            {
                Console.WriteLine(s.lot);
            }
            Console.WriteLine($"Departures/ Odloty {nazwa}:\n");
            foreach (var s in odloty)
            {
                Console.WriteLine(s.lot);
            }
            Console.WriteLine($"Odleciały z {nazwa}:\n");
            foreach (var s in historia)
            {
                Console.WriteLine(s.lot);
            }
        }

        // funkcja losująca samoloty przylatujące
        public List<Samolot> LosujSamoloty(int liczbaSamolotow, bool czyPrzyloty=true)
        {
            // nie powinienem tworzyć obiektów - linii- tutaj, ale koncepcyjnie nie wiem jak inaczej
            LiniaLotnicza wizzair = new("Wizz Air", 10, 20, true);
            LiniaLotnicza ryanair = new("Ryanair", 11, 22, true);
            LiniaLotnicza LOT = new("LOT", 8, 26, false);
            LiniaLotnicza Lufthansa = new("Lufthansa", 9, 23, false);

            List<LiniaLotnicza> linie = new() { wizzair, ryanair, LOT, Lufthansa };

            for (int i=0; i< liczbaSamolotow; i++)
            {
                Random rnd = new Random();
                int pom2 = rnd.Next(3); // losowanie modelu samolotu

                LiniaLotnicza linia = linie[rnd.Next(linie.Count)]; // losowanie linii lotniczej
               

                double stanPaliwa = rnd.NextDouble() + rnd.Next(1000) + 1000;
                if (pom2 == 0)
                {
                    Airbus320 samolot = new(stanPaliwa, linia);
                    samolot.LosujLot(czyPrzyloty);
                    samolot.Boarding(samolot.LosujPasazerow());
                    samolot.ZaladunekBagazy();
                    przyloty.Add(samolot);
                }
                else if (pom2 == 1)
                {
                    Boeing737 samolot = new Boeing737(stanPaliwa, linia);
                    samolot.LosujLot(czyPrzyloty);
                    samolot.Boarding(samolot.LosujPasazerow());
                    samolot.ZaladunekBagazy();
                    przyloty.Add(samolot);
                }
                else if (pom2 == 2)
                {
                    Airbus380 samolot = new Airbus380(stanPaliwa, linia);
                    samolot.LosujLot(czyPrzyloty);
                    samolot.Boarding(samolot.LosujPasazerow());
                    samolot.ZaladunekBagazy();
                    przyloty.Add(samolot);
                }

                // zakładam że to co losuję to są samoloty przylatujące
            }
            return przyloty;


        }

        public void RuchCzasu()
        {
            AktualnyCzas.aktualnyCzas += new TimeSpan(0, 15, 0);
            TimeSpan aktualnyCzas = AktualnyCzas.aktualnyCzas;
            Console.WriteLine($"Aktualny czas: {aktualnyCzas:hh\\:mm}");
            if (aktualnyCzas <= new TimeSpan(21, 30, 0))
            {
                LosujSamoloty(1, true);
            }
            else
            {
                Console.WriteLine("Koniec przylotów");
            }

            List<Samolot> doPrzeniesieniaDoOdlotow = new List<Samolot>();
            List<Samolot> doUsunieciaZOdlotow = new List<Samolot>();

            //Console.WriteLine($"Przyloty:");

            foreach (Samolot s in przyloty)
            {
                TimeSpan czasDoOdlotu = s.lot.czasDoOdlotu;

                if (czasDoOdlotu > TimeSpan.FromMinutes(120))
                {
                    s.lot.faza = "Przylot";
                }
                else if (czasDoOdlotu <= TimeSpan.FromMinutes(120) && czasDoOdlotu > TimeSpan.FromMinutes(105))
                {
                    s.lot.faza = "Kolowanie do bramki";
                }
                else if (czasDoOdlotu <= TimeSpan.FromMinutes(105) && czasDoOdlotu > TimeSpan.FromMinutes(90))
                {
                    s.lot.faza = "Lot zakończony";

                    s.Deboarding();
                    s.RozladunekBagazy();
                    doPrzeniesieniaDoOdlotow.Add(s); // Dodaj do listy tymczasowej
                }
                s.lot.czasDoOdlotu -= TimeSpan.FromMinutes(15);
                //Console.WriteLine($"{s.lot}");
            }

            foreach (Samolot s in doPrzeniesieniaDoOdlotow)
            {
                przyloty.Remove(s);
                odloty.Add(s);
            }

            //Console.WriteLine($"Odloty:");
            foreach (Samolot s in odloty)
            {
                TimeSpan czasDoOdlotu = s.lot.czasDoOdlotu;
                //s.lot.czasDoOdlotu -= new TimeSpan(0, 15, 0);
                if (czasDoOdlotu <= TimeSpan.FromMinutes(90) && czasDoOdlotu > TimeSpan.FromMinutes(75))
                {
                    s.lot.faza = "Przygotowanie do lotu";
                    s.LosujLot(false);
                }

                else if (czasDoOdlotu <= TimeSpan.FromMinutes(75) && czasDoOdlotu > TimeSpan.FromMinutes(30))
                {
                    s.lot.faza = "Boarding";
                    //czasDoOdlotu -= new TimeSpan(0, 15, 0);
                    if(s.LiczbaPasazerow() == 0)
                    {
                        s.Boarding(s.LosujPasazerow(false));
                        s.ZaladunekBagazy();
                        s.Tankowanie();
                    }
                }
                else if (czasDoOdlotu <= TimeSpan.FromMinutes(30) && czasDoOdlotu > TimeSpan.FromMinutes(15))
                {
                    s.lot.faza = "Kolowanie na pas";
                    //czasDoOdlotu -= new TimeSpan(0, 15, 0);
                }
                else if (czasDoOdlotu <= TimeSpan.FromMinutes(15) && czasDoOdlotu >= TimeSpan.FromMinutes(0))
                {
                    s.lot.faza = "Odlecial";
                    //czasDoOdlotu -= new TimeSpan(0, 15, 0);
                }
                else
                {
                    czasDoOdlotu -= new TimeSpan(0, 15, 0);
                    doUsunieciaZOdlotow.Add(s);
                }
                s.lot.czasDoOdlotu -= TimeSpan.FromMinutes(15);

            }


            foreach (Samolot s in doUsunieciaZOdlotow)
            {
                odloty.Remove(s);
                historia.Add(s);
            }
        }

    }
}
