using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using ValueObjects;

namespace ClientModels
{
    public class ClientEvents: IDisposable
    {
        [JsonIgnore]
        private readonly IRequests<Event> Requests;
        [JsonIgnore]
        public IReadOnlyList<Event> Events => _Events.AsReadOnly();
        private List<Event> _Events { get; set; }

        
        public ClientEvents(IRequests<Event> requests)
        {
            Requests = requests;
        }

        public List<Event>? TryGet(string login, string uri)
        {
            var content = Requests.Get(login, uri);
            if (content.Result.IsSuccessStatusCode)
            {
                _Events = ResponseInEvent(content.Result);
            }

            return _Events;
        }

        private List<Event>? ResponseInEvent(HttpResponseMessage content)
        {
            return content.Content.ReadFromJsonAsync(typeof(List<Event>)).Result as List<Event>;
        }

        public bool TryAdd(string login, Event _event, string uri)
        {
            var response = Requests.Add(login, _event, uri);
            return response.Result.IsSuccessStatusCode;
        }

        public bool TryDelete(string login, Event _event, string uri)
        {
            var response = Requests.Delete(login, _event, uri);
            return response.Result.IsSuccessStatusCode;
        }

        public void Dispose()
        {
            
        }
    }
}