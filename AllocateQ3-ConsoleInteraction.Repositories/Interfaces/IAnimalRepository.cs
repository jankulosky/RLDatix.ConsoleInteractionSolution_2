using AllocateQ3_ConsoleInteraction.Models;
using System.Collections.Generic;

namespace AllocateQ3_ConsoleInteraction.Repositories.Interfaces
{
    public interface IAnimalRepository
    {
        IReadOnlyList<Animal> GetAll();

        Animal GetByType(string type);

        void Add(Animal animal);

        void Remove(string type);

        void SaveChanges();
    }
}
