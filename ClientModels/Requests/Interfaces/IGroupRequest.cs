using System.Net.Http;
using System.Threading.Tasks;
using ValueObjects;

namespace ClientModels
{
    public interface IGroupRequest
    {
        Task<HttpResponseMessage> GetAll(string uri);
        Task<HttpResponseMessage> GetNamesByLogin(string login, string uri);
        Task<HttpResponseMessage> GetByName(string login, string groupName, string uri);
        Task<HttpResponseMessage> Create(string login, Group @group, string uri);
        Task<HttpResponseMessage> Delete(string login, string groupName, string uri);
        Task<HttpResponseMessage> AddUser(string login, string groupName, string otherLogin, GroupRole role, string uri);
        Task<HttpResponseMessage> RemoveUser(string login, string groupName, string otherLogin, string uri);
        Task<HttpResponseMessage> AddEvent(string login, string groupName, Event @event, string uri);
        Task<HttpResponseMessage> RemoveEvent(string login, string groupName, Event @event, string uri);
        Task<HttpResponseMessage> ChangeRole(string login, string groupName, string otherLogin, GroupRole role, string uri);
    }
}