namespace OxygenMeasurementApi.Data.Entities;

public class ApiKey
{
    public int ApiKeyId { get; set; }
    public string ApiKeyValue { get; set; }
    public virtual OxygenMeasurementSystem OxygenMeasurementSystem { get; set; }
}