using Newtonsoft.Json;
static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
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
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("[I] - Inloggen");
        Console.WriteLine("[R] - Registreren");

        string input = Console.ReadLine().ToLower();
        if (input == "i")
        {
            Console.WriteLine("Voer uw email in: ");
            string email = Console.ReadLine();
            Console.WriteLine("Voer uw wachtwoord in: ");
            string password = Console.ReadLine();
            AccountModel acc = accountsLogic.CheckLogin(email, password);
            if (acc != null)
            {
                Console.Clear();
                Console.WriteLine("Welkom terug " + acc.fName + acc.lName);
                // Console.WriteLine("Uw email is " + acc.email);

                //Write some code to go back to the menu
                if (email == "ADMIN@hr.nl" && password == "ADMINLOGIN")
                {
                    bool admin = true;
                    Console.Clear();
                    Console.WriteLine("Welkom admin.");
                    Overzicht_Admin.Admin(admin);
                }
                bool user = true;

                Menu.Start(user);
            }
        }
        else if (input == "r")
        {
            Console.WriteLine("Voornaam: ");
            string fname = Console.ReadLine();
            Console.WriteLine("Achternaam: ");
            string lname = Console.ReadLine();
            Console.WriteLine("Voer een nieuw emailadres in: ");
            string email1 = Console.ReadLine();
            Console.WriteLine("Voer een nieuw wachtwoord in: ");
            string password1 = Console.ReadLine();

            string jsondata = File.ReadAllText("accounts.json");
            List<AccountModel> data = JsonConvert.DeserializeObject<List<AccountModel>>(jsondata);
            dynamic newLogin = new
            {
                email = email1,
                wachtwoord = password1,
                fName = fname,
                lName = lname
            };
            data.Add(newLogin);

            string output = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText("accounts.json", output);
            Console.WriteLine("Uw account is succesvol opgeslagen in ons systeem!");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Geen account gevonden met die email en password!");
            bool user = false;
            Menu.Start(user);

        }
    }
}