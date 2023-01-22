using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ValueObjects;
using WebAPI.Server;
using WebAPI.Server.Data;

namespace Deadlindar.Repositories
{
    public class UserRepositoryDatabase: IUserRepository
    {
        public UserRepositoryDatabase()
        { }
        public List<User> GetAll()
        {
            using (UserContext db = new())
            {
                return db.Users.ToList();
            }
        }

        public User? GetById(int id)
        {
            using (UserContext db = new())
            {
                return db.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public User? GetByLogin(string login)
        {
            using (UserContext db = new())
            {
                return db.Users.FirstOrDefault(u => u.Login == login);
            }
        }

        public void Add(User user)
        {
            using (UserContext db = new())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public User? Delete(int id)
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

        public bool Update(User user)
        {
             var u = Delete(user.Id);
             if (u is null)
                 return false;  
             Add(user);
             return true;
        }

        public bool IsLoginExist(string login)
        {
            return GetByLogin(login) is not null;
        }
    }
}