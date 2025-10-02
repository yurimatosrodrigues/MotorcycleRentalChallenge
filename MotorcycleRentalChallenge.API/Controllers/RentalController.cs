using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalChallenge.Application.InputModel;
using MotorcycleRentalChallenge.Application.Interfaces;
using MotorcycleRentalChallenge.Core.Exceptions;

namespace MotorcycleRentalChallenge.API.Controllers
{
    [ApiController]
    [Route("locacao")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddRentalInputModel request)
        {
            try
            {
                var id = await _rentalService.AddAsync(request);
                return CreatedAtAction(null, new { id }, new { id });
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            try
            {
                if(!Guid.TryParse(id, out var guid))
                {
                    return BadRequest(new { mensagem = "Invalid Id." });
                }

                var rental = await _rentalService.GetByIdAsync(guid);
                return Ok(rental);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, UpdateRentalInputModel request)
        {
            try
            {
                if (!Guid.TryParse(id, out var guid))
                {
                    return BadRequest(new { mensagem = "Invalid Id." });
                }

                var totalCost = await _rentalService.UpdateAsync(guid, request);
                return Ok(new { mensagem = "Return Date reported successfully. ", totalCost = totalCost });
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
