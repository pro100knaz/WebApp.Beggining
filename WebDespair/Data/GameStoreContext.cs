using Microsoft.EntityFrameworkCore;
using WebDespair.Entities;

namespace WebDespair.Data
{
    public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
    {
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Genre> Genres => Set<Genre>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Rpleplay"},
                new Genre { Id = 2, Name = "Fighting"},
                new Genre { Id = 3, Name = "Sport"},
                new {Id = 4, Name = "Racing"},
                new {Id = 5, Name = "Vr"},
                new {Id = 6, Name = "Fighting"},
                new {Id = 7, Name = "Hide and Seek"}
            );
        }
    }
}
