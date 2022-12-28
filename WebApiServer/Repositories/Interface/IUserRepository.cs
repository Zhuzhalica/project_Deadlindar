using System.Collections.Generic;
using ValueObjects;
using WebAPI.Server;

namespace Deadlindar.Repositories
{
    public interface IUserRepository
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