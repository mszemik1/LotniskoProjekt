using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lotnisko
{
    public class Pasazer : IComparable<Pasazer>
    {
        string? imie;
        string? nazwisko;
        DateTime dataUrodzenia;
        public Bilet bilet;
        public List<Bagaz> bagaze; // lista bagaży danego pasażera - podręcznych i rejestrowanych ( można mieć > 1 każdego z nich)

        public Pasazer(string imie, string nazwisko, Bilet bilet, string data)
        {
            if (!DateTime.TryParseExact(data, new string[] {"dd-MM-yyyy", "yyyy-MM-dd",
                "dd-MMM-yyyy"}, null, DateTimeStyles.None, out DateTime d))
            {
                throw new FormatException("Błędny format daty urodzenia!");
            }

            bagaze = new List<Bagaz>();
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.bilet = bilet;
            dataUrodzenia = d;
        }

        public Pasazer(Bilet bilet, List<Bagaz> bagaze)
        {
            this.bilet = bilet;
            this.bagaze = bagaze;

            // losowe generowanie daty urodzenia
            Random rand = new Random();
            DateTime start = new DateTime(1940, 1, 1);
            int range = (DateTime.Today - start).Days;
            dataUrodzenia = start.AddDays(rand.Next(range));
        }

        public void WezBagaz(Bagaz bagaz) => bagaze.Add(bagaz);

        public int IleSztukBagazu(enumRodzajBagazu rodzaj) => bagaze.Where(b => b.rodzaj == rodzaj).Count();
        public override string ToString()
        {
            return $"Pasażer {imie} {nazwisko},\n" +
                $"liczba sztuk bagażu podręcznego: {IleSztukBagazu(enumRodzajBagazu.podreczny)}, \n" + 
                $"liczba sztuk bagażu rejestrowanego: {IleSztukBagazu(enumRodzajBagazu.rejestrowany)}\n" + 
                $"{bilet.ToString()}";
        }

        public int CompareTo(Pasazer? other)
        {
            if (other == null) return 1;

            // porównanie pasażerów wg klasy - wyższa klasa ma pierwszeństwo wejścia na pokład
            if (bilet.klasa != other.bilet.klasa)
            {
                return -1*bilet.klasa.CompareTo(other.bilet.klasa); // chcemy, żeby wyższa klasa była najpierw na liście
            }

            // jeśli klasa jest ta sama, kolejność wejścia na pokład ustalamy wg wieku pasażera - starszy ma pierwszeństwo

            return dataUrodzenia.CompareTo(other.dataUrodzenia); 
        }
    }

}
