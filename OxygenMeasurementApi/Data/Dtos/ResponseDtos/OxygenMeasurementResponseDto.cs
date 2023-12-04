namespace OxygenMeasurementApi.Data.Dtos.ResponseDtos;

public class OxygenMeasurementResponseDto
{
        public int Id { get; init; }
        public decimal OxygenValue { get; set; }
        public DateTime MeasurementTime { get; set; }
        public int OxygenMeasurementSystemId { get; set; }

        public string SystemName { get; set; }
        
        public string SystemLocation { get; set; }
        
        public string SystemZipcode { get; set; }
}