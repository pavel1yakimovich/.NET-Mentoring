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

            fsv.FileFound += (object sender, FsvArgs e) => Console.WriteLine("File found");
            fsv.DirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Directory found");
            fsv.FilteredFileFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered file found");
            fsv.FilteredDirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered directory found");

            foreach (var item in fsv)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("With filtration via delegate - shouldn't contain \"2\"");

            fsv = new FileSystemVisitor(s => s.Contains("2"))
            {
                RootDirectory = "D:\\testfolder"
            };
            fsv.SearchStarted += (object sender, FsvArgs e) => Console.WriteLine("\nFiltered search started!\n");
            fsv.SearchFinished += (object sender, FsvArgs e) => Console.WriteLine("\nFiltered search finished\n");

            fsv.FileFound += (object sender, FsvArgs e) => Console.WriteLine("File found");
            fsv.DirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Directory found");
            fsv.FilteredFileFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered file found");
            fsv.FilteredDirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered directory found");

            foreach (var item in fsv)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("With filtration via events - shouldn't contain \"2\"");

            fsv = new FileSystemVisitor()
            {
                RootDirectory = "D:\\testfolder"
            };
            fsv.SearchStarted += (object sender, FsvArgs e) => Console.WriteLine("\nFiltered search started!\n");
            fsv.SearchFinished += (object sender, FsvArgs e) => Console.WriteLine("\nFiltered search finished\n");

            fsv.FileFound += (object sender, FsvArgs e) =>
            {
                Console.WriteLine("File found");
                if (e.Name.Contains("2"))
                {
                    e.ExcludeFileOrDirectory();
                    Console.WriteLine("File excluded");
                }
            };
            fsv.DirectoryFound += (object sender, FsvArgs e) =>
            {
                Console.WriteLine("Directory found");
                if (e.Name.Contains("2"))
                {
                    e.ExcludeFileOrDirectory();
                    Console.WriteLine("Directory excluded");
                }
            };
            fsv.FilteredFileFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered file found");
            fsv.FilteredDirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered directory found");

            foreach (var item in fsv)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("After file, containing \"file212.txt\" is found, finish search");

            fsv = new FileSystemVisitor()
            {
                RootDirectory = "D:\\testfolder"
            };
            fsv.SearchStarted += (object sender, FsvArgs e) => Console.WriteLine("\nSearch started!\n");
            fsv.SearchFinished += (object sender, FsvArgs e) => Console.WriteLine("\nSearch finished\n");

            fsv.FileFound += (object sender, FsvArgs e) =>
            {
                Console.WriteLine("File found");
                if (e.Name.Contains("file212.txt"))
                {
                    e.Stop();
                    Console.WriteLine("Terminate search");
                }
            };
            fsv.DirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Directory found");
            fsv.FilteredFileFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered file found");
            fsv.FilteredDirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered directory found");

            foreach (var item in fsv)
            {
                Console.WriteLine(item);
            }

            Console.Read();
        }
    }
}
