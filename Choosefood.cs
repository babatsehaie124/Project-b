using Newtonsoft.Json;

class ChooseFood
{

    public static void PickFood(bool user)
    {
        Console.Clear();
        string json = File.ReadAllText("Store.json");
        List<Storeproducts> storeProductsList = JsonConvert.DeserializeObject<List<Storeproducts>>(json);

        foreach (var product in storeProductsList)
        {
            Console.WriteLine("                                   Extra informatie:");
            Console.WriteLine($"Kleine Popcorn: {product.Small_Popcorn} euro           | Dezelfde prijzen gelden voor de smaken: Zoet, Zout, Mix en Caramel.");
            Console.WriteLine($"Medium Popcorn: {product.Medium_Popcorn} euro");
            Console.WriteLine($"Grote Popcorn:  {product.Large_Popcorn} euro\n");

            Console.WriteLine($"Nachos:         {product.Nachos} euro           | Dit zijn driehoekvormige chips met een lichte paprika smaak.");
            Console.WriteLine($"Loaded Nachos:  {product.Loaded_Nachos} euro         | Bevat Paprika saus, uien, augurken, jalapeños en kaas.\n");

            Console.WriteLine($"Hotdog:         {product.Hotdog} euro        | Bevat varkensvlees en saus is naar keuze.");
            Console.WriteLine($"Loaded Hotdog:  {product.Loaded_Hotdog} euro         | Bevat Varkensvlees, Mosterd saus, kaas, uien en paprika stukjes.\n");

            Console.WriteLine($"Koude dranken:  {product.Cold_Drinks} euro         | Dezelfde prijzen voor alle dranken: Cola, Chaudfontaine blauw (water), Fristy, Chocolademelk, Lipton, Fanta, Sprite en 7up.");
            Console.WriteLine($"Warme dranken:  {product.Hot_Drinks} euro         | Dezelfde prijzen voor alle dranken: Alle soorten koffie, warme chocolademelk, warme water.\n");

            Console.WriteLine($"Kindermenu:     {product.Kids_Meal} euro         | Bevat Kleine stukjes chocolade, een speelgoed, en een appelmoes.\n");

            Console.WriteLine($"Snoepzak:       {product.Candy_Bag} euro           | Bevat stukjes van Haribo, rode drop, Pico Bella, kleine lolly, Maoam.\n");

            Console.WriteLine($"Chips:          {product.Crisps} euro           | Bevat alleen Lays merk chips.\n");
        }
        Console.WriteLine("[E] Selecteer hier uw eten en drinken");
        Console.WriteLine("[F] Filter door aanwezige producten");
        Console.WriteLine("[M] Ga naar uw bewijs/ bonnetje");
        string choice = Console.ReadLine().ToLower();
        if (choice == "e")
        {
            
            Console.WriteLine("Selecteer a.u.b welke eet-drink gelegenheden u wilt hebben?"); 
            Console.Clear();




        }





        Storeproducts storeproducts = storeProductsList[0];

        double Foodcost = 0.0;
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
        Console.WriteLine("Welkom bij onze Eet- drinkmenu!");
        Console.WriteLine("Wilt u eten en drinken bestellen? (J(Ja) N(Nee))");
        string pick = Console.ReadLine().ToLower();
        if (pick == "j")
        {


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
        }
        else
        {
            Console.WriteLine("U wordt doorverwezen naar u bewijs/bonnetje");
            Console.Clear();
            Thread.Sleep(3000);
            // Hier wordt de totalcost file verwezen. 
        }


    }

    static double OrderFood(string itemName, double itemPrice)
    {
        Console.Write($"{itemName}: ");
        double amount = Convert.ToDouble(Console.ReadLine());


        return amount * itemPrice;
    }
}









