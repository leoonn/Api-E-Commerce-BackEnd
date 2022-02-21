using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Data
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {

        }
        
        public DbSet<Models.User> User {get; set;}
        public DbSet<Models.UserSession> UserSession { get; set; }
        public DbSet<Models.ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Models.Product> Product { get; set; }
        public DbSet<Models.Address> Address { get; set; }
        public DbSet<Models.Order> Order { get; set; }
    }
}
