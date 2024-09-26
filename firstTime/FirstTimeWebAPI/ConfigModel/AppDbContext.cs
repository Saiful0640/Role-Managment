using FirstTimeWebAPI.Models;
<<<<<<< HEAD
using FirstTimeWebAPI.Models.Settings;
=======
using FirstTimeWebAPI.Models.Config;
>>>>>>> 03be91ae136cb2c30cd37ef4a945a167c8bbe44b
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
        public DbSet<Seetings> Seetings { get; set; }

=======
        public DbSet<Settings> Settings { get; set; }
>>>>>>> 03be91ae136cb2c30cd37ef4a945a167c8bbe44b

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
