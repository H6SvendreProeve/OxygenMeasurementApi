namespace OxygenMeasurementApi.Data.Dtos.ResponseDtos;

/// <summary>
/// Data transfer object (DTO) representing the response for an OxygenMeasurementSystem.
/// </summary>
public class OxygenMeasurementSystemResponseDto
{
    public int Id { get; set; }
    public string SystemName { get; set; }
    public string Location { get; set; }
    
    public List<SystemNotificationAdvisorResponseDto> SystemNotificationAdvisors { get; set; }
    public string ApiKeyValue { get; set; }
}