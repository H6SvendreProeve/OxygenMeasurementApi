using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OxygenMeasurementApi.Data.Entities;

public class SystemNotificationAdvisor
{
    public int Id { get; set; }
    public string Email { get; set; }
    public int OxygenMeasurementSystemId { get; set; }
    public virtual OxygenMeasurementSystem OxygenMeasurementSystem { get; set; }
}