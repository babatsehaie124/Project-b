using System;
using System.IO;
using Newtonsoft.Json;


public static class CinemaStore
{
    public static void Products(bool user)
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
        Console.WriteLine();
        Console.Clear();
        string storedata = File.ReadAllText("Store.json");
        var products = JsonConvert.DeserializeObject<List<Storeproducts>>(storedata);
        Console.Clear();
        Console.WriteLine(menu2);
        Console.WriteLine("Welkom klant bij ons eet-drinkmenu");
        Console.WriteLine("Als u iets vanuit dit menu wilt bestellen");
        Console.WriteLine("Ga terug naar het Menu -> Reserveren -> Eten bestellen\n");

        foreach (var product in products)
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

        Console.WriteLine("Bedankt voor het bekijken van onze menu.");
        Console.WriteLine("[T] terug naar het menu.");
        string? choice = Console.ReadLine().ToLower();
        if (choice == "t")
        {
            Console.Clear();
            Menu.Start(user);
        }
        else
            System.Console.WriteLine("Ongeldige invoer.");
            CinemaStore.Products(user);
    }
}

