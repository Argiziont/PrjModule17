using System;
using System.IO;
using FileMaintainer;

namespace PrjModule17
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //FileManager.GatherFiles("File.txt", "File1.txt", "File2.txt");
            //FileManager.SplitFiles("File.txt", 2);

            Console.WriteLine("\n\n\n\n");
            FileManager.DeleteSubstring("File.txt", "123");
            Console.WriteLine("\n\n\n\n");
            FileManager.PrintTextFromFiles(Directory.GetCurrentDirectory(), SearchOption.AllDirectories, "444");
            Console.WriteLine("\n\n\n\n");
            FileManager.FileDataAndInfo("*.txt*");
        }
    }
}