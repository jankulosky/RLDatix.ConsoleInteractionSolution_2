using AllocateQ3_ConsoleInteraction.Interfaces;
using System;
using System.IO;

namespace AllocateQ3_ConsoleInteraction.Common
{
    public class FilePathProvider : IFilePathProvider
    {
        private readonly string _filePath;

        public FilePathProvider(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public string GetAnimalsFilePath() => _filePath;
    }
}
