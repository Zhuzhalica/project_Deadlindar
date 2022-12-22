using System.Collections.Generic;
using System.Text.Json.Serialization;
using ValueObjects;

namespace ClientModels
{
    public class UserDataHandler : IHandler
    {
        public User User { get; private set; }
        private List<Event> _Events { get; set; }
        private List<Notification> _Notifications { get; set; }
        
        [JsonIgnore]
        public IReadOnlyList<Event> Events => _Events.AsReadOnly();
        [JsonIgnore]
        public IReadOnlyList<Notification> Notifications => _Notifications.AsReadOnly();
        [JsonIgnore]
        public string URI => "https://localhost:7135";

        private static IService<Event> EventService;
        private static IService<Notification> NotificationsService;

        public UserDataHandler(IService<Event> eventService, IService<Notification> notificationService)
        {
            EventService = eventService;
            NotificationsService = notificationService;
        }
        
        public void Setup(User user)
        {
            User = user;
            
            var events = EventService.TryGet(User.Login, URI);
            if (events is null)
            {
            }
            else
            {
                _Events = events;
            }

            var notifications = NotificationsService.TryGet(User.Login, URI);
            if (notifications is null)
            {
            }
            else
            {
                _Notifications = notifications;
            }
        }

        public void Add(Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Event _event)
        {
            _Events.Add(_event);
            var success = EventService.TryAdd(User.Login, _event, URI);
            if (!success)
            {
            }
        }

        public void Delete(Event _event)
        {
            _Events.Remove(_event);
            var success = EventService.TryDelete(User.Login, _event, URI);
            if (!success)
            {
            }
        }
    }
}