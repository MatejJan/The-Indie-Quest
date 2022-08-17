using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Battle
    {
        public void Simulate(List<string> heroNames, Monster monster)
        {
            Console.WriteLine($"Watch out, {monster.name} with {monster.hitPoints} HP appears!");

            do
            {
                // Heroes' turn.
                foreach (string hero in heroNames)
                {
                    var greatswordHit = Dice.Roll(2, 6);
                    monster.hitPoints -= greatswordHit;
                    if (monster.hitPoints < 0) monster.hitPoints = 0;

                    Console.WriteLine($"{hero} hits the {monster.name} for {greatswordHit} damage. The {monster.name} has {monster.hitPoints} HP left.");

                    if (monster.hitPoints == 0) break;
                }

                if (monster.hitPoints > 0)
                {
                    // Monster's turn.
                    int randomHeroIndex = Random.Range(0, heroNames.Count);
                    string attackedHero = heroNames[randomHeroIndex];
                    Console.WriteLine($"The {monster.name} attacks {attackedHero}!");

                    // Do the saving throw.
                    int d20Roll = Dice.Roll(1, 20);
                    int savingThrow = 5 + d20Roll;

                    if (savingThrow >= monster.attackSavingThrowDC)
                    {
                        Console.WriteLine($"{attackedHero} rolls a {d20Roll} and is saved from the attack.");
                    }
                    else
                    {
                        Console.WriteLine($"{attackedHero} rolls a {d20Roll} and fails to be saved. {attackedHero} is killed.");
                        heroNames.Remove(attackedHero);
                    }
                }

            } while (monster.hitPoints > 0 && heroNames.Count > 0);

            if (monster.hitPoints == 0)
            {
                Console.WriteLine($"The {monster.name} collapses and the heroes celebrate their victory!");
            }
            else
            {
                Console.WriteLine($"The party has failed and the {monster.name} continues to attack unsuspecting adventurers.");
            }
        }
    }
}
