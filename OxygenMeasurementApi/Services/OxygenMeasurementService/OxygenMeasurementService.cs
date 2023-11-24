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
        var oxygenMeasurement = createOxygenMeasurement.OxygenMeasurementAsEntity();
        await OxygenDbContext.OxygenMeasurements.AddAsync(oxygenMeasurement);
        await OxygenDbContext.SaveChangesAsync();
        return oxygenMeasurement.Id > 1;
    }

    public async Task<List<OxygenMeasurement>> GetAllOxygenMeasurements()
    {
        return await OxygenDbContext.OxygenMeasurements.ToListAsync();
    }

    public async Task<List<OxygenMeasurement>> GetSpecificAmountOfOxygenMeasurements(int amount)
    {
        var dbMeasurements = await OxygenDbContext.OxygenMeasurements.ToListAsync();

        return (from measurement in dbMeasurements
            orderby measurement.Id descending
            select measurement).Take(amount).ToList();
    }

    public async Task<OxygenMeasurement?> GetOxygenMeasurementById(int id)
    {
        return await OxygenDbContext.OxygenMeasurements.FindAsync(id);
    }
}