// using System.Collections.Generic;
// using System.Linq;
// using Deadlindar.Models;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
//
// namespace WebAPI.Server.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class EventController2 : ControllerBase
//     {
//         private readonly ILogger<EventController2> logger;
//         private UserManager<UserEvent> userManager;
//
//         public EventController2(UserManager<UserEvent> userManager)
//         {
//             this.userManager = userManager;
//         }
//         
//         [HttpGet(Name="Get Events")]
//         public IEnumerable<UserEvent> GetEvents(string login)
//         {
//             logger.LogInformation(MyLogEvents.GetItem, "Get users");
//             return userManager.Users.ToList();
//         }
//     }
// }