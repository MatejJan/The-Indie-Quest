using System;
using System.Collections.Generic;

namespace AnyStandardDiceNotation
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeThrows("d6");
            MakeThrows("2d4");
            MakeThrows("d8+12");
            MakeThrows("2d4-1");
        }

        static void MakeThrows(string diceNotation)
        {
            Console.Write($"Throwing {diceNotation} … ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{DiceRoll(diceNotation)} ");
            }
            Console.WriteLine();
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
            string[] parts = diceNotation.Split(new[] { 'd', '+', '-' });
            int numberOfRolls = parts[0].Length > 0 ? int.Parse(parts[0]) : 1;
            int diceSides = int.Parse(parts[1]);

            if (parts.Length > 2)
            {
                int fixedBonus = int.Parse(parts[2]);

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
