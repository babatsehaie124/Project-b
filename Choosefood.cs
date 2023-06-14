
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class ChooseFood
{
    public static void PickFood(bool user)
    {
        Console.WriteLine("Welkom bij onze Eet-drink menu!");
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
        // Geef hier je bon aan Adrian 

        Console.ReadLine();
    }
}







