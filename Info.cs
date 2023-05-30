using System.Text.RegularExpressions;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



public class Info
{

    public static void CinemaInfo(bool user)
    {
        Console.WriteLine("[a] Bioscoop informatie\n[b] Contact\n[c] Informatie contact\n[d]terug naar het menu");
        string User = Console.ReadLine();
        string User_lower = User.ToLower();
        if (User_lower == "a")
        {
            Schouwburgplein(user);
        }
        else if (User_lower == "b")
        {
            Contact(user);
        }
        else if (User_lower == "c")
        {
            Contactinfo(user);
        }
        else if (User_lower == "d")
        {
            Console.Clear();
            Console.WriteLine("Keert terug naar het menu");
            Menu.Start(user);
        }
        else
        {
            Console.WriteLine("Verkeerde Input!probeer opnieuw!");
            CinemaInfo(user);
        }
    }

    public static void Schouwburgplein(bool user)
    {
        Console.WriteLine(
            "Openings uren:\n" +
            "Opening van de bioscoop is 30 minuten\n" +
            "voor de eerste voorstelling en sluit\n" +
            "15 minuten na de start van de\n" +
            "laatste voorstelling.\n" +
            "\n" +
            "Adres:\n" +
            "Wijnhaven 107\n" +
            "3011 WN in Rotterdam\n" +
            "\n" +
            "Lift:\n" +
            "Ja\n" +
            "\n" +
            "Bereikbaarheid:\n" +
            "Parkeergarage Schouwburgplein The Parking rate\n" +
            "is  2,30 Euro per hour. De dichtsbijzijnde\n" +
            "gehandicapte parkeerplaatsen kan worden gevonden bij\n" +
            "Schouwburgplein near De Doelen.\n" +
            "Adres Schouwburgplein 101\n" +
            "Tarief  2,30 Euro\n" +
            "\n" +
            "Q-Park Weena reserveer je parkeerplek met een kortingscode\n" +
            "bij Q-Park and betaal per 22 minutes van een gedeelte/n" +
            "van dit 1,50 Euro met een maximale dag tarief van 30,00 Euro.\n" +
            "Adres Karel Doormanstraat 10\n" +
            "tarief  4,50 Euro\n" +
            "\n" +
            "OV:\n" +
            "Als je naar de Bioscoop komt met het openbaar vervoer,\n" +
            "kan je je reis plannen (met als voorbeeld via www.9292.nl)\n" +
            "naar de dichtsbijzijnde tramhalte, Kruisplein.\n" +
            "\n"
            );
        CinemaInfo(user);

    }

    public static void Contactinfo(bool user)
    {
        Console.Clear();
        Console.WriteLine("Als uw vraag niet is beantwoord in het bioscoopinformatie pagina kunt u ons contacteren.");
        Console.WriteLine("Om ons te kunnen bereiken moet u eerst uw naam, email en vraag doorgeven. ");
        Console.WriteLine("Uw vraag is misschien al beantwoord in de FAQ.\n");
        Console.WriteLine("Wilt u weten of u vraag al beantwoord is in Frequently Asked Questions?\n" +
        "[a] ja\n[b] nee\n[c]terug naar informatie menu");
        string User = Console.ReadLine();
        string User_lower = User.ToLower();
        if (User_lower is "a")
        {
            Console.Clear();
            Faq(user);
        }
        else if (User_lower is "b")
        {
            Console.Clear();
            Contact(user);
        }
        else if (User_lower is "c")
        {
            Console.Clear();
            CinemaInfo(user);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Verkeerde input!Probeer opnieuw!");
            Contactinfo(user);
        }
    }

    public static void Contact(bool user)
    {
        // if statement voor als user als in ingelogd, als t true is dan hoef je geen data in te vullen maar kan je meteen je vraag stellen
        if (user == true)
        {
            Loggedinquestion(true);
        }
        Console.WriteLine("Vul de gegevens in:");

        Console.WriteLine("Volledige naam: ");
        string fullName = Console.ReadLine();

        Console.WriteLine("Email: ");
        string email = Console.ReadLine();
        bool isValidEmail = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        while (!isValidEmail)
        {
            Console.WriteLine("Onjuiste email format. Probeer opnieuw.");
            Console.WriteLine("Email: ");
            email = Console.ReadLine();
            isValidEmail = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        Console.WriteLine("Vraag: ");
        string question = Console.ReadLine();

        User newUser = new User { Fullname = fullName, Email = email, Question = question };

        string filename = "userdata.json";

        string json = File.ReadAllText(filename);

        List<User> userList = JsonConvert.DeserializeObject<List<User>>(json);

        userList.Add(newUser);

        json = JsonConvert.SerializeObject(userList, Formatting.Indented);

        File.WriteAllText(filename, json);

        Console.WriteLine("Uw vraag is toegevoegd.");
        Console.Clear();
        CinemaInfo(user);
    }

    public static void Loggedinquestion(bool user)
    {
        // Read the JSON file content
            string jsonfile = File.ReadAllText("accounts.json");

            // Parse the JSON array
            JArray jsonArray = JArray.Parse(jsonfile);

            // Get the last object from the array
            JObject lastObject = (JObject)jsonArray.Last;

            // Access the last account's FullName and EmailAddress
            string lastFullName = lastObject["fullName"].ToString();
            string lastEmailAddress = lastObject["emailAddress"].ToString();

            // Use the last account information
            Console.WriteLine($"Last Account:\nFull Name: {lastFullName}\nEmail: {lastEmailAddress}");

            Console.WriteLine("Vraag: ");
            string question = Console.ReadLine();

            User newUser = new User { Fullname = lastFullName, Email = lastEmailAddress, Question = question };

            string filename = "userdata.json";

            string json = File.ReadAllText(filename);

            List<User> userList = JsonConvert.DeserializeObject<List<User>>(json);

            userList.Add(newUser);

            json = JsonConvert.SerializeObject(userList, Formatting.Indented);

            File.WriteAllText(filename, json);

            Console.WriteLine("Uw vraag is toegevoegd.");
            Console.Clear();
            CinemaInfo(user);
    
    }

    public static void Faq(bool user)
    {
        Console.WriteLine("Wat voor 3D bril moet ik gebruiken?\n" +
        "Voor 3D, 4DX 3D en Dolby Atmos 3D gebruik je de normale 3D Brillen.\n" +
        "Imax 3D Gebruik je speciale brillen, Imax 3D laser glasses.\n");
        CinemaInfo(user);
    }


}

