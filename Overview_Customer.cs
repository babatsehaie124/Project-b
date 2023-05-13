using System;
using System.IO;
// using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

class Overzicht_Customer
{
    static public void User(bool user)
    {
        string jsondata = File.ReadAllText("MovieBio.json");
        var movie = JsonSerializer.Deserialize<Movies[]>(jsondata);
        // Console.WriteLine("Overview Current Movies\n");
        Console.WriteLine("Druk [A] om het huidg overzicht van de films te zien.");
        Console.WriteLine("Druk [B] om terug naar het menu te gaan.");
        string input = Console.ReadLine().ToLower();

        if (input == "a")
        {
            List<Movies> movies = GetMovieData();
            ViewData(movies);
            Console.WriteLine("Druk [A] voor meer informatie over een film.");
            Console.WriteLine("Druk [B] om terug naar het menu te gaan.");
            string input2 = Console.ReadLine().ToLower();
            if (input2 == "a")
            {
                Console.WriteLine("Voer een film in:\n");
                string input3 = Console.ReadLine().ToLower();
                switch (input3)
                {
                    case "john wick 4" or "john wick iv" or "john wick":
                        PrintMov2(movie[0]);
                        Console.WriteLine("[S] Selecteer een film");
                        Console.WriteLine("[T] Terug naar het menu");
                        string input4 = Console.ReadLine();
                        if (input4.ToLower() == "s")
                        {
                            ShowMov();
                            break;
                        }
                        else if (input4 == "t")
                        {
                            Menu.Start();
                        }
                        break;
                    case "creed iii" or "creed 3" or "creed":
                        PrintMov2(movie[1]);
                        Console.WriteLine("[S] Selecteer een film");
                        Console.WriteLine("[T] Terug naar het menu");
                        string input3 = Console.ReadLine();
                        if (input3.ToLower() == "s")
                        {
                            ShowMov();
                            break;
                        }
                        else if (input3 == "t")
                        {
                            Menu.Start();
                        }
                        break;
                    case "the super mario bros. movie" or "the super mario bros movie" or "mario" or "super mario bros movie" or "super mario movie":
                        PrintMov2(movie[2]);
                        Console.WriteLine("[S] Selecteer een film");
                        Console.WriteLine("[T] Terug naar het menu");
                        string input4 = Console.ReadLine();
                        if (input4.ToLower() == "s")
                        {
                            ShowMov();
                            break;
                        }
                        else if (input4 == "t")
                        {
                            Menu.Start();
                        }
                        break;
                    case "shazam! fury of the gods" or "shazam 2" or "shazam fury of the gods" or "shazam":
                        PrintMov2(movie[3]);
                        Console.WriteLine("[S] Selecteer een film");
                        Console.WriteLine("[T] Terug naar het menu");
                        string input5 = Console.ReadLine();
                        if (input5.ToLower() == "s")
                        {
                            ShowMov();
                            break;
                        }
                        else if (input5 == "t")
                        {
                            Menu.Start();
                        }
                        break;
                    case "65":
                        PrintMov2(movie[4]);
                        Console.WriteLine("[S] Selecteer een film");
                        Console.WriteLine("[T] Terug naar het menu");
                        string input6 = Console.ReadLine();
                        if (input6.ToLower() == "s")
                        {
                            ShowMov();
                            break;
                        }
                        else if (input6 == "t")
                        {
                            Menu.Start();
                        }
                        break;
                    case "black lotus":
                        PrintMov2(movie[5]);
                        Console.WriteLine("[S] Selecteer een film");
                        Console.WriteLine("[T] Terug naar het menu");
                        string input7 = Console.ReadLine();
                        if (input7.ToLower() == "s")
                        {
                            ShowMov();
                            break;
                        }
                        else if (input7 == "t")
                        {
                            Menu.Start();
                        }
                        break;
                    case "gotg" or "guardians of the galaxy" or "guardians of the galaxy vol. 3" or "guardians of the galaxy volume 3" or "guardians of the galaxy volume three" or "guardian of the galaxy":
                        PrintMov2(movie[6]);
                        Console.WriteLine("[S] Selecteer een film");
                        Console.WriteLine("[T] Terug naar het menu");
                        string input8 = Console.ReadLine();
                        if (input8.ToLower() == "s")
                        {
                            ShowMov();
                            break;
                        }
                        else if (input8 == "t")
                        {
                            Menu.Start();
                        }
                        break;
                    case "suzume" or "suzume no tojimari":
                        PrintMov2(movie[7]);
                        Console.WriteLine("[S] Selecteer een film");
                        Console.WriteLine("[T] Terug naar het menu");
                        string input9 = Console.ReadLine();
                        if (input9.ToLower() == "s")
                        {
                            ShowMov();
                            break;
                        }
                        else if (input9 == "t")
                        {
                            Menu.Start();
                        }
                        break;
                    case "dungeons & dragons: honor among thieves" or "d&d" or "d and d" or "dungeons and dragons" or "dungeons and dragons" or "dd":
                        PrintMov2(movie[8]);
                        Console.WriteLine("[S] Selecteer een film");
                        Console.WriteLine("[T] Terug naar het menu");
                        string input10 = Console.ReadLine();
                        if (input10.ToLower() == "s")
                        {
                            ShowMov();
                            break;
                        }
                        else if (input10 == "t")
                        {
                            Menu.Start();
                        }
                        break;
                    case "spoiler alert":
                        PrintMov2(movie[9]);
                        Console.WriteLine("[S] Selecteer een film");
                        Console.WriteLine("[T] Terug naar het menu");
                        string input11 = Console.ReadLine();
                        if (input11.ToLower() == "s")
                        {
                            ShowMov();
                            break;
                        }
                        else if (input11 == "t")
                        {
                            Menu.Start();
                        }
                        break;
                    default:
                        Console.WriteLine("Ongeldige invoer");
                        ShowMov();
                        break;
                }
            }
            else if (input2 == "b")
            {
                Console.Clear();
                Menu.Start(user);
            }
            else
                Console.WriteLine("Ongeldige invoer. Probeer het opnieuw.\n");
            User(user);
            // else if (input2 = "a")

        }
        else if (input == "b")
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
            Console.WriteLine($"Titel: {movie.Title}");
            // Console.WriteLine($"Genres: {movie.Genres}");
            // Console.WriteLine($"Director(s): {movie.Director}");
            // Console.WriteLine($"Lead actor(s): {movie.LeadActor}");
            Console.WriteLine($"Duur: {movie.Duration} minuten");
            // Console.WriteLine($"Release date: {movie.Release}");
            Console.WriteLine($"Beschrijving: {movie.Description}");
            Console.WriteLine($"Prijs: {movie.Price},-\n");
        }
        // Console.WriteLine("Enter [B] to go back to the menu.");
        // string input2 = Console.ReadLine().ToLower();
        // if (input2 == "b")
        // Menu.Start(user);
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
