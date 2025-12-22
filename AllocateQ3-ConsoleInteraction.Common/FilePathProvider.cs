using AllocateQ3_ConsoleInteraction.Interfaces;
using System;
using System.IO;

namespace AllocateQ3_ConsoleInteraction.Common
{
    public class FilePathProvider : IFilePathProvider
    {
        private const string FileName = "Animals.txt";

        public string GetAnimalsFilePath()
        {
            var path = Path.Combine(AppContext.BaseDirectory, FileName);

            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");
            }

            return path;
        }
    }
}
