using System.Collections.Generic;
using ValueObjects;

namespace Deadlindar.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetByLogin(string login);
        void Add(string login, Event deadline);
        bool Delete(string login, Event deadline);
    }
}