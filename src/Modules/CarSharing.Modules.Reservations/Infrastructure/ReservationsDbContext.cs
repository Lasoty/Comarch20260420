using CarSharing.Modules.Reservations.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Modules.Reservations.Infrastructure;

public sealed class ReservationsDbContext(DbContextOptions<ReservationsDbContext> options) : DbContext(options)
{
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<ReservationSagaState> ReservationSagaStates => Set<ReservationSagaState>();

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

        modelBuilder.Entity<ReservationSagaState>(entity =>
        {
            entity.ToTable("reservation_saga_states");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.ReservationId).IsRequired();
            entity.Property(x => x.CarId).IsRequired();
            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.Price).HasColumnType("decimal(18,2)");
            entity.Property(x => x.State).HasMaxLength(50).IsRequired();
            entity.Property(x => x.CreatedAtUtc).IsRequired();
            entity.Property(x => x.UpdatedAtUtc).IsRequired(false);

            entity.HasIndex(x => x.ReservationId).IsUnique();
        });
    }
}
