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

        public bool Update(UserServer userServer)
        {
             var u = Delete(userServer.Id);
             if (u is null)
                 return false;  
             Add(userServer);
             return true;
        }

        public bool IsLoginExist(string login)
        {
            return GetByLogin(login) is not null;
        }
    }
}