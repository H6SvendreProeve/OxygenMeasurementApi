using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Extensions;

public static class OxygenMeasurementExtensions
{
    // convert to local datetime and not UTC
    public static List<OxygenMeasurement> ConvertMeasurementTimeToLocalTimezone(
        this List<OxygenMeasurement> oxygenMeasurements)
    {
        
        foreach (var oxygenMeasurement in oxygenMeasurements)
        {
            oxygenMeasurement.MeasurementTime = oxygenMeasurement.MeasurementTime.ToLocalTime();
        }

        return oxygenMeasurements;
    }
}