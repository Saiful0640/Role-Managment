using FirstTimeWebAPI.Models;
using FirstTimeWebAPI.Models.Settings;
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
        public DbSet<Seetings> Seetings { get; set; }


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
