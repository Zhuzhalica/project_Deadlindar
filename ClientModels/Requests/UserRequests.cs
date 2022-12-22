using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ValueObjects;

namespace ClientModels
{
    public class UserRequests : IRequests<User>
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

        public async Task<HttpResponseMessage> Add(string login, User user, string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri =
                    new Uri(
                        $"{uri}/{controllerName}/CreateUser?name={user.Name}&surname={user.Surname}&login={user.Login}"),
                Method = HttpMethod.Post,
            };
            return await new Client().SendAsync(request);
        }

        public Task<HttpResponseMessage> Delete(string login, User user, string uri)
        {
            throw new NotImplementedException();
        }
    }
}