using System.Collections.Generic;
using ValueObjects;

namespace ClientModels
{
    public interface IHandler
    {
        ClientEvents ClientEvents { get; }
        ClientNotifications ClientNotifications { get; }
        ClientUser ClientUser { get; }
        string URI { get; }
        void Setup();
        void Add(Notification notification);
        void Delete(Notification notification);
        void Add(Event _event);
        void Delete(Event _event);
    }
}