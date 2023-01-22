using System.Collections.Generic;
using Deadlindar.Repositories;
using ValueObjects;

namespace WebAPI.Server.Services
{
    public class GroupService : IGroupService
    {
        private IGroupRepository groupRepository;

        public GroupService(IGroupRepository repository)
        {
            groupRepository = repository;
        }

        public IEnumerable<Group> GetAll()
        {
            return groupRepository.GetAll();
        }

        public bool Create(string login, Group _group)
        {
            return groupRepository.Create(login, _group);
        }

        public IEnumerable<string> GetNamesByLogin(string login)
        {
            return groupRepository.GetNamesByLogin(login);
        }

        public Group? GetGroupByName(string login, string groupName)
        {
            return groupRepository.GetGroupByName(login, groupName);
        }

        public bool Delete(string login, string groupName)
        {
            return groupRepository.Delete(login, groupName);
        }

        public bool AddUser(string memberLogin, string groupName, string newLogin, GroupRole role)
        {
            return groupRepository.AddUser(memberLogin, groupName, newLogin, role);
        }

        public bool RemoveUser(string memberLogin, string groupName, string newLogin)
        {
            return groupRepository.RemoveUser(memberLogin, groupName, newLogin);
        }

        public bool AddEvent(string memberLogin, string groupName, Event _event)
        {
            return groupRepository.AddEvent(memberLogin, groupName, _event);
        }

        public bool ChangeEvent(string memberLogin, string groupName, Event _event)
        {
            return groupRepository.ChangeEvent(memberLogin, groupName, _event);
        }

        public bool RemoveEvent(string memberLogin, string groupName, Event _event)
        {
            return groupRepository.RemoveEvent(memberLogin, groupName, _event);
        }

        public bool ChangeRole(string memberLogin, string groupName, string changeRoleLogin, GroupRole role)
        {
            return groupRepository.ChangeRole(memberLogin, groupName, changeRoleLogin, role);
        }
    }
}