using GamePlay.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gameplay.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationIdentityUser>
    {
      
       public DbSet<Category> Category { get; set; }
       public DbSet<Device> Devices { get; set; }
       public DbSet<GameDevice> GameDevices { get; set; }
        public DbSet<Game> Games { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category[]
                {
                    new Category { Id = 1, Name = "Sports" },
                new Category { Id = 2, Name = "Action" },
                new Category { Id = 3, Name = "Adventure" },
                new Category { Id = 4, Name = "Racing" },
                new Category { Id = 5, Name = "Fighting" },
                new Category { Id = 6, Name = "Film" }
                }
                );
            modelBuilder.Entity<Device>().HasData(
                new Device[]
                {
                    new Device { Id = 1, Name = "PlayStation", Iocn = "bi bi-playstation" },
                new Device { Id = 2, Name = "xbox", Iocn = "bi bi-xbox" },
                new Device { Id = 3, Name = "Nintendo Switch", Iocn = "bi bi-nintendo-switch" },
                new Device { Id = 4, Name = "PC", Iocn = "bi bi-pc-display" }
                }
                );
            modelBuilder.Entity<GameDevice>().HasKey(x => new {x.DeviceId,x.GameId});
            base.OnModelCreating(modelBuilder);
        }
    }
}
