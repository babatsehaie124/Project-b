using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// using System.Text.Json;

class Overzicht_Customer
{
    // static public void User(bool user)
    // {
    //     // string jsondata = File.ReadAllText("MovieBio.json");
    //     // var movie = JsonSerializer.Deserialize<Movies[]>(jsondata);
    //     // Console.WriteLine("Overview Current Movies\n");
    //     Console.WriteLine("Druk [A] om het huidg overzicht van de films te zien.");
    //     Console.WriteLine("Druk [B] om terug naar het menu te gaan.");
    //     string input0 = Console.ReadLine().ToLower();

    //     if (input0 == "a")
    //     {
    //         List<Movies> movies = GetMovieData();
    //         ViewData(movies);
    //         Console.WriteLine("Druk [A] om meer informatie te lezen over een van de films.");
    //         Console.WriteLine("Druk [B] om terug naar het menu te gaan.");
    //         string input1 = Console.ReadLine().ToLower();
    //         if (input1 == "a")
    //         {
    //             Console.WriteLine("Typ een film in:\n");
    //             string input2 = Console.ReadLine().ToLower();
    //         }
    //     }
    //     else if (input0 == "b")
    //     {
    //         Console.Clear();
    //         Console.WriteLine("Terug gaan naar het menu...");
    //         Menu.Start(user);
    //     }
    //     else
    //     {
    //         Console.WriteLine("Ongeldige invoer. Probeer het opnieuw.\n");
    //         User(user);
    //     }
    // }

    static public void User(bool user)
    {
        Console.WriteLine("Druk [A] om het huidige overzicht van de films te zien.");
        Console.WriteLine("Druk [B] om terug naar het menu te gaan.");
        string input0 = Console.ReadLine().ToLower();

        if (input0 == "a")
        {
            List<Movies> movies = GetMovieData();
            ViewData(movies);
            Console.WriteLine("Typ een zoekterm in:\n");
            string searchQuery = Console.ReadLine().ToLower();
            List<Movies> searchResults = SearchMovies(movies, searchQuery);

            if (searchResults.Count > 0)
            {
                Console.WriteLine("Zoekresultaten:\n");
                ViewData(searchResults);
                Console.WriteLine("Druk [C] om meer informatie te lezen over een van de films.");
                Console.WriteLine("Druk [B] om terug naar het menu te gaan.");
                string input1 = Console.ReadLine().ToLower();

                if (input1 == "c")
                {
                    Console.WriteLine("Typ de titel van de film om meer informatie te lezen:\n");
                    string movieTitle = Console.ReadLine().ToLower();
                    Movies selectedMovie = searchResults.FirstOrDefault(movie => movie.Title.ToLower() == movieTitle);

                    if (selectedMovie != null)
                    {
                        PrintMov2(selectedMovie);
                        Console.WriteLine("Druk [B] om terug naar het menu te gaan.");
                        string input2 = Console.ReadLine().ToLower();

                        if (input2 == "b")
                        {
                            Console.Clear();
                            Console.WriteLine("Terug gaan naar het menu...");
                            Menu.Start(user);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Film niet gevonden.");
                        User(user);
                    }
                }
                else if (input1 == "b")
                {
                    Console.Clear();
                    Console.WriteLine("Terug gaan naar het menu...");
                    Menu.Start(user);
                }
                else
                {
                    Console.WriteLine("Ongeldige invoer. Probeer het opnieuw.\n");
                    User(user);
                }
            }
            else
            {
                Console.WriteLine("Geen zoekresultaten gevonden.");
                User(user);
            }
        }
        else if (input0 == "b")
        {
            Console.Clear();
            Console.WriteLine("Terug gaan naar het menu...");
            Menu.Start(user);
        }
        else
        {
            Console.WriteLine("Ongeldige invoer. Probeer het opnieuw.\n");
            User(user);
        }
    }

    static List<Movies> SearchMovies(List<Movies> movies, string searchQuery)
    {
        return movies.Where(movie =>
            movie.Title.ToLower().Contains(searchQuery) ||
            movie.Genres.ToLower().Contains(searchQuery) ||
            movie.Director.ToLower().Contains(searchQuery) ||
            movie.LeadActor.ToLower().Contains(searchQuery)
        ).ToList();
    }


    static List<Movies> GetMovieData()
    {
        string jsonData = File.ReadAllText("MovieBio.json");
        Newtonsoft.Json.Linq.JArray data = JArray.Parse(jsonData);
        List<Movies> movies = new List<Movies>();
        foreach (JObject item in data)
        {
            Movies movie = new Movies
            {
                Title = (string?)item["Title"],
                Genres = (string?)item["Genres"],
                Director = (string?)item["Director"],
                LeadActor = (string?)item["LeadActor"],
                Duration = (int)item["Duration"],
                Release = (string?)item["Release"],
                Description = (string?)item["Description"],
                Price = (string?)item["Price"]
            };
            movies.Add(movie);
        }
        return movies;
    }

    static void ViewData(List<Movies> movies)
    {
        Console.WriteLine("Overzicht van huidige films:\n");
        foreach (Movies movie in movies)
        {
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Genres: {movie.Genres}");
            // Console.WriteLine($"Director(s): {movie.Director}");
            // Console.WriteLine($"Lead actor(s): {movie.LeadActor}");
            Console.WriteLine($"Duration: {movie.Duration} minutes");
            // Console.WriteLine($"Release date: {movie.Release}");
            // Console.WriteLine($"Description: {movie.Description}");
            Console.WriteLine($"Price: {movie.Price},-\n");
        }
    }

    // static void PrintMov1(Movies movie)
    //     {
    //         Console.WriteLine($"Titel: {movie.Title}");
    //         Console.WriteLine($"Genres: {movie.Genres}");
    //         Console.WriteLine($"Duur: {movie.Duration} minuten");
    //         Console.WriteLine($"Standaard prijs(2D): {movie.Price} euro");
    //     }

    static void PrintMov2(Movies movie)
    {
        Console.WriteLine($"Titel: {movie.Title}");
        Console.WriteLine($"Genres: {movie.Genres}");
        Console.WriteLine($"Regisseur(s): {movie.Director}");
        Console.WriteLine($"Hoofd acteur(s): {movie.LeadActor}");
        Console.WriteLine($"Duur: {movie.Duration} minuten");
        Console.WriteLine($"Release: {movie.Release}");
        Console.WriteLine($"Beschrijving: {movie.Description}");
        Console.WriteLine($"Standaard prijs(2D): {movie.Price} euro");
    }

}
