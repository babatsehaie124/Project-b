using System;
using System.IO;
using Newtonsoft.Json;


public static class CinemaStore
{
    public static void Products(bool user)
    {
        Console.WriteLine("Welkom bij onze online winkel.");
        Console.WriteLine("[P] Bekijk aanwezige producten.");
        Console.WriteLine("[T] Terug naar het menu.");
        string? input = Console.ReadLine().ToLower();

        if (input == "p")
        {
            Console.Clear();
            string storedata = File.ReadAllText("Store.json");
            var products = JsonConvert.DeserializeObject<List<Storeproducts>>(storedata);


            Console.WriteLine("Welkom klant bij onze Eet-drinkmenu");
            Console.WriteLine("Als u iets vanuit dit menu wilt bestellen");
            Console.WriteLine("Ga terug naar het Menu -> Reserveren -> Eten bestellen\n");

            foreach (var product in products)
            {
                Console.WriteLine($"Kleine Popcorn: {product.Small_Popcorn} euro         |  Informatie hierover: Dezelfde prijzen gelden voor de smaken: Zoet, Zout, Mix en Caramel.");
                Console.WriteLine($"Medium Popcorn: {product.Medium_Popcorn} euro");
                Console.WriteLine($"Grote Popcorn: {product.Large_Popcorn} euro\n");

                Console.WriteLine($"Nachos: {product.Nachos} euro                 | Informatie hierover: Dit zijn driehoekvormige chips met een lichte paprika smaak.");
                Console.WriteLine($"Loaded Nachos: {product.Loaded_Nachos} euro          | Informatie hierover: Bevat Paprika saus, uien, augurken, jalapeños en kaas.\n");

                Console.WriteLine($"Hotdog: {product.Hotdog} euro                 | Informatie hierover: Bevat varkensvlees en saus is naar keuze.");
                Console.WriteLine($"Loaded Hotdog: {product.Loaded_Hotdog} euro          | Informatie hierover: Bevat Varkensvlees, Mosterd saus, kaas, uien en paprika stukjes.\n");

                Console.WriteLine($"Koude dranken: {product.Cold_Drinks} euro          | Informatie hierover: Dezelfde prijzen voor alle dranken: Cola, Chaudfontaine blauw (water), Fristy, Chocolademelk, Lipton, Fanta, Sprite en 7up.");
                Console.WriteLine($"Warme dranken: {product.Hot_Drinks} euro          | Informatie hierover: Dezelfde prijzen voor alle dranken: Alle soorten koffie, warme chocolademelk, warme water.\n");

                Console.WriteLine($"Kindermenu: {product.Kids_Meal} euro             | Informatie hierover: Bevat Kleine stukjes chocolade, een speelgoed, en een appelmoes.\n");

                Console.WriteLine($"Snoepzak: {product.Candy_Bag} euro               | Informatie hierover: Bevat stukjes van Haribo, rode drop, Pico Bella, kleine lolly, Maoam.\n");

                Console.WriteLine($"Chips: {product.Crisps} euro                  | Informatie hierover: Bevat alleen Lays merk chips.\n");
            }

            Console.WriteLine("Bedankt voor het bekijken van onze menu.");
            Console.WriteLine("[T] terug naar het menu.");
            string? choice = Console.ReadLine().ToLower();
            if (choice == "t")
            {
                Console.Clear();
                Menu.Start(user);
            }
        }

        if (input == "t")
        {
            Console.Clear();
            Menu.Start(user);
        }

    }
}
