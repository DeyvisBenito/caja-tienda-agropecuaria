using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.UseCases.CarritosUseCases.CarritosQuerys;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CarritoController : ControllerBase
    {
        private readonly GetCarritoByUserId getCarritoByUser;

        public CarritoController(GetCarritoByUserId getCarritoByUser)
        {
            this.getCarritoByUser = getCarritoByUser;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> Get(string userId)
        {
            try
            {
                var carrito = await getCarritoByUser.ExecuteAsync(userId);
                if (carrito is null)
                {
                    return BadRequest("Ha ocurrido un error");
                }

                return Ok(new { success = true });

            }
            catch (KeyNotFoundException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }
    }
}
