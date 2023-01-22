using System.Net.Http;
using System.Threading.Tasks;
using ValueObjects;

namespace ClientModels
{
    public interface IUserRequest
    {
        Task<HttpResponseMessage> Get(User user, string uri);
        Task<HttpResponseMessage> Add(string login, User obj, string uri);
        Task<HttpResponseMessage> Delete(string login, User obj, string uri);
        Task<HttpResponseMessage> CheckLoginExist(User user, string uri);
    }
}