using System;
using System.Collections.Generic;

namespace BowlingGame
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            var pinsStanding = new List<int>();
            int currentFrame = 1;
            int currentRoll = 1;

            var knockedPinsCount = new int[10][];
            var pointsGained = new int[10];
            var frameScores = new int[10];
            for (int i = 0; i < 10; i++)
            {
                pointsGained[i] = -1;
                frameScores[i] = -1;
            }

            while (currentFrame <= 10)
            {
                bool resetPins = false;
                if (currentRoll == 1) resetPins = true;
                if (currentFrame == 10)
                {
                    if (currentRoll == 2 && knockedPinsCount[9][0] == 10) resetPins = true;
                    if (currentRoll == 3 && (knockedPinsCount[9][1] == 10 || knockedPinsCount[9][0] + knockedPinsCount[9][1] == 10)) resetPins = true;
                }

                if (resetPins)
                {
                    // Reset pins.
                    pinsStanding.Clear();
                    for (int i = 1; i <= 10; i++) pinsStanding.Add(i);
                }

                Console.Clear();
                Console.Write("".PadLeft((currentFrame - 1) * 6));
                Console.WriteLine($"FRAME {currentFrame}");

                for (int frameIndex = 0; frameIndex < 10; frameIndex++)
                {
                    Console.Write("┌─┬─┬─");
                    if (frameIndex == 9) Console.Write("┬─");
                }
                Console.WriteLine("┐");

                for (int frameIndex = 0; frameIndex < 10; frameIndex++)
                {
                    string firstRollText = " ";
                    string secondRollText = " ";
                    string thirdRollText = " ";

                    int[] knockedPinsCountFrame = knockedPinsCount[frameIndex];

                    if (knockedPinsCountFrame != null)
                    {
                        if (knockedPinsCountFrame.Length >= 1)
                        {
                            if (knockedPinsCountFrame[0] == 0)
                            {
                                firstRollText = "-";
                            }
                            else if (knockedPinsCountFrame[0] == 10)
                            {
                                firstRollText = "X";
                            }
                            else
                            {
                                firstRollText = knockedPinsCountFrame[0].ToString();
                            }
                        }

                        if (knockedPinsCountFrame.Length >= 2)
                        {
                            if (knockedPinsCountFrame[1] == 0)
                            {
                                secondRollText = "-";
                            }
                            else if (frameIndex == 9 && knockedPinsCountFrame[1] == 10)
                            {
                                secondRollText = "X";
                            }
                            else if (knockedPinsCountFrame[0] + knockedPinsCountFrame[1] == 10)
                            {
                                secondRollText = "/";
                            }
                            else if (knockedPinsCountFrame[1] > 0)
                            {
                                secondRollText = knockedPinsCountFrame[1].ToString();
                            }
                        }

                        if (knockedPinsCountFrame.Length == 3)
                        {
                            if (knockedPinsCountFrame[2] == 0)
                            {
                                thirdRollText = "-";
                            }
                            else if (knockedPinsCountFrame[2] == 10)
                            {
                                thirdRollText = "X";
                            }
                            else if (knockedPinsCountFrame[0] == 10 && knockedPinsCountFrame[1] + knockedPinsCountFrame[2] == 10)
                            {
                                thirdRollText = "/";
                            }
                            else if (knockedPinsCountFrame[2] > 0)
                            {
                                thirdRollText = knockedPinsCountFrame[2].ToString();
                            }
                        }
                    }

                    Console.Write($"| |{firstRollText}|{secondRollText}");
                    if (frameIndex == 9) Console.Write($"|{thirdRollText}");
                }
                Console.WriteLine("│");

                for (int frameIndex = 0; frameIndex < 10; frameIndex++)
                {
                    Console.Write("│ └─┴─");
                    if (frameIndex == 9) Console.Write("┴─");
                }
                Console.WriteLine("┤");

                for (int frameIndex = 0; frameIndex < 10; frameIndex++)
                {
                    int[] knockedPinsCountFrame = knockedPinsCount[frameIndex];
                    if (knockedPinsCountFrame != null)
                    {
                        int first = knockedPinsCountFrame[0];
                        int second = -1;
                        if (knockedPinsCountFrame.Length > 1) second = knockedPinsCountFrame[1];

                        if (frameIndex < 9)
                        {
                            int[] knockedPinsCountNextFrame = knockedPinsCount[frameIndex + 1];

                            if (first == 10)
                            {
                                if (knockedPinsCountNextFrame != null)
                                {
                                    if (knockedPinsCountNextFrame[0] == 10)
                                    {
                                        if (frameIndex == 8)
                                        {
                                            if (knockedPinsCountNextFrame[1] > -1)
                                            {
                                                pointsGained[frameIndex] = 10 + knockedPinsCountNextFrame[0] + knockedPinsCountNextFrame[1];
                                            }
                                        }
                                        else
                                        {
                                            int[] knockedPinsCountOneAfterNextFrame = knockedPinsCount[frameIndex + 2];

                                            if (knockedPinsCountOneAfterNextFrame != null)
                                            {
                                                pointsGained[frameIndex] = 10 + knockedPinsCountNextFrame[0] + knockedPinsCountOneAfterNextFrame[0];
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (knockedPinsCountNextFrame[1] > -1)
                                        {
                                            pointsGained[frameIndex] = 10 + knockedPinsCountNextFrame[0] + knockedPinsCountNextFrame[1];
                                        }
                                    }
                                }
                            }
                            else if (first + second == 10)
                            {
                                if (knockedPinsCountNextFrame != null)
                                {
                                    pointsGained[frameIndex] = first + second + knockedPinsCountNextFrame[0];
                                }
                            }
                            else if (knockedPinsCountFrame[1] > -1)
                            {
                                pointsGained[frameIndex] = knockedPinsCountFrame[0] + knockedPinsCountFrame[1];
                            }
                        }
                        else
                        {
                            int third = knockedPinsCountFrame[2];

                            if (second > -1)
                            {
                                if (first == 10 || first + second == 10)
                                {
                                    if (third > -1)
                                    {
                                        pointsGained[frameIndex] = first + second + third;
                                    }
                                }
                                else
                                {
                                    pointsGained[frameIndex] = first + second;
                                }
                            }
                        }

                    }

                    if (pointsGained[frameIndex] > -1)
                    {
                        frameScores[frameIndex] = pointsGained[frameIndex];
                        if (frameIndex > 0) frameScores[frameIndex] += frameScores[frameIndex - 1];
                    }

                    int frameScore = frameScores[frameIndex];
                    if (frameScore > -1)
                    {
                        Console.Write($"│ {frameScore,3} ");
                    }
                    else
                    {
                        Console.Write($"│     ");
                    }
                    if (frameIndex == 9) Console.Write("  ");
                }
                Console.WriteLine("│");

                for (int frameIndex = 0; frameIndex < 10; frameIndex++)
                {
                    Console.Write("└─────");
                    if (frameIndex == 9) Console.Write("──");
                }
                Console.WriteLine("┘");

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

                int rollsPerFrame = 2;
                if (currentFrame == 10 && currentRoll >= 3)
                {
                    bool frameHasAStrike = knockedPinsCount[9][0] == 10;
                    bool frameHasASpare = knockedPinsCount[9][0] + knockedPinsCount[9][1] == 10;
                    if (frameHasASpare || frameHasAStrike)
                    {
                        rollsPerFrame = 3;
                    }
                }

                if (currentRoll <= rollsPerFrame)
                {
                    Console.WriteLine("1 2 3 4 5 6 7");
                    Console.WriteLine();
                    Console.Write("Enter where to roll the ball (1-7): ");
                    int path = 0;
                    while (path == 0)
                    {
                        try
                        {
                            path = Int32.Parse(Console.ReadLine());
                        }
                        catch { }
                    }

                    int currentRollKnockedPinsCount = KnockPinOnPath(path, pinsStanding);
                    if (currentRoll == 1)
                    {
                        if (currentFrame < 10)
                        {
                            if (currentRollKnockedPinsCount == 10)
                            {
                                knockedPinsCount[currentFrame - 1] = new int[] { 10 };
                            }
                            else
                            {
                                knockedPinsCount[currentFrame - 1] = new int[] { currentRollKnockedPinsCount, -1 };
                            }
                        }
                        else
                        {
                            knockedPinsCount[currentFrame - 1] = new int[] { currentRollKnockedPinsCount, -1, -1 };
                        }
                    }
                    else
                    {
                        knockedPinsCount[currentFrame - 1][currentRoll - 1] = currentRollKnockedPinsCount;
                    }

                    currentRoll++;

                    if (currentFrame < 10 && pinsStanding.Count == 0)
                    {
                        currentRoll = 3;
                    }
                }
                else
                {
                    if (currentFrame < 10)
                    {
                        Console.WriteLine("Press Enter to continue.");
                        currentRoll = 1;
                    }
                    else
                    {
                        Console.WriteLine("Game over!");
                    }
                    Console.ReadLine();
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
