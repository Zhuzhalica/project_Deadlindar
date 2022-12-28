using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Deadlindar.Models;
using Deadlindar.Repositories;
using Deadlindar.Repositories.Json;
using Microsoft.EntityFrameworkCore;
using ValueObjects;
using WebAPI.Server.Data;

namespace WebAPI.Server.Services
{
    public class EventService: IEventService
    {
        private IEventRepository _eventRepository;
        
        public EventService(IEventRepository repository)
        {
            _eventRepository = repository;
        }
        
        public IEnumerable<Event> GetByLogin(string login)
        {
            return _eventRepository.GetByLogin(login);
        }

        public void Add(string login, Event deadline)
        {
            _eventRepository.Add(login, deadline);
        }

        public bool Delete(string login, Event deadline)
        {
            return _eventRepository.Delete(login, deadline);
        }
    }
}