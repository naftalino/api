using gacha.Models;
using Microsoft.EntityFrameworkCore;

namespace gacha.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Collection> Collections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=gacha.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Collection>()
                .HasKey(c => new { c.UserId, c.CardId });
        }
    }
}
