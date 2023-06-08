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

    private static Reservering currentReservation;

    public static void Reserveren(bool user)
    {
        currentReservation = new(1);
        InitializeSeats();
        bool ja = true;
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
            PrintSeatingArea();
            PrintInstructions();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.UpArrow && cursorRow > 0)
            {
                if (seats[cursorRow - 1, cursorCol] == LOVESEAT_AVAILABLE && cursorCol > 0 && seats[cursorRow - 1, cursorCol - 1] == LOVESEAT_AVAILABLE)
                {
                    cursorCol--;
                }
                cursorRow--;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow && cursorRow < ROW_COUNT - 1)
            {
                if (seats[cursorRow + 1, cursorCol] == LOVESEAT_AVAILABLE && cursorCol > 0 && seats[cursorRow + 1, cursorCol - 1] == LOVESEAT_AVAILABLE)
                {
                    cursorCol--;
                }
                cursorRow++;
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow && cursorCol > 0)
            {
                if (seats[cursorRow, cursorCol - 1] == LOVESEAT_AVAILABLE)
                {
                    cursorCol -= 2;
                }
                else
                {
                    cursorCol--;
                }
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow && cursorCol < COL_COUNT - 1)
            {
                if (seats[cursorRow, cursorCol] == LOVESEAT_AVAILABLE)
                {
                    if (cursorCol + 1 < COL_COUNT && seats[cursorRow, cursorCol + 1] == LOVESEAT_AVAILABLE)
                    {
                        cursorCol += 2;
                    }
                    else
                    {
                        cursorCol++;
                    }
                }
                else
                {
                    cursorCol++;
                }
            }

            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Je wordt teruggestuurd naar het menu...\n");
                Menu.Start(user);
            }

            else if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                if (selectedRow == -1 && selectedCol == -1)
                {
                    selectedRow = cursorRow;
                    selectedCol = cursorCol;
                }
                else
                {
                    if (seats[selectedRow, selectedCol] == SEAT_TAKEN)
                    {
                        Console.WriteLine("Sorry, deze stoel is al bezet.");
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        SelectSeat(selectedRow, selectedCol);
                        Console.WriteLine($"Je hebt stoel {GetSeatRow(selectedRow, selectedCol)} geselecteerd.\n");
                        reservedSeatCount++;

                        selectedRow = -1;
                        selectedCol = -1;

                        if (reservedSeatCount >= 10)
                        {
                            Console.WriteLine("Het maximale aantal stoelen per Reserverings is bereikt. Je kunt niet meer stoelen selecteren.");
                            break;
                        }
                        Console.WriteLine("Wil je nog meer stoelen selecteren? [J] of [N]");
                        string reser_input = Console.ReadLine().ToUpper();
                        if (reser_input == "N")
                        {
                            // doorverstuurd naar eten
                            Console.WriteLine("Je wordt doorverwezen...\n");
                            Thread.Sleep(3000);
                            //Choosefood.PickFood();
                            ja = false;
                            Menu.Start(user);
                            break;
                        }
                    }
                }
            }
        } while (ja == true);


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
                    seats[row, col] = LOVESEAT_AVAILABLE;
                }
                else if (row == 7 && (col == 1 || col == 2 || col == 5 ||
                        col == 6 || col == 9 || col == 10 || col == 12 || col == 13))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    seats[row, col] = PREMIUMSEAT_AVAILABLE;
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




    private static void PrintSeatingArea()
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
        Console.WriteLine("Druk [Space] om een stoel te selecteren");
        Console.WriteLine("Druk [Esc] om terug te keren naar het menu");
        Console.WriteLine();
    }

    private static void SaveSeatsData()
    {
        // currentReservation.Seats.
        using (StreamWriter writer = new StreamWriter("selected_seats1.txt"))
        {
            for (int row = 0; row < ROW_COUNT; row++)
            {
                for (int col = 0; col < COL_COUNT; col++)
                {
                    if (seats[row, col] == SELECT_SEAT)
                    {
                        string seatName = GetSeatRow(row, col);
                        writer.WriteLine(seatName);
                    }
                }
            }
        }
    }

    private static void SelectSeat(int row, int col)
    {

        if (seats[row, col - 1] == LOVESEAT_AVAILABLE)
        {
            seats[row, col - 1] = SELECT_SEAT;
        }
        else if (seats[row, col + 1] == LOVESEAT_AVAILABLE)
        {
            seats[row, col + 1] = SELECT_SEAT;
        }

        if (seats[row, col] == SELECT_SEAT)
        {
            seats[row, col] = SEAT_AVAILABLE;
            currentReservation.Stoelen.Remove(GetSeatRow(row, col));
            Console.WriteLine($"Je hebt stoel {GetSeatRow(selectedRow, selectedCol)} gedeselecteerd.\n");
        }
        else
        {
            seats[row, col] = SELECT_SEAT;
            currentReservation.Stoelen.Add(GetSeatRow(row, col));
        }

        currentReservation.SaveAsCurrent();
    }
    private static void ReserveSeat(int row, int col)
    {
        // rushil moet dit aanroepen zodra eten en drinken 
        if (seats[row, col - 1] == LOVESEAT_AVAILABLE)
        {
            seats[row, col - 1] = SEAT_TAKEN;
        }
        else if (seats[row, col + 1] == LOVESEAT_AVAILABLE)
        {
            seats[row, col + 1] = SEAT_TAKEN;
        }
        seats[row, col] = SEAT_TAKEN;
        // SaveSeatsData();
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
        //this._reserveringen = ...;
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