using System.Collections.Generic;
using ValueObjects;

namespace WebAPI.Server.Services
{
    public interface IGroupService
    {
        IEnumerable<Group> GetAll();
        public Group? GetGroupByName(string login, string groupName);
        public IEnumerable<string> GetNamesByLogin(string login);
        bool Create(string login, Group _group);
        bool Delete(string login, string groupName);
        bool AddUser(string memberLogin, string groupName, string newLogin, GroupRole role);
        bool RemoveUser(string memberLogin, string groupName, string removeLogin);
        bool AddEvent(string memberLogin, string groupName, Event _event);
        bool ChangeEvent(string memberLogin, string groupName, Event _event);
        bool RemoveEvent(string memberLogin, string groupName, Event _event);
        bool ChangeRole(string memberLogin, string groupName, string changeRoleLogin, GroupRole role);
    }
}