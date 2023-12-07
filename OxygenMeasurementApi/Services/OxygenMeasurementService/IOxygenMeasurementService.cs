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
    /// Adds a new Oxygen Measurement.
    /// </summary>
    /// <param name="oxygenMeasurement">The data for the new Oxygen Measurement.</param>
    /// <returns>A task that represents the asynchronous operation, returning the created Oxygen Measurement response DTO.</returns>
    Task<OxygenMeasurementResponseDto?> AddOxygenMeasurementAsync(AddOxygenMeasurementRequestDto oxygenMeasurement);
    

    Task<List<string>> GetSystemNotificationAdvisors(int systemId);
}
