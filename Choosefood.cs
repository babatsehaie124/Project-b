
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class ChooseFood
{
    public static void PickFood(bool user)
    {
        Console.WriteLine("Welkom bij ons eet- en drinkkaart!");
        Console.WriteLine("Wilt u:");
        Console.WriteLine("[E] Het eet- en drinkkaart bekijken");
        Console.WriteLine("[S] Uw eten en drinken bestellen");
        Console.WriteLine("[B] Uw bonnetje ontvangen");
        string choice = Console.ReadLine().ToLower();

        string storedata = File.ReadAllText("Store.json");
        var products = JsonConvert.DeserializeObject<List<Storeproducts>>(storedata);


        if (choice == "e")
        {
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
            Console.WriteLine("[T] Terug naar het eet-drink menu");
            if (choice == "t")
            {
                ChooseFood.PickFood(user);
            }
        }
        else if (choice == "s")
        {
            string jsonFilePath = "Store.json";
            List<Dictionary<string, decimal>> menu = JsonConvert.DeserializeObject<List<Dictionary<string, decimal>>>(File.ReadAllText(jsonFilePath));

            Dictionary<string, decimal> menuItems = menu[0];

            Console.WriteLine("Menu:");
            foreach (var item in menuItems)
            {
                Console.WriteLine($"{item.Key}: {item.Value} euro");
            }

            Dictionary<string, int> orderedItems = new Dictionary<string, int>();

            string userInput;
            do
            {
                Console.Write("Vul het artikel in dat je wilt bestellen (of 'klaar' om af te ronden):");
                userInput = Console.ReadLine();

                if (menuItems.ContainsKey(userInput))
                {
                    Console.Write("Voer a.u.b uw aantal in: ");
                    int quantity;
                    bool isValidQuantity = int.TryParse(Console.ReadLine(), out quantity);

                    if (isValidQuantity && quantity > 0)
                    {
                        if (orderedItems.ContainsKey(userInput))
                        {
                            orderedItems[userInput] += quantity;
                        }
                        else
                        {
                            orderedItems.Add(userInput, quantity);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige invoer, probeer a.u.b opnieuw");
                    }
                }
                else if (userInput.ToLower() != "klaar")
                {
                    Console.WriteLine("Ongeldige invoer, probeer a.u.b opnieuw");
                }
            } while (userInput.ToLower() != "klaar");


            decimal totalCost = 0;
            foreach (var item in orderedItems)
            {
                totalCost += menuItems[item.Key] * item.Value;
            }

            Console.WriteLine("Gekozen producten:");
            foreach (var item in orderedItems)
            {
                Console.WriteLine($"{item.Key}: Aantal: {item.Value}");
            }

            Console.WriteLine($"De totale kosten zijn: {totalCost} euro");
            Console.WriteLine("Bedankt voor het bestellen bij onze Eet-drink menu!");
            Console.WriteLine("U wordt doorverwezen naar...");
        }
        // Geef hier je bon aan Adrian

        Console.ReadLine();
    }
    
}
