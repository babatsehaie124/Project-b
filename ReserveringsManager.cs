using Newtonsoft.Json;
using System;
class ReserveringsManager
{
    private const int ROW_COUNT = 10;
    private const int COL_COUNT = 15;
    private const char SEAT_AVAILABLE = 'O';
    private const char SEAT_TAKEN = 'X';
    private const char LOVESEAT_AVAILABLE = '♥';
    private const char LOVESEAT_TAKEN = '♡';
    private const char SELECT_SEAT = '-';

    private const char PREMIUMSEAT_AVAILABLE = '■';

    private static char[,] seats;
    private static int cursorRow = 0;
    private static int cursorCol = 0;

    private static int selectedRow = -1;
    private static int selectedCol = -1;

    private static Reservering? currentReservation;

    private static List<(int, int)> selectedSeats = new();

    private static string latestError = "";

    private static List<(int, int)> LoveSeats = new();
    private static List<(int, int)> PremiumSeats = new();
    public static void Reserveren(bool user, int Rooster_Id)
    {
        currentReservation = new(1);
        InitializeSeats();
        bool seatChosen = false;
        int reservedSeatCount = 0;

        do
        {
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
            DrawSeats();

            if (latestError != "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(latestError + "\n");
                Console.ResetColor();
                latestError = "";
            }

            PrintInstructions();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (cursorRow > 0)
                    {
                        if (seats[cursorRow - 1, cursorCol] == LOVESEAT_AVAILABLE && cursorCol > 0 && seats[cursorRow - 1, cursorCol - 1] == LOVESEAT_AVAILABLE)
                        {
                            cursorCol--;
                        }
                        cursorRow--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (cursorRow < ROW_COUNT - 1)
                    {
                        if (seats[cursorRow + 1, cursorCol] == LOVESEAT_AVAILABLE && cursorCol > 0 && seats[cursorRow + 1, cursorCol - 1] == LOVESEAT_AVAILABLE)
                        {
                            cursorCol--;
                        }
                        cursorRow++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (cursorCol > 0)
                    {
                        if (seats[cursorRow, cursorCol - 1] == LOVESEAT_AVAILABLE)
                        {
                            cursorCol -= 1;
                        }
                        cursorCol--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (cursorCol < COL_COUNT - 1)
                    {
                        if (seats[cursorRow, cursorCol] == LOVESEAT_AVAILABLE)
                        {
                            cursorCol += 1;
                        }
                        cursorCol++;
                    }
                    break;
                case ConsoleKey.Enter:
                    currentReservation.Stoelen.Clear();
                    foreach (var seat in selectedSeats)
                    {
                        currentReservation.Stoelen.Add(GetSeatRow(seat.Item1, seat.Item2));
                        Console.WriteLine($"Je hebt stoel {GetSeatRow(seat.Item1, seat.Item2)} geselecteerd.\n");
                    }
                    System.Console.WriteLine("Je wordt doorverwezen...");
                    Thread.Sleep(3000);
                    currentReservation.RoosterId = Rooster_Id;
                    currentReservation.SaveAsCurrent();
                    Console.Clear();
                    ChooseFood.PickFood(user);
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Menu.Start(user);
                    break;
                case ConsoleKey.Spacebar:
                    TryToSelectSeat(cursorRow, cursorCol, reservedSeatCount);
                    break;
            }

        } while (seatChosen == false);

    }

    private static void InitializeSeats()
    {
        seats = new char[ROW_COUNT, COL_COUNT];

        for (int row = 0; row < ROW_COUNT; row++)
        {
            for (int col = 0; col < COL_COUNT; col++)
            {
                if (row == 5 && (col == 1 || col == 2 || col == 5 ||
                    col == 6 || col == 9 || col == 10 || col == 12 || col == 13))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    seats[row, col] = LOVESEAT_AVAILABLE;
                    LoveSeats.Add((row, col));
                    Console.ResetColor();
                }
                else if (row == 7 && (col == 1 || col == 2 || col == 5 ||
                        col == 6 || col == 9 || col == 10 || col == 12 || col == 13))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    seats[row, col] = PREMIUMSEAT_AVAILABLE;
                    PremiumSeats.Add((row, col));
                    Console.ResetColor();
                }
                else
                {
                    seats[row, col] = SEAT_AVAILABLE;
                }
            }
        }
        // hier controleert hij de huidig beschikbare stoelen
        currentReservation.LoadFromCurrent();
        foreach (string seatName in currentReservation.Stoelen)
        {
            if (seatName.Length >= 2)
            {
                int row = seatName[0] - 'A';
                int col = int.Parse(seatName.Substring(1)) - 1;

                if (row >= 0 && row < ROW_COUNT && col >= 0 && col < COL_COUNT)
                {
                    seats[row, col] = SEAT_TAKEN;
                }
            }
        }
    }

    private static void TryToSelectSeat(int row, int col, int reservedSeatCount)
    {
        if (seats[cursorRow, cursorCol] == SEAT_TAKEN)
        {
            latestError = "Deze stoel is al bezet, kies een andere stoel";
        }
        else if (seats[cursorRow, cursorCol] == SELECT_SEAT)
        {
            if (LoveSeats.Contains((row, col)))
            {
                seats[row, col] = LOVESEAT_AVAILABLE;
                if (LoveSeats.Contains((row, col + 1)))
                {
                    seats[row, col + 1] = LOVESEAT_AVAILABLE;
                }
                else if (LoveSeats.Contains((row, col - 1)))
                {
                    seats[row, col - 1] = LOVESEAT_AVAILABLE;
                }
                currentReservation.Stoelen.Remove(GetSeatRow(row, col));
            }
            else if (PremiumSeats.Contains((row, col)))
            {
                seats[row, col] = PREMIUMSEAT_AVAILABLE;
                currentReservation.Stoelen.Remove(GetSeatRow(row, col));
            }
            else
            {
                seats[cursorRow, cursorCol] = SEAT_AVAILABLE;
                selectedSeats.Remove((cursorRow, cursorCol));
                reservedSeatCount--;
            }
        }
        else if (reservedSeatCount == 10 || reservedSeatCount + selectedSeats.Count() == 10)
        {
            latestError = "Maximaal aantal stoelen bereikt.";
        }
        else
        {
            if (seats[cursorRow, cursorCol] == LOVESEAT_AVAILABLE)
            {
                seats[cursorRow, cursorCol] = SELECT_SEAT;
                seats[cursorRow, cursorCol + 1] = SELECT_SEAT;
                selectedSeats.Add((cursorRow, cursorCol));
                selectedSeats.Add((cursorRow, cursorCol + 1));
                reservedSeatCount = reservedSeatCount + 2;
            }
            else if (seats[cursorRow, cursorCol] == PREMIUMSEAT_AVAILABLE)
            {
                seats[cursorRow, cursorCol] = SELECT_SEAT;
                selectedSeats.Add((cursorRow, cursorCol));
                reservedSeatCount++;
            }
            else
            {
                seats[cursorRow, cursorCol] = SELECT_SEAT;
                selectedSeats.Add((cursorRow, cursorCol));
                reservedSeatCount++;
            }
        }
    }


    private static void DrawSeats()
    {
        Console.WriteLine("                   Scherm Zaal 1");
        Console.WriteLine("  -----------------------------------------------\n");
        Console.WriteLine("Uitgang                                   Uitgang");

        Console.Write("   ");
        for (int col = 0; col < COL_COUNT; col++)
        {
            Console.Write($"{col + 1,2} ");
        }
        Console.WriteLine();

        for (int row = 0; row < ROW_COUNT; row++)
        {
            Console.Write($"{(char)('A' + row)} |");
            for (int col = 0; col < COL_COUNT; col++)
            {
                if (row == cursorRow && col == cursorCol)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                if (seats[row, col] == SEAT_TAKEN)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[X]");
                }
                else if (seats[row, col] == SELECT_SEAT)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("[-]");
                    Console.ResetColor();
                }
                else if (seats[row, col] == LOVESEAT_AVAILABLE)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($"[_{LOVESEAT_AVAILABLE}{LOVESEAT_AVAILABLE}_]");
                    Console.ResetColor();
                    col++;
                }
                else if (seats[row, col] == PREMIUMSEAT_AVAILABLE)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"[{PREMIUMSEAT_AVAILABLE}]");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("[_]");
                }

                Console.ResetColor();
            }

            Console.WriteLine("|");
        }
        Console.WriteLine("  -------------------Projector-------------------\n");
    }

    private static void PrintInstructions()
    {
        Console.WriteLine("[_] = Normale seats");

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($" {LOVESEAT_AVAILABLE} = Love seats");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($" {PREMIUMSEAT_AVAILABLE} = Premium seats");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("X = Bezet");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("- = Gelesecteerd\n");
        Console.ResetColor();

        Console.WriteLine("Gebruik de pijltjes om rond te bewegen");
        Console.WriteLine("[Space] - Selecteer een stoel");
        Console.WriteLine("[Enter] - Ga door met je reservering");
        Console.WriteLine("[Esc]   - Keer terug naar het menu");
        Console.WriteLine();
    }

    private static void SaveSeatsData()
    {
        List<Reservering> reservations = new List<Reservering>();
        if (File.Exists("HuidigeReservering.json"))
        {
            string jsonData = File.ReadAllText("HuidigeReservering.json");
            reservations = JsonConvert.DeserializeObject<List<Reservering>>(jsonData);
        }
        Reservering newReservation = new Reservering(1);
        for (int row = 0; row < ROW_COUNT; row++)
        {
            for (int col = 0; col < COL_COUNT; col++)
            {
                if (seats[row, col] == SELECT_SEAT)
                {
                    string seatName = GetSeatRow(row, col);
                    newReservation.Stoelen.Add(seatName);
                }
            }
        }
        reservations.Add(newReservation);
        string updatedJsonData = JsonConvert.SerializeObject(reservations);
        File.WriteAllText("HuidigeReservering.json", updatedJsonData);
    }
    private static char SelectSeat(int row, int col)
    {
        if (seats[row, col - 1] == LOVESEAT_AVAILABLE)
        {
            seats[row, col - 1] = SELECT_SEAT;
            return SELECT_SEAT;
        }
        else if (seats[row, col + 1] == LOVESEAT_AVAILABLE)
        {
            seats[row, col + 1] = SELECT_SEAT;
            return SELECT_SEAT;
        }

        if (seats[row, col] == SELECT_SEAT)
        {
            seats[row, col] = SEAT_AVAILABLE;
            currentReservation.Stoelen.Remove(GetSeatRow(row, col));
            return SEAT_AVAILABLE;
        }
        else
        {
            seats[row, col] = SELECT_SEAT;
            currentReservation.Stoelen.Add(GetSeatRow(row, col));
            return SELECT_SEAT;
        }
    }
    private static string GetSeatRow(int row, int col)
    {
        char rowName = (char)('A' + row);
        int seatNumber = col + 1;
        return $"{rowName}{seatNumber}";
    }
}

public class Gereserveerd
{
    private List<Reservering> _reserveringen = new();

    public Gereserveerd()
    {
        string jsonData = File.ReadAllText("HuidigeReservering.json");
        this._reserveringen = JsonConvert.DeserializeObject<List<Reservering>>(jsonData);
    }

    public void Add(Reservering _reservering)
    {
        this._reserveringen.Append(_reservering);
    }

    public List<Reservering> FindByScheduleId(int roosterId)
    {
        return this._reserveringen.Where(r => r.RoosterId == roosterId).ToList();
    }
}