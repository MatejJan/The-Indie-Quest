using System;
using System.Collections.Generic;

namespace AdventureMap
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                DrawMap(60, 20);

                var keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape) break;
            }
        }

        static List<int> GenerateCurve(int startingValue, int length, double curveChance)
        {
            var curveValues = new List<int>();
            int currentValue = startingValue;
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                curveValues.Add(currentValue);
                if (random.NextDouble() < curveChance)
                {
                    int direction = random.Next(2);
                    if (direction == 0) currentValue--;
                    if (direction == 1) currentValue++;
                }
            }

            return curveValues;
        }

        static void DrawMap(int width, int height)
        {
            // Prepare helper variable.
            int leftQuarterEnd = width / 4;
            int rightQuarterStart = width * 3 / 4;
            var random = new Random();

            // Generate river in the right quarter of the map.
            List<int> riverStart = GenerateCurve(rightQuarterStart, height, 0.5);

            // Generate a wall in the left quarter of the map.
            List<int> wallStart = GenerateCurve(leftQuarterEnd, height, 0.1);

            // Generate road in the middle of the map.
            var roadY = new List<int>();

            int currentRoadY = height / 2;

            for (int x = 0; x < width; x++)
            {
                roadY.Add(currentRoadY);

                // Are we away from the river?
                if (x >= riverStart[currentRoadY] - 2 && x <= riverStart[currentRoadY] + 6) continue;

                // Are we away from the wall?
                if (x >= wallStart[currentRoadY] - 1 && x <= wallStart[currentRoadY] + 2) continue;

                // Move the road slightly.
                int direction = random.Next(7);
                if (direction == 0 && currentRoadY > 1) currentRoadY--;
                if (direction == 1 && currentRoadY < height - 2) currentRoadY++;
            }

            // Find where the road intersection is.
            int roadIntersectionX = 0;
            for (int x = 0; x < width; x++)
            {
                // Are we 5 away from the river?
                if (x > riverStart[roadY[x]] - 5)
                {
                    roadIntersectionX = x;
                    break;
                }
            }

            int roadIntersectionY = roadY[roadIntersectionX];

            // Find where the title is positioned.
            var title = "ADVENTURE MAP";
            var titleX = (width - title.Length) / 2;

            // Draw the map.
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Are we at the border?
                    bool verticalBorder = x == 0 || x == width - 1;
                    bool horizontalBorder = y == 0 || y == height - 1;

                    Console.ForegroundColor = ConsoleColor.Yellow;

                    if (verticalBorder && horizontalBorder)
                    {
                        Console.Write("+");
                        continue;
                    }
                    else if (verticalBorder)
                    {
                        Console.Write("|");
                        continue;
                    }
                    else if (horizontalBorder)
                    {
                        Console.Write("-");
                        continue;
                    }

                    // Should we draw the title?
                    if (y == 1 && x == titleX)
                    {
                        Console.Write(title);

                        // Skip to the end of the title.
                        x += title.Length - 1;
                        continue;
                    }

                    // Are we on the bridge?
                    if ((y == roadY[x] - 1 || y == roadY[x] + 1) && x > riverStart[roadY[x]] - 3 && x < riverStart[roadY[x]] + 5)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("=");
                        continue;
                    }

                    // Are we on the gate?
                    if (y == roadY[x] - 1 || y == roadY[x] + 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                        if (x == wallStart[roadY[x]])
                        {
                            Console.Write("[");
                            continue;
                        }

                        if (x == wallStart[roadY[x]] + 1)
                        {
                            Console.Write("]");
                            continue;
                        }
                    }

                    // Are we on the middle road?
                    if (y == roadY[x])
                    {

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("#");
                        continue;
                    }

                    // Are we on the river road?
                    int riverRoadX = riverStart[y] - 5;
                    if (y > roadIntersectionY && x == riverRoadX)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("#");
                        continue;
                    }

                    // Are we in the river?
                    if (x >= riverStart[y] && x < riverStart[y] + 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        DrawCurve(riverStart, y);
                        continue;
                    }

                    // Are we in the wall?
                    if (x >= wallStart[y] && x < wallStart[y] + 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        DrawCurve(wallStart, y);
                        continue;
                    }

                    // Should we draw the forest?
                    if (x < leftQuarterEnd)
                    {
                        int forestInverseChance = x - 1;
                        if (random.Next(forestInverseChance) == 0)
                        {
                            // Draw one of the trees.
                            var trees = "AT@%()";

                            if (random.Next(2) == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                            }

                            Console.Write(trees[random.Next(trees.Length)]);
                            continue;
                        }
                    }

                    // We don't have to draw anything.
                    Console.Write(" ");
                }

                Console.WriteLine();
            }
        }

        static void DrawCurve(List<int> curve, int position)
        {
            // Which direction is the curve going in?
            int direction = curve[position + 1] - curve[position];

            if (direction == -1)
            {
                Console.Write("/");
            }
            else if (direction == 1)
            {
                Console.Write("\\");
            }
            else
            {
                Console.Write("|");
            }
        }
    }
}
