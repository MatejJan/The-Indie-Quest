using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MonsterManual
{
    class Program
    {
        class MonsterEntry
        {
            public string Name;
            public string Description;
            public string Alignment;
            public string HitPoints;
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("MonsterManual.txt");

            var monsterEntries = new List<MonsterEntry>();

            int monsterBlockLineIndex = 0;

            var currentMonsterEntry = new MonsterEntry();

            foreach (string line in lines)
            {
                monsterBlockLineIndex++;

                // Parse the name.
                if (monsterBlockLineIndex == 1)
                {
                    currentMonsterEntry.Name = line;
                }

                if (monsterBlockLineIndex == 2)
                {
                    string[] parts = line.Split(", ");

                    currentMonsterEntry.Description = parts[0];
                    currentMonsterEntry.Alignment = parts[1];
                }

                if (monsterBlockLineIndex == 3)
                {
                    Match match = Regex.Match(line, @"\((.*)\)");

                    if (match.Success)
                    {
                        currentMonsterEntry.HitPoints = match.Groups[1].Value;
                    }
                }

                if (line.Length == 0)
                {
                    monsterEntries.Add(currentMonsterEntry);

                    monsterBlockLineIndex = 0;
                    currentMonsterEntry = new MonsterEntry();
                }
            }

            Console.WriteLine("MONSTER MANUAL");
            Console.WriteLine();
            Console.WriteLine("Enter a query to search monsters by name:");

            var results = new List<MonsterEntry>();

            do
            {
                string lowercaseQuery = Console.ReadLine().ToLowerInvariant();
                Console.WriteLine();

                results.Clear();

                foreach (MonsterEntry monsterEntry in monsterEntries)
                {
                    string lowercaseMonsterName = monsterEntry.Name.ToLowerInvariant();

                    if (lowercaseMonsterName.Contains(lowercaseQuery))
                    {
                        results.Add(monsterEntry);
                    }
                }

                if (results.Count == 0)
                {
                    Console.WriteLine("No monsters were found. Try again:");
                }

            } while (results.Count == 0);

            MonsterEntry selectedMonsterEntry;

            if (results.Count == 1)
            {
                selectedMonsterEntry = results[0];
            }
            else
            {
                Console.WriteLine("Which monster did you want to look up?");

                for (int i = 0; i < results.Count; i++)
                {
                    Console.WriteLine($"{i + 1,3}: {results[i].Name}");
                }

                Console.WriteLine();
                Console.WriteLine("Enter number:");

                int selectedIndex = -1;

                do
                {
                    string selectionText = Console.ReadLine();
                    Console.WriteLine();

                    selectedIndex = int.Parse(selectionText) - 1;

                    if (selectedIndex < 0 || selectedIndex >= results.Count)
                    {
                        Console.WriteLine("No moster has that number. Try again:");
                        selectedIndex = -1;
                    }

                } while (selectedIndex < 0);

                selectedMonsterEntry = results[selectedIndex];
            }

            Console.WriteLine($"Displaying information for {selectedMonsterEntry.Name}.");
            Console.WriteLine();
            Console.WriteLine($"Name: {selectedMonsterEntry.Name}");
            Console.WriteLine($"Description: {selectedMonsterEntry.Description}");
            Console.WriteLine($"Alignment: {selectedMonsterEntry.Alignment}");
            Console.WriteLine($"Hit points: {selectedMonsterEntry.HitPoints}");
        }
    }
}
