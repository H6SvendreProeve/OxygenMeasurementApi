namespace OxygenMeasurementApi.Data.Dtos.ResponseDtos;

/// <summary>
/// Data transfer object (DTO) representing the response for an OxygenMeasurementSystems.
/// </summary>
public class OxygenMeasurementSystemsResponseDto
{
    public int OxygenMeasurementSystemId { get; set; }
    public string SystemName { get; set; }
    public string Location { get; set; }
    
}