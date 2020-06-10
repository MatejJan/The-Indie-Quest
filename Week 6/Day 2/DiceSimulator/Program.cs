using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace DiceSimulator
{
    class Program
    {
        const int maximumSides = 20;

        static string[] diceArt = new string[maximumSides + 1];
        static ConsoleColor[] diceColors = new ConsoleColor[maximumSides + 1];

        static void Main(string[] args)
        {
            // Load dice ASCII art.
            string[] lines = File.ReadAllLines("Dice.txt");

            string currentDiceArt = "";
            int currentDiceSides = 0;

            foreach (string line in lines)
            {
                if (line.Length == 0)
                {
                    // Store dice art.
                    diceArt[currentDiceSides] = currentDiceArt;
                    continue;
                }

                Match match = Regex.Match(line, @"(\d+) (.+)");

                if (match.Success)
                {
                    // This is a description of a new dice.
                    currentDiceSides = int.Parse(match.Groups[1].Value);

                    int colorNumber = int.Parse(match.Groups[2].Value);
                    ConsoleColor color = (ConsoleColor)colorNumber;

                    diceColors[currentDiceSides] = color;
                    currentDiceArt = "";
                }
                else
                {
                    // This is a new line of dice art.
                    currentDiceArt += $"{line}\n";
                }
            }

            // Run the simulator for the first time.
            string lastDiceText = RunSimulator();

            do
            {
                // See if the user wants to do another roll.
                Console.WriteLine();
                Console.WriteLine("Do you want to (r)epeat, enter a (n)ew roll, or (q)uit?");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.R)
                {
                    lastDiceText = RunSimulator(lastDiceText);
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    lastDiceText = RunSimulator();
                }
                else
                {
                    return;
                }

            } while (true);
        }

        static string RunSimulator(string diceText = null)
        {
            Console.Clear();
            Console.WriteLine("DICE SIMULATOR");
            Console.WriteLine();

            int numberOfRolls, diceSides, fixedBonus;

            Console.WriteLine("Enter desired dice roll in standard dice notation:");

            do
            {
                if (diceText == null)
                {
                    diceText = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine(diceText);
                }

                Console.WriteLine();

                Match match = Regex.Match(diceText, @"(\d+)?d(\d+)([+-]\d+)?");

                if (match.Success)
                {
                    if (match.Groups[1].Success)
                    {
                        numberOfRolls = int.Parse(match.Groups[1].Value);
                    }
                    else
                    {
                        numberOfRolls = 1;
                    }

                    diceSides = int.Parse(match.Groups[2].Value);

                    if (match.Groups[3].Success)
                    {
                        fixedBonus = int.Parse(match.Groups[3].Value);
                    }
                    else
                    {
                        fixedBonus = 0;
                    }

                    break;
                }
                else
                {
                    Console.WriteLine("You did not use standard dice notation. Try again:");
                    diceText = null;
                }

            } while (true);

            Console.WriteLine("Simulating …");
            Console.WriteLine();
            Thread.Sleep(500);

            var random = new Random();
            int sum = 0;

            for (int rollIndex = 1; rollIndex <= numberOfRolls; rollIndex++)
            {
                int rolledNumber = random.Next(diceSides) + 1;

                if (diceArt[diceSides] != null)
                {
                    string currentDiceArt = diceArt[diceSides];

                    // Replace number placeholders.
                    int writtenNumber = rolledNumber;
                    if (diceSides == 10 && rolledNumber == 10) writtenNumber = 0;

                    currentDiceArt = currentDiceArt.Replace("??", $"{writtenNumber,2}").Replace("?", $"{writtenNumber}");

                    // Draw the dice in correct color.
                    Console.ForegroundColor = diceColors[diceSides];
                    Console.WriteLine(currentDiceArt);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine($"{OrdinalNumber(rollIndex)} roll is: {rolledNumber}");
                }

                Thread.Sleep(500);

                sum += rolledNumber;
            }

            Console.WriteLine();

            if (fixedBonus == 0)
            {
                Console.WriteLine($"You rolled {sum}.");
            }
            else
            {
                int score = sum + fixedBonus;

                if (fixedBonus > 0)
                {
                    Console.WriteLine($"You rolled {sum}. Together with the bonus {fixedBonus}, the result is {score}.");
                }
                else
                {
                    Console.WriteLine($"You rolled {sum}. Together with the penalty {fixedBonus}, the result is {score}.");
                }
            }

            return diceText;
        }

        static string OrdinalNumber(int number)
        {
            int lastDigit = number % 10;

            if (number > 10)
            {
                int secondToLastDigit = (number / 10) % 10;
                if (secondToLastDigit == 1) return $"{number}th";
            }

            if (lastDigit == 1) return $"{number}st";
            if (lastDigit == 2) return $"{number}nd";
            if (lastDigit == 3) return $"{number}rd";

            return $"{number}th";
        }
    }
}
