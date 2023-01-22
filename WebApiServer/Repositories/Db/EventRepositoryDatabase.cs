using System.Collections.Generic;
using System.Linq;
using Deadlindar.Models;
using ValueObjects;
using WebAPI.Server.Data;

namespace Deadlindar.Repositories
{
    public class EventRepository: IEventRepository
    {
        public EventRepository() { }
        
        public IEnumerable<Event> GetByLogin(string login)
        {
            using (EventContext db = new())
            {
                var l= db.Events.ToList();
                return l.Where(e => e.Login==login).Select(e => e.Event);
            }
        }

        public void Add(string login, Event deadline)
        {
            using (EventContext db = new())
            {
                var eventS = new EventServer(){Login = login, Event = deadline};
                db.Events.Add(eventS);
                var r = db.Events.ToList();
                db.SaveChanges();
            }
        }

        public bool Delete(string login, Event deadline)
        {
            using (EventContext db = new())
            {
                var events = GetByLogin(login);
                if (!events.Contains(deadline))
                    return false;
                db.Events.Remove(new EventServer(){Login = login, Event = deadline});
                db.SaveChanges();
                return true;
            }
        }
    }
}