using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ValueObjects;

namespace Deadlindar.Models
{
    public class EventServer
    {
        [Key]
        public string Login { get; set; }
        public Event Event { get; set; }

        // public EventServer(string login, Event Event)
        // {
        //     this.Login = login;
        //     this.Event = Event;
        // }
    }
}