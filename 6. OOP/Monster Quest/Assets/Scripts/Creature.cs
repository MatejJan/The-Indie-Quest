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

        public void Attack(Creature target)
        {
            int damage = Dice.Roll(attackDamage);
            target.TakeDamage(this, damage);
        }

        public void TakeDamage(Creature attacker, int damage)
        {
            hitPoints -= damage;
            if (hitPoints < 0) hitPoints = 0;

            Console.WriteLine($"{attacker.name} hits {name} for {damage} damage. {name} has {hitPoints} HP left.");
        }
    }
}
