using System;
using System.Collections.Generic;

namespace BowlingSimulation
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            var pinsStanding = new List<int>();
            int totalFrames = 10;
            int currentFrame = 1;
            int currentRoll = 1;
            var knockedPinsCount = new List<int> { 0, 0 };

            while (currentFrame <= totalFrames)
            {
                if (currentRoll == 1)
                {
                    // Reset pins.
                    pinsStanding.Clear();
                    for (int i = 1; i <= 10; i++) pinsStanding.Add(i);

                    // Reset stats.
                    knockedPinsCount[0] = 0;
                    knockedPinsCount[1] = 0;
                }

                string firstRollText = " ";
                string secondRollText = " ";

                if (currentRoll > 1)
                {
                    if (knockedPinsCount[0] == 0)
                    {
                        firstRollText = "-";
                    }
                    else if (knockedPinsCount[0] == 10)
                    {
                        firstRollText = "X";
                    }
                    else
                    {
                        firstRollText = knockedPinsCount[0].ToString();
                    }
                }

                if (currentRoll > 2 && knockedPinsCount[0] < 10)
                {
                    if (knockedPinsCount[1] == 0)
                    {
                        secondRollText = "-";
                    }
                    else if (knockedPinsCount[0] + knockedPinsCount[1] == 10)
                    {
                        secondRollText = "/";
                    }
                    else
                    {
                        secondRollText = knockedPinsCount[1].ToString();
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
                    Console.WriteLine("1 2 3 4 5 6 7");
                    Console.WriteLine();
                    Console.Write("Enter where to roll the ball (1-7): ");
                    int path = Int32.Parse(Console.ReadLine());

                    knockedPinsCount[currentRoll - 1] = KnockPinOnPath(path, pinsStanding);
                    currentRoll++;

                    if (pinsStanding.Count == 0)
                    {
                        currentRoll = 3;
                    }
                }
                else
                {
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                    currentRoll = 1;
                    currentFrame++;
                }
            }
        }

        static int KnockPinOnPath(int path, List<int> pinsStanding)
        {
            int pinToBeKnocked = 0;

            if (path == 1)
            {
                if (pinsStanding.Contains(7)) pinToBeKnocked = 7;
            }
            else if (path == 2)
            {
                if (pinsStanding.Contains(4)) pinToBeKnocked = 4;
            }
            else if (path == 3)
            {
                if (pinsStanding.Contains(2)) pinToBeKnocked = 2;
                else if (pinsStanding.Contains(8)) pinToBeKnocked = 8;
            }
            else if (path == 4)
            {
                if (pinsStanding.Contains(1)) pinToBeKnocked = 1;
                else if (pinsStanding.Contains(5)) pinToBeKnocked = 5;
            }
            else if (path == 5)
            {
                if (pinsStanding.Contains(3)) pinToBeKnocked = 3;
                else if (pinsStanding.Contains(9)) pinToBeKnocked = 9;
            }
            else if (path == 6)
            {
                if (pinsStanding.Contains(6)) pinToBeKnocked = 6;
            }
            else if (path == 7)
            {
                if (pinsStanding.Contains(10)) pinToBeKnocked = 10;
            }

            if (pinToBeKnocked == 0) return 0;

            pinsStanding.Remove(pinToBeKnocked);
            int knockedPinsCount = 1;

            for (int i = 0; i < 2; i++)
            {
                int percentage = random.Next(100);

                if (percentage < 45)
                {
                    knockedPinsCount += KnockPinOnPath(path - 1, pinsStanding);
                }
                else if (percentage < 90)
                {
                    knockedPinsCount += KnockPinOnPath(path + 1, pinsStanding);
                }
                else
                {
                    knockedPinsCount += KnockPinOnPath(path, pinsStanding);
                }
            }

            return knockedPinsCount;
        }
    }
}
