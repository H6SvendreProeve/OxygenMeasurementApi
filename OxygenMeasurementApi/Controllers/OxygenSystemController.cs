using Microsoft.AspNetCore.Mvc;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Exceptions;
using OxygenMeasurementApi.Services.OxygenMeasurementSystemService;
using ArgumentException = OxygenMeasurementApi.Exceptions.ArgumentException;

namespace OxygenMeasurementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OxygenSystemController : ControllerBase
{

   private IOxygenMeasurementSystemService SystemService { get; }
   
   public OxygenSystemController(IOxygenMeasurementSystemService systemService)
   {
      SystemService = systemService;
   }

   [HttpPost("PostOxygenMeasurementSystem")]
   public async Task<IActionResult> CreateOxygenMeasurementSystem(
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