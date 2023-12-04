using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Context;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Dtos.ResponseDtos;
using OxygenMeasurementApi.Data.Extensions;
using OxygenMeasurementApi.Data.Mappers;
using OxygenMeasurementApi.Exceptions;

namespace OxygenMeasurementApi.Services.OxygenMeasurementService;

public class OxygenMeasurementService : IOxygenMeasurementService
{
    private readonly IOxygenDbContext oxygenDbContext;

    public OxygenMeasurementService(IOxygenDbContext dbContext)
    {
        oxygenDbContext = dbContext;
    }

    public async Task<OxygenMeasurementResponseDto?> AddOxygenMeasurementAsync(
        AddOxygenMeasurementRequestDto oxygenMeasurementDto)
    {
        try
        {
            var dbOxygenMeasurementSystem = await oxygenDbContext.OxygenMeasurementSystems.FirstOrDefaultAsync(oms =>
                oms.Id == oxygenMeasurementDto.OxygenMeasurementSystemId);

            if (dbOxygenMeasurementSystem == null)
            {
                throw new NotFoundException(
                    $"systemId with value {oxygenMeasurementDto.OxygenMeasurementSystemId} was not found");
            }

            var oxygenMeasurement = oxygenMeasurementDto.ToDbEntity();
            await oxygenDbContext.OxygenMeasurements.AddAsync(oxygenMeasurement);
            await oxygenDbContext.SaveChangesAsync();

            if (oxygenMeasurement.Id > 0)
            {
                oxygenMeasurement.OxygenMeasurementSystem =
                    oxygenDbContext.OxygenMeasurementSystems.First(oms =>
                        oms.Id == oxygenMeasurement.OxygenMeasurementSystemId);
            }

            return oxygenMeasurement.ToResponse();
        }

        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            await Logger.Logger.LogAsync($"error during insert of request {oxygenMeasurementDto} \n exception {e}");
            throw new CustomDbException($"something went wrong when inserting your request {oxygenMeasurementDto}");
        }
    }

    public async Task<OxygenMeasurementResponseDto?> GetOxygenMeasurementByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new CustomArgumentOutOfRangeException(nameof(id), id, "must be greater than 0");
        }

        var dbOxygenMeasurement = await oxygenDbContext.OxygenMeasurements
                                      .FirstOrDefaultAsync(om => om.Id == id)
                                  ?? throw new NotFoundException($"Oxygen measurement with id {id} is not found");

        return dbOxygenMeasurement.ToResponse();
    }


    public async Task<List<OxygenMeasurementResponseDto?>> GetAllSystemOxygenMeasurementsAsync(int systemId)
    {
        var dbOxygenMeasurementSystem = await
            oxygenDbContext.OxygenMeasurementSystems.FirstOrDefaultAsync(oms => oms.Id == systemId);

        if (dbOxygenMeasurementSystem == null)
        {
            throw new NotFoundException($"OxygenMeasurementSystem with id {systemId} is not found");
        }
        
        var oxygenMeasurements = await oxygenDbContext.OxygenMeasurements
            .Where(om => om.OxygenMeasurementSystemId == systemId)
            .OrderByDescending(om => om.Id)
            .ToListAsync();

        var oxygenMeasurementResponse = oxygenMeasurements.ToResponseList();

        return oxygenMeasurementResponse;
    }


    public async Task<List<OxygenMeasurementResponseDto?>> GetAllOxygenMeasurementsAsync()
    {
        var oxygenMeasurements = await oxygenDbContext.OxygenMeasurements.ToListAsync();

        oxygenMeasurements = oxygenMeasurements.ConvertMeasurementTimeToLocalTimezone();

        return oxygenMeasurements.ToResponseList();
    }


    public async Task<List<OxygenMeasurementResponseDto?>> GetSpecificAmountOfOxygenMeasurementsAsync(int systemId,
        int amount)
    {
        var dbSystem = await oxygenDbContext.OxygenMeasurementSystems.FirstOrDefaultAsync(oms => oms.Id == systemId);

        if (dbSystem == null)
        {
            throw new NotFoundException($"systemId with value {systemId} is not found");
        }
        
        var dbMeasurements = await GetAllSystemOxygenMeasurementsAsync(systemId);

        var measurements = dbMeasurements.Where(om => om.OxygenMeasurementSystemId == systemId)
            .OrderByDescending(om => om.Id)
            .Take(amount).ToList();


        return measurements;
    }
}