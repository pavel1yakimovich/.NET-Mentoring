using System;
using Task1AdvancedCSharp;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var fsv = new FileSystemVisitor
            {
                RootDirectory = "E:\\testfolder"
            };

            foreach (var item in fsv)
            {
                Console.WriteLine(item);
            }

            Console.Read();
        }
    }
}
