
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Overzicht_Admin
{
    static public void Main()
    {
        Console.WriteLine("Overzicht huidige films\n");
        Console.WriteLine("Voer 1 in om huidige films te bekijken:");
        Console.WriteLine("Voer 2 in om een film toe te voegen:");
        Console.WriteLine("Voer 3 in om een film te bewerken:");
        Console.WriteLine("Voer 4 in om een film te verwijderen:");
        Console.WriteLine("Voer 5 in om terug te keren naar het menu:");

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
            Console.WriteLine("Ongeldige keuze. Probeer het opnieuw.\n");
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
    static void AddData(string fileName)
        {
            Console.WriteLine("Voer de gegevens in\n");
            Console.WriteLine("Id: ");
            int idData = int.Parse(Console.ReadLine());
            Console.WriteLine("Titel: ");
            string name_Data = Console.ReadLine();
            Console.WriteLine("Genre: ");
            string genre_Data = Console.ReadLine();
            Console.WriteLine("Hoofdrol: ");
            string lead_Data = Console.ReadLine();
            Console.WriteLine("Duur: ");
            int duration_Data = int.Parse(Console.ReadLine());
            Console.WriteLine("Publicatie datum: ");
            string release_data = Console.ReadLine();
            Console.WriteLine("Beschrijving: ");
            string desc_Data = Console.ReadLine();
            Console.WriteLine("Prijs: ");
            int price_Data = int.Parse(Console.ReadLine());

            string jsonData = File.ReadAllText(fileName);
            List<dynamic> data = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

            dynamic newMovie = new { id = idData, name = name_Data, genre = genre_Data, lead_actor = lead_Data, 
            duration = duration_Data, release = release_data, description = desc_Data, price = price_Data};
            data.Add(newMovie);

            string output = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(fileName, output);
            Console.WriteLine("Gegevens toegevoegd in " + fileName + ".");
            Main();
        }

    static void EditData(string fileName)
        {
            Console.WriteLine("Voer de index van de datum in die je wil wijzigen:");
            int index = int.Parse(Console.ReadLine());

            string jsonData = File.ReadAllText(fileName);
            JArray data = JArray.Parse(jsonData);

            if (index < data.Count)
            {
                JObject movie = (JObject)data[index];
                Console.WriteLine("Welk gedeelte zou je willen wijzigen? (Titel/Beschrijving/Prijs/)");
                string fieldToEdit = Console.ReadLine();
                
                switch (fieldToEdit)
                {
                    case "Titel":
                        Console.WriteLine($"Voer de nieuwe titel van de film in({movie["Titel"]}):");
                        string newTitle =Console.ReadLine();
                        movie["Titel"] = newTitle;
                        break;
                    case "Beschrijving":
                        Console.WriteLine($"Voer de nieuwe beschrijving van de film in({movie["Beschrijving"]}):");
                        string newDescription = Console.ReadLine();
                        movie["Beschrijving"] = newDescription;
                        break;
                    case "prijs":
                        Console.WriteLine($"Voer de nieuwe prijs van de film in({movie["Prijs"]}):");
                        string newPrice = Console.ReadLine();
                        movie["Prijs"] = newPrice;
                        break;
                    default:
                        Console.WriteLine("Ongeldige gedeelte.");
                        EditData(fileName);
                        return;
                }

                string output = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(fileName, output);
                Console.WriteLine($"Gegevens in index {index} zijn geupdate {fileName}.");
                Main();
            }
            else
            {
                Console.WriteLine("Ongeldige index. Probeer nogmaals.\n");
                EditData(fileName);
            }
        }


    static void DeleteData(string fileName)
        {
            string jsonData = File.ReadAllText(fileName);
            dynamic data = JsonConvert.DeserializeObject(jsonData);
            Console.WriteLine("Gegevens in " + fileName + ":\n");
            Console.WriteLine(data);
            Console.WriteLine();
            Console.WriteLine("Voer de id index in van de gegevens die je wil verwijderen:");
            int index = int.Parse(Console.ReadLine());

            if (index < data.Count)
            {
                data.RemoveAt(index);
                string output = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(fileName, output);
                Console.WriteLine("Gegevens verwijderd van " + fileName + ".");
            }
            else
            {
                Console.WriteLine("Ongeldige index. Probeer nogmaals.");
                DeleteData(fileName);
                Main();
            }
        }
}