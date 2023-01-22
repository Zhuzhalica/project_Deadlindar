using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Azure.Core;
using NUnit.Framework.Internal.Execution;

namespace Deadlindar.Repositories.Json
{
    public class JsonRepository: IJsonRepository
    {
        public T OpenFile<T>(string login, string fileName) where T: new()
        {
            var obj = new T();
            

            if (File.Exists($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\{fileName}.json"))
            {
                using FileStream stream =
                    File.OpenRead($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\{fileName}.json");
                obj = JsonSerializer.DeserializeAsync<T>(stream).Result;
            }

            obj ??= new T();
            return obj;
        }

        public void SaveFile<T>(string login, T obj, string fileName)
        {
            using FileStream createStream = File.Create($"C:\\Users\\portu\\Desktop\\pDeadlindar\\WebApiServer\\AppData\\Json\\{fileName}.json");
            JsonSerializer.SerializeAsync(createStream, obj);
        }
    }
}