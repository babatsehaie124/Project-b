using Newtonsoft.Json;
using System.Text.RegularExpressions;
public class UserLogin
{
    public static string loginEmail { get; set; }
    public static string loginWachtwoord { get; set; }
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static private int NextId = 1;
    public static string email;
    public static void Start(bool user)
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
        Console.WriteLine("Welkom bij de inlog/registreer pagina");
        Console.WriteLine("[I] - Inloggen");
        Console.WriteLine("[R] - Registreren");
        Console.WriteLine("[T] - Terug naar het menu");

        string input = Console.ReadLine().ToLower();
        if (input == "i")
        {
            Inloggen(user);
        }
        else if (input == "r")
        {
            Registreren(user);
        }
        else if (input == "t")
        {
            Console.WriteLine("U wordt terug gestuurd naar het menu.");
            Thread.Sleep(1500);
            Console.Clear();
            Menu.Start(user);
        }
        else
            Console.Clear();
        System.Console.WriteLine("Ongeldige invoer.");
        UserLogin.Start(user);
    }

    public static void Inloggen(bool user)
    {
        Console.WriteLine("Voer uw email in: ");
        loginEmail = Console.ReadLine();
        Console.WriteLine("Voer uw wachtwoord in: ");
        loginWachtwoord = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(loginEmail, loginWachtwoord);
        if (acc != null)
        {
            Console.Clear();
            Console.WriteLine("Welkom terug, " + acc.FName + " " + acc.LName);

            if (loginEmail == "ADMIN@hr.nl" && loginWachtwoord == "ADMINLOGIN")
            {
                bool admin = true;
                Console.Clear();
                Console.WriteLine("Welkom admin.");
                Overzicht_Admin.Admin(admin);
            }
            user = true;
            Menu.Start(user);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Geen account gevonden met die email en wachtwoord!");
            user = false;
            UserLogin.Start(user);
        }
    }

    public static void Registreren(bool user)
    {
        Console.Clear();
        System.Console.WriteLine("Voer uw gegevens in: ");
        Console.WriteLine("Voornaam: ");
        string fname = Console.ReadLine();
        if (fname.Length < 3 || fname.Any(char.IsDigit))
        {
            Console.WriteLine("Ongeldige voornaam. De voornaam moet minimaal 3 tekens bevatten en geen cijfers bevatten.");
            Thread.Sleep(1500);
            Console.Clear();
            Registreren(user);
            return;
        }

        Console.WriteLine("Achternaam: ");
        string lname = Console.ReadLine();
        if (lname.Length < 3 || lname.Any(char.IsDigit))
        {
            Console.WriteLine("Ongeldige achternaam. De achternaam moet minimaal 3 tekens bevatten en geen cijfers bevatten.");
            Thread.Sleep(1500);
            Console.Clear();
            Registreren(user);
            return;
        }
        Console.WriteLine("Voer een nieuw emailadres in: ");
        string email1 = Console.ReadLine();
        string emailaddress = email1.Trim();
        int atIndex = emailaddress.IndexOf('@');
        int dotIndex = emailaddress.LastIndexOf('.');
        if (atIndex <= 0 || dotIndex <= atIndex + 1 || dotIndex == emailaddress.Length - 1)
        {
            System.Console.WriteLine("Ongeldige email. Probeer het opnieuw.");
            Thread.Sleep(1500);
            Console.Clear();
            Registreren(user);
        }
        Console.WriteLine("Voer een nieuw wachtwoord in: ");
        string password1 = Console.ReadLine();
        if (password1.Length < 8)
        {
            Console.WriteLine("Ongeldig wachtwoord. Het wachtwoord moet minimaal 8 tekens bevatten.");
            Thread.Sleep(1500);
            Console.Clear();
            Registreren(user);
            return;
        }


        else
        {
            string jsondata = File.ReadAllText("DataSources/accounts.json");
            List<AccountModel> data = JsonConvert.DeserializeObject<List<AccountModel>>(jsondata);
            AccountModel newLogin = new AccountModel(NextId++, emailaddress, password1, fname, lname);
            data.Add(newLogin);

            string output = JsonConvert.SerializeObject(data, Formatting.Indented);
            string outputemail = JsonConvert.SerializeObject(emailaddress, Formatting.Indented);
            File.WriteAllText("DataSources/accounts.json", output);
            File.WriteAllText("Gereserveerd.json", outputemail);
            Thread.Sleep(1500);
            Console.Clear();
            Console.WriteLine("Uw account is succesvol opgeslagen in ons systeem!");
            accountsLogic.UpdateList(newLogin);
            Menu.Start(user);
        }
    }
}
