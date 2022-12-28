using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using ValueObjects;

namespace ClientModels
{
    public class ClientEvents : IDisposable, IPersistingClient
    {
        [JsonIgnore] private readonly IRequests<Event> Requests;
        [JsonIgnore] public IReadOnlyList<Event> Events => _Events.AsReadOnly();
        private List<Event> _Events { get; set; }
        private IClientSaver Saver;


        public ClientEvents(IRequests<Event> requests, IClientSaver saver)
        {
            Requests = requests;
            Saver = saver;
        }

        public void Setup(string login)
        {
            var clientEvents = Saver.Read<ClientEvents>(login);
            if (clientEvents != null && clientEvents?._Events != null)
                _Events = clientEvents._Events;
            else
                _Events = new List<Event>();
        }

        public List<Event>? TryGet(User user, string uri)
        {
            var content = Requests.Get(user, uri);
            List<Event>? events = null;
            if (content.Result.IsSuccessStatusCode)
            {
                events = ResponseInEvent(content.Result);
                if (_Events.Count != 0)
                {
                    foreach (var _event in _Events)
                    {
                        if (!events.Contains(_event))
                        {
                            TryAdd(user.Login, _event, uri);
                            events.Add(_event);
                        }
                    }
                    
                }

                _Events.Clear();
                _Events.AddRange(events);
                return events;
            }
            else
            {
                return null;
            }
        }

        private List<Event>? ResponseInEvent(HttpResponseMessage content)
        {
            return content.Content.ReadFromJsonAsync(typeof(List<Event>)).Result as List<Event>;
        }

        public bool TryAdd(string login, Event _event, string uri)
        {
            var response = Requests.Add(login, _event, uri);
            _Events.Add(_event);
            return response.Result.IsSuccessStatusCode;
        }

        public bool TryDelete(string login, Event _event, string uri)
        {
            var response = Requests.Delete(login, _event, uri);
            if (_Events.Contains(_event))
            {
                var index = _Events.IndexOf(_event);
                _Events[index].IsDeleted = true;
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