using System.Collections.Generic;
using ValueObjects;

namespace WebAPI.Server.Services
{
    public interface IUserService
    {
        List<UserServer> GetAll();
        UserServer? GetById(int id);
        UserServer? GetByLogin(string login);
        void Add(UserServer userServer);
        UserServer? Delete(int id);
        bool Update(UserServer userServer);
        bool IsLoginExist(string login);
    }
}