using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Mvc;
// using WebAPI.Server.Services;
//
// namespace WebAPI.Server.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class AccountController2: ControllerBase
//     {
//         private readonly IAuthService _authenticationService;
//         
//         public AccountController2(IAuthService authenticationService)
//         {
//             _authenticationService = authenticationService;
//         }
//
//         [HttpPost("login")]
//         public async Task<ActionResult<LoginResponse>> Login([FromQuery] LoginRequest request)
//         {
//             return Ok(await _authenticationService.Login(request));
//         }
//
//         [HttpPost("register")]
//         public async Task<ActionResult<RegistrationResponse>> Register([FromQuery] RegisterRequest request)
//         {
//             return Ok(await _authenticationService.Register(request));
//         }
//     }
// }