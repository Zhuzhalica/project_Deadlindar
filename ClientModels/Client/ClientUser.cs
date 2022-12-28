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

        public User? TryGet(string login, string uri)
        {
            var content = Requests.Get(login, uri);
            if (content.Result.IsSuccessStatusCode)
            {
                User = ResponseInUser(content.Result);
            }

            return User;
        }

        private User? ResponseInUser(HttpResponseMessage content)
        {
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