using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Context;

public interface IOxygenDbContext
{
    public DbSet<OxygenMeasurement> OxygenMeasurements { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}