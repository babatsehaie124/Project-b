using System;
using System.IO;
using Newtonsoft.Json;


public static class CinemaStore
{
    public static void Products(bool user)
    {
        Console.WriteLine("Welkom bij onze online winkel");
        Console.WriteLine("[P] Bekijk aanwezige producten");
        Console.WriteLine("[T] Terug naar het menu");
        string? input = Console.ReadLine().ToLower();

        if (input == "p")
        {

            string storedata = File.ReadAllText("Store.json");
            var products = JsonConvert.DeserializeObject<List<Storeproducts>>(storedata);

            foreach (var product in products)
            {
                Console.WriteLine($"Popcorn: {product.Popcorn}");
                Console.WriteLine($"Genres: {product.Sodas}");
                Console.WriteLine($"Regisseur(s): {product.Nachos}");
                Console.WriteLine($"Hoofd acteur(s): {product.Hotdog}");
                Console.WriteLine($"Duur: {product.Hot_Drinks} minuten");
                Console.WriteLine($"Release: {product.Kids_Meal}");
                Console.WriteLine($"Beschrijving: {product.Candy_Bag}");
                Console.WriteLine($"Standaard prijs(2D): {product.Crisps} euro");
            }
        }

        if (input == "t")
        {
            Console.Clear();
            Menu.Start(user);
        }

    }
}
