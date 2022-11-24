using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MonsterManual
{
    class Program
    {
        class MonsterType
        {
            public string Name;
            public string Description;
            public string Alignment;
            public string HitPoints;
			public int ArmorClass;
			public ArmorTypeId ArmorTypeId;
		}

        enum ArmorTypeId
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

        class ArmorType
        {
            public string Name;
            public ArmorCategory Category;
            public int Weight;
        }

        static void Main(string[] args)
        {
            // Read monster manual data.
            var monsterTypes = new List<MonsterType>();

            string[] lines = File.ReadAllLines("MonsterManual.txt");
            int monsterBlockLineIndex = 0;
            MonsterType currentMonsterType = null;

            foreach (string line in lines)
            {
                monsterBlockLineIndex++;

                // Parse the name.
                if (monsterBlockLineIndex == 1)
                {
                    currentMonsterType = new MonsterType();
                    currentMonsterType.Name = line;
                }

                if (monsterBlockLineIndex == 2)
                {
                    string[] parts = line.Split(", ");

                    currentMonsterType.Description = parts[0];
                    currentMonsterType.Alignment = parts[1];
                }

                if (monsterBlockLineIndex == 3)
                {
                    Match match = Regex.Match(line, @"\((.*)\)");

                    if (match.Success)
                    {
                        currentMonsterType.HitPoints = match.Groups[1].Value;
                    }
                }

                if (monsterBlockLineIndex == 4)
                {
                    Match match = Regex.Match(line, @"(\d+)(?: \((.*)\))?");

                    if (match.Success)
                    {
                        currentMonsterType.ArmorClass = int.Parse(match.Groups[1].Value);

                        if (match.Groups[2].Success)
                        {
                            string armorTypeString = match.Groups[2].Value.ToLowerInvariant();

                            if (armorTypeString.Contains("natural"))
                            {
                                currentMonsterType.ArmorTypeId = ArmorTypeId.Natural;
                            }
                            else if (armorTypeString.Contains("studded leather"))
                            {
                                currentMonsterType.ArmorTypeId = ArmorTypeId.StuddedLeather;
                            }
                            else if (armorTypeString.Contains("leather"))
                            {
                                currentMonsterType.ArmorTypeId = ArmorTypeId.Leather;
                            }
                            else if (armorTypeString.Contains("hide"))
                            {
                                currentMonsterType.ArmorTypeId = ArmorTypeId.Hide;
                            }
                            else if (armorTypeString.Contains("chain shirt"))
                            {
                                currentMonsterType.ArmorTypeId = ArmorTypeId.ChainShirt;
                            }
                            else if (armorTypeString.Contains("chain mail"))
                            {
                                currentMonsterType.ArmorTypeId = ArmorTypeId.ChainMail;
                            }
                            else if (armorTypeString.Contains("scale mail"))
                            {
                                currentMonsterType.ArmorTypeId = ArmorTypeId.ScaleMail;
                            }
                            else if (armorTypeString.Contains("plate"))
                            {
                                currentMonsterType.ArmorTypeId = ArmorTypeId.Plate;
                            }
                            else
                            {
                                currentMonsterType.ArmorTypeId = ArmorTypeId.Other;
                            }
                        }
                        else
                        {
                            currentMonsterType.ArmorTypeId = ArmorTypeId.Unspecified;
                        }
                    }
                }

                if (line.Length == 0)
                {
                    monsterTypes.Add(currentMonsterType);
                    monsterBlockLineIndex = 0;
                }
            }

            // Read armor type data.
            var armorTypeEntries = new Dictionary<ArmorTypeId, ArmorType>();

            lines = File.ReadAllLines("ArmorTypes.txt");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(",");

                var armorType = (ArmorTypeId)Enum.Parse(typeof(ArmorTypeId), parts[0]);

                var armorTypeInformation = new ArmorType();
                armorTypeInformation.Name = parts[1];
                armorTypeInformation.Category = (ArmorCategory)Enum.Parse(typeof(ArmorCategory), parts[2]);
                armorTypeInformation.Weight = int.Parse(parts[3]);

                armorTypeEntries[armorType] = armorTypeInformation;
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

            var results = new List<MonsterType>();

            if (searchByName)
            {
                Console.WriteLine("Enter a query to search monsters by name:");

                do
                {
                    string lowercaseQuery = Console.ReadLine().ToLowerInvariant();
                    Console.WriteLine();

                    results.Clear();

                    foreach (MonsterType monsterType in monsterTypes)
                    {
                        string lowercaseMonsterName = monsterType.Name.ToLowerInvariant();

                        if (lowercaseMonsterName.Contains(lowercaseQuery))
                        {
                            results.Add(monsterType);
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

                string[] armorTypeNames = Enum.GetNames(typeof(ArmorTypeId));

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

                ArmorTypeId selectedArmorType = (ArmorTypeId)selectedIndex;

                foreach (MonsterType monsterType in monsterTypes)
                {
                    if (monsterType.ArmorTypeId == selectedArmorType)
                    {
                        results.Add(monsterType);
                    }
                }
            }

            MonsterType selectedMonsterType;

            if (results.Count == 1)
            {
                selectedMonsterType = results[0];
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

                selectedMonsterType = results[selectedIndex];
            }

            Console.WriteLine($"Displaying information for {selectedMonsterType.Name}.");
            Console.WriteLine();
            Console.WriteLine($"Name: {selectedMonsterType.Name}");
            Console.WriteLine($"Description: {selectedMonsterType.Description}");
            Console.WriteLine($"Alignment: {selectedMonsterType.Alignment}");
            Console.WriteLine($"Hit points: {selectedMonsterType.HitPoints}");
            Console.WriteLine($"Armor class: {selectedMonsterType.ArmorClass}");

            if (selectedMonsterType.ArmorTypeId != ArmorTypeId.Unspecified)
            {
                if (armorTypeEntries.ContainsKey(selectedMonsterType.ArmorTypeId))
                {
                    ArmorType armorTypeInformation = armorTypeEntries[selectedMonsterType.ArmorTypeId];

                    Console.WriteLine($"Armor type: {armorTypeInformation.Name}");
                    Console.WriteLine($"Armor category: {armorTypeInformation.Category}");
                    Console.WriteLine($"Armor weight: {armorTypeInformation.Weight} lb.");
                }
                else
                {
                    Console.WriteLine($"Armor type: {selectedMonsterType.ArmorTypeId}");
                }
            }
        }
    }
}
