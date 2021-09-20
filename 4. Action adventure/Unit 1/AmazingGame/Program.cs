using System;
using System.IO;

namespace AmazingGame
{
    class Program
    {
        static int width;
        static int height;
        static char[,] map;

        static int playerX;
        static int playerY;

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("MazeLevel.txt");

            string levelName = lines[0];

            string[] dimensionsText = lines[1].Split('x');

            width = int.Parse(dimensionsText[0]);
            height = int.Parse(dimensionsText[1]);

            map = new char[width, height];

            var random = new Random();

            for (int y = 0; y < height; y++)
            {
                string line = lines[y + 2];

                for (int x = 0; x < width; x++)
                {
                    char symbol = line[x];

                    if (symbol == 'S')
                    {
                        playerX = x;
                        playerY = y;
                        continue;
                    }

                    if (y < 3 && random.Next((y + 1) * 2) == 0)
                    {
                        symbol = '♠';
                    }

                    map[x, y] = symbol;
                }
            }

            DrawMap();
        }

        static void DrawMap()
        {
            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('☺');
                    }

                    char symbol = map[x, y];

                    if (symbol == 'M')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (symbol == '♠')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }
    }
}
