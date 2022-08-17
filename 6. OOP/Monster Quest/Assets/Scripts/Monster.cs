using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster
    {
        public int hitPoints;
        public string name;
        public int attackSavingThrowDC;

        public void TakeDamage(string attackerName, int damage)
        {
            hitPoints -= damage;
            if (hitPoints < 0) hitPoints = 0;

            Console.WriteLine($"{attackerName} hits the {name} for {damage} damage. The {name} has {hitPoints} HP left.");
        }
    }
}
