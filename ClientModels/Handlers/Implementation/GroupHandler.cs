using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using ValueObjects;

namespace ClientModels
{
    public class GroupHandler : IGroupHandler
    {
        [JsonIgnore] private ClientGroup ClientGroup { get; }
        [JsonIgnore] public IReadOnlyList<string> Names => _Names.AsReadOnly();
        private List<string> _Names { get; set; }
        public List<Event> Events { get; private set; }
        private IEventHandler EventHandler;

        public GroupHandler(ClientGroup clientGroup, IEventHandler eventHandler)
        {
            Events = new List<Event>();
            ClientGroup = clientGroup;
            EventHandler = eventHandler;
        }

        public void Setup(string login, string uri)
        {
            _Names = GetNamesByLogin(login, uri);
            Events = new List<Event>();
            foreach (var name in _Names)
            {
                foreach (var eEvent in EventHandler.Get($"Group{name}", uri))
                {
                    eEvent.Group = name;
                    Events.Add(eEvent);
                }
            }
        }

        public List<string> GetAll(string uri)
        {
            var names = ClientGroup.TryGetAll(uri);
            if (names == null)
            {
                // уведомление не удалось синхронизироваться с данными сервера
                return new List<string>();
            }

            return names;
        }

        public List<string> GetNamesByLogin(string login, string uri)
        {
            var names = ClientGroup.TryGetNamesByLogin(login, uri);
            if (names == null)
            {
                // уведомление не удалось синхронизироваться с данными сервера
                return new List<string>();
            }

            return names;
        }

        public Group GetByName(string login, string groupName, string uri)
        {
            var names = ClientGroup.TryGetByName(login, groupName, uri);
            if (names == null)
            {
                // уведомление не удалось синхронизироваться с данными сервера
                return new Group();
            }

            return names;
        }

        public bool Create(string login, Group group, string uri)
        {
            return ClientGroup.TryCreate(login, group, uri);
        }

        public bool Delete(string login, string groupName, string uri)
        {
            return ClientGroup.TryDelete(login, groupName, uri);
        }

        public bool AddUser(string login, string groupName, string addLogin, GroupRole role, string uri)
        {
            return ClientGroup.TryAddUser(login, groupName, addLogin, role, uri);
        }

        public bool RemoveUser(string login, string groupName, string removeLogin, string uri)
        {
            return ClientGroup.TryRemoveUser(login, groupName, removeLogin, uri);
        }

        public bool AddEvent(string login, string groupName, Event @event, string uri)
        {
            return ClientGroup.TryAddEvent(login, groupName, @event, uri);
        }

        public bool RemoveEvent(string login, string groupName, Event @event, string uri)
        {
            return ClientGroup.TryRemoveEvent(login, groupName, @event, uri);
        }

        public bool ChangeRole(string login, string groupName, string changeLogin, GroupRole role, string uri)
        {
            return ClientGroup.TryChangeRole(login, groupName, changeLogin, role, uri);
        }
    }
}