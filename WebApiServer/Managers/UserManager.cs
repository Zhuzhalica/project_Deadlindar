using ValueObjects;
using WebAPI.Server;

namespace Deadlindar.Managers
{
    public class UserManager
    {
        public UserServer Create(RegisterRequest model)
        {
            var user = new UserServer(1, model.Name, model.Surname, model.Login, model.Password,2);
            return user;
        }
        //Класс будет присваивать роли
    }
}