using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using ValueObjects;

namespace Deadlindar.Repositories.Json
{
    public class EventRepositoryJson: IEventRepository
    {
        private readonly IJsonRepository repository;
        public EventRepositoryJson(IJsonRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Event> GetByLogin(string login)
        {
            return repository.OpenFile<List<Event>>(login, $"Event{login}");
        }

        public void Add(string login, Event deadline)
        {
            var events = repository.OpenFile<List<Event>>(login, $"Event{login}");
            events.Add(deadline);
            repository.SaveFile(login, events, $"Event{login}");
        }

        public bool Delete(string login, Event deadline)
        {
            var events = repository.OpenFile<List<Event>>(login, $"Event{login}");
            var answer = false;
            if (events.Contains(deadline))
            {
                var ind = events.IndexOf(deadline);
                events[ind].IsDeleted = true;
                answer = true;
            }
            
            repository.SaveFile(login, events, $"Event{login}");
            return answer;
        }
    }
}