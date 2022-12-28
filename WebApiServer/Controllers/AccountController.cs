
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
using ValueObjects;
using WebAPI.Server.Services;

namespace WebAPI.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController :Controller
    {
        private IUserService userService;
        private readonly ILogger<AccountController> logger;
        
        public AccountController(ILogger<AccountController> logger)
        {
            this.logger = logger;
            userService = new UserService();
        }

        [AllowAnonymous]
        [HttpGet("/login")]
        public ActionResult<LoginResponse> Login2([FromQuery] LoginRequest request)
        {
            var user = userService.GetByLogin(request.Login);
            if (user is null || user.Password != "")
            {
                logger.LogWarning(MyLogEvents.GetItemNotFound, $" {request.Login} isn't exist");
                return BadRequest(user);
            }
            logger.LogInformation(MyLogEvents.GetItem, $"Login {request.Login} ");
            return Ok(new LoginResponse()
             {
                 Id = Convert.ToInt32(user.Id),
                 Cookie = SetCookie(user).ToJson(),
                 Login = user.Login,
                 Surname = user.Surname,
                 Role = (int)user.Role,
                 Name = user.Name
             });
        }
        
        [HttpGet("/logout")]
        public ActionResult<UserServer> Logout()
        {
            logger.LogInformation(MyLogEvents.DeleteItem, $"Logout");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("/register")]
        public ActionResult<RegistrationResponse> Register([FromQuery] RegisterRequest model)
        {
            if (userService.IsLoginExist(model.Login))
            {
                logger.LogWarning(MyLogEvents.GenerateItems, $"Login {model.Login} is exist");
                return BadRequest();
            }
            logger.LogInformation(MyLogEvents.GenerateItems, $"Register new user");
            var user = new UserServer(0, model.Name, model.Surname, model.Login);
            userService.Add(user);
            return Ok(new RegistrationResponse(){UserId = user.Id, Cookies = SetCookie(user).ToString()});
        } 
        
        private List<Claim> SetCookie(UserServer userServer)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, userServer.Login), new (ClaimTypes.UserData, userServer.Password)
                , new(ClaimsIdentity.DefaultRoleClaimType, userServer.Role.ToString())
            };
            // создаем объект ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return claims;
        }
    }
}