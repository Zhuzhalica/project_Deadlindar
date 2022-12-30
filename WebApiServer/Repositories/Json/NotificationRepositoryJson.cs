using System.Collections.Generic;
using System.IO;
using ValueObjects;
using static System.Text.Json.JsonSerializer;

namespace Deadlindar.Repositories.Json
{
    public class NotificationRepositoryJson: INotificationRepository, IJsonRepository<Notification>
    {
        public NotificationRepositoryJson(IJsonRepository<Notification> repository)
        {
            
        }

        public IEnumerable<Notification> GetByLogin(string login)
        {
            return OpenFile(login);
        }

        public void Add(string login, Notification notification)
        {
            var notifications = OpenFile(login);
            notifications.Add(notification);
            SaveFile(login, notifications);
        }

        public bool Delete(string login, Notification notification)
        {
            var notifications = OpenFile(login);
            var answer = false;
            var t = Equals(notifications[0], notification);
            if (notifications.Contains(notification))
            {
                var ind = notifications.IndexOf(notification);
                notifications[ind].IsDeleted = true;
                answer = true;
            }
            
            SaveFile(login, notifications);
            return answer;
        }

        private List<Notification> OpenFile(string login)
        {
            var notification = new List<Notification>();
            if (File.Exists($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\Notifications{login}.json"))
            {
                using FileStream stream =
                    File.OpenRead($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\Notifications{login}.json");
                notification = DeserializeAsync<List<Notification>>(stream).Result;
            }

            notification ??= new List<Notification>();
            return notification;
        }

        private void SaveFile(string login, List<Notification> events)
        {
            using FileStream createStream = File.Create($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\Notifications{login}.json");
            SerializeAsync(createStream, events);
        }
    }
}