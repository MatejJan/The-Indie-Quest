using System;

namespace TankBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DANGER! A tank is approaching our position. Your artilery unit is our only hope!");
            Console.WriteLine();
            Console.WriteLine("What is your name, commander?");
            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            var random = new Random();
            int tankDistance = random.Next(15, 75);
            int shellsCount = 5;

            Console.WriteLine();
            Console.WriteLine("Here is the map of the battlefield:");
            Console.WriteLine();

            Console.Write("_/");

            for (int i = 0; i < tankDistance; i++) Console.Write("_");
            Console.Write("T");
            for (int i = 0; i < 77 - tankDistance; i++) Console.Write("_");
            Console.WriteLine();

            while (shellsCount > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Aim your shot, {name}!");
                Console.Write("Enter distance: ");
                string shotInputText = Console.ReadLine();
                int shotDistance = Int32.Parse(shotInputText);

                for (int i = 0; i < shotDistance + 2; i++) Console.Write(" ");
                Console.WriteLine("*");

                shellsCount--;

                if (shotDistance == tankDistance)
                {
                    Console.WriteLine("BOOM! Your aim is legendary and the tank is destroyed!");
                    return;
                }
                else if (shotDistance < tankDistance)
                {
                    Console.WriteLine("Oh no, your shot was too short.");
                }
                else
                {
                    Console.WriteLine("Alas, the shell flies past the tank.");
                }

                if (shellsCount > 0)
                {
                    if (shellsCount > 1)
                    {
                        Console.WriteLine($"You have {shellsCount} shells left.");
                    }
                    else
                    {
                        Console.WriteLine($"You have only one shell left. Make it count!");
                    }
                }
            }

            Console.WriteLine("Unfortunately you run out of shells and the tank destroys your outpost.");
        }
    }
}
