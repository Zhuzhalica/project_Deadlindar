using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using ValueObjects;

namespace ClientModels
{
    public class ClientNotifications : IPersistingClient
    {
        private readonly INotificationRequests requests;

        public ClientNotifications(INotificationRequests requests)
        {
            this.requests = requests;
        }

        public List<Notification>? TryGet(string login, string uri)
        {
            var content = requests.Get(login, uri);
            if (content.Result.IsSuccessStatusCode)
            {
                var notifications = ResponseInNotification(content.Result);
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
            var response = requests.Add(login, notification, uri);
            return response.Result.IsSuccessStatusCode;
        }
        
        public bool TryRead(string login, Notification notification, string uri)
        {
            var response = requests.Read(login, notification, uri);
            return response.Result.IsSuccessStatusCode;
        }

        public bool TryDelete(string login, Notification notification, string uri)
        {
            var response = requests.Delete(login, notification, uri);
            return response.Result.IsSuccessStatusCode;
        }
    }
}