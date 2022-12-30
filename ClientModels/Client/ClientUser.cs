using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using ValueObjects;

namespace ClientModels
{
    public class ClientUser
    {
        private readonly IUserRequest Requests;
        public User User { get; private set; }

        public ClientUser(IUserRequest requests)
        {
            Requests = requests;
        }

        public User? TryGet(User user, string uri)
        {
            try
            {
                var content = Requests.Get(user, uri).Result;
                if (content.IsSuccessStatusCode)
                {
                    User = ResponseInUser(content);
                }

                return User;
            }
            catch (AggregateException e)
            {
                return null;
            }
        }

        private User? ResponseInUser(HttpResponseMessage content)
        {
            return content.Content.ReadFromJsonAsync(typeof(User)).Result as User;
        }

        public bool TryAdd(string login, User user, string uri)
        {
            try
            {
                var response = Requests.Add(login, user, uri);
                return response.Result.IsSuccessStatusCode;
            }
            catch (AggregateException e)
            {
                return false;
            }
        }

        public bool TryDelete(string login, User user, string uri)
        {
            try
            {
                var response = Requests.Delete(login, user, uri);
                return response.Result.IsSuccessStatusCode;
            }
            catch (AggregateException e)
            {
                return false;
            }
        }

        public bool CheckLoginExist(User user, string uri)
        {
            try
            {
                var response = Requests.CheckLoginExist(user, uri);
                return response.Result.IsSuccessStatusCode;
            }
            catch (AggregateException e)
            {
                return false;
            }
        }
    }
}