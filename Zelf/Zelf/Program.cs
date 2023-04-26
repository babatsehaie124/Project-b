class Program
{
    static void Main(string[] args)
    {
            Console.WriteLine("\nKies tussen de volgende opties:");
            Console.WriteLine("Toets R in om te reserveren");
            Console.WriteLine("Toets H in om de huidige zaal te zien");

            string input = Console.ReadLine().ToLower();
            if (input == "r")
            {
                Reservering.Reserveren();
               
            }
            else if (input == "h")
            {
               ;
            }
            else if (input == "q")
            {
                ;
            }
    }
}