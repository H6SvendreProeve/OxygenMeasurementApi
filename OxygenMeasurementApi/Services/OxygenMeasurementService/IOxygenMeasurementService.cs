using OxygenMeasurementApi.Api.Dtos.OxygenMeasurementDtos;
using OxygenMeasurementApi.Entities;

namespace OxygenMeasurementApi.Services.OxygenMeasurementService;

public interface IOxygenMeasurementService
{
    Task<bool> CreateOxygenMeasurement(CreateOxygenMeasurement createOxygenMeasurement);
    Task<List<OxygenMeasurement>> GetAllOxygenMeasurements();
    Task<List<OxygenMeasurement>> GetSpecificAmountOfOxygenMeasurements(int amount);
}