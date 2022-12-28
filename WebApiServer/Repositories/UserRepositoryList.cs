using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ValueObjects;
using WebAPI.Server;

namespace Deadlindar.Repositories
{
    public class UserRepositoryList : IUserRepository
    {
        private static List<UserServer> Users { get; }
        private static int nextId = 7;

        static UserRepositoryList()
        {

            
            var u = new UserServer(20, "Vadim", "Bykov", "Zhuzha");
            // var d = new Day(DateTime.Today);
            // var t = new TimeInterval(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1).AddHours(1));
            // //d.Events.Add(new Event("OOP", timeInterval:t));
            Users = new List<UserServer>()
            {
                new UserServer(10, "German", "Markov", "Nobody"),
                new UserServer(23, "Alina", "Valitova", "kissliinka"),
                u
            };
        }

        public List<UserServer> GetAll() => Users;
        public UserServer? GetById(int id) => Users.FirstOrDefault(u => u.Id == id);
        public UserServer? GetByLogin(string login) => Users.FirstOrDefault(u => u.Login == login);

        public void Add(UserServer userServer)
        {
            userServer.Id = nextId++;
            Users.Add(userServer);
        }

        public UserServer? Delete(int id)
        {
            var user = GetById(id);
            if (user is null)
                return null;

            Users.Remove(user);
            return user;
        }

        public bool Update(UserServer userServer)
        {
            var index = Users.FindIndex(p => p.Id == userServer.Id);
            if (index == -1)
                return false;

            Users[index] = userServer;
            return true;
        }

        public bool IsLoginExist(string login)
        {
            return GetByLogin(login) is not null;
        }
    }
}