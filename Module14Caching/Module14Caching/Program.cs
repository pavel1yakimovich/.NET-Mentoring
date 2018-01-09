using System;
using System.Runtime.Caching;

namespace Module14Caching
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter index of fibonacci number. To exit enter \"e\"");

                var value = Console.ReadLine();

                if (value == "e")
                {
                    break;
                }

                int index = 0;
                try
                {
                    index = Convert.ToInt32(value);
                }
                catch (Exception)
                {
                    Console.WriteLine("not an integer");
                    continue;
                }

                if (index <= 0)
                {
                    Console.WriteLine("wrong index. it must be > 0");
                }

                Console.WriteLine($"Fibonacci number: {GetFibonacciNumber(index)}");
            }
        }

        private static int GetFibonacciNumber(int index)
        {
            var cache = MemoryCache.Default;

            int previous = 0;
            int result = 1;

            cache.AddOrGetExisting("1", 1, DateTimeOffset.Now.AddMinutes(5));

            for (int i = 2; i <= index; i++)
            {
                int newResult = result + previous;
                previous = result;
                result = (int) (cache.AddOrGetExisting(i.ToString(), newResult, DateTimeOffset.Now.AddMinutes(5)) ?? newResult);
            }

            return result;
        }
    }
}
