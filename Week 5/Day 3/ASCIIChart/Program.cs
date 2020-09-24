using System;

namespace ASCIIChart
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 255; i++)
            {
                Console.WriteLine($"{i} = {(char)i}");
            }
        }
    }
}
