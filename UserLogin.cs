static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Wilt u inloggen(type I) of een nieuwe account aanmaken?(type N)");




        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.Clear();
            Console.WriteLine("Welkom terug " + acc.FullName);
            Console.WriteLine("Je email nummer is " + acc.EmailAddress);

            //Write some code to go back to the menu
            if (email == "ADMIN@hr.nl" && password == "ADMINLOGIN")
            {
                bool admin = true;
                Console.Clear();
                Console.WriteLine("welkom admin.");
                Overzicht_Admin.Admin(admin);
            }
            bool user = true;

            Menu.Start(user);
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