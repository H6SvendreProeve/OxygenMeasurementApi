using OxygenMeasurementApi.Data.Entities;
using OxygenMeasurementApi.OxygenMeasurements.Create;

namespace OxygenMeasurementApi.Services.OxygenMeasurementService;

public interface IOxygenMeasurementService
{
    Task<bool> CreateOxygenMeasurement(CreateOxygenMeasurement createOxygenMeasurement);
    Task<List<OxygenMeasurement>> GetAllOxygenMeasurements();
    Task<List<OxygenMeasurement>> GetSpecificAmountOfOxygenMeasurements(int amount);
    Task<OxygenMeasurement?> GetOxygenMeasurementById(int id);
}