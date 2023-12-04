using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Context;

public interface IOxygenDbContext
{
    public DbSet<OxygenMeasurement> OxygenMeasurements { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }

    public DbSet<OxygenMeasurementSystem> OxygenMeasurementSystems { get; set; }
    public DbSet<SystemNotificationAdvisor> SystemNotificationAdvisor { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
}