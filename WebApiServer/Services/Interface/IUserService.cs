using System.Collections.Generic;
using ValueObjects;

namespace WebAPI.Server.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User? GetById(int id);
        User? GetByLogin(string login);
        void Add(User user);
        User? Delete(int id);
        bool Update(User user);
        bool IsLoginExist(string login);
    }
}