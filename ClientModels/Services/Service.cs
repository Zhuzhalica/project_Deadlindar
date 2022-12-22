using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using ValueObjects;

namespace ClientModels
{
    public abstract class Service<T>: IService<T>
    {
        private readonly IRequests<T> Requests;
        public Service(IRequests<T> requests)
        {
            Requests = requests;
        }

        public List<T>? TryGet(string login, string uri)
        {
            var content = Requests.Get(login, uri);
            return content.Result.IsSuccessStatusCode 
                ? ResponseInType(content.Result) 
                : null;
        }

        private List<T>? ResponseInType(HttpResponseMessage content)
        {
            return content.Content.ReadFromJsonAsync(typeof(List<T>)).Result as List<T>;
        }

        public bool TryAdd(string login, T obj, string uri)
        {
            var response = Requests.Add(login, obj, uri);
            return response.Result.IsSuccessStatusCode;
        }

        public bool TryDelete(string login, T obj, string uri)
        {
            var response = Requests.Delete(login, obj, uri);
            return response.Result.IsSuccessStatusCode;
        }
    }
}