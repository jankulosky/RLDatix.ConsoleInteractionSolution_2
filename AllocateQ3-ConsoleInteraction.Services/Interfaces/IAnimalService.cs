using AllocateQ3_ConsoleInteraction.Models;
using System.Collections.Generic;

namespace AllocateQ3_ConsoleInteraction.Services.Interfaces
{
    public interface IAnimalService
    {
        IReadOnlyList<Animal> GetAnimals();

        Animal GetAnimal(string type);

        void AddAnimal(string type, string sound);

        void RemoveAnimal(string type);
    }
}
