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

        Console.WriteLine();

        Roosterweek(user);
    }

    public static void Roosterweek(bool user)
    {
        string json = File.ReadAllText("Rooster.json");
        var scheduleByDay = JsonConvert.DeserializeObject<Dictionary<string, List<MovieSchedule>>>(json);

        foreach (var kvp in scheduleByDay)
        {
            string day = kvp.Key;
            List<MovieSchedule> movieSchedules = kvp.Value;

            // Sort movie schedules by start time
            movieSchedules.Sort((a, b) => DateTime.Parse(a.Start).CompareTo(DateTime.Parse(b.Start)));

            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"{day}\n----------------------------------------------------------------------------");

            foreach (var movieSchedule in movieSchedules)
            {
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine($"║   Titel: {movieSchedule.Title,-27} ");
                Console.WriteLine($"║   Tijd: {movieSchedule.Start}  - {movieSchedule.Ending}");
                Console.WriteLine($"║   Zaal: {movieSchedule.Zaal,-28} ");
                Console.WriteLine("╚════════════════════════════════════════════╝");
                // Console.WriteLine($"Titel: {movieSchedule.Title}\n Tijd: {movieSchedule.Start} -  {movieSchedule.Ending}\n Zaal: {movieSchedule.Zaal}");
                Console.WriteLine();
            }
        }

        Console.WriteLine("[F] - Filter rooster");
        Console.WriteLine("[R] - Reserveer voor een film");
        Console.WriteLine("[T] - Terug naar het menu");
        Console.WriteLine("Selecteer een van de opties: ");

        string? input0 = Console.ReadLine();
        Console.Clear();
        if (input0.ToLower() == "t")
        {
            Console.Clear();
            Console.WriteLine("Keer terug naar het menu...");
            Menu.Start(user);
        }
        else if (input0.ToLower() == "f")
        {
            Console.Clear();
            RoosterOneDay(user);
        }
        else if (input0.ToLower() == "r")
        {   
            Console.Clear();
            Reserveervoorfilm(user);
        }
        else
        {
            Console.WriteLine("Ongeldige invoer");
            Console.Clear();
            RoosterMenu(user);
        }
    }



    public static void Reserveervoorfilm(bool user)
    {
        Console.Clear();
        string json = File.ReadAllText("Rooster.json");
        var scheduleByDay = JsonConvert.DeserializeObject<Dictionary<string, List<MovieSchedule>>>(json);

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Voer een dag in (Maandag, Dinsdag, Woensdag, etc.)");
            Console.WriteLine("[T] - Terug naar het roostermenu");
            string input = Console.ReadLine();
            input = Char.ToUpper(input[0]) + input.Substring(1);

            if (input.ToLower() == "t")
            {
                Console.Clear();
                RoosterMenu(user);
            }
            else if (scheduleByDay.ContainsKey(input))
            {
                List<MovieSchedule> movieSchedules = scheduleByDay[input];

                Console.WriteLine(input);

                // Sort movie schedules by start time
                movieSchedules.Sort((a, b) => DateTime.Parse(a.Start).CompareTo(DateTime.Parse(b.Start)));

                foreach (var movieSchedule in movieSchedules)
                {
                    Console.WriteLine("╔════════════════════════════════════════════╗");
                    Console.WriteLine($"║   Titel: {movieSchedule.Title,-27} ");
                    Console.WriteLine($"║   Tijd: {movieSchedule.Start}  - {movieSchedule.Ending}");
                    Console.WriteLine($"║   Zaal: {movieSchedule.Zaal,-28} ");
                    Console.WriteLine("╚════════════════════════════════════════════╝");
                    // Console.WriteLine($"Titel: {movieSchedule.Title}\n Tijd: {movieSchedule.Start} -  {movieSchedule.Ending}\n Zaal: {movieSchedule.Zaal}");
                    Console.WriteLine();
                }

                Console.WriteLine();
                

                Console.WriteLine("Voer de titel van de film in:");
                string filmTitle = Console.ReadLine();
                Console.Clear();

                var selectedFilm = movieSchedules.Find(schedule => schedule.Title.Equals(filmTitle, StringComparison.OrdinalIgnoreCase));

                if (selectedFilm != null)
                {
                    Console.Clear();
                    Console.WriteLine(input);
                    Console.WriteLine("╔════════════════════════════════════════════╗");
                    Console.WriteLine($"║   Titel: {selectedFilm.Title,-27} ");
                    Console.WriteLine($"║   Tijd: {selectedFilm.Start}  - {selectedFilm.Ending}");
                    Console.WriteLine($"║   Zaal: {selectedFilm.Zaal,-28} ");
                    Console.WriteLine("╚════════════════════════════════════════════╝");
                    // Console.WriteLine($"Titel: {selectedFilm.Title}\n Tijd: {selectedFilm.Start} -  {selectedFilm.Ending}\n Zaal: {selectedFilm.Zaal}");
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
                    Roosterreserveer roosterreserveer = new Roosterreserveer(selectedFilm.Zaal, selectedFilm.Rooster_Id);
                    Roosterreserveer.Reserve(user, selectedFilm.Rooster_Id);
                    //Roosterreserveer.Reser(user);
                }
            }
        }
    }


    public static void RoosterOneDay(bool user)
    {
        Console.Clear();
        string json = File.ReadAllText("Rooster.json");
        var scheduleByDay = JsonConvert.DeserializeObject<Dictionary<string, List<MovieSchedule>>>(json);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Voer een dag in (Maandag, Dinsdag, Woensdag, etc.)");
            Console.WriteLine("[T] - Terug naar het roostermenu");
            string input = Console.ReadLine();
            input = Char.ToUpper(input[0]) + input.Substring(1);
            Console.Clear();


            if (input.ToLower() == "t")
            {
                Console.Clear();
                RoosterMenu(user);
            }
            else if (scheduleByDay.ContainsKey(input))
            {
                Console.Clear();

                List<MovieSchedule> movieSchedules = scheduleByDay[input];

                Console.WriteLine(input);

                // Sort movie schedules by start time
                movieSchedules.Sort((a, b) => DateTime.Parse(a.Start).CompareTo(DateTime.Parse(b.Start)));

                foreach (var movieSchedule in movieSchedules)
                {
                    Console.WriteLine("╔════════════════════════════════════════════╗");
                    Console.WriteLine($"║   Titel: {movieSchedule.Title,-27} ");
                    Console.WriteLine($"║   Tijd: {movieSchedule.Start}  - {movieSchedule.Ending}");
                    Console.WriteLine($"║   Zaal: {movieSchedule.Zaal,-28} ");
                    Console.WriteLine("╚════════════════════════════════════════════╝");
                    // Console.WriteLine($"Titel: {movieSchedule.Title}\n Tijd: {movieSchedule.Start} -  {movieSchedule.Ending}\n Zaal: {movieSchedule.Zaal}");
                    Console.WriteLine();
                }

                Console.WriteLine();

                Console.WriteLine("Wil je verder filteren?\n[J] - Ja\n[N] - Nee");
                string filter = Console.ReadLine();
                if (filter.ToLower() == "j")
                {
                    // Console.Clear();
                    Console.WriteLine("Voer de titel van de film in:");
                    string filmTitle = Console.ReadLine();
                    Console.Clear();

                    var selectedFilm = movieSchedules.Find(schedule => schedule.Title.Equals(filmTitle, StringComparison.OrdinalIgnoreCase));

                    if (selectedFilm != null)
                    {
                        Console.Clear();
                        Console.WriteLine(input);
                        Console.WriteLine("╔════════════════════════════════════════════╗");
                        Console.WriteLine($"║   Titel: {selectedFilm.Title,-27} ");
                        Console.WriteLine($"║   Tijd: {selectedFilm.Start}  - {selectedFilm.Ending}");
                        Console.WriteLine($"║   Zaal: {selectedFilm.Zaal,-28} ");
                        Console.WriteLine("╚════════════════════════════════════════════╝");
                        // Console.WriteLine($"Titel: {selectedFilm.Title}\n Tijd: {selectedFilm.Start} -  {selectedFilm.Ending}\n Zaal: {selectedFilm.Zaal}");
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
                        Roosterreserveer roosterreserveer = new Roosterreserveer(selectedFilm.Zaal, selectedFilm.Rooster_Id);
                        Roosterreserveer.Reserve(user, selectedFilm.Rooster_Id);
                        // Roosterreserveer.Reser(user);
                    }
                }
                else if (filter.ToLower() == "n")
                {
                    // Console.Clear();
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

}
