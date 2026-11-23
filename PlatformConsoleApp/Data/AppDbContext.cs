using Microsoft.EntityFrameworkCore;
using PlatformConsoleApp.Models;

namespace PlatformConsoleApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Platform> Platforms { get; set; }

    public DbSet<Well> Wells { get; set; }

    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platform>(entity =>
        {
            entity.ToTable("platforms");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Latitude)
                .HasPrecision(18, 6);

            entity.Property(p => p.Longitude)
                .HasPrecision(18, 6);

            entity.Property(p => p.CreatedAt)
                .HasColumnType("datetime(3)");

            entity.Property(p => p.UpdatedAt)
                .HasColumnType("datetime(3)");
        });

        modelBuilder.Entity<Well>(entity =>
        {
            entity.ToTable("wells");

            entity.HasKey(w => w.Id);

            entity.Property(w => w.Latitude)
                .HasPrecision(18, 6);

            entity.Property(w => w.Longitude)
                .HasPrecision(18, 6);

            entity.Property(w => w.CreatedAt)
                .HasColumnType("datetime(3)");

            entity.Property(w => w.UpdatedAt)
                .HasColumnType("datetime(3)");

            entity.HasOne(w => w.Platform)
                .WithMany(p => p.Wells)
                .HasForeignKey(w => w.PlatformId);
        });
    }
}