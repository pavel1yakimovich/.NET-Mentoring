using System;
using Task1AdvancedCSharp;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootDir = "D:\\testfolder";
            var fsv = new FileSystemVisitor(rootDir);

            //fsv.SearchStarted += (object sender, FsvArgs e) => Console.WriteLine("\nSearch started!\n");
            //fsv.SearchFinished += (object sender, FsvArgs e) => Console.WriteLine("\nSearch finished\n");

            //fsv.FileFound += (object sender, FsvArgs e) => Console.WriteLine("File found");
            //fsv.DirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Directory found");
            //fsv.FilteredFileFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered file found");
            //fsv.FilteredDirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered directory found");

            //foreach (var item in fsv)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine("With filtration via delegate - shouldn't contain \"2\"");

            //fsv = new FileSystemVisitor(rootDir, s => s.Contains("2"));

            //fsv.SearchStarted += (object sender, FsvArgs e) => Console.WriteLine("\nFiltered search started!\n");
            //fsv.SearchFinished += (object sender, FsvArgs e) => Console.WriteLine("\nFiltered search finished\n");

            //fsv.FileFound += (object sender, FsvArgs e) => Console.WriteLine("File found");
            //fsv.DirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Directory found");
            //fsv.FilteredFileFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered file found");
            //fsv.FilteredDirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered directory found");

            //foreach (var item in fsv)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine("With filtration via events - shouldn't contain \"2\"");

            //fsv = new FileSystemVisitor(rootDir);

            //fsv.SearchStarted += (object sender, FsvArgs e) => Console.WriteLine("\nFiltered search started!\n");
            //fsv.SearchFinished += (object sender, FsvArgs e) => Console.WriteLine("\nFiltered search finished\n");

            //fsv.FileFound += (object sender, FsvArgs e) =>
            //{
            //    Console.WriteLine("File found");
            //    if (e.Name.Contains("2"))
            //    {
            //        e.ExcludeFileOrDirectory();
            //        Console.WriteLine("File excluded");
            //    }
            //};
            //fsv.DirectoryFound += (object sender, FsvArgs e) =>
            //{
            //    Console.WriteLine("Directory found");
            //    if (e.Name.Contains("2"))
            //    {
            //        e.ExcludeFileOrDirectory();
            //        Console.WriteLine("Directory excluded");
            //    }
            //};
            //fsv.FilteredFileFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered file found");
            //fsv.FilteredDirectoryFound += (object sender, FsvArgs e) => Console.WriteLine("Filtered directory found");

            //foreach (var item in fsv)
            //{
            //    Console.WriteLine(item);
            //}

            Console.WriteLine("After file, containing \"file221.txt\" is found, finish search");

            fsv = new FileSystemVisitor(rootDir);

            fsv.SearchStarted += (object sender, FsvArgs e) => Console.WriteLine("\nSearch started!\n");
            fsv.SearchFinished += (object sender, FsvArgs e) => Console.WriteLine("\nSearch finished\n");

            fsv.FileFound += (object sender, FsvArgs e) =>
            {
                Console.WriteLine("File found");
                if (e.Name == "file221.txt")
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

            Console.WriteLine("Empty string passed as a rootElement");

            try
            {
                fsv = new FileSystemVisitor(String.Empty);

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.Read();
        }
    }
}
