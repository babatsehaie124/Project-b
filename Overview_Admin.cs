using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Overzicht_Admin
{
    static public void Admin(bool user)
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Clear();
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
        Console.WriteLine("Welkom Admin! \n");

        Console.WriteLine("[A] - Overzicht van huidige films");
        Console.WriteLine("[B] - Voeg een film toe");
        Console.WriteLine("[C] - Wijzig data van een film");
        Console.WriteLine("[D] - Verwijder een film");
        Console.WriteLine("[E] - Vragen van klanten bekijken");
        Console.WriteLine("[F] - Klantgegevens bekijken");
        Console.WriteLine("[G] - Overzicht van huidige etenswaren");
        Console.WriteLine("[H] - Voeg een etenswaar toe");
        Console.WriteLine("[I] - Terugkeren naar het menu");

        // optie voor rooster zien en kunnen wijzigen

        string input = Console.ReadLine().ToUpper();
        string fileName = "MovieBio.json";

        if (input == "A")
        {
            List<Movies> movies = GetMovieData();
            Console.Clear();
            ViewData(movies);
        }

        else if (input == "B")
        {
            Console.Clear();
            AddData(fileName);
        }

        else if (input == "C")
        {
            Console.Clear();
            EditData(fileName);
        }

        else if (input == "D")
        {
            Console.Clear();
            DeleteData(fileName);
        }

        else if (input == "E")
        {
            Console.Clear();
            PrintQuestionsFromJson(true);
        }
        else if (input == "F")
        {
            Console.Clear();
            List<Users> gegevens = GetUserData();
            ViewKlant(gegevens);
        }
        else if (input == "G")
        {
            ViewFood(user);
        }
        /*else if(input == "H")
        {
            AddFood(); 
        }*/
        else if (input == "I")
        {
            Console.Clear();
            Console.WriteLine("U wordt teruggestuurd naar het menu...");
            Thread.Sleep(3000); // gamechanger
            Console.ResetColor();
            Console.Clear();
            user = false;
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
                Regisseur = (string)item["Regisseur"],
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
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Data in MovieBio.json:\n");
        foreach (Movies movie in movies)
        {
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Genres: {movie.Genres}");
            Console.WriteLine($"Director(s): {movie.Regisseur}");
            Console.WriteLine($"Lead actor(s): {movie.LeadActor}");
            Console.WriteLine($"Duration: {movie.Duration} minutes");
            Console.WriteLine($"Release date: {movie.Release}");
            Console.WriteLine($"Description: {movie.Description}");
            Console.WriteLine($"Price: {movie.Price},-\n");
        }
        // Console.ResetColor();
        Admin(true);
    }
    static void AddData(string fileName)
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
        Console.WriteLine("Data toegevoegd in " + fileName + ".");
        Admin(true);
        Console.ResetColor();
    }

    static public void EditData(string fileName)
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
        string jsonData = File.ReadAllText(fileName);
        JArray data = JArray.Parse(jsonData);
        dynamic ddata = JsonConvert.DeserializeObject(jsonData);
        int index = 0;

        do
        {
            try
            {
                Console.WriteLine("Data in " + fileName + ":\n");
                Console.WriteLine(ddata);
                Console.WriteLine("Voer de id van de movie in die je wil wijzigen:");
                index = int.Parse(Console.ReadLine());
            }
            catch
            {
                continue;
            }
        }
        while (false);


        if (index < data.Count)
        {

            Console.WriteLine("[W] - Weet u zeker dat u dit wilt wijzigen?\n[T] - Terug naar het menu");
            string answer = Console.ReadLine().ToUpper();
            if (answer == "W")

            {
                JObject movie = (JObject)data[index];
                Console.WriteLine("Welke data zou je willen wijzigen? (Titel/Beschrijving/Prijs/)");
                string fieldToEdit = Console.ReadLine().ToUpper();
                switch (fieldToEdit)
                {
                    case "TITEL":
                        Console.WriteLine($"Voer de nieuwe titel van de film in ({movie["Title"]}):");
                        string newTitle = Console.ReadLine();
                        movie["Title"] = newTitle;
                        break;
                    case "BESCHRIJVING":
                        Console.WriteLine($"Voer de nieuwe beschrijving van de film in ({movie["Description"]}):");
                        string newDescription = Console.ReadLine();
                        movie["Description"] = newDescription;
                        break;
                    case "PRIJS":
                        Console.WriteLine($"Voer de nieuwe prijs in van de film ({movie["Price"]}):");
                        int newPrice = int.Parse(Console.ReadLine());
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
                Thread.Sleep(3000);
                Admin(true);
            }
            else if (answer == "T")
            {
                Console.WriteLine("Keert terug naar de menu...");
                Thread.Sleep(3000);
                Admin(true);
            }
            else
            {
                Console.WriteLine("Verkeerde input. probeer het opnieuw.");
                EditData(fileName);
            }

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
        Admin(true);
        // Console.ResetColor();
    }

    public static void PrintQuestionsFromJson(bool user)
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
        string filename = "userdata.json";
        string json = File.ReadAllText(filename);
        List<User> userList = JsonConvert.DeserializeObject<List<User>>(json);
        System.Console.WriteLine();

        foreach (User userEntry in userList)
        {
            Console.WriteLine("Question from " + userEntry.Voornaam + userEntry.Achternaam + ": " + userEntry.Question);
        }
        System.Console.WriteLine();

        Admin(user);

        // AdminInfo.CinemaInfo(user);
    }

    static public void ViewKlant(List<Users> gegevens)
    {

        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Accountgegevens van alle klanten:");
        foreach (Users userdata in gegevens)
        {
            Console.WriteLine($"Voornaam: {userdata.fName}");
            Console.WriteLine($"Achternaam: {userdata.lName}\n");
            Console.WriteLine($"Email: {userdata.Email}");
            Console.WriteLine($"Wachtwoord: {userdata.Wachtwoord}");
        }
        // EditAccount("Datasources/accounts.json");
        OngInvoer(true);
    }

    static public List<Users> GetUserData()
    {
        string jsonDataUsers = File.ReadAllText("DataSources/accounts.json");
        Newtonsoft.Json.Linq.JArray data = JArray.Parse(jsonDataUsers);
        List<Users> usergegevens = new List<Users>();
        Console.Clear();
        foreach (JObject item in data)
        {
            Users userdata = new Users
            {
                Email = (string)item["Email"],
                Wachtwoord = (string)item["Wachtwoord"],
                fName = (string)item["fName"],
                lName = (string)item["lName"],
            };
            usergegevens.Add(userdata);
        }
        Console.ResetColor();
        return usergegevens;
    }

    static public void ViewFood(bool user)
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
        Console.WriteLine();
        Console.Clear();


        Console.WriteLine("Welkom bij ons eet-drinkmenu");
        Console.WriteLine("Als u iets vanuit dit menu wilt bestellen");
        Console.WriteLine("Ga terug naar het Menu -> Reserveren -> Eten bestellen\n");
        string storedata = File.ReadAllText("Store.json");
        var products = JsonConvert.DeserializeObject<List<Storeproducts>>(storedata);
        Console.Clear();

        foreach (var product in products)
        {
            Console.WriteLine("                                   Extra informatie:");
            Console.WriteLine($"Kleine Popcorn: {product.Small_Popcorn} euro           | Dezelfde prijzen gelden voor de smaken: Zoet, Zout, Mix en Caramel.");
            Console.WriteLine($"Medium Popcorn: {product.Medium_Popcorn} euro");
            Console.WriteLine($"Grote Popcorn:  {product.Large_Popcorn} euro\n");

            Console.WriteLine($"Nachos:         {product.Nachos} euro           | Dit zijn driehoekvormige chips met een lichte paprika smaak.");
            Console.WriteLine($"Loaded Nachos:  {product.Loaded_Nachos} euro         | Bevat Paprika saus, uien, augurken, jalapeños en kaas.\n");

            Console.WriteLine($"Hotdog:         {product.Hotdog} euro        | Bevat varkensvlees en saus is naar keuze.");
            Console.WriteLine($"Loaded Hotdog:  {product.Loaded_Hotdog} euro         | Bevat Varkensvlees, Mosterd saus, kaas, uien en paprika stukjes.\n");

            Console.WriteLine($"Koude dranken:  {product.Cold_Drinks} euro         | Dezelfde prijzen voor alle dranken: Cola, Chaudfontaine blauw (water), Fristy, Chocolademelk, Lipton, Fanta, Sprite en 7up.");
            Console.WriteLine($"Warme dranken:  {product.Hot_Drinks} euro         | Dezelfde prijzen voor alle dranken: Alle soorten koffie, warme chocolademelk, warme water.\n");

            Console.WriteLine($"Kindermenu:     {product.Kids_Meal} euro         | Bevat Kleine stukjes chocolade, een speelgoed, en een appelmoes.\n");

            Console.WriteLine($"Snoepzak:       {product.Candy_Bag} euro           | Bevat stukjes van Haribo, rode drop, Pico Bella, kleine lolly, Maoam.\n");

            Console.WriteLine($"Chips:          {product.Crisps} euro           | Bevat alleen Lays merk chips.\n");
        }
        OngInvoer(user);
    }
    /*public static void AddFood() {}*/

    public static void OngInvoer(bool user)
    {
        System.Console.WriteLine("[T] - Terug naar het menu");
        string input1 = Console.ReadLine().ToLower();
        if (input1 == "t")
        {
            Admin(user);
        }
        else
        {
            System.Console.WriteLine("Ongeldige invoer.");
            OngInvoer(user);
        }

    }
}