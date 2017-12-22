using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePerfomanceCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!PerformanceCounterCategory.Exists("Custom category"))
            {
                var counterCreationDataCollection = new CounterCreationDataCollection();

                var loginCounter = new CounterCreationData()
                {
                    CounterName = "Log in counter",
                    CounterType = PerformanceCounterType.NumberOfItems32
                };
                counterCreationDataCollection.Add(loginCounter);

                var logoffCounter = new CounterCreationData()
                {
                    CounterName = "Log off counter",
                    CounterType = PerformanceCounterType.NumberOfItems32
                };
                counterCreationDataCollection.Add(logoffCounter);

                var homeIndexCounter = new CounterCreationData()
                {
                    CounterName = "Calling Home/Index",
                    CounterType = PerformanceCounterType.NumberOfItems32
                };
                counterCreationDataCollection.Add(homeIndexCounter);

                try
                {
                    PerformanceCounterCategory.Create(
                        "Custom category", "",
                        PerformanceCounterCategoryType.MultiInstance,
                        counterCreationDataCollection);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Do not have permissions");
                    Console.Read();
                }

                Console.WriteLine("Category created");
                Console.Read();
            }
            else
            {
                Console.WriteLine("This counter already exists");
                Console.Read();
            }
        }
    }
}
