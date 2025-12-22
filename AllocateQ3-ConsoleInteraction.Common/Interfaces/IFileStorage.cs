using System.Collections.Generic;

namespace AllocateQ3_ConsoleInteraction.Common.Interfaces
{
    public interface IFileStorage<T>
    {
        List<T> Load();

        void Save(List<T> data);
    }
}
