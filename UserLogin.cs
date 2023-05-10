static class UserLogin
{
    private static AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page!");
        Console.WriteLine("Please enter your email address: ");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password: ");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.Clear();
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);

            //Write some code to go back to the menu
            bool user = true;
            Menu.Start(user);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("No account found with that email and password");
            bool user = false;
            Menu.Start(user);

        }
    }
}