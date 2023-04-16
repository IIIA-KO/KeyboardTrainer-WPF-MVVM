using KeyboardTrainerModel;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace KeyboardTrainerDAL
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Text> Texts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["LocalSQLiteDB"].ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();
        }
    }
}