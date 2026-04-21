using CarSharing.Modules.Payments.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Modules.Payments.Infrastructure;

public sealed class PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : DbContext(options)
{
    public DbSet<PaymentAttempt> PaymentAttempts => Set<PaymentAttempt>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PaymentAttempt>(entity =>
        {
            entity.ToTable("payment_attempts");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.ReservationId).IsRequired();
            entity.Property(x => x.Amount).HasColumnType("decimal(18,2)");
            entity.Property(x => x.Status).HasMaxLength(30).IsRequired();
            entity.Property(x => x.CreatedAtUtc).IsRequired();
        });
    }
}
