using System.Net.Http;
using System.Threading.Tasks;
using ValueObjects;

namespace ClientModels
{
    public interface INotificationRequests
    {
        Task<HttpResponseMessage> Get(string login, string uri);
        Task<HttpResponseMessage> Add(string login, Notification obj, string uri);
        Task<HttpResponseMessage> Read(string login, Notification obj, string uri);
        Task<HttpResponseMessage> Delete(string login, Notification obj, string uri);
    }
}