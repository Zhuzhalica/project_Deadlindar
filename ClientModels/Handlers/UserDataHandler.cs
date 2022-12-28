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
        
        public void Setup(string login)
        {
            ClientEvents.Setup(login);
            var events = ClientEvents.TryGet(ClientUser.User, URI);
            if (events == null)
            {
                // уведомление не удалось синхронизироваться с данными сервера
            }
            
            ClientNotifications.Setup(login);
            var notifications = ClientNotifications.TryGet(ClientUser.User, URI);
            if (notifications == null)
            {
                // уведомление не удалось синхронизироваться с данными сервера
            }
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
            ClientEvents.Dispose(ClientUser.User, URI);
            ClientNotifications.Dispose(ClientUser.User, URI);
        }
    }
}