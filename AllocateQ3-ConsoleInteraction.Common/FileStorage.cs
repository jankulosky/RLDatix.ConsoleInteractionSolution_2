using AllocateQ3_ConsoleInteraction.Common.Exceptions;
using AllocateQ3_ConsoleInteraction.Common.Interfaces;
using AllocateQ3_ConsoleInteraction.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AllocateQ3_ConsoleInteraction.Common
{
    public class FileStorage<T> : IFileStorage<T>
    {
        private readonly string _filePath;

        public FileStorage(IFilePathProvider pathProvider)
        {
            _filePath = pathProvider.GetAnimalsFilePath();
        }

        public List<T> Load()
        {
            var json = File.ReadAllText(_filePath);

            try
            {
                return JsonSerializer.Deserialize<List<T>>(json) ?? [];
            }
            catch (JsonException ex)
            {
                throw new InputException($"Failed to parse JSON data from {_filePath}: {ex.Message}");
            }
        }

        public void Save(List<T> data)
        {
            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(_filePath, json);
        }
    }
}
