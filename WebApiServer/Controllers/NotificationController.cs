using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ValueObjects;
using WebAPI.Server.Services;

namespace WebAPI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController: ControllerBase
    {
        private readonly ILogger<NotificationController> logger;
        private readonly INotificationService _eventService;

        public NotificationController(ILogger<NotificationController> logger, INotificationService service)
        {
            this.logger = logger;
            this._eventService = service;
        }
        
        [HttpGet(Name="GetNotifications{login}")]
        public IEnumerable<Notification> GetNotifications(string login)
        {
            logger.LogInformation(MyLogEvents.GetItem, "Get notification");
            return _eventService.GetByLogin(login);
        }
        
        [HttpPost("Add")]
        public ActionResult<Event> AddNotification(string login, Notification notification)
        {
            _eventService.Add(login, notification);
            logger.LogInformation(MyLogEvents.InsertItem, $"Add new event");
            return Ok();
        }

        [HttpDelete("Delete")]
        public ActionResult<Event> Delete(string login, Notification notification)
        {
            if (_eventService.Delete(login, notification))
            {
                logger.LogInformation(MyLogEvents.DeleteItem, $"Event delete");
                return Ok();
            }
            logger.LogWarning(MyLogEvents.GetItemNotFound, $"Not event with {login}");
            return BadRequest(notification);
        }
    }
}