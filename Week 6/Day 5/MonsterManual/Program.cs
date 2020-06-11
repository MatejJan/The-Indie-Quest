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
            public ArmorInformation Armor = new ArmorInformation();
        }

        class ArmorInformation
        {
            public int Class;
            public ArmorType Type;
        }

        enum ArmorType
        {
            Unspecified,
            Natural,
            Leather,
            StuddedLeather,
            Hide,
            ChainShirt,
            ChainMail,
            ScaleMail,
            Plate,
            Other
        }

        enum ArmorCategory
        {
            Light,
            Medium,
            Heavy
        }

        class ArmorTypeEntry
        {
            public string Name;
            public ArmorCategory Category;
            public int Weight;
        }

        static void Main(string[] args)
        {
            // Read monster manual data.
            var monsterEntries = new List<MonsterEntry>();

            string[] lines = File.ReadAllLines("MonsterManual.txt");
            int monsterBlockLineIndex = 0;
            MonsterEntry currentMonsterEntry = null;

            foreach (string line in lines)
            {
                monsterBlockLineIndex++;

                // Parse the name.
                if (monsterBlockLineIndex == 1)
                {
                    currentMonsterEntry = new MonsterEntry();
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

                if (monsterBlockLineIndex == 4)
                {
                    Match match = Regex.Match(line, @"(\d+)(?: \((.*)\))?");

                    if (match.Success)
                    {
                        currentMonsterEntry.Armor.Class = int.Parse(match.Groups[1].Value);

                        if (match.Groups[2].Success)
                        {
                            string armorTypeString = match.Groups[2].Value.ToLowerInvariant();

                            if (armorTypeString.Contains("natural"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorType.Natural;
                            }
                            else if (armorTypeString.Contains("studded leather"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorType.StuddedLeather;
                            }
                            else if (armorTypeString.Contains("leather"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorType.Leather;
                            }
                            else if (armorTypeString.Contains("hide"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorType.Hide;
                            }
                            else if (armorTypeString.Contains("chain shirt"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorType.ChainShirt;
                            }
                            else if (armorTypeString.Contains("chain mail"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorType.ChainMail;
                            }
                            else if (armorTypeString.Contains("scale mail"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorType.ScaleMail;
                            }
                            else if (armorTypeString.Contains("plate"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorType.Plate;
                            }
                            else
                            {
                                currentMonsterEntry.Armor.Type = ArmorType.Other;
                            }
                        }
                        else
                        {
                            currentMonsterEntry.Armor.Type = ArmorType.Unspecified;
                        }
                    }
                }

                if (line.Length == 0)
                {
                    monsterEntries.Add(currentMonsterEntry);
                    monsterBlockLineIndex = 0;
                }
            }

            // Read armor type data.
            var armorTypeEntries = new Dictionary<ArmorType, ArmorTypeEntry>();

            lines = File.ReadAllLines("ArmorTypes.txt");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(", ");

                var armorType = (ArmorType)Enum.Parse(typeof(ArmorType), parts[0]);

                var armorTypeEntry = new ArmorTypeEntry();
                armorTypeEntry.Name = parts[1];
                armorTypeEntry.Category = (ArmorCategory)Enum.Parse(typeof(ArmorCategory), parts[2]);
                armorTypeEntry.Weight = int.Parse(parts[3]);

                armorTypeEntries[armorType] = armorTypeEntry;
            }

            // Display user interface.
            Console.WriteLine("MONSTER MANUAL");
            Console.WriteLine();
            Console.WriteLine("Do you want to search by (n)ame or (a)rmor class?");

            bool searchByName = false;
            bool searchByArmor = false;

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.N)
                {
                    searchByName = true;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.A)
                {
                    searchByArmor = true;
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine();

            var results = new List<MonsterEntry>();

            if (searchByName)
            {
                Console.WriteLine("Enter a query to search monsters by name:");

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
            }
            else if (searchByArmor)
            {
                Console.WriteLine("Which armor type do you want to display?");

                string[] armorTypeNames = Enum.GetNames(typeof(ArmorType));

                for (int i = 0; i < armorTypeNames.Length; i++)
                {
                    Console.WriteLine($"{i + 1,3}: {armorTypeNames[i]}");
                }

                Console.WriteLine();
                Console.WriteLine("Enter number:");

                int selectedIndex = -1;

                do
                {
                    string selectionText = Console.ReadLine();
                    Console.WriteLine();

                    selectedIndex = int.Parse(selectionText) - 1;

                    if (selectedIndex < 0 || selectedIndex >= armorTypeNames.Length)
                    {
                        Console.WriteLine("Invalid armor type number. Try again:");
                        selectedIndex = -1;
                    }

                } while (selectedIndex < 0);

                ArmorType selectedArmorType = (ArmorType)selectedIndex;

                foreach (MonsterEntry monsterEntry in monsterEntries)
                {
                    if (monsterEntry.Armor.Type == selectedArmorType)
                    {
                        results.Add(monsterEntry);
                    }
                }
            }

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
            Console.WriteLine($"Armor class: {selectedMonsterEntry.Armor.Class}");

            if (selectedMonsterEntry.Armor.Type != ArmorType.Unspecified)
            {
                if (armorTypeEntries.ContainsKey(selectedMonsterEntry.Armor.Type))
                {
                    ArmorTypeEntry armorTypeEntry = armorTypeEntries[selectedMonsterEntry.Armor.Type];

                    Console.WriteLine($"Armor type: {armorTypeEntry.Name}");
                    Console.WriteLine($"Armor category: {armorTypeEntry.Category}");
                    Console.WriteLine($"Armor weight: {armorTypeEntry.Weight}");
                }
                else
                {
                    Console.WriteLine($"Armor type: {selectedMonsterEntry.Armor.Type}");
                }
            }
        }
    }
}
