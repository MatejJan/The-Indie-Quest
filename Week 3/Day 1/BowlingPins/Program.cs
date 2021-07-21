using System;
using System.Collections.Generic;

namespace BowlingPins
{
    class Program
    {
        static Random random = new Random();

        // Arrays for keeping score
        static int[][] knockedPinsCount = new int[10][];
        static int[] pointsGained = new int[10];
        static int[] frameScores = new int[10];

        // State of the pins
        static int currentFrame;
        static List<int> pinsStanding = new List<int>();

        static void Main(string[] args)
        {
            // Fill points and scores with -1 to represent they haven't been calculated yet.
            for (int i = 0; i < 10; i++)
            {
                pointsGained[i] = -1;
                frameScores[i] = -1;
            }

            for (currentFrame = 1; currentFrame <= 10; currentFrame++)
            {
                // Reset pins.
                ResetPins();

                // Draw the scoreboard and pins.
                DrawGameState();

                // Perform the first roll.
                int knockedPinsCount1 = PerformRoll();
                knockedPinsCount[currentFrame - 1] = new[] { knockedPinsCount1 };

                // Display result.
                DrawGameState();

                // Handle a strike.
                if (knockedPinsCount1 == 10)
                {
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();

                    if (currentFrame == 10)
                    {
                        // In 10th frame we need to reset the pins and keep going.
                        ResetPins();
                        DrawGameState();
                    }
                    else
                    {
                        // In other frames we go to the next frame.
                        continue;
                    }
                }

                // Perform the second roll.
                int knockedPinsCount2 = PerformRoll();
                knockedPinsCount[currentFrame - 1] = new[] { knockedPinsCount1, knockedPinsCount2 };

                // Display result.
                DrawGameState();

                // Handle strike or spare in the 10th frame.
                if (currentFrame == 10 && (knockedPinsCount1 == 10 || knockedPinsCount1 + knockedPinsCount2 == 10))
                {
                    // If this was a strike or a spare, reset the pins again.
                    if (pinsStanding.Count == 0)
                    {
                        Console.WriteLine("Press Enter to continue.");
                        Console.ReadLine();

                        ResetPins();
                        DrawGameState();
                    }
                }
                else
                {
                    // If this is the end of 10th frame, skip to the display of final score.
                    if (currentFrame == 10)
                    {
                        break;
                    }

                    // Go to the next frame (or end the game).
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                    continue;
                }

                // Perform the third roll.
                int knockedPinsCount3 = PerformRoll();
                knockedPinsCount[currentFrame - 1] = new[] { knockedPinsCount1, knockedPinsCount2, knockedPinsCount3 };

                // Display result.
                DrawGameState();
            }

            // Display end-of-gme report.
            Console.WriteLine($"Game over! Your score was {frameScores[9]}.");
            Console.ReadLine();
        }

        static void ResetPins()
        {
            pinsStanding.Clear();
            for (int i = 1; i <= 10; i++) pinsStanding.Add(i);
        }

        static int PerformRoll()
        {
            Console.WriteLine("Press Enter to roll.");
            Console.ReadLine();

            int knockedPins = random.Next(pinsStanding.Count + 1);
            for (int i = 0; i < knockedPins; i++)
            {
                pinsStanding.RemoveAt(random.Next(pinsStanding.Count));
            }

            return knockedPins;
        }

        /// <summary>
        /// Calculates how many points were gained in each frame, where there is enough data.
        /// If points are calculated, it also updates the score that should display for that frame.
        /// </summary>
        static void UpdateScores()
        {
            for (int frameIndex = 0; frameIndex < 10; frameIndex++)
            {
                int[] knockedPinsCountInThisFrame = knockedPinsCount[frameIndex];

                // Make sure this roll has already happened.
                if (knockedPinsCountInThisFrame != null)
                {
                    // Get the first two rolls.
                    int first = knockedPinsCountInThisFrame[0];
                    int second = -1;
                    if (knockedPinsCountInThisFrame.Length > 1) second = knockedPinsCountInThisFrame[1];

                    if (frameIndex < 9)
                    {
                        // Calculate points for frames 1–9.
                        int[] knockedPinsCountInNextFrame = knockedPinsCount[frameIndex + 1];

                        if (first == 10)
                        {
                            // We had a strike, so we need to add next two rolls. Make sure the next frame already started.
                            if (knockedPinsCountInNextFrame != null)
                            {
                                // If we have another strike, we need to add rolls from two consecutive frames,
                                // except on frame 9 (where both rolls will have to come from frame 10).
                                if (knockedPinsCountInNextFrame[0] == 10 && frameIndex < 8)
                                {
                                    int[] knockedPinsCountInFrameAfterNextFrame = knockedPinsCount[frameIndex + 2];

                                    // Make sure the frame after next frame already started.
                                    if (knockedPinsCountInFrameAfterNextFrame != null)
                                    {
                                        pointsGained[frameIndex] = 20 + knockedPinsCountInFrameAfterNextFrame[0];
                                    }
                                }
                                else
                                {
                                    // Make sure the second roll already happened.
                                    if (knockedPinsCountInNextFrame.Length > 1)
                                    {
                                        pointsGained[frameIndex] = 10 + knockedPinsCountInNextFrame[0] + knockedPinsCountInNextFrame[1];
                                    }
                                }
                            }
                        }
                        else if (first + second == 10)
                        {
                            // We had a spare, so we need to add the next roll. Make sure the next frame already started.
                            if (knockedPinsCountInNextFrame != null)
                            {
                                pointsGained[frameIndex] = 10 + knockedPinsCountInNextFrame[0];
                            }
                        }
                        else if (second > -1)
                        {
                            // Add both rolls together.
                            pointsGained[frameIndex] = first + second;
                        }
                    }
                    else
                    {
                        // Calculate points for 10th frame.
                        int third = -1;
                        if (knockedPinsCountInThisFrame.Length > 2) third = knockedPinsCountInThisFrame[2];

                        // Make sure the second roll already happened.
                        if (second != -1)
                        {
                            // See if we need a third roll (we do if we got a strike or a spare).
                            if (first == 10 || first + second == 10)
                            {
                                // We do need the third roll. Make sure it already happened.
                                if (third != -1)
                                {
                                    pointsGained[frameIndex] = first + second + third;
                                }
                            }
                            else
                            {
                                // We don't need the third roll, so we can calculate the points.
                                pointsGained[frameIndex] = first + second;
                            }
                        }
                    }

                }

                // Calculate the frame score if we know how many points we got in this turn.
                if (pointsGained[frameIndex] != -1)
                {
                    frameScores[frameIndex] = pointsGained[frameIndex];
                    if (frameIndex > 0) frameScores[frameIndex] += frameScores[frameIndex - 1];
                }
            }
        }

        static void DrawGameState()
        {
            // Output which frame we're currently at.
            Console.Clear();
            Console.Write("".PadLeft((currentFrame - 1) * 6));
            Console.WriteLine($"FRAME {currentFrame}");

            // Update scores and draw the scoreboard.
            UpdateScores();
            DrawScoreboard();

            // Draw which pins are standing.
            DrawCurrentPins();
        }

        /// <summary>
        /// Draws the entire scoreboard with ASCII art. 
        /// </summary>
        static void DrawScoreboard()
        {
            // Draw the first row (top border).
            Console.Write("┌─┬─┬─");
            for (int frameIndex = 1; frameIndex < 10; frameIndex++)
            {
                Console.Write("┬─┬─┬─");
                if (frameIndex == 9) Console.Write("┬─");
            }
            Console.WriteLine("┐");

            // Draw the second row with amounts of knocked pins.
            for (int frameIndex = 0; frameIndex < 10; frameIndex++)
            {
                string firstRollText = " ";
                string secondRollText = " ";
                string thirdRollText = " ";

                int[] knockedPinsCountInThisFrame = knockedPinsCount[frameIndex];

                // Make sure this roll has already happened.
                if (knockedPinsCountInThisFrame != null)
                {
                    // Determine which symbol to draw in the first box.
                    if (knockedPinsCountInThisFrame[0] == 0)
                    {
                        firstRollText = "-";
                    }
                    else if (knockedPinsCountInThisFrame[0] == 10)
                    {
                        firstRollText = "X";
                    }
                    else
                    {
                        firstRollText = knockedPinsCountInThisFrame[0].ToString();
                    }

                    // See if this frame had a second roll.
                    if (knockedPinsCountInThisFrame.Length >= 2)
                    {
                        // Determine which symbol to draw in the second box.
                        if (knockedPinsCountInThisFrame[1] == 0)
                        {
                            secondRollText = "-";
                        }
                        else if (frameIndex == 9 && knockedPinsCountInThisFrame[1] == 10)
                        {
                            secondRollText = "X";
                        }
                        else if (knockedPinsCountInThisFrame[0] + knockedPinsCountInThisFrame[1] == 10)
                        {
                            secondRollText = "/";
                        }
                        // Make sure the roll even happened (otherwise it will be -1).
                        else if (knockedPinsCountInThisFrame[1] != -1)
                        {
                            secondRollText = knockedPinsCountInThisFrame[1].ToString();
                        }
                    }

                    // See if this frame had a third roll.
                    if (knockedPinsCountInThisFrame.Length == 3)
                    {
                        // Determine which symbol to draw in the third box.
                        bool firstTwoWereASpare = knockedPinsCountInThisFrame[0] + knockedPinsCountInThisFrame[1] == 10;
                        bool secondTwoWereASpare = knockedPinsCountInThisFrame[1] + knockedPinsCountInThisFrame[2] == 10;

                        if (knockedPinsCountInThisFrame[2] == 0)
                        {
                            thirdRollText = "-";
                        }
                        else if (knockedPinsCountInThisFrame[2] == 10 && (knockedPinsCountInThisFrame[1] == 10 || firstTwoWereASpare))
                        {
                            thirdRollText = "X";
                        }
                        else if (knockedPinsCountInThisFrame[0] == 10 && secondTwoWereASpare)
                        {
                            thirdRollText = "/";
                        }
                        // Make sure the roll even happened (otherwise it will be -1).
                        else if (knockedPinsCountInThisFrame[2] != -1)
                        {
                            thirdRollText = knockedPinsCountInThisFrame[2].ToString();
                        }
                    }
                }

                // Write out the 2 symbols (or 3 in the 10th frame).
                Console.Write($"│ │{firstRollText}│{secondRollText}");
                if (frameIndex == 9) Console.Write($"│{thirdRollText}");
            }
            Console.WriteLine("│");

            // Draw the middle row.
            Console.Write("│ └─┴─");
            for (int frameIndex = 1; frameIndex < 10; frameIndex++)
            {
                Console.Write("┤ └─┴─");
                if (frameIndex == 9) Console.Write("┴─");
            }
            Console.WriteLine("┤");

            // Draw the row with scores.
            for (int frameIndex = 0; frameIndex < 10; frameIndex++)
            {
                int frameScore = frameScores[frameIndex];
                if (frameScore != -1)
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

            // Draw the last row (bottom border).
            Console.Write("└─────");
            for (int frameIndex = 1; frameIndex < 10; frameIndex++)
            {
                Console.Write("┴─────");
                if (frameIndex == 9) Console.Write("──");
            }
            Console.WriteLine("┘");
            Console.WriteLine();
        }

        static void DrawCurrentPins()
        {
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
        }
    }
}
