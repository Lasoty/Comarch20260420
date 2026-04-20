using CarSharing.Modules.Fleet.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Modules.Fleet.Infrastructure;

public sealed class FleetDbContext(DbContextOptions<FleetDbContext> options) : DbContext(options)
{
    public DbSet<Car> Cars => Set<Car>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToTable("fleet_cars");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Brand).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Model).HasMaxLength(100).IsRequired();
            entity.Property(x => x.RegistrationNumber).HasMaxLength(30).IsRequired();
            entity.HasIndex(x => x.RegistrationNumber).IsUnique();
            entity.Property(x => x.IsAvailable).IsRequired();
        });
    }
}
