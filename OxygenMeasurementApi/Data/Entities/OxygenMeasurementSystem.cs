namespace OxygenMeasurementApi.Data.Entities;

public class OxygenMeasurementSystem
{
    public int Id { get; set; }
    public string SystemName { get; set; }
    public string Zipcode { get; set; }
    public string Location { get; set; }

    public int ApiKeyId { get; set; }
    public virtual ApiKey ApiKey { get; set; }
    
    public virtual ICollection<OxygenMeasurement> OxygenMeasurements { get;set; }
    
    public virtual List<SystemNotificationAdvisor> SystemNotificationAdvisors { get;set; }
    
    
}