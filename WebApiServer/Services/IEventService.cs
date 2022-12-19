using ValueObjects;

namespace WebAPI.Server.Services
{
    public interface IEventService
    {
        void AddEvent(string login, Event _event);
        void DeleteEvent(string login, Event _event);
    }
}