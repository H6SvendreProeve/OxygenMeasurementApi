using OxygenMeasurementApi.Api.Dtos.OxygenMeasurementDtos;
using OxygenMeasurementApi.Entities;

namespace OxygenMeasurementApi.Data.Extensions;

public static class EntityExtensions
{
  
    public static OxygenMeasurement OxygenMeasurementAsEntity(this CreateOxygenMeasurement createOxygenMeasurement)
    {
        return new OxygenMeasurement
        {
            MeasurementTime = DateTime.UtcNow,
            OxygenValue = createOxygenMeasurement.OxygenValue
        };
    }
}