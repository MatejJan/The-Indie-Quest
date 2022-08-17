using System;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster : Creature
    {
        public MonsterType type { get; private set; }

        protected override int proficiencyBonusBase => (int)type.challengeRating;

        public Monster(MonsterType type)
        {
            this.type = type;

            // Roll the monster's hit points.
            hitPoints = Dice.Roll(type.hitPointsRoll);

            // Copy the rest of the properties from the monster type.
            name = type.name;
            armorClass = type.armorClass;

            foreach (Ability ability in Enum.GetValues(typeof(Ability)))
            {
                abilityScores[ability].score = type.abilityScores[ability];
            }

            attackDamageRoll = type.attackDamageRoll;
        }
    }
}
