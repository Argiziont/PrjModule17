using System;
using FileMaintainer;

namespace PrjModule17
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            FileManager.GatherFiles("File.txt", "File1.txt", "File2.txt");
            FileManager.SplitFiles("File.txt", 2);
            FileManager.DeleteSubstring("File.txt", "abd");
        }
    }
}
