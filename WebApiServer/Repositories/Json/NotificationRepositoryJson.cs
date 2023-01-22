using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using ValueObjects;
using static System.Text.Json.JsonSerializer;

namespace Deadlindar.Repositories.Json
{
    public class NotificationRepositoryJson: INotificationRepository
    {
        private readonly IJsonRepository repository;
        public NotificationRepositoryJson(IJsonRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Notification> GetByLogin(string login)
        {
            return repository.OpenFile<List<Notification>>(login);
        }

        public void Add(string login, Notification notification)
        {
            var notifications =repository.OpenFile<List<Notification>>(login);
            notifications.Add(notification);
            repository.SaveFile(login, notifications);
        }

        public bool Delete(string login, Notification notification)
        {
            var notifications = repository.OpenFile<List<Notification>>(login);
            var answer = false;
            var t = Equals(notifications[0], notification);
            if (notifications.Contains(notification))
            {
                var ind = notifications.IndexOf(notification);
                notifications[ind].IsDeleted = true;
                answer = true;
            }
            
            repository.SaveFile(login, notifications);
            return answer;
        }
    }
}