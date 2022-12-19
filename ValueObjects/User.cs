using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using ValueObjects;

namespace ValueObjects
{
    public class User:IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public HashSet<Event> Events { get; set; }

        public User(int id, string name, string surname, string login)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            Events = new HashSet<Event>();
        }
    }
}