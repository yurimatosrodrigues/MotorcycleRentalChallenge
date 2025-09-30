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
            catch (DomainException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] string? plate)
        {
            try
            {
                var motorcycles = await _motorcycleService.GetByPlateAsync(plate);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id,
            [FromBody] UpdateMotorcycleInputModel request)
        {
            try
            {
                if (!Guid.TryParse(id, out var guid))
                {
                    return BadRequest(new { mensagem = "Invalid Id." });
                }

                await _motorcycleService.UpdateAsync(guid, request);

                return Ok();
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotorcycleById([FromRoute] string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guid))
                {
                    return BadRequest(new { mensagem = "Invalid Id." });
                }

                var motorcycles = await _motorcycleService.GetByIdAsync(guid);

                return Ok(motorcycles);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guid))
                {
                    return BadRequest(new { mensagem = "Invalid Id." });
                }
                await _motorcycleService.DeleteAsync(guid);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
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
