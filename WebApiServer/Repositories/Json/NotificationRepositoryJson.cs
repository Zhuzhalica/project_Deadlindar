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
            return repository.OpenFile<List<Notification>>(login, $"Notification{login}");
        }

        public void Add(string login, Notification notification)
        {
            var notifications =repository.OpenFile<List<Notification>>(login, $"Notification{login}");
            notifications.Add(notification);
            repository.SaveFile(login, notifications, $"Notification{login}");
        }

        public bool Read(string login, Notification notification)
        {
            var notifications = repository.OpenFile<List<Notification>>(login, $"Notification{login}");
            var answer = false;
            var t = Equals(notifications[0], notification);
            if (notifications.Contains(notification))
            {
                var ind = notifications.IndexOf(notification);
                notifications[ind].IsRead = true;
                answer = true;
            }
            
            repository.SaveFile(login, notifications, $"Notification{login}");
            return answer;
        }

        public bool Delete(string login, Notification notification)
        {
            var notifications = repository.OpenFile<List<Notification>>(login, $"Notification{login}");
            var answer = false;
            if (notifications.Contains(notification))
            {
                notifications.Remove(notification);
                answer = true;
            }
            
            repository.SaveFile(login, notifications, $"Notification{login}");
            return answer;
        }
    }
}