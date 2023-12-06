using System.ComponentModel.DataAnnotations;

namespace OxygenMeasurementApi.Data.Dtos.RequestDtos;

/// <summary>
/// Data transfer object (DTO) for adding a new OxygenMeasurementSystem.
/// </summary>
public class AddOxygenMeasurementSystemRequestDto
{
    /// <value>The name of the system. Must be a required non-null string.</value>
    [Required(ErrorMessage = "SystemName is required.")]
    public string SystemName { get; init; }
    /// <value>The location of the system. Must be a required non-null string.</value>
    [Required(ErrorMessage = "Location is required.")]
    public string Location { get; init; }
    /// <value>The list of system notification advisors. Must be a required non-null list.</value>
    [Required(ErrorMessage = "SystemNotificationAdvisors is required.")]
    public List<AddSystemNotificationAdvisorRequestDto> SystemNotificationAdvisors { get; init; }
    
}