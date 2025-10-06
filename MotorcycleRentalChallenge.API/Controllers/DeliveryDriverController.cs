using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalChallenge.Application.InputModel;
using MotorcycleRentalChallenge.Application.Interfaces;
using MotorcycleRentalChallenge.Application.Services;
using MotorcycleRentalChallenge.Core.Exceptions;

namespace MotorcycleRentalChallenge.API.Controllers
{
    [ApiController]
    [Route("/entregadores")]
    public class DeliveryDriverController : ControllerBase
    {
        private readonly IDeliveryDriverService _deliveryDriverService;

        public DeliveryDriverController(IDeliveryDriverService deliveryDriverService)
        {
            _deliveryDriverService = deliveryDriverService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddDeliveryDriverInputModel request)
        {
            try
            {
                var id = await _deliveryDriverService.AddAsync(request);

                return Created(string.Empty, new { id });
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

        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromRoute] string id, [FromBody] CnhImageInputModel request)
        {
            try
            {
                if (!Guid.TryParse(id, out var guid))
                {
                    return BadRequest(new { mensagem = "Invalid Id." });
                }

                await _deliveryDriverService.SendCnhImageAsync(guid, request);

                return Created(string.Empty, new { });
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

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                var drivers = await _deliveryDriverService.GetAllAsync();

                return Ok(drivers);
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
