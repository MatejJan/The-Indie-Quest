using System;
using System.Collections.Generic;
using System.Linq;

namespace MonsterQuest
{
    public class Character : Creature
    {
        public int level;

        protected override int proficiencyBonusBase => level;

        public Character()
        {
            // Generate possible ability scores.
            var possibleAbilityScores = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                // Roll 4d6 and remove the lowest dice.
                var rolls = new List<int>();

                for (int j = 0; j < 4; j++)
                {
                    rolls.Add(Dice.Roll("d6"));
                }

                rolls.Sort();
                rolls.RemoveAt(0);
                possibleAbilityScores.Add(rolls.Sum());
            }

            // Randomly assign ability scores.
            possibleAbilityScores.Shuffle();

            for (int i = 0; i < 6; i++)
            {
                abilityScores[(Ability)i].score = possibleAbilityScores[i];
            }
        }
    }
}
