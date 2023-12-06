namespace OxygenMeasurementApi.Data.Dtos.ResponseDtos;

/// <summary>
/// Data transfer object (DTO) representing the response for an OxygenMeasurement.
/// </summary>
public class OxygenMeasurementResponseDto
{
        
        public int Id { get; set; }
        public decimal OxygenValue { get; set; }
        public DateTime MeasurementTime { get; set; }
        public int OxygenMeasurementSystemId { get; set; }

        public string SystemName { get; set; }
        
        public string SystemLocation { get; set; }
        
}