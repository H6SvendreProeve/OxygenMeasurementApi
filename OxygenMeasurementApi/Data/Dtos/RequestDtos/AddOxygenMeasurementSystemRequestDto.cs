using System.ComponentModel.DataAnnotations;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Dtos.RequestDtos;

public class AddOxygenMeasurementSystemRequestDto
{
    [Required]
    public string SystemName { get; init; }
    [Required]
    public string Zipcode { get; init; }
    [Required]
    public string Location { get; init; }
    [Required]
    public List<SystemNotificationAdvisorDto> SystemNotificationAdvisors { get; init; }
    
}