using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Game : MonoBehaviour
    {
        private void Start()
        {
            var battle = new Battle();
            var heroes = new List<string> { "Jazlyn", "Theron", "Dayana", "Rolando" };

            Console.WriteLine($"A party of warriors ({String.Join(", ", heroes)}) descends into the dungeon.");

            battle.Simulate(heroes, "orc", Dice.Roll(2, 8, 6), 12);
            if (heroes.Count > 0) battle.Simulate(heroes, "mage", Dice.Roll(9, 8), 20);
            if (heroes.Count > 0) battle.Simulate(heroes, "troll", Dice.Roll(8, 10, 40), 18);

            if (heroes.Count > 1)
            {
                Console.WriteLine($"After three grueling battles, the heroes {String.Join(", ", heroes)} return from the dungeons to live another day.");
            }
            else if (heroes.Count == 1)
            {
                Console.WriteLine($"After three grueling battles, {heroes[0]} returns from the dungeons. Unfortunately, none of the other party members survived.");
            }
        }
    }
}
