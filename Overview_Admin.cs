
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Overzicht_Admin
{
    static public void Admin()
    {
        Console.WriteLine("Overview Current Movies\n");
        Console.WriteLine("Enter 1 to view current movies:");
        Console.WriteLine("Enter 2 to add a movie:");
        Console.WriteLine("Enter 3 to edit a movie:");
        Console.WriteLine("Enter 4 to delete a movie:");
        Console.WriteLine("Enter 5 to return to menu:");

        string input = Console.ReadLine();
        string fileName = "MovieBio_A.json";

        if (input == "1")
        {
            List<Movies> movies = GetMovieData();
            ViewData(movies);
        }

        else if (input == "2")
        {
            AddData(fileName);
        }

        else if (input == "3")
        {
            EditData(fileName);
        }

        else if (input == "4")
        {
            DeleteData(fileName);
        }

        else if (input == "5")
        {

        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.\n");
            Admin();
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
        Console.WriteLine("Data in MovieBio_A.json:\n");
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
        Admin();
    }
    static void AddData(string fileName)
    {
        Console.WriteLine("Enter the data\n");
        Console.WriteLine("Id: ");
        int idData = int.Parse(Console.ReadLine());
        Console.WriteLine("Film Title: ");
        string name_Data = Console.ReadLine();
        Console.WriteLine("Genre: ");
        string genre_Data = Console.ReadLine();
        Console.WriteLine("Lead Actor: ");
        string lead_Data = Console.ReadLine();
        Console.WriteLine("Duration: ");
        int duration_Data = int.Parse(Console.ReadLine());
        Console.WriteLine("Release: ");
        string release_data = Console.ReadLine();
        Console.WriteLine("Description: ");
        string desc_Data = Console.ReadLine();
        Console.WriteLine("Price: ");
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
        Admin();
    }

    static void EditData(string fileName)
    {
        Console.WriteLine("Enter the index of the data to edit:");
        int index = int.Parse(Console.ReadLine());

        string jsonData = File.ReadAllText(fileName);
        JArray data = JArray.Parse(jsonData);

        if (index < data.Count)
        {
            JObject movie = (JObject)data[index];
            Console.WriteLine("Which field would you like to edit? (Title/Description/Price/)");
            string fieldToEdit = Console.ReadLine();

            switch (fieldToEdit)
            {
                case "Title":
                    Console.WriteLine($"Enter the new title for the movie ({movie["Title"]}):");
                    string newTitle = Console.ReadLine();
                    movie["Title"] = newTitle;
                    break;
                case "Description":
                    Console.WriteLine($"Enter the new description for the movie ({movie["Description"]}):");
                    string newDescription = Console.ReadLine();
                    movie["Description"] = newDescription;
                    break;
                case "Price":
                    Console.WriteLine($"Enter the new price for the movie ({movie["Price"]}):");
                    string newPrice = Console.ReadLine();
                    movie["Price"] = newPrice;
                    break;
                default:
                    Console.WriteLine("Invalid field name.");
                    EditData(fileName);
                    return;
            }

            string output = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(fileName, output);
            Console.WriteLine($"Data at index {index} has been updated in {fileName}.");
            Admin();
        }
        else
        {
            Console.WriteLine("Invalid index. Please try again.\n");
            EditData(fileName);
        }
    }


    static void DeleteData(string fileName)
    {
        string jsonData = File.ReadAllText(fileName);
        dynamic data = JsonConvert.DeserializeObject(jsonData);
        Console.WriteLine("Data in " + fileName + ":\n");
        Console.WriteLine(data);
        Console.WriteLine();
        Console.WriteLine("Enter the id index of the data to delete:");
        int index = int.Parse(Console.ReadLine());

        if (index < data.Count)
        {
            data.RemoveAt(index);
            string output = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(fileName, output);
            Console.WriteLine("Data deleted from " + fileName + ".");
        }
        else
        {
            Console.WriteLine("Invalid index. Please try again.");
            DeleteData(fileName);
            Admin();
        }
    }
}