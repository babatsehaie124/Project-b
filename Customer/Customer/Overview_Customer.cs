using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Overzicht_Customer
{
static public void Main()
{
    Console.WriteLine("Overzicht Huidige Films\n");
    Console.WriteLine("Voer 1 in om huidige films te zien:");
    Console.WriteLine("Voer 2 in om terug te keren naar het menu:");
    string input = Console.ReadLine();

    if (input == "1")
    {
        List<Movies> movies = GetMovieData();
        ViewData(movies);
    }
    else if (input == "2")
    {
        
    }
    else
    {
        Console.WriteLine("Ongeldige keuze. Probeer nogmaals.\n");
        Main();
    }
}

        static List<Movies> GetMovieData()
        {
            string jsonData = File.ReadAllText("MovieBio_A.json");
            Newtonsoft.Json.Linq.JArray data = JArray.Parse(jsonData);
            List<Movies> movies = new List<Movies>();
            foreach (JObject item in data)
            {
                Movies movie = new Movies
                {
                    Title = (string)item["Titel"],
                    Genres = (string)item["Genres"],
                    Regisseur = (string)item["Regisseur"],
                    LeadActor = (string)item["Hoofdrol"],
                    Duration = (int)item["Duur"],
                    Release = (string)item["Publicatie datum"],
                    Description = (string)item["Beschrijving"],
                    Price = (int)item["Prijs"]
                };
                movies.Add(movie);
            }
            return movies;
        }       

    static void ViewData(List<Movies> movies)
        {
            Console.WriteLine("Data in MovieBio_A.json:\n");
            foreach (Movies movie in movies)
            {
                Console.WriteLine($"Titel: {movie.Title}");
                Console.WriteLine($"Genres: {movie.Genres}");
                Console.WriteLine($"Regisseur(s): {movie.Regisseur}");
                Console.WriteLine($"Hoofdrol(len): {movie.LeadActor}");
                Console.WriteLine($"Duur: {movie.Duration} minuten");
                Console.WriteLine($"Publicatie datum: {movie.Release}");
                Console.WriteLine($"Beschrijving: {movie.Description}");
                Console.WriteLine($"Prijs: {movie.Price},-\n");
            }
            Main();
        }
}
