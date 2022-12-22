using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using ValueObjects;

namespace ClientModels
{
    public class EventService: Service<Event>
    {
        public EventService(IRequests<Event> requests) : base(requests)
        {
        }

        // protected override List<Event>? ResponseInType(HttpResponseMessage content)
        // {
        //     
        // }
    }
}