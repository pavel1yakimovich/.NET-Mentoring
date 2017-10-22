using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a string.");
                var str = Console.ReadLine();
                try
                {
                    Console.WriteLine(str[0]);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("You entered empty string.");
                }
            }
        }
    }
}
