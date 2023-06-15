using System;
using System.IO;
using System.Text.Json;

public static class Overzicht_Customer
{
    static public void User(bool user)
    {
        string menu2 = @"
______ _                                  ______      _   _               _                 
| ___ (_)                                 | ___ \    | | | |             | |                
| |_/ /_  ___  ___  ___ ___   ___  _ __   | |_/ /___ | |_| |_ ___ _ __ __| | __ _ _ __ ___  
| ___ | |/ _ \/ __|/ __/ _ \ / _ \| '_ \  |    // _ \| __| __/ _ | '__/ _` |/ _` | '_ ` _ \ 
| |_/ | | (_) \__ | (_| (_) | (_) | |_) | | |\ | (_) | |_| ||  __| | | (_| | (_| | | | | | |
\____/|_|\___/|___/\___\___/ \___/| .__/  \_| \_\___/ \__|\__\___|_|  \__,_|\__,_|_| |_| |_|
                                  | |                                                       
                                  |_|";

        Console.WriteLine(menu2);
        Console.WriteLine("Overzicht van huidige films:\n");

        string jsondata = File.ReadAllText("MovieBio.json");
        var movies = JsonSerializer.Deserialize<Movies[]>(jsondata);

        for (int i = 0; i < movies.Length; i++)
        {
            Console.WriteLine($"[{i + 1}] {movies[i].Title}");
        }
        Console.WriteLine();
        Console.WriteLine("[T] - Terug naar het menu");
        Console.WriteLine("Selecteer een van de films: ");

        string? input0 = Console.ReadLine();
        if (input0?.ToLower() == "t")
        {
            Console.Clear();
            Menu.Start(user);
        }
        else if (int.TryParse(input0, out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= movies.Length)
        {
            PrintMov(movies[selectedIndex - 1]);

            Console.WriteLine("[S] - Selecteer een andere film");
            Console.WriteLine("[T] - Terug naar het menu");

            string input = Console.ReadLine();
            if (input?.ToLower() == "s")
            {
                Console.Clear();
                User(user);
            }
            else if (input?.ToLower() == "t")
            {
                Console.Clear();
                Menu.Start(user);
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Ongeldige invoer.");
            User(user);
        }
    }

    static void PrintMov(Movies movie)
    {
        Console.Clear();
        Console.WriteLine($"Titel: {movie.Title}");
        Console.WriteLine($"Genres: {movie.Genres}");
        Console.WriteLine($"Regisseur(s): {movie.Regisseur}");
        Console.WriteLine($"Hoofd acteur(s): {movie.LeadActor}");
        Console.WriteLine($"Duur: {movie.Duration} minuten");
        Console.WriteLine($"Release: {movie.Release}");
        Console.WriteLine($"Beschrijving: {movie.Description}");
        Console.WriteLine($"Standaard prijs(2D): {movie.Price} euro");
    }
}
