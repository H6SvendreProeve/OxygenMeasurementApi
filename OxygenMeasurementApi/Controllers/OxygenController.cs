using Microsoft.AspNetCore.Mvc;
using OxygenMeasurementApi.Api.Dtos.OxygenMeasurementDtos;
using OxygenMeasurementApi.Services.OxygenMeasurementService;

namespace OxygenMeasurementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OxygenController : ControllerBase
{
    private IOxygenMeasurementService OxygenMeasurementService { get; }
    private readonly ILogger<OxygenController> logger;

    public OxygenController(ILogger<OxygenController> logger, IOxygenMeasurementService oxygenMeasurementService)
    {
        this.logger = logger;
        OxygenMeasurementService = oxygenMeasurementService;
    }

    [HttpPost("CreateOxygenMeasurement")]
    public async Task<IActionResult> CreateOxygenMeasurement(CreateOxygenMeasurement createOxygenMeasurement)
    {
        var created = await OxygenMeasurementService.CreateOxygenMeasurement(createOxygenMeasurement);

        if (!created)
        {
            return BadRequest("something went wrong when creating measurement");
        }

        return Ok();
    }

    [HttpGet("GetAllOxygenMeasurements")]
    public async Task<IActionResult> GetAllOxygenMeasurements()
    {
        var oxygenMeasurements = await OxygenMeasurementService.GetAllOxygenMeasurements();

        return Ok(oxygenMeasurements);
    }

    [HttpGet("GetSpecificAmountOfOxygenMeasurements")]
    public async Task<IActionResult> GetSpecificAmountOfOxygenMeasurements([FromQuery] int amount)
    {
        var oxygenMeasurements = await OxygenMeasurementService.GetSpecificAmountOfOxygenMeasurements(amount);

        return Ok(oxygenMeasurements);
    }
}