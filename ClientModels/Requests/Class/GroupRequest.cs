using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ValueObjects;

namespace ClientModels
{
    public class GroupRequest: IGroupRequest
    {
        private readonly string controllerName = "Group";

        public Task<HttpResponseMessage> GetAll(string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/GetAll"),
                Method = HttpMethod.Get,
            };
            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> GetNamesByLogin(string login, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/GetNamesByLogin?login={login}"),
                Method = HttpMethod.Get,
            };
            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> GetByName(string login, string groupName, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/GetByName?login={login}&groupName={groupName}"),
                Method = HttpMethod.Get,
            };
            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> Create(string login, Group @group, string uri)
        {
            var content = JsonSerializer.Serialize(@group);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/Create?login={login}"),
                Method = HttpMethod.Post,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Accept", "application/json");
            return new Client().SendAsync(request);;
        }

        public Task<HttpResponseMessage> Delete(string login, string groupName, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/Delete?login={login}&groupName={groupName}"),
                Method = HttpMethod.Delete,
            };
            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> AddUser(string login, string groupName, string otherLogin, GroupRole role, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/AddUser?memberLogin={login}&groupName={groupName}&newLogin={otherLogin}&role={role}"),
                Method = HttpMethod.Post,
            };
            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> RemoveUser(string login, string groupName, string otherLogin, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/RemoveUser?memberLogin={login}&groupName={groupName}&removeLogin={otherLogin}"),
                Method = HttpMethod.Delete,
            };
            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> AddEvent(string login, string groupName, Event @event, string uri)
        {
            var content = JsonSerializer.Serialize(@event);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/AddEvent?memberLogin={login}&groupName={groupName}"),
                Method = HttpMethod.Post,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Accept", "application/json");
            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> RemoveEvent(string login, string groupName, Event @event, string uri)
        {
            var content = JsonSerializer.Serialize(@event);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/RemoveEvent?memberLogin={login}&groupName={groupName}"),
                Method = HttpMethod.Delete,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Accept", "application/json");
            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> ChangeRole(string login, string groupName, string otherLogin, GroupRole role, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/ChangeRole?memberLogin={login}&groupName={groupName}&changeRoleLogin={otherLogin}&role={role}"),
                Method = HttpMethod.Put,
            };
            return new Client().SendAsync(request);
        }
    }
}