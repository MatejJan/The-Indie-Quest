using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MonsterManual
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("MonsterManual.txt");

            var axis1Values = new[] { "lawful", "neutral", "chaotic" };
            var axis2Values = new[] { "good", "neutral", "evil" };

            var namesByAlignment = new List<string>[3, 3];
            var namesOfUnaligned = new List<string>();
            var namesOfAnyAlignment = new List<string>();
            var namesOfSpecialCases = new List<string>();
            var specialCases = new List<string>();

            for (int axis1 = 0; axis1 < 3; axis1++)
                for (int axis2 = 0; axis2 < 3; axis2++)
                    namesByAlignment[axis1, axis2] = new List<string>();

            int monsterBlockLineIndex = 0;
            string currentName = "";

            foreach (string line in lines)
            {
                monsterBlockLineIndex++;

                // Parse the name.
                if (monsterBlockLineIndex == 1)
                {
                    currentName = line;
                }

                // Parse the alignment line.
                if (monsterBlockLineIndex == 2)
                {
                    Match match = Regex.Match(line, @"(lawful|neutral|chaotic) (good|neutral|evil)");

                    if (match.Success)
                    {
                        int axis1 = Array.IndexOf(axis1Values, match.Groups[1].Value);
                        int axis2 = Array.IndexOf(axis2Values, match.Groups[2].Value);

                        namesByAlignment[axis1, axis2].Add(currentName);
                        continue;
                    }

                    if (line.Contains("neutral"))
                    {
                        namesByAlignment[1, 1].Add(currentName);
                        continue;
                    }

                    if (line.Contains("unaligned"))
                    {
                        namesOfUnaligned.Add(currentName);
                        continue;
                    }

                    if (line.Contains("any alignment"))
                    {
                        namesOfAnyAlignment.Add(currentName);
                        continue;
                    }

                    namesOfSpecialCases.Add(currentName);

                    string[] parts = line.Split(", ");
                    specialCases.Add(parts[1]);

                    continue;
                }

                if (line.Length == 0)
                {
                    monsterBlockLineIndex = 0;
                }
            }

            for (int axis2 = 0; axis2 < 3; axis2++)
            {
                for (int axis1 = 0; axis1 < 3; axis1++)
                {
                    if (axis1 == 1 && axis2 == 1)
                    {
                        Console.WriteLine($"Monsters with alignment true neutral are:");
                    }
                    else
                    {
                        Console.WriteLine($"Monsters with alignment {axis1Values[axis1]} {axis2Values[axis2]} are:");
                    }

                    foreach (string name in namesByAlignment[axis1, axis2]) Console.WriteLine(name);
                    Console.WriteLine();
                }
            }

            Console.WriteLine($"Unaligned monsters are:");
            foreach (string name in namesOfUnaligned) Console.WriteLine(name);
            Console.WriteLine();

            Console.WriteLine($"Monsters which can be of any alignment are:");
            foreach (string name in namesOfAnyAlignment) Console.WriteLine(name);
            Console.WriteLine();

            Console.WriteLine($"Monsters with special cases are:");
            for (int i = 0; i < namesOfSpecialCases.Count; i++)
            {
                Console.WriteLine($"{namesOfSpecialCases[i]} ({specialCases[i]})");
            }
        }
    }
}
