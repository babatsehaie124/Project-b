static class Menu
{

    static public void Start(bool user)
    {


        bool Admin = false;


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

        if (Admin == true)
        {
            Console.WriteLine("[A]- Admin access movies");
        }
        if (user != true && Admin != true)
        {
            Console.WriteLine("[L1]- Inloggen");
        }
        Console.WriteLine("[B]- Bioscoop informatie");
        Console.WriteLine("[M]- Overzicht films");
        Console.WriteLine("[R]- Reserveren");
        if (user == true || Admin == true)
        {
            Console.WriteLine("[L2]- Uitloggen");
        }
        Console.WriteLine("[Q]- Verlaat programma");

        string input = Console.ReadLine().ToUpper();
        if (input == "L1")
        {
            Console.Clear();
            UserLogin.Start();
        }
        else if (input == "B")
        {
            Console.Clear();
            Console.WriteLine("U wordt nu doorverwezen naar onze bioscoop informatie pagina...");
            Info.CinemaInfo(user);
        }
        else if (input == "M")
        {
            Console.Clear();
            Console.WriteLine("U wordt nu doorverwezen naar onze filmoverzicht pagina...");
            Overzicht_Customer.User(user);


            // Overzicht();
        }
        else if (input == "R")
        {
            Console.Clear();
            Console.WriteLine("Je reservering begint hier");
            Reservering.Reserveren();

            // Reservation();
        }
        else if (input == "L2" && user == true || input == "L2" && Admin == true)
        {
            Console.WriteLine("Weet je zeker dat je wil uitloggen? (J of N)");
            string choice = Console.ReadLine().ToUpper();
            if (choice == "J")
            {
                Console.Clear();
                Console.WriteLine("Je bent uitgelogd...");
                user = false;

                Start(user);
            }
            else if (choice == "N")
            {
                Console.WriteLine("Je keert terug naar het menu...");
                Start(user);
            }
            else
            {
                Console.WriteLine("Ongeldige invoer.");
                Start(user);
            }
        }
        else if (input == "Q")
        {
            Console.WriteLine("Weet je zeker dat je het programma wil verlaten? [J] of [N]");
            string choice = Console.ReadLine().ToUpper();
            if (choice == "J")
            {
                Console.WriteLine("Sluit programma af...");


            }
            else if (choice == "N")
            {
                Console.Clear();
                Console.WriteLine("Je keert terug naar het menu...");

                Start(user);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ongeldige invoer.");

                Start(user);

            }
        }
        else if (input == "A" && Admin == true)
        {

            Console.WriteLine("Hallo admin. Hier kun je films toevoegen en verwijderen, en het wijzigen van de data in huidige films.");
            Overzicht_Admin.Admin();

        }
        else
        {
            Console.WriteLine("Ongeldige invoer.");
            Console.Clear();
            Start(user);
        }

    }
}