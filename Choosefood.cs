/*using Newtonsoft.Json;

class ChooseFood
{
    static void Main()
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
        Foodcost += OrderFood("Medium Popcorn", storeProducts.Medium_Popcorn);
        Foodcost += OrderFood("Grote Popcorn", storeProducts.Large_Popcorn);
        Foodcost += OrderFood("Nachos", storeProducts.Nachos);
        Foodcost += OrderFood("Loaded Nachos", storeProducts.Loaded_Nachos);
        Foodcost += OrderFood("Hotdog", storeProducts.Hotdog);
        Foodcost += OrderFood("Loaded Hotdog", storeProducts.Loaded_Hotdog);
        Foodcost += OrderFood("Koude dranken", storeProducts.Cold_Drinks);
        Foodcost += OrderFood("Warme dranken", storeProducts.Hot_Drinks);
        Foodcost += OrderFood("Kindermenu", storeProducts.Kids_Meal);
        Foodcost += OrderFood("Snoepzak", storeProducts.Candy_Bag);
        Foodcost += OrderFood("Chips", storeProducts.Crisps);


        Console.WriteLine($"De totale kosten zijn: {Foodcost} euro");
        Console.WriteLine("Bedankt voor het bestellen van onze Eet-drink winkel!");

        Console.ReadLine();
    }

    static double OrderFood(string itemName, double itemPrice)
    {
        Console.Write($"Voer a.u.b in aantal {itemName}: ");
        int amount = Convert.ToInt32(Console.ReadLine());

        return amount * itemPrice;
    }
}*/















