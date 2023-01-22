using System.Text.Json.Serialization;

namespace ValueObjects
{

    public enum UserStatus
    {
        Submitted,
        Approved,
        Rejected
    }

    public class User
    {
        [JsonIgnore] public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        [JsonIgnore] public string Password { get; set; }
        [JsonIgnore] public UserStatus Status { get; set; }

        public User()
        {
        }

        public User(string name, string surname, string login, string password)
        {
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            Password = password;
        }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }


        public User(int id, string name, string surname, string login)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            this.Status = UserStatus.Approved;
            this.Password = "";
        }

        public User(int id, string name, string surname, string login, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            this.Status = UserStatus.Approved;
            this.Password = password;
        }

        public User(int id, string name, string surname, string login, string password, int role)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            this.Status = UserStatus.Approved;
            this.Password = password;
        }
    }
}