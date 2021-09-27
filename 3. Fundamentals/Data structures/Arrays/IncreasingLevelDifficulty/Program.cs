using System;

namespace IncreasingLevelDifficulty
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var monstersPerLevel = new int[100];

            for (int i = 0; i < monstersPerLevel.Length; i++)
            {
                monstersPerLevel[i] = random.Next(1, 51);
            }

            Array.Sort(monstersPerLevel);

            Console.WriteLine($"Number of monsters in levels: {String.Join(", ", monstersPerLevel)}");
        }
    }
}
