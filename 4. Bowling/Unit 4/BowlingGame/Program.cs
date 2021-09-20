using System;
using System.Collections.Generic;

namespace BowlingGame
{
    class Program
    {
        static Random random = new Random();

        // Variables for current state of the game (which frame and roll we're handling).
        static int currentFrame = 1;
        static int currentRoll = 1;

        // Arrays for keeping score.
        static int[][] knockedPinsCount = new int[10][];
        static int[] pointsGained = new int[10];
        static int[] frameScores = new int[10];

        // A list that holds pin numbers (1-10) of the pins that are still standing.
        static List<int> pinsStanding = new List<int>();

        /// <summary>
        /// Main game loop.
        /// </summary>
        static void Main()
        {
            // Fill points and scores with -1 to represent they haven't been calculated yet.
            for (int i = 0; i < 10; i++)
            {
                pointsGained[i] = -1;
                frameScores[i] = -1;
            }

            // Keep doing rolls until we've done all 10 frames.
            while (currentFrame <= 10)
            {
                // Reset pins when needed.
                ResetPins();

                // Output which frame we're currently at.
                Console.Clear();
                Console.Write("".PadLeft((currentFrame - 1) * 6));
                Console.WriteLine($"FRAME {currentFrame}");

                // Update scores and draw the scoreboard.
                UpdateScores();
                DrawScoreboard();

                // Draw which pins are standing.
                DrawCurrentPins();

                // Determine if the player needs to do a roll.
                int rollsInThisFrame = 2;
                if (currentFrame == 10 && currentRoll >= 3)
                {
                    // In the 10th frame, if the player gets a strike or a spare, they get the third roll.
                    bool frameHasAStrike = knockedPinsCount[9][0] == 10;
                    bool frameHasASpare = knockedPinsCount[9][0] + knockedPinsCount[9][1] == 10;
                    if (frameHasASpare || frameHasAStrike)
                    {
                        rollsInThisFrame = 3;
                    }
                }

                if (currentRoll <= rollsInThisFrame)
                {
                    // Ask the player where to roll the ball.
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

                    // Send the ball flying down the chosen path and see how many pins we knock down.
                    int currentRollKnockedPinsCount = KnockPinOnPath(path);

                    // Store the amount of knocked pins.
                    if (currentRoll == 1)
                    {
                        // This was the first roll of the frame, so we have to create the array for holding the rolls.
                        if (currentFrame == 10)
                        {
                            // In the 10th frame, we need place to store 3 rolls.
                            knockedPinsCount[currentFrame - 1] = new int[] { currentRollKnockedPinsCount, -1, -1 };
                        }
                        else
                        {
                            if (currentRollKnockedPinsCount == 10)
                            {
                                // We got a strike, so we just need one roll in this frame.
                                knockedPinsCount[currentFrame - 1] = new int[] { 10 };
                            }
                            else
                            {
                                // We will have two rolls in this frame.
                                knockedPinsCount[currentFrame - 1] = new int[] { currentRollKnockedPinsCount, -1 };
                            }
                        }
                    }
                    else
                    {
                        // This is the second or third roll of the frame, so we can
                        // just write the amount into the array we prepared previously.
                        knockedPinsCount[currentFrame - 1][currentRoll - 1] = currentRollKnockedPinsCount;
                    }

                    // Advance to the next roll.
                    currentRoll++;

                    // If we knocked all pins in frames 1-9, skip to the end-of-frame report (which will happen on roll #3).
                    if (currentFrame < 10 && pinsStanding.Count == 0)
                    {
                        currentRoll = 3;
                    }
                }
                else
                {
                    // Display the end-of-frame report.
                    if (currentFrame < 10)
                    {
                        // We're not in frame 10 yet, so ask the player to go to the next frame.
                        Console.WriteLine("Press Enter to continue.");
                        currentRoll = 1;
                    }
                    else
                    {
                        // We're at the end of the game.
                        Console.WriteLine($"Game over! Your score was {frameScores[9]}.");
                    }

                    // Wait for the player to press enter and move to next frame.
                    Console.ReadLine();
                    currentFrame++;
                }
            }
        }

        /// <summary>
        /// Draw the diagram of the pins that are still standing.
        /// </summary>
        private static void DrawCurrentPins()
        {
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
        }

        /// <summary>
        /// Resets the standing pins when needed.
        /// </summary>
        static void ResetPins()
        {
            // Determine if we have to reset the pins.
            bool resetPins = false;

            // Before the first roll, we always have to reset them.
            if (currentRoll == 1) resetPins = true;

            if (currentFrame == 10)
            {
                // In the 10th frame we have to reset them before the second roll if there was a strike.
                if (currentRoll == 2 && knockedPinsCount[9][0] == 10) resetPins = true;

                // We also need to reset them before the third roll if we had another strike or a spare.
                if (currentRoll == 3 && (knockedPinsCount[9][1] == 10 || knockedPinsCount[9][0] + knockedPinsCount[9][1] == 10)) resetPins = true;
            }

            if (resetPins)
            {
                // Reset pins by adding pins 1–10 to the list of standing pins.
                pinsStanding.Clear();
                for (int i = 1; i <= 10; i++) pinsStanding.Add(i);
            }
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
                                if (knockedPinsCountInNextFrame[0] == 10 && frameIndex < 9)
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
                                    // Make sure the second roll already happened (otherwise it will be -1).
                                    if (knockedPinsCountInNextFrame[1] != -1)
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
                        else if (knockedPinsCountInThisFrame[1] > -1)
                        {
                            // Add both rolls together.
                            pointsGained[frameIndex] = knockedPinsCountInThisFrame[0] + knockedPinsCountInThisFrame[1];
                        }
                    }
                    else
                    {
                        // Calculate points for 10th frame.
                        int third = knockedPinsCountInThisFrame[2];

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
        }

        /// <summary>
        /// Simulates an item flying down the specified path and hitting the pins.
        /// </summary>
        /// <param name="path">Which of the paths (1-7) the item is flying down.</param>
        /// <returns>The number of pins knocked with this throw.</returns>
        static int KnockPinOnPath(int path)
        {
            // Determine which pin (1-10) will get knocked down. 0 means no pin gets hit.
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

            // If no pin was knocked down, there's nothing to do.
            if (pinToBeKnocked == 0) return 0;

            // Remove the pin that was knocked down.
            pinsStanding.Remove(pinToBeKnocked);
            int knockedPinsCount = 1;

            // Since the pin got knocked down, we have two things flying down the lane (the original thing and the knocked pin).
            for (int i = 0; i < 2; i++)
            {
                // There's a 45% chance the thing will change direction to the left, 45% right, 10% it stays in the same lane.
                int percentage = random.Next(100);

                if (percentage < 45)
                {
                    knockedPinsCount += KnockPinOnPath(path - 1);
                }
                else if (percentage < 90)
                {
                    knockedPinsCount += KnockPinOnPath(path + 1);
                }
                else
                {
                    knockedPinsCount += KnockPinOnPath(path);
                }
            }

            // Return the total amount of pins that got knocked down due to this roll (and subsequent collisions).
            return knockedPinsCount;
        }
    }
}
