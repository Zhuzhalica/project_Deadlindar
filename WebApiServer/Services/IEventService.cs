using System.Collections.Generic;
using Deadlindar.Models;
using ValueObjects;

namespace WebAPI.Server.Services
{
    public interface IEventService
    {
        IEnumerable<Event> GetByLogin(string login);
        void Add(string login, Event deadline);
        bool Delete(string login, Event deadline);
    }
}