using System.ComponentModel.DataAnnotations;

namespace OxygenMeasurementApi.Api.Dtos.OxygenMeasurementDtos;

public record CreateOxygenMeasurement
(
    [Required]
    decimal OxygenValue
);