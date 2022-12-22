using System.Collections.Generic;
using ValueObjects;

namespace ClientModels
{
    public interface IHandler
    {
        User User { get; }
        IReadOnlyList<Event> Events { get; }
        IReadOnlyList<Notification> Notifications { get; }
        string URI { get; }
        void Setup(User user);
        void Add(Notification notification);
        void Delete(Notification notification);
        void Add(Event _event);
        void Delete(Event _event);
    }
}