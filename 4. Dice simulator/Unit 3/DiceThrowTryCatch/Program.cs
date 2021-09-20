using System;
using System.Collections.Generic;

namespace DiceThrowTryCatch
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeThrows("2d6");
            MakeThrows("34");
            MakeThrows("-12");
            MakeThrows("d+");
            MakeThrows("-3d6");
            MakeThrows("0d6");
            MakeThrows("2d-4");
            MakeThrows("2d2.5");
            MakeThrows("ad6");
            MakeThrows("2d$");
            MakeThrows("33d4-b");
        }

        static void MakeThrows(string diceNotation)
        {
            try
            {
                // Perform a dummy dice roll to catch a potential exception before we continue.
                DiceRoll(diceNotation);

                Console.Write($"Throwing {diceNotation} … ");

                for (int i = 0; i < 10; i++)
                {
                    Console.Write($"{DiceRoll(diceNotation)} ");
                }

                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Can't throw {diceNotation} … {e.Message}");
            }
        }

        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {
            var random = new Random();

            int result = fixedBonus;

            for (var i = 0; i < numberOfRolls; i++)
            {
                result += random.Next(1, diceSides + 1);
            }

            return result;
        }

        static int DiceRoll(string diceNotation)
        {
            int numberOfRolls, diceSides, fixedBonus;

            string[] parts = diceNotation.Split('d');
            if (parts.Length <= 1) throw new ArgumentException($"Roll description is not in standard dice notation.");

            try
            {
                numberOfRolls = parts[0].Length > 0 ? int.Parse(parts[0]) : 1;
            }
            catch
            {
                throw new ArgumentException($"Number of rolls ({parts[0]}) is not an integer.");
            }

            if (numberOfRolls <= 0)
            {
                throw new ArgumentException($"Number of rolls ({parts[0]}) has to be positive.");
            }

            parts = parts[1].Split(new[] { '+', '-' });

            try
            {
                diceSides = int.Parse(parts[0]);
            }
            catch
            {
                throw new ArgumentException($"Number of dice sides ({parts[0]}) is not an integer.");
            }

            if (diceSides <= 0)
            {
                throw new ArgumentException($"Number of sides ({parts[0]}) has to be positive.");
            }

            if (parts.Length > 1)
            {
                try
                {
                    fixedBonus = int.Parse(parts[1]);
                }
                catch
                {
                    throw new ArgumentException($"Fixed bonus ({parts[1]}) is not an integer.");
                }

                if (diceNotation.IndexOf('-') > -1) fixedBonus *= -1;

                return DiceRoll(numberOfRolls, diceSides, fixedBonus);
            }
            else
            {
                return DiceRoll(numberOfRolls, diceSides);
            }
        }
    }
}
