using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Creature
    {
        public string name;
        public int armorClass;
        public int hitPoints;
        public readonly AbilityScores abilityScores = new();
        public string attackDamageRoll;

        public string definiteName => EnglishHelpers.GetDefiniteNounForm(name);
        public string indefiniteName => EnglishHelpers.GetIndefiniteNounForm(name);

        public int proficiencyBonus => 2 + Math.Max(0, (proficiencyBonusBase - 1) / 4);
        protected virtual int proficiencyBonusBase => throw new NotImplementedException();

        public void Attack(Creature target)
        {
            // Roll a d20.
            int attackRoll = Dice.Roll("d20");

            // Add modifiers.
            attackRoll += abilityScores.strength.modifier;
            attackRoll += proficiencyBonus;

            // If the attack roll is higher than the target's AC, the attack hits.
            if (attackRoll >= target.armorClass)
            {
                int damage = Dice.Roll(attackDamageRoll);
                target.TakeDamage(this, damage);
            }
            else
            {
                Console.WriteLine($"{definiteName.ToUpperFirst()} takes a shot at {target.definiteName} but misses.");
            }
        }

        public void TakeDamage(Creature attacker, int damage)
        {
            hitPoints -= damage;
            if (hitPoints < 0) hitPoints = 0;

            Console.WriteLine($"{attacker.definiteName.ToUpperFirst()} hits {definiteName} for {damage} damage. {definiteName.ToUpperFirst()} has {hitPoints} HP left.");
        }
    }
}
