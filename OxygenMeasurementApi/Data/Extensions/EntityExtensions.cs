using OxygenMeasurementApi.Data.Entities;
using OxygenMeasurementApi.OxygenMeasurements.Create;

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