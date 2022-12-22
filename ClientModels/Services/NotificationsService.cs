using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using ValueObjects;

namespace ClientModels
{
    public class NotificationsService : Service<Notification>
    {
        public NotificationsService(IRequests<Notification> requests) : base(requests)
        {
        }

        // protected override List<Notification>? ResponseInType(HttpResponseMessage content)
        // {
        //     return content.Content.ReadFromJsonAsync(typeof(List<Notification>)).Result as List<Notification>;
        // }
    }
}