namespace OxygenMeasurementApi.Data.Entities;

public class OxygenMeasurement
{
    public int Id { get; set; }
    public decimal OxygenValue { get; set; }
    public DateTime MeasurementTime { get; set; }

    public int OxygenMeasurementSystemId { get; set; }

    public virtual OxygenMeasurementSystem OxygenMeasurementSystem { get; set; }
     
}