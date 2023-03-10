using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using ValueObjects;
using WebAPI.Server.Services;

namespace WebAPI.Server.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ApiUser : Controller
    {
        private readonly ILogger<ApiUser> logger;
        private readonly IUserService _userService;

        public ApiUser(ILogger<ApiUser> logger, IUserService service)
        {
            this.logger = logger;
            this.logger.LogInformation("User logger called");
            _userService = service;
        }

        //[AllowAnonymous]
        [HttpGet(Name = "GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            logger.LogInformation(MyLogEvents.GetItem, "Get users");
            return _userService.GetAll();
        }
        
        [HttpGet("{login}")]
        public ActionResult<User> GetByLogin(string login)
        {
            logger.LogInformation(MyLogEvents.GetItem, $"Get item {login}");
            var user = _userService.GetByLogin(login);
            if (user == null)
            {
                logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get {login} NOT FOUND");
                return NotFound();
            }
            return user;
        }

        [HttpPost("CreateUser")]
        public ActionResult<User> Create(string name, string surname, string login)
        {
            if (!_userService.IsLoginExist(login))
            {
                logger.LogWarning(MyLogEvents.GenerateItems, $"Login {login} is exist");
                return BadRequest();
            }
            var user = new User(0, name, surname, login);
            _userService.Add(user);
            logger.LogInformation(MyLogEvents.InsertItem, $"Create new user");
            return user;
        }
        
        
        [HttpPut("{id}")]
        public ActionResult<User> Update([FromQuery] User user)
        {
            var result = _userService.Update(user);
            if (!result)
            {
                logger.LogWarning(MyLogEvents.UpdateItemNotFound, $"Not  update");
                return NotFound();
            }
            logger.LogInformation(MyLogEvents.UpdateItem, $"Update is complete");
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            var user = _userService.Delete(id);
            if (user is null)
            {
                logger.LogInformation(MyLogEvents.UpdateItemNotFound, $"Not found user");
                return NotFound();
            }
            logger.LogInformation(MyLogEvents.DeleteItem, $"User delete");
            return Ok(user);
        }
    }
}