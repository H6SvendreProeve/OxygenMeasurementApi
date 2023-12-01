namespace OxygenMeasurementApi.Data.Entities;

public class ApiKey
{
    public string ApiKeyId { get; set; }
    
    public virtual OxygenMeasurementSystem OxygenMeasurementSystem { get; set; }
}