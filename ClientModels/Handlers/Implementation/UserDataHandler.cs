using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ValueObjects;

namespace ClientModels
{
    public class UserDataHandler : IUserDataHandler
    {
        public string URI => "https://localhost:7135";
        public ClientUser ClientUser { get; }
        public string Login => ClientUser.User.Login;

        public UserDataHandler(ClientUser clientUser)
        {
            ClientUser = clientUser;
        }
        
        public void Setup(string login)
        {

        }

    }
}