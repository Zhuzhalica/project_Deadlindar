using ValueObjects;
using WebAPI.Server;

namespace Deadlindar.Managers
{
    public class UserManager
    {
        public User Create(RegisterRequest model)
        {
            var user = new User(1, model.Name, model.Surname, model.Login, model.Password,2);
            return user;
        }
        //Класс будет присваивать роли
    }
}