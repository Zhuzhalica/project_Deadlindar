using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ValueObjects;

namespace WebAPI.Server
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Role { get; set; }
    }
}