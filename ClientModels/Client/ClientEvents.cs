using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using ValueObjects;

namespace ClientModels
{
    public class ClientEvents : IPersistingClient
    {
        private readonly IEventRequest requests;

        public ClientEvents(IEventRequest requests)
        {
            this.requests = requests;
        }

        public List<Event>? TryGet(string login, string uri)
        {
            try
            {
                var content = requests.Get(login, uri).Result;
                return ResponseInEvent(content);
            }
            catch (AggregateException e)
            {
                return null;
            }
        }

        private List<Event>? ResponseInEvent(HttpResponseMessage content)
        {
            return content.Content.ReadFromJsonAsync(typeof(List<Event>)).Result as List<Event>;
        }

        public bool TryAdd(string login, Event _event, string uri)
        {
            try
            {
                var response = requests.Add(login, _event, uri);
                return response.Result.IsSuccessStatusCode;
            }
            catch (AggregateException e)
            {
                return false;
            }
        }

        public bool TryDelete(string login, Event _event, string uri)
        {
            try
            {
                var response = requests.Delete(login, _event, uri);
                return response.Result.IsSuccessStatusCode;
            }
            catch (AggregateException e)
            {
                return false;
            }
        }
    }
}