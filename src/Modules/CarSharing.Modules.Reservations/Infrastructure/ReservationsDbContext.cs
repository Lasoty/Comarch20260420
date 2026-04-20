using CarSharing.Modules.Reservations.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Modules.Reservations.Infrastructure;

public sealed class ReservationsDbContext(DbContextOptions<ReservationsDbContext> options) : DbContext(options)
{
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("reservations");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.CarId).IsRequired();
            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.FromUtc).IsRequired();
            entity.Property(x => x.ToUtc).IsRequired();
            entity.Property(x => x.Status).HasMaxLength(30).IsRequired();
        });
    }
}
