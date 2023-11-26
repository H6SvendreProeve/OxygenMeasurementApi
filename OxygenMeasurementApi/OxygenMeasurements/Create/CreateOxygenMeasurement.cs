using System.ComponentModel.DataAnnotations;

namespace OxygenMeasurementApi.OxygenMeasurements.Create;

public record CreateOxygenMeasurement
(
    [Required]
    decimal OxygenValue
);