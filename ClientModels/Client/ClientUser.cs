using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using ValueObjects;

namespace ClientModels
{
    public class ClientUser
    {
        private readonly IRequests<User> Requests;
        public User User { get; private set; }

        public ClientUser(IRequests<User> requests)
        {
            Requests = requests;
        }

        public User? TryGet(User user, string uri)
        {
            var content = Requests.Get(user, uri);
            if (content.Result.IsSuccessStatusCode)
            {
                User = ResponseInUser(content.Result);
            }

            return User;
        }

        private User? ResponseInUser(HttpResponseMessage content)
        {
            var t = content.Content;
            var f = content.Content.ReadAsStringAsync().Result;
            return content.Content.ReadFromJsonAsync(typeof(User)).Result as User;
        }

        public bool TryAdd(string login, User user, string uri)
        {
            var response = Requests.Add(login, user, uri);
            return response.Result.IsSuccessStatusCode;
        }

        public bool TryDelete(string login, User user, string uri)
        {
            var response = Requests.Delete(login, user, uri);
            return response.Result.IsSuccessStatusCode;
        }
    }
}