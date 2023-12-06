namespace OxygenMeasurementApi.Data.Entities;

/// <summary>
/// EntityFrameWork Entity class ApiKey.
/// with a 1..1 relationship to OxygenMeasurementSystem.
/// </summary>
public class ApiKey
{
    public int ApiKeyId { get; set; }
    public string ApiKeyValue { get; set; }
    public virtual OxygenMeasurementSystem OxygenMeasurementSystem { get; set; }
}