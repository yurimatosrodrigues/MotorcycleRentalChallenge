using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalChallenge.Application.InputModel;
using MotorcycleRentalChallenge.Application.Interfaces;
using MotorcycleRentalChallenge.Core.Exceptions;

namespace MotorcycleRentalChallenge.API.Controllers
{
    [ApiController]
    [Route("/motos")]
    public class MotorcycleController : ControllerBase
    {
        private readonly IMotorcycleService _motorcycleService;

        public MotorcycleController(IMotorcycleService motorcycleService)
        {
            _motorcycleService = motorcycleService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddMotorcycleInputModel request)
        {
            try
            {
                var id = await _motorcycleService.AddAsync(request);

                return CreatedAtAction(nameof(Get), new { id }, null);
            }
            catch(DomainException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? plate)
        {
            try
            {
                var motorcycles = await _motorcycleService.GetByPlate(plate);
               
                return Ok(motorcycles);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
