using System.Net.Http;
using System.Threading.Tasks;

namespace ClientModels
{
    public interface IUserRequest : IRequests<User>
    {
        Task<HttpResponseMessage> CheckLoginExist(User user, string uri);
    }
}