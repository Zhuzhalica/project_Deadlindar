using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using ValueObjects;

namespace ClientModels
{
    public class ClientNotifications : IDisposable
    {
        private readonly IRequests<Notification> Requests;
        private List<Notification> _Notifications { get; set; }
        [JsonIgnore] public IReadOnlyList<Notification> Notifications => _Notifications.AsReadOnly();

        public ClientNotifications(IRequests<Notification> requests)
        {
            Requests = requests;
        }

        public List<Notification>? TryGet(string login, string uri)
        {
            var content = Requests.Get(login, uri);
            return content.Result.IsSuccessStatusCode
                ? ResponseInNotification(content.Result)
                : null;
        }

        private List<Notification>? ResponseInNotification(HttpResponseMessage content)
        {
            return content.Content.ReadFromJsonAsync(typeof(List<Notification>)).Result as List<Notification>;
        }

        public bool TryAdd(string login, Notification notification, string uri)
        {
            var response = Requests.Add(login, notification, uri);
            return response.Result.IsSuccessStatusCode;
        }

        public bool TryDelete(string login, Notification notification, string uri)
        {
            var response = Requests.Delete(login, notification, uri);
            return response.Result.IsSuccessStatusCode;
        }

        public void Dispose()
        {
        }
    }
}