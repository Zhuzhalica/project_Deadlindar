using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ValueObjects;
using WebAPI.Server.Data;
using System.Drawing;


namespace WebAPI.Server.Services
{
    public class UserServiceDatabase: IUserService
    {
        private readonly UserContext userContext;
        public UserServiceDatabase()
        {
           userContext = new UserContext(new DbContextOptions<UserContext>());
        }
        public List<User> GetAll()
        {
            using (UserContext db = userContext)
            {
                return db.Users.ToList();
            }
        }

        public User? GetById(int id)
        {
            using (UserContext db = userContext)
            {
                return db.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public User? GetByLogin(string login)
        {
            using (UserContext db = userContext)
            {
                return db.Users.FirstOrDefault(u => u.Login == login);
            }
        }

        public void Add(User user)
        {
            using (UserContext db = userContext)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public User? Delete(int id)
        {
            using (UserContext db = userContext)
            {
                var user = GetById(id);
                if (user is null)
                    return null;
                db.Users.Remove(user);
                db.SaveChanges();
                return user;
            }
        }

        public bool Update(int id, User user)
        {
            using (UserContext db = userContext)
            {
                var u = Delete(id);
                if (u is null)
                    return false;
                Add(u);
                return true;
            }
        }

        public User Register(RegisterRequest model)
        {
            using (UserContext db = userContext)
            {
                if (db.Users.Any(u => u.Login == model.Login))
                    throw new ArgumentException($"Login {model.Login} is taken");
                var user = new ServerUser(100, model.Name, model.Surname, model.Login, model.Password);
                db.Users.Add(user);
                db.SaveChanges();
                return user;
            }
        }

        public bool IsLoginExist(string login)
        {
            return GetByLogin(login) is not null;
        }
    }
}