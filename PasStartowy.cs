using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lotnisko
{
    public class ZlaKategoriaStanowiskaException : Exception
    {
        public ZlaKategoriaStanowiskaException() : base() { }
        public ZlaKategoriaStanowiskaException(string message) : base(message) { }
    }
    public class PasStartowy
    {
        string kierunek;
        int dlugosc;
        bool czyZajety;

        public PasStartowy(string kierunek, int dlugosc)
        {
            this.kierunek = kierunek;
            this.dlugosc = dlugosc;
            this.czyZajety = false;
        }
    }

    enum EnumKategoria { A, B, C, D }
    public class StanowiskoPostojowe
    {
        int numer;
        bool czyZajete;
        EnumKategoria kategoria;

        public StanowiskoPostojowe(int numer, string kategoria)
        {
            if (!Enum.TryParse(typeof(EnumKategoria), kategoria, true, out object? kategoriaStanowiska))
            {
                throw new ZlaKategoriaStanowiskaException("Stanowisko postojowe może mieć kategorię: A, B, C lub D.");
            }

            this.numer = numer;
            czyZajete = false;
            this.kategoria = (EnumKategoria)kategoriaStanowiska;
        }
    }
}
