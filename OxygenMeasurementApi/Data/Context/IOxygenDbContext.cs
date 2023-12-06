using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Context;

/// <summary>
/// Interface representing the OxygenDbContext, which provides access to various DbSet properties for database interactions.
/// </summary>
public interface IOxygenDbContext
{
    /// <summary>
    /// 
    /// </summary>
    public DbSet<OxygenMeasurement> OxygenMeasurements { get; set; }
    /// <summary>
    /// DbSet property of ApiKey
    /// </summary>
    public DbSet<ApiKey> ApiKeys { get; set; }
    /// <summary>
    /// DbSet property of OxygenMeasurementSystem
    /// </summary>
    public DbSet<OxygenMeasurementSystem> OxygenMeasurementSystems { get; set; }
    /// <summary>
    /// DbSet property of SystemNotificationAdvisor
    /// </summary>
    public DbSet<SystemNotificationAdvisor> SystemNotificationAdvisors { get; set; }
    /// <summary>
    /// EntityframeworkCore SaveChangesAsync
    /// </summary>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete. if not provided use default</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
}