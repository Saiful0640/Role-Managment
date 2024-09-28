using FirstTimeWebAPI.Models;
<<<<<<< HEAD
using FirstTimeWebAPI.Models.Config;
=======

 
>>>>>>> 3ac25e3758825af8081de0e13ebd8ce711b31bdd
using Microsoft.EntityFrameworkCore;

namespace FirstTimeWebAPI.ConfigModel
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
<<<<<<< HEAD
        public DbSet<Settings> Settings { get; set; }
=======


>>>>>>> 3ac25e3758825af8081de0e13ebd8ce711b31bdd

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role {Id=1, RoleName= "Admin"},
                new Role { Id=2, RoleName ="User"}
                );

            base.OnModelCreating(modelBuilder);

            // Entity configurations
        }


    }
}
