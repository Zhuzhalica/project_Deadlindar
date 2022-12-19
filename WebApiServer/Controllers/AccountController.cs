using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ValueObjects;
using WebAPI.Server.Services;

namespace WebAPI.Server.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            this.logger = logger;
            this.logger.LogInformation("Account logger called");
            _userService = userService;
        }
        // GET
        //[AllowAnonymous]
        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult<User> Register(RegisterRequest model)
        {
            if (model.IsValid)
            {
                try
                {
                    var user = _userService.Register(model);
                    logger.LogInformation(MyLogEvents.InsertItem, $"Register new user");
                    Authenticate(model.Login);
                    return user;
                }
                catch
                {
                    logger.LogWarning(MyLogEvents.GenerateItems, $"Non register");
                    return NotFound();
                }
            }
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthenticateRequest model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetByLogin(model.Login);
                if (user != null)
                {
                    Authenticate(model.Login); // аутентификация
                    return Ok();
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return null;
        }
        
        private void Authenticate(string login)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}