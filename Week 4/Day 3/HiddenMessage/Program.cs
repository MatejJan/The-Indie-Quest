using System;

namespace HiddenMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "Want to meet after work? I'll leave the address in the south maintenance closet. Bring an ASCII chart, message will be coded.";

            for (int i = 0; i < message.Length; i++)
            {
                Console.Write($"{(int)message[i],3}, ");

                if (i % 10 == 9) Console.WriteLine();
            }

            int[] numbers = {87, 97, 110, 116, 32, 116, 111, 32, 109, 101,
                101, 116, 32, 97, 102, 116, 101, 114, 32, 119,
                111, 114, 107, 63, 32, 73, 39, 108, 108, 32,
                108, 101, 97, 118, 101, 32, 116, 104, 101, 32,
                 97, 100, 100, 114, 101, 115, 115, 32, 105, 110,
                 32, 116, 104, 101, 32, 115, 111, 117, 116, 104,
                 32, 109, 97, 105, 110, 116, 101, 110, 97, 110,
                 99, 101, 32, 99, 108, 111, 115, 101, 116, 46,
                 32, 66, 114, 105, 110, 103, 32, 97, 110, 32,
                 65, 83, 67, 73, 73, 32, 99, 104, 97, 114,
                116, 44, 32, 109, 101, 115, 115, 97, 103, 101,
                 32, 119, 105, 108, 108, 32, 98, 101, 32, 99,
                111, 100, 101, 100, 46
            };

            Console.WriteLine();
            foreach (int number in numbers) Console.Write((char)number);
        }
    }
}
