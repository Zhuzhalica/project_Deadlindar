using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ClientModels
{
    public class JsonSaver<T> : IHandlerSaver<T>
        where T : IHandler
    {
        public async void Save(T handler)
        {
            await using var createStream = File.Create(@"C:\Users\portu\Desktop\pDeadlindar\ClientModels\save.json");
            await JsonSerializer.SerializeAsync(createStream, handler);
            createStream.Close();
        }

        public T? Read()
        {
            using var r = new StreamReader(@"C:\Users\portu\Desktop\pDeadlindar\ClientModels\save.json");
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}