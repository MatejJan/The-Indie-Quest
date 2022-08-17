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

            var orc = new Monster { name = "orc", hitPoints = Dice.Roll(2, 8, 6), attackSavingThrowDC = 12 };
            var mage = new Monster { name = "mage", hitPoints = Dice.Roll(9, 8), attackSavingThrowDC = 20 };
            var troll = new Monster { name = "troll", hitPoints = Dice.Roll(8, 10, 40), attackSavingThrowDC = 18 };

            battle.Simulate(heroes, orc);
            if (heroes.Count > 0) battle.Simulate(heroes, mage);
            if (heroes.Count > 0) battle.Simulate(heroes, troll);

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
