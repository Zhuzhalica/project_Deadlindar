using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using ValueObjects;

namespace ClientModels
{
    public class UserLogin: Service<User>
    {
        public UserLogin(IRequests<User> requests) : base(requests)
        {
        }

        // protected override List<User>? ResponseInType(HttpResponseMessage content)
        // {
        //     var user = content.Content.ReadFromJsonAsync(typeof(User)).Result as User;
        //     return new List<User> {user};
        // }
    }
}