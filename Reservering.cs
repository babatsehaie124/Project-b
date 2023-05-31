class Reservering
{
    private const int ROW_COUNT = 10;
    private const int COL_COUNT = 15;
    private const char SEAT_AVAILABLE = 'O';
    private const char SEAT_TAKEN = 'X';
    private const char LOVESEAT_AVAILABLE = '♥';
    private const char LOVESEAT_TAKEN = '♡';

    private const char PREMIUMSEAT_AVAILABLE = '■';

    private static char[,] seats;
    private static int cursorRow = 0;
    private static int cursorCol = 0;

    private static int selectedRow = -1;
    private static int selectedCol = -1;



    public static void Reserveren(bool user)
    {
        InitializeSeats();
        bool ja = true;
        int reservedSeatCount = 0;

        do
        {

            Console.Clear();
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
                Console.WriteLine("Je wordt teruggestuurd naar het menu...\n");
                Menu.Start(user);
            }

            else if (keyInfo.Key == ConsoleKey.Enter)
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
                        ReserveSeat(selectedRow, selectedCol);
                        Console.WriteLine($"Je hebt stoel {GetSeatRow(selectedRow, selectedCol)} gereserveerd.\n");
                        reservedSeatCount++;

                        selectedRow = -1;
                        selectedCol = -1;

                        if (reservedSeatCount >= 10)
                        {
                            Console.WriteLine("Het maximale aantal stoelen per reservering is bereikt. Je kunt niet meer stoelen reserveren.");
                            break;
                        }
                        Console.WriteLine("Wil je nog meer stoelen reserveren? [J] of [N]");
                        string reser_input = Console.ReadLine().ToUpper();
                        if (reser_input == "N")
                        {
                            Console.WriteLine("Je wordt teruggestuurd naar het menu...");
                            Thread.Sleep(3000);
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
                col == 6 || col == 9 || col == 10 || col == 13 || col == 14))
            {
                seats[row, col] = LOVESEAT_AVAILABLE;
            }
            else if (row == 7 && (col == 1 || col == 2 || col == 5 ||
                    col == 6 || col == 9 || col == 10 || col == 13 || col == 14))
            {
                Console.BackgroundColor = ConsoleColor.White;
                seats[row, col] = PREMIUMSEAT_AVAILABLE;
                Console.ResetColor();
            }
            else if (row == selectedRow && col == selectedCol)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                seats[row, col] = SEAT_AVAILABLE;
            }
        }
    }

    if (File.Exists("reserved_seats.txt"))
    {
        string[] reservedSeats = File.ReadAllLines("reserved_seats.txt");
        foreach (string seatName in reservedSeats)
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
        Console.WriteLine("[X] = Bezet\n");
        Console.ResetColor();

        Console.WriteLine("Gebruik de pijltjes om rond te bewegen");
        Console.WriteLine("Druk Enter om een stoel te reserveren.");
        Console.WriteLine("Druk Esc om terug te keren naar het menu");
        Console.WriteLine();
    }

    private static void SaveReservedSeatsData()
    {
        using (StreamWriter writer = new StreamWriter("reserved_seats.txt"))
        {
            for (int row = 0; row < ROW_COUNT; row++)
            {
                for (int col = 0; col < COL_COUNT; col++)
                {
                    if (seats[row, col] == SEAT_TAKEN)
                    {
                        string seatName = GetSeatRow(row, col);
                        writer.WriteLine(seatName);
                    }
                }
            }
        }
    }


    private static void ReserveSeat(int row, int col)
    {

        if (seats[row, col - 1] == LOVESEAT_AVAILABLE)
        {
            seats[row, col - 1] = SEAT_TAKEN;
        }
        else if (seats[row, col + 1] == LOVESEAT_AVAILABLE)
        {
            seats[row, col + 1] = SEAT_TAKEN;
        }
        seats[row, col] = SEAT_TAKEN;
        SaveReservedSeatsData();
    }

    private static string GetSeatRow(int row, int col)
    {
        char rowName = (char)('A' + row);
        int seatNumber = col + 1;
        return $"{rowName}{seatNumber}";
    }
}