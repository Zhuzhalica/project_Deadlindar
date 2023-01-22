using System.Collections.Generic;
using ValueObjects;

namespace Deadlindar.Repositories
{
    public interface IJsonRepository
    {
        T OpenFile<T>(string login, string fileName) where T : new();

       void SaveFile<T>(string login, T obj, string fileName);
    }
}