using Newtonsoft.Json;

class ChooseFood
{
    public static void PickFood(bool user)
    {
        string json = File.ReadAllText("Store.json");
        List<Storeproducts> storeProductsList = JsonConvert.DeserializeObject<List<Storeproducts>>(json);
        Storeproducts storeproducts = storeProductsList[0];

        double Foodcost = 0.0;

        Console.WriteLine("Welkom bij onze eet-menu");
        Console.WriteLine("Maak a.u.b u keuze");

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


        Console.WriteLine($"De totale kosten zijn: {Foodcost} euro");
        Console.WriteLine("Bedankt voor het bestellen van onze Eet-drink winkel!");

        Console.ReadLine();
    }

    static double OrderFood(string itemName, double itemPrice)
    {
        Console.Write($"Voer a.u.b in aantal {itemName}: ");
        double amount = Convert.ToDouble(Console.ReadLine());


        return amount * itemPrice;
    }
}















