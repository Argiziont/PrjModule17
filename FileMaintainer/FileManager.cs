using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FileMaintainer
{
    public static class FileManager
    {
        private static readonly string _gatheredName = "gathered.txt";
        private static readonly string _splitName = "split";

        public static void GatherFiles(params string[] inputFiles)
        {
            const int chunkSize = 2 * 1024; // 2KB

            if (File.Exists(_gatheredName))
            {
                File.SetAttributes(_gatheredName, File.GetAttributes(_gatheredName) & ~FileAttributes.ReadOnly);
                File.Delete(_gatheredName);
            }

            using var output = File.Create(_gatheredName);

            foreach (var file in inputFiles)
            {
                using var input = File.OpenRead(file);
                var buffer = new byte[chunkSize];
                int bytesRead;
                //Read files in chunks
                while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                    //Write to single file in chunks
                    output.Write(buffer, 0, bytesRead);
            }

            File.SetAttributes(_gatheredName, FileAttributes.ReadOnly);
        }

        public static void SplitFiles(string fileName, int parts)
        {
            using var input = File.OpenRead(fileName);
            var inputFileLength = input.Length;
            for (var i = 0; i < parts; i++)
            {
                if (File.Exists($"{_splitName}{i}.txt"))
                {
                    File.SetAttributes($"{_splitName}{i}.txt",
                        File.GetAttributes($"{_splitName}{i}.txt") & ~FileAttributes.ReadOnly);
                    File.Delete($"{_splitName}{i}.txt");
                }

                using var output = File.Create($"{_splitName}{i}.txt");

                var buffer = new byte[inputFileLength / parts];
                var bytesRead = input.Read(buffer, 0, buffer.Length);
                //Read files in chunks

                //Write to single file in chunks
                output.Write(buffer, 0, bytesRead);


                File.SetAttributes($"{_splitName}{i}.txt", FileAttributes.ReadOnly);
            }
        }

        public static void DeleteSubstring(string fileName, string substring)
        {
            //using var input = File.OpenWrite(fileName);
            var shift = 0;
            var found = false;
            var pos = 0;
            using var myStreamReader = new StreamReader(fileName);

            while (!myStreamReader.EndOfStream)
            {
                var ch = myStreamReader.Read();
                if (ch == substring[shift])
                {
                    if (shift + 1 == substring.Length)
                    {
                        found = true;
                        Console.WriteLine($"Substring was found on position {pos - shift}");
                        break;
                    }

                    shift++;
                }
                else
                {
                    shift = 0;
                }

                pos++;
            }

            myStreamReader.Close();

            if (!found) return;

            var text = File.ReadAllText(fileName);
            text = text.Replace(substring, "");
            File.WriteAllText(fileName, text);
        }

        public static void FileDataAndInfo(string fileMask)
        {
            var filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.*",
                SearchOption.AllDirectories);

            var fileNumber = 0;
            foreach (var fileDir in filePaths)
            {
                Console.WriteLine($"File {fileNumber}================");

                Console.WriteLine($"Creation time: {File.GetCreationTime(fileDir)}");
                Console.WriteLine($"Modification time: {File.GetLastWriteTime(fileDir)}");
                Console.WriteLine($"Attributes : {File.GetAttributes(fileDir)}");
                Console.WriteLine(FitsMask(Path.GetFileName(fileDir), fileMask)
                    ? "File fits mask"
                    : "File don't fits mask");

                Console.WriteLine("\n\n");
                fileNumber++;
            }
        }

        public static void PrintTextFromFiles(string directory,SearchOption searchOption,string substring)
        {
            var filePaths = Directory.GetFiles(directory, "*.*", searchOption);

            foreach (var fileDir in filePaths)
            {
                var fileText = File.ReadAllText(fileDir);


                Console.WriteLine(fileText.Contains(substring)
                    ? $"File {Path.GetFileName(fileDir)} contains sub-string"
                    : $"File {Path.GetFileName(fileDir)} doesn't contains sub-string");
            }
        }

        private static bool FitsMask(string sFileName, string sFileMask)
        {
            var mask = new Regex(sFileMask.Replace(".", "[.]").Replace("*", ".*").Replace("?", "."));
            return mask.IsMatch(sFileName);
        }
    }
}