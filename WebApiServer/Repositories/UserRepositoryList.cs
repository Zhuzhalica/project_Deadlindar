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
        private static List<User> Users { get; }
        private static int nextId = 7;

        static UserRepositoryList()
        {

            
            var u = new User(20, "Vadim", "Bykov", "Zhuzha");
            // var d = new Day(DateTime.Today);
            // var t = new TimeInterval(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1).AddHours(1));
            // //d.Events.Add(new Event("OOP", timeInterval:t));
            Users = new List<User>()
            {
                new User(10, "German", "Markov", "Nobody"),
                new User(23, "Alina", "Valitova", "kissliinka"),
                u
            };
        }

        public List<User> GetAll() => Users;
        public User? GetById(int id) => Users.FirstOrDefault(u => u.Id == id);
        public User? GetByLogin(string login) => Users.FirstOrDefault(u => u.Login == login);

        public void Add(User user)
        {
            user.Id = nextId++;
            Users.Add(user);
        }

        public User? Delete(int id)
        {
            var user = GetById(id);
            if (user is null)
                return null;

            Users.Remove(user);
            return user;
        }

        public bool Update(User user)
        {
            var index = Users.FindIndex(p => p.Id == user.Id);
            if (index == -1)
                return false;

            Users[index] = user;
            return true;
        }

        public bool IsLoginExist(string login)
        {
            return GetByLogin(login) is not null;
        }
    }
}