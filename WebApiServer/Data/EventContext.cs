using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Deadlindar.Models;
using Microsoft.EntityFrameworkCore;
using ValueObjects;

namespace WebAPI.Server.Data
{
    public class EventContext: DbContext
    {
        public EventContext() : base(new DbContextOptions<EventContext>())
        { }

        public EventContext (DbContextOptions<EventContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated(); //Расскоментировать если нужна новая чистая бд
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\German\ProjectD\WebApiServer\AppData\Events.db;");
        }
        public DbSet<EventServer> Events { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var g = new GoalType() {Title = "df", Color = new ColorARGB() {A = 0, B = 1, G = 2, R = 1}};
            var t = new TimeInterval() {startTime = DateTime.MinValue, endTime = DateTime.Now};
            
            var ev = new Event(){Description="OOP", GoalType = g, TimeInterval = t, Title="dg"};
            modelBuilder.Entity<EventServer>().OwnsOne(e => e.Event);
            //modelBuilder.Entity<EventServer>().HasData(new EventServer(){Login = "German", Event = ev});
        }
    }
}