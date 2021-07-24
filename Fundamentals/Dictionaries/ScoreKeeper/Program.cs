using System;
using System.Collections.Generic;
using System.Linq;

namespace ScoreKeeper
{
    class Program
    {
        static void Main(string[] args)
        {
            var scores = new Dictionary<string, int>();

            while (true)
            {
                Console.Write("Who won this round? ");
                string name = Console.ReadLine();
                if (name.Length == 0) return;

                if (scores.ContainsKey(name))
                {
                    scores[name]++;
                }
                else
                {
                    scores[name] = 1;
                }

                Console.WriteLine();
                Console.WriteLine("RANKINGS");

                /* Unsorted
                foreach (var scoreEntry in scores)
                {
                    Console.WriteLine($"{scoreEntry.Key} {scoreEntry.Value}");
                }
                */

                // Sorted with LINQ
                var sortedPlayers = scores.Keys.OrderBy((player) => scores[player]).Reverse();

                /* Sorted with Array.Sort
                string[] sortedPlayers = new List<string>(scores.Keys).ToArray();
                int[] sortedScores = new List<int>(scores.Values).ToArray();

                Array.Sort(sortedScores, sortedPlayers);
                Array.Reverse(sortedPlayers);
                */

                /* Sorted with list sort
                var sortedPlayers = new List<string>(scores.Keys);
                sortedPlayers.Sort((a, b) => scores[b].CompareTo(scores[a]));
                */

                foreach (string player in sortedPlayers)
                {
                    Console.WriteLine($"{player} {scores[player]}");
                }

                Console.WriteLine();
            }
        }
    }
}
