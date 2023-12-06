namespace OxygenMeasurementApi.Data.Entities;

/// <summary>
/// EntityFrameWork Entity OxygenMeasurement.
/// With a 1..1 relationship to OxygenMeasurementSystem.
/// </summary>
public class OxygenMeasurement
{
    public int OxygenMeasurementId { get; set; }
    public decimal OxygenValue { get; set; }
    public DateTime MeasurementTime { get; set; }

    public int OxygenMeasurementSystemId { get; set; }

    public virtual OxygenMeasurementSystem OxygenMeasurementSystem { get; set; }
     
}