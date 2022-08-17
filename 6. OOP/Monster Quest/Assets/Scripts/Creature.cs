using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Creature
    {
        public int hitPoints;
        public string name;
        public string attackDamage;
        public int proficiencyBonus;
        public int armorClass;

        public string definiteName => EnglishHelpers.GetDefiniteNounForm(name);
        public string indefiniteName => EnglishHelpers.GetIndefiniteNounForm(name);

        public void Attack(Creature target)
        {
            // Roll a d20.
            int attackRoll = Dice.Roll("d20");

            // Add modifiers.
            attackRoll += proficiencyBonus;

            // If the attack roll is higher than the target's AC, the attack hits.
            if (attackRoll >= target.armorClass)
            {
                int damage = Dice.Roll(attackDamage);
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
