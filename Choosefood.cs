public class Choosefood
{
    public static void PickFood()
    {
        Console.WriteLine("Bedankt voor het reserveren van een ticket");
        Console.WriteLine("Wilt u?:");
        Console.WriteLine("[E] Eten en drinken nu bestellen");
        Console.WriteLine("[D] Doorgaan naar de laatste stap");
        string? pick = Console.ReadLine().ToLower();
        if (pick == "D")
        {
            Console.WriteLine("U wordt doorgestuurd naar u bon");
        }
        if (pick == "E")
        {

        }

        if (pick == "T")

            Console.WriteLine("Welkom bij onze Eet en drinkmenu");
    }
}