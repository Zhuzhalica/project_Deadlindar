using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ValueObjects;
using WebAPI.Server.Services;

namespace WebAPI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> logger;
        private readonly INotificationService _eventService;

        public NotificationController(ILogger<NotificationController> logger, INotificationService service)
        {
            this.logger = logger;
            this._eventService = service;
        }

        [HttpGet(Name = "GetNotifications{login}")]
        public IEnumerable<Notification> GetNotifications(string login)
        {
            logger.LogInformation(MyLogEvents.GetItem, "Get notification");
            return _eventService.GetByLogin(login);
        }

        [HttpPost("Add")]
        public ActionResult<Notification> AddNotification(string login, Notification notification)
        {
            _eventService.Add(login, notification);
            logger.LogInformation(MyLogEvents.InsertItem, $"Add new event");
            return Ok();
        }

        [HttpPut("Read")]
        public ActionResult<Notification> ReadNotification(string login, Notification notification)
        {
            if (_eventService.Read(login, notification))
            {
                logger.LogInformation(MyLogEvents.InsertItem,
                    $"Read notification. Notifictaion: {notification.Title}, Login: {login}");
                return Ok();
            }
            
            logger.LogWarning(MyLogEvents.GetItemNotFound, $"Not notification with {login}");
            return BadRequest(notification);
        }

        [HttpDelete("Delete")]
        public ActionResult<Notification> Delete(string login, Notification notification)
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