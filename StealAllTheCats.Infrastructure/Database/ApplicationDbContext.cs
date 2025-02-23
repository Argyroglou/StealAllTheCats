using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Core.Entities.Persistent;

namespace StealAllTheCats.Infrastructure.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Cat> Cats { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCatEntity(modelBuilder);
            ConfigureTagEntity(modelBuilder);

            // Set delete behavior for all foreign keys to Restrict
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureCatEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasIndex(c => c.CatId)
                      .IsUnique();

                entity.Property(c => c.CatId)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(c => c.ImageUrl)
                      .IsRequired()
                      .HasMaxLength(2048);

                entity.Property(c => c.Created)
                      .IsRequired()
                      .ValueGeneratedOnAdd();
            });
        }

        private static void ConfigureTagEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Name)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(t => t.Created)
                      .IsRequired();

                entity.HasIndex(t => t.Name)
                      .IsUnique();
            });
        }
    }
}
