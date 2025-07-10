using gacha.Models;
using Microsoft.EntityFrameworkCore;
using pd.Models;

namespace gacha.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Subgenre> Subgenres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=C:\\dev\\pd\\gacha.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Collection>()
                .HasKey(c => new { c.UserId, c.CardId });

            // Genre -> Subgenre
            modelBuilder.Entity<Subgenre>()
                .HasOne(sg => sg.Genre)
                .WithMany(g => g.Subgenres)
                .HasForeignKey(sg => sg.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            // Genre -> Series
            modelBuilder.Entity<Serie>()
                .HasOne(s => s.Genre)
                .WithMany(g => g.Series)
                .HasForeignKey(s => s.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            // Subgenre -> Series
            modelBuilder.Entity<Serie>()
                .HasOne(s => s.SubGenre)
                .WithMany(sg => sg.Series)
                .HasForeignKey(s => s.SubGenreId)
                .OnDelete(DeleteBehavior.Cascade);

            // Serie -> Cards
            modelBuilder.Entity<Card>()
                .HasOne(c => c.Serie)
                .WithMany(s => s.Card)
                .HasForeignKey(c => c.SerieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
