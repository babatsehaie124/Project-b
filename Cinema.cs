using System;

namespace CinemaReservation
{
    class Program
    {
        private const int ROW_COUNT = 10;
        private const int COL_COUNT = 20;
        private const char SEAT_AVAILABLE = 'O';
        private const char SEAT_TAKEN = 'X';
        private const char LOVESEAT_AVAILABLE = '♥';
        private const char LOVESEAT_TAKEN = '♡';

        private static char[,] seats;
        private static int cursorRow = 0;
        private static int cursorCol = 0;

        static void Main(string[] args)
        {
            InitializeSeats();

            while (true)
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
                        Console.ReadLine();
                    }
                    else
                    {
                        ReserveSeat(cursorRow, cursorCol);
                        Console.WriteLine($"je hebt {GetSeatName(cursorRow, cursorCol)} gereserveerd.");
                        Console.ReadLine();
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
                    if (row == 5 && (col == 4 || col == 5 || col == 9 || col == 10 || col == 14 || col == 15))
                    {
                        seats[row, col] = LOVESEAT_AVAILABLE;
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
            Console.WriteLine("     -------------------------------------------------");
            Console.WriteLine(" ");

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
                        Console.Write("[__]");
                        Console.ResetColor();
                        col++;
                    }
                    else if (seats[row, col] == LOVESEAT_TAKEN)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("[XX]");
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

            Console.WriteLine("   -----------------------------------------------------------");
        }

        private static void PrintInstructions()
        {
            Console.WriteLine("[_] = Beschikbare stoel");
            Console.WriteLine("[__] = Liefde stoelen");
            Console.WriteLine("[X] = bezet");
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
                            string seatName = GetSeatName(row, col);
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

        private static string GetSeatName(int row, int col)
        {
            char rowName = (char)('A' + row);
            int seatNumber = col + 1;
            return $"{rowName}{seatNumber}";
        }
    }

}

