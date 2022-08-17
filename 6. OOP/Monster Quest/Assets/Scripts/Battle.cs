using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Battle
    {
        public void Simulate(List<Creature> heroes, Creature monster)
        {
            Console.WriteLine($"Watch out, {monster.name} with {monster.hitPoints} HP appears!");

            do
            {
                // Heroes' turn.
                foreach (Creature hero in heroes)
                {
                    hero.Attack(monster);
                    
                    if (monster.hitPoints == 0) break;
                }

                if (monster.hitPoints > 0)
                {
                    // Monster's turn.
                    int randomHeroIndex = Random.Range(0, heroes.Count);
                    Creature attackedHero = heroes[randomHeroIndex];
                    Console.WriteLine($"The {monster.name} attacks {attackedHero.name}!");
                    monster.Attack(attackedHero);

                    if (attackedHero.hitPoints == 0)
                    {
                        heroes.Remove(attackedHero);
                    }
                }

            } while (monster.hitPoints > 0 && heroes.Count > 0);

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
