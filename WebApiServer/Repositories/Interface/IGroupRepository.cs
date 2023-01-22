using System.Collections.Generic;
using ValueObjects;

namespace Deadlindar.Repositories
{
    public interface IGroupRepository
    {
        IEnumerable<Group> GetAll();
        bool Create(string login, Group _group);
        public IEnumerable<string> GetNamesByLogin(string login);
        public Group? GetGroupByName(string login, string groupName);
        bool Delete(string login, string groupName);
        bool AddUser(string memberLogin, string groupName, string newLogin, GroupRole role);
        bool RemoveUser(string memberLogin, string groupName, string removeLogin);
        bool AddEvent(string memberLogin, string groupName, Event _event);
        bool ChangeEvent(string memberLogin, string groupName, Event _event);
        bool RemoveEvent(string memberLogin, string groupName, Event _event);
        bool ChangeRole(string memberLogin, string groupName, string changeRoleLogin, GroupRole role);
    }
}