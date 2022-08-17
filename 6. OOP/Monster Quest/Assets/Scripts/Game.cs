using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public class Game : MonoBehaviour
    {
        private void Start()
        {
            var battle = new Battle();
            var heroes = new List<Creature>
            {
                new() { name = "Jazlyn", hitPoints = 12, attackDamage = "2d6" },
                new() { name = "Theron", hitPoints = 12, attackDamage = "2d6" },
                new() { name = "Dayana", hitPoints = 12, attackDamage = "2d6" },
                new() { name = "Rolando", hitPoints = 12, attackDamage = "2d6" },
            };

            Console.WriteLine($"A party of warriors ({String.Join(", ", heroes.Select(hero => hero.name))}) descends into the dungeon.");

            var orc = new Creature { name = "orc", hitPoints = Dice.Roll("2d8+6"), attackDamage = "1d12+3" };
            var mage = new Creature { name = "mage", hitPoints = Dice.Roll("9d8"), attackDamage = "3d4+4" };
            var troll = new Creature { name = "troll", hitPoints = Dice.Roll("8d10+40"), attackDamage = "2d6+4" };

            battle.Simulate(heroes, orc);
            if (heroes.Count > 0) battle.Simulate(heroes, mage);
            if (heroes.Count > 0) battle.Simulate(heroes, troll);

            if (heroes.Count > 1)
            {
                Console.WriteLine($"After three grueling battles, the heroes {String.Join(", ", heroes.Select(hero => hero.name))} return from the dungeons to live another day.");
            }
            else if (heroes.Count == 1)
            {
                Console.WriteLine($"After three grueling battles, {heroes[0].name} returns from the dungeons. Unfortunately, none of the other party members survived.");
            }
        }
    }
}