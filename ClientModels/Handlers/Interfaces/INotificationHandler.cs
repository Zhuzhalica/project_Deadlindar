using System.Collections.Generic;
using ValueObjects;

namespace ClientModels
{
    public interface INotificationHandler: ISyncHandler<List<Notification>>
    {
        void Setup(string login, string uri);
        List<Notification> Get(string login, string uri);
        void Add(string login, Notification notification, string uri);
        void Read(string login, Notification notification, string uri);
        void Delete(string login, Notification notification, string uri);
    }
}