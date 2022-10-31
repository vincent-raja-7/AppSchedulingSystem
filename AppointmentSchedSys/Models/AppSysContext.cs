using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Models
{
    public class AppSysContext : DbContext
    {
        public AppSysContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<AppointmentEntries> AppointmentEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            HMACSHA512 hmac = new HMACSHA512();
            var ph = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin@pass"));
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Username = "admin",
                    Password = "admin@pass",
                    Email = "admin@company.com",
                    DOB = Convert.ToDateTime("2000-09-14"),
                    Phone = "9543457896",
                    Role = "Admin",
                    Key = hmac.Key,
                    PasswordHash = ph
        });
        }
    }
}
