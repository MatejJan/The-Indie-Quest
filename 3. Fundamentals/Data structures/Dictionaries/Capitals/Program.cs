using System;
using System.Collections.Generic;

namespace Capitals
{
    class Program
    {
        static void Main(string[] args)
        {
            var capitals = new SortedList<string, string>
            {
                {"Slovenia", "Ljubljana" },
                {"Slovakia", "Bratislava" },
                {"Italy", "Rome" },
                {"France", "Paris" },
                {"Sweden", "Stockholm" },
                {"United Kingdom", "London" },
            };

            var random = new Random();
            string country = capitals.Keys[random.Next(capitals.Count)];

            Console.WriteLine($"What is the capital of {country}?");
            string answer = Console.ReadLine();
            Console.WriteLine();

            if (answer == capitals[country])
            {
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine($"Incorrect. It is {capitals[country]}.");
            }
        }
    }
}
