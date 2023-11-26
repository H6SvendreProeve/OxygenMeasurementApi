using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Context;

public class OxygenDbContext : DbContext, IOxygenDbContext
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