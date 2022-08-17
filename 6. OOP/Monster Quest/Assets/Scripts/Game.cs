using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private MonsterType[] monsterTypes;

        private void Start()
        {
            var battle = new Battle();
            var heroes = new Party
            {
                new() { name = "Jazlyn", hitPoints = 12, attackDamageRoll = "2d6", level = 3, armorClass = 15 },
                new() { name = "Theron", hitPoints = 12, attackDamageRoll = "2d6", level = 3, armorClass = 15 },
                new() { name = "Dayana", hitPoints = 12, attackDamageRoll = "2d6", level = 3, armorClass = 15 },
                new() { name = "Rolando", hitPoints = 12, attackDamageRoll = "2d6", level = 3, armorClass = 15 },
            };

            Console.WriteLine($"A party of warriors ({heroes}) descends into the dungeon.");

            foreach (MonsterType monsterType in monsterTypes)
            {
                var monster = new Monster(monsterType);
                battle.Simulate(heroes, monster);
                if (heroes.Count == 0) break;
            }

            if (heroes.Count > 1)
            {
                Console.WriteLine($"After three grueling battles, the heroes {heroes} return from the dungeons to live another day.");
            }
            else if (heroes.Count == 1)
            {
                Console.WriteLine($"After three grueling battles, {heroes[0].name} returns from the dungeons. Unfortunately, none of the other party members survived.");
            }
        }
    }
}
