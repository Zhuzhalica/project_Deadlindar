using System.Collections.Generic;
using ValueObjects;

namespace Deadlindar.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetByLogin(string login);
        void Add(string login, Notification notification);
        bool Delete(string login, Notification notification);
    }
}