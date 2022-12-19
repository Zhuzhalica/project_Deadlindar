using System.Text.Json.Serialization;
using ValueObjects;

namespace WebAPI.Server
{
    public class ServerUser: User
    {
        [JsonIgnore]
        public string Password { get; set; }
        public ServerUser(int id, string name, string surname, string login, string password) : base(id, name, surname, login)
        {
            Password = password;
        }
    }
}