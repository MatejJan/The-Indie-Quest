using System;
using System.Collections.Generic;

namespace GenerateCharacters
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            var abilityScores = new List<int>();

            for (var i = 0; i < 6; i++)
            {
                var rolls = new List<int>();

                for (var j = 0; j < 4; j++)
                {
                    rolls.Add(random.Next(1, 7));
                }

                Console.Write($"You roll {String.Join(", ", rolls)}. ");

                rolls.Sort();
                int attribute = rolls[1] + rolls[2] + rolls[3];

                Console.WriteLine($"The ability score is {attribute}.");
                abilityScores.Add(attribute);
            }

            abilityScores.Sort();
            Console.WriteLine($"Your available ability scores are {String.Join(", ", abilityScores)}. ");
        }
    }
}
