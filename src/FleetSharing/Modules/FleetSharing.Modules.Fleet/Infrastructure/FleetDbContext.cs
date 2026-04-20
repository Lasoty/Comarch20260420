using CarSharing.Modules.Fleet.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Modules.Fleet.Infrastructure;

public sealed class FleetDbContext(DbContextOptions<FleetDbContext> options) 
: DbContext(options)
{
    public DbSet<Car> Cars => Set<Car>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(builder =>
        {
            builder.Property(car => car.Brand)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(car => car.Model)
                .IsRequired()
                .HasMaxLength(100);
        });
    }
}