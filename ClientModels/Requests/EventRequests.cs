using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ValueObjects;

namespace ClientModels
{
    public class EventRequests : IRequests<Event>
    {
        private readonly string controllerName = "EventControllers";
        public Task<HttpResponseMessage> Get(User user, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}?Login={user.Login}"),
                Method = HttpMethod.Get,
            };
            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> Add(string login, Event _event, string uri)
        {
            var content = JsonSerializer.Serialize(_event);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/Add?login={login}"),
                Method = HttpMethod.Post,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Accept", "application/json");

            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> Delete(string login, Event _event, string uri)
        {
            var content = JsonSerializer.Serialize(_event);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/Delete?login={login}"),
                Method = HttpMethod.Delete,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Accept", "application/json");

            return new Client().SendAsync(request);
        }
    }
}