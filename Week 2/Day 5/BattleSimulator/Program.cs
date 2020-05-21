using System;
using System.Collections.Generic;

namespace BattleSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var heroes = new List<string> { "Jazlyn", "Theron", "Dayana", "Rolando" };

            Console.Clear();
            Console.WriteLine($"A party of warriors ({String.Join(", ", heroes)}) descends into the dungeon.");

            SimulateBattle(heroes, "orc", 15, 12);
            if (heroes.Count > 0) SimulateBattle(heroes, "mage", 40, 20);
            if (heroes.Count > 0) SimulateBattle(heroes, "troll", 84, 18);

            if (heroes.Count > 1)
            {
                Console.WriteLine($"After three grueling battles, the heroes {String.Join(", ", heroes)} return from the dungeons to live another day.");
            }
            else if (heroes.Count == 1)
            {
                Console.WriteLine($"After three grueling battles, {heroes[0]} returns from the dungeons. Unfortunately, none of the other party members survived.");
            }
        }

        static void SimulateBattle(List<string> heroes, string monster, int monsterHP, int savingThrowDC)
        {

            var random = new Random();

            Console.WriteLine($"Watch out, {monster} with {monsterHP} HP appears!");

            do
            {
                // Heroes' turn.
                foreach (string hero in heroes)
                {
                    var greatswordHit = random.Next(1, 7) + random.Next(1, 7);
                    monsterHP -= greatswordHit;
                    if (monsterHP < 0) monsterHP = 0;

                    Console.WriteLine($"{hero} hits the {monster} for {greatswordHit} damage. The {monster} has {monsterHP} HP left.");

                    if (monsterHP == 0) break;
                }

                if (monsterHP > 0)
                {
                    // Monster's turn.
                    int randomHeroIndex = random.Next(0, heroes.Count);
                    string attackedHero = heroes[randomHeroIndex];
                    Console.WriteLine($"The {monster} attacks {attackedHero}!");

                    // Do the saving throw.
                    int d20roll = random.Next(1, 21);
                    int savingThrow = 5 + d20roll;

                    if (savingThrow >= savingThrowDC)
                    {
                        Console.WriteLine($"{attackedHero} rolls a {d20roll} and is saved from the attack.");
                    }
                    else
                    {
                        Console.WriteLine($"{attackedHero} rolls a {d20roll} and fails to be saved. {attackedHero} is killed.");
                        heroes.Remove(attackedHero);
                    }
                }

            } while (monsterHP > 0 && heroes.Count > 0);

            if (monsterHP == 0)
            {
                Console.WriteLine($"The {monster} collapses and the heroes celebrate their victory!");
            }
            else
            {
                Console.WriteLine($"The party has failed and the {monster} continues to attack unsuspecting adventurers.");
            }
        }
    }
}
