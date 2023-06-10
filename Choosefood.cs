using Newtonsoft.Json;

class ChooseFood
{
    public static void PickFood(bool user)
    {
        string json = File.ReadAllText("Store.json");
        List<Storeproducts> storeProductsList = JsonConvert.DeserializeObject<List<Storeproducts>>(json);
        Storeproducts storeproducts = storeProductsList[0];

        double Foodcost = 0.0;

        Console.WriteLine("Welkom bij onze Eet- drinkmenu!");
        Console.WriteLine("Maak a.u.b u keuze: ");
        Console.WriteLine("Voer a.u.b het aantal dat u wilt hebben per product:");
        Console.WriteLine();

        // List<double> Food_Drinks = new();
        // foreach(var food in )
        Foodcost += OrderFood("Kleine Popcorn", storeproducts.Small_Popcorn);
        Foodcost += OrderFood("Medium Popcorn", storeproducts.Medium_Popcorn);
        Foodcost += OrderFood("Grote Popcorn", storeproducts.Large_Popcorn);
        Foodcost += OrderFood("Nachos", storeproducts.Nachos);
        Foodcost += OrderFood("Loaded Nachos", storeproducts.Loaded_Nachos);
        Foodcost += OrderFood("Hotdog", storeproducts.Hotdog);
        Foodcost += OrderFood("Loaded Hotdog", storeproducts.Loaded_Hotdog);
        Foodcost += OrderFood("Koude dranken", storeproducts.Cold_Drinks);
        Foodcost += OrderFood("Warme dranken", storeproducts.Hot_Drinks);
        Foodcost += OrderFood("Kindermenu", storeproducts.Kids_Meal);
        Foodcost += OrderFood("Snoepzak", storeproducts.Candy_Bag);
        Foodcost += OrderFood("Chips", storeproducts.Crisps);

        Console.WriteLine();
        Console.WriteLine($"De totale kosten zijn: {Foodcost} euro");
        Console.WriteLine("Bedankt voor het bestellen bij onze Eet-drinkmenu!");
        Console.WriteLine("Je wordt doorverwezen...\n");
        Thread.Sleep(3000);
        Console.Clear();
        // Hier wordt de totalcost file verwezen. 

        Console.ReadLine();
    }

    static double OrderFood(string itemName, double itemPrice)
    {
        Console.Write($"{itemName}: ");
        double amount = Convert.ToDouble(Console.ReadLine());


        return amount * itemPrice;
    }
}















