namespace OxygenMeasurementApi.Api.Dtos.OxygenMeasurementDtos;

public record OxygenMeasurementDto
(
    int Id,
    DateTime MeasurementDate,
    double OxygenValue
);