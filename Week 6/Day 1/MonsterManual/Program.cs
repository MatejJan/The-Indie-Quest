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

            var names = new List<string>();
            var canFly = new List<bool>();
            var tenPlusDiceRollsForHP = new List<bool>();

            var namesOfSlowFlyers = new List<string>();

            int monsterBlockLineIndex = 0;

            string currentName = "";

            foreach (string line in lines)
            {
                // Parse the name.
                if (monsterBlockLineIndex == 0)
                {
                    names.Add(line);
                    currentName = line;
                }

                // Parse the hit points line.
                if (monsterBlockLineIndex == 2)
                {
                    tenPlusDiceRollsForHP.Add(Regex.IsMatch(line, @"\(\d{2,}d"));
                }

                // Parse the speed line.
                if (monsterBlockLineIndex == 4)
                {
                    canFly.Add(line.Contains("fly"));

                    if (Regex.IsMatch(line, @"fly [1-4]\d "))
                    {
                        namesOfSlowFlyers.Add(currentName);
                    }
                }

                if (line.Length == 0)
                {
                    monsterBlockLineIndex = 0;
                }
                else
                {
                    monsterBlockLineIndex++;
                }
            }

            Console.WriteLine("Monsters in the manual are:");

            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine($"{names[i]} - 10+ dice rolls: {tenPlusDiceRollsForHP[i]}");
            }

            Console.WriteLine();

            Console.WriteLine("Monsters that can fly 10-40 feet per turn:");

            foreach (string name in namesOfSlowFlyers)
            {
                Console.WriteLine(name);
            }
        }
    }
}
