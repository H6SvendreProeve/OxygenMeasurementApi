using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Context;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Dtos.ResponseDtos;
using OxygenMeasurementApi.Data.Mappers;
using OxygenMeasurementApi.Exceptions;

namespace OxygenMeasurementApi.Services.OxygenMeasurementSystemService;

/// <summary>
/// Service implementation for managing Oxygen Measurement Systems.
/// </summary>
public class OxygenMeasurementSystemService : IOxygenMeasurementSystemService
{
    private readonly IOxygenDbContext oxygenDbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="OxygenMeasurementSystemService"/> class.
    /// </summary>
    /// <param name="dbContext">The Oxygen database context.</param>
    public OxygenMeasurementSystemService(IOxygenDbContext dbContext)
    {
        oxygenDbContext = dbContext;
    }

    /// <summary>
    /// Gets an Oxygen Measurement System by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the Oxygen Measurement System to retrieve.</param>
    /// <returns>The Oxygen Measurement System response DTO if found; otherwise, null.</returns>
    /// <exception cref="NotFoundException">Thrown when the Oxygen Measurement System with the specified ID is not found.</exception>
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

    /// <summary>
    /// Gets all Oxygen Measurement Systems asynchronously.
    /// </summary>
    /// <returns>A list of Oxygen Measurement System response DTOs.</returns>
    public async Task<List<OxygenMeasurementSystemsResponseDto?>> GetAllOxygenMeasurementSystemsAsync()
    {
        var dbOxygenMeasurementSystems = await oxygenDbContext.OxygenMeasurementSystems.ToListAsync();

        return dbOxygenMeasurementSystems.ToResponseList();
    }
    
 
    /// <summary>
    /// Adds a new Oxygen Measurement System asynchronously.
    /// </summary>
    /// <param name="addRequestDto">The DTO containing information to create the new Oxygen Measurement System.</param>
    /// <returns>The newly created Oxygen Measurement System response DTO.</returns>
    public async Task<OxygenMeasurementSystemResponseDto?> AddOxygenMeasurementSystemAsync(
        AddOxygenMeasurementSystemRequestDto addRequestDto)
    {
        var oxygenMeasurementSystem = addRequestDto.ToDbEntity();
        await oxygenDbContext.OxygenMeasurementSystems.AddAsync(oxygenMeasurementSystem);
        await oxygenDbContext.SaveChangesAsync();

        return oxygenMeasurementSystem.ToResponse();
    }
}