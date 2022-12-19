using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ValueObjects;

namespace WebApiClient
{
    public static class ClientRequests
    {
        public static HttpResponseMessage Get(HttpRequestMessage request)
        {
            return new Client().SendAsync(request).Result;
        }

        public static HttpRequestMessage GetUserRequestConstruct(string identificator, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/ApiUser/{identificator}"),
                Method = HttpMethod.Get,
            };
            return request;
        }

        // public static HttpRequestMessage AuthorizationRequestConstruct(User user, string uri)
        // {
        //     var request = new HttpRequestMessage()
        //     {
        //         RequestUri = new Uri($"{uri}"),
        //         Method = HttpMethod.Get,
        //     };
        //     request.Headers.Authorization = new AuthenticationHeaderValue("Basic", $"{user.Name}&&{user.Surname}");
        //     return request;
        // }

        public static async Task<HttpResponseMessage> Post(HttpRequestMessage request)
        {
            return await new Client().SendAsync(request);
        }

        public static HttpRequestMessage AddNewUserRequestConstruct(User server, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/ApiUser/CreateUser?name={server.Name}&surname={server.Surname}&login={server.Login}"),
                Method = HttpMethod.Post,
            };
            return request;
        }
        public static HttpRequestMessage AddNewEventRequestConstruct(string login, Event _event, string uri)
        {
            var content = JsonSerializer.Serialize(_event);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/ApiUser/AddEvent?login={login}"),
                Method = HttpMethod.Post,
                Content = new StringContent(content.ToString(), Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Accept", "application/json");
            return request;
        }
        
        public static HttpRequestMessage DeleteEventRequestConstruct(string login, Event _event, string uri)
        {
            var content = JsonSerializer.Serialize(_event);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/ApiUser/DeleteEvent?login={login}"),
                Method = HttpMethod.Delete,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Accept", "application/json");
            return request;
        }
    }
}