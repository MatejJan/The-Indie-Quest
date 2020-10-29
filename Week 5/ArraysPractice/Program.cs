using System;

namespace ArraysPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Part3();
        }

        static void Part1()
        {
            var daysOfTheWeek = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            Console.WriteLine($"Days of the week are: {string.Join(", ", daysOfTheWeek)}");

            var daysThisMonth = new string[31];
            for (int i = 0; i < 31; i++)
            {
                int dayOfTheWeekIndex = (3 + i) % 7;
                daysThisMonth[i] = $"{i + 1}: {daysOfTheWeek[dayOfTheWeekIndex]}";
            }
            Console.WriteLine($"\nDays this month are: {string.Join(", ", daysThisMonth)}");

            var random = new Random();
            var randomLength = random.Next(5, 11);
            var randomNumbers = new double[randomLength];
            for (int i = 0; i < randomLength; i++)
            {
                randomNumbers[i] = random.Next(1, 11);
            }
            Console.WriteLine($"\n{randomLength} random numbers are: {string.Join(", ", randomNumbers)}");

            var interpolatedNumbers = new double[randomLength * 2 - 1];
            interpolatedNumbers[0] = randomNumbers[0];
            for (int i = 1; i < randomLength; i++)
            {
                interpolatedNumbers[i * 2] = randomNumbers[i];
                interpolatedNumbers[i * 2 - 1] = (randomNumbers[i] + randomNumbers[i - 1]) / 2;
            }
            Console.WriteLine($"Interpolated numbers are: {string.Join(", ", interpolatedNumbers)}");
        }

        static void Part2()
        {
            var random = new Random();

            void writeMatrix<T>(T[,] matrix)
            {
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        Console.Write(matrix[j, i]);
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Random matrix");
            var dx = random.Next(2, 6);
            var dy = random.Next(2, 6);

            var matrix = new int[dx, dy];

            for (int i = 0; i < dx; i++)
            {
                for (int j = 0; j < dy; j++)
                {
                    matrix[i, j] = random.Next(10);
                }
            }

            writeMatrix(matrix);

            Console.WriteLine("\nMatrix transpose");
            var matrixTranspose = new int[dy, dx];
            for (int i = 0; i < dy; i++)
            {
                for (int j = 0; j < dx; j++)
                {
                    matrixTranspose[i, j] = matrix[j, i];
                }
            }
            writeMatrix(matrixTranspose);

            Console.WriteLine("\nMultiplication table");
            var multiplicationTable = new int[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    multiplicationTable[i, j] = (i + 1) * (j + 1);
                    Console.Write($"{multiplicationTable[i, j],-2} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nKnight distance");
            var chessboard = new int[8, 8];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    chessboard[x, y] = Int32.MaxValue;
                }
            }

            void knightMove(int sx, int sy, int move)
            {
                // Make sure we're in bounds.
                if (sx < 0 || sx > 7 || sy < 0 || sy > 7) return;

                // Make sure we got here faster than any other way.
                if (chessboard[sx, sy] < move) return;

                // Mark this as the new minimum.
                chessboard[sx, sy] = move;

                // Continue moving.
                knightMove(sx + 1, sy + 2, move + 1);
                knightMove(sx + 2, sy + 1, move + 1);
                knightMove(sx - 1, sy + 2, move + 1);
                knightMove(sx - 2, sy + 1, move + 1);
                knightMove(sx + 1, sy - 2, move + 1);
                knightMove(sx + 2, sy - 1, move + 1);
                knightMove(sx - 1, sy - 2, move + 1);
                knightMove(sx - 2, sy - 1, move + 1);
            }

            knightMove(random.Next(8), random.Next(8), 0);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Console.Write(chessboard[x, y]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nTic tac toe");

            for (int gameIndex = 0; gameIndex < 3; gameIndex++)
            {
                Console.WriteLine();

                // Create an empty board.
                var board = new char[3, 3];
                string winText = null;

                // Keep placing marks until somebody wins.
                for (int moveIndex = 0; moveIndex < 9; moveIndex++)
                {
                    // Place a mark on a random empty location.
                    bool placed = false;

                    do
                    {
                        int x = random.Next(3);
                        int y = random.Next(3);
                        if (board[x, y] == '\0')
                        {
                            board[x, y] = moveIndex % 2 == 0 ? 'x' : 'o';
                            placed = true;
                        }
                    } while (!placed);

                    // See if we have a winner.
                    for (int i = 0; i < 3; i++)
                    {
                        if (board[i, 0] != '\0' && board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2])
                        {
                            winText = $"Win in column {i + 1} for {board[i, 0]}.";
                        }

                        if (board[0, i] != '\0' && board[0, i] == board[1, i] && board[0, i] == board[2, i])
                        {
                            winText = $"Win in row {i + 1} for {board[0, i]}.";
                        }
                    }

                    if (board[0, 0] != '\0' && board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2])
                    {
                        winText = $"Win on downwards diagonal for {board[0, 0]}.";
                    }

                    if (board[0, 2] != '\0' && board[0, 2] == board[1, 1] && board[0, 2] == board[2, 0])
                    {
                        winText = $"Win on upwards diagonal for {board[0, 2]}.";
                    }

                    if (winText != null) break;
                }

                // Output the final board.
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        Console.Write(board[x, y] != '\0' ? board[x, y] : ' ');
                    }
                    Console.WriteLine();
                }

                // Output the result.
                if (winText == null)
                {
                    Console.WriteLine("It's a tie.");
                }
                else
                {
                    Console.WriteLine(winText);
                }
            }
        }

        static void Part3()
        {
            Console.WriteLine("Days of the year");
            var daysOfTheWeek = new[] { "M", "T", "W", "T", "F", "S", "S" };
            var daysCountPerMonth = new[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            var daysOfTheYear = new string[12][];

            int dayOfTheWeekIndex = 2;
            for (int monthIndex = 0; monthIndex < 12; monthIndex++)
            {
                int daysCount = daysCountPerMonth[monthIndex];
                daysOfTheYear[monthIndex] = new string[daysCount];
                for (int dayIndex = 0; dayIndex < daysCount; dayIndex++)
                {
                    daysOfTheYear[monthIndex][dayIndex] = daysOfTheWeek[dayOfTheWeekIndex];
                    dayOfTheWeekIndex = (dayOfTheWeekIndex + 1) % 7;
                    Console.Write(daysOfTheYear[monthIndex][dayIndex]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nTeams");
            var random = new Random();
            int peopleCount = random.Next(15, 26);
            var people = new char[peopleCount];
            for (int i = 0; i < peopleCount; i++)
            {
                people[i] = (char)('A' + i);
            }

            int groupsCount;

            do
            {
                groupsCount = random.Next(3, 8);
            } while (peopleCount % groupsCount == 0);

            var groups = new char[groupsCount][];
            int minPeoplePerGroup = peopleCount / groupsCount;
            int groupsWithExtraPerson = peopleCount - groupsCount * minPeoplePerGroup;

            int firstPersonIndex = 0;
            for (int i = 0; i < groupsCount; i++)
            {
                int groupPeopleCount = minPeoplePerGroup;
                if (i < groupsWithExtraPerson)
                {
                    groupPeopleCount++;
                }
                groups[i] = new char[groupPeopleCount];

                for (int j = 0; j < groupPeopleCount; j++)
                {
                    groups[i][j] = people[firstPersonIndex + j];
                }
                firstPersonIndex += groupPeopleCount;

                Console.WriteLine($"Team {i + 1}: {String.Join(", ", groups[i])}");
            }

            Console.WriteLine("\nRace");
            var contestants = new char[groupsCount];
            for (int i = 0; i < groupsCount; i++)
            {
                int personIndex = random.Next(groups[i].Length);
                contestants[i] = groups[i][personIndex];
            }
            Console.WriteLine($"Contestants: {String.Join(", ", contestants)}");

            Console.WriteLine("\nField");
            int width = random.Next(3, 6);
            int height = random.Next(3, 6);
            var fields = new char[groupsCount][,];
            for (int i = 0; i < groupsCount; i++)
            {
                fields[i] = new char[width, height];
                char[] groupPeople = groups[i];

                for (int j = 0; j < groupPeople.Length; j++)
                {
                    bool placed = false;

                    do
                    {
                        int x = random.Next(width);
                        int y = random.Next(height);

                        if (fields[i][x, y] == '\0')
                        {
                            fields[i][x, y] = groupPeople[j];
                            placed = true;
                        }
                    } while (!placed);
                }

                Console.WriteLine($"Team {i + 1}:");
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Console.Write(fields[i][x, y] == '\0' ? '.' : fields[i][x, y]);
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\nField 2");
            var field = new bool[width, height];
            for (int i = 0; i < groupsCount; i++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (fields[i][x, y] != '\0')
                        {
                            field[x, y] = true;
                        }
                    }
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(field[x, y] ? '#' : '.');
                }
                Console.WriteLine();
            }
        }
    }
}
