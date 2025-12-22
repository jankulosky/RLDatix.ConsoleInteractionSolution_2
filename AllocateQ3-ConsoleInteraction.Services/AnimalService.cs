using AllocateQ3_ConsoleInteraction.Common.Exceptions;
using AllocateQ3_ConsoleInteraction.Models;
using AllocateQ3_ConsoleInteraction.Repositories.Interfaces;
using AllocateQ3_ConsoleInteraction.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace AllocateQ3_ConsoleInteraction.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
        }

        public IReadOnlyList<Animal> GetAnimals()
        {
            return _animalRepository.GetAll();
        }

        public Animal GetAnimal(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new InputException("Animal type must be provided");

            return _animalRepository.GetByType(type) ?? throw new InputException($"Animal '{type}' not found");
        }

        public void AddAnimal(string type, string sound)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new InputException("Type is required");
            if (string.IsNullOrWhiteSpace(sound)) throw new InputException("Sound is required");

            if (_animalRepository.GetByType(type) != null) throw new InputException($"Animal '{type}' already exists");

            _animalRepository.Add(new Animal(type, sound));
            _animalRepository.SaveChanges();
        }

        public void RemoveAnimal(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new InputException("Type is required");

            _animalRepository.Remove(type);
            _animalRepository.SaveChanges();
        }
    }
}
