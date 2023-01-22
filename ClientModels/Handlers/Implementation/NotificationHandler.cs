using System.Collections.Generic;
using System.Text.Json.Serialization;
using ValueObjects;

namespace ClientModels
{
    public class NotificationHandler : INotificationHandler
    {
        [JsonIgnore] private ClientNotifications ClientNotifications { get; }
        [JsonIgnore] public IReadOnlyList<Notification> Notifications => _Notifications.AsReadOnly();
        private List<Notification> _Notifications { get; set; }
        private List<Notification> NoneSyncNotifications { get; set; }
        private IClientSaver Saver;

        public NotificationHandler(ClientNotifications clientNotifications, IClientSaver saver)
        {
            ClientNotifications = clientNotifications;
            Saver = saver;
        }

        public void Setup(string login, string uri)
        {
            var saveNotifications =  Saver.Read<List<Notification>>(login);
            var notifications = new List<Notification>();
            if (saveNotifications != null)
                notifications = Sync(login, saveNotifications, uri);

            var serverNotifications = Get(login, uri);
            notifications.AddRange(serverNotifications);
            _Notifications = notifications;
        }

        public List<Notification> Get(string login, string uri)
        {
            var notifications =  ClientNotifications.TryGet(login, uri);
            if (notifications == null)
            {
                // уведомление не удалось синхронизироваться с данными сервера
                return new List<Notification>();
            }

            return notifications;
        }

        public void Add(string login, Notification notification, string uri)
        {
            _Notifications.Add(notification);
            if (!ClientNotifications.TryAdd(login, notification, uri))
                NoneSyncNotifications.Add(notification);
        }
        
        public void Read(string login, Notification notification, string uri)
        {
            if (_Notifications.Contains(notification))
            {
                var index = NoneSyncNotifications.IndexOf(notification);
                NoneSyncNotifications[index].IsRead = true;
                
                if (!ClientNotifications.TryDelete(login, notification, uri))
                {
                    if (NoneSyncNotifications.Contains(notification))
                    {
                        index = NoneSyncNotifications.IndexOf(notification);
                        NoneSyncNotifications[index].IsRead = true;
                    }
                    else
                    {
                        notification.IsRead = true;
                        NoneSyncNotifications.Add(notification);
                    }
                }
            }
        }

        public void Delete(string login, Notification notification, string uri)
        {
            if (_Notifications.Contains(notification))
            {
                _Notifications.Remove(notification);
                
                if (!ClientNotifications.TryDelete(login, notification, uri))
                {
                    if (NoneSyncNotifications.Contains(notification))
                    {
                        var index = NoneSyncNotifications.IndexOf(notification);
                        NoneSyncNotifications[index].IsDeleted = true;
                    }
                    else
                    {
                        notification.IsDeleted = true;
                        NoneSyncNotifications.Add(notification);
                    }
                }
            }
        }

        public List<Notification> Sync(string login, List<Notification> notifications, string uri)
        {
            var result = new List<Notification>();
            foreach (var notification in notifications)
            {
                if (!notification.IsDeleted && !notification.IsRead && !ClientNotifications.TryAdd(login, notification, uri))
                    result.Add(notification);
                else if (!notification.IsDeleted && notification.IsRead && !ClientNotifications.TryRead(login, notification, uri))
                    result.Add(notification);
                else if (notification.IsDeleted && !ClientNotifications.TryDelete(login, notification, uri))
                    result.Add(notification);
            }

            return result;
        }

        public void Dispose()
        {
        }
    }
}