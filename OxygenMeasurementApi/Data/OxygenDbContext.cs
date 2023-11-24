using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Entities;

namespace OxygenMeasurementApi.Data;

public class OxygenDbContext : DbContext
{
    public OxygenDbContext(DbContextOptions<OxygenDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OxygenMeasurement>()
                       .Property(p => p.OxygenValue)
                      .HasPrecision(4, 2);
    }

    public DbSet<OxygenMeasurement> OxygenMeasurements { get; set; }
}