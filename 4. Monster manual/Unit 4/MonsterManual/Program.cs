using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonsterManual
{
    class Program
    {
        public enum MonsterType
        {
            None,
            Aberration,
            Beast,
            Celestial,
            Construct,
            Dragon,
            Elemental,
            Fey,
            Fiend,
            Giant,
            Humanoid,
            Monstrosity,
            Ooze,
            Plant,
            Undead
        }

        public enum SizeCategory
        {
            None,
            Tiny,
            Small,
            Medium,
            Large,
            Huge,
            Gargantuan
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

        class MonsterKind
        {
            public string Name;
            public MonsterType Type;
            public SizeCategory SizeCategory;
            public string Alignment;
            public string HitPointsRoll;
            public int ArmorClass;
            public ArmorType ArmorType;
        }

        class ArmorKind
        {
            public string DisplayName;
            public ArmorCategory Category;
            public int Weight;
        }

        static void Main(string[] args)
        {
            MonsterType[] monsterTypes = (MonsterType[])Enum.GetValues(typeof(MonsterType));

            // Read monster manual data.
            var monsterKinds = new List<MonsterKind>();

            string[] lines = File.ReadAllLines("MonsterManual.txt");
            int monsterBlockLineIndex = 0;
            MonsterKind currentMonsterKind = null;

            foreach (string line in lines)
            {
                monsterBlockLineIndex++;

                // Parse the name.
                if (monsterBlockLineIndex == 1)
                {
                    currentMonsterKind = new MonsterKind();
                    currentMonsterKind.Name = line;
                }

                if (monsterBlockLineIndex == 2)
                {
                    Match match = Regex.Match(line, @"(\w*)(.*?), (.*)");

                    if (match.Success)
                    {
                        currentMonsterKind.SizeCategory = Enum.Parse<SizeCategory>(match.Groups[1].Value);

                        string typeText = match.Groups[2].Value;
                        foreach (MonsterType monsterType in monsterTypes)
                        {
                            if (typeText.Contains(monsterType.ToString(), StringComparison.InvariantCultureIgnoreCase))
                            {
                                currentMonsterKind.Type = monsterType;
                            }
                        }

                        currentMonsterKind.Alignment = match.Groups[3].Value;
                    }
                }

                if (monsterBlockLineIndex == 3)
                {
                    Match match = Regex.Match(line, @"\((.*)\)");

                    if (match.Success)
                    {
                        currentMonsterKind.HitPointsRoll = match.Groups[1].Value;
                    }
                }

                if (monsterBlockLineIndex == 4)
                {
                    Match match = Regex.Match(line, @"(\d+)(?: \((.*)\))?");

                    if (match.Success)
                    {
                        currentMonsterKind.ArmorClass = int.Parse(match.Groups[1].Value);

                        if (match.Groups[2].Success)
                        {
                            string armorTypeString = match.Groups[2].Value.ToLowerInvariant();

                            if (armorTypeString.Contains("natural"))
                            {
                                currentMonsterKind.ArmorType = ArmorType.Natural;
                            }
                            else if (armorTypeString.Contains("studded leather"))
                            {
                                currentMonsterKind.ArmorType = ArmorType.StuddedLeather;
                            }
                            else if (armorTypeString.Contains("leather"))
                            {
                                currentMonsterKind.ArmorType = ArmorType.Leather;
                            }
                            else if (armorTypeString.Contains("hide"))
                            {
                                currentMonsterKind.ArmorType = ArmorType.Hide;
                            }
                            else if (armorTypeString.Contains("chain shirt"))
                            {
                                currentMonsterKind.ArmorType = ArmorType.ChainShirt;
                            }
                            else if (armorTypeString.Contains("chain mail"))
                            {
                                currentMonsterKind.ArmorType = ArmorType.ChainMail;
                            }
                            else if (armorTypeString.Contains("scale mail"))
                            {
                                currentMonsterKind.ArmorType = ArmorType.ScaleMail;
                            }
                            else if (armorTypeString.Contains("plate"))
                            {
                                currentMonsterKind.ArmorType = ArmorType.Plate;
                            }
                            else
                            {
                                currentMonsterKind.ArmorType = ArmorType.Other;
                            }
                        }
                        else
                        {
                            currentMonsterKind.ArmorType = ArmorType.Unspecified;
                        }
                    }
                }

                if (line.Length == 0)
                {
                    monsterKinds.Add(currentMonsterKind);
                    monsterBlockLineIndex = 0;
                }
            }

            // Read armor type data.
            var armorKinds = new Dictionary<ArmorType, ArmorKind>();

            lines = File.ReadAllLines("ArmorKinds.txt");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(",");

                var armorType = Enum.Parse<ArmorType>(parts[0]);

                var armorKind = new ArmorKind();
                armorKind.DisplayName = parts[1];
                armorKind.Category = Enum.Parse<ArmorCategory>(parts[2]);
                armorKind.Weight = int.Parse(parts[3]);

                armorKinds[armorType] = armorKind;
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

            var results = new List<MonsterKind>();

            if (searchByName)
            {
                Console.WriteLine("Enter a query to search monsters by name:");

                do
                {
                    string lowercaseQuery = Console.ReadLine().ToLowerInvariant();
                    Console.WriteLine();

                    results.Clear();

                    foreach (MonsterKind monsterType in monsterKinds)
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

                string[] armorTypeNames = Enum.GetNames(typeof(ArmorType));

                for (int i = 0; i < armorTypeNames.Length; i++)
                {
                    Console.WriteLine($"{i + 1,3}: {armorTypeNames[i]}");
                }

                Console.WriteLine();
                Console.WriteLine("Enter number:");

                int selectedIndex;

                do
                {
                    string selectionText = Console.ReadLine();
                    Console.WriteLine();

                    selectedIndex = int.Parse(selectionText) - 1;

                    if (selectedIndex < 0 || selectedIndex >= armorTypeNames.Length)
                    {
                        Console.WriteLine("Invalid armor type number. Try again:");
                        continue;
                    }

                } while (selectedIndex < 0);

                ArmorType selectedArmorType = (ArmorType)selectedIndex;

                foreach (MonsterKind monsterType in monsterKinds)
                {
                    if (monsterType.ArmorType == selectedArmorType)
                    {
                        results.Add(monsterType);
                    }
                }
            }

            MonsterKind selectedMonsterType;

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
            Console.WriteLine($"Size: {selectedMonsterType.SizeCategory}");
            Console.WriteLine($"Type: {selectedMonsterType.Type}");
            Console.WriteLine($"Alignment: {selectedMonsterType.Alignment}");
            Console.WriteLine($"Hit points roll: {selectedMonsterType.HitPointsRoll}");
            Console.WriteLine($"Armor class: {selectedMonsterType.ArmorClass}");

            if (selectedMonsterType.ArmorType != ArmorType.Unspecified)
            {
                if (armorKinds.ContainsKey(selectedMonsterType.ArmorType))
                {
                    ArmorKind armorKind = armorKinds[selectedMonsterType.ArmorType];

                    Console.WriteLine($"Armor type: {armorKind.DisplayName}");
                    Console.WriteLine($"Armor category: {armorKind.Category}");
                    Console.WriteLine($"Armor weight: {armorKind.Weight} lb.");
                }
                else
                {
                    Console.WriteLine($"Armor type: {selectedMonsterType.ArmorType}");
                }
            }
        }
    }
}
