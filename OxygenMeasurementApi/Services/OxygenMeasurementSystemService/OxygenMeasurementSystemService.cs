using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Context;
using OxygenMeasurementApi.Data.Dtos.Mapper;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Dtos.ResponseDtos;

namespace OxygenMeasurementApi.Services.OxygenMeasurementSystemService;

public class OxygenMeasurementSystemService : IOxygenMeasurementSystemService
{
    private IOxygenDbContext DbContext { get; }
    
    public OxygenMeasurementSystemService(IOxygenDbContext dbContext)
    {
        DbContext = dbContext;
    }


    public async Task<OxygenMeasurementSystemResponseDto> CreateOxygenMeasurementSystem(
        CreateOxygenMeasurementSystemDto createDto)
    {
        var oxygenMeasurementSystem = createDto.ToEntity();
        await DbContext.OxygenMeasurementSystems.AddAsync(oxygenMeasurementSystem);
        await DbContext.SaveChangesAsync();

        return oxygenMeasurementSystem.ToResponse();
    }

    public async Task<OxygenMeasurementSystemResponseDto?> GetOxygenMeasurementSystemById(int id)
    {
        var dbOxygenMeasurementSystem =
            await DbContext.OxygenMeasurementSystems.FirstOrDefaultAsync(oms => oms.Id == id);

        if (dbOxygenMeasurementSystem != null && dbOxygenMeasurementSystem.Id == id)
        {
            return dbOxygenMeasurementSystem.ToResponse();
        }

        return null;
    }
}