using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Dtos.ResponseDtos;

namespace OxygenMeasurementApi.Services.OxygenMeasurementService;

/// <summary>
/// 
/// </summary>
/// <summary>
/// Service interface for managing Oxygen Measurements.
/// </summary>
public interface IOxygenMeasurementService
{
    /// <summary>
    /// Gets an Oxygen Measurement by its unique identifier.
    /// </summary>
    /// <param name="id">The identifier of the Oxygen Measurement.</param>
    /// <returns>A task that represents the asynchronous operation, returning the Oxygen Measurement response DTO if found; otherwise, null.</returns>
    Task<OxygenMeasurementResponseDto?> GetOxygenMeasurementByIdAsync(int id);

    /// <summary>
    /// Gets all Oxygen Measurements.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, returning a list of Oxygen Measurement response DTOs.</returns>
    Task<List<OxygenMeasurementResponseDto?>> GetAllOxygenMeasurementsAsync();

    /// <summary>
    /// Gets all Oxygen Measurements for a specific Oxygen Measurement System.
    /// </summary>
    /// <param name="systemId">The identifier of the Oxygen Measurement System.</param>
    /// <returns>A task that represents the asynchronous operation, returning a list of Oxygen Measurement response DTOs for the specified system.</returns>
    Task<List<OxygenMeasurementResponseDto?>> GetAllSystemOxygenMeasurementsAsync(int systemId);

    /// <summary>
    /// Adds a new Oxygen Measurement.
    /// </summary>
    /// <param name="oxygenMeasurement">The data for the new Oxygen Measurement.</param>
    /// <returns>A task that represents the asynchronous operation, returning the created Oxygen Measurement response DTO.</returns>
    Task<OxygenMeasurementResponseDto?> AddOxygenMeasurementAsync(AddOxygenMeasurementRequestDto oxygenMeasurement);

    /// <summary>
    /// Gets a specific amount of the latest Oxygen Measurements for a specific Oxygen Measurement System.
    /// </summary>
    /// <param name="systemId">The identifier of the Oxygen Measurement System.</param>
    /// <param name="amount">The desired amount of Oxygen Measurements.</param>
    /// <returns>A task that represents the asynchronous operation, returning a list of the latest Oxygen Measurement response DTOs for the specified system.</returns>
    Task<List<OxygenMeasurementResponseDto?>> GetSpecificAmountOfOxygenMeasurementsAsync(int systemId, int amount);

    Task<List<string>> GetSystemNotificationAdvisors(int systemId);
}
