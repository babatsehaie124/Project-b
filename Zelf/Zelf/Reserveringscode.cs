public class Reservering
{
    public static void Reserveren()
    {
        bool run = true;
        int seatsReserved = 0;

        bool[,] seats = new bool[10, 10];

        // zet alle stoelen beschikbaar
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                seats[i, j] = true;
            }
        }

        int currentRow = -1;
        int currentCol = -1;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (seats[i, j])
                {
                    currentRow = i;
                    currentCol = j;
                    break;
                }
            }

            if (currentRow != -1)
            {
                break;
            }
        }

        // Rond bewegen met pijl
        ConsoleKeyInfo keyInfo;
        int previousRow = -1;
        int previousCol = -1;

        do
        {
            // overzicht printen
            Console.Clear();
            Console.WriteLine("Huidige Zaal:\n");
            Console.WriteLine("  1  2  3  4  5  6  7  8  9  10");

            for (int i = 0; i < 10; i++)
            {
                Console.Write((i + 1) + " ");

                for (int j = 0; j < 10; j++)
                {
                    if (i == currentRow && j == currentCol)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write(" X ");
                        Console.ResetColor();
                    }
                    else if (seats[i, j])
                    {
                        Console.Write("[ ]");
                    }

                    else if (seats[i, j])

                    
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("X ");
                        Console.ResetColor();
                    }
                }

                Console.WriteLine();
            }

            keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (currentRow > 0 && seats[currentRow - 1, currentCol])
                    {
                        currentRow--;
                    }
                    continue;
                case ConsoleKey.DownArrow:
                    if (currentRow < 9 && seats[currentRow + 1, currentCol])
                    {
                        currentRow++;
                    }
                    continue;
                case ConsoleKey.LeftArrow:
                    if (currentCol > 0 && seats[currentRow, currentCol - 1])
                    {
                        currentCol--;
                    }
                    continue;
                case ConsoleKey.RightArrow:
                    if (currentCol < 9 && seats[currentRow, currentCol + 1])
                    {
                        currentCol++;
                    }
                    continue;
                
                case ConsoleKey.Enter:
                    if (seats[currentRow, currentCol])
                    {
                        seats[currentRow, currentCol] = false;

                        previousRow = currentRow;
                        previousCol = currentCol;

                        Console.WriteLine("Je hebt deze stoel gereserveerd: " + (currentRow + 1) + "," + (currentCol + 1), "\n");

                        seatsReserved++;

                        // Check of maximum aantal stoelen bereikt is
                        if (seatsReserved >= 10)
                        {
                            Console.WriteLine("Je hebt het maximum aantal stoelen per reservering bereikt.");
                            Console.WriteLine("Druk een knop om af te sluiten...");
                            Console.ReadKey();
                            run = false;
                        }
                        else
                        {
                            // vragen of gebruiker meer wilt reserveren
                            Console.WriteLine("Wil je meer stoelen reserveren? (j/n)");

                            bool validResponse = false;
                            while (!validResponse)
                            {
                                keyInfo = Console.ReadKey(true);

                                switch (keyInfo.Key)
                                {
                                    case ConsoleKey.J:
                                        validResponse = true;
                                        break;
                                    case ConsoleKey.N:
                                        validResponse = true;
                                        run = false;
                                        break;
                                    default:
                                        Console.WriteLine("Niet geldig antwoord. Beantwoord in een J of N graag.");
                                        break;
                                }
                            }
                        }
                    }
                    continue;

                case ConsoleKey.Backspace:
                    run = false;
                    break;
            }
        } while (keyInfo.Key != ConsoleKey.Enter && run);
    }
}

