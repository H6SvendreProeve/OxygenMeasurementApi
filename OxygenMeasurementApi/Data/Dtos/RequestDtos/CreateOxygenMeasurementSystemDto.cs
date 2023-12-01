using System.ComponentModel.DataAnnotations;

namespace OxygenMeasurementApi.Data.Dtos.RequestDtos;

public class CreateOxygenMeasurementSystemDto
{
    [Required]
    public string SystemName { get; set; }
    [Required]
    public string Zipcode { get; set; }
    [Required]
    public string Location { get; set; }
    [Required]
    [MaxLength(250)]
    public string AdminstratorEmail { get; set; }
    
    
}