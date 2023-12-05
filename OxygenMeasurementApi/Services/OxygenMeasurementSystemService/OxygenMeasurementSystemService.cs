using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Context;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Dtos.ResponseDtos;
using OxygenMeasurementApi.Data.Mappers;
using OxygenMeasurementApi.Exceptions;

namespace OxygenMeasurementApi.Services.OxygenMeasurementSystemService;

public class OxygenMeasurementSystemService : IOxygenMeasurementSystemService
{
    private readonly IOxygenDbContext oxygenDbContext;

    public OxygenMeasurementSystemService(IOxygenDbContext dbContext)
    {
        oxygenDbContext = dbContext;
    }

    public async Task<OxygenMeasurementSystemResponseDto?> GetOxygenMeasurementSystemByIdAsync(int id)
    {
        var dbOxygenMeasurementSystem =
            await oxygenDbContext.OxygenMeasurementSystems.FirstOrDefaultAsync(oms => oms.OxygenMeasurementSystemId == id);

        if (dbOxygenMeasurementSystem == null)
        {
            throw new NotFoundException($"OxygenMeasurementSystem with id {id} is not found");
        }
        
        return dbOxygenMeasurementSystem.ToResponse();
    }

    public async Task<List<OxygenMeasurementSystemsResponseDto?>> GetAllOxygenMeasurementSystemsAsync()
    {
        var dbOxygenMeasurementSystems = await oxygenDbContext.OxygenMeasurementSystems.ToListAsync();

        return dbOxygenMeasurementSystems.ToResponseList();
    }

    public async Task<OxygenMeasurementSystemResponseDto?> AddOxygenMeasurementSystemAsync(
        AddOxygenMeasurementSystemRequestDto addRequestDto)
    {
        var oxygenMeasurementSystem = addRequestDto.ToDbEntity();
        await oxygenDbContext.OxygenMeasurementSystems.AddAsync(oxygenMeasurementSystem);
        await oxygenDbContext.SaveChangesAsync();

        return oxygenMeasurementSystem.ToResponse();
    }
}