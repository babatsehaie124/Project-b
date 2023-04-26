// public class Reservering
// {
//     private static readonly int totalSeatsReserved;

//     public static void Reserveren()
//     {
//         bool run = true;
//         bool reserveMoreSeats = false;
//         int numberOfReservedSeats = 0;

//         // Create a 2D array to represent the seats
//         bool[,] seats = new bool[10, 10];

//         // Initialize all seats to be available
//         for (int i = 0; i < 10; i++)
//         {
//             for (int j = 0; j < 10; j++)
//             {
//                 seats[i, j] = true;
//             }
//         }

//         // Initialize the current seat to be the first available seat
//         int currentRow = -1;
//         int currentCol = -1;

//         for (int i = 0; i < 10; i++)
//         {
//             for (int j = 0; j < 10; j++)
//             {
//                 if (seats[i, j])
//                 {
//                     currentRow = i;
//                     currentCol = j;
//                     break;
//                 }
//             }

//             if (currentRow != -1)
//             {
//                 break;
//             }
//         }

//         // Move around the seats using arrow keys
//         ConsoleKeyInfo keyInfo;
//         int previousRow = -1;
//         int previousCol = -1;

//         do
//         {
//             // Print the overview of seats
//             Console.Clear();
//             Console.WriteLine("Huidige Zaal:\n");
//             Console.WriteLine("  1 2 3 4 5 6 7 8 9 10");

//             for (int i = 0; i < 10; i++)
//             {
//                 Console.Write((i + 1) + " ");

//                 for (int j = 0; j < 10; j++)
//                 {
//                     if (i == currentRow && j == currentCol)
//                     {
//                         Console.BackgroundColor = ConsoleColor.Blue;
//                         Console.Write("X ");
//                         Console.ResetColor();
//                     }
//                     else if (seats[i, j])
//                     {
//                         Console.Write("[] ");
//                     }
//                     else
//                     {
//                         Console.BackgroundColor = ConsoleColor.Red;
//                         Console.Write("X ");
//                         Console.ResetColor();
//                     }
//                 }

//                 Console.WriteLine();
//             }

//             keyInfo = Console.ReadKey(true);

//             switch (keyInfo.Key)
//             {
//                 case ConsoleKey.UpArrow:
//                     if (currentRow > 0 && seats[currentRow - 1, currentCol])
//                     {
//                         currentRow--;
//                     }
//                     continue;
//                 case ConsoleKey.DownArrow:
//                     if (currentRow < 9 && seats[currentRow + 1, currentCol])
//                     {
//                         currentRow++;
//                     }
//                     continue;
//                 case ConsoleKey.LeftArrow:
//                     if (currentCol > 0 && seats[currentRow, currentCol - 1])
//                     {
//                         currentCol--;
//                     }
//                     continue;
//                 case ConsoleKey.RightArrow:
//                     if (currentCol < 9 && seats[currentRow, currentCol + 1])
//                     {
//                         currentCol++;
//                     }
//                     continue;
                
//         case ConsoleKey.Enter:
//             if (seats[currentRow, currentCol])
//             {
//                 seats[currentRow, currentCol] = false;

//                 previousRow = currentRow;
//                 previousCol = currentCol;

//                 Console.WriteLine("You have reserved seat " + (currentRow + 1) + "," + (currentCol + 1), "\n");

//                 seatsReserved++;

//                 // Check if the maximum number of seats have been reserved
//                 if (seatsReserved >= 10)
//                 {
//                     Console.WriteLine("You have reserved the maximum number of seats.");
//                     Console.WriteLine("Press any key to exit...");
//                     Console.ReadKey();
//                     run = false;
//                 }
//                 else
//                 {
//                     // Ask if the user wants to reserve more seats
//                     Console.WriteLine("Do you want to reserve another seat? (y/n)");

//                     bool validResponse = false;
//                     while (!validResponse)
//                     {
//                         keyInfo = Console.ReadKey(true);

//                         switch (keyInfo.Key)
//                         {
//                             case ConsoleKey.Y:
//                                 validResponse = true;
//                                 break;
//                             case ConsoleKey.N:
//                                 validResponse = true;
//                                 run = false;
//                                 break;
//                             default:
//                                 Console.WriteLine("Invalid response. Please enter y or n.");
//                                 break;
//                         }
//                     }
//                 }
//             }
//             continue;

//         case ConsoleKey.Backspace:
//             run = false;
//             break;
//             }
//         } while (keyInfo.Key != ConsoleKey.Enter && run);

//         // Ask the user if they want to reserve more seats
//         Console.WriteLine("Do you want to reserve more seats? (Y/N)");

//         while (true)
//         {
//             keyInfo = Console.ReadKey(true);

//             if (keyInfo.Key == ConsoleKey.Y)
//             {
//                 if (totalSeatsReserved == 10)
//                 {
//                     Console.WriteLine("You have already reserved the maximum number of seats (10).");
//                     break;
//                 }

//                 // Reset the current seat to be the first available seat
//                 currentRow = -1;
//                 currentCol = -1;

//                 for (int i = previousRow; i < 10; i++)
//                 {
//                     for (int j = 0; j < 10; j++)
//                     {
//                         if (seats[i, j])
//                         {
//                             currentRow = i;
//                             currentCol = j;
//                             break;
//                         }
//                     }

//                     if (currentRow != -1)
//                     {
//                         break;
//                     }
//                 }
//             }
//         }
//     }
// }

