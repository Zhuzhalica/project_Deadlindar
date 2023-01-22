using System.Net.Http;
using System.Threading.Tasks;
using ValueObjects;

namespace ClientModels
{
    public interface IEventRequest
    {
        Task<HttpResponseMessage> Get(string login, string uri);
        Task<HttpResponseMessage> Add(string login, Event obj, string uri);
        Task<HttpResponseMessage> Delete(string login, Event obj, string uri);
    }
}