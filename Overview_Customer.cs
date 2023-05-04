using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Overzicht_Customer
{
    static public void User(bool user)
    {
        Console.WriteLine("Overview Current Movies\n");
        Console.WriteLine("Enter 1 to view current movies:");
        Console.WriteLine("Enter 2 to go back to menu:");
        string input = Console.ReadLine();

        if (input == "1")
        {
            List<Movies> movies = GetMovieData();
            ViewData(movies);
            User(user);
        }
        else if (input == "2")
        {
            Console.Clear();
            Console.WriteLine("Returning to menu.");
            Menu.Start(user);



        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.\n");
            User(user);
        }
    }

    static List<Movies> GetMovieData()
    {
        string jsonData = File.ReadAllText("Customer/MovieBio.json");
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
                Price = (string)item["Price"]
            };
            movies.Add(movie);
        }
        return movies;
    }

    static void ViewData(List<Movies> movies)
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

    }

}
