using System.ComponentModel.DataAnnotations;

namespace OxygenMeasurementApi.Data.Dtos.RequestDtos;

/// <summary>
/// Data transfer object (DTO) for adding a new SystemNotificationAdvisor.
/// </summary>
public class AddSystemNotificationAdvisorRequestDto
{
    /// <value>The email address of the system notification advisor. Must be a required non-null string.</value>
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; init; }
}