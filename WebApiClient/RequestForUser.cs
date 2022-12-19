using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ValueObjects;

namespace WebApiClient
{
    public class RequestForUser
    {
        public static User? TryAuthorization(string login, string uri)
        {
            var req = ClientRequests.GetUserRequestConstruct(login, uri);
            var content = ClientRequests.Get(req);
            return content.IsSuccessStatusCode 
                ? ResponseInUser(content) 
                : null;
        }

        private static User? ResponseInUser(HttpResponseMessage content)
        {
            return content.Content.ReadFromJsonAsync(typeof(User)).Result as User;
        }

        public static async void Registration(string uri, string login = "", string name = "", string surname = "")
        {
            var user = new User(0, name, surname, login);
            var req = ClientRequests.AddNewUserRequestConstruct(user, uri);
            var cont = await ClientRequests.Post(req);
        }
    }
}