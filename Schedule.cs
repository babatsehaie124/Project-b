using Newtonsoft.Json;

public class Rooster
{
    public static void RoosterMenu(bool user)
    {
        Console.WriteLine("[A] - Rooster\n[T] - Terug naar het menu");
        string User = Console.ReadLine();
        string User_lower = User.ToLower();
        if (User_lower == "a")
        {
            Roosterweek(user);
        }
        else if (User_lower == "t")
        {
            Console.Clear();
            Console.WriteLine("Keer terug naar het menu");
            Menu.Start(user);
        }
        else
        {
            Console.WriteLine("Verkeerde Input! Probeer opnieuw!");
            RoosterMenu(user);
        }
    }

    public static void Roosterweek(bool user)
    {
        string json = File.ReadAllText("Rooster.json");
        var scheduleByDay = JsonConvert.DeserializeObject<Dictionary<string, List<MovieSchedule>>>(json);

        foreach (var kvp in scheduleByDay)
        {
            string day = kvp.Key;
            List<MovieSchedule> movieSchedules = kvp.Value;

            Console.WriteLine(day);

            foreach (var movieSchedule in movieSchedules)
            {
                Console.WriteLine($"Titel: {movieSchedule.Title}\n Tijd: {movieSchedule.Start} -  {movieSchedule.Ending}\n Zaal: {movieSchedule.Zaal}");
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        Console.WriteLine("[F] - Filter rooster");
        Console.WriteLine("[R] - Reserveer voor een film");
        Console.WriteLine("[T] - Terug naar het rooster menu");
        Console.WriteLine("Selecteer een van de opties: ");

        string? input0 = Console.ReadLine();
        if (input0?.ToLower() == "t")
        {
            Console.Clear();
            RoosterMenu(user);
        }
        else if (input0.ToLower() == "f")
        {
            RoosterOneDay(user);
        }
        else if (input0.ToLower() == "r")
        {
            Console.Clear();
            RoosterOneDay(user);
        }
    }


    public static void RoosterOneDay(bool user)
    {
        string json = File.ReadAllText("Rooster.json");
        var scheduleByDay = JsonConvert.DeserializeObject<Dictionary<string, List<MovieSchedule>>>(json);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Voer een dag in (Maandag, Dinsdag, Woensdag, etc.) of [T] om terug te gaan");
            string input = Console.ReadLine();
            input = Char.ToUpper(input[0]) + input.Substring(1);

            if (input.ToLower() == "t")
            {
                RoosterMenu(user);
            }
            else if (scheduleByDay.ContainsKey(input))
            {
                List<MovieSchedule> movieSchedules = scheduleByDay[input];

                Console.WriteLine(input);

                foreach (var movieSchedule in movieSchedules)
                {
                    Console.WriteLine($"Titel: {movieSchedule.Title}\n Tijd: {movieSchedule.Start} -  {movieSchedule.Ending}\n Zaal: {movieSchedule.Zaal}");
                    Console.WriteLine();
                }

                Console.WriteLine();

                Console.WriteLine("Wil je verder filteren?\n [J] - Ja\n [N] - Nee");
                string filter = Console.ReadLine();
                if (filter.ToLower() == "j")
                {
                    Console.WriteLine("Voer de titel van de film:");
                    string filmTitle = Console.ReadLine();

                    var selectedFilm = movieSchedules.Find(schedule => schedule.Title.Equals(filmTitle, StringComparison.OrdinalIgnoreCase));

                    if (selectedFilm != null)
                    {
                        Console.WriteLine($"Titel: {selectedFilm.Title}\n Tijd: {selectedFilm.Start} -  {selectedFilm.Ending}\n Zaal: {selectedFilm.Zaal}");
                    }
                    else
                    {
                        Console.WriteLine("Film niet gevonden. Probeer opnieuw.");
                    }

                    Console.WriteLine();

                    Console.WriteLine("[R] - Reserveer voor de film");
                    Console.WriteLine("[T] - Terug naar het rooster menu");
                    Console.WriteLine("Selecteer een van de opties: ");

                    string? input0 = Console.ReadLine();
                    if (input0?.ToLower() == "t")
                    {
                        Console.Clear();
                        RoosterMenu(user);
                    }
                    else if (input0.ToLower() == "r")
                    {
                        Console.Clear();
                        Roosterreserveer(selectedFilm.Zaal);
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid day entered. Please try again.");
                RoosterOneDay(user);
            }
        }
    }

    public static void Roosterreserveer(int zaal)
    {
        if (zaal == 1)
        {
            Reservering reserveer = new();
        }
        else if (zaal == 2)
        {
            ReserveringZaal2 reserveer = new();
        }
        else if (zaal == 3)
        {
            ReserveringZaal3 reserveer = new();
        }
    }
}





