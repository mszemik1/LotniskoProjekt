using lotnisko;

internal class Program
{
    static void Main(string[] args)
    {
        // zakładamy że nasze lotnisko jest w Krakowie 
        /*
        // tworzymy linie lotnicze
        LiniaLotnicza wizzair = new("Wizz Air", 10, 20, true);
        LiniaLotnicza ryanair = new("Ryanair", 11, 22, true);
        LiniaLotnicza LOT = new("LOT", 8, 26, false);

        // tworzymy loty - przylatujące - trzeba podać miasto z którego wyleciał, czas lotu i cenę
        Lot lot1 = new("Warszawa", 0.5, 357);
        Lot lot2 = new("Londyn", 2.25, 1200);
        Lot lot3 = new("Paryż", 2.15, 1325);  

        // tworzymy samoloty
        Airbus320 airbus1 = new(4000, wizzair, lot1);
        Boeing737 boeing1 = new(3000, ryanair, lot2);
        Boeing737 boeing2 = new(2000, LOT, lot3);

        Console.WriteLine(boeing2.ToString());

        // tworzymy bilety
        Bilet bilet1 = new(EnumKlasa.ekonomiczna, 400, lot1);
        Bilet bilet2 = new(EnumKlasa.ekonomiczna, 843.32, lot2);


        // tworzymy pasażerów
        Pasazer pasazer1 = new("Adam", "Kowalski", bilet1, "2002-12-02");
        Pasazer pasazer2 = new("Joanna", "Nowak", bilet2, "1998-04-04");

        // tworzymy bagaże i dodajemy do pasażerów
        Bagaz b1 = new(6.5, enumRodzajBagazu.podreczny);
        Bagaz b2 = new(15.67, enumRodzajBagazu.rejestrowany);
        Bagaz b3 = new(12, enumRodzajBagazu.podreczny);
        Bagaz b4 = new(17, enumRodzajBagazu.rejestrowany);
        pasazer1.WezBagaz(b1);
        pasazer1.WezBagaz(b2);
        pasazer1.WezBagaz(b3);

        pasazer2.WezBagaz(b1);
        pasazer2.WezBagaz(b2);
        pasazer2.WezBagaz(b4);

        Console.WriteLine(pasazer1.ToString());
        Console.WriteLine(pasazer2.ToString());

        // lista pasażerów, którzy wejdą na pokład
        List<Pasazer> pasazerowie = new() { pasazer1, pasazer2 };


        // lista pasażerów - wykorzystanie funkcji losującej
        List<Pasazer> losowaniPasazerowie = boeing2.LosujPasazerow();
        List<Pasazer> losowaniPasazerowie2 = airbus1.LosujPasazerow();
        
        // boarding
        boeing2.Boarding(losowaniPasazerowie);
        boeing2.Tankowanie();

        Console.WriteLine(boeing2.ToString());

        // deboarding

        boeing2.Deboarding();
        Console.WriteLine(boeing2.ToString());

        Console.WriteLine(airbus1.ToString());
        airbus1.Boarding(losowaniPasazerowie2);
        airbus1.Tankowanie();
        Console.WriteLine(airbus1.ToString());
        // dodanie lotniska



        // dodanie kolejnych lotów przylatujących i wypisanie listy
        Lot lot4 = new ("Amsterdam", 1.5, 1287);
        Lot lot5 = new ("Barcelona", 2.25, 1657);


        // konsekwencja tego co w klasie lotnisko: musimy dodawać Samolot z lotem

        
        krakowLotnisko.DodajLot(lot1);
        krakowLotnisko.DodajLot(lot2);
        krakowLotnisko.DodajLot(lot3);
        krakowLotnisko.DodajLot(lot4);
        krakowLotnisko.DodajLot(lot5);
        
        Lotnisko krakowLotnisko = new Lotnisko("Kraków");
        krakowLotnisko.DodajSamolot(airbus1);
        krakowLotnisko.DodajSamolot(boeing1);

        // losowanie samolotów, można sobie wylosować i przyloty i wyloty, ale w praktyce tylko przyloty 
        //krakowLotnisko.LosujSamoloty(20);

        // jeśli czyPrzyloty= false to losujemy wyloty
        //krakowLotnisko.LosujSamoloty(20, false);
        */
        Lotnisko krakowLotnisko = new Lotnisko("Kraków");
        for (int i = 0; i < 10; i++) 
        { 
            krakowLotnisko.RuchCzasu();
            krakowLotnisko.WyswietlLoty();
            Console.WriteLine("");
            Console.WriteLine("########################");
        }
        krakowLotnisko.SortujLotyPoGodzinieWylotu();
       
    }
}
