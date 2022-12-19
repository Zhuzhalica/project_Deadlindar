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
        public List<UserServer> GetAll()
        {
            using (UserContext db = new())
            {
                return db.Users.ToList();
            }
        }

        public UserServer? GetById(int id)
        {
            using (UserContext db = new())
            {
                return db.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public UserServer? GetByLogin(string login)
        {
            using (UserContext db = new())
            {
                return db.Users.FirstOrDefault(u => u.Login == login);
            }
        }

        public void Add(UserServer userServer)
        {
            using (UserContext db = new())
            {
                db.Users.Add(userServer);
                db.SaveChanges();
            }
        }

        public UserServer? Delete(int id)
        {
            using (UserContext db = new())
            {
                var user = GetById(id);
                if (user is null)
                    return null;
                db.Users.Remove(user);
                db.SaveChanges();
                return user;
            }
        }

        public bool Update(int id, UserServer userServer)
        {
            using (UserContext db = new())
            {
                var u = Delete(id);
                if (u is null)
                    return false;
                Add(u);
                return true;
            }
        }

        public UserServer Register(RegisterRequest model)
        {
            using (UserContext db = new())
            {
                if (db.Users.Any(u => u.Login == model.Login))
                    throw new ArgumentException($"Login {model.Login} is taken");
                var user = new UserServer(100, model.Name, model.Surname, model.Login, model.Password);
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