using System;

namespace CityGenerator
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

        static void GenerateRoad(bool[,] roads, int x, int y, int direction)
        {
            int width = roads.GetLength(0);
            int height = roads.GetLength(1);

            while (x >= 0 && x < width && y >= 0 && y < height)
            {
                roads[x, y] = true;

                if (direction == 0) x++;
                if (direction == 1) y++;
                if (direction == 2) x--;
                if (direction == 3) y--;
            }
        }

        static void GenerateIntersection(bool[,] roads, int x, int y)
        {
            var random = new Random();

            for (int direction = 0; direction < 4; direction++)
            {
                if (random.NextDouble() < 0.7) GenerateRoad(roads, x, y, direction);
            }
        }

        static void DrawMap(int width, int height)
        {
            var random = new Random();

            var roads = new bool[width, height];

            // Generate roads.
            for (var i = 0; i < 5; i++)
                GenerateIntersection(roads, random.Next(width), random.Next(height));

            // Find where the title is positioned.
            var title = "CITY MAP";
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

                    // Are we on a road?
                    if (roads[x, y])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                        bool up = roads[x, y - 1];
                        bool down = roads[x, y + 1];
                        bool left = roads[x - 1, y];
                        bool right = roads[x + 1, y];

                        if (up && down && left && right)
                        {
                            Console.Write("╬");
                            continue;
                        }

                        if (up && down && left)
                        {
                            Console.Write("╣");
                            continue;
                        }

                        if (up && down && right)
                        {
                            Console.Write("╠");
                            continue;
                        }

                        if (up && left && right)
                        {
                            Console.Write("╩");
                            continue;
                        }

                        if (down && left && right)
                        {
                            Console.Write("╦");
                            continue;
                        }

                        if (left && up)
                        {
                            Console.Write("╝");
                            continue;
                        }

                        if (left && down)
                        {
                            Console.Write("╗");
                            continue;
                        }

                        if (up && right)
                        {
                            Console.Write("╚");
                            continue;
                        }

                        if (right && down)
                        {
                            Console.Write("╔");
                            continue;
                        }

                        if (up || down)
                        {
                            Console.Write("║");
                            continue;
                        }

                        Console.Write("═");
                        continue;
                    }

                    // We don't have to draw anything.
                    Console.Write(" ");
                }

                Console.WriteLine();
            }
        }
    }
}
