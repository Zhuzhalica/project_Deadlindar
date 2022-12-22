using System.Collections.Generic;
using System.Net.Http;

namespace ClientModels
{
    public interface IService<T>
    {
        List<T>? TryGet(string login, string uri);
        bool TryAdd(string login, T obj, string uri);
        bool TryDelete(string login, T obj, string uri);
    }
}