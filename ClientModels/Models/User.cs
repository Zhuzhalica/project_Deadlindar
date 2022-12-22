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

        public User(string name, string surname, string login)
        {
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
        }
    }
}