using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Dtos.ResponseDtos;

namespace OxygenMeasurementApi.Services.OxygenMeasurementSystemService;

public interface IOxygenMeasurementSystemService
{
    public Task<OxygenMeasurementSystemResponseDto?> GetOxygenMeasurementSystemByIdAsync(int id);
    Task<List<OxygenMeasurementSystemsResponseDto?>> GetAllOxygenMeasurementSystemsAsync();

    Task<OxygenMeasurementSystemResponseDto?> AddOxygenMeasurementSystemAsync(
        AddOxygenMeasurementSystemRequestDto addRequestDto);
}