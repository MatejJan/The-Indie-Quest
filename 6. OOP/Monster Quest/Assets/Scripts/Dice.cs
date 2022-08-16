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
    }
}
