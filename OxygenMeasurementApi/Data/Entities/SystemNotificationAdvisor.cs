namespace OxygenMeasurementApi.Data.Entities;

/// <summary>
/// EntityFramework Entity.
/// With a relationship to OxygenMeasurementSystem
/// </summary>
public class SystemNotificationAdvisor
{
    public int SystemNotificationAdvisorId { get; set; }
    public string Email { get; set; }
    public int OxygenMeasurementSystemId { get; set; }
    public virtual OxygenMeasurementSystem OxygenMeasurementSystem { get; set; }
}