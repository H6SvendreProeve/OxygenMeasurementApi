namespace OxygenMeasurementApi.Data.Dtos.ResponseDtos;

public class OxygenMeasurementSystemResponseDto
{
    public int Id { get; set; }
    public string SystemName { get; set; }
    public string Zipcode { get; set; }
    public string Location { get; set; }
    public string AdminstratorEmail { get; set; }
    public string ApiKeyId { get; set; }
}