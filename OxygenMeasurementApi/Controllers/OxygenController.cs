using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OxygenMeasurementApi.Api.Dtos.OxygenMeasurementDtos;
using OxygenMeasurementApi.Services.OxygenMeasurementService;

namespace OxygenMeasurementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OxygenController : ControllerBase
{
    private IOxygenMeasurementService OxygenMeasurementService { get; }
    private ILogger<OxygenController> Logger { get; }
    private IHubContext<OxygenMeasurementHub> OxygenHub { get; }

    public OxygenController(ILogger<OxygenController> logger, IOxygenMeasurementService oxygenMeasurementService,IHubContext<OxygenMeasurementHub> oxygenHub )
    {
        Logger = logger;
        OxygenMeasurementService = oxygenMeasurementService;
        OxygenHub = oxygenHub;
    }

    [HttpPost("CreateOxygenMeasurement")]
    public async Task<IActionResult> CreateOxygenMeasurement(CreateOxygenMeasurement createOxygenMeasurement)
    {
        var created = await OxygenMeasurementService.CreateOxygenMeasurement(createOxygenMeasurement);

        if (!created)
        {
            return BadRequest("something went wrong when creating measurement");
        }
        
        await OxygenHub.Clients.All.SendAsync("ReceiveOxygenMeasurement", createOxygenMeasurement);

        return Created("result", created);
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