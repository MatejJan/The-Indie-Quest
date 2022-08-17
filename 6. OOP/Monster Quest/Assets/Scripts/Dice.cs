using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public static class Dice
    {
        public static int Roll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {
            int result = fixedBonus;

            for (var i = 0; i < numberOfRolls; i++)
            {
                result += Random.Range(1, diceSides + 1);
            }

            return result;
        }
        
        public static int Roll(string diceNotation)
        {
            string[] parts = diceNotation.Split(new[] { 'd', '+', '-' });
            int numberOfRolls = parts[0].Length > 0 ? int.Parse(parts[0]) : 1;
            int diceSides = int.Parse(parts[1]);

            if (parts.Length > 2)
            {
                int fixedBonus = int.Parse(parts[2]);

                if (diceNotation.IndexOf('-') > -1) fixedBonus *= -1;

                return Dice.Roll(numberOfRolls, diceSides, fixedBonus);
            }
            else
            {
                return Dice.Roll(numberOfRolls, diceSides);
            }
        }
    }
}
