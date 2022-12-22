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
        private readonly string controllerName = "ApiUser";
        public async Task<HttpResponseMessage> Get(string login, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/{login}"),
                Method = HttpMethod.Get,
            };
            return await new Client().SendAsync(request);
        }

        public async Task<HttpResponseMessage> Add(string login, Event _event, string uri)
        {
            var content = JsonSerializer.Serialize(_event);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/Add?login={login}"),
                Method = HttpMethod.Post,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Accept", "application/json");

            return await new Client().SendAsync(request);
        }

        public async Task<HttpResponseMessage> Delete(string login, Event _event, string uri)
        {
            var content = JsonSerializer.Serialize(_event);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/{controllerName}/Delete?login={login}"),
                Method = HttpMethod.Delete,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Accept", "application/json");

            return await new Client().SendAsync(request);
        }
    }
}