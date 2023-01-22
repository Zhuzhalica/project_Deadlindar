using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using ValueObjects;

namespace ClientModels
{
    public class EventsHandler : IEventHandler
    {
        [JsonIgnore] private ClientEvents ClientEvents { get; }
        [JsonIgnore] public IReadOnlyList<Event> Events => _Events.AsReadOnly();
        private List<Event> _Events { get; set; }
        private List<Event> NoneSyncEvents { get; set; }
        private IClientSaver Saver;

        public EventsHandler(ClientEvents clientEvents, IClientSaver saver)
        {
            ClientEvents = clientEvents;
            Saver = saver;
        }

        public void Setup(string login, string uri)
        {
            var saveEvents = Saver.Read<List<Event>>(login);
            var events = new List<Event>();
            if (saveEvents != null)
                events = Sync(login, saveEvents, uri);

            var serverEvents = Get(login, uri);
            events.AddRange(serverEvents);
            _Events = events;
        }

        public List<Event> Get(string login, string uri)
        {
            var events = ClientEvents.TryGet(login, uri);
            if (events == null)
            {
                // уведомление не удалось синхронизироваться с данными сервера
                return new List<Event>();
            }

            return events;
        }

        public void Add(string login, Event _event, string uri)
        {
            _Events.Add(_event);
            if (!ClientEvents.TryAdd(login, _event, uri))
                NoneSyncEvents.Add(_event);
        }

        public void Delete(string login, Event _event, string uri)
        {
            if (_Events.Contains(_event))
            {
                var index = _Events.IndexOf(_event);
                _Events[index].IsDeleted = true;

                if (!ClientEvents.TryDelete(login, _event, uri))
                {
                    if (NoneSyncEvents.Contains(_event))
                    {
                        index = NoneSyncEvents.IndexOf(_event);
                        NoneSyncEvents[index].IsDeleted = true;
                    }
                    else
                    {
                        _event.IsDeleted = true;
                        NoneSyncEvents.Add(_event);
                    }
                }
            }
        }

        public List<Event> Sync(string login, List<Event> notifications, string uri)
        {
            var result = new List<Event>();
            foreach (var @event in notifications)
            {
                if (!@event.IsDeleted && !ClientEvents.TryAdd(login, @event, uri))
                    result.Add(@event);
                else if (@event.IsDeleted && !ClientEvents.TryDelete(login, @event, uri))
                    result.Add(@event);
            }

            return result;
        }

        public void Dispose()
        {
            // ToDo
            // if (NoneSyncEvents.Count != 0)
            //     NoneSyncEvents = Sync(NoneSyncEvents);
        }
    }
}