using System;
using System.Collections.Generic;

namespace ListsPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
        }

        static void Part1()
        {
            int n = 50;

            var random = new Random();
            var list = new List<int>();
            for (int i = 0; i < n; i++)
            {
                list.Add(random.Next(100));
            }

            list = new List<int> { 75, 78, 80, 18, 11, 45, 58, 39, 55, 57, 75, 76, 84, 58, 26, 83, 22, 60, 58, 6, 30, 78, 19, 18, 50, 25, 50, 66, 92, 74, 72, 43, 18, 19, 52, 40, 70, 91, 59, 63, 26, 36, 0, 91, 64, 49, 38, 69, 89, 17 };

            Console.WriteLine($"Numbers are: {string.Join(", ", list)}");

            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += list[i];
            }
            Console.WriteLine($"\nThe sum of the numbers is: {sum}");
            Console.WriteLine($"\nThe average of the numbers is: {(double)sum / (double)n}");

            long product = 1;
            for (int i = 0; i < 10; i++)
            {
                product *= list[i];
            }
            Console.WriteLine($"\nThe product of the first 10 numbers is: {product}");

            list.Sort();
            Console.WriteLine($"\nSorted numbers are: {string.Join(", ", list)}");

            var evenList = new List<int>();
            foreach (int i in list)
            {
                if (i % 2 == 0) evenList.Add(i);
            }
            Console.WriteLine($"\nEven numbers are: {string.Join(", ", evenList)}");

            var largestTenList = new List<int>();
            for (int i = list.Count - 10; i < list.Count; i++)
            {
                largestTenList.Add(list[i]);
            }
            Console.WriteLine($"\nLargest ten numbers are: {string.Join(", ", largestTenList)}");

            var largestTenUniqueList = new List<int>();
            for (int i = list.Count - 1; i > 0; i--)
            {
                if (!largestTenUniqueList.Contains(list[i]))
                {
                    largestTenUniqueList.Add(list[i]);
                }
                if (largestTenUniqueList.Count == 10) break;
            }
            Console.WriteLine($"\nLargest ten unique numbers are: {string.Join(", ", largestTenUniqueList)}");

            var uniqueList = new List<int>();
            foreach (int i in list)
            {
                if (!uniqueList.Contains(i))
                {
                    uniqueList.Add(i);
                }
            }
            Console.WriteLine($"\nThere are {uniqueList.Count} total unique numbers.");

            var missingNumbers = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                if (!list.Contains(i))
                {
                    missingNumbers.Add(i);
                }
            }
            Console.WriteLine($"\nThe missing numbers are: {string.Join(", ", missingNumbers)}");

            Console.WriteLine($"\nHistogram:");
            var histogram = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                histogram.Add(0);
            }

            foreach (int i in list)
            {
                int bucket = i / 10;
                histogram[bucket]++;
            }

            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{i * 10}-{i * 10 + 9}: ".PadLeft(7));
                Console.WriteLine("".PadLeft(histogram[i], '#'));
            }
        }

        static void Part2()
        {
            var names = new List<string> { "Allie", "Ben", "Claire", "Dan", "Eleanor" };
            Console.WriteLine($"1.");
            Console.WriteLine(string.Join(", ", names));

            names[0] = "Duke";
            Console.WriteLine($"\n2.");
            Console.WriteLine(string.Join(", ", names));

            names[3] = "Lara";
            Console.WriteLine($"\n3.");
            Console.WriteLine(string.Join(", ", names));

            names[names.Count - 1] = "Aaron";
            Console.WriteLine($"\n4.");
            Console.WriteLine(string.Join(", ", names));

            names.Sort();
            Console.WriteLine($"\n5.");
            Console.WriteLine(string.Join(", ", names));

            names.Reverse();
            Console.WriteLine($"\n6.");
            Console.WriteLine(string.Join(", ", names));

            Console.WriteLine($"\n7.");
            Console.WriteLine(names.Contains("Duke"));

            Console.WriteLine($"\n8.");
            Console.WriteLine(names.IndexOf("Aaron"));

            names.Insert(0, "Mario");
            Console.WriteLine($"\n9.");
            Console.WriteLine(string.Join(", ", names));

            names.Insert(names.Count / 2, "Luigi");
            Console.WriteLine($"\n10.");
            Console.WriteLine(string.Join(", ", names));

            for (int i = names.Count - 1; i >= 0; i--)
            {
                names.Insert(i, names[i]);
            }
            Console.WriteLine($"\n11.");
            Console.WriteLine(string.Join(", ", names));

            (names[0], names[names.Count - 1]) = (names[names.Count - 1], names[0]);
            Console.WriteLine($"\n12.");
            Console.WriteLine(string.Join(", ", names));

            names.RemoveAt(4);
            Console.WriteLine($"\n13.");
            Console.WriteLine(string.Join(", ", names));

            names.Remove("Mario");
            Console.WriteLine($"\n14.");
            Console.WriteLine(string.Join(", ", names));

            Console.WriteLine($"\n15.");
            Console.WriteLine(names.LastIndexOf("Claire"));

            names.RemoveAt(names.LastIndexOf("Aaron"));
            Console.WriteLine($"\n16.");
            Console.WriteLine(string.Join(", ", names));

            for (int i = names.Count - 2; i >= 0; i--)
            {
                if (names[i] == names[i + 1])
                {
                    names.RemoveAt(i);
                    names.RemoveAt(i);
                }
            }
            Console.WriteLine($"\n17.");
            Console.WriteLine(string.Join(", ", names));

            names.Clear();
            Console.WriteLine($"\n18.");
            Console.WriteLine(string.Join(", ", names));
        }
    }
}
