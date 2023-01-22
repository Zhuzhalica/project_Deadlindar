using System.Collections.Generic;
using ValueObjects;

namespace ClientModels
{
    public interface IUserDataHandler
    {
        ClientUser ClientUser { get; }
        string URI { get; }
        void Setup(string login);
        string Login { get; }
    }
}