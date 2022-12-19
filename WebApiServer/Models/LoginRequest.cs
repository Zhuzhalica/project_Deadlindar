using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.Server
{
    public class LoginRequest
    {
        // public LoginRequest(string login, string password)
        // {
        //     Login = login;
        //     Password = password;
        // }

        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}