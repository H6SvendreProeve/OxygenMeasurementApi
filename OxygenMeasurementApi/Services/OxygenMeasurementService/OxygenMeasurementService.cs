using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Context;
using OxygenMeasurementApi.Data.Entities;
using OxygenMeasurementApi.Data.Extensions;
using OxygenMeasurementApi.OxygenMeasurements.Create;

namespace OxygenMeasurementApi.Services.OxygenMeasurementService;

public class OxygenMeasurementService : IOxygenMeasurementService
{
    private IOxygenDbContext OxygenDbContext { get; }

    public OxygenMeasurementService(IOxygenDbContext oxygenDbContext)
    {
        OxygenDbContext = oxygenDbContext;
    }

    public async Task<bool> CreateOxygenMeasurement(CreateOxygenMeasurement createOxygenMeasurement)
    {
        var oxygenMeasurement = createOxygenMeasurement.AsEntity();
        await OxygenDbContext.OxygenMeasurements.AddAsync(oxygenMeasurement);
        await OxygenDbContext.SaveChangesAsync();
        return oxygenMeasurement.Id > 0;
    }

    public async Task<List<OxygenMeasurement>> GetAllOxygenMeasurements()
    {
        var oxygenMeasurements = await OxygenDbContext.OxygenMeasurements.ToListAsync();
        
        return oxygenMeasurements.ConvertMeasurementTimeToLocalTimezone();
    }

    public async Task<List<OxygenMeasurement>> GetSpecificAmountOfOxygenMeasurements(int amount)
    {
        var dbMeasurements = await OxygenDbContext.OxygenMeasurements.ToListAsync();

        var measurements = (from measurement in dbMeasurements
            orderby measurement.Id descending
            select measurement).Take(amount).ToList();

        return measurements.ConvertMeasurementTimeToLocalTimezone();
    }
}