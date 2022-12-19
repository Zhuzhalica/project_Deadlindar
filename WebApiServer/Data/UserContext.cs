using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using ValueObjects;
using System.Drawing;


namespace WebAPI.Server.Data
{
    public class UserContext : DbContext
    {
        public UserContext():
            base(new DbContextOptions<UserContext>()){}
        public UserContext (DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated(); //Расскоментировать если нужна новая чистая бд
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dataSource = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "user.db");
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\portu\Desktop\pDeadlindar\WebApiServer\AppData\users.db;");
        }
        public DbSet<UserServer> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var u = new UserServer(20, "Vadim", "Bykov", "Zhuzha", "12345678",1);
            modelBuilder.Entity<UserServer>().HasData(
                 new UserServer(10, "German", "Markov", "Nobody", "asfaf", 2),
                 new UserServer(23, "Alina", "Valitova", "kissliinka", "afs",1),
                 u);
        }
    }
}