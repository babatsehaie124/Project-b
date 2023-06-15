using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class TotalCost
{
    public string Emailaddress;
    public int RoosterId;
    public List<string> Stoelen;
    public Dictionary<string, int> Snacks;
    public dynamic Result;

    private static Dictionary<int, Dictionary<string, double>> prices = new Dictionary<int, Dictionary<string, double>>()
    {
        {1, new Dictionary<string, double>() {
            {"R", 20.00},
            {"P", 25.00},
            {"L", 45.00}
        }},
        {2, new Dictionary<string, double>() {
            {"R", 17.50},
            {"P", 22.50},
            {"L", 40.00}
        }},
        {3, new Dictionary<string, double>() {
            {"R", 15.00},
            {"P", 20.00},
            {"L", 35.00}
        }}
    };

    public static void PrintReceipt()
    {
        string jsonData = File.ReadAllText("HuidigeReservering.json");
        TotalCost data = JsonConvert.DeserializeObject<TotalCost>(jsonData);
        Console.Clear();
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
        Console.WriteLine("      Bestelling Reservering      ");
        Console.WriteLine("==================================");
        Console.WriteLine("Email: " + data.Emailaddress);
        Console.WriteLine("Movie ID: " + data.RoosterId);
        Console.WriteLine("Seats: " + string.Join(", ", data.Stoelen));
        Console.WriteLine("Snacks:");
        foreach (var snack in data.Snacks)
        {
            Console.WriteLine(snack.Key + ": " + snack.Value);
        }
        Console.WriteLine("Total Price: " + GetTotalPrice(data));
        Console.WriteLine("==================================");
        System.Console.WriteLine("Bedankt voor het reserveren bij Bioscoop Rotterdam!");
        Console.WriteLine("Het programma wordt nu afgesloten...");
        Thread.Sleep(2000);
        System.Environment.Exit(0);
    }

    private static double GetTotalPrice(TotalCost data)
    {
        double totalPrice = 0.0;
        int zaalID = GetZaalID(data.RoosterId);

        // Calculate seat price
        foreach (var chair in data.Stoelen)
        {
            totalPrice += prices[zaalID][chair.Substring(0, 1)];
        }


        // Calculate snack price
        foreach (var snack in data.Snacks)
        {
            double snackPrice = GetSnackPrice(snack.Key);
            totalPrice += snackPrice * snack.Value;
        }

        return totalPrice;
    }

    private static int GetZaalID(int roosterId)
    {
        // Load movie data
        string movieData = File.ReadAllText("Rooster.json");
        var data = JsonConvert.DeserializeObject<Dictionary<string, List<MovieSchedule>>>(movieData);

        // Find the movie with the matching roosterId
        foreach (var day in data)
        {
            foreach (var movie in day.Value)
            {
                if (movie.Rooster_Id == roosterId)
                {
                    return movie.Zaal;
                }
            }

        }

        return 1; // Movie not found, return 0 as default price
    }
    private static double GetSnackPrice(string snackName)
    {
        // Add your logic to determine the price of each snack
        // You can use the constants declared in the class or modify them as needed
        return 0.0;
    }
}
