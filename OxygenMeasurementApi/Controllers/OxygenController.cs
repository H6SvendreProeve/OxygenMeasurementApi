using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OxygenMeasurementApi.Authorization.Filters;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Exceptions;
using OxygenMeasurementApi.Services.OxygenMeasurementService;

namespace OxygenMeasurementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OxygenController : ControllerBase
{
    private readonly IOxygenMeasurementService oxygenMeasurementService;
    private readonly IHubContext<OxygenMeasurementHub> oxygenHub;

    public OxygenController(IOxygenMeasurementService oxygenMeasurementService,
        IHubContext<OxygenMeasurementHub> oxygenHub)
    {
        this.oxygenMeasurementService = oxygenMeasurementService;
        this.oxygenHub = oxygenHub;
    }

    [HttpGet("/")]
    public IActionResult Get()
    {
        return Ok("hello from OxygenMeasurement api");
    }

    [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
    [HttpPost("PostOxygenMeasurement")]
    public async Task<IActionResult> CreateOxygenMeasurement(AddOxygenMeasurementRequestDto addOxygenMeasurement)
    {
        var createdOxygenMeasurement = await oxygenMeasurementService.AddOxygenMeasurementAsync(addOxygenMeasurement);

        if (createdOxygenMeasurement == null)
        {
            return BadRequest("Failed to create oxygen measurement");
        }

        await oxygenHub.Clients.All.SendAsync("ReceiveOxygenMeasurement", createdOxygenMeasurement);

        return CreatedAtAction(nameof(GetOxygenMeasurementById), new { id = createdOxygenMeasurement.Id },
            createdOxygenMeasurement);
    }

    [HttpGet("GetOxygenMeasurementById/{id:int}")]
    public async Task<IActionResult> GetOxygenMeasurementById(int id)
    {
        var oxygenMeasurement = await oxygenMeasurementService.GetOxygenMeasurementByIdAsync(id);

        if (oxygenMeasurement == null)
        {
            return NotFound();
        }

        return Ok(oxygenMeasurement);
    }

    [HttpGet("GetAllOxygenMeasurements")]
    public async Task<IActionResult> GetAllOxygenMeasurements()
    {
        var oxygenMeasurements = await oxygenMeasurementService.GetAllOxygenMeasurementsAsync();

        return Ok(oxygenMeasurements);
    }

    [HttpGet("GetAllOxygenMeasurementsForSystem")]
    public async Task<IActionResult> GetAllOxygenMeasurements(int systemId)
    {
        var oxygenMeasurements = await oxygenMeasurementService.GetAllSystemOxygenMeasurementsAsync(systemId);
        
        return Ok(oxygenMeasurements);
    }


    [HttpGet("GetSpecificAmountOfOxygenMeasurements")]
    public async Task<IActionResult> GetSpecificAmountOfOxygenMeasurements(int systemId, int amount)
    {
        var oxygenMeasurements =
            await oxygenMeasurementService.GetSpecificAmountOfOxygenMeasurementsAsync(systemId, amount);

        return Ok(oxygenMeasurements);
    }
}