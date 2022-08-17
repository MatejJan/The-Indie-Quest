using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Battle
    {
        public void Simulate(Party heroes, Creature monster)
        {
            Console.WriteLine($"Watch out, {monster.indefiniteName} with {monster.hitPoints} HP appears!");

            do
            {
                // Heroes' turn.
                foreach (Character hero in heroes)
                {
                    hero.Attack(monster);

                    if (monster.hitPoints == 0) break;
                }

                if (monster.hitPoints > 0)
                {
                    // Monster's turn.
                    int randomHeroIndex = Random.Range(0, heroes.Count);
                    Character attackedHero = heroes[randomHeroIndex];
                    monster.Attack(attackedHero);

                    if (attackedHero.hitPoints == 0)
                    {
                        heroes.Remove(attackedHero);
                    }
                }
            } while (monster.hitPoints > 0 && heroes.Count > 0);

            if (monster.hitPoints == 0)
            {
                Console.WriteLine($"{monster.definiteName.ToUpperFirst()} collapses and the heroes celebrate their victory!");
            }
            else
            {
                Console.WriteLine($"The party has failed and {monster.definiteName} continues to attack unsuspecting adventurers.");
            }
        }
    }
}
