using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ValueObjects;

namespace ClientModels
{
    public class UserRequests : IUserRequest
    {
        private readonly string controllerName = "Account";

        public Task<HttpResponseMessage> Get(User user, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/login?Login={user.Login}&Password={user.Password}"),
                Method = HttpMethod.Get,
            };

            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> Add(string login, User user, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri =
                    new Uri(
                        $"{uri}/register?Name={user.Name}&Surname={user.Surname}&Login={user.Login}&Password={user.Password}"),
                Method = HttpMethod.Post,
            };
            return new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> Delete(string login, User user, string uri)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> CheckLoginExist(User user, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{uri}/loginExist?Login={user.Login}&Password={user.Password}"),
                Method = HttpMethod.Get,
            };
            return new Client().SendAsync(request);
        }
    }
}