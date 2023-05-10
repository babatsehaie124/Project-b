static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start(bool user)
    {


        bool Admin = false;
        // ConsoleKeyInfo cki;
        // cki = Console.ReadKey();
        // Console.WriteLine("u pressed ");
        // Random rnd = new Random();
        // int menu = rnd.Next(1, 3);


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
            Console.WriteLine("[L1]- login");
        }
        Console.WriteLine("[B]- info cinema");
        Console.WriteLine("[M]- overzicht movies");
        Console.WriteLine("[R]- reservation");
        if (user == true || Admin == true)
        {
            Console.WriteLine("[L2]- logout");
        }
        Console.WriteLine("[Q]- quit program");

        string input = Console.ReadLine().ToUpper();
        if (input == "L1")
        {
            Console.Clear();
            UserLogin.Start();

        }
        else if (input == "B")
        {
            Console.Clear();
            Console.WriteLine("You will be redirected to our cinema information page...");
            Info.CinemaInfo(user);
        }
        else if (input == "M")
        {
            Console.Clear();
            Console.WriteLine("You will be redirected to our movie library...");
            Overzicht_Customer.User(user);


            // Overzicht();
        }
        else if (input == "R")
        {
            Console.Clear();
            Console.WriteLine("Your reservation starts here");
            Reservering.Reserveren();

            // Reservation();
        }
        else if (input == "L2" && user == true || input == "L2" && Admin == true)
        {
            Console.WriteLine("Are you sure you want to logout? (Y or N)");
            string choice = Console.ReadLine().ToUpper();
            if (choice == "Y")
            {
                Console.Clear();
                Console.WriteLine("You are being logged out...");
                user = false;

                Start(user);
            }
            else if (choice == "N")
            {
                Console.WriteLine("Going back to the menu");
                Start(user);
            }
            else
            {
                Console.WriteLine("Invalid input");
                Start(user);
            }
        }
        else if (input == "Q")
        {
            Console.WriteLine("Do you wish to leave? [Y] or [N]");
            string choice = Console.ReadLine().ToUpper();
            if (choice == "Y")
            {
                Console.WriteLine("Quiting program...");


            }
            else if (choice == "N")
            {
                Console.Clear();
                Console.WriteLine("Going back to the menu");

                Start(user);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid input");

                Start(user);

            }
        }
        else if (input == "A" && Admin == true)
        {

            Console.WriteLine("Hello admin. You can add or delete movies and change the data from the movies which is possible as well.");
            // AdminOverzicht();

        }
        else
        {
            Console.WriteLine("Invalid input");
            Console.Clear();
            Start(user);
        }

    }
}