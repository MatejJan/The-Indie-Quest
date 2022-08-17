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

        public string definiteName => EnglishHelpers.GetDefiniteNounForm(name);
        public string indefiniteName => EnglishHelpers.GetIndefiniteNounForm(name);

        public void Attack(Creature target)
        {
            int damage = Dice.Roll(attackDamage);
            target.TakeDamage(this, damage);
        }

        public void TakeDamage(Creature attacker, int damage)
        {
            hitPoints -= damage;
            if (hitPoints < 0) hitPoints = 0;

            Console.WriteLine($"{attacker.definiteName.ToUpperFirst()} hits {definiteName} for {damage} damage. {definiteName.ToUpperFirst()} has {hitPoints} HP left.");
        }
    }
}
