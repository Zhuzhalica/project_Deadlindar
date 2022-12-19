using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Deadlindar.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signManager;

        
        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            userManager = userManager;
            signManager = signInManager;
        }
        
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Login);

            if (user == null)
            {
                throw new Exception($"User with {request.Login} not found.");
            }

            var result = await signManager.PasswordSignInAsync(user.Surname, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception($"Credentials for '{request.Login} aren't valid'.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            
            await signManager.SignInAsync(user, false);
            LoginResponse response = new LoginResponse()
            {
                Id = Convert.ToInt32(user.Id),
                Cookie = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Login = user.Email,
                Name = user.UserName
            };

            return response;
        }



        public async Task<RegistrationResponse> Register(RegisterRequest request)
        {
            var existingUser = await userManager.FindByNameAsync(request.Name);

            if (existingUser != null)
            {
                throw new Exception($"Username '{request.Name}' already exists.");
            }

            var user = new ApplicationUser
            {
                Email = request.Login,
                Name = request.Name,
                Surname = request.Surname,
                UserName = request.Name,
                EmailConfirmed = true
            };

            var existingEmail = await userManager.FindByEmailAsync(request.Login);

            if (existingEmail == null)
            {
                var result = await userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Member");
                    return new RegistrationResponse() { UserId = int.Parse(user.Id) };
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"Email {request.Login } already exists.");
            }
        }
        
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: JWTSettings.Issuer,
                audience: JWTSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(JWTSettings.LIFETIME),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
    public class JWTSettings
    {
        public const string Issuer = "MyAuthServer"; // издатель токена
        public const string Audience = "MyAuthClient"; // потребитель токена
        public const string Key = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}