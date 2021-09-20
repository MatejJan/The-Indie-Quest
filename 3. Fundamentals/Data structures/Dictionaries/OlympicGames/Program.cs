using System;
using System.Collections.Generic;

namespace OlympicGames
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostCountries = new Dictionary<int, string>
            {
                {2000, "Australia" },
                {2004, "Greece" },
                {2008, "China" },
                {2012, "United Kingdom" },
                {2016, "Brazil" },
                {2020, "Japan" },
            };

            var random = new Random();
            int year = 2000 + random.Next(hostCountries.Count) * 4;

            Console.WriteLine($"Which country hosted the Summer Olympic Games in {year}?");
            string answer = Console.ReadLine();
            Console.WriteLine();

            if (answer == hostCountries[year])
            {
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine($"Incorrect. It was {hostCountries[year]}.");
            }
        }
    }
}
