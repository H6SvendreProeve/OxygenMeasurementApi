using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Context;

/// <summary>
/// DbContext for the OxygenMeasurementApi, Implementing the IOxygenDbContext interface.
/// </summary>
public class OxygenDbContext : DbContext, IOxygenDbContext
{
    /// <summary>
    /// Initializes a new instance of the OxygenDbContext class with the specified options.
    /// </summary>
    /// <param name="options">The options for configuring the DbContext.</param>
    public OxygenDbContext(DbContextOptions<OxygenDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the DbContext options, enabling lazy loading proxies.
    /// </summary>
    /// <param name="optionsBuilder">The options builder to configure.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Configures the model relationships and properties for the DbContext.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for the DbContext.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OxygenMeasurement>()
            .Property(p => p.OxygenValue)
            .HasPrecision(4, 2);

        modelBuilder.Entity<OxygenMeasurement>()
            .HasOne(m => m.OxygenMeasurementSystem)
            .WithMany(s => s.OxygenMeasurements)
            .HasForeignKey(m => m.OxygenMeasurementSystemId);

        modelBuilder.Entity<OxygenMeasurementSystem>()
            .HasMany(system => system.OxygenMeasurements)
            .WithOne(measurement => measurement.OxygenMeasurementSystem)
            .HasForeignKey(measurement => measurement.OxygenMeasurementSystemId);

        modelBuilder.Entity<ApiKey>()
            .HasOne<OxygenMeasurementSystem>(ak => ak.OxygenMeasurementSystem)
            .WithOne(oms => oms.ApiKey)
            .HasForeignKey<OxygenMeasurementSystem>(om => om.ApiKeyId);

        modelBuilder.Entity<OxygenMeasurementSystem>()
            .HasMany(sna => sna.SystemNotificationAdvisors)
            .WithOne(oms => oms.OxygenMeasurementSystem)
            .HasForeignKey(sna => sna.OxygenMeasurementSystemId);


        base.OnModelCreating(modelBuilder);
    }

    public DbSet<OxygenMeasurement> OxygenMeasurements { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }
    public DbSet<OxygenMeasurementSystem> OxygenMeasurementSystems { get; set; }

    public DbSet<SystemNotificationAdvisor> SystemNotificationAdvisors { get; set; }
}