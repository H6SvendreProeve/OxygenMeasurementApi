namespace OxygenMeasurementApi.Entities;

public class OxygenMeasurement
{
    public int Id { get; set; }
    public decimal OxygenValue { get; set; }
    public DateTime MeasurementTime { get; set; }
    
}