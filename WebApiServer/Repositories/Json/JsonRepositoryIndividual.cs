using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Azure.Core;
using NUnit.Framework.Internal.Execution;

namespace Deadlindar.Repositories.Json
{
    public class JsonRepositoryIndividual: IJsonRepository
    {
        public T OpenFile<T>(string login) where T: new()
        {
            var obj = new T();
            
            var type = typeof(T).Name;
            if (typeof(T).GenericTypeArguments.Length > 0)
                type = typeof(T).GenericTypeArguments.First().Name;

            if (File.Exists($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\{type}{login}.json"))
            {
                using FileStream stream =
                    File.OpenRead($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\{type}{login}.json");
                obj = JsonSerializer.DeserializeAsync<T>(stream).Result;
            }

            obj ??= new T();
            return obj;
        }

        public void SaveFile<T>(string login, T obj)
        {
            var type = typeof(T).Name;
            if (typeof(T).GenericTypeArguments.Length > 0)
                type = typeof(T).GenericTypeArguments.First().Name;
            
            using FileStream createStream = File.Create($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\{type}{login}.json");
            JsonSerializer.SerializeAsync(createStream, obj);
        }
    }
}