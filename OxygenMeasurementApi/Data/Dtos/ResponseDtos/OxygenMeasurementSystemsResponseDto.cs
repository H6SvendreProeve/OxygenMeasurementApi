namespace OxygenMeasurementApi.Data.Dtos.ResponseDtos;

public class OxygenMeasurementSystemsResponseDto
{
    public int Id { get; init; }
    public string SystemName { get; set; }
    public string Zipcode { get; set; }
    public string Location { get; set; }
    
}