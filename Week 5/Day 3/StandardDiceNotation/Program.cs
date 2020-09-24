using System;
using System.Collections.Generic;

namespace StandardDiceNotation
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeThrows("1d6");
            MakeThrows("2d8");
            MakeThrows("3d6+8");
            MakeThrows("1d4+4");
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
            int numberOfRolls = ((int)diceNotation[0]) - 48;
            int diceSides = ((int)diceNotation[2]) - 48;

            if (diceNotation.Length > 3)
            {
                int fixedBonus = ((int)diceNotation[4]) - 48;
                return DiceRoll(numberOfRolls, diceSides, fixedBonus);
            }
            else
            {
                return DiceRoll(numberOfRolls, diceSides);
            }
        }
    }
}
