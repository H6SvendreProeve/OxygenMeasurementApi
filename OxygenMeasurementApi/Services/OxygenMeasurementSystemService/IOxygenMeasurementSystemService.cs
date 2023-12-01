using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Dtos.ResponseDtos;

namespace OxygenMeasurementApi.Services.OxygenMeasurementSystemService;

public interface IOxygenMeasurementSystemService
{
    public Task<OxygenMeasurementSystemResponseDto> CreateOxygenMeasurementSystem(CreateOxygenMeasurementSystemDto createDto);
    public Task<OxygenMeasurementSystemResponseDto?> GetOxygenMeasurementSystemById(int id);
}