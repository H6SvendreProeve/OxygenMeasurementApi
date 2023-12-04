using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Dtos.ResponseDtos;

public class OxygenMeasurementSystemResponseDto
{
    public int Id { get; init; }
    public string SystemName { get; set; }
    public string Zipcode { get; set; }
    public string Location { get; set; }
    
    public List<SystemNotificationAdvisorResponseDto> SystemNotificationAdvisors { get; set; }
    public string ApiKeyValue { get; init; }
}