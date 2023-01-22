using System.Collections.Generic;
using System.Text.Json.Serialization;
using ValueObjects;

namespace ClientModels
{
    public interface IEventHandler: ISyncHandler<List<Event>>
    {
        public IReadOnlyList<Event> Events { get; }
        void Setup(string login, string uri);
        List<Event> Get(string login, string uri);
        void Add(string login, Event _event, string uri);
        void Delete(string login, Event _event, string uri);
    }
}