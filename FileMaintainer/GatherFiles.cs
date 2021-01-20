using System;
using System.IO;

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
                {
                    //Write to single file in chunks
                    output.Write(buffer, 0, bytesRead);
                }
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
                    File.SetAttributes($"{_splitName}{i}.txt", File.GetAttributes($"{_splitName}{i}.txt") & ~FileAttributes.ReadOnly);
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
            bool found = false;
            var pos = 0;
            var MyStreamReader = new StreamReader(fileName);

            while (!MyStreamReader.EndOfStream)
            {
                
                var ch = MyStreamReader.Read();
                if (ch== substring[shift])
                {
                    shift++;

                    if (shift==substring.Length)
                    {
                        found = true;
                    }
                }
                else
                {
                    shift = 0;
                }
                Console.WriteLine(Convert.ToChar(ch));
                pos++;
            }

            if (found)
            {
                var MyStreamWriter = new StreamWriter(fileName);
                MyStreamWriter.BaseStream.Seek(pos-shift, SeekOrigin.Begin);
                for (int i = 0; i < shift; i++)
                {
                    MyStreamWriter.Write('A');
                }   
            }
        }
    }
}
