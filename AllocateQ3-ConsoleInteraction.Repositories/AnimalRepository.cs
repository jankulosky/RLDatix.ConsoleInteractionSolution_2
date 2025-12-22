using AllocateQ3_ConsoleInteraction.Common.Exceptions;
using AllocateQ3_ConsoleInteraction.Common.Interfaces;
using AllocateQ3_ConsoleInteraction.Models;
using AllocateQ3_ConsoleInteraction.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllocateQ3_ConsoleInteraction.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly IFileStorage<Animal> _storage;
        private readonly List<Animal> _data;

        public AnimalRepository(IFileStorage<Animal> storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _data = _storage.Load();
        }

        public void Add(Animal animal)
        {
            if (animal == null) throw new ArgumentNullException(nameof(animal));
            animal.Id = GenerateNextId();
            _data.Add(animal);
        }

        public IReadOnlyList<Animal> GetAll()
        {
            return _data.AsReadOnly();
        }

        public Animal GetByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) return null;

            return _data.FirstOrDefault(a => a.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
        }

        public void Remove(string type)
        {
            var animal = GetByType(type) ?? throw new InputException("Animal not found");
            _data.Remove(animal);
        }

        public void SaveChanges()
        {
            _storage.Save(_data);
        }

        private int GenerateNextId()
        {
            return _data.Count == 0 ? 1 : _data.Max(a => a.Id) + 1;
        }
    }
}
