
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Deadlindar.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using NUnit.Framework;
using ValueObjects;
using WebAPI.Server.Services;
//
namespace WebAPI.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController :Controller
    {
        private IUserService userService;
        private readonly ILogger<AccountController> logger;
        
        public AccountController(ILogger<AccountController> logger, IUserService service)
        {
            this.logger = logger;
            userService = service;
        }

        [AllowAnonymous]
        [HttpGet("/login")]
        public ActionResult<LoginResponse> Login([FromQuery] LoginRequest request)
        {
            var user = userService.GetByLogin(request.Login);
            if (user is null)
            {
                logger.LogWarning(MyLogEvents.GetItemNotFound, $" {request.Login} isn't exist");
                return BadRequest(user);
            }
            if (request.Password!=user.Password)
            {
                logger.LogWarning(MyLogEvents.GetItemNotFound, $" {request.Login}:{request.Password} wrong password");
                return Problem();
            }
            logger.LogInformation(MyLogEvents.GetItem, $"Login {request.Login} ");
            return Ok(new LoginResponse()
             {
                 Id = Convert.ToInt32(user.Id),
                 Cookie = SetCookie(user).ToJson(),
                 Login = user.Login,
                 Surname = user.Surname,
                 Name = user.Name
             });
        }
        
        [HttpGet("/logout")]
        public ActionResult<User> Logout()
        {
            logger.LogInformation(MyLogEvents.DeleteItem, $"Logout");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
        
        [HttpGet("/loginExist")]
        public ActionResult<User> CheckLoginExist([FromQuery] LoginRequest request)
        {
            var user = userService.GetByLogin(request.Login);
            if (user is null)
            {
                logger.LogWarning(MyLogEvents.GetItemNotFound, $"{request.Login} isn't exist");
                return BadRequest();
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public ActionResult<RegistrationResponse> Register([FromQuery] RegisterRequest model)
        {
            if (userService.IsLoginExist(model.Login))
            {
                logger.LogWarning(MyLogEvents.GenerateItems, $"Login {model.Login} is exist");
                return BadRequest();
            }
            logger.LogInformation(MyLogEvents.GenerateItems, $"Register new user");
            var user = new User(0, model.Name, model.Surname, model.Login);
            userService.Add(user);
            return Ok(new RegistrationResponse(){UserId = user.Id, Cookies = SetCookie(user).ToString()});
        } 
        
        private List<Claim> SetCookie(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, user.Login), new (ClaimTypes.UserData, user.Password)
            };
            // создаем объект ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return claims;
        }
    }
}