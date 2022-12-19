using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ValueObjects;

namespace WebAPI.Server.Services
{
    public class UserServiceWithList : IUserService
    {
        private static List<User> Users { get; }
        private static int nextId = 7;

        static UserServiceWithList()
        {
            var u = new User(20, "Vadim", "Bykov", "Zhuzha");
            var t = new TimeInterval(DateTime.Today.AddHours(10), DateTime.Today.AddHours(12));
            u.Events.Add(new Event("OOP",
                new GoalType("type", new ColorARGB(Color.Aqua.A, Color.Aqua.R, Color.Aqua.G, Color.Aqua.B)), t));
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

        public bool Update(int id, User user)
        {
            var index = Users.FindIndex(p => p.Id == user.Id);
            if (index == -1)
                return false;

            Users[index] = user;
            return true;
        }

        public User Register(RegisterRequest model)
        {
            if (Users.Any(u => u.Login == model.Login))
                throw new ArgumentException($"Login {model.Login} is taken");
            var user = new ServerUser(nextId++, model.Name, model.Surname, model.Login, model.Password);
            Users.Add(user);
            return user;
        }

        public bool IsLoginExist(string login)
        {
            return GetByLogin(login) is not null;
        }
    }
}