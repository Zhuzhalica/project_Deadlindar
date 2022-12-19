using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using ValueObjects;
using System.Drawing;


namespace WebAPI.Server.Data
{
    public class UserContext : DbContext
    {
        public UserContext (DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated(); //Расскоментировать если нужна новая чистая бд
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dataSource = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "user.db");
            optionsBuilder.UseSqlite(@"Data Source=.\Deadlindar\WebApiServer\AppData\user.db;");
        }
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var u = new ServerUser(20, "Vadim", "Bykov", "Zhuzha", "1234");
            modelBuilder.Entity<ServerUser>().HasData(
                 new ServerUser(10, "German", "Markov", "Nobody", "asfaf"),
                 new ServerUser(23, "Alina", "Valitova", "kissliinka", "afs"),
                 u);
        }
    }
}