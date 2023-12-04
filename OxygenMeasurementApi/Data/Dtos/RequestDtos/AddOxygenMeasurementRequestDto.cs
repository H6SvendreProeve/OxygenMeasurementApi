using System.ComponentModel.DataAnnotations;
using OxygenMeasurementApi.Data.AttributeValidators;

namespace OxygenMeasurementApi.Data.Dtos.RequestDtos;

public class AddOxygenMeasurementRequestDto
{
    [Required] public decimal OxygenValue { get; init; }

    [Required]
    [GreaterThanZero(ErrorMessage = "OxygenMeasurementSystemId must be greater than 0.")]
    public int OxygenMeasurementSystemId { get; init; }
}