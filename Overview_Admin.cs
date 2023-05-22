
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Overzicht_Admin
{
    static public void Admin(bool user)
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Clear(); // Clear the console to apply the background color
        Console.WriteLine("Welkom Admin! \n");
        Console.WriteLine("Toets A in om huidige films te zien:");
        Console.WriteLine("Toets B in om een film toe te voegen:");
        Console.WriteLine("Toets C om een film te wijzigen:");
        Console.WriteLine("Toets D om een film te verwijderen:");
        Console.WriteLine("Toets E om de prijs van een film te veranderen:");
        Console.WriteLine("Toets F om de vragen van de klanten te zien:");
        Console.WriteLine("Toets G om terug te keren naar het menu:");

        string input = Console.ReadLine().ToUpper();
        string fileName = "MovieBio.json";

        if (input == "A")
        {
            List<Movies> movies = GetMovieData();
            ViewData(movies);
        }

        else if (input == "B")
        {
            AddData(fileName);
        }

        else if (input == "C")
        {
            EditData(fileName);
        }

        else if (input == "D")
        {
            DeleteData(fileName);
        }

        else if (input == "E")
        {
            ChangePrice();
        }

        else if (input == "F")
        {
            CustomerQuestions();
        }

        else if (input == "G")
        {
            Console.WriteLine("Je wordt teruggestuurd naar het menu...");
            Console.ResetColor();
            Menu.Start(user);
        }

        else
        {
            Console.WriteLine("Onjuiste keuze. Probeer nogmaals.\n");
            Admin(user);
        }
    }
    static public List<Movies> GetMovieData()
    {
        string jsonData = File.ReadAllText("MovieBio.json");
        Newtonsoft.Json.Linq.JArray data = JArray.Parse(jsonData);
        List<Movies> movies = new List<Movies>();
        foreach (JObject item in data)
        {
            Movies movie = new Movies
            {
                Title = (string)item["Title"],
                Genres = (string)item["Genres"],
                Director = (string)item["Director"],
                LeadActor = (string)item["LeadActor"],
                Duration = (int)item["Duration"],
                Release = (string)item["Release"],
                Description = (string)item["Description"],
                Price = (int)item["Price"]
            };
            movies.Add(movie);
        }
        Console.ResetColor();
        return movies;

    }

    static public void ViewData(List<Movies> movies)
    {
        Console.WriteLine("Data in MovieBio.json:\n");
        foreach (Movies movie in movies)
        {
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Genres: {movie.Genres}");
            Console.WriteLine($"Director(s): {movie.Director}");
            Console.WriteLine($"Lead actor(s): {movie.LeadActor}");
            Console.WriteLine($"Duration: {movie.Duration} minutes");
            Console.WriteLine($"Release date: {movie.Release}");
            Console.WriteLine($"Description: {movie.Description}");
            Console.WriteLine($"Price: {movie.Price},-\n");
        }
        Console.ResetColor();
        Admin(true);
    }
    static void AddData(string fileName)
    {
        Console.WriteLine("Voer de data in\n");
        Console.WriteLine("Id: ");
        int idData = int.Parse(Console.ReadLine());
        Console.WriteLine("Film Titel: ");
        string name_Data = Console.ReadLine();
        Console.WriteLine("Genre: ");
        string genre_Data = Console.ReadLine();
        Console.WriteLine("Hoofdrol: ");
        string lead_Data = Console.ReadLine();
        Console.WriteLine("Duur: ");
        int duration_Data = int.Parse(Console.ReadLine());
        Console.WriteLine("Uitkomst: ");
        string release_data = Console.ReadLine();
        Console.WriteLine("Beschrijving: ");
        string desc_Data = Console.ReadLine();
        Console.WriteLine("Prijs: ");
        int price_Data = int.Parse(Console.ReadLine());

        string jsonData = File.ReadAllText(fileName);
        List<dynamic> data = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);
        dynamic newMovie = new
        {
            id = idData,
            name = name_Data,
            genre = genre_Data,
            lead_actor = lead_Data,
            duration = duration_Data,
            release = release_data,
            description = desc_Data,
            price = price_Data
        };
        data.Add(newMovie);

        string output = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(fileName, output);
        Console.WriteLine("Data added to " + fileName + ".");
        Admin(true);
        Console.ResetColor();
    }

    static public void EditData(string fileName)
    {
        Console.WriteLine("Voer de datum van de index in die je wil wijzigen:");
        int index = int.Parse(Console.ReadLine());

        string jsonData = File.ReadAllText(fileName);
        JArray data = JArray.Parse(jsonData);

        if (index < data.Count)
        {
            JObject movie = (JObject)data[index];
            Console.WriteLine("Welke data zou je willen wijzigen? (Titel/Beschrijving/Prijs/)");
            string fieldToEdit = Console.ReadLine();
            switch (fieldToEdit)
            {
                case "Title":
                    Console.WriteLine($"Voer de nieuwe titel van de film in ({movie["Title"]}):");
                    string newTitle = Console.ReadLine();
                    movie["Title"] = newTitle;
                    break;
                case "Description":
                    Console.WriteLine($"Voer de nieuwe beschrijving van de film in ({movie["Description"]}):");
                    string newDescription = Console.ReadLine();
                    movie["Description"] = newDescription;
                    break;
                case "Price":
                    Console.WriteLine($"Voer de nieuwe prijs in van de film ({movie["Price"]}):");
                    string newPrice = Console.ReadLine();
                    movie["Price"] = newPrice;
                    break;
                default:
                    Console.WriteLine("Ongeldige invoer.");
                    EditData(fileName);
                    return;
            }
            string output = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(fileName, output);
            Console.WriteLine($"Data op index {index} is gewijzigd in {fileName}.");
            Admin(true);
        }
        else
        {
            Console.WriteLine("Ongeldige index. Probeer nog een keer .\n");
            EditData(fileName);
        }
        Console.ResetColor();
    }

    static void DeleteData(string fileName)
    {
        string jsonData = File.ReadAllText(fileName);
        dynamic data = JsonConvert.DeserializeObject(jsonData);
        Console.WriteLine("Data in " + fileName + ":\n");
        Console.WriteLine(data);
        Console.WriteLine();
        Console.WriteLine("Voer de index van de film in die je wil verwijderen:");
        int index = int.Parse(Console.ReadLine());

        if (index < data.Count)
        {
            data.RemoveAt(index);
            string output = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(fileName, output);
            Console.WriteLine("Data verwijderd van " + fileName + ".");
        }
        else
        {
            Console.WriteLine("Ongeldige index. Probeer nogmaals.");
            DeleteData(fileName);
            Admin(true);
        }
        Console.ResetColor();
    }

    static void ChangePrice()
    {

    }

    static void CustomerQuestions()
    {

    }
}