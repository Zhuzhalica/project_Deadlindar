using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Deadlindar.Authorization
{
    public class AuthenticationOptions
    {
        public const string ISSUER = "WebAPIServer"; // издатель токена
        public const string AUDIENCE = "User"; // потребитель токена
        const string KEY = "Deadlindar123444422";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}