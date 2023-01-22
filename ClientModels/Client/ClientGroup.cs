using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using ValueObjects;

namespace ClientModels
{
    public class ClientGroup
    {
        private readonly IGroupRequest request;

        public ClientGroup(IGroupRequest request)
        {
            this.request = request;
        }

        public List<string>? TryGetAll(string uri)
        {
            var content = request.GetAll(uri).Result;
            if (content.IsSuccessStatusCode)
                return ResponseInNames(content);

            return null;
        }

        public List<string>? TryGetNamesByLogin(string login, string uri)
        {
            var content = request.GetNamesByLogin(login, uri).Result;
            if (content.IsSuccessStatusCode)
                return ResponseInNames(content);

            return null;
        }
        
        public Group? TryGetByName(string login, string groupName, string uri)
        {
            var content = request.GetByName(login, groupName, uri).Result;
            if (content.IsSuccessStatusCode)
                return ResponseInGroup(content);

            return null;
        }
        
        public bool TryCreate(string login, Group group, string uri)
        {
            var content = request.Create(login, group, uri).Result;
            return content.IsSuccessStatusCode;
        }
        
        public bool TryDelete(string login, string groupName, string uri)
        {
            var content = request.Delete(login, groupName, uri).Result;
            return content.IsSuccessStatusCode;
        }
        
        public bool TryAddUser(string login, string groupName, string addLogin, GroupRole role, string uri)
        {
            var content = request.AddUser(login, groupName, addLogin, role, uri).Result;
            return content.IsSuccessStatusCode;
        }
        
        public bool TryRemoveUser(string login, string groupName, string removeLogin, string uri)
        {
            var content = request.RemoveUser(login, groupName, removeLogin, uri).Result;
            return content.IsSuccessStatusCode;
        }

        public bool TryAddEvent(string login, string groupName, Event @event, string uri)
        {
            var content = request.AddEvent(login, groupName, @event, uri).Result;
            return content.IsSuccessStatusCode;
        }
        
        public bool TryRemoveEvent(string login, string groupName, Event @event, string uri)
        {
            var content = request.RemoveEvent(login, groupName, @event, uri).Result;
            return content.IsSuccessStatusCode;
        }
        
        public bool TryChangeRole(string login, string groupName,string changeLogin, GroupRole role, string uri)
        {
            var content = request.ChangeRole(login, groupName, changeLogin, role, uri).Result;
            return content.IsSuccessStatusCode;
        }
        
        private List<string>? ResponseInNames(HttpResponseMessage content)
        {
            return content.Content.ReadFromJsonAsync(typeof(List<string>)).Result as List<string>;
        }
        
        private Group? ResponseInGroup(HttpResponseMessage content)
        {
            return content.Content.ReadFromJsonAsync(typeof(Group)).Result as Group;
        }
    }
}