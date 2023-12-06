using Microsoft.AspNetCore.Mvc;
using OxygenMeasurementApi.Authorization.Filters;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Services.OxygenMeasurementService;

namespace OxygenMeasurementApi.Controllers;

/// <summary>
/// Controller for handling OxygenMeasurement-related API requests.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OxygenController : ControllerBase
{
    private readonly IOxygenMeasurementService oxygenMeasurementService;

    // Constructor that takes an IOxygenMeasurementService dependency to interact with oxygen measurements.
    public OxygenController(IOxygenMeasurementService oxygenMeasurementService)
    {
        this.oxygenMeasurementService = oxygenMeasurementService;
    }


    /// <summary>
    /// Endpoint to add a new OxygenMeasurement.
    /// </summary>
    /// <param name="addOxygenMeasurement">The data for the new OxygenMeasurement.</param>
    /// <returns>
    /// if the operation was successful. A response indicating the result of the operation and the newly created OxygenMeasurement
    /// or a badRequest
    /// </returns>
    [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
    [HttpPost("PostOxygenMeasurement")]
    public async Task<IActionResult> PostOxygenMeasurement(AddOxygenMeasurementRequestDto addOxygenMeasurement)
    {
        var createdOxygenMeasurement = await oxygenMeasurementService.AddOxygenMeasurementAsync(addOxygenMeasurement);

        if (createdOxygenMeasurement == null)
        {
            return BadRequest("Failed to create oxygen measurement");
        }

        // Return a 201 Created response with the newly created OxygenMeasurement.
        return CreatedAtAction(nameof(GetOxygenMeasurementById), new { id = createdOxygenMeasurement.Id },
            createdOxygenMeasurement);
    }

    /// <summary>
    /// Endpoint to retrieve an OxygenMeasurement by its ID.
    /// </summary>
    /// <param name="id">The ID of the OxygenMeasurement to retrieve.</param>
    /// <returns>
    /// A response containing the retrieved OxygenMeasurement if it exists,
    /// or a NotFound response if the OxygenMeasurement was not found.
    /// </returns>
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
    /// <summary>
    /// Endpoint to retrieve all OxygenMeasurements.
    /// </summary>
    /// <returns>A response containing a list of all OxygenMeasurements.</returns>
    [HttpGet("GetAllOxygenMeasurements")]
    public async Task<IActionResult> GetAllOxygenMeasurements()
    {
        var oxygenMeasurements = await oxygenMeasurementService.GetAllOxygenMeasurementsAsync();

        return Ok(oxygenMeasurements);
    }

    /// <summary>
    /// Endpoint to retrieve all OxygenMeasurements for a specific system.
    /// </summary>
    /// <param name="systemId">The ID of the system for which to retrieve OxygenMeasurements.</param>
    /// <returns>
    /// a List of OxygenMeasurements if there is any OxygenMeasurements for the specified system.
    /// or an empty list
    /// </returns>
    [HttpGet("GetAllOxygenMeasurementsForSystem")]
    public async Task<IActionResult> GetAllOxygenMeasurements(int systemId)
    {
        var oxygenMeasurements = await oxygenMeasurementService.GetAllSystemOxygenMeasurementsAsync(systemId);

        return Ok(oxygenMeasurements);
    }

    /// <summary>
    /// Endpoint to retrieve a specific amount of recent OxygenMeasurements for a specific system.
    /// </summary>
    /// <param name="systemId">The ID of the system for which to retrieve OxygenMeasurements.</param>
    /// <param name="amount">The desired amount of recent OxygenMeasurements.</param>
    /// <returns>
    /// a List of OxygenMeasurements if there is any OxygenMeasurements for the specified system.
    /// or an empty list
    /// </returns>
    [HttpGet("GetSpecificAmountOfOxygenMeasurements")]
    public async Task<IActionResult> GetSpecificAmountOfOxygenMeasurements(int systemId, int amount)
    {
        var oxygenMeasurements =
            await oxygenMeasurementService.GetSpecificAmountOfOxygenMeasurementsAsync(systemId, amount);

        return Ok(oxygenMeasurements);
    }
}