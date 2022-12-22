using System.Net.Http;
using System.Threading.Tasks;
using ValueObjects;

namespace ClientModels
{
    public interface IRequests<T>
    {
        // string controllerName = {get;}
        Task<HttpResponseMessage> Get(string login, string uri);
        Task<HttpResponseMessage> Add(string login, T obj, string uri);
        Task<HttpResponseMessage> Delete(string login, T obj, string uri);
    }
}