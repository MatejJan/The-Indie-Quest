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
            public ArmorTypes Type;
        }

        enum ArmorTypes
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
                                currentMonsterEntry.Armor.Type = ArmorTypes.Natural;
                            }
                            else if (armorTypeString.Contains("studded leather"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorTypes.StuddedLeather;
                            }
                            else if (armorTypeString.Contains("leather"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorTypes.Leather;
                            }
                            else if (armorTypeString.Contains("hide"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorTypes.Hide;
                            }
                            else if (armorTypeString.Contains("chain shirt"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorTypes.ChainShirt;
                            }
                            else if (armorTypeString.Contains("chain mail"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorTypes.ChainMail;
                            }
                            else if (armorTypeString.Contains("scale mail"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorTypes.ScaleMail;
                            }
                            else if (armorTypeString.Contains("plate"))
                            {
                                currentMonsterEntry.Armor.Type = ArmorTypes.Plate;
                            }
                            else
                            {
                                currentMonsterEntry.Armor.Type = ArmorTypes.Other;
                            }
                        }
                        else
                        {
                            currentMonsterEntry.Armor.Type = ArmorTypes.Unspecified;
                        }
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

                string[] armorTypeNames = Enum.GetNames(typeof(ArmorTypes));

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

                ArmorTypes selectedArmorType = (ArmorTypes)selectedIndex;

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
            if (selectedMonsterEntry.Armor.Type != ArmorTypes.Unspecified)
            {
                Console.WriteLine($"Armor type: {selectedMonsterEntry.Armor.Type}");
            }
        }
    }
}
