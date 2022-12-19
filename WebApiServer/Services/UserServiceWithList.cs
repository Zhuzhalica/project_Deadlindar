﻿using System;
using System.Collections.Generic;
using System.Linq;
using ValueObjects;

namespace WebAPI.Server.Services
{
    public class UserServiceWithList: IUserService
    {
        private static List<UserServer> Users { get; }
        private static int nextId = 7;

        static UserServiceWithList()
        {
            var u = new UserServer(20, "Vadim", "Bykov", "Zhuzha");
            var d = new Day(DateTime.Today);
            var t = new TimeInterval(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1).AddHours(1));
            //d.Events.Add(new Event("OOP", timeInterval:t));
            Users = new List<UserServer>() 
            {
                new UserServer(10, "German", "Markov","Nobody"),
                new UserServer(23, "Alina", "Valitova", "kissliinka"),
                u
            };
        }

        public  List<UserServer> GetAll() => Users;
        public  UserServer? GetById(int id) => Users.FirstOrDefault(u => u.Id == id);
        public UserServer? GetByLogin(string login) => Users.FirstOrDefault(u => u.Login == login);

        public void Add(UserServer userServer)
        {
            userServer.Id = nextId++;
            Users.Add(userServer);
        }
        
        public  UserServer? Delete(int id)
        {
            var user = GetById(id);
            if(user is null)
                return null;

            Users.Remove(user);
            return user;
        }
        
        public bool Update(int id, UserServer userServer)
        {
            var index = Users.FindIndex(p => p.Id == userServer.Id);
            if(index == -1)
                return false;
            
            Users[index] = userServer;
            return true;
        }
        
        public UserServer Register(RegisterRequest model)
        {
            if (Users.Any(u => u.Login == model.Login))
                throw new ArgumentException($"Login {model.Login} is taken");
            var user = new UserServer(nextId++, model.Name, model.Surname, model.Login, model.Password);
            Users.Add(user);
            return user;
        }

        public  bool IsLoginExist(string login)
        {
            return GetByLogin(login) is not null;
        }
    }
}