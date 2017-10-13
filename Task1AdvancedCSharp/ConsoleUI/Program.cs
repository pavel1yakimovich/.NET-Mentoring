using System;
using Task1AdvancedCSharp;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var fsv = new FileSystemVisitor()
            {
                RootDirectory = "D:\\testfolder"                
            };
            fsv.SearchStarted += (object sender, FsvArgs e) => Console.WriteLine("\nSearch started!\n");
            fsv.SearchFinished += (object sender, FsvArgs e) => Console.WriteLine("\nSearch finished\n");

            foreach (var item in fsv)
            {
                Console.WriteLine(item);
            }
            
            Console.WriteLine("With filtration - shouldn't contain \"2\"");

            fsv = new FileSystemVisitor(s => s.Contains("2"))
            {
                RootDirectory = "D:\\testfolder"
            };
            fsv.SearchStarted += (object sender, FsvArgs e) => Console.WriteLine("\nFiltered search started!\n");
            fsv.SearchFinished += (object sender, FsvArgs e) => Console.WriteLine("\nFiltered search finished\n");

            foreach (var item in fsv)
            {
                Console.WriteLine(item);
            }

            Console.Read();
        }
    }
}
