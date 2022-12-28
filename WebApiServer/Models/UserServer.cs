using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using ValueObjects;

namespace WebAPI.Server
{
    public enum Role
    {
        Anonym,
        Member,
        Admin
    }

    public enum UserStatus
    {
        Submitted,
        Approved,
        Rejected
    }

    public class UserServer : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        [JsonIgnore] public Role Role { get; set; }
        [JsonIgnore] public string Password { get; set; }
        [JsonIgnore] public UserStatus Status { get; set; }

        public UserServer()
        {
        }

        public UserServer(int id, string name, string surname, string login)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            this.Status = UserStatus.Approved;
            this.Password = "";
            this.Role = Role.Anonym;
        }

        public UserServer(int id, string name, string surname, string login, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            this.Status = UserStatus.Approved;
            this.Role = Role.Member;
            this.Password = password;
        }

        public UserServer(int id, string name, string surname, string login, string password, int role)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            this.Status = UserStatus.Approved;
            this.Role = (Role) role;
            this.Password = password;
        }
    }
}