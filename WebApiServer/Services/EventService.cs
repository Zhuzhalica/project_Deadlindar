using System;
using System.Collections.Generic;
using System.Linq;
using Deadlindar.Models;
using Microsoft.EntityFrameworkCore;
using ValueObjects;
using WebAPI.Server.Data;

namespace WebAPI.Server.Services
{
    public class EventService: IEventService
    {
        private EventContext context;
        
        public EventService()
        {
            context = new EventContext(new DbContextOptions<EventContext>());
        }

        // public void AddEvents(UserEvent user)
        // {
        //     using (EventContext db = new())
        //     {
        //         foreach (var ev in user.Events)
        //         {
        //             db.Events.Add(ev);
        //         }
        //     }
        // }
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
                db.SaveChanges();
            }
        }

        public bool Delete(string login, Event deadline)
        {
            using (EventContext db = new())
            {
                if (!GetByLogin(login).Contains(deadline))
                    return false;
                db.Events.Remove(new EventServer(){Login = login, Event = deadline});
                db.SaveChanges();
                return true;
            }
        }
    }
}