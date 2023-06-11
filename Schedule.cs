using Newtonsoft.Json;

public class Rooster
{
    public static void RoosterMenu(bool user)
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
            Console.WriteLine("Keer terug naar het menu...");
            Menu.Start(user);
        }
        else
        {
            Console.WriteLine("Ongeldige invoer");
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
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"{day}\n----------------------------------------------------------------------------");

            foreach (var movieSchedule in movieSchedules)
            {
                Console.WriteLine($"Titel: {movieSchedule.Title}\n Tijd: {movieSchedule.Start} -  {movieSchedule.Ending}\n Zaal: {movieSchedule.Zaal}");
                Console.WriteLine();
            }
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
            Console.WriteLine("Voer een dag in (Maandag, Dinsdag, Woensdag, etc.");
            Console.WriteLine("[T] - Terug naar het roostermenu");
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

                Console.WriteLine("Wil je verder filteren?\n[J] - Ja\n[N] - Nee");
                string filter = Console.ReadLine();
                if (filter.ToLower() == "j")
                {
                    Console.WriteLine("Voer de titel van de film in:");
                    string filmTitle = Console.ReadLine();

                    var selectedFilm = movieSchedules.Find(schedule => schedule.Title.Equals(filmTitle, StringComparison.OrdinalIgnoreCase));

                    if (selectedFilm != null)
                    {
                        Console.WriteLine($"Titel: {selectedFilm.Title}\n Tijd: {selectedFilm.Start} -  {selectedFilm.Ending}\n Zaal: {selectedFilm.Zaal}");
                    }
                    else
                    {
                        Console.WriteLine("Film niet gevonden. Probeer het opnieuw.");
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
                        Roosterreserveer roosterreserveer = new(selectedFilm.Zaal);
                        //Roosterreserveer.Reser(user);
                    }
                }
                else if (filter.ToLower() == "n")
                {
                    Console.WriteLine("[T] - Terug naar het rooster menu");
                    Console.WriteLine("Selecteer de opties: ");

                    string? input0 = Console.ReadLine();
                    if (input0?.ToLower() == "t")
                    {
                        Console.Clear();
                        RoosterMenu(user);
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ongeldige invoer. Probeer opnieuw.");
                RoosterOneDay(user);
            }
        }
    }

    public static void Roosterreserveer(int zaal)
    {
        if (zaal == 1)
        {
            ReserveringsManager reserveer = new();
        }
        else if (zaal == 2)
        {
            ReserveringsManagerZaal2 reserveer = new();
        }
        else if (zaal == 3)
        {
            ReserveringsManagerZaal3 reserveer = new();
        }
    }
}





