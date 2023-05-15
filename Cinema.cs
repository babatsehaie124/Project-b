    class Reservering
    {
        private const int ROW_COUNT = 10;
        private const int COL_COUNT = 20;
        private const char SEAT_AVAILABLE = 'O';
        private const char SEAT_TAKEN = 'X';
        private const char LOVESEAT_AVAILABLE = '♥';
        private const char LOVESEAT_TAKEN = '♡';

        private const char PREMIUMSEAT_AVAILABLE = '■';

        private static char[,] seats;
        private static int cursorRow = 0;
        private static int cursorCol = 0;

        public static void Reserveren()
        {
            InitializeSeats();
            bool ja = true;

            while (ja)
            {
                Console.Clear();
                PrintSeatingArea();
                PrintInstructions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow && cursorRow > 0)
                {
                    cursorRow--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && cursorRow < ROW_COUNT - 1)
                {
                    cursorRow++;
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow && cursorCol > 0)
                {
                    cursorCol--;
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow && cursorCol < COL_COUNT - 1)
                {
                    cursorCol++;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (seats[cursorRow, cursorCol] == SEAT_TAKEN)
                    {
                        Console.WriteLine("Sorry, deze stoel is al bezet.");
                    }
                    else
                    {
                        ReserveSeat(cursorRow, cursorCol);
                        Console.WriteLine($"Je hebt stoel {GetSeatRow(cursorRow, cursorCol)} gereserveerd.\n");
                        Console.WriteLine("Wil je nog meer stoelen reserveren? [Y] of [N]");
                        string reser_input = Console.ReadLine();
                        if (reser_input == "Y")
                        {
                            Reserveren();
                        }
                        else if (reser_input == "N")
                        {
                            Console.WriteLine("Je wordt teruggestuurd naar het menu...");
                            ja = false;
                            break;
                        }
                    }
                }
            }
        }

        private static void InitializeSeats()
        {
            seats = new char[ROW_COUNT, COL_COUNT];

            for (int row = 0; row < ROW_COUNT; row++)
            {
                for (int col = 0; col < COL_COUNT; col++)
                {
                    if (row == 5 && (col == 2 || col == 3 || col == 7 || 
                        col == 8 || col == 12 || col == 13 || col == 17|| col == 18))
                    {
                        seats[row, col] = LOVESEAT_AVAILABLE;
                    }
                    else if (row == 7 && (col == 2 || col == 7 || col == 12 || 
                        col == 17))
                    {
                        seats[row, col] = PREMIUMSEAT_AVAILABLE;
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
                    int row = seatName[0] - 'A';
                    int col = int.Parse(seatName.Substring(1)) - 1;
                    seats[row, col] = SEAT_TAKEN;
                }
            }
        }



        private static void PrintSeatingArea()
{
    Console.WriteLine("                           Scherm");
    Console.WriteLine("  --------------------------------------------------------------\n");

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
                Console.Write("[_]");
            }
            else if (seats[row, col] == SEAT_TAKEN)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[X]");
            }
            else if (seats[row, col] == LOVESEAT_AVAILABLE)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"[_{LOVESEAT_AVAILABLE}_]");
                Console.ResetColor();
                col++;
            }
            else if (seats[row, col] == PREMIUMSEAT_AVAILABLE)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"[_{PREMIUMSEAT_AVAILABLE}_]");
                Console.ResetColor();
                col++;
            }
            else
            {
                Console.Write("[_]");
            }

            Console.ResetColor();
        }

        Console.WriteLine("|");
    }
    Console.WriteLine("  --------------------------------------------------------------\n");
}


        private static void PrintInstructions()
        {
            Console.WriteLine("[_] = Beschikbare stoel");
            Console.WriteLine($" {LOVESEAT_AVAILABLE} = Love seats");
            Console.WriteLine($" {PREMIUMSEAT_AVAILABLE} = Premium seats");

            Console.WriteLine("[X] = Bezet\n");
            Console.WriteLine("Druk Enter om een stoel te selecteren.");
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


