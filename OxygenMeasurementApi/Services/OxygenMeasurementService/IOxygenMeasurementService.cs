using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Dtos.ResponseDtos;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Services.OxygenMeasurementService;

public interface IOxygenMeasurementService
{
    Task<OxygenMeasurementResponseDto?> GetOxygenMeasurementByIdAsync(int id);
    Task<List<OxygenMeasurementResponseDto?>> GetAllOxygenMeasurementsAsync();
    Task<List<OxygenMeasurementResponseDto?>> GetAllSystemOxygenMeasurementsAsync(int systemId);
    Task<OxygenMeasurementResponseDto?> AddOxygenMeasurementAsync(AddOxygenMeasurementRequestDto oxygenMeasurement);
    Task<List<OxygenMeasurementResponseDto?>> GetSpecificAmountOfOxygenMeasurementsAsync(int systemId, int amount);
    
}