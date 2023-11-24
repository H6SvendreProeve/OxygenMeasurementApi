using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Api.Dtos.OxygenMeasurementDtos;
using OxygenMeasurementApi.Data;
using OxygenMeasurementApi.Data.Extensions;
using OxygenMeasurementApi.Entities;

namespace OxygenMeasurementApi.Services.OxygenMeasurementService;

public class OxygenMeasurementService : IOxygenMeasurementService
{

    private OxygenDbContext OxygenDbContext { get; }
    
    public OxygenMeasurementService(OxygenDbContext oxygenDbContext)
    {
        OxygenDbContext = oxygenDbContext;
    }
    
    public async Task<bool> CreateOxygenMeasurement(CreateOxygenMeasurement createOxygenMeasurement)
    { 
        await OxygenDbContext.OxygenMeasurements.AddAsync(createOxygenMeasurement.OxygenMeasurementAsEntity());
        await OxygenDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<OxygenMeasurement>> GetAllOxygenMeasurements()
    {
        return await OxygenDbContext.OxygenMeasurements.ToListAsync();
    }

    public async Task<List<OxygenMeasurement>> GetSpecificAmountOfOxygenMeasurements(int amount)
    {
        var measurements = await OxygenDbContext.OxygenMeasurements.ToListAsync();

        if (!measurements.Any())
        {
            return new List<OxygenMeasurement>();
        }
        return (List<OxygenMeasurement>)(from measurement in measurements
            orderby measurement.MeasurementTime descending
            select measurement).Take(amount);
    }
}