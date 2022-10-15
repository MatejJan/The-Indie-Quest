using System;
using System.Collections.Generic;

namespace BasiliskBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            var characterNames = new List<string> { "Jazlyn", "Theron", "Dayana", "Rolando" };

            Console.Clear();
            Console.WriteLine($"Fighters {String.Join(", ", characterNames)} descend into the dungeon.");

            var random = new Random();
            var basiliskHP = 16;
            for (int i = 0; i < 8; i++) basiliskHP += random.Next(1, 9);

            Console.WriteLine($"A basilisk with {basiliskHP} HP appears!");

            do
            {
                // Heroes' turn.
                foreach (string hero in characterNames)
                {
                    var daggerHit = random.Next(1, 5);
                    basiliskHP -= daggerHit;
                    if (basiliskHP < 0) basiliskHP = 0;

                    Console.WriteLine($"{hero} hits the basilisk for {daggerHit} damage. Basilisk has {basiliskHP} HP left.");

                    if (basiliskHP == 0) break;
                }

                if (basiliskHP > 0)
                {
                    // Basilisk's turn.
                    int randomHeroIndex = random.Next(0, characterNames.Count);
                    string attackedHero = characterNames[randomHeroIndex];
                    Console.WriteLine($"The basilisk uses petrifying gaze on {attackedHero}!");

                    // Do the saving throw.
                    int d20roll = random.Next(1, 21);
                    int savingThrow = 3 + d20roll;

                    if (savingThrow >= 12)
                    {
                        Console.WriteLine($"{attackedHero} rolls a {d20roll} and is saved from the attack.");
                    }
                    else
                    {
                        Console.WriteLine($"{attackedHero} rolls a {d20roll} and fails to be saved. {attackedHero} is turned into stone.");
                        characterNames.Remove(attackedHero);
                    }
                }

            } while (basiliskHP > 0 && characterNames.Count > 0);

            if (basiliskHP == 0)
            {
                Console.WriteLine("The basilisk collapses and the heroes celebrate their victory!");
            }
            else
            {
                Console.WriteLine("The party has failed and the basilisk continues to turn unsuspecting adventurers to stone.");
            }
        }
    }
}
