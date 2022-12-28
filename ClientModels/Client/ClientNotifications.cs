using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using ValueObjects;

namespace ClientModels
{
    public class ClientNotifications : IDisposable, IPersistingClient
    {
        private readonly IRequests<Notification> Requests;
        private List<Notification> _Notifications { get; set; }
        [JsonIgnore] public IReadOnlyList<Notification> Notifications => _Notifications.AsReadOnly();
        private IClientSaver Saver;

        public ClientNotifications(IRequests<Notification> requests, IClientSaver saver)
        {
            Requests = requests;
            Saver = saver;
        }
        
        public void Setup(string login)
        {
            var clientNotifications = Saver.Read<ClientNotifications>(login);
            if (clientNotifications != null && clientNotifications?._Notifications != null)
                _Notifications = clientNotifications._Notifications;
            else
                _Notifications = new List<Notification>();
        }

        public List<Notification>? TryGet(User user, string uri)
        {
            var content = Requests.Get(user, uri);
            List<Notification>? notifications = null;
            if (content.Result.IsSuccessStatusCode)
            {
                notifications = ResponseInNotification(content.Result);
                if (_Notifications.Count != 0)
                {
                    foreach (var notification in _Notifications)
                    {
                        if (!notifications.Contains(notification))
                        {
                            TryAdd(user.Login, notification, uri);
                            notifications.Add(notification);
                        }
                    }
                    
                }

                _Notifications.Clear();
                _Notifications.AddRange(notifications);
                return notifications;
            }
            else
            {
                return null;
            }
        }

        private List<Notification>? ResponseInNotification(HttpResponseMessage content)
        {
            return content.Content.ReadFromJsonAsync(typeof(List<Notification>)).Result as List<Notification>;
        }

        public bool TryAdd(string login, Notification notification, string uri)
        {
            var response = Requests.Add(login, notification, uri);
            _Notifications.Add(notification);
            return response.Result.IsSuccessStatusCode;
        }

        public bool TryDelete(string login, Notification notification, string uri)
        {
            var response = Requests.Delete(login, notification, uri);
            if (_Notifications.Contains(notification))
            {
                var index = _Notifications.IndexOf(notification);
                _Notifications[index].IsDeleted = true;
            }
            return response.Result.IsSuccessStatusCode;
        }

        public void Dispose(User user, string uri)
        {
            if (TryGet(user, uri) == null)
                Saver.Save(user.Login, this);
            Dispose();
        }

        public void Dispose()
        {
        }
    }
}