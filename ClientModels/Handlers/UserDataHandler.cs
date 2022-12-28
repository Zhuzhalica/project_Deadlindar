using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ValueObjects;

namespace ClientModels
{
    public class UserDataHandler : IHandler, IDisposable
    {
        public string URI => "https://localhost:7135";
        public ClientEvents ClientEvents { get; }
        public ClientNotifications ClientNotifications { get;  }
        public ClientUser ClientUser { get; }

        public UserDataHandler(ClientEvents clientEvents, ClientNotifications clientNotifications, ClientUser clientUser)
        {
            ClientEvents = clientEvents;
            ClientNotifications = clientNotifications;
            ClientUser = clientUser;
        }
        
        public void Setup()
        {
            ClientEvents.TryGet(ClientUser.User.Login, URI);
            ClientNotifications.TryGet(ClientUser.User.Login, URI);
        }

        public void Add(Notification notification)
        {
            ClientNotifications.TryAdd(ClientUser.User.Login, notification, URI);
        }

        public void Delete(Notification notification)
        {
            ClientNotifications.TryDelete(ClientUser.User.Login, notification, URI);
        }

        public void Add(Event _event)
        {
            ClientEvents.TryAdd(ClientUser.User.Login, _event, URI);
        }

        public void Delete(Event _event)
        {
            ClientEvents.TryDelete(ClientUser.User.Login, _event, URI);
        }

        public void Dispose()
        {
            ClientEvents.Dispose();
            ClientNotifications.Dispose();
        }
    }
}