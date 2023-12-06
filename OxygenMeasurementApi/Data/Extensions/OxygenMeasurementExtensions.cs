using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Extensions;

/// <summary>
/// Extension methods for manipulating lists of OxygenMeasurements.
/// </summary>
public static class OxygenMeasurementExtensions
{
    /// <summary>
    /// Converts the MeasurementTime of each OxygenMeasurement in the list to the local timezone.
    /// </summary>
    /// <param name="oxygenMeasurements">The list of OxygenMeasurements to be converted.</param>
    /// <returns>The list of OxygenMeasurements with MeasurementTime converted to the local timezone.</returns>
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