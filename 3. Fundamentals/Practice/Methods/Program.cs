using System;
using System.Collections.Generic;

namespace MethodsPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
        }

        static void Part1()
        {
            int Add(int a, int b)
            {
                return a + b;
            }
            Console.WriteLine("Add");
            Console.WriteLine($"1 + 4 = {Add(1, 4)}");

            int SafeDivision(int dividend, int divisor)
            {
                return divisor == 0 ? dividend : dividend / divisor;
            }
            Console.WriteLine("\nSafe division");
            Console.WriteLine($"10 / 2 = {SafeDivision(10, 2)}");
            Console.WriteLine($"10 / 0 = {SafeDivision(10, 0)}");

            double AreaOfCircle(double radius)
            {
                return Math.PI * radius * radius;
            }
            Console.WriteLine("\nArea of circle");
            Console.WriteLine($"A circle with radius 5 has area {AreaOfCircle(5)}.");

            int MaximumInteger(int a, int b)
            {
                return a > b ? a : b;
            }
            Console.WriteLine("\nMaximum integer");
            Console.WriteLine($"The maximum of 1 and 4 is {MaximumInteger(1, 4)}.");

            int AddIntegers(List<int> integers)
            {
                int sum = 0;
                foreach (int i in integers)
                {
                    sum += i;
                }
                return sum;
            }
            Console.WriteLine("\nAdd integers");
            var integers = new List<int> { 1, 2, 3, 4, 5 };
            Console.WriteLine($"The sum of integers [{string.Join(", ", integers)}] is {AddIntegers(integers)}.");

            int SmallestOfIntegers(List<int> integers)
            {
                int minimum = integers[0];
                foreach (int i in integers)
                {
                    if (i < minimum)
                    {
                        minimum = i;
                    }
                }
                return minimum;
            }
            Console.WriteLine("\nSmallest of integers");
            integers = new List<int> { 3, -2, 7, 0, 10 };
            Console.WriteLine($"The smallest of integers [{string.Join(", ", integers)}] is {SmallestOfIntegers(integers)}.");

            void SortIntegersDescending(List<int> integers)
            {
                integers.Sort();
                integers.Reverse();
            }
            Console.WriteLine("\nSort integers descending");
            SortIntegersDescending(integers);
            Console.WriteLine($"The previous integers in descending order are [{string.Join(", ", integers)}].");

            List<int> UniqueIntegers(List<int> integers)
            {
                var unique = new List<int>();
                foreach (int i in integers)
                {
                    if (!unique.Contains(i))
                    {
                        unique.Add(i);
                    }
                }
                unique.Sort();
                return unique;
            }
            Console.WriteLine("\nUnique integers");
            integers = new List<int> { 10, 3, 10, 2, 0, 2 };
            Console.WriteLine($"Unique integers in [{string.Join(", ", integers)}] are {string.Join(", ", UniqueIntegers(integers))}.");

            List<int> JoinIntegers(List<int> a, List<int> b)
            {
                var result = new List<int>(a);
                result.AddRange(b);
                return result;
            }
            Console.WriteLine("\nJoin integers");
            integers = new List<int> { 1, 2, 3 };
            var integers2 = new List<int> { 7, 8, 9 };
            Console.WriteLine($"The lists [{string.Join(", ", integers)}] and [{string.Join(", ", integers2)}] joined together are [{string.Join(", ", JoinIntegers(integers, integers2))}].");

            List<int> CreateRandomIntegers(int count, int minimum, int maximum)
            {
                var random = new Random();
                var result = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    result.Add(random.Next(minimum, maximum + 1));
                }
                return result;
            }
            Console.WriteLine("\nRandom integers");
            Console.WriteLine($"Here is a list of 10 random integers between -2 and 2 [{string.Join(", ", CreateRandomIntegers(10, -2, 2))}].");

            string Indent(string text, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    text = " " + text;
                }

                return text;
            }
            Console.WriteLine("\nIndent");
            Console.WriteLine(Indent("I am indented by 1 space.", 1));
            Console.WriteLine(Indent("I am indented by 5 spaces.", 5));

            List<string> CreateFullNames(List<string> firstNames, List<string> lastNames)
            {
                var fullNames = new List<string>();
                for (int i = 0; i < firstNames.Count; i++)
                {
                    fullNames.Add($"{firstNames[i]} {lastNames[i]}");
                }
                return fullNames;
            }
            Console.WriteLine("\nFull names");
            var names1 = new List<string> { "Lara", "Duke", "Sonic" };
            var names2 = new List<string> { "Croft", "Nukem", "the Hedgehog" };
            Console.WriteLine($"If we join first names {string.Join(", ", names1)} and last names {string.Join(", ", names2)} we get {string.Join(", ", CreateFullNames(names1, names2))}.");

            List<string> ZipStrings(List<string> a, List<string> b)
            {
                var result = new List<string>();
                int i = 0;
                int j = 0;
                while (i < a.Count || j < b.Count)
                {
                    if (i < a.Count)
                    {
                        result.Add(a[i]);
                    }
                    if (j < b.Count)
                    {
                        result.Add(b[j]);
                    }
                    i++;
                    j++;
                }
                return result;
            }
            Console.WriteLine("\nZip strings");
            names1 = new List<string> { "Lara", "Duke", "Sonic" };
            names2 = new List<string> { "Mario", "Luigi", "Peach", "Bowser" };
            Console.WriteLine($"If we zip {string.Join(", ", names1)} with {string.Join(", ", names2)} we get {string.Join(", ", ZipStrings(names1, names2))}.");

            int CountStrings(string searchValue, List<string> strings)
            {
                int count = 0;
                foreach (string s in strings)
                {
                    if (s == searchValue) count++;
                }
                return count;
            }
            Console.WriteLine("\nCount strings");
            var codes = new List<string> { "ADD", "DEL", "INC", "ADD", "JMP", "SUB", "DEC" };
            Console.WriteLine($"The command ADD appears {CountStrings("ADD", codes)} times in the program [{string.Join(", ", codes)}]");
        }

        static void Part2()
        {
            int Power(int a, int b)
            {
                return b > 1 ? a * Power(a, b - 1) : a;
            }
            Console.WriteLine($"4 to the power of 6 is {Power(4, 6)}.");

            int Factorial(int n)
            {
                return n == 0 ? 1 : n * Factorial(n - 1);
            }
            var factorials = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                factorials.Add(Factorial(i));
            }
            Console.WriteLine($"\nThe factorials of the first 10 positive integers are: {string.Join(", ", factorials)}.");

            int Fibonacci(int n)
            {
                if (n == 1) return 0;
                if (n == 2) return 1;
                return Fibonacci(n - 1) + Fibonacci(n - 2);
            }
            var fibonaccis = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                fibonaccis.Add(Fibonacci(i));
            }
            Console.WriteLine($"\nThe first 10 Fibonacci numbers are: {string.Join(", ", fibonaccis)}.");

            List<int> CreateListOfDigits(int number)
            {
                if (number < 10)
                {
                    return new List<int> { number };
                }
                else
                {
                    int lastDigit = number % 10;
                    List<int> digits = CreateListOfDigits(number / 10);
                    digits.Add(lastDigit);
                    return digits;
                }
            }
            Console.WriteLine($"\nThe digits of 12345 are: {string.Join(", ", CreateListOfDigits(12345))}.");

            string ConvertToBinary(int i)
            {
                if (i <= 1)
                {
                    return i.ToString();
                }
                else
                {
                    return ConvertToBinary(i / 2) + i % 2;
                }
            }
            var binaryNumbers = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                binaryNumbers.Add(ConvertToBinary(i));
            }
            Console.WriteLine($"\nThe first 10 binary numbers are: {string.Join(", ", binaryNumbers)}.");

            int SmallestOfIntegers(List<int> integers)
            {
                if (integers.Count == 1)
                {
                    return integers[0];
                }
                else
                {
                    int middle = integers.Count / 2;
                    int minLeft = SmallestOfIntegers(integers.GetRange(0, middle));
                    int minRight = SmallestOfIntegers(integers.GetRange(middle, integers.Count - middle));
                    return Math.Min(minLeft, minRight);
                }
            }
            var integers = new List<int> { 3, -2, 7, 0, 10, -5, 3 };
            Console.WriteLine($"\nThe smallest of integers [{string.Join(", ", integers)}] is {SmallestOfIntegers(integers)}.");

            void SelectionSort(List<int> integers)
            {
                if (integers.Count <= 1) return;

                int smallest = SmallestOfIntegers(integers);
                integers.Remove(smallest);
                SelectionSort(integers);
                integers.Insert(0, smallest);
            }
            SelectionSort(integers);
            Console.WriteLine($"\nThe previous integers sorted are [{string.Join(", ", integers)}].");
        }
    }
}
