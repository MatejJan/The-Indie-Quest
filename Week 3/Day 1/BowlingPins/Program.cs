using System;
using System.Collections.Generic;

namespace BowlingPins
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var pinsStanding = new List<int>();
            int totalFrames = 5;
            int currentFrame = 1;
            int currentRoll = 1;
            var knockedPins = new List<int> { 0, 0 };

            while (currentFrame <= totalFrames)
            {
                if (currentRoll == 1)
                {
                    // Reset pins.
                    pinsStanding.Clear();
                    for (int i = 1; i <= 10; i++) pinsStanding.Add(i);
                }

                string firstRollText = " ";
                string secondRollText = " ";

                if (currentRoll > 1)
                {
                    if (knockedPins[0] == 0)
                    {
                        firstRollText = "-";
                    }
                    else if (knockedPins[0] == 10)
                    {
                        firstRollText = "X";
                    }
                    else
                    {
                        firstRollText = knockedPins[0].ToString();
                    }
                }

                if (currentRoll > 2 && knockedPins[0] < 10)
                {
                    if (knockedPins[1] == 0)
                    {
                        secondRollText = "-";
                    }
                    else if (knockedPins[0] + knockedPins[1] == 10)
                    {
                        secondRollText = "/";
                    }
                    else
                    {
                        secondRollText = knockedPins[1].ToString();
                    }
                }

                Console.Clear();
                Console.WriteLine($"FRAME {currentFrame}");
                Console.WriteLine("+-----+");
                Console.WriteLine($"| |{firstRollText}|{secondRollText}|");
                Console.WriteLine("| ----|");
                Console.WriteLine("|     |");
                Console.WriteLine("+-----+");

                Console.WriteLine();
                Console.WriteLine("Current pins:\n");

                for (int pin = 7; pin <= 10; pin++)
                {
                    Console.Write(pinsStanding.Contains(pin) ? "O" : " ");
                    Console.Write("   ");
                }

                Console.Write("\n\n  ");


                for (int pin = 4; pin <= 6; pin++)
                {
                    Console.Write(pinsStanding.Contains(pin) ? "O" : " ");
                    Console.Write("   ");
                }

                Console.Write("\n\n    ");

                for (int pin = 2; pin <= 3; pin++)
                {
                    Console.Write(pinsStanding.Contains(pin) ? "O" : " ");
                    Console.Write("   ");
                }

                Console.Write("\n\n      ");

                Console.WriteLine(pinsStanding.Contains(1) ? "O" : " ");
                Console.WriteLine();

                if (currentRoll <= 2)
                {
                    Console.WriteLine("Press Enter to roll.");

                    int currentKnockedPins = random.Next(pinsStanding.Count + 1);
                    knockedPins[currentRoll - 1] = currentKnockedPins;
                    for (int i = 0; i < currentKnockedPins; i++)
                    {
                        pinsStanding.RemoveAt(random.Next(pinsStanding.Count));
                    }

                    currentRoll++;

                    if (pinsStanding.Count == 0)
                    {
                        currentRoll = 3;
                    }
                }
                else
                {
                    Console.WriteLine("Press Enter to continue.");
                    currentRoll = 1;
                    currentFrame++;
                }

                Console.ReadLine();
            }
        }
    }
}
