using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OxygenMeasurementApi.Authorization.Filters;
using OxygenMeasurementApi.OxygenMeasurements.Create;
using OxygenMeasurementApi.Services.OxygenMeasurementService;

namespace OxygenMeasurementApi.Controllers;

[ApiController]
[Route("[controller]")]
[ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
public class OxygenController : ControllerBase
{
    private IOxygenMeasurementService OxygenMeasurementService { get; }
    private IHubContext<OxygenMeasurementHub> OxygenHub { get; }

    public OxygenController(IOxygenMeasurementService oxygenMeasurementService,IHubContext<OxygenMeasurementHub> oxygenHub )
    {
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

    [AllowAnonymous]
    [HttpGet("GetAllOxygenMeasurements")]
    public async Task<IActionResult> GetAllOxygenMeasurements()
    {
        var oxygenMeasurements = await OxygenMeasurementService.GetAllOxygenMeasurements();

        return Ok(oxygenMeasurements);
    }

    [AllowAnonymous]
    [HttpGet("GetSpecificAmountOfOxygenMeasurements")]
    public async Task<IActionResult> GetSpecificAmountOfOxygenMeasurements([FromQuery] int amount)
    {
        var oxygenMeasurements = await OxygenMeasurementService.GetSpecificAmountOfOxygenMeasurements(amount);

        return Ok(oxygenMeasurements);
    }

    [HttpGet("/")]
    public IActionResult Get()
    {
        return Ok("hello from OxygenMeasurement api");
    }
}