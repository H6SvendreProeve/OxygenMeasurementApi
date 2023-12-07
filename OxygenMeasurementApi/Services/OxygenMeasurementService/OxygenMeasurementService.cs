using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Context;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Dtos.ResponseDtos;
using OxygenMeasurementApi.Data.Mappers;
using OxygenMeasurementApi.Exceptions;

namespace OxygenMeasurementApi.Services.OxygenMeasurementService;

/// <summary>
/// Service class for managing Oxygen Measurements, implementing the <see cref="IOxygenMeasurementService"/> interface.
/// </summary>
public class OxygenMeasurementService : IOxygenMeasurementService
{
    private readonly IOxygenDbContext oxygenDbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="OxygenMeasurementService"/> class.
    /// with dependency injection of IOxygenDbContext
    /// </summary>
    /// <param name="dbContext">The OxygenDbContext instance.</param>
    public OxygenMeasurementService(IOxygenDbContext dbContext)
    {
        oxygenDbContext = dbContext;
    }

    /// <summary>
    /// Adds a new Oxygen Measurement to the system.
    /// </summary>
    /// <param name="oxygenMeasurementDto">The data for the new Oxygen Measurement.</param>
    /// <returns>
    /// A <see cref="Task{OxygenMeasurementResponseDto}"/> representing the asynchronous operation, containing the added
    /// <see cref="OxygenMeasurementResponseDto"/> if successful.
    /// </returns>
    /// <exception cref="NotFoundException">
    /// Thrown if the specified Oxygen Measurement System is not found.
    /// </exception>
    /// <exception cref="CustomDbException">
    /// Thrown if an unexpected error occurs during the database operation.
    /// </exception>
    public async Task<OxygenMeasurementResponseDto?> AddOxygenMeasurementAsync(
        AddOxygenMeasurementRequestDto oxygenMeasurementDto)
    {
        try
        {
            var dbOxygenMeasurementSystem = await oxygenDbContext.OxygenMeasurementSystems.FirstOrDefaultAsync(oms =>
                oms.OxygenMeasurementSystemId == oxygenMeasurementDto.OxygenMeasurementSystemId);

            if (dbOxygenMeasurementSystem == null)
            {
                throw new NotFoundException(
                    $"systemId with value {oxygenMeasurementDto.OxygenMeasurementSystemId} was not found");
            }

            var oxygenMeasurement = oxygenMeasurementDto.ToDbEntity();
            await oxygenDbContext.OxygenMeasurements.AddAsync(oxygenMeasurement);
            await oxygenDbContext.SaveChangesAsync();

            if (oxygenMeasurement.OxygenMeasurementId > 0)
            {
                oxygenMeasurement.OxygenMeasurementSystem =
                    oxygenDbContext.OxygenMeasurementSystems.First(oms =>
                        oms.OxygenMeasurementSystemId == oxygenMeasurement.OxygenMeasurementSystemId);
            }

            return oxygenMeasurement.ToResponse();
        }

        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception)
        {
            throw new CustomDbException($"something went wrong when inserting your request {oxygenMeasurementDto}");
        }
    }

    /// <summary>
    /// Retrieves a specific Oxygen Measurement by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the Oxygen Measurement to retrieve.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the retrieved
    /// <see cref="OxygenMeasurementResponseDto"/> if found.
    /// </returns>
    /// <exception cref="CustomArgumentOutOfRangeException">
    /// Thrown if the specified ID is less than or equal to 0.
    /// </exception>
    /// <exception cref="NotFoundException">
    /// Thrown if the specified Oxygen Measurement is not found.
    /// </exception>
    public async Task<OxygenMeasurementResponseDto?> GetOxygenMeasurementByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new CustomArgumentOutOfRangeException(nameof(id), id, "must be greater than 0");
        }

        var dbOxygenMeasurement = await oxygenDbContext.OxygenMeasurements
                                      .FirstOrDefaultAsync(om => om.OxygenMeasurementId == id)
                                  ?? throw new NotFoundException($"Oxygen measurement with id {id} is not found");

        return dbOxygenMeasurement.ToResponse();
    }
    
    
    public async Task<List<string>> GetSystemNotificationAdvisors(int systemId)
    {
        var systemNotificationAdvisors =
            await oxygenDbContext.SystemNotificationAdvisors.Where(sna => sna.OxygenMeasurementSystemId == systemId).ToListAsync();

        return systemNotificationAdvisors.Select(sna => sna.Email).ToList();
    }
}