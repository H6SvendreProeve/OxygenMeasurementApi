namespace OxygenMeasurementApi.Data.Entities;

public class OxygenMeasurementSystem
{
    public int Id { get; set; }
    public string SystemName { get; set; }
    public string Zipcode { get; set; }
    public string Location { get; set; }
    public string AdminstratorEmail { get; set; }

    public string ApiKeyId { get; set; }
    public virtual ApiKey ApiKey { get; set; }
}