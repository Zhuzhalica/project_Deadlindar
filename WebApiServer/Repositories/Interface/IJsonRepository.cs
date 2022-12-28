using System.Collections.Generic;
using ValueObjects;

namespace Deadlindar.Repositories
{
    public interface IJsonRepository<T>
    {
        private List<T> OpenFile(string login)
        {
            throw new System.NotImplementedException();
        }

        private void SaveFile(string login, List<T> events)
        {
            throw new System.NotImplementedException();
        }
    }
}