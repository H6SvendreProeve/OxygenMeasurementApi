namespace OxygenMeasurementApi.Data.Entities;

/// <summary>
/// EntityFramework Entity.
/// With a 1..1 relationship to ApiKey.
/// 1..* relationship to OxygenMeasurement.
/// 1..* relationship to SystemNotificationAdvisor.
/// </summary>
public class OxygenMeasurementSystem
{
    public int OxygenMeasurementSystemId { get; set; }
    public string SystemName { get; set; }
    public string Location { get; set; }

    public int ApiKeyId { get; set; }
    public virtual ApiKey ApiKey { get; set; }

    public virtual ICollection<OxygenMeasurement> OxygenMeasurements { get; set; }

    public virtual ICollection<SystemNotificationAdvisor> SystemNotificationAdvisors { get; set; }
}