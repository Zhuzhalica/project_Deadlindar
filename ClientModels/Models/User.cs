using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using ValueObjects;

namespace ClientModels
{
    public class User : IUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password;

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

        public User()
        {
        }
    }
}