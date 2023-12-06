using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Dtos.ResponseDtos;

namespace OxygenMeasurementApi.Services.OxygenMeasurementSystemService;

/// <summary>
/// 
/// </summary>
public interface IOxygenMeasurementSystemService
{

    /// <summary>
    /// Retrieves an Oxygen Measurement System by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the Oxygen Measurement System to retrieve.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the retrieved
    /// <see cref="OxygenMeasurementSystemResponseDto"/> if found.
    /// </returns>

    public Task<OxygenMeasurementSystemResponseDto?> GetOxygenMeasurementSystemByIdAsync(int id);
    
    /// <summary>
    /// Retrieves all Oxygen Measurement Systems in the system.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing a list of
    /// <see cref="OxygenMeasurementSystemsResponseDto"/> representing all Oxygen Measurement Systems.
    /// </returns>
    Task<List<OxygenMeasurementSystemsResponseDto?>> GetAllOxygenMeasurementSystemsAsync();

    /// <summary>
    /// Adds a new Oxygen Measurement System to the system.
    /// </summary>
    /// <param name="addRequestDto">The data for the new Oxygen Measurement System.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the added
    /// <see cref="OxygenMeasurementSystemResponseDto"/> if successful.
    /// </returns>
    Task<OxygenMeasurementSystemResponseDto?> AddOxygenMeasurementSystemAsync(
        AddOxygenMeasurementSystemRequestDto addRequestDto);
}