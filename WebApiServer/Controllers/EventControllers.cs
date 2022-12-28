using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Server.Data;
using WebAPI.Server.Services;
using ValueObjects;

namespace WebAPI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventControllers : ControllerBase
    {
        private readonly ILogger<EventControllers> logger;
        private readonly IEventService _eventService;

        public EventControllers(ILogger<EventControllers> logger)
        {
            this.logger = logger;
            this._eventService = new EventService();
        }
        
        [HttpGet(Name="GetEvents{login}")]
        public IEnumerable<Event> GetEvents(string login)
        {
            logger.LogInformation(MyLogEvents.GetItem, "Get events");
            return _eventService.GetByLogin(login);
        }
        
        [HttpPost(Name="Add")]
        public ActionResult<Event> AddEvent(string login, [FromQuery] Event deadline)
        {
            
            _eventService.Add(login, deadline);
            logger.LogInformation(MyLogEvents.InsertItem, $"Add new event");
            return Ok();
        }

        [HttpDelete("delete")]
        public ActionResult<Event> Delete(string login, [FromQuery] Event deadline)
        {
            if (_eventService.Delete(login, deadline))
            {
                logger.LogInformation(MyLogEvents.DeleteItem, $"Event delete");
                return Ok();
            }
            logger.LogWarning(MyLogEvents.GetItemNotFound, $"Not event with {login}");
            return BadRequest(deadline);
        }

    }
}