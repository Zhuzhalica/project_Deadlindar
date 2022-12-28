using System.Collections.Generic;
using ValueObjects;

namespace WebAPI.Server.Services
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetByLogin(string login);
        void Add(string login, Notification notification);
        bool Delete(string login, Notification notification);
    }
}