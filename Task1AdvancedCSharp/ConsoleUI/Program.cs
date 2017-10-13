﻿using System;
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

            Console.Read();
        }
    }
}
