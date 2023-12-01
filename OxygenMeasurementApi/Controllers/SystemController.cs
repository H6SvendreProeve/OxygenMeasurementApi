using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using OxygenMeasurementApi.Data.Dtos.Mapper;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Services.OxygenMeasurementSystemService;

namespace OxygenMeasurementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SystemController : ControllerBase
{

   private IOxygenMeasurementSystemService SystemService { get; }
   
   public SystemController(IOxygenMeasurementSystemService systemService)
   {
      SystemService = systemService;
   }
   
   [HttpPost]
   public async Task<IActionResult> CreateOxygenMeasurementSystem(CreateOxygenMeasurementSystemDto oxygenMeasurementSystemDto)
   {
      if (!TryValidateModel(oxygenMeasurementSystemDto))
      {
         return BadRequest("not all required parameters is met");
      }

      var oxygenMeasurementSystemResponse = await SystemService.CreateOxygenMeasurementSystem(oxygenMeasurementSystemDto);

      if (oxygenMeasurementSystemResponse.Id < 1)
      {
         return BadRequest("something went wrong");
      }
      
      return CreatedAtAction(nameof(GetOxygenMeasurementSystem), new { id = oxygenMeasurementSystemResponse.Id }, oxygenMeasurementSystemResponse);
   }

   [HttpGet("GetOxygenMeasurementSystem/{id:int}")]
   public async Task<IActionResult> GetOxygenMeasurementSystem(int id)
   {
      var oxygenMeasurementSystemResponse = await SystemService.GetOxygenMeasurementSystemById(id);

      if (oxygenMeasurementSystemResponse == null)
      {
         return NotFound("id was not found");
      }
      
      return Ok(oxygenMeasurementSystemResponse);
   }
}