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
        // als admin true is in userlogin 
        // if (Admin == true)
        // {
        //     Console.WriteLine("[A]- Admin access movies");
        // }
        if (user != true && Admin != true)
        {
            Console.WriteLine("[I] - Inloggen / Registreren");
        }
        Console.WriteLine("[B] - Bioscoop informatie");
        Console.WriteLine("[E] - Aanwezige etenswaren");
        Console.WriteLine("[M] - Overzicht films");
        Console.WriteLine("[R] - Reserveren");
        if (user == true || Admin == true)
        {
            Console.WriteLine("[U] - Uitloggen");
        }
        Console.WriteLine("[Q] - Verlaat programma");

        string input = Console.ReadLine().ToUpper();
        if (input == "I")
        {
            Console.Clear();
            UserLogin.Start(user);
        }
        else if (input == "B")
        {
            Console.Clear();
            Console.WriteLine("U wordt doorverwezen naar onze bioscoop informatie pagina...");
            Thread.Sleep(2000);
            Info.CinemaInfo(user);
        }
        else if (input == "M")
        {
            Console.Clear();
            Console.WriteLine("U wordt doorverwezen naar onze filmoverzicht pagina...");
            Thread.Sleep(2000);
            Overzicht_Customer.User(user);
            //ReserveringsManager.Reserveren(user, 20);
            // Overzicht_Admin.Admin(user);
            // ChooseFood.PickFood(user);
        }
        else if (input == "R")
        {
            Console.Clear();
            Console.WriteLine("U wordt doorverwezen naar ons reserveringssysteem.");
            Thread.Sleep(2000);
            Rooster.RoosterMenu(user);

        }
        else if (input == "E")
        {
            Console.Clear();
            System.Console.WriteLine("U wordt doorverwezen naar ons eet-drinkmenu...");
            Thread.Sleep(2000);
            CinemaStore.Products(user);

        }
        else if (input == "U" && user == true || input == "U" && Admin == true)
        {
            Console.WriteLine("Weet u zeker dat u wilt uitloggen? (J of N)");
            string choice = Console.ReadLine().ToUpper();
            if (choice == "J")
            {
                Console.Clear();
                Console.WriteLine("U bent uitgelogd...");
                user = false;

                Start(user);
            }
            else if (choice == "N")
            {
                Console.WriteLine("U keert terug naar het menu...");
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
            Console.WriteLine("Weet u zeker dat u het programma wilt verlaten? [J] of [N]");
            string choice = Console.ReadLine().ToUpper();
            if (choice == "J")
            {
                Console.WriteLine("Sluit programma af...");
                Thread.Sleep(2000);
                System.Environment.Exit(0);
            }
            else if (choice == "N")
            {
                Console.Clear();
                Console.WriteLine("U keert terug naar het menu...");
                Start(user);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ongeldige invoer.");
                Start(user);
            }
        }
        // else if (input == "A" && Admin == true)
        // {
        //     Console.WriteLine("Hallo admin. Hier kun je films toevoegen en verwijderen, en het wijzigen van de data in huidige films.");
        //     Overzicht_Admin.Admin(user);
        // }
        else
        {
            Console.WriteLine("Ongeldige invoer.");
            Console.Clear();
            Start(user);
        }
    }
}