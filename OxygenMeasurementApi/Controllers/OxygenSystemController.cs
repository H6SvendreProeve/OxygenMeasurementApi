using Microsoft.AspNetCore.Mvc;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Services.OxygenMeasurementSystemService;

namespace OxygenMeasurementApi.Controllers;

/// <summary>
/// Controller for handling OxygenMeasurementSystem-related API requests.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OxygenSystemController : ControllerBase
{

   private IOxygenMeasurementSystemService SystemService { get; }
   
   // Constructor that takes an IOxygenMeasurementSystemService dependency to interact with oxygen measurement systems.
   public OxygenSystemController(IOxygenMeasurementSystemService systemService)
   {
      SystemService = systemService;
   }

   /// <summary>
   /// Endpoint to create a new OxygenMeasurementSystem.
   /// </summary>
   /// <param name="oxygenMeasurementSystemRequestDto">The data for the new OxygenMeasurementSystem.</param>
   /// <returns>
   /// A response indicating the result of the operation and the newly created OxygenMeasurementSystem.
   /// or a badRequest if it could not be created
   /// </returns>
   [HttpPost("PostOxygenMeasurementSystem")]
   public async Task<IActionResult> PostOxygenMeasurementSystem(
      AddOxygenMeasurementSystemRequestDto oxygenMeasurementSystemRequestDto)
   {
      var oxygenMeasurementSystemResponse =
         await SystemService.AddOxygenMeasurementSystemAsync(oxygenMeasurementSystemRequestDto);

      if (oxygenMeasurementSystemResponse == null || oxygenMeasurementSystemResponse.Id < 1)
      {
         return BadRequest("something went wrong");
      }

      return CreatedAtAction(nameof(GetOxygenMeasurementSystem), new { id = oxygenMeasurementSystemResponse.Id },
         oxygenMeasurementSystemResponse);
   }

   /// <summary>
   /// Endpoint to retrieve an OxygenMeasurementSystem by its ID.
   /// </summary>
   /// <param name="id">The ID of the OxygenMeasurementSystem to retrieve.</param>
   /// <returns>
   /// A response containing the retrieved OxygenMeasurementSystem.
   /// or a notFound response
   /// </returns>
   [HttpGet("GetOxygenMeasurementSystem/{id:int}")]
   public async Task<IActionResult> GetOxygenMeasurementSystem(int id)
   {
      var oxygenMeasurementSystemResponse = await SystemService.GetOxygenMeasurementSystemByIdAsync(id);

      if (oxygenMeasurementSystemResponse == null)
      {
         return NotFound("id was not found");
      }
      
      return Ok(oxygenMeasurementSystemResponse);
   }
}