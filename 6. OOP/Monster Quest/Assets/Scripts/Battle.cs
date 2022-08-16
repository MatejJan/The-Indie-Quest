using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Battle
    {
        public void Simulate(List<string> heroes, string monster, int monsterHP, int savingThrowDC)
        {
            Console.WriteLine($"Watch out, {monster} with {monsterHP} HP appears!");

            do
            {
                // Heroes' turn.
                foreach (string hero in heroes)
                {
                    var greatswordHit = Dice.Roll(2, 6);
                    monsterHP -= greatswordHit;
                    if (monsterHP < 0) monsterHP = 0;

                    Console.WriteLine($"{hero} hits the {monster} for {greatswordHit} damage. The {monster} has {monsterHP} HP left.");

                    if (monsterHP == 0) break;
                }

                if (monsterHP > 0)
                {
                    // Monster's turn.
                    int randomHeroIndex = Random.Range(0, heroes.Count);
                    string attackedHero = heroes[randomHeroIndex];
                    Console.WriteLine($"The {monster} attacks {attackedHero}!");

                    // Do the saving throw.
                    int d20Roll = Dice.Roll(1, 20);
                    int savingThrow = 5 + d20Roll;

                    if (savingThrow >= savingThrowDC)
                    {
                        Console.WriteLine($"{attackedHero} rolls a {d20Roll} and is saved from the attack.");
                    }
                    else
                    {
                        Console.WriteLine($"{attackedHero} rolls a {d20Roll} and fails to be saved. {attackedHero} is killed.");
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
