using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ValueObjects
{
    public class Group
    {
        public string Name { get; set; }

        public Dictionary<string, GroupRole> Members { get; set; }

        public ImmutableArray<string> Users => Members.Keys.ToImmutableArray();
        public bool UserIsAdmin(string login) => Users.Contains(login) && Members[login] == GroupRole.Admin;
        public bool UserIsMember(string login) => Users.Contains(login) && Members[login] == GroupRole.Member;
        public GroupRole GetRole(string login) => Members[login];
        public List<Event> Events { get; set; }

        public Group()
        {
            Members = new Dictionary<string, GroupRole>();
        }

        public Group(string name, Dictionary<string, GroupRole> members)
        {
            Name = name;
            this.Members = members;
            Events = new List<Event>();
        }

        public void AddUser(string login, GroupRole role)
        {
            Members.Add(login, role);
        }

        public void RemoveUser(string login)
        {
            if (Users.Contains(login))
            {
                Members.Remove(login);
                if (Members.Count > 0)
                {
                    if (!Members.ContainsValue(GroupRole.Admin))
                    {
                        var members = Members.Where(pair => pair.Value == GroupRole.Member).ToArray();
                        if (members.Length > 0)
                        {
                            Members[members[0].Key] = GroupRole.Admin;
                        }
                        else
                        {
                            Members[Users[0]] = GroupRole.Admin;
                        }
                    }
                }
            }
        }

        public void ChangeRole(string login, GroupRole role)
        {
            if (Members.ContainsKey(login))
                Members[login] = role;
            else
                throw new ArgumentException($"Has no user with {login} username in group {Name}");
        }
    }
}