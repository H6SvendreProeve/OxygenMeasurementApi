using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Context;

public class OxygenDbContext : DbContext, IOxygenDbContext
{
    public OxygenDbContext(DbContextOptions<OxygenDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OxygenMeasurement>()
            .Property(p => p.OxygenValue)
            .HasPrecision(4, 2);

        modelBuilder.Entity<ApiKey>()
            .HasOne<OxygenMeasurementSystem>(oms => oms.OxygenMeasurementSystem)
            .WithOne(ak => ak.ApiKey)
            .HasForeignKey<OxygenMeasurementSystem>(om => om.ApiKeyId);
     
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<OxygenMeasurement> OxygenMeasurements { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }
    public DbSet<OxygenMeasurementSystem> OxygenMeasurementSystems { get; set; }
}