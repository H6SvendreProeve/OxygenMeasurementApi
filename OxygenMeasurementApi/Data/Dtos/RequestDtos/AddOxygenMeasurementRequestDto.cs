using System.ComponentModel.DataAnnotations;
using OxygenMeasurementApi.Data.AttributeValidators;

namespace OxygenMeasurementApi.Data.Dtos.RequestDtos;

/// <summary>
/// Data transfer object (DTO) representing the request to add a new OxygenMeasurement.
/// </summary>
public class AddOxygenMeasurementRequestDto
{
    ///  <value>Property <c>OxygenValue</c> The oxygen value for the new measurement.</value>
    [Required(ErrorMessage = "OxygenValue is required.")] 
    public decimal OxygenValue { get; init; }


   /// <value>The ID of the associated OxygenMeasurementSystem. Must be a required non-null integer greater than 0.</value>
    [Required(ErrorMessage = "OxygenMeasurementSystemId is required")]
    [GreaterThanZero(ErrorMessage = "OxygenMeasurementSystemId must be greater than 0.")]
    public int OxygenMeasurementSystemId { get; init; }
}