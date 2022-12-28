using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ValueObjects;

namespace Deadlindar.Repositories.Json
{
    public class EventRepositoryJson: IEventRepository, IJsonRepository<Event>
    {
        private List<Event> OpenFile(string login)
        {
            var events = new List<Event>();
            if (File.Exists($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\Events{login}.json"))
            {
                using FileStream stream =
                    File.OpenRead($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\Events{login}.json");
                events = JsonSerializer.DeserializeAsync<List<Event>>(stream).Result;
            }

            events ??= new List<Event>();
            return events;
        }

        private void SaveFile(string login, List<Event> events)
        {
            using FileStream createStream = File.Create($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\Events{login}.json");
            JsonSerializer.SerializeAsync(createStream, events);
        }

        public IEnumerable<Event> GetByLogin(string login)
        {
            return OpenFile(login);
        }

        public void Add(string login, Event deadline)
        {
            var events = OpenFile(login);
            events.Add(deadline);
            SaveFile(login, events);
        }

        public bool Delete(string login, Event deadline)
        {
            var events = OpenFile(login);
            var answer = false;
            var t = Equals(events[0], deadline);
            if (events.Contains(deadline))
            {
                var ind = events.IndexOf(deadline);
                events[ind].IsDeleted = true;
                answer = true;
            }
            
            SaveFile(login, events);
            return answer;
        }
    }
}