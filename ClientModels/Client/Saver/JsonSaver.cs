using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ClientModels
{
    public class JsonSaver : IClientSaver
    {
        public async void Save<T>(string login, T client)
        {
            await using var createStream = File.Create($"C:\\Users\\portu\\Desktop\\pDeadlindar\\ClientModels\\Saves\\Save{typeof(T).Name}{login}.json");
            await JsonSerializer.SerializeAsync(createStream, client);
        }

        public T? Read<T>(string login)
        {
            T? client = default;
            if (File.Exists($"C:\\Users\\portu\\Desktop\\pDeadlindar\\ClientModels\\Saves\\Save{typeof(T).Name}{login}.json"))
            {
                using FileStream stream =
                    File.OpenRead($"C:\\Users\\portu\\Desktop\\pDeadlindar\\ClientModels\\Saves\\Save{typeof(T).Name}{login}.json");
                client = JsonSerializer.DeserializeAsync<T>(stream).Result;
            }

            client ??= default;
            return client;
        }
    }
}