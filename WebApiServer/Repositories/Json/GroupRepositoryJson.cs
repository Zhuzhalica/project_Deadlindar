using System.Collections.Generic;
using System.IO;
using System.Linq;
using ValueObjects;

namespace Deadlindar.Repositories.Json
{
    public class GroupRepositoryJson : IGroupRepository
    {
        private readonly IJsonRepository groupRepository;
        private readonly ILoginGroupsRepository loginGroupsRepository;
        private readonly IEventRepository eventRepository;

        public GroupRepositoryJson(IJsonRepository groupRepository, ILoginGroupsRepository loginGroupsRepository,
            IEventRepository eventRepository)
        {
            this.groupRepository = groupRepository;
            this.loginGroupsRepository = loginGroupsRepository;
            this.eventRepository = eventRepository;
        }

        public IEnumerable<Group> GetAll()
        {
            return groupRepository.OpenFile<List<Group>>("", "Group");
        }

        public IEnumerable<string> GetNamesByLogin(string login)
        {
            return loginGroupsRepository.GetByLogin(login);
        }

        public Group? GetGroupByName(string login, string groupName)
        {
            var groups = groupRepository.OpenFile<List<Group>>("", "Group");
            return groups.FirstOrDefault(g => g.Name == groupName && g.Users.Contains(login));
        }

        public bool Create(string login, Group _group)
        {
            var groups = groupRepository.OpenFile<List<Group>>("", "Group");
            if (groups.FirstOrDefault(g => g.Name == _group.Name && g.Users.Contains(login)) != default)
                return false;

            _group.AddUser(login, GroupRole.Admin);
            groups.Add(_group);
            loginGroupsRepository.AddGroup(login, _group.Name);
            groupRepository.SaveFile("", groups, "Group");
            return true;
        }

        public bool Delete(string login, string groupName)
        {
            var groups = groupRepository.OpenFile<List<Group>>("", "Group");
            var answer = false;
            var _group = groups.FirstOrDefault(g => g.Name == groupName && g.UserIsAdmin(login));
            if (_group != default)
            {
                foreach (var userLogin in _group.Users)
                {
                    loginGroupsRepository.RemoveGroup(userLogin, groupName);
                }

                groups.Remove(_group);
                answer = true;
            }

            groupRepository.SaveFile("", groups, "Group");
            return answer;
        }

        public bool AddUser(string memberLogin, string groupName, string newLogin, GroupRole role)
        {
            var groups = groupRepository.OpenFile<List<Group>>("", "Group");
            var answer = false;

            var @group = groups.FirstOrDefault(g => g.Name == groupName && g.Users.Contains(memberLogin));
            if (@group != default)
            {
                if (@group.GetRole(memberLogin) >= role)
                {
                    @group.AddUser(newLogin, role);
                    loginGroupsRepository.AddGroup(newLogin, @group.Name);
                    answer = true;
                }
            }

            groupRepository.SaveFile("", groups, "Group");
            return answer;
        }

        public bool RemoveUser(string memberLogin, string groupName, string removeLogin)
        {
            var groups = groupRepository.OpenFile<List<Group>>("", "Group");
            var answer = false;

            var group = groups.FirstOrDefault(g =>
                g.Name == groupName && g.Users.Contains(memberLogin) && g.Users.Contains(removeLogin));
            if (group != default)
            {
                if (@group.GetRole(removeLogin) <= @group.GetRole(memberLogin))
                {
                    group.RemoveUser(removeLogin);
                    loginGroupsRepository.RemoveGroup(removeLogin, group.Name);
                    answer = true;
                }
            }

            groupRepository.SaveFile("", groups, "Group");
            return answer;
        }

        public bool AddEvent(string memberLogin, string groupName, Event _event)
        {
            var groups = groupRepository.OpenFile<List<Group>>("", "Group");

            var group = groups.FirstOrDefault(g =>
                g.Name == groupName && g.UserIsAdmin(memberLogin));
            if (@group == default) return false;
            eventRepository.Add($"Group{groupName}", _event);

            return true;
        }

        public bool ChangeEvent(string memberLogin, string groupName, Event _event)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveEvent(string memberLogin, string groupName, Event _event)
        {
            var groups = groupRepository.OpenFile<List<Group>>("", "Group");
            var answer = false;

            var group = groups.FirstOrDefault(g =>
                g.Name == groupName && g.UserIsAdmin(memberLogin));
            return @group != default 
                ? eventRepository.Delete($"Group{groupName}", _event) 
                : answer;
        }

        public bool ChangeRole(string memberLogin, string groupName, string changeRoleLogin, GroupRole role)
        {
            var groups = groupRepository.OpenFile<List<Group>>("", "Group");
            var answer = false;
            var @group = groups.FirstOrDefault(g =>
                g.Name == groupName && g.Users.Contains(memberLogin) && g.Users.Contains(changeRoleLogin));
            if (@group != default)
            {
                if (role <= @group.GetRole(memberLogin))
                {
                    @group.ChangeRole(changeRoleLogin, role);
                    answer = true;
                }
            }

            groupRepository.SaveFile("", groups, "Group");
            return answer;
        }
    }
}