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
        
        modelBuilder.Entity<OxygenMeasurement>()
            .HasOne(m => m.OxygenMeasurementSystem)
            .WithMany(s => s.OxygenMeasurements)
            .HasForeignKey(m => m.OxygenMeasurementSystemId);
        

        modelBuilder.Entity<ApiKey>()
            .HasOne<OxygenMeasurementSystem>(ak => ak.OxygenMeasurementSystem)
            .WithOne(oms => oms.ApiKey)
            .HasForeignKey<OxygenMeasurementSystem>(om => om.ApiKeyId);


        modelBuilder.Entity<OxygenMeasurementSystem>()
            .HasMany(system => system.OxygenMeasurements)
            .WithOne(measurement => measurement.OxygenMeasurementSystem)
            .HasForeignKey(measurement => measurement.OxygenMeasurementSystemId);

        modelBuilder.Entity<OxygenMeasurementSystem>()
            .HasMany(sna => sna.SystemNotificationAdvisors)
            .WithOne(oms => oms.OxygenMeasurementSystem)
            .HasForeignKey(sna => sna.OxygenMeasurementSystemId);
     
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<OxygenMeasurement> OxygenMeasurements { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }
    public DbSet<OxygenMeasurementSystem> OxygenMeasurementSystems { get; set; }
    
    public DbSet<SystemNotificationAdvisor> SystemNotificationAdvisor { get; set; }
}