using System;
using System.Collections.Generic;

namespace JoinWithAnd
{
    class Program
    {
        static void Main(string[] args)
        {
            var heroes = new List<string> { "Jazlyn", "Theron", "Dayana", "Rolando" };

            while (heroes.Count > 0)
            {
                Console.WriteLine($"The heroes in the party are: {JoinWithAnd(heroes, false)}.");
                heroes.RemoveAt(0);
            }
        }

        static string JoinWithAnd(List<string> items, bool useSerialComma = true)
        {
            int count = items.Count;

            if (count == 0) return "";
            if (count == 1) return items[0];
            if (count == 2) return $"{items[0]} and {items[1]}";

            var itemsCopy = new List<string>(items);

            if (useSerialComma)
            {
                itemsCopy[count - 1] = $"and {items[count - 1]}";
            }
            else
            {
                itemsCopy[count - 2] = $"{items[count - 2]} and {items[count - 1]}";
                itemsCopy.RemoveAt(count - 1);
            }

            return String.Join(", ", itemsCopy);
        }
    }
}
