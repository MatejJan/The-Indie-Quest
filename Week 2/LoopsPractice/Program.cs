using System;

namespace LoopsPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
        }

        static void Part1()
        {
            Console.Write("Enter number: ");
            int n = Int32.Parse(Console.ReadLine());

            Console.WriteLine("\nLine:");
            for (int i = 0; i < n; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            Console.WriteLine("\nSquare:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nRight triangle:");
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nParallelogram:");
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    Console.Write(" ");
                }
                for (int j = 0; j < n; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nIsosceles triangle:");
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    Console.Write(" ");
                }
                for (int j = 0; j < i * 2 - 1; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nRows:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(i % 2 == 0 ? "#" : " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nColumns:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(j % 2 == 0 ? "#" : " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nGrid:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(i % 2 == 0 || j % 2 == 0 ? "#" : " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nFence:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(i % 2 == 0 && j % 2 == 0 ? " " : "#");
                }
                Console.WriteLine();
            }


            Console.WriteLine("\nChessboard:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(i % 2 == j % 2 ? "#" : " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nSlope:");
            int width = 1;
            while (width < 80)
            {
                for (int i = 0; i < width; i++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
                width *= 2;
            }

            Console.WriteLine("\nReverse slope:");
            width = 40;

            while (width > 0)
            {
                for (int i = 0; i < width; i++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
                width -= 5;
            }
            Console.WriteLine();

            Console.WriteLine("\nCliff:");
            width = n;

            while (width > 0)
            {
                for (int i = 0; i <= n - width; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Console.Write("#");
                    }
                    Console.WriteLine();
                }
                width--;
            }

            Console.WriteLine("\nShortening lines:");

            for (int i = n; i > 0; i--)
            {
                for (int j = i; j > 0; j--)
                {
                    for (int k = 0; k < j; k++)
                    {
                        Console.Write("#");
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static void Part2()
        {
            Console.Write("Enter number: ");
            int n = Int32.Parse(Console.ReadLine());

            Console.WriteLine("\nMeasuring tape:");
            for (int i = 0; i <= n * 10; i++)
            {
                if (i % 5 == 0)
                {
                    Console.Write(i);

                    if (i > 9) i++;
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();

            for (int i = 0; i <= n * 10; i++)
            {
                if (i % 5 == 0)
                {
                    Console.Write("|");
                }
                else
                {
                    Console.Write("_");
                }
            }
            Console.WriteLine();

            Console.WriteLine("\nCastle:");
            for (int i = 0; i < n; i++)
            {
                Console.Write("[^^^] ");
            }
            Console.WriteLine();
            Console.Write(" | ");
            for (int i = 0; i < n - 1; i++)
            {
                Console.Write("|___| ");
            }
            Console.WriteLine("|");

            int totalWidth = 6 * n - 1;
            int innerWidth = totalWidth - 4;
            int spaceToGate = (innerWidth - 3) / 2;

            Console.Write(" |");
            for (int i = 0; i < spaceToGate; i++) Console.Write(" ");
            Console.Write("/|\\");
            for (int i = 0; i < spaceToGate; i++) Console.Write(" ");
            Console.WriteLine("|");

            Console.Write(" |");
            for (int i = 0; i < spaceToGate; i++) Console.Write("_");
            Console.Write("|||");
            for (int i = 0; i < spaceToGate; i++) Console.Write("_");
            Console.WriteLine("|");

            Console.WriteLine("\nLCD number:");
            Console.Write(" ");
            Console.WriteLine(n == 2 || n == 3 || n == 5 || n == 6 || n == 7 || n == 8 || n == 9 || n == 0 ? "_" : " ");
            Console.Write(n == 4 || n == 5 || n == 6 || n == 8 || n == 9 || n == 0 ? "|" : " ");
            Console.Write(n == 2 || n == 3 || n == 4 || n == 5 || n == 6 || n == 8 || n == 9 ? "_" : " ");
            Console.WriteLine(n == 1 || n == 2 || n == 3 || n == 4 || n == 7 || n == 8 || n == 9 || n == 0 ? "|" : " ");
            Console.Write(n == 2 || n == 6 || n == 8 || n == 0 ? "|" : " ");
            Console.Write(n == 2 || n == 3 || n == 5 || n == 6 || n == 8 || n == 9 || n == 0 ? "_" : " ");
            Console.WriteLine(n == 1 || n == 3 || n == 4 || n == 5 || n == 6 || n == 7 || n == 8 || n == 9 || n == 0 ? "|" : " ");
        }
    }
}