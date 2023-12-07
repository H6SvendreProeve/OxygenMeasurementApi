using Microsoft.AspNetCore.Mvc;
using OxygenMeasurementApi.Authorization.Filters;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Services.OxygenMeasurementService;
using OxygenMeasurementMailLibrary;

namespace OxygenMeasurementApi.Controllers;

/// <summary>
/// Controller for handling OxygenMeasurement-related API requests.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OxygenController : ControllerBase
{
    private readonly IOxygenMeasurementService oxygenMeasurementService;

    private MailHandler MailHandler { get; }
    private OxygenMail OxygenMail { get; set; }

    private SmtpConfigLoader ConfigLoader { get; set; }

    // Constructor that takes an IOxygenMeasurementService dependency to interact with oxygen measurements.
    public OxygenController(IOxygenMeasurementService oxygenMeasurementService, SmtpConfigLoader configLoader)
    {
        this.oxygenMeasurementService = oxygenMeasurementService;
        ConfigLoader = configLoader;

        MailHandler = new MailHandler(ConfigLoader.SmtpConfig?.SmtpUser, ConfigLoader.SmtpConfig.SmtpPassword,
            ConfigLoader.SmtpConfig.SmtpHost, ConfigLoader.SmtpConfig.Port);
        OxygenMail = new OxygenMail(MailHandler);
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
        
        var mailReceivers =
            await oxygenMeasurementService.GetSystemNotificationAdvisors(createdOxygenMeasurement
                .OxygenMeasurementSystemId);
        
        if (createdOxygenMeasurement.OxygenValue > (decimal)4.00)
        {
           // OxygenMail.SendMailToSubscribes(mailReceivers, OxygenMail.MailOptions.Harvest);
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
        
        return Ok(oxygenMeasurement);
    }
    
}