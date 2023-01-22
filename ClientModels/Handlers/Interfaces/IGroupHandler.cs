using System.Collections.Generic;
using ValueObjects;

namespace ClientModels
{
    public interface IGroupHandler
    {
        IReadOnlyList<string> Names { get; }
        List<Event> Events { get; }
        void Setup(string login, string uri);
        List<string> GetAll(string uri);
        List<string> GetNamesByLogin(string login, string uri);
        Group GetByName(string login, string groupName, string uri);
        bool Create(string login, Group group, string uri);
        bool Delete(string login, string groupName, string uri);
        bool AddUser(string login, string groupName, string addLogin, GroupRole role, string uri);
        bool RemoveUser(string login, string groupName, string removeLogin, string uri);
        bool AddEvent(string login, string groupName, Event @event, string uri);
        bool RemoveEvent(string login, string groupName, Event @event, string uri);
        bool ChangeRole(string login, string groupName, string changeLogin, GroupRole role, string uri);
    }
}